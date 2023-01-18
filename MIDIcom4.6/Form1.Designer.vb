<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ExportExcelBar = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportTxtBar = New System.Windows.Forms.ToolStripMenuItem()
        Me.SFTPUploadToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LogTxt = New System.Windows.Forms.TextBox()
        Me.ConvertBtn = New System.Windows.Forms.Button()
        Me.SubjectBox = New System.Windows.Forms.ListBox()
        Me.InputFolderBtn = New System.Windows.Forms.Button()
        Me.OutputFolderBtn = New System.Windows.Forms.Button()
        Me.InputFolderTxt = New System.Windows.Forms.TextBox()
        Me.OutputFolderTxt = New System.Windows.Forms.TextBox()
        Me.EnrolLogTxt = New System.Windows.Forms.TextBox()
        Me.EnrolFileBtn = New System.Windows.Forms.Button()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(0, 24)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(540, 182)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExportExcelBar, Me.ExportTxtBar, Me.SFTPUploadToolStripMenuItem, Me.SettingsToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(907, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ExportExcelBar
        '
        Me.ExportExcelBar.Name = "ExportExcelBar"
        Me.ExportExcelBar.Size = New System.Drawing.Size(83, 20)
        Me.ExportExcelBar.Text = "Export Excel"
        '
        'ExportTxtBar
        '
        Me.ExportTxtBar.Name = "ExportTxtBar"
        Me.ExportTxtBar.Size = New System.Drawing.Size(85, 20)
        Me.ExportTxtBar.Text = "  Export .txt  "
        '
        'SFTPUploadToolStripMenuItem
        '
        Me.SFTPUploadToolStripMenuItem.Name = "SFTPUploadToolStripMenuItem"
        Me.SFTPUploadToolStripMenuItem.Size = New System.Drawing.Size(85, 20)
        Me.SFTPUploadToolStripMenuItem.Text = "SFTP Upload"
        '
        'SettingsToolStripMenuItem
        '
        Me.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem"
        Me.SettingsToolStripMenuItem.Size = New System.Drawing.Size(61, 20)
        Me.SettingsToolStripMenuItem.Text = "Settings"
        '
        'LogTxt
        '
        Me.LogTxt.Location = New System.Drawing.Point(0, 254)
        Me.LogTxt.Multiline = True
        Me.LogTxt.Name = "LogTxt"
        Me.LogTxt.ReadOnly = True
        Me.LogTxt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.LogTxt.Size = New System.Drawing.Size(540, 171)
        Me.LogTxt.TabIndex = 2
        Me.LogTxt.WordWrap = False
        '
        'ConvertBtn
        '
        Me.ConvertBtn.Location = New System.Drawing.Point(546, 394)
        Me.ConvertBtn.Name = "ConvertBtn"
        Me.ConvertBtn.Size = New System.Drawing.Size(357, 28)
        Me.ConvertBtn.TabIndex = 3
        Me.ConvertBtn.Text = "Anonymise"
        Me.ConvertBtn.UseVisualStyleBackColor = True
        '
        'SubjectBox
        '
        Me.SubjectBox.Font = New System.Drawing.Font("OCR A Extended", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SubjectBox.FormattingEnabled = True
        Me.SubjectBox.ItemHeight = 12
        Me.SubjectBox.Location = New System.Drawing.Point(546, 48)
        Me.SubjectBox.Name = "SubjectBox"
        Me.SubjectBox.Size = New System.Drawing.Size(357, 340)
        Me.SubjectBox.TabIndex = 4
        '
        'InputFolderBtn
        '
        Me.InputFolderBtn.Location = New System.Drawing.Point(0, 205)
        Me.InputFolderBtn.Name = "InputFolderBtn"
        Me.InputFolderBtn.Size = New System.Drawing.Size(82, 23)
        Me.InputFolderBtn.TabIndex = 5
        Me.InputFolderBtn.Text = "Input Folder"
        Me.InputFolderBtn.UseVisualStyleBackColor = True
        '
        'OutputFolderBtn
        '
        Me.OutputFolderBtn.Location = New System.Drawing.Point(0, 229)
        Me.OutputFolderBtn.Name = "OutputFolderBtn"
        Me.OutputFolderBtn.Size = New System.Drawing.Size(82, 23)
        Me.OutputFolderBtn.TabIndex = 6
        Me.OutputFolderBtn.Text = "Output Folder"
        Me.OutputFolderBtn.UseVisualStyleBackColor = True
        '
        'InputFolderTxt
        '
        Me.InputFolderTxt.Location = New System.Drawing.Point(81, 207)
        Me.InputFolderTxt.Name = "InputFolderTxt"
        Me.InputFolderTxt.Size = New System.Drawing.Size(459, 20)
        Me.InputFolderTxt.TabIndex = 7
        Me.InputFolderTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'OutputFolderTxt
        '
        Me.OutputFolderTxt.Location = New System.Drawing.Point(81, 231)
        Me.OutputFolderTxt.Name = "OutputFolderTxt"
        Me.OutputFolderTxt.Size = New System.Drawing.Size(459, 20)
        Me.OutputFolderTxt.TabIndex = 8
        Me.OutputFolderTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'EnrolLogTxt
        '
        Me.EnrolLogTxt.Location = New System.Drawing.Point(627, 25)
        Me.EnrolLogTxt.Name = "EnrolLogTxt"
        Me.EnrolLogTxt.Size = New System.Drawing.Size(276, 20)
        Me.EnrolLogTxt.TabIndex = 10
        Me.EnrolLogTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'EnrolFileBtn
        '
        Me.EnrolFileBtn.Location = New System.Drawing.Point(546, 23)
        Me.EnrolFileBtn.Name = "EnrolFileBtn"
        Me.EnrolFileBtn.Size = New System.Drawing.Size(82, 23)
        Me.EnrolFileBtn.TabIndex = 9
        Me.EnrolFileBtn.Text = "Enrol. Log File"
        Me.EnrolFileBtn.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(907, 428)
        Me.Controls.Add(Me.EnrolLogTxt)
        Me.Controls.Add(Me.EnrolFileBtn)
        Me.Controls.Add(Me.OutputFolderTxt)
        Me.Controls.Add(Me.InputFolderTxt)
        Me.Controls.Add(Me.OutputFolderBtn)
        Me.Controls.Add(Me.InputFolderBtn)
        Me.Controls.Add(Me.SubjectBox)
        Me.Controls.Add(Me.ConvertBtn)
        Me.Controls.Add(Me.LogTxt)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Text = "MIDI Study DICOM Anonymiser Tool"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ExportExcelBar As ToolStripMenuItem
    Friend WithEvents SettingsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LogTxt As TextBox
    Friend WithEvents ConvertBtn As Button
    Friend WithEvents ExportTxtBar As ToolStripMenuItem
    Friend WithEvents SubjectBox As ListBox
    Friend WithEvents InputFolderBtn As Button
    Friend WithEvents OutputFolderBtn As Button
    Friend WithEvents InputFolderTxt As TextBox
    Friend WithEvents OutputFolderTxt As TextBox
    Friend WithEvents EnrolLogTxt As TextBox
    Friend WithEvents EnrolFileBtn As Button
    Friend WithEvents SFTPUploadToolStripMenuItem As ToolStripMenuItem
End Class
