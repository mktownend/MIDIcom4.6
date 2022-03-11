<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.FoldersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InputToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OutputToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LogTxt = New System.Windows.Forms.TextBox()
        Me.ConvertBtn = New System.Windows.Forms.Button()
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
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.FoldersToolStripMenuItem, Me.AboutToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(530, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(12, 20)
        '
        'FoldersToolStripMenuItem
        '
        Me.FoldersToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.InputToolStripMenuItem, Me.OutputToolStripMenuItem})
        Me.FoldersToolStripMenuItem.Name = "FoldersToolStripMenuItem"
        Me.FoldersToolStripMenuItem.Size = New System.Drawing.Size(57, 20)
        Me.FoldersToolStripMenuItem.Text = "Folders"
        '
        'InputToolStripMenuItem
        '
        Me.InputToolStripMenuItem.Name = "InputToolStripMenuItem"
        Me.InputToolStripMenuItem.Size = New System.Drawing.Size(112, 22)
        Me.InputToolStripMenuItem.Text = "Input"
        '
        'OutputToolStripMenuItem
        '
        Me.OutputToolStripMenuItem.Name = "OutputToolStripMenuItem"
        Me.OutputToolStripMenuItem.Size = New System.Drawing.Size(112, 22)
        Me.OutputToolStripMenuItem.Text = "Output"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(52, 20)
        Me.AboutToolStripMenuItem.Text = "About"
        '
        'LogTxt
        '
        Me.LogTxt.Location = New System.Drawing.Point(0, 206)
        Me.LogTxt.Multiline = True
        Me.LogTxt.Name = "LogTxt"
        Me.LogTxt.ReadOnly = True
        Me.LogTxt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.LogTxt.Size = New System.Drawing.Size(530, 219)
        Me.LogTxt.TabIndex = 2
        Me.LogTxt.WordWrap = False
        '
        'ConvertBtn
        '
        Me.ConvertBtn.Location = New System.Drawing.Point(453, 427)
        Me.ConvertBtn.Name = "ConvertBtn"
        Me.ConvertBtn.Size = New System.Drawing.Size(75, 23)
        Me.ConvertBtn.TabIndex = 3
        Me.ConvertBtn.Text = "Anonymise"
        Me.ConvertBtn.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(530, 452)
        Me.Controls.Add(Me.ConvertBtn)
        Me.Controls.Add(Me.LogTxt)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Form1"
        Me.Text = "MIDI Study DICOM Anonymiser Tool"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents FoldersToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents InputToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OutputToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LogTxt As TextBox
    Friend WithEvents ConvertBtn As Button
End Class
