Imports System.IO

Public Class ReportForm
    Public outputDirectory As String = Form1.OutputDirectoryString()
    Public currentSubjectFolders As New List(Of String)
    Public currentReports As New Dictionary(Of String, String)
    Private Sub ReportForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        currentSubjectFolders.Clear()
        ReportListBox.Items.Clear()
        If Directory.Exists(outputDirectory) Then
            Dim folderList As String() = Directory.GetDirectories(outputDirectory)
            For i As Integer = 0 To folderList.Count - 1
                currentSubjectFolders.Add(Path.GetFileName(folderList(i)))
                Dim Suffix As String = ""
                For Each subDir As String In Directory.GetDirectories(outputDirectory & "/" & currentSubjectFolders(i))
                    If Not AnyTextFiles(subDir) Then
                        Suffix = " *missing report(s)*"
                    End If
                Next
                ReportListBox.Items.Add(currentSubjectFolders.Last & Suffix)
            Next
        End If
    End Sub
    Sub UpdateSubjectList()
        For i As Integer = 0 To currentSubjectFolders.Count - 1
            Dim Suffix As String = ""
            For Each subDir As String In Directory.GetDirectories(outputDirectory & "/" & currentSubjectFolders(i))
                If Not AnyTextFiles(subDir) Then
                    Suffix = " *missing report(s)*"
                End If
            Next
            ReportListBox.Items(i) = currentSubjectFolders(i) & Suffix
            Next
    End Sub

    Public Function AnyTextFiles(path As String) As Boolean
        If Directory.Exists(path) AndAlso Directory.GetFiles(path, "*.txt").Count > 0 Then
            Return True
        End If
        Return False
    End Function

    Private Sub ReportListBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ReportListBox.SelectedIndexChanged
        AccComboBox.Items.Clear()
        If ReportListBox.SelectedIndex <> -1 Then
            Dim subFolderList As String() = Directory.GetDirectories(outputDirectory & "/" & currentSubjectFolders(ReportListBox.SelectedIndex))
            For i As Integer = 0 To subFolderList.Count - 1
                AccComboBox.Items.Add(Path.GetFileName(subFolderList(i)))
            Next
        End If
        If AccComboBox.SelectedIndex = -1 AndAlso AccComboBox.Items.Count > 0 Then
            AccComboBox.SelectedIndex = 0
        End If
    End Sub

    Private Sub AccComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles AccComboBox.SelectedIndexChanged
        Dim folderPath As String = outputDirectory & "/" & currentSubjectFolders(ReportListBox.SelectedIndex) & "/" & AccComboBox.SelectedItem
        Dim fileList As String() = Directory.GetFiles(folderPath, "*.txt")
        currentReports.Clear()
        TextBox1.Clear()
        For f As Integer = 0 To fileList.Count - 1
            Dim reportText As String = IO.File.ReadAllText(fileList(f))
            currentReports.Add(fileList(f), reportText)
            TextBox1.Text &= reportText & vbCrLf
        Next
        Select Case fileList.Count
            Case 0 : FileNumberLbl.Text = ""
            Case 1 : FileNumberLbl.Text = "1 file"
            Case > 1 : FileNumberLbl.Text = fileList.Count & " files"
        End Select
    End Sub

    Private Sub SaveBtn_Click(sender As Object, e As EventArgs) Handles SaveBtn.Click
        If AccComboBox.SelectedIndex <> -1 And AccComboBox.SelectedIndex <> -1 Then
            If currentReports.Count = 0 Then
                Try
                    File.WriteAllText(outputDirectory & "/" & currentSubjectFolders(ReportListBox.SelectedIndex) & "/" & AccComboBox.SelectedItem & "/report.txt", TextBox1.Text)
                    currentReports.Add(outputDirectory & "/" & currentSubjectFolders(ReportListBox.SelectedIndex) & "/" & AccComboBox.SelectedItem & "/report.txt", TextBox1.Text)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            Else
                Try
                    File.WriteAllText(outputDirectory & "/" & currentSubjectFolders(ReportListBox.SelectedIndex) & "/" & AccComboBox.SelectedItem & "/report.txt", TextBox1.Text)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End If
        End If
        UpdateSubjectList()
    End Sub

    Private Sub DiscardBtn_Click(sender As Object, e As EventArgs) Handles DiscardBtn.Click
        TextBox1.Text = ""
        If currentReports.Count <> 0 Then
            For Each key As String In currentReports.Keys
                TextBox1.Text &= currentReports(key) & vbCrLf
            Next
        End If
    End Sub

    Private Sub ExploreBtn_Click(sender As Object, e As EventArgs) Handles ExploreBtn.Click
        Dim folderPath As String = outputDirectory & "\" & currentSubjectFolders(ReportListBox.SelectedIndex) & "\" & AccComboBox.SelectedItem
        If IO.Directory.Exists(folderPath) Then
            Process.Start("explorer.exe", folderPath)
        End If
    End Sub
End Class