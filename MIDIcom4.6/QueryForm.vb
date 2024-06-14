Imports Dicom
Imports Dicom.Network
Imports System.IO

Public Class QueryForm

    Dim study_UID As String = ""
    Dim echo_lock As New Object
    Dim echo_response As Integer = 0
    Dim list_lock As New Object
    Dim nhs_study As New Dictionary(Of String, String)
    Dim study_list As New List(Of ServerStudy)
    Dim ui_update As Boolean = False

    Dim remote_config_path As String = ".\Resources\dicom_remote.txt"
    Dim remote_host As String = "www.dicomserver.co.uk"
    Dim remote_AEtitle As String = "DICOMserver"
    Dim remote_port As String = 104

    Dim local_config_path As String = ".\Resources\dicom_local.txt"
    Dim local_AEtitle As String = "MIDIcom"
    Dim local_port As String = 104

    Dim logStr = ""
    Dim debugEnab = False
    Public Sub Log_String(input As String)
        SyncLock echo_lock
            If logStr = "" Then
                logStr &= (input)
            Else
                logStr &= (vbCrLf & input)
            End If
        End SyncLock
    End Sub
    Public Function Debug_Enabled()
        SyncLock echo_lock
            Return debugEnab
        End SyncLock
    End Function
    Class ServerStudy
        Public pat_id As String
        Public sub_id As String
        Public study_acc As String
        Public study_uid As String
        Public study_desc As String
        Public study_num As String
        Public study_date As String
        Sub New(pat_id As String, sub_id As String, study_acc As String, study_uid As String, study_desc As String, study_num As String, study_date As String)
            Me.pat_id = pat_id.ToUpper
            Me.sub_id = sub_id
            Me.study_acc = study_acc
            Me.study_uid = study_uid
            Me.study_desc = study_desc
            Me.study_num = study_num
            Me.study_date = study_date
        End Sub

        Public Function to_string() As String
            Return "Patient ID: " & pat_id & " Subject ID: " & sub_id & " Study UID: " & study_uid & " Accession Number: " & study_acc & " Study Date: " & study_date
        End Function
    End Class

    Private Sub QueryForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim earliestDate As String = "20190101"
        For Each subj In Form1.knownSubjects
            Debug.Print(subj.Value.DcmImageDate())
            If Int(subj.Value.DcmImageDate()) < Int(earliestDate) Then
                earliestDate = subj.Value.DcmImageDate()
            End If
        Next

        Dim eYear As Integer = Int(Mid(earliestDate, 1, 4))
        If eYear < 2000 Then
            eYear = 2000
        End If
        Dim eMonth As Integer = Int(Mid(earliestDate, 5, 2))
        Dim eDay As Integer = Int(Mid(earliestDate, 7, 2))
        Debug.Print(eYear & ", " & eMonth & ", " & eDay)
        Dim start_date = New Date(eYear, eMonth, eDay, 23, 59, 59)
        StaCal.SelectionStart = start_date
        StaCal.SelectionEnd = start_date
        StaCal.Select()

        EndCal.SelectionStart = Date.Today
        EndCal.SelectionEnd = Date.Today
        EndCal.Select()

        Update_Query_UI()

        If File.Exists(local_config_path) Then
            Dim config() = File.ReadAllLines(local_config_path)
            If config.Length > 1 Then
                local_port = config(0)
                local_AEtitle = config(1)
            End If
        End If

        If File.Exists(remote_config_path) Then
            Dim config() = File.ReadAllLines(remote_config_path)
            If config.Length > 2 Then
                remote_host = config(0)
                remote_port = config(1)
                remote_AEtitle = config(2)
            End If
        End If

        RemoteHostTxt.Text = remote_host
        RemotePortTxt.Text = remote_port
        RemoteAETxt.Text = remote_AEtitle
        LocalAETitleTxt.Text = local_AEtitle
        LocalPortTxt.Text = local_port

    End Sub
    Sub Save_Network_Details()
        Try
            File.WriteAllLines(local_config_path, {local_port, local_AEtitle})
            File.WriteAllLines(remote_config_path, {remote_host, remote_port, remote_AEtitle})
        Catch ex As Exception
            SyncLock echo_lock
                If logStr = "" Then
                    logStr &= (ex.Message)
                Else
                    logStr &= (vbCrLf & ex.Message)
                End If
            End SyncLock
        End Try
    End Sub
    Sub Cfind_Response(rq As DicomCFindRequest, rp As DicomCFindResponse)
        Debug.Print(rp.Status.ToString())
        SyncLock echo_lock
            If logStr = "" Then
                logStr &= (rp.ToString)
            Else
                logStr &= (vbCrLf & rp.ToString)
            End If
        End SyncLock
        If rp.HasDataset Then
            Log_String("Response has dataset...")
            If debugEnab Then
                Log_String("C-FIND Request:")
                For Each item In rq.Dataset
                    If Debug_Enabled() Then
                        Dim tag_value As String = ""
                        rq.Dataset.TryGetString(item.Tag, tag_value)
                        Log_String("   " & item.Tag.DictionaryEntry.Name & ": " & tag_value)
                    End If
                Next

                Log_String("")
                Log_String("C-FIND Response:")
                Try
                    For Each item In rp.Dataset
                        If Debug_Enabled() Then
                            Dim tag_value As String = ""
                            rp.Dataset.TryGetString(item.Tag, tag_value)
                            Log_String("   " & item.Tag.DictionaryEntry.Name & ": " & tag_value)
                        End If
                    Next
                Catch ex As Exception
                    Log_String(ex.Message)
                End Try

                Log_String("")

            End If

            Dim p_id, s_id, s_acc, s_uid, s_mod, s_desc, s_num, s_date As New String("")
            rp.Dataset.TryGetString(DicomTag.PatientID, p_id)
            If p_id = Nothing Or p_id = "" Then
                rp.Dataset.TryGetString(DicomTag.PatientID, p_id)
                Log_String("Patient ID; " & p_id)
            End If

            s_id = ""

            rp.Dataset.TryGetString(DicomTag.AccessionNumber, s_acc)
            rp.Dataset.TryGetString(DicomTag.StudyInstanceUID, s_uid)
            rp.Dataset.TryGetString(DicomTag.Modality, s_mod)
            rp.Dataset.TryGetString(DicomTag.StudyDescription, s_desc)
            rp.Dataset.TryGetString(DicomTag.NumberOfStudyRelatedInstances, s_num)
            rp.Dataset.TryGetString(DicomTag.StudyDate, s_date)

            SyncLock list_lock
                Dim known_uid = False
                For Each study In study_list
                    If study.study_uid = s_uid Then
                        known_uid = True
                    End If
                Next

                Dim new_study As New ServerStudy(p_id, s_id, s_acc, s_uid, s_desc, s_num, s_date)
                study_list.Add(new_study)
                Log_String("Study added to study list...")
                ui_update = True
            End SyncLock

            Log_String("")
            Log_String("")
            Log_String("")
        End If
    End Sub
    Sub Cmove_Response(rq As DicomCMoveRequest, rp As DicomCMoveResponse)
        SyncLock echo_lock
            If logStr = "" Then
                logStr &= (rp.ToString)
            Else
                logStr &= (vbCrLf & rp.ToString)
            End If
        End SyncLock
        Debug.Print(rp.Status.ToString())
    End Sub
    Sub Cecho_Response(rq As DicomCEchoRequest, rp As DicomCEchoResponse)
        Debug.Print(rp.Status.ToString())
        SyncLock echo_lock
            If rp.Status = DicomStatus.Success Then
                echo_response = -2
            Else
                echo_response = -1
            End If
            If logStr = "" Then
                logStr &= (rp.ToString)
            Else
                logStr &= (vbCrLf & rp.ToString)
            End If
        End SyncLock
    End Sub

    Private Sub Query(sender As Object, e As EventArgs) Handles QueryBtn.Click

        remote_host = RemoteHostTxt.Text
        remote_port = RemotePortTxt.Text
        remote_AEtitle = RemoteAETxt.Text
        local_AEtitle = LocalAETitleTxt.Text
        local_port = LocalPortTxt.Text

        Save_Network_Details()

        study_list.Clear()
        StudyView.Rows.Clear()
        nhs_study.Clear()

        Debug.WriteLine(StaCal.SelectionStart)
        Debug.WriteLine(EndCal.SelectionStart)

        Try
            Dim client = New Client.DicomClient(RemoteHostTxt.Text, Int(RemotePortTxt.Text), False, local_AEtitle, remote_AEtitle)
            client.NegotiateAsyncOps()

            Dim subjQueryCount As Integer = Form1.knownSubjects.Keys.Count

            SyncLock echo_lock
                debugEnab = DebugCheck.Checked
            End SyncLock

            Select Case NumSubBox.SelectedIndex
                Case 0
                    subjQueryCount = 1
                Case 1
                    subjQueryCount = 25
                Case 2
                    subjQueryCount = 50
                Case 3
                    subjQueryCount = 100
            End Select

            For i As Integer = FirstSubUpDwn.Value - 1 To (FirstSubUpDwn.Value + subjQueryCount) - 2
                If Form1.knownSubjects.Keys.Count > i Then
                    Dim Key = Form1.knownSubjects.Keys(i)

                    nhs_study.Add(Form1.knownSubjects(Key).nhsID, Form1.knownSubjects(Key).studyID)

                    Dim cfind = DicomCFindRequest.CreateStudyQuery(Form1.knownSubjects(Key).nhsID, "*", New DicomDateRange(StaCal.SelectionStart, EndCal.SelectionStart))
                    cfind.Dataset.Remove(DicomTag.StudyTime)
                    cfind.Dataset.AddOrUpdate(DicomTag.IssuerOfPatientID, IssuerTxt.Text.Trim())
                    cfind.Dataset.AddOrUpdate(DicomTag.StudyInstanceUID, "")
                    If Not (ModaTxt.Text.Trim() = "*" Or String.IsNullOrWhiteSpace(ModaTxt.Text.Trim())) Then
                        cfind.Dataset.AddOrUpdate(DicomTag.ModalitiesInStudy, ModaTxt.Text.Trim())
                    End If

                    cfind.OnResponseReceived = AddressOf Cfind_Response

                    client.AddRequestAsync(cfind)
                    If debugEnab Then
                        Log_String("Adding CFind request: " & Form1.knownSubjects(Key).studyID & " - " & Form1.knownSubjects(Key).nhsID)
                    End If
                End If

            Next

            client.SendAsync()
        Catch ex As Exception
            Log_String(ex.Message)
        End Try

    End Sub
    Private Sub CustomQuery(sender As Object, e As EventArgs) Handles CustomQueryBtn.Click

        If PatientIDTxt.Text = "*" And PatientNameTxt.Text = "*" Then
            Debug.Print("PatID: " & PatientIDTxt.Text)
            Debug.Print("PatNm: " & PatientNameTxt.Text)
            Log_String("Cannot search with both patient name and ID being wildcards! This would find *every* study.")
        Else
            remote_host = RemoteHostTxt.Text
            remote_port = RemotePortTxt.Text
            remote_AEtitle = RemoteAETxt.Text
            local_AEtitle = LocalAETitleTxt.Text
            local_port = LocalPortTxt.Text

            Save_Network_Details()

            study_list.Clear()
            StudyView.Rows.Clear()
            nhs_study.Clear()

            Debug.WriteLine(StaCal.SelectionStart)
            Debug.WriteLine(EndCal.SelectionStart)

            Try
                Dim client = New Client.DicomClient(RemoteHostTxt.Text, Int(RemotePortTxt.Text), False, local_AEtitle, remote_AEtitle)
                client.NegotiateAsyncOps()

                SyncLock echo_lock
                    debugEnab = DebugCheck.Checked
                End SyncLock

                Dim patient_id As String = PatientIDTxt.Text.Trim().ToUpper()
                If patient_id = "*" Then : patient_id = Nothing
                End If
                Dim patient_name As String = PatientNameTxt.Text.Trim().ToUpper()
                If patient_name = "*" Then : patient_name = Nothing
                End If

                If Form1.knownSubjects.Keys.Contains(patient_id) Then
                    nhs_study.Add(patient_id, Form1.knownSubjects(patient_id).studyID)
                Else
                    nhs_study.Add(patient_id, "?")
                End If

                Dim cfind = DicomCFindRequest.CreateStudyQuery(patient_id, patient_name, New DicomDateRange(StaCal.SelectionStart, EndCal.SelectionStart))
                cfind.Dataset.Remove(DicomTag.StudyTime)
                cfind.Dataset.AddOrUpdate(DicomTag.IssuerOfPatientID, IssuerTxt.Text.Trim())
                If Not (ModaTxt.Text.Trim() = "*" Or String.IsNullOrWhiteSpace(ModaTxt.Text.Trim())) Then
                    cfind.Dataset.AddOrUpdate(DicomTag.ModalitiesInStudy, ModaTxt.Text.Trim())
                End If

                cfind.OnResponseReceived = AddressOf Cfind_Response

                client.AddRequestAsync(cfind)

                client.SendAsync()

            Catch ex As Exception
                Log_String(ex.Message)
            End Try
        End If

    End Sub

    Private Sub Request(sender As Object, e As EventArgs) Handles TransBtn.Click

        remote_host = RemoteHostTxt.Text
        remote_port = RemotePortTxt.Text
        remote_AEtitle = RemoteAETxt.Text
        local_AEtitle = LocalAETitleTxt.Text
        local_port = LocalPortTxt.Text

        Save_Network_Details()

        Dim echo_client = New Client.DicomClient("127.0.0.1", local_port, False, local_AEtitle, local_AEtitle)
        Dim cecho = New DicomCEchoRequest()
        cecho.OnResponseReceived = AddressOf Cecho_Response
        echo_client.AddRequestAsync(cecho)
        echo_client.SendAsync()

        MoveResponseTxt.Clear()

        TransBtn.Text = "Launching Server..."

        SyncLock echo_lock
            echo_response = 0
        End SyncLock

        While echo_response > -1 And echo_response < 40
            Debug.Print("Waiting for CEcho response...")
            SyncLock echo_lock
                echo_response += 1
            End SyncLock
            Threading.Thread.Sleep(50)
            Application.DoEvents()
        End While

        If echo_response > 0 Then
            Process.Start(".\MIDIserv.exe")
        End If

        TransBtn.Text = "Transfer"

        Dim client = New Client.DicomClient(RemoteHostTxt.Text, Int(RemotePortTxt.Text), False, local_AEtitle, remote_AEtitle)
        client.NegotiateAsyncOps()
        client.SendAsync()

        For s As Integer = 0 To study_list.Count - 1
            If StudyView.Rows(s).Cells(0).Value = True Then
                Dim cmove = New DicomCMoveRequest(local_AEtitle, StudyView.Rows(s).Cells("StudyUID").Value)
                cmove.Dataset.AddOrUpdate(DicomTag.QueryRetrieveLevel, "STUDY")
                cmove.OnResponseReceived = AddressOf Cmove_Response
                client.AddRequestAsync(cmove)
            End If
        Next

        client.SendAsync()

    End Sub

    Private Sub UiTimer_Tick(sender As Object, e As EventArgs) Handles UiTimer.Tick
        SyncLock list_lock
            If ui_update Then
                Debug.WriteLine(study_list.Count)
                For s As Integer = 0 To study_list.Count - 1
                    If s >= StudyView.Rows.Count Then
                        Dim index As Integer = StudyView.Rows.Add()
                        If study_list(s).study_uid = "" Then
                            StudyView.Rows.Item(index).Cells(0).ReadOnly = True
                            StudyView.Rows.Item(index).Cells(0).Style.BackColor = Color.Red
                        End If

                        If study_list(s).pat_id <> "" And nhs_study.ContainsKey(study_list(s).pat_id) Then
                            StudyView.Rows.Item(index).Cells(1).Value = nhs_study(study_list(s).pat_id)
                            StudyView.Rows.Item(index).Cells(2).Value = study_list(s).pat_id
                        Else
                            StudyView.Rows.Item(index).Cells(1).Value = "?"
                            StudyView.Rows.Item(index).Cells(2).Value = "?"
                        End If

                        StudyView.Rows.Item(index).Cells(2).Value = study_list(s).pat_id
                        StudyView.Rows.Item(index).Cells(3).Value = study_list(s).study_acc
                        StudyView.Rows.Item(index).Cells(4).Value = study_list(s).study_desc
                        StudyView.Rows.Item(index).Cells("s_count").Value = study_list(s).study_num
                        If Int(study_list(s).study_num) <= 0 Then
                            StudyView.Rows.Item(index).Cells("s_count").Style.BackColor = Color.Red
                        End If
                        If study_list(s).study_date.Length = 8 And Form1.knownSubjects.ContainsKey(study_list(s).pat_id.ToUpper) Then
                            StudyView.Rows.Item(index).Cells("s_date").Value = Mid(study_list(s).study_date, 7, 2) & "/" & Mid(study_list(s).study_date, 5, 2) & "/" & Mid(study_list(s).study_date, 1, 4)
                            Dim date_delta As Integer = (Int(Form1.knownSubjects(study_list(s).pat_id.ToUpper).DcmImageDate) - Int(study_list(s).study_date))
                            If date_delta = 0 Then ''Correct date
                                StudyView.Rows.Item(index).Cells("s_date").Style.BackColor = Color.Green
                                StudyView.Rows.Item(index).Cells("s_date").Style.ForeColor = Color.White
                            ElseIf date_delta >= -7 And date_delta <= 7 Then ''Within a week
                                StudyView.Rows.Item(index).Cells("s_date").Style.BackColor = Color.DarkOrange
                                StudyView.Rows.Item(index).Cells("s_date").Style.ForeColor = Color.Black
                            End If
                        Else
                            StudyView.Rows.Item(index).Cells("s_date").Value = study_list(s).study_date
                        End If
                        StudyView.Rows.Item(index).Cells(7).Value = study_list(s).study_uid
                    End If
                Next
                ui_update = False
            End If
        End SyncLock
        SyncLock echo_lock
            If logStr <> "" Then
                If MoveResponseTxt.Text <> "" Then
                    logStr = vbCrLf & logStr
                End If
                MoveResponseTxt.AppendText(logStr)
                logStr = ""
            End If
        End SyncLock
        Update_Filter()
    End Sub

    Private Sub Update_Query_UI() Handles NumSubBox.SelectedIndexChanged, FirstSubUpDwn.ValueChanged
        If Form1.knownSubjects.Count = 0 Then
            NumSubBox.Enabled = False
            FirstSubUpDwn.Enabled = False
            NumLabel.Text = "(Total: 0 subjects)"
        Else
            Select Case NumSubBox.SelectedIndex
                Case 0, 4 ''1, All
                    FirstSubUpDwn.Increment = 1
                Case 1 ''25
                    FirstSubUpDwn.Increment = 25
                Case 2 ''50
                    FirstSubUpDwn.Increment = 50
                Case 3 ''100
                    FirstSubUpDwn.Increment = 100
            End Select
            FirstSubUpDwn.Maximum = Form1.knownSubjects.Count

            Dim numSubjects As Integer = (Form1.knownSubjects.Count - FirstSubUpDwn.Value) + 1
            If (Not NumSubBox.SelectedIndex = 4) And numSubjects > FirstSubUpDwn.Increment Then
                numSubjects = FirstSubUpDwn.Increment
            End If

            NumLabel.Text = "(Total: " & Str(numSubjects) & " subjects)"

        End If
        Update_Filter()
    End Sub
    Sub Update_Filter() Handles FilterBox.TextChanged
        For r As Integer = 0 To StudyView.Rows.Count - 1
            With StudyView.Rows.Item(r)
                If FilterBox.Text.Trim() <> "" Then
                    If .Cells(4).Value.ToString.ToUpper.Contains(FilterBox.Text.Trim.ToUpper) Then
                        .Visible = True
                    Else
                        .Visible = False
                        .Cells(0).Value = False
                    End If
                Else
                    .Visible = True
                End If
            End With
        Next
    End Sub

    Private Sub Clear_Selected_Studies(sender As Object, e As EventArgs) Handles ClearBtn.Click
        For r As Integer = 0 To StudyView.Rows.Count - 1
            With StudyView.Rows.Item(r)
                .Cells(0).Value = False
            End With
        Next
    End Sub

    Private Sub SelectBtn_Click(sender As Object, e As EventArgs) Handles SelectBtn.Click
        For r As Integer = 0 To StudyView.Rows.Count - 1
            With StudyView.Rows.Item(r)
                If .Visible = True Then
                    .Cells(0).Value = True
                End If
            End With
        Next
    End Sub

    Private Sub GreenBtn_Click(sender As Object, e As EventArgs) Handles GreenBtn.Click
        For r As Integer = 0 To StudyView.Rows.Count - 1
            With StudyView.Rows.Item(r)
                If .Cells("s_date").Style.BackColor = Color.Green Then
                    .Cells(0).Value = True
                End If
            End With
        Next
    End Sub

    Private Sub AmberBtn_Click(sender As Object, e As EventArgs) Handles AmberBtn.Click
        For r As Integer = 0 To StudyView.Rows.Count - 1
            With StudyView.Rows.Item(r)
                If .Cells("s_date").Style.BackColor = Color.DarkOrange Then
                    .Cells(0).Value = True
                End If
            End With
        Next

    End Sub

    Private Sub TestDICOM_Click(sender As Object, e As EventArgs) Handles TestDICOM.Click
        Try
            remote_host = RemoteHostTxt.Text
            remote_port = RemotePortTxt.Text
            remote_AEtitle = RemoteAETxt.Text
            local_AEtitle = LocalAETitleTxt.Text
            local_port = LocalPortTxt.Text

            Save_Network_Details()

            SyncLock echo_lock
                echo_response = 0
            End SyncLock

            Log_String("Creating DICOM client...")
            Dim echo_client = New Client.DicomClient(remote_host, remote_port, False, local_AEtitle, remote_AEtitle)
            Dim cecho = New DicomCEchoRequest()
            cecho.OnResponseReceived = AddressOf Cecho_Response
            Log_String("Adding echo request...")
            echo_client.AddRequestAsync(cecho)
            Log_String("Sending request...")
            echo_client.SendAsync()
        Catch ex As Exception
            Log_String(ex.Message)
        End Try
    End Sub

    Private Sub TestPing_Click(sender As Object, e As EventArgs) Handles TestPing.Click
        Try
            remote_host = RemoteHostTxt.Text
            remote_port = RemotePortTxt.Text
            remote_AEtitle = RemoteAETxt.Text
            local_AEtitle = LocalAETitleTxt.Text
            local_port = LocalPortTxt.Text

            Save_Network_Details()

            If My.Computer.Network.Ping(remote_host) Then
                Log_String("Ping successful")
            Else
                Log_String("Ping unsuccessful")
            End If

        Catch ex As Exception
            Log_String("Ping error - " & ex.Message)
        End Try
    End Sub

    Private Sub SaveLogBtn_Click(sender As Object, e As EventArgs) Handles SaveLogBtn.Click
        System.IO.File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory(), "Debug Logs", DateAndTime.Now.Day & "_" & DateAndTime.Now.Month & "_" & "_" & DateAndTime.Now.Hour & "_" & DateAndTime.Now.Minute & "_" & DateAndTime.Now.Second & ".txt"), MoveResponseTxt.Text)
    End Sub
End Class

