Public Class SettingsForm
    Public descText As String = "MIDI Study DICOM Deidentifier Tool

Produced by the MIDI Consortium

Developed by Dr M Townend

Testing by Mr J Ledger"

    Private Sub StudyInfoGroup_Enter(sender As Object, e As EventArgs) Handles StudyInfoGroup.MouseEnter
        InfoBox.Text =
            "Study Information

Study Code is the identifier of the study the app is anonymising data for. This is used to generate the study ID for each subject. By default this is MIDI, for the MR Imaging abnormality Deep learning Identification trial for which the app was designed.

Centre Code is the identifier of which centre the app is being used in. This is used to generate the study ID for each subject. This is a 3 character abbreviation unique to your centre - for example, 'KCH' for King's College Hospital.

Centre Name is the full name of the centre the app is being used in. This is used to set the centre name on the enrolment log files that the app saves - for example, 'KING'S COLLEGE HOSPITAL'

Principal Investigator is the name of the PI of the centre the app is being used in. This is used to set the PI name on the enrolment log files that the app saves.

"
    End Sub

    Private Sub SaveFormatGroup_Enter(sender As Object, e As EventArgs) Handles SaveFormatGroup.MouseEnter
        InfoBox.Text = "Save Format
        
ID Format sets the number of digits used in the study IDs. The default is '0000', which results in 4 digit IDs; '00000' would give 5 digits, and so on.

Enrolment Log File is the location of the enrolment log the app will read from and save to. This is an absolute path, meaning if the file is moved (even if the other app files move with it) it will need to be changed. If the app cannot find the file on launch then you will be prompted to select a new file. If you do not do so, the app will default to saving 'KnownIDs.xlsx' in the app's base directory.

If you select to backup existing files, before the app overwrites the enrolment log it will make a copy of the existing file as a backup.

If you select to save to a .txt file, the app will write a plaintext .txt file version of the enrolment log in addition to the Excel document

"
    End Sub

    Private Sub OptionsGroup_Enter(sender As Object, e As EventArgs) Handles OptionsGroup.MouseEnter
        InfoBox.Text = "Options
        
If you select to reuse skipped study IDs, the app will try to assign the lowest possible available study ID to any new subjects - ie, if IDs 0001, 0002, & 0004 are assigned to subjects, then the app will assign ID 0003. By default this is not used to avoid the possibility of different subjects sharing study IDs in the case of an error - the app will assign a study ID one higher than the highest previous ID, in this case, ID 0005.

If you select to ignore DICOMs not already assigned IDs, the app will only deidentify files that match a known (ie, matching a subject in the enrolment log file) subject.

If you select to ignore DICOMs with blank IDs, the app will not attempt to deidentify these files. This is probably sensible, as if the ID is blank they cannot be reliably assigned to a given subject. Some PAC systems seem to generate these dummy files frequently for some reason, but they do not seem essential to reading the genuine images. 

"
    End Sub

    Private Sub InfoBox_TextChanged(sender As Object, e As EventArgs) Handles StudyInfoGroup.MouseLeave, SaveFormatGroup.MouseLeave, OptionsGroup.MouseLeave
        InfoBox.Text = descText
    End Sub

    Private Sub _TextChanged(sender As TextBox, e As EventArgs) Handles STUDYCODEtxt.TextChanged, CENTRECODEtxt.TextChanged, SITENAMEtxt.TextChanged, PRINCIPALINVtxt.TextChanged, IDFORMATtxt.TextChanged, ENROLFILEPATHtxt.TextChanged
        sender.Text = sender.Text.Replace("#"c, " "c)
        sender.Text = sender.Text.Replace("="c, " "c)
    End Sub

    Private Sub SettingsForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        InfoBox.Text = descText
    End Sub
End Class