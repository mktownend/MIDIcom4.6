Imports System.IO
Imports System.IO.Compression
Imports System.Threading
Imports Dicom
Imports Renci.SshNet
Imports Renci.SshNet.Sftp
Imports System.Security.Cryptography

Public Class FTPForm

    Public outputDirectory As String = Form1.OutputDirectoryString()
    Public tempDirectory As String = AppDomain.CurrentDomain.BaseDirectory & "/Temp/"
    Public uploadDirectory As String = AppDomain.CurrentDomain.BaseDirectory & "/Upload/"
    Public credDir As String = AppDomain.CurrentDomain.BaseDirectory & "/Resources/cred.res"

    Public ssh As New Con

    Public Class Con
        Public SFTPConnection As New SftpClient("sftp.isd.kcl.ac.uk", "username", "password")
        Public uploadThread As Thread

        Private working As Boolean = False
        Private progress As ULong = 0
        Private fileSize As Long = 0
        Private curId As String = ""
        Private state As ProcessState = ProcessState.Inactive

        Private field As New Object

        Sub ResetWorkValues()
            progress = 0
            fileSize = 0
            curId = ""
        End Sub

        Sub SetState(ps As ProcessState, Optional id As String = Nothing, Optional pr As ULong = Nothing)
            SyncLock field
                state = ps
                If Not IsNothing(id) Then
                    curId = id
                End If
                If Not IsNothing(pr) Then
                    progress = pr
                End If
            End SyncLock
        End Sub
        Function GetState() As ProcessState
            SyncLock field
                Return state
            End SyncLock
        End Function
        Sub SetProgress(uploaded As ULong)
            SyncLock field
                progress = uploaded
            End SyncLock
        End Sub
        Function GetProgress() As ULong
            SyncLock field
                Return progress
            End SyncLock
        End Function
        Sub SetFileSize(fs As String)
            SyncLock field
                fileSize = fs
            End SyncLock
        End Sub
        Function getFileSize()
            SyncLock field
                Return fileSize
            End SyncLock
        End Function
        Function GetId() As String
            SyncLock field
                Return curId
            End SyncLock
        End Function
        Sub SetWorking(w As Boolean)
            SyncLock field
                working = w
            End SyncLock
        End Sub
        Function isWorking() As Boolean
            SyncLock field
                Return working
            End SyncLock
        End Function

    End Class

    Enum ProcessState
        Inactive
        Collating
        Compressing
        Uploading
        Closing
    End Enum

    Private Sub FTPForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        SetUpEncryption()

        If File.Exists(credDir) Then
            Dim credFile = File.ReadAllText(credDir)
            credFile = Decrypt(credFile)
            If credFile.Contains("/") Then
                Dim splitCred() = credFile.Split({"/"c})
                If splitCred.Length > 1 Then
                    UserBox.Text = splitCred(0)
                    PassBox.Text = splitCred(1)
                End If
            End If
        End If

        UpdateFolderList()

    End Sub

    Sub UpdateFolderList()
        UploadListBox.Items.Clear()
        Dim folderList As String() = Directory.GetDirectories(outputDirectory)

        For i As Integer = 0 To folderList.Count - 1
            UploadListBox.Items.Add(Path.GetFileName(folderList(i)))
            UploadListBox.SetItemChecked(i, True)
        Next

    End Sub

    Private Sub DeselectBtn_Click(sender As Object, e As EventArgs) Handles DeselectBtn.Click
        For i As Integer = 0 To UploadListBox.Items.Count - 1
            UploadListBox.SetItemChecked(i, False)
        Next
    End Sub
    Private Sub SelectBtn_Click(sender As Object, e As EventArgs) Handles SelectBtn.Click
        For i As Integer = 0 To UploadListBox.Items.Count - 1
            UploadListBox.SetItemChecked(i, True)
        Next
    End Sub

    Sub CompressAndUploadFolders()

        Try
            If Directory.Exists(tempDirectory) Then
                Directory.Delete(tempDirectory, True)
            End If

            For Each subjID As Integer In UploadListBox.CheckedIndices

                If ssh.GetState = ProcessState.Closing Then
                    ThreadClose()
                    Exit Sub
                End If

                Directory.CreateDirectory(tempDirectory)

                Dim inputDirectory As String = outputDirectory & "/" & UploadListBox.Items(subjID)

                ssh.SetState(ProcessState.Collating, UploadListBox.Items(subjID))

                For Each accFolder As String In Directory.GetDirectories(inputDirectory)
                    Dim accSubFolder As String = Path.GetFileName(accFolder)
                    Directory.CreateDirectory(tempDirectory & "/" & accSubFolder)
                    If File.Exists(inputDirectory & "/" & accFolder & "/report.txt") Then
                        File.Copy(inputDirectory & "/" & accFolder & "/report.txt", tempDirectory & "/" & accSubFolder & "/report.txt")
                    End If
                    For Each serFolder As String In Directory.GetDirectories(accFolder)
                        Dim serSubFolder As String = Path.GetFileName(serFolder)
                        Directory.CreateDirectory(tempDirectory & "/" & accSubFolder & "/" & serSubFolder)
                        For Each dcmFile As String In Directory.GetFiles(serFolder, "*.DCM")
                            Dim dcmHead As DicomFile = DicomFile.Open(dcmFile, FileReadOption.SkipLargeTags)
                            If dcmHead.Dataset.Contains(DicomTag.PatientIdentityRemoved) AndAlso dcmHead.Dataset.GetString(DicomTag.PatientIdentityRemoved).ToUpper = "YES" Then
                                File.Copy(dcmFile, tempDirectory & "/" & accSubFolder & "/" & serSubFolder & "/" & Path.GetFileName(dcmFile))
                                If ssh.GetState = ProcessState.Closing Then
                                    ThreadClose()
                                    Exit Sub
                                End If
                            End If
                        Next
                    Next
                Next

                ssh.SetState(ProcessState.Compressing)

                Dim outputFile As String = uploadDirectory & "/" & UploadListBox.Items(subjID) & ".zip"

                If Not Directory.Exists(uploadDirectory) Then
                    Directory.CreateDirectory(uploadDirectory)
                End If
                If File.Exists(outputFile) Then
                    File.Delete(outputFile)
                End If

                ZipFile.CreateFromDirectory(tempDirectory, outputFile)

                Dim fs As New FileInfo(outputFile)
                ssh.SetFileSize(fs.Length)

                If ssh.GetState = ProcessState.Closing Then
                    ThreadClose()
                    Exit Sub
                End If

                ssh.SetState(ProcessState.Uploading)

                Try
                    UploadFile(outputFile, ssh.SFTPConnection)
                Catch ex As Exception
                    Select Case ex.Message
                        Case "Thread was being aborted."

                        Case Else
                            MsgBox(ex.Message)
                    End Select
                    ' QuenchConnection()
                    ThreadClose()
                    Return
                End Try

                If ssh.GetState = ProcessState.Closing Then
                    ThreadClose()
                    Exit Sub
                End If

                If deleteBox.Checked Then
                    Directory.Delete(inputDirectory, True)
                End If

                Directory.Delete(tempDirectory, True)
                File.Delete(outputFile)

            Next
        Catch ex As Exception
            MsgBox(ex.Message)
            ThreadClose()
            Exit Sub
        End Try

        ssh.SetState(ProcessState.Inactive)
        ssh.SetWorking(False)

    End Sub

    Sub ThreadClose()
        ' QuenchConnection()
        ssh.SFTPConnection.Disconnect()
        ssh.SetWorking(False)
        ssh.SetState(ProcessState.Inactive)
    End Sub

    Sub UploadFile(inputFile As String, ftp As SftpClient)
        If File.Exists(inputFile) And ftp.IsConnected Then
            Using inputStream = File.Open(inputFile, FileMode.Open)
                ftp.UploadFile(inputStream, Path.GetFileName(inputFile), AddressOf ssh.SetProgress)
            End Using
        End If
    End Sub

    'Private Sub ConnectBtn_Click(sender As Object, e As EventArgs)
    '    If ConnectBtn.Text = "Connect" Then
    '        Try
    '            ssh.SFTPConnection = New SftpClient("sftp.isd.kcl.ac.uk", UserBox.Text, PassBox.Text)
    '            ssh.SFTPConnection.Connect()
    '            ConnectBtn.Text = "Connected"
    '            UploadBox.Enabled = True
    '            Try
    '                File.WriteAllText(credDir, Encrypt(UserBox.Text & "/" & PassBox.Text))
    '            Catch ex As Exception
    '            End Try
    '        Catch ex As Exception
    '            MsgBox(ex.Message)
    '        End Try
    '    Else
    '        QuenchConnection()
    '    End If
    'End Sub

    Sub QuenchConnection()
        ssh.ResetWorkValues()
        ssh.SetWorking(False)
        ssh.SetState(ProcessState.Closing)
        UpdateProgress()
        UpdateFolderList()
        ClearBar()
        'ConnectBtn.Text = "Connect"
        UploadBtn.Text = "Upload"
    End Sub

    'Sub UploadComplete()
    '    ssh.ResetWorkValues()
    '    ssh.SetState(ProcessState.Inactive)
    '    UpdateProgress()
    '    UploadBtn.Text = "Upload"
    '    UpdateFolderList()
    '    ClearBar()
    'End Sub

    Private Sub UploadBtn_Click(sender As Object, e As EventArgs) Handles UploadBtn.Click
        Dim succesfulUploads = 0
        Try
            If ssh.isWorking Then
                QuenchConnection()
            Else
                ssh.SetWorking(True)
                UploadBtn.Text = "Cancel"

                StateTxt.Text = "Connecting..."

                Try
                    ssh.SFTPConnection = New SftpClient("sftp.isd.kcl.ac.uk", UserBox.Text, PassBox.Text)
                    ssh.SFTPConnection.Connect()
                Catch ex As Exception
                    MsgBox(ex.Message)
                    QuenchConnection()
                    Return
                End Try

                Try
                    File.WriteAllText(credDir, Encrypt(UserBox.Text & "/" & PassBox.Text))
                Catch ex As Exception
                End Try

                'updateLabel.Text = "Processing " & UploadListBox.Items(subjID) & "..."

                ssh.uploadThread = New Thread(New ThreadStart(AddressOf CompressAndUploadFolders))

                ssh.uploadThread.Start()

                While ssh.isWorking()
                    UpdateProgress()
                    Application.DoEvents()
                End While

                ssh.SFTPConnection.Disconnect()
                MsgBox("Upload complete.")

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
            QuenchConnection()
            Return
        End Try

        ssh.SetWorking(False)
        QuenchConnection()

    End Sub

    Sub UpdateProgress()
        If ssh.GetState = ProcessState.Inactive Then
            StateTxt.Text = "Inactive"
            IdTxt.Text = ""
            ClearBar()
        Else
            IdTxt.Text = ssh.GetId()
            Select Case ssh.GetState
                Case ProcessState.Collating
                    StateTxt.Text = "Collating..."
                Case ProcessState.Compressing
                    StateTxt.Text = "Compressing..."
                Case ProcessState.Uploading
                    StateTxt.Text = "Uploading..."
                    UpdateBar()
                Case ProcessState.Closing
                    StateTxt.Text = "Inactive"
                    If ssh.isWorking = False Then
                        UploadBtn.Text = "Upload"
                        ClearBar()
                        UpdateFolderList()
                    End If
            End Select
        End If
    End Sub

    Sub UpdateBar()
        ProgressBar.Maximum = CInt(ssh.getFileSize)
        ProgressBar.Value = CInt(ssh.GetProgress)
    End Sub

    Sub ClearBar()
        ProgressBar.Maximum = 1
        ProgressBar.Value = 0
    End Sub

End Class