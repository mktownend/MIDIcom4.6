<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class QueryForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(QueryForm))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RemoteAETxt = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.RemotePortTxt = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.RemoteHostTxt = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TransBtn = New System.Windows.Forms.Button()
        Me.StudyView = New System.Windows.Forms.DataGridView()
        Me.check = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.s_id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.p_id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.s_acc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.s_desc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.s_count = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.s_date = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StudyUID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StaCal = New System.Windows.Forms.MonthCalendar()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.EndCal = New System.Windows.Forms.MonthCalendar()
        Me.QueryBtn = New System.Windows.Forms.Button()
        Me.UiTimer = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.LocalPortTxt = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.LocalAETitleTxt = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.AltQueryCheck = New System.Windows.Forms.CheckBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.ModaTxt = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.IssuerTxt = New System.Windows.Forms.TextBox()
        Me.DebugCheck = New System.Windows.Forms.CheckBox()
        Me.NumLabel = New System.Windows.Forms.Label()
        Me.FirstSubUpDwn = New System.Windows.Forms.NumericUpDown()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.NumSubBox = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.FilterBox = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ClearBtn = New System.Windows.Forms.Button()
        Me.SelectBtn = New System.Windows.Forms.Button()
        Me.GreenBtn = New System.Windows.Forms.Button()
        Me.AmberBtn = New System.Windows.Forms.Button()
        Me.MoveResponseTxt = New System.Windows.Forms.TextBox()
        Me.TestDICOM = New System.Windows.Forms.Button()
        Me.TestPing = New System.Windows.Forms.Button()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.PatientNameTxt = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.PatientIDTxt = New System.Windows.Forms.TextBox()
        Me.CustomQueryBtn = New System.Windows.Forms.Button()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SaveLogBtn = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        CType(Me.StudyView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        CType(Me.FirstSubUpDwn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox6.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.RemoteAETxt)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.RemotePortTxt)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.RemoteHostTxt)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(697, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(250, 103)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "DICOM Server Details"
        '
        'RemoteAETxt
        '
        Me.RemoteAETxt.Location = New System.Drawing.Point(73, 71)
        Me.RemoteAETxt.Name = "RemoteAETxt"
        Me.RemoteAETxt.Size = New System.Drawing.Size(166, 20)
        Me.RemoteAETxt.TabIndex = 6
        Me.RemoteAETxt.Text = "DICOMserver"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(26, 74)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "AETitle"
        '
        'RemotePortTxt
        '
        Me.RemotePortTxt.Location = New System.Drawing.Point(73, 45)
        Me.RemotePortTxt.Name = "RemotePortTxt"
        Me.RemotePortTxt.Size = New System.Drawing.Size(166, 20)
        Me.RemotePortTxt.TabIndex = 4
        Me.RemotePortTxt.Text = "104"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(41, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(26, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Port"
        '
        'RemoteHostTxt
        '
        Me.RemoteHostTxt.Location = New System.Drawing.Point(73, 19)
        Me.RemoteHostTxt.Name = "RemoteHostTxt"
        Me.RemoteHostTxt.Size = New System.Drawing.Size(166, 20)
        Me.RemoteHostTxt.TabIndex = 2
        Me.RemoteHostTxt.Text = "www.dicomserver.co.uk"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Hostname"
        '
        'TransBtn
        '
        Me.TransBtn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TransBtn.Location = New System.Drawing.Point(697, 648)
        Me.TransBtn.Name = "TransBtn"
        Me.TransBtn.Size = New System.Drawing.Size(250, 23)
        Me.TransBtn.TabIndex = 2
        Me.TransBtn.Text = "Transfer"
        Me.TransBtn.UseVisualStyleBackColor = True
        '
        'StudyView
        '
        Me.StudyView.AllowUserToAddRows = False
        Me.StudyView.AllowUserToDeleteRows = False
        Me.StudyView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.StudyView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.check, Me.s_id, Me.p_id, Me.s_acc, Me.s_desc, Me.s_count, Me.s_date, Me.StudyUID})
        Me.StudyView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.StudyView.Location = New System.Drawing.Point(0, 0)
        Me.StudyView.Name = "StudyView"
        Me.StudyView.RowHeadersVisible = False
        Me.StudyView.RowHeadersWidth = 82
        Me.StudyView.Size = New System.Drawing.Size(693, 395)
        Me.StudyView.TabIndex = 3
        '
        'check
        '
        Me.check.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.check.HeaderText = "Select"
        Me.check.MinimumWidth = 10
        Me.check.Name = "check"
        Me.check.Width = 50
        '
        's_id
        '
        Me.s_id.HeaderText = "Subject ID"
        Me.s_id.MinimumWidth = 10
        Me.s_id.Name = "s_id"
        Me.s_id.Width = 65
        '
        'p_id
        '
        Me.p_id.HeaderText = "Patient ID"
        Me.p_id.MinimumWidth = 10
        Me.p_id.Name = "p_id"
        Me.p_id.Width = 150
        '
        's_acc
        '
        Me.s_acc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.s_acc.HeaderText = "Accession"
        Me.s_acc.MinimumWidth = 10
        Me.s_acc.Name = "s_acc"
        '
        's_desc
        '
        Me.s_desc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.s_desc.HeaderText = "Description"
        Me.s_desc.MinimumWidth = 10
        Me.s_desc.Name = "s_desc"
        '
        's_count
        '
        Me.s_count.HeaderText = "#"
        Me.s_count.MinimumWidth = 10
        Me.s_count.Name = "s_count"
        Me.s_count.Width = 40
        '
        's_date
        '
        Me.s_date.HeaderText = "Date"
        Me.s_date.MinimumWidth = 10
        Me.s_date.Name = "s_date"
        Me.s_date.Width = 70
        '
        'StudyUID
        '
        Me.StudyUID.HeaderText = "StudyUID"
        Me.StudyUID.MinimumWidth = 10
        Me.StudyUID.Name = "StudyUID"
        Me.StudyUID.Visible = False
        Me.StudyUID.Width = 200
        '
        'StaCal
        '
        Me.StaCal.Location = New System.Drawing.Point(12, 25)
        Me.StaCal.MaxSelectionCount = 1
        Me.StaCal.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.StaCal.Name = "StaCal"
        Me.StaCal.ShowToday = False
        Me.StaCal.ShowTodayCircle = False
        Me.StaCal.TabIndex = 4
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.StaCal)
        Me.GroupBox2.Location = New System.Drawing.Point(697, 237)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(250, 200)
        Me.GroupBox2.TabIndex = 5
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Start Date"
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.EndCal)
        Me.GroupBox3.Location = New System.Drawing.Point(697, 443)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(250, 200)
        Me.GroupBox3.TabIndex = 6
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "End Date"
        '
        'EndCal
        '
        Me.EndCal.Location = New System.Drawing.Point(12, 25)
        Me.EndCal.MaxSelectionCount = 1
        Me.EndCal.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.EndCal.Name = "EndCal"
        Me.EndCal.TabIndex = 4
        '
        'QueryBtn
        '
        Me.QueryBtn.Location = New System.Drawing.Point(549, 15)
        Me.QueryBtn.Name = "QueryBtn"
        Me.QueryBtn.Size = New System.Drawing.Size(119, 57)
        Me.QueryBtn.TabIndex = 7
        Me.QueryBtn.Text = "Query"
        Me.QueryBtn.UseVisualStyleBackColor = True
        '
        'UiTimer
        '
        Me.UiTimer.Enabled = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.Controls.Add(Me.LocalPortTxt)
        Me.GroupBox4.Controls.Add(Me.Label8)
        Me.GroupBox4.Controls.Add(Me.LocalAETitleTxt)
        Me.GroupBox4.Controls.Add(Me.Label6)
        Me.GroupBox4.Location = New System.Drawing.Point(697, 121)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(250, 77)
        Me.GroupBox4.TabIndex = 7
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "DICOM Client Details"
        '
        'LocalPortTxt
        '
        Me.LocalPortTxt.Location = New System.Drawing.Point(73, 45)
        Me.LocalPortTxt.Name = "LocalPortTxt"
        Me.LocalPortTxt.Size = New System.Drawing.Size(166, 20)
        Me.LocalPortTxt.TabIndex = 6
        Me.LocalPortTxt.Text = "104"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(41, 48)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(26, 13)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "Port"
        '
        'LocalAETitleTxt
        '
        Me.LocalAETitleTxt.Location = New System.Drawing.Point(73, 19)
        Me.LocalAETitleTxt.Name = "LocalAETitleTxt"
        Me.LocalAETitleTxt.Size = New System.Drawing.Size(166, 20)
        Me.LocalAETitleTxt.TabIndex = 2
        Me.LocalAETitleTxt.Text = "MIDIcom"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(26, 22)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(41, 13)
        Me.Label6.TabIndex = 1
        Me.Label6.Text = "AETitle"
        '
        'GroupBox5
        '
        Me.GroupBox5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox5.Controls.Add(Me.AltQueryCheck)
        Me.GroupBox5.Controls.Add(Me.Label10)
        Me.GroupBox5.Controls.Add(Me.ModaTxt)
        Me.GroupBox5.Controls.Add(Me.Label9)
        Me.GroupBox5.Controls.Add(Me.IssuerTxt)
        Me.GroupBox5.Controls.Add(Me.DebugCheck)
        Me.GroupBox5.Controls.Add(Me.NumLabel)
        Me.GroupBox5.Controls.Add(Me.FirstSubUpDwn)
        Me.GroupBox5.Controls.Add(Me.Label7)
        Me.GroupBox5.Controls.Add(Me.NumSubBox)
        Me.GroupBox5.Controls.Add(Me.Label4)
        Me.GroupBox5.Controls.Add(Me.QueryBtn)
        Me.GroupBox5.Location = New System.Drawing.Point(12, 539)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(675, 82)
        Me.GroupBox5.TabIndex = 7
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Query Settings"
        '
        'AltQueryCheck
        '
        Me.AltQueryCheck.AutoSize = True
        Me.AltQueryCheck.Location = New System.Drawing.Point(401, 55)
        Me.AltQueryCheck.Name = "AltQueryCheck"
        Me.AltQueryCheck.Size = New System.Drawing.Size(129, 17)
        Me.AltQueryCheck.TabIndex = 18
        Me.AltQueryCheck.Text = "Alternate Query Mode"
        Me.AltQueryCheck.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(202, 54)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(76, 13)
        Me.Label10.TabIndex = 17
        Me.Label10.Text = "Modality code:"
        '
        'ModaTxt
        '
        Me.ModaTxt.Location = New System.Drawing.Point(285, 51)
        Me.ModaTxt.Name = "ModaTxt"
        Me.ModaTxt.Size = New System.Drawing.Size(58, 20)
        Me.ModaTxt.TabIndex = 16
        Me.ModaTxt.Text = "*"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(9, 54)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(65, 13)
        Me.Label9.TabIndex = 15
        Me.Label9.Text = "Issuer code:"
        '
        'IssuerTxt
        '
        Me.IssuerTxt.Location = New System.Drawing.Point(80, 51)
        Me.IssuerTxt.Name = "IssuerTxt"
        Me.IssuerTxt.Size = New System.Drawing.Size(58, 20)
        Me.IssuerTxt.TabIndex = 14
        Me.IssuerTxt.Text = "*"
        '
        'DebugCheck
        '
        Me.DebugCheck.AutoSize = True
        Me.DebugCheck.Location = New System.Drawing.Point(402, 36)
        Me.DebugCheck.Name = "DebugCheck"
        Me.DebugCheck.Size = New System.Drawing.Size(88, 17)
        Me.DebugCheck.TabIndex = 13
        Me.DebugCheck.Text = "Debug Mode"
        Me.DebugCheck.UseVisualStyleBackColor = True
        '
        'NumLabel
        '
        Me.NumLabel.AutoSize = True
        Me.NumLabel.Location = New System.Drawing.Point(398, 16)
        Me.NumLabel.Name = "NumLabel"
        Me.NumLabel.Size = New System.Drawing.Size(92, 13)
        Me.NumLabel.TabIndex = 12
        Me.NumLabel.Text = "(Total: X subjects)"
        Me.NumLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FirstSubUpDwn
        '
        Me.FirstSubUpDwn.Location = New System.Drawing.Point(285, 25)
        Me.FirstSubUpDwn.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.FirstSubUpDwn.Name = "FirstSubUpDwn"
        Me.FirstSubUpDwn.Size = New System.Drawing.Size(58, 20)
        Me.FirstSubUpDwn.TabIndex = 11
        Me.FirstSubUpDwn.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(139, 27)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(146, 13)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "subjects, starting from subject"
        '
        'NumSubBox
        '
        Me.NumSubBox.FormattingEnabled = True
        Me.NumSubBox.Items.AddRange(New Object() {"1", "25", "50", "100", "All"})
        Me.NumSubBox.Location = New System.Drawing.Point(80, 24)
        Me.NumSubBox.Name = "NumSubBox"
        Me.NumSubBox.Size = New System.Drawing.Size(57, 21)
        Me.NumSubBox.TabIndex = 9
        Me.NumSubBox.Text = "1"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(22, 27)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(56, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Search for"
        '
        'FilterBox
        '
        Me.FilterBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.FilterBox.FormattingEnabled = True
        Me.FilterBox.Items.AddRange(New Object() {" ", "MR", "MR BRAIN", "MR HEAD", "MRI", "MRI BRAIN", "MRI HEAD"})
        Me.FilterBox.Location = New System.Drawing.Point(554, 512)
        Me.FilterBox.Name = "FilterBox"
        Me.FilterBox.Size = New System.Drawing.Size(133, 21)
        Me.FilterBox.TabIndex = 8
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(516, 515)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(32, 13)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Filter:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ClearBtn
        '
        Me.ClearBtn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ClearBtn.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ClearBtn.Location = New System.Drawing.Point(286, 511)
        Me.ClearBtn.Name = "ClearBtn"
        Me.ClearBtn.Size = New System.Drawing.Size(87, 21)
        Me.ClearBtn.TabIndex = 13
        Me.ClearBtn.Text = "Select None"
        Me.ClearBtn.UseVisualStyleBackColor = True
        '
        'SelectBtn
        '
        Me.SelectBtn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.SelectBtn.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SelectBtn.Location = New System.Drawing.Point(12, 511)
        Me.SelectBtn.Name = "SelectBtn"
        Me.SelectBtn.Size = New System.Drawing.Size(87, 21)
        Me.SelectBtn.TabIndex = 14
        Me.SelectBtn.Text = "Select All"
        Me.SelectBtn.UseVisualStyleBackColor = True
        '
        'GreenBtn
        '
        Me.GreenBtn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GreenBtn.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GreenBtn.ForeColor = System.Drawing.Color.Green
        Me.GreenBtn.Location = New System.Drawing.Point(100, 511)
        Me.GreenBtn.Name = "GreenBtn"
        Me.GreenBtn.Size = New System.Drawing.Size(92, 21)
        Me.GreenBtn.TabIndex = 15
        Me.GreenBtn.Text = "Select Green"
        Me.GreenBtn.UseVisualStyleBackColor = True
        '
        'AmberBtn
        '
        Me.AmberBtn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.AmberBtn.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AmberBtn.ForeColor = System.Drawing.Color.DarkOrange
        Me.AmberBtn.Location = New System.Drawing.Point(193, 511)
        Me.AmberBtn.Name = "AmberBtn"
        Me.AmberBtn.Size = New System.Drawing.Size(92, 21)
        Me.AmberBtn.TabIndex = 16
        Me.AmberBtn.Text = "Select Amber"
        Me.AmberBtn.UseVisualStyleBackColor = True
        '
        'MoveResponseTxt
        '
        Me.MoveResponseTxt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MoveResponseTxt.Location = New System.Drawing.Point(0, 0)
        Me.MoveResponseTxt.Multiline = True
        Me.MoveResponseTxt.Name = "MoveResponseTxt"
        Me.MoveResponseTxt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.MoveResponseTxt.Size = New System.Drawing.Size(693, 103)
        Me.MoveResponseTxt.TabIndex = 17
        '
        'TestDICOM
        '
        Me.TestDICOM.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TestDICOM.Location = New System.Drawing.Point(825, 204)
        Me.TestDICOM.Name = "TestDICOM"
        Me.TestDICOM.Size = New System.Drawing.Size(124, 23)
        Me.TestDICOM.TabIndex = 18
        Me.TestDICOM.Text = "Test Connection"
        Me.TestDICOM.UseVisualStyleBackColor = True
        '
        'TestPing
        '
        Me.TestPing.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TestPing.Location = New System.Drawing.Point(699, 204)
        Me.TestPing.Name = "TestPing"
        Me.TestPing.Size = New System.Drawing.Size(120, 23)
        Me.TestPing.TabIndex = 19
        Me.TestPing.Text = "Ping Host"
        Me.TestPing.UseVisualStyleBackColor = True
        '
        'GroupBox6
        '
        Me.GroupBox6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox6.Controls.Add(Me.Label11)
        Me.GroupBox6.Controls.Add(Me.PatientNameTxt)
        Me.GroupBox6.Controls.Add(Me.Label12)
        Me.GroupBox6.Controls.Add(Me.PatientIDTxt)
        Me.GroupBox6.Controls.Add(Me.CustomQueryBtn)
        Me.GroupBox6.Location = New System.Drawing.Point(12, 627)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(675, 47)
        Me.GroupBox6.TabIndex = 18
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Custom Query"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(276, 20)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(74, 13)
        Me.Label11.TabIndex = 17
        Me.Label11.Text = "Patient Name:"
        '
        'PatientNameTxt
        '
        Me.PatientNameTxt.Location = New System.Drawing.Point(356, 17)
        Me.PatientNameTxt.Name = "PatientNameTxt"
        Me.PatientNameTxt.Size = New System.Drawing.Size(184, 20)
        Me.PatientNameTxt.TabIndex = 16
        Me.PatientNameTxt.Text = "*"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(9, 20)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(57, 13)
        Me.Label12.TabIndex = 15
        Me.Label12.Text = "Patient ID:"
        '
        'PatientIDTxt
        '
        Me.PatientIDTxt.Location = New System.Drawing.Point(80, 17)
        Me.PatientIDTxt.Name = "PatientIDTxt"
        Me.PatientIDTxt.Size = New System.Drawing.Size(184, 20)
        Me.PatientIDTxt.TabIndex = 14
        Me.PatientIDTxt.Text = "*"
        '
        'CustomQueryBtn
        '
        Me.CustomQueryBtn.Location = New System.Drawing.Point(549, 13)
        Me.CustomQueryBtn.Name = "CustomQueryBtn"
        Me.CustomQueryBtn.Size = New System.Drawing.Size(119, 29)
        Me.CustomQueryBtn.TabIndex = 7
        Me.CustomQueryBtn.Text = "Custom Query"
        Me.CustomQueryBtn.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 3)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.StudyView)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.MoveResponseTxt)
        Me.SplitContainer1.Size = New System.Drawing.Size(693, 502)
        Me.SplitContainer1.SplitterDistance = 395
        Me.SplitContainer1.TabIndex = 20
        '
        'SaveLogBtn
        '
        Me.SaveLogBtn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.SaveLogBtn.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SaveLogBtn.Location = New System.Drawing.Point(402, 511)
        Me.SaveLogBtn.Name = "SaveLogBtn"
        Me.SaveLogBtn.Size = New System.Drawing.Size(87, 21)
        Me.SaveLogBtn.TabIndex = 21
        Me.SaveLogBtn.Text = "Save Log"
        Me.SaveLogBtn.UseVisualStyleBackColor = True
        '
        'QueryForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(955, 686)
        Me.Controls.Add(Me.SaveLogBtn)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.GroupBox6)
        Me.Controls.Add(Me.TestPing)
        Me.Controls.Add(Me.TestDICOM)
        Me.Controls.Add(Me.AmberBtn)
        Me.Controls.Add(Me.GreenBtn)
        Me.Controls.Add(Me.SelectBtn)
        Me.Controls.Add(Me.ClearBtn)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.FilterBox)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.TransBtn)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "QueryForm"
        Me.Text = "MIDIQuery"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.StudyView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        CType(Me.FirstSubUpDwn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label1 As Label
    Friend WithEvents RemotePortTxt As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents RemoteHostTxt As TextBox
    Friend WithEvents TransBtn As Button
    Friend WithEvents StudyView As DataGridView
    Friend WithEvents StaCal As MonthCalendar
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents EndCal As MonthCalendar
    Friend WithEvents QueryBtn As Button
    Friend WithEvents UiTimer As Timer
    Friend WithEvents RemoteAETxt As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents LocalAETitleTxt As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents NumSubBox As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents FirstSubUpDwn As NumericUpDown
    Friend WithEvents Label7 As Label
    Friend WithEvents NumLabel As Label
    Friend WithEvents FilterBox As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents ClearBtn As Button
    Friend WithEvents SelectBtn As Button
    Friend WithEvents GreenBtn As Button
    Friend WithEvents AmberBtn As Button
    Friend WithEvents MoveResponseTxt As TextBox
    Friend WithEvents LocalPortTxt As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents check As DataGridViewCheckBoxColumn
    Friend WithEvents s_id As DataGridViewTextBoxColumn
    Friend WithEvents p_id As DataGridViewTextBoxColumn
    Friend WithEvents s_acc As DataGridViewTextBoxColumn
    Friend WithEvents s_desc As DataGridViewTextBoxColumn
    Friend WithEvents s_count As DataGridViewTextBoxColumn
    Friend WithEvents s_date As DataGridViewTextBoxColumn
    Friend WithEvents StudyUID As DataGridViewTextBoxColumn
    Friend WithEvents DebugCheck As CheckBox
    Friend WithEvents TestDICOM As Button
    Friend WithEvents TestPing As Button
    Friend WithEvents Label9 As Label
    Friend WithEvents IssuerTxt As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents ModaTxt As TextBox
    Friend WithEvents GroupBox6 As GroupBox
    Friend WithEvents Label11 As Label
    Friend WithEvents PatientNameTxt As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents PatientIDTxt As TextBox
    Friend WithEvents CustomQueryBtn As Button
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents AltQueryCheck As CheckBox
    Friend WithEvents SaveLogBtn As Button
End Class
