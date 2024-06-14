<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FTPForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FTPForm))
        Me.PassBox = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.UserBox = New System.Windows.Forms.TextBox()
        Me.UploadListBox = New System.Windows.Forms.CheckedListBox()
        Me.DeselectBtn = New System.Windows.Forms.Button()
        Me.SelectBtn = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.UploadBox = New System.Windows.Forms.GroupBox()
        Me.FTPBox = New System.Windows.Forms.TextBox()
        Me.IdTxt = New System.Windows.Forms.TextBox()
        Me.StateTxt = New System.Windows.Forms.TextBox()
        Me.ProgressBar = New System.Windows.Forms.ProgressBar()
        Me.deleteBox = New System.Windows.Forms.CheckBox()
        Me.UploadBtn = New System.Windows.Forms.Button()
        Me.UIChecker = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox1.SuspendLayout()
        Me.UploadBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'PassBox
        '
        Me.PassBox.Location = New System.Drawing.Point(85, 45)
        Me.PassBox.Name = "PassBox"
        Me.PassBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.PassBox.Size = New System.Drawing.Size(135, 20)
        Me.PassBox.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(26, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Password"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(25, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Username"
        '
        'UserBox
        '
        Me.UserBox.Location = New System.Drawing.Point(85, 19)
        Me.UserBox.Name = "UserBox"
        Me.UserBox.Size = New System.Drawing.Size(135, 20)
        Me.UserBox.TabIndex = 3
        '
        'UploadListBox
        '
        Me.UploadListBox.CheckOnClick = True
        Me.UploadListBox.FormattingEnabled = True
        Me.UploadListBox.Location = New System.Drawing.Point(15, 21)
        Me.UploadListBox.Name = "UploadListBox"
        Me.UploadListBox.Size = New System.Drawing.Size(230, 364)
        Me.UploadListBox.TabIndex = 5
        '
        'DeselectBtn
        '
        Me.DeselectBtn.Location = New System.Drawing.Point(15, 402)
        Me.DeselectBtn.Name = "DeselectBtn"
        Me.DeselectBtn.Size = New System.Drawing.Size(106, 23)
        Me.DeselectBtn.TabIndex = 6
        Me.DeselectBtn.Text = "Deselect All"
        Me.DeselectBtn.UseVisualStyleBackColor = True
        '
        'SelectBtn
        '
        Me.SelectBtn.Location = New System.Drawing.Point(139, 402)
        Me.SelectBtn.Name = "SelectBtn"
        Me.SelectBtn.Size = New System.Drawing.Size(106, 23)
        Me.SelectBtn.TabIndex = 7
        Me.SelectBtn.Text = "Select All"
        Me.SelectBtn.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.UserBox)
        Me.GroupBox1.Controls.Add(Me.PassBox)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(251, 21)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(247, 78)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "SFTP Login"
        '
        'UploadBox
        '
        Me.UploadBox.Controls.Add(Me.FTPBox)
        Me.UploadBox.Controls.Add(Me.IdTxt)
        Me.UploadBox.Controls.Add(Me.StateTxt)
        Me.UploadBox.Controls.Add(Me.ProgressBar)
        Me.UploadBox.Controls.Add(Me.deleteBox)
        Me.UploadBox.Controls.Add(Me.UploadBtn)
        Me.UploadBox.Location = New System.Drawing.Point(251, 105)
        Me.UploadBox.Name = "UploadBox"
        Me.UploadBox.Size = New System.Drawing.Size(247, 320)
        Me.UploadBox.TabIndex = 9
        Me.UploadBox.TabStop = False
        Me.UploadBox.Text = "Upload Settings"
        '
        'FTPBox
        '
        Me.FTPBox.Location = New System.Drawing.Point(10, 101)
        Me.FTPBox.Multiline = True
        Me.FTPBox.Name = "FTPBox"
        Me.FTPBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.FTPBox.Size = New System.Drawing.Size(225, 148)
        Me.FTPBox.TabIndex = 5
        '
        'IdTxt
        '
        Me.IdTxt.Location = New System.Drawing.Point(10, 49)
        Me.IdTxt.Name = "IdTxt"
        Me.IdTxt.ReadOnly = True
        Me.IdTxt.Size = New System.Drawing.Size(225, 20)
        Me.IdTxt.TabIndex = 4
        Me.IdTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'StateTxt
        '
        Me.StateTxt.Location = New System.Drawing.Point(10, 75)
        Me.StateTxt.Name = "StateTxt"
        Me.StateTxt.ReadOnly = True
        Me.StateTxt.Size = New System.Drawing.Size(225, 20)
        Me.StateTxt.TabIndex = 3
        Me.StateTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ProgressBar
        '
        Me.ProgressBar.Enabled = False
        Me.ProgressBar.Location = New System.Drawing.Point(10, 255)
        Me.ProgressBar.Name = "ProgressBar"
        Me.ProgressBar.Size = New System.Drawing.Size(225, 23)
        Me.ProgressBar.TabIndex = 2
        '
        'deleteBox
        '
        Me.deleteBox.AutoSize = True
        Me.deleteBox.Checked = True
        Me.deleteBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.deleteBox.Location = New System.Drawing.Point(17, 22)
        Me.deleteBox.Name = "deleteBox"
        Me.deleteBox.Size = New System.Drawing.Size(215, 17)
        Me.deleteBox.TabIndex = 1
        Me.deleteBox.Text = "Delete local files after successful upload"
        Me.deleteBox.UseVisualStyleBackColor = True
        '
        'UploadBtn
        '
        Me.UploadBtn.Location = New System.Drawing.Point(85, 284)
        Me.UploadBtn.Name = "UploadBtn"
        Me.UploadBtn.Size = New System.Drawing.Size(74, 23)
        Me.UploadBtn.TabIndex = 0
        Me.UploadBtn.Text = "Upload"
        Me.UploadBtn.UseVisualStyleBackColor = True
        '
        'FTPForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(510, 444)
        Me.Controls.Add(Me.UploadBox)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.SelectBtn)
        Me.Controls.Add(Me.DeselectBtn)
        Me.Controls.Add(Me.UploadListBox)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FTPForm"
        Me.Text = "FTPForm"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.UploadBox.ResumeLayout(False)
        Me.UploadBox.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PassBox As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents UserBox As TextBox
    Friend WithEvents UploadListBox As CheckedListBox
    Friend WithEvents DeselectBtn As Button
    Friend WithEvents SelectBtn As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents UploadBox As GroupBox
    Friend WithEvents deleteBox As CheckBox
    Friend WithEvents UploadBtn As Button
    Friend WithEvents ProgressBar As ProgressBar
    Friend WithEvents UIChecker As Timer
    Friend WithEvents FTPBox As TextBox
    Friend WithEvents IdTxt As TextBox
    Friend WithEvents StateTxt As TextBox
End Class
