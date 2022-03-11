Imports System
Imports System.IO
Imports Dicom

Public Class Form1

    Dim inputFolderDlg As FolderBrowserDialog
    Dim outputFolderDlg As FolderBrowserDialog

    Dim baseDirectory As String = AppDomain.CurrentDomain.BaseDirectory()

    Dim working As Boolean = False

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If Not Directory.Exists(baseDirectory & "\Output\") Then
            Directory.CreateDirectory(baseDirectory & "\Output\")
        End If
        If Not Directory.Exists(baseDirectory & "\Logs\") Then
            Directory.CreateDirectory(baseDirectory & "\Logs\")
        End If

        inputFolderDlg = New FolderBrowserDialog
        inputFolderDlg.Description = "Select input directory to convert..."
        inputFolderDlg.SelectedPath = baseDirectory & "\Output\"
        outputFolderDlg = New FolderBrowserDialog
        outputFolderDlg.Description = "Select output directory for anonymised files..."
        outputFolderDlg.SelectedPath = baseDirectory & "\Output\"

    End Sub
    Private Sub InputToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InputToolStripMenuItem.Click
        inputFolderDlg.ShowDialog()
    End Sub
    Private Sub OutputToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OutputToolStripMenuItem.Click
        outputFolderDlg.ShowDialog()
    End Sub
    Private Sub ConvertBtn_Click(sender As Object, e As EventArgs) Handles ConvertBtn.Click
        If working = False Then
            If inputFolderDlg.SelectedPath = outputFolderDlg.SelectedPath Then
                If inputFolderDlg.ShowDialog() = DialogResult.OK AndAlso inputFolderDlg.SelectedPath <> outputFolderDlg.SelectedPath Then
                    working = True
                    ConvertBtn.Text = "Stop"
                    ProcessFolder("")
                    working = False
                    SaveLog(DateAndTime.Now.Day & "_" & DateAndTime.Now.Month & "_" & "_" & DateAndTime.Now.Hour & "_" & DateAndTime.Now.Minute & "_" & DateAndTime.Now.Second)
                End If
            Else
                working = True
                ConvertBtn.Text = "Stop"
                ProcessFolder("")
                working = False
                SaveLog(DateAndTime.Now.Day & "_" & DateAndTime.Now.Month & "_" & "_" & DateAndTime.Now.Hour & "_" & DateAndTime.Now.Minute & "_" & DateAndTime.Now.Second)
            End If
        Else
            working = False
            ConvertBtn.Text = "Anonymise"
        End If
    End Sub
    Sub WriteLog(input As String)
        LogTxt.AppendText(input & vbCrLf)
    End Sub
    Sub SaveLog(name As String)
        Dim savePath As String = baseDirectory & "/Logs/" & name & ".txt"
        If Not File.Exists(savePath) Then
            Try
                IO.File.WriteAllLines(savePath, LogTxt.Text.Split(vbCrLf))
                WriteLog("***  ***  Log file saved  ***  ***")
            Catch ex As Exception
                WriteLog("***  ***  Log file not saved - unable to write  ***  ***")
            End Try
        End If
    End Sub

    Function ip(subFolder As String)
        Return inputFolderDlg.SelectedPath & subFolder
    End Function
    Function op(subFolder As String)
        Return outputFolderDlg.SelectedPath & subFolder
    End Function

    Sub ProcessFolder(folder As String)
        If Not Directory.Exists(op(folder)) Then
            Directory.CreateDirectory(op(folder))
        End If
        If Directory.Exists(ip(folder)) Then
            For Each subFolder As String In Directory.GetDirectories(ip(folder))

                If working = False Then : Exit For
                End If

                Dim folderName As String = New DirectoryInfo(subFolder).Name
                Debug.WriteLine(folder & "\" & folderName)
                WriteLog("Creating folder: " & folderName)
                Application.DoEvents()
                ProcessFolder(folder & "\" & folderName)

            Next
            For Each file As String In Directory.GetFiles(ip(folder))
                If working = False Then : Exit For
                End If
                Dim fileName As String = New DirectoryInfo(file).Name
                If fileName.ToLower.EndsWith(".dcm") Or Not fileName.Contains(".") Then
                    Dim fileDest As String = op(folder) & "\" & fileName
                    'IO.File.Copy(file, fileDest)
                    WriteLog("***   Found file: " & fileName)
                    Application.DoEvents()
                    Try
                        Dim dcmFile As DicomFile = DicomFile.Open(file, FileReadOption.ReadLargeOnDemand)

                        dcmFile.Dataset.Remove(DicomTag.PatientAddress)
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
                        dcmFile.Dataset.Remove(DicomTag.PatientID)
                        dcmFile.Dataset.Remove(DicomTag.PatientInstitutionResidence)
                        dcmFile.Dataset.Remove(DicomTag.PatientInsurancePlanCodeSequence)
                        dcmFile.Dataset.Remove(DicomTag.PatientLocationCoordinatesCodeSequence)
                        dcmFile.Dataset.Remove(DicomTag.PatientLocationCoordinatesSequence)
                        dcmFile.Dataset.Remove(DicomTag.PatientMotherBirthName)
                        dcmFile.Dataset.Remove(DicomTag.PatientName)
                        dcmFile.Dataset.Remove(DicomTag.PatientPrimaryLanguageCodeSequence)
                        dcmFile.Dataset.Remove(DicomTag.PatientPrimaryLanguageModifierCodeSequence)
                        dcmFile.Dataset.Remove(DicomTag.PatientReligiousPreference)
                        dcmFile.Dataset.Remove(DicomTag.PatientSetupLabel)
                        dcmFile.Dataset.Remove(DicomTag.PatientSetupNumber)
                        dcmFile.Dataset.Remove(DicomTag.PatientSex)
                        dcmFile.Dataset.Remove(DicomTag.PatientSexNeutered)
                        dcmFile.Dataset.Remove(DicomTag.PatientSize)
                        dcmFile.Dataset.Remove(DicomTag.PatientSizeCodeSequence)
                        dcmFile.Dataset.Remove(DicomTag.PatientSpeciesCodeSequence)
                        dcmFile.Dataset.Remove(DicomTag.PatientSpeciesDescription)
                        dcmFile.Dataset.Remove(DicomTag.PatientTelecomInformation)
                        dcmFile.Dataset.Remove(DicomTag.PatientTelephoneNumbers)
                        dcmFile.Dataset.Remove(DicomTag.PatientWeight)
                        dcmFile.Dataset.Remove(DicomTag.CurrentPatientLocation)
                        dcmFile.Dataset.Remove(DicomTag.AdditionalPatientHistory)
                        dcmFile.Dataset.Remove(DicomTag.GroupOfPatientsIdentificationSequence)
                        dcmFile.Dataset.Remove(DicomTag.OtherPatientIDsSequence)

                        dcmFile.Dataset.AddOrUpdate(DicomTag.PatientIdentityRemoved, "YES")

                        dcmFile.Save(fileDest)
                        WriteLog("***   Anonymised file copied to: " & fileDest)
                    Catch ex As Exception
                        WriteLog("***   Processing failed for: " & file & " - Is this a valid DICOM file?")
                    End Try
                End If
            Next
        End If
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        MessageBox.Show("This software was developed by Dr M Townend to enable easy data collection for the MIDI Study. Please contact me on matthew.townend@wwl.nhs.uk with any issues.", "MIDI Study DICOM Anonymiser V0.1")
    End Sub
End Class
