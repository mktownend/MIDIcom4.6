Imports System
Imports System.IO
Imports System.IO.Compression
Imports System.Diagnostics
Imports Dicom
Imports NPOI.XSSF.UserModel
Imports NPOI.SS.UserModel
Imports NPOI.HPSF
Imports NPOI.POIFS.FileSystem
Imports System.Xml.Serialization

Public Class Form1

    Dim inputFolderDlg As FolderBrowserDialog
    Dim outputFolderDlg As FolderBrowserDialog

    Dim outputFileDlg As SaveFileDialog

    Dim baseDirectory As String = AppDomain.CurrentDomain.BaseDirectory()
    Dim configPath As String = AppDomain.CurrentDomain.BaseDirectory & "\Resources\config.txt"

    Public knownSubjects As New Dictionary(Of String, Subject)

    Dim writeIDs As Boolean = True

    Public cf As New ConfigSettings

    Dim working As Boolean = False
    Dim totalAnonymised As Integer = 0
    Dim totalFailed As Integer = 0
    Dim skippedNoID As Integer = 0
    Dim skippedUnknownID As Integer = 0

    Dim curAccFolder As String = ""
    Dim accWithReports As New List(Of String)

    Public logAccs As Boolean = True

    Dim zip_object As New Object
    Dim zip_active As Integer = 0

    Enum ReportScoutMode
        NONE = -1
        SECTRA
        CARESTREAM
    End Enum

    Function NewStudyID() As String
        Dim newID As Integer = 1
        Dim usedIDs As New List(Of Integer)
        For Each key As String In knownSubjects.Keys
            usedIDs.Add(Int(knownSubjects(key).studyID))
        Next
        If cf.reuseStudyID Then
            Do
                newID += 1
            Loop While usedIDs.Contains(newID)
        Else
            For Each usedID In usedIDs
                If newID <= usedID Then
                    newID = usedID + 1
                End If
            Next
        End If
        Return Format(newID, cf.IDFormat)
    End Function

    Public Function OutputDirectoryString() As String
        Return outputFolderDlg.SelectedPath & ReplaceInvalidChars(cf.studyCode) & "\" & ReplaceInvalidChars(cf.centreCode)
    End Function

    Public Class ConfigSettings
        Public configKeys As New Dictionary(Of String, String)
        Public studyCode As String = "NOCODE"
        Public centreCode As String = "NOCODE"
        Public siteName As String = "NOTSET"
        Public principalInv As String = "NOTSET"
        Public IDFormat As String = "0000"
        Public enrolFilePath As String = ""
        Public ignoreUnassigned As Boolean = False
        Public ignoreNoID As Boolean = False
        Public reuseStudyID As Boolean = False
        Public saveTxt As Boolean = False
        Public backupFiles As Boolean = False

        Public setDialog As New SettingsForm

        Dim configLines() As String

        Public Sub CreateDefaultTxt(path As String)
            If IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory & "\Resources\config.res") Then
                configLines = IO.File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory & "\Resources\config.res")
                IO.File.WriteAllLines(path, configLines)
            End If
        End Sub
        Public Sub LoadFromTxt(path As String)
            If IO.File.Exists(path) Then
                configLines = IO.File.ReadAllLines(path)
                For Each keyPair As String In configLines
                    keyPair = keyPair.Trim()
                    If (Not (keyPair = "" Or keyPair.Contains("#"))) And keyPair.Contains("=") Then
                        Dim contents() As String = keyPair.Split("=")
                        If contents.Length > 1 Then
                            configKeys.Add(contents(0).ToUpper, contents(1).ToUpper)
                            Select Case configKeys.Last.Key
                                Case "STUDYCODE"
                                    studyCode = configKeys.Last.Value
                                Case "CENTRECODE"
                                    centreCode = configKeys.Last.Value
                                Case "SITENAME"
                                    siteName = configKeys.Last.Value
                                Case "PRINCIPALINV"
                                    principalInv = configKeys.Last.Value
                                Case "IDFORMAT"
                                    IDFormat = configKeys.Last.Value
                                Case "IGNORENOID"
                                    If configKeys.Last.Value = "TRUE" Then : ignoreNoID = True
                                    End If
                                Case "IGNOREUNASSIGNED"
                                    If configKeys.Last.Value = "TRUE" Then : ignoreUnassigned = True
                                    End If
                                Case "REUSESTUDYID"
                                    If configKeys.Last.Value = "TRUE" Then : reuseStudyID = True
                                    End If
                                Case "BACKUPFILES"
                                    If configKeys.Last.Value = "TRUE" Then : backupFiles = True
                                    End If
                                Case "SAVETXT"
                                    If configKeys.Last.Value = "TRUE" Then : saveTxt = True
                                    End If
                            End Select
                        End If
                    End If
                Next
                If configKeys.ContainsKey("ENROLFILEPATH") Then
                    If Not File.Exists(configKeys("ENROLFILEPATH")) Then
                        SelectEnrolFile()
                    Else
                        enrolFilePath = configKeys("ENROLFILEPATH")
                    End If
                End If
            Else
                CreateDefaultTxt(path)
                LoadFromTxt(path)
            End If
            SaveToTxt(path)
        End Sub
        Public Sub SaveToTxt(path As String)
            For l As Integer = 0 To configLines.Count - 1
                Dim line As String = configLines(l).Trim
                If (Not (line = "" Or line.Contains("#"))) And line.Contains("=") Then
                    Dim key As String = line.Split("=")(0).ToUpper
                    If key.Contains("STUDYCODE") Then
                        configLines(l) = "STUDYCODE=" & studyCode.Trim.ToUpper
                    ElseIf key.Contains("CENTRECODE") Then
                        configLines(l) = "CENTRECODE=" & centreCode.Trim.ToUpper
                    ElseIf key.Contains("SITENAME") Then
                        configLines(l) = "SITENAME=" & siteName.Trim.ToUpper
                    ElseIf key.Contains("PRINCIPALINV") Then
                        configLines(l) = "PRINCIPALINV=" & principalInv.Trim.ToUpper
                    ElseIf key.Contains("IDFORMAT") Then
                        configLines(l) = "IDFORMAT=" & IDFormat.Trim.ToUpper
                    ElseIf key.Contains("IGNORENOID") Then
                        If ignoreNoID Then
                            configLines(l) = "IGNORENOID=TRUE"
                        Else
                            configLines(l) = "IGNORENOID=FALSE"
                        End If
                    ElseIf key.Contains("IGNOREUNASSIGNED") Then
                        If ignoreUnassigned Then
                            configLines(l) = "IGNOREUNASSIGNED=TRUE"
                        Else
                            configLines(l) = "IGNOREUNASSIGNED=FALSE"
                        End If
                    ElseIf key.Contains("REUSESTUDYID") Then
                        If reuseStudyID Then
                            configLines(l) = "REUSESTUDYID=TRUE"
                        Else
                            configLines(l) = "REUSESTUDYID=FALSE"
                        End If
                    ElseIf key.Contains("BACKUPFILES") Then
                        If backupFiles Then
                            configLines(l) = "BACKUPFILES=TRUE"
                        Else
                            configLines(l) = "BACKUPFILES=FALSE"
                        End If
                    ElseIf key.Contains("SAVETXT") Then
                        If saveTxt Then
                            configLines(l) = "SAVETXT=TRUE"
                        Else
                            configLines(l) = "SAVETXT=FALSE"
                        End If
                    ElseIf key.Contains("ENROLFILEPATH") Then
                        configLines(l) = "ENROLFILEPATH=" & enrolFilePath.Trim.ToUpper
                    End If
                End If
            Next

            IO.File.WriteAllLines(path, configLines)

        End Sub
        Public Sub SetDialogInfo()
            With setDialog
                .STUDYCODEtxt.Text = studyCode
                .CENTRECODEtxt.Text = centreCode
                .SITENAMEtxt.Text = siteName
                .PRINCIPALINVtxt.Text = principalInv

                .IDFORMATtxt.Text = IDFormat
                .ENROLFILEPATHtxt.Text = enrolFilePath
                .BACKUPFILESbox.Checked = backupFiles
                .SAVETXTbox.Checked = saveTxt

                .REUSESTUDYIDbox.Checked = reuseStudyID
                .IGNOREUNASSIGNEDbox.Checked = ignoreUnassigned
                .IGNORENOIDbox.Checked = ignoreNoID
            End With
        End Sub
        Public Sub SaveDialogInfo()
            With setDialog
                studyCode = .STUDYCODEtxt.Text
                centreCode = .CENTRECODEtxt.Text
                siteName = .SITENAMEtxt.Text
                principalInv = .PRINCIPALINVtxt.Text

                IDFormat = .IDFORMATtxt.Text
                enrolFilePath = .ENROLFILEPATHtxt.Text
                backupFiles = .BACKUPFILESbox.Checked
                saveTxt = .SAVETXTbox.Checked

                reuseStudyID = .REUSESTUDYIDbox.Checked
                ignoreUnassigned = .IGNOREUNASSIGNEDbox.Checked
                ignoreNoID = .IGNORENOIDbox.Checked

            End With
        End Sub
        Public Sub SelectEnrolFile()
            Dim oFD As New OpenFileDialog With {
                .Filter = "Excel file|*.xlsx",
                .Title = "Select default patient enrolment file...",
                .InitialDirectory = AppDomain.CurrentDomain.BaseDirectory & "Resources"
            }
            If oFD.ShowDialog = DialogResult.OK Then
                enrolFilePath = oFD.FileName

                If enrolFilePath.Contains(AppDomain.CurrentDomain.BaseDirectory) Then
                    enrolFilePath = enrolFilePath.Replace(AppDomain.CurrentDomain.BaseDirectory, ".\")
                End If

                If configKeys.Keys.Contains("ENROLFILEPATH") Then
                    configKeys("ENROLFILEPATH") = oFD.FileName
                Else
                    configKeys.Add("ENROLFILEPATH", oFD.FileName)
                End If
            Else
                enrolFilePath = ".\Resources\enrolment_log.xlsx"
                If configKeys.Keys.Contains("ENROLFILEPATH") Then
                    configKeys("ENROLFILEPATH") = enrolFilePath
                Else
                    configKeys.Add("ENROLFILEPATH", enrolFilePath)
                End If
            End If
            If Not File.Exists(enrolFilePath) Then
                CreateEnrolFile(enrolFilePath)
            End If
        End Sub
        Public Sub CreateEnrolFile(FilePath As String)

            Try

                Using readStream As New FileStream(AppDomain.CurrentDomain.BaseDirectory & "\Resources\enrol.res", IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.ReadWrite)

                    Dim xl As New XSSFWorkbook(readStream)
                    Dim studySheet As ISheet = xl.GetSheetAt(0)

                    studySheet.GetRow(2).Cells(2).SetCellValue(siteName)
                    studySheet.GetRow(3).Cells(2).SetCellValue(principalInv)

                    Using writeStream As New FileStream(FilePath, FileMode.Create)
                        xl.Write(writeStream)
                    End Using

                End Using

            Catch ex As Exception

                MsgBox(ex.GetBaseException.Message)

            End Try

        End Sub
    End Class

    Public Class Subject
        Public nhsID As String
        Public studyID As String
        Public initials As String
        Public birthDate As String
        Public imageDate As String
        Public accs As Dictionary(Of String, Accession)
        Sub New(nhs As String, study As String, Optional ini As String = "NONE", Optional birth As String = "NONE", Optional image As String = "NONE")
            nhsID = nhs
            studyID = study

            initials = ini
            birthDate = birth
            imageDate = image

            accs = New Dictionary(Of String, Accession)
        End Sub

        'Sub WriteID(idWriter As IO.StreamWriter)
        '    Try
        '        idWriter.WriteLine(nhsID & "," & studyID & "," & initials & "," & birthDate & "," & imageDate)
        '        idWriter.Flush()
        '    Catch ex As Exception
        '        Form1.WriteLog("Unable to save new subject ID pair!")
        '    End Try
        'End Sub
        Public Function DcmImageDate() As String
            Dim iD As Date
            If imageDate.Length = 8 And IsNumeric(imageDate) Then
                Return imageDate.Trim()
            ElseIf (Date.TryParse(imageDate, iD)) Then
                Return Str(iD.ToString("yyyyMMdd")).Trim()
            Else
                Return "19000101"
            End If
        End Function
    End Class

    Public Class Accession
        Public id As String
        Public scan_date As String
        Public series_descs As List(Of String)
        Sub New(accID As String, sDate As String, descs() As String)
            id = accID
            scan_date = sDate
            series_descs = New List(Of String)
            For Each desc In descs
                If desc <> "" And Not series_descs.Contains(desc) Then
                    series_descs.AddRange(descs)
                End If
            Next
        End Sub
        Sub New(accID As String, sDate As String, desc As String)
            id = accID
            scan_date = sDate
            series_descs = New List(Of String)
            If desc <> "" Then
                series_descs.Add(desc)
            End If
        End Sub
        Sub New(accID As String, sDate As String)
            id = accID
            scan_date = sDate
            series_descs = New List(Of String)
        End Sub
        Function Get_String() As String
            Dim return_val As String = id & vbCrLf & scan_date
            series_descs.Sort()
            For Each desc In series_descs
                return_val &= vbCrLf & vbTab & desc
            Next
            Return return_val
        End Function
    End Class

    Public Sub CheckCreateDirectory(path As String)
        Try
            If Not Directory.Exists(path) Then
                Directory.CreateDirectory(path)
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Dim enhanced_profile = Dicom.DicomAnonymizer.SecurityProfile.LoadProfile(Nothing, DicomAnonymizer.SecurityProfileOptions.BasicProfile _
                                                                             Or DicomAnonymizer.SecurityProfileOptions.RetainSafePrivate _
                                                                             Or DicomAnonymizer.SecurityProfileOptions.RetainDeviceIdent _
                                                                             Or DicomAnonymizer.SecurityProfileOptions.CleanDesc)
    Dim enhanced_anonymiser = New Dicom.DicomAnonymizer(enhanced_profile)
    Public Sub AnonymiseDCMFile(ByRef dcmFile As DicomFile, Optional patient_ID As String = "")

        Dim accessionNumber As String = ""
        Dim seriesNumber As String = ""
        Dim patientWeight As String = ""
        Dim patientAge As String = ""
        Dim patientSex As String = ""
        Dim seriesDesc As String = ""
        Dim birthDate As String = ""
        Dim scanDate As String = ""

        dcmFile.Dataset.TryGetString(DicomTag.AccessionNumber, accessionNumber)
        dcmFile.Dataset.TryGetString(DicomTag.SeriesNumber, seriesNumber)
        dcmFile.Dataset.TryGetString(DicomTag.PatientWeight, patientWeight)
        dcmFile.Dataset.TryGetString(DicomTag.PatientAge, patientAge)
        dcmFile.Dataset.TryGetString(DicomTag.PatientSex, patientSex)
        dcmFile.Dataset.TryGetString(DicomTag.SeriesDescription, seriesDesc)
        dcmFile.Dataset.TryGetString(DicomTag.StudyDate, scanDate)
        dcmFile.Dataset.TryGetString(DicomTag.PatientBirthDate, birthDate)

        enhanced_anonymiser.AnonymizeInPlace(dcmFile)

        dcmFile.Dataset.AddOrUpdate(DicomTag.AccessionNumber, accessionNumber)
        dcmFile.Dataset.AddOrUpdate(DicomTag.SeriesNumber, seriesNumber)
        dcmFile.Dataset.AddOrUpdate(DicomTag.PatientID, patient_ID)
        dcmFile.Dataset.AddOrUpdate(DicomTag.PatientWeight, patientWeight)
        dcmFile.Dataset.AddOrUpdate(DicomTag.PatientAge, patientAge)
        dcmFile.Dataset.AddOrUpdate(DicomTag.PatientSex, patientSex)
        dcmFile.Dataset.AddOrUpdate(DicomTag.SeriesDescription, seriesDesc)
        dcmFile.Dataset.AddOrUpdate(DicomTag.StudyDate, scanDate)
        dcmFile.Dataset.AddOrUpdate(DicomTag.PatientBirthDate, birthDate)

        dcmFile.Dataset.AddOrUpdate(DicomTag.StudyTime, "000000")
        dcmFile.Dataset.AddOrUpdate(DicomTag.ReferringPhysicianName, "ANONYMOUS")
        dcmFile.Dataset.AddOrUpdate(DicomTag.PatientName, "ANONYMOUS")
        dcmFile.Dataset.AddOrUpdate(DicomTag.PatientAddress, "ANONYMOUS")

        dcmFile.Dataset.Remove(DicomTag.PatientAlternativeCalendar)
        dcmFile.Dataset.Remove(DicomTag.PatientBirthName)
        dcmFile.Dataset.Remove(DicomTag.PatientBirthTime)
        dcmFile.Dataset.Remove(DicomTag.PatientBodyMassIndex)
        dcmFile.Dataset.Remove(DicomTag.PatientBreedCodeSequence)
        dcmFile.Dataset.Remove(DicomTag.PatientBreedDescription)
        dcmFile.Dataset.Remove(DicomTag.PatientClinicalTrialParticipationSequence)
        dcmFile.Dataset.Remove(DicomTag.PatientComments)
        dcmFile.Dataset.Remove(DicomTag.PatientDeathDateInAlternativeCalendar)
        dcmFile.Dataset.Remove(DicomTag.PatientEquipmentRelationshipCodeSequence)
        dcmFile.Dataset.Remove(DicomTag.PatientInstitutionResidence)
        dcmFile.Dataset.Remove(DicomTag.PatientInsurancePlanCodeSequence)
        dcmFile.Dataset.Remove(DicomTag.PatientLocationCoordinatesCodeSequence)
        dcmFile.Dataset.Remove(DicomTag.PatientLocationCoordinatesSequence)
        dcmFile.Dataset.Remove(DicomTag.PatientMotherBirthName)
        dcmFile.Dataset.Remove(DicomTag.PatientPrimaryLanguageCodeSequence)
        dcmFile.Dataset.Remove(DicomTag.PatientPrimaryLanguageModifierCodeSequence)
        dcmFile.Dataset.Remove(DicomTag.PatientReligiousPreference)
        dcmFile.Dataset.Remove(DicomTag.PatientSetupLabel)
        dcmFile.Dataset.Remove(DicomTag.PatientSetupNumber)
        dcmFile.Dataset.Remove(DicomTag.PatientSexNeutered)
        dcmFile.Dataset.Remove(DicomTag.PatientSize)
        dcmFile.Dataset.Remove(DicomTag.PatientSizeCodeSequence)
        dcmFile.Dataset.Remove(DicomTag.PatientSpeciesCodeSequence)
        dcmFile.Dataset.Remove(DicomTag.PatientSpeciesDescription)
        dcmFile.Dataset.Remove(DicomTag.PatientTelecomInformation)
        dcmFile.Dataset.Remove(DicomTag.PatientTelephoneNumbers)
        dcmFile.Dataset.Remove(DicomTag.CurrentPatientLocation)
        dcmFile.Dataset.Remove(DicomTag.AdditionalPatientHistory)
        dcmFile.Dataset.Remove(DicomTag.GroupOfPatientsIdentificationSequence)
        dcmFile.Dataset.Remove(DicomTag.OtherPatientIDsSequence)
        dcmFile.Dataset.Remove(DicomTag.OtherPatientNames)
        dcmFile.Dataset.Remove(DicomTag.OtherPatientIDsRETIRED)
        dcmFile.Dataset.Remove(DicomTag.ResponsibleOrganization)
        dcmFile.Dataset.Remove(DicomTag.ResponsiblePerson)
        dcmFile.Dataset.Remove(DicomTag.ResponsiblePersonRole)
        dcmFile.Dataset.Remove(DicomTag.ConsultingPhysicianName)

        dcmFile.Dataset.AddOrUpdate(DicomTag.PatientIdentityRemoved, "YES")

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        CheckCreateDirectory(baseDirectory & "Input\")
        CheckCreateDirectory(baseDirectory & "Output\")
        CheckCreateDirectory(baseDirectory & "Debug Logs\")

        inputFolderDlg = New FolderBrowserDialog With {
            .Description = "Select input directory to convert...",
            .SelectedPath = baseDirectory & "Input\"
        }
        outputFolderDlg = New FolderBrowserDialog With {
            .Description = "Select output directory for anonymised files...",
            .SelectedPath = baseDirectory & "Output\"
        }
        outputFileDlg = New SaveFileDialog With {
            .InitialDirectory = baseDirectory,
            .Title = "Save ID file..."
        }

        Dim firstLaunch As Boolean = False

        If Not File.Exists(configPath) Then
            firstLaunch = True

            cf.setDialog.descText = "This appears to be the first time you've launched this application

Please review and enter correct details for your local site within the 'Study Information' settings

This helps ensure that all data is processed in a well organised way

If you have any queries about these settings please contact the MIDI team"

        End If

        cf.LoadFromTxt(configPath)

        If firstLaunch Then
            SettingsToolMenu()
        End If

        If File.Exists(cf.enrolFilePath) Then
            ReadExcelFile(cf.enrolFilePath)
        End If

        UpdateMainFormUI()

    End Sub

    Private Sub InputToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InputFolderBtn.Click
        If inputFolderDlg.ShowDialog() = DialogResult.OK Then
            InputFolderTxt.Text = inputFolderDlg.SelectedPath
        End If
    End Sub

    Private Sub OutputToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OutputFolderBtn.Click
        If outputFolderDlg.ShowDialog() = DialogResult.OK Then
            OutputFolderTxt.Text = outputFolderDlg.SelectedPath
        End If
    End Sub

    Public Sub WorkSummary()
        WriteLog("***")
        WriteLog("Total files anonymised: " & totalAnonymised)
        WriteLog("Total files skipped due to blank IDs: " & skippedNoID)
        WriteLog("Total files skipped due to unknown IDs: " & skippedUnknownID)
        WriteLog("Total files failed: " & totalFailed)
        WriteLog("***")
        SaveLog(DateAndTime.Now.Day & "_" & DateAndTime.Now.Month & "_" & "_" & DateAndTime.Now.Hour & "_" & DateAndTime.Now.Minute & "_" & DateAndTime.Now.Second)
    End Sub
    Private Sub zip_folders()
        Do
            Dim folder_path As String = ""
            SyncLock zip_object
                If folders_to_zip.Count > 0 Then
                    folder_path = folders_to_zip.First()
                    folders_to_zip.RemoveAt(0)
                ElseIf halt_zipping Then
                    zip_active -= 1
                    Return
                End If
            End SyncLock

            If folder_path <> "" Then
                Dim zip_path = folder_path
                If zip_path.EndsWith("/") Or zip_path.EndsWith("\") Then
                    zip_path = zip_path.Substring(0, zip_path.Length - 1)
                End If
                zip_path &= ".zip"

                If Directory.Exists(folder_path) And Not File.Exists(zip_path) Then
                    ZipFile.CreateFromDirectory(folder_path, zip_path)
                End If

                If File.Exists(zip_path) Then
                    Try
                        Directory.Delete(folder_path, True)
                    Catch ex As Exception

                    End Try
                End If

            End If

            Threading.Thread.Sleep(300)
        Loop
    End Sub
    Private Sub ConvertBtn_Click(sender As Object, e As EventArgs) Handles ConvertBtn.Click
        If working = False Then
            skippedNoID = 0
            skippedUnknownID = 0
            totalAnonymised = 0
            totalFailed = 0

            If zip_check.Checked = True Then
                halt_zipping = False
                If zip_check.Checked Then
                    For t As Integer = 1 To num_zip_threads
                        zipping_threads.Add(New Threading.Thread(AddressOf zip_folders))
                        zipping_threads.Last.Start()
                    Next
                End If
            End If

            If inputFolderDlg.SelectedPath = outputFolderDlg.SelectedPath Then
                If inputFolderDlg.ShowDialog() = DialogResult.OK AndAlso inputFolderDlg.SelectedPath <> outputFolderDlg.SelectedPath Then
                    working = True
                    UpdateMainFormUI()
                    'LaunchStreamwriter()
                    ProcessFolder("", True)
                    'DisposeStreamwriter()
                    working = False
                    SyncLock zip_object
                        halt_zipping = True
                    End SyncLock
                    UpdateMainFormUI()

                    WorkSummary()
                End If
            Else
                working = True
                UpdateMainFormUI()
                ConvertBtn.Text = "Stop"
                'LaunchStreamwriter()
                ProcessFolder("", True)
                'DisposeStreamwriter()
                working = False
                SyncLock zip_object
                    halt_zipping = True
                End SyncLock
                UpdateMainFormUI()

                WorkSummary()
            End If

            Save_Enrolment_Log()

        Else
            working = False
            SyncLock zip_object
                halt_zipping = True
            End SyncLock
            UpdateMainFormUI()

            WorkSummary()
        End If
    End Sub
    Public Sub Save_Enrolment_Log()
        WriteExcelFile(cf.enrolFilePath)
        If cf.saveTxt Then
            WriteTxtFile(cf.enrolFilePath & ".txt")
        End If
    End Sub
    Public Sub WriteLog(input As String, Optional msg As Boolean = False)
        If LogTxt.Enabled = False Then
            LogTxt.Enabled = True
        End If
        LogTxt.AppendText(input & vbCrLf)
        If msg Then
            MsgBox(input)
        End If
    End Sub

    Sub SaveLog(name As String)
        Dim savePath As String = baseDirectory & "/Debug Logs/" & name & ".txt"
        If Not File.Exists(savePath) Then
            Try
                IO.File.WriteAllLines(savePath, LogTxt.Text.Split(vbCrLf))
                WriteLog("***  ***  Log file saved  ***  ***")
            Catch ex As Exception
                WriteLog("***  ***  Log file not saved - unable to write  ***  ***")
            End Try
        End If
    End Sub

    Function Ip(subFolder As String) As String
        Return inputFolderDlg.SelectedPath & subFolder
    End Function
    Function Op(subFolder As String) As String
        Return outputFolderDlg.SelectedPath & subFolder
    End Function
    Public Function ReplaceInvalidChars(filename As String) As String
        Return String.Join("_", filename.Split(Path.GetInvalidFileNameChars()))
    End Function

    Function LogDcmFile(path As String, Optional logID As Boolean = True) As String

        Dim fileName As String = New DirectoryInfo(path).Name

        Dim dcmFile As DicomFile = DicomFile.Open(path, FileReadOption.ReadLargeOnDemand)

        Dim subjID As String = "NOSUBJID"
        Dim accID As String = "NOACCID"
        Dim serID As String = "NOSERID"
        Dim dcmBirth As String = "NODOB"
        Dim dcmDate As String = "NODATE"
        Dim dcmName As String = ""
        Dim dcmInit As String = ""
        Dim serDesc As String = ""

        If dcmFile.Dataset.Contains(DicomTag.PatientID) Then
            subjID = dcmFile.Dataset.GetString(DicomTag.PatientID).ToUpper
            If subjID = "" Then
                subjID = "NOSUBJID"
            End If
        End If
        If dcmFile.Dataset.Contains(DicomTag.AccessionNumber) Then
            accID = dcmFile.Dataset.GetString(DicomTag.AccessionNumber).ToUpper
            If accID = "" Then
                accID = "NOACCID"
            End If
        End If
        If dcmFile.Dataset.Contains(DicomTag.SeriesNumber) Then
            serID = dcmFile.Dataset.GetString(DicomTag.SeriesNumber).ToUpper
            If serID = "" Then
                serID = "NOSERID"
            End If
        End If
        If dcmFile.Dataset.Contains(DicomTag.SeriesDescription) Then
            serDesc = dcmFile.Dataset.GetString(DicomTag.SeriesDescription).ToUpper
        End If
        If dcmFile.Dataset.Contains(DicomTag.PatientBirthDate) Then
            dcmBirth = dcmFile.Dataset.GetString(DicomTag.PatientBirthDate).ToUpper
            If dcmBirth = "" Then
                dcmBirth = "NODATE"
            End If
        End If
        If dcmFile.Dataset.Contains(DicomTag.AcquisitionDate) Then
            dcmDate = dcmFile.Dataset.GetString(DicomTag.AcquisitionDate).ToUpper
            If dcmDate = "" Then
                dcmDate = "NODATE"
            End If
        End If
        If dcmFile.Dataset.Contains(DicomTag.PatientName) Then
            dcmName = dcmFile.Dataset.GetString(DicomTag.PatientName).ToUpper
            If dcmName <> "" Then
                Dim subNames As String() = dcmName.Split({"^"c})
                If subNames.Count > 1 Then
                    If subNames(0) <> "" And subNames(1) <> "" Then
                        dcmInit = subNames(1)(0) & subNames(0)(0)
                    End If
                Else
                    For Each subName As String In subNames
                        dcmInit &= subName(0)
                    Next
                End If
            Else
                dcmInit = "NOINIT"
            End If
        Else
            dcmInit = "NOINIT"
        End If

        If subjID = "NOSUBJID" And cf.ignoreNoID Then
            skippedNoID += 1
            WriteLog("Skipping DCM with no subject ID")
            Return accID
        End If

        Dim newSubjStudyID As String

        WriteLog("Subject ID: " & subjID)

        If knownSubjects.ContainsKey(subjID) Then
            WriteLog("Found file with known subject ID " & subjID)
            newSubjStudyID = knownSubjects(subjID).studyID

            With knownSubjects(subjID)
                If .birthDate = "NODATE" Or .birthDate = "" Then
                    If dcmBirth <> "NODATE" And dcmBirth <> "" Then
                        .birthDate = dcmBirth
                    End If
                End If
                If .imageDate = "NODATE" Or .imageDate = "" Then
                    If dcmDate <> "NODATE" And dcmDate <> "" Then
                        .imageDate = dcmDate
                    End If
                End If
                If .initials = "NOINIT" Or .initials = "" Then
                    If dcmInit <> "NOINIT" And dcmInit <> "" Then
                        .initials = dcmInit
                    End If
                End If

                If Not .accs.Keys.Contains(accID) Then
                    .accs.Add(accID, New Accession(accID, dcmDate))
                End If
                If serDesc <> "" Then
                    If Not .accs(accID).series_descs.Contains(serDesc) Then
                        .accs(accID).series_descs.Add(serDesc)
                    End If
                End If

            End With

        Else

            WriteLog("Found file with unknown subject ID " & subjID)

            If Not cf.ignoreUnassigned Then

                newSubjStudyID = NewStudyID()

                knownSubjects.Add(subjID, New Subject(subjID, newSubjStudyID, dcmInit, dcmBirth, dcmDate))

                With knownSubjects(subjID)

                    If Not .accs.Keys.Contains(accID) Then
                        .accs.Add(accID, New Accession(accID, dcmDate))
                    End If
                    If serDesc <> "" Then
                        If Not .accs(accID).series_descs.Contains(serDesc) Then
                            .accs(accID).series_descs.Add(serDesc)
                        End If
                    End If

                End With

                UpdateSubjectList()

            Else
                skippedUnknownID += 1
                WriteLog("Skipping DCM with unknown subject ID")
                Return accID
            End If

        End If

        'Dim difference_dict As New Dictionary(Of String, String)
        'For Each item In dcmFile.Dataset
        '    Dim str = ""
        '    dcmFile.Dataset.TryGetString(item.Tag, str)
        '    ''Debug.Print(item.Tag.DictionaryEntry.Name & ": " & str)
        '    difference_dict.Add(item.ToString, str)
        'Next

        'Debug.Print("   ###   ")

        AnonymiseDCMFile(dcmFile, subjID)

        'For Each item In dcmFile.Dataset
        '    Dim str = ""
        '    dcmFile.Dataset.TryGetString(item.Tag, str)
        '    If difference_dict.Keys.Contains(item.ToString) AndAlso difference_dict(item.ToString) <> str Then
        '        Debug.Print(item.Tag.DictionaryEntry.Name & ": " & difference_dict(item.ToString) & " ==> " & str)
        '    End If
        'Next

        dcmFile.Dataset.AddOrUpdate(DicomTag.AccessionNumber, accID)
        dcmFile.Dataset.AddOrUpdate(DicomTag.PatientID, cf.studyCode & "_" & cf.centreCode & "_" & newSubjStudyID)

        Dim subjFolder As String = outputFolderDlg.SelectedPath & "/" & ReplaceInvalidChars(cf.studyCode) & "/" & ReplaceInvalidChars(cf.centreCode) & "/" & ReplaceInvalidChars(newSubjStudyID)
        curAccFolder = subjFolder & "/" & ReplaceInvalidChars(accID)
        Dim serFolder As String = subjFolder & "/" & ReplaceInvalidChars(accID) & "/" & ReplaceInvalidChars(serID)

        CheckCreateDirectory(subjFolder)
        CheckCreateDirectory(serFolder)

        Dim fileDest As String = serFolder & "/" & fileName
        If Not fileDest.ToUpper.EndsWith(".DCM") Then
            fileDest &= ".dcm"
        End If
        dcmFile.Save(fileDest)
        totalAnonymised += 1
        WriteLog("***   Anonymised file copied to: " & fileDest)
        If deleteBox.Checked Then
            Try
                IO.File.Delete(path)
                WriteLog("***   Source file deleted.")
            Catch ex As Exception
                WriteLog("***   Source file could NOT be deleted.")
            End Try
        End If

        Return accID
    End Function

    Dim folders_to_zip As New List(Of String)
    Dim halt_zipping As Boolean = True
    Dim zipping_threads As New List(Of Threading.Thread)
    Dim num_zip_threads As Integer = 4

    Sub ProcessFolder(folder As String, Optional top_level As Boolean = False)

        If Directory.Exists(Ip(folder)) Then
            Dim folder_count As Integer = 0
            For Each subFolder As String In Directory.GetDirectories(Ip(folder))

                If top_level Then
                    folder_count += 1
                    WriteLog(folder_count)
                End If

                If working = False Then
                    'DisposeStreamwriter()
                    Exit For
                End If

                Dim folderName As String = New DirectoryInfo(subFolder).Name
                'Debug.WriteLine(folder & "/" & folderName)
                'WriteLog("Creating folder: " & folderName)
                Application.DoEvents()
                ProcessFolder(folder & "/" & folderName)

                If top_level And zip_check.Checked Then
                    WriteLog("Requesting .zip archive...")
                    SyncLock zip_object
                        folders_to_zip.Add(subFolder)
                    End SyncLock
                End If

                If top_level And folder_count >= 25 Then
                    folder_count = 0
                    Save_Enrolment_Log()
                End If

            Next

            Dim folderReportScouted As Boolean = False

            For Each file As String In Directory.GetFiles(Ip(folder))

                If working = False Then
                    'DisposeStreamwriter()
                    Exit For
                End If

                Dim fileName As String = New DirectoryInfo(file).Name
                If fileName.ToLower.EndsWith(".dcm") Or Not fileName.Contains(".") Then

                    Dim fileDest As String = Op(folder) & "/" & fileName
                    'IO.File.Copy(file, fileDest)
                    WriteLog("***   Found file: " & fileName)

                    Dim loggedAcc As String = ""

                    Application.DoEvents()
                    Try

                        loggedAcc = LogDcmFile(file, writeIDs)

                    Catch ex As Exception
                        WriteLog("***   Processing failed for: " & file & " - Is this a valid DICOM file?")
                        WriteLog(ex.Message)
                        totalFailed += 1
                    End Try

                    Try
                        If Not folderReportScouted And Not accWithReports.Contains(loggedAcc) Then
                            folderReportScouted = True
                            Dim curFolder As String = Ip(folder)
                            If curFolder.Contains("/DICOM/") Then
                                Dim secParentFolder As String = curFolder.Remove(curFolder.IndexOf("/DICOM/"))
                                ' Dim secSubFolders As String = curFolder.Substring(curFolder.IndexOf("/DICOM/") + 8)
                                Dim serFolder As String = secParentFolder & "/REPORTS/"
                                If Directory.Exists(serFolder) AndAlso Directory.GetDirectories(serFolder).Length > 0 Then
                                    Do
                                        serFolder = Directory.GetDirectories(serFolder)(0)
                                    Loop Until Directory.GetDirectories(serFolder).Length = 0
                                    Dim textFiles As String() = Directory.GetFiles(serFolder, "*.TXT")
                                    If textFiles.Length > 0 Then
                                        Dim repText As String = IO.File.ReadAllText(textFiles(0))
                                        IO.File.WriteAllText(curAccFolder & "/report.txt", repText)
                                        accWithReports.Add(loggedAcc)
                                        WriteLog("***   Report file copied to: " & curAccFolder & "/report.txt")
                                    End If
                                End If
                            End If
                        End If

                    Catch ex As Exception
                        WriteLog("***   Could not copy report file for: " & file)
                        WriteLog(ex.Message)
                    End Try

                End If
            Next
        End If

    End Sub

    Dim saveHeader As String() = {"Subject Study ID", "Initials", "DOB", "ID Number", "Date of MRI"}
    Sub WriteExcelFile(filepath As String)
        Try

            Dim xl As XSSFWorkbook
            Dim studySheet As ISheet
            Dim accSheet As ISheet

            If Not File.Exists(filepath) Then
                cf.CreateEnrolFile(filepath)
            ElseIf cf.backupFiles Then
                Dim bkFileName As String = filepath.Insert(filepath.LastIndexOf("."), "_old")
                If File.Exists(bkFileName) Then
                    Try
                        File.Delete(bkFileName)
                        File.Copy(filepath, bkFileName)
                    Catch ex As Exception

                        MsgBox("Clearing old backup & creating new Excel backup failed. Are any files currently open in another process? Backup has not been created." & vbCrLf & vbCrLf & "Error message: " & vbCrLf & ex.ToString)

                    End Try
                Else
                    Try
                        File.Copy(filepath, bkFileName)
                    Catch ex As Exception
                        MsgBox("Creating Excel backup failed. Is the file currently open in another process? Backup has not been created." & vbCrLf & vbCrLf & "Error message: " & vbCrLf & ex.ToString)
                    End Try
                End If
            End If

            Using readStream As New FileStream(filepath, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read)

                xl = New XSSFWorkbook(readStream)

                Dim studySheetID = 0
                Dim accSheetID = -1
                For s As Integer = 0 To (xl.NumberOfSheets - 1)
                    Dim sheetName As String = xl.GetSheetName(s)
                    If sheetName = "Enrolment Log" Then
                        studySheetID = s
                    ElseIf sheetName = "Accession Log" Then
                        accSheetID = s
                    End If
                Next

                If accSheetID = -1 Then
                    accSheet = xl.CreateSheet("Accession Log")
                Else
                    accSheet = xl.GetSheetAt(accSheetID)
                End If

                studySheet = xl.GetSheetAt(studySheetID)

            End Using

            studySheet.GetRow(2).Cells(2).SetCellValue(cf.siteName)
            studySheet.GetRow(3).Cells(2).SetCellValue(cf.principalInv)

            Dim r As Integer = 1

            For Each iKey As String In knownSubjects.Keys

                With knownSubjects(iKey)

                    Dim subjectRow As IRow

                    subjectRow = studySheet.CreateRow(r + 5)

                    For c As Integer = 0 To saveHeader.Count
                        subjectRow.CreateCell(c)
                    Next

                    subjectRow.Cells(0).SetCellValue(cf.studyCode & "_" & cf.centreCode & "_" & .studyID)
                    subjectRow.Cells(1).SetCellValue(.initials)
                    subjectRow.Cells(2).SetCellValue(.birthDate)
                    subjectRow.Cells(3).SetCellValue(.nhsID)
                    subjectRow.Cells(5).SetCellValue(.imageDate)

                    If logAccs And .accs.Count > 0 Then
                        Dim accRow As IRow = accSheet.CreateRow(r)
                        accRow.CreateCell(0)
                        accRow.Cells(0).SetCellValue(cf.studyCode & "_" & cf.centreCode & "_" & .studyID)
                        For a As Integer = 1 To .accs.Count
                            Dim acc_str As String = .accs.ElementAt(a - 1).Value.Get_String()
                            If acc_str.Length > 30000 Then
                                acc_str = acc_str.Substring(0, 30000)
                            End If
                            accRow.CreateCell(a)
                            accRow.Cells(a).SetCellValue(acc_str)
                        Next
                    End If

                End With

                r += 1

            Next

            For clearRowIndex As Integer = r + 5 To studySheet.LastRowNum
                Dim clearRow As IRow = studySheet.CreateRow(clearRowIndex)
                For c As Integer = 0 To clearRow.LastCellNum - 1
                    clearRow.Cells(c).SetCellValue("")
                Next
            Next

            Try
                Using writeStream As New IO.FileStream(filepath, IO.FileMode.Create)
                    xl.Write(writeStream)
                End Using

                WriteLog("Excel save successful.")

            Catch ex As Exception

                WriteLog("Excel save unsuccessful. Is the file currently open in another process?" & vbCrLf & "YOU SHOULD MANUALLY EXPORT THE FILE USING 'Export Excel' ON THE TASKBAR. Failure to do so could result in lost data. Alternatively, you can run 'Anonymise' again once other processes using the file (such as Excel) have been closed." & vbCrLf & vbCrLf & "Error message: " & vbCrLf & ex.ToString)

            End Try

        Catch ex As Exception

            WriteLog(ex.GetBaseException.Message)

        End Try

    End Sub
    Sub WriteTxtFile(filepath As String)
        Try

            Dim txtLines As New List(Of String) From {
                String.Join(",", saveHeader)
            }

            For Each iKey As String In knownSubjects.Keys
                With knownSubjects(iKey)
                    txtLines.Add(String.Join(",", { .studyID, .initials, .birthDate, .nhsID, .imageDate}))
                End With
            Next

            If File.Exists(filepath) And cf.backupFiles Then
                Dim bkFileName As String = filepath.Insert(filepath.LastIndexOf("."), "Old")
                If File.Exists(bkFileName) Then
                    Try
                        File.Delete(bkFileName)
                        File.Move(filepath, bkFileName)
                    Catch ex As Exception

                    End Try
                Else
                    Try
                        File.Move(filepath, bkFileName)
                    Catch ex As Exception

                    End Try
                End If
            End If

            Try

                IO.File.WriteAllLines(filepath, txtLines.ToArray())

                WriteLog(".txt save successful.")

            Catch ex As Exception

                WriteLog(".txt save unsuccessful. Is the file currently open in another process? You can manually export the file using the taskbar." & vbCrLf & vbCrLf & "Error message: " & vbCrLf & ex.ToString)

            End Try

        Catch ex As Exception

            WriteLog(ex.GetBaseException.Message)

        End Try

    End Sub
    Sub ReadExcelFile(filepath As String)

        knownSubjects.Clear()

        Try

            Using readStream As New FileStream(filepath, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.ReadWrite)

                Dim xl As New XSSFWorkbook(readStream)

                Dim activeSheet = 0
                For s As Integer = 0 To (xl.NumberOfSheets - 1)
                    If xl.GetSheetName(s) = "Enrolment Log" Then
                        activeSheet = s
                    End If
                Next

                Dim reportSheet As ISheet = xl.GetSheetAt(activeSheet)

                For row As Integer = 6 To reportSheet.LastRowNum
                    Dim reportRow As IRow = reportSheet.GetRow(row)
                    Dim cellList As New List(Of String)
                    For c As Integer = 0 To 5
                        cellList.Add(reportRow.GetCell(c, MissingCellPolicy.CREATE_NULL_AS_BLANK).ToString)
                    Next

                    If reportRow.Cells.Count > 5 Then
                        If cellList(0) <> "" And cellList(3) <> "" AndAlso (IsNumeric(cellList(0)) Or cellList(0).Contains("_")) Then
                            If Not knownSubjects.ContainsKey(cellList(3)) Then
                                knownSubjects.Add(cellList(3).Trim.ToUpper, New Subject(cellList(3).Trim.ToUpper, cellList(0).Split({"_"c}).Last.ToUpper, cellList(1).ToUpper, cellList(2).ToUpper, cellList(5).ToUpper))
                                Debug.Print("New image date: " & knownSubjects.Last.Value.DcmImageDate())
                            End If
                        End If
                    End If

                Next

            End Using

        Catch ex As Exception

            MsgBox(ex.GetBaseException.Message)

        End Try

    End Sub
    Public Sub UpdateSubjectList()
        SubjectBox.Items.Clear()
        If knownSubjects.Keys.Count = 0 Then
            SubjectBox.Items.Add("No known subjects")
        End If
        For Each iKey As String In knownSubjects.Keys
            With knownSubjects(iKey)
                If .nhsID.Trim <> "" Then
                    SubjectBox.Items.Add(.studyID.PadLeft(4) & ",  " & .initials.PadLeft(6) & ",  " & .nhsID.PadLeft(24))
                End If
            End With
        Next
    End Sub
    Public Sub UpdateMainFormUI()
        UpdateSubjectList()
        If inputFolderDlg.SelectedPath <> outputFolderDlg.SelectedPath Then
            InputFolderTxt.Text = inputFolderDlg.SelectedPath
        Else
            InputFolderTxt.Text = "Not selected"
        End If

        check_zip_threads()

        OutputFolderTxt.Text = outputFolderDlg.SelectedPath
        EnrolLogTxt.Text = cf.enrolFilePath

        InputFolderBtn.Enabled = Not working
        OutputFolderBtn.Enabled = Not working
        EnrolFileBtn.Enabled = Not working
        SettingsToolStripMenuItem.Enabled = Not working

        If working Then
            ConvertBtn.Text = "Stop"
        Else
            ConvertBtn.Text = "Deidentify"
        End If

    End Sub

    Private Sub EnrolFileBtn_Click(sender As Object, e As EventArgs) Handles EnrolFileBtn.Click
        cf.SelectEnrolFile()

        If File.Exists(cf.enrolFilePath) Then
            ReadExcelFile(cf.enrolFilePath)
        End If

        cf.SaveToTxt(configPath)

        UpdateMainFormUI()

    End Sub

    Private Sub SubjectBox_DoubleClick(sender As Object, e As EventArgs) Handles SubjectBox.DoubleClick
        If SubjectBox.SelectedIndex >= 0 AndAlso SubjectBox.SelectedIndex < knownSubjects.Count Then
            Dim subjOutputPath As String = outputFolderDlg.SelectedPath & cf.studyCode & "\" & cf.centreCode & "\" & knownSubjects(knownSubjects.Keys(SubjectBox.SelectedIndex)).studyID
            If IO.Directory.Exists(subjOutputPath) Then
                Process.Start("explorer.exe", subjOutputPath)
            End If
        End If
    End Sub

    Private Sub SFTPUploadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SFTPUploadToolStripMenuItem.Click
        Dim UploadDlg As New FTPForm

        Try
            UploadDlg.ShowDialog()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub SettingsToolMenu() Handles SettingsToolStripMenuItem1.Click
        cf.SetDialogInfo()
        If cf.setDialog.ShowDialog = DialogResult.OK Then
            cf.SaveDialogInfo()
            cf.SaveToTxt(configPath)
        End If
    End Sub

    Private Sub ExportExcel(sender As Object, e As EventArgs) Handles AsExcelxlsxToolStripMenuItem.Click
        outputFileDlg.Filter = "Excel file|*.xlsx"
        outputFileDlg.Title = "Select output file..."
        If outputFileDlg.ShowDialog = DialogResult.OK Then
            Try
                WriteExcelFile(outputFileDlg.FileName)
            Catch ex As Exception
                WriteLog(ex.Message, True)
            End Try
        End If
    End Sub
    Private Sub ExportTxt(sender As Object, e As EventArgs) Handles AsTexttxtToolStripMenuItem.Click
        outputFileDlg.Filter = "Txt file|*.txt"
        outputFileDlg.Title = "Select output file..."
        If outputFileDlg.ShowDialog = DialogResult.OK Then
            Try
                WriteTxtFile(outputFileDlg.FileName)
            Catch ex As Exception
                WriteLog(ex.Message, True)
            End Try
        End If
    End Sub

    Private Sub AddReportsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddReportsToolStripMenuItem.Click
        Dim reportForm As New ReportForm

        reportForm.ShowDialog()

    End Sub

    Private Sub InputExplBtn_Click(sender As Object, e As EventArgs) Handles InputExplBtn.Click
        If IO.Directory.Exists(InputFolderTxt.Text) Then
            Process.Start("explorer.exe", InputFolderTxt.Text)
        End If
    End Sub

    Private Sub OutputExplBtn_Click(sender As Object, e As EventArgs) Handles OutputExplBtn.Click
        If IO.Directory.Exists(OutputFolderTxt.Text) Then
            Process.Start("explorer.exe", OutputFolderTxt.Text)
        End If
    End Sub

    Private Sub MIDIQueryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MIDIQueryToolStripMenuItem.Click
        QueryForm.Show()
    End Sub

    Private Sub ui_timer_Tick(sender As Object, e As EventArgs) Handles ui_timer.Tick
        check_zip_threads()
    End Sub
    Sub check_zip_threads()
        zip_lbl.Visible = False
        ui_timer.Enabled = False
        For Each thread In zipping_threads
            If thread.IsAlive = True Then
                zip_lbl.Visible = True
                ui_timer.Enabled = True
            End If
        Next
    End Sub
End Class
