<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReportForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ReportForm))
        Me.AccComboBox = New System.Windows.Forms.ComboBox()
        Me.SaveBtn = New System.Windows.Forms.Button()
        Me.DiscardBtn = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.ReportListBox = New System.Windows.Forms.ListBox()
        Me.FileNumberLbl = New System.Windows.Forms.Label()
        Me.ExploreBtn = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'AccComboBox
        '
        Me.AccComboBox.FormattingEnabled = True
        Me.AccComboBox.Location = New System.Drawing.Point(135, 27)
        Me.AccComboBox.Name = "AccComboBox"
        Me.AccComboBox.Size = New System.Drawing.Size(236, 21)
        Me.AccComboBox.TabIndex = 1
        '
        'SaveBtn
        '
        Me.SaveBtn.Location = New System.Drawing.Point(135, 397)
        Me.SaveBtn.Name = "SaveBtn"
        Me.SaveBtn.Size = New System.Drawing.Size(173, 24)
        Me.SaveBtn.TabIndex = 2
        Me.SaveBtn.Text = "Save"
        Me.SaveBtn.UseVisualStyleBackColor = True
        '
        'DiscardBtn
        '
        Me.DiscardBtn.Location = New System.Drawing.Point(329, 397)
        Me.DiscardBtn.Name = "DiscardBtn"
        Me.DiscardBtn.Size = New System.Drawing.Size(173, 24)
        Me.DiscardBtn.TabIndex = 3
        Me.DiscardBtn.Text = "Discard Changes"
        Me.DiscardBtn.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(135, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(99, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Accession Number:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 11)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Subject ID:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(135, 61)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(66, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Report Text:"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(135, 77)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(367, 314)
        Me.TextBox1.TabIndex = 7
        '
        'ReportListBox
        '
        Me.ReportListBox.FormattingEnabled = True
        Me.ReportListBox.Location = New System.Drawing.Point(12, 27)
        Me.ReportListBox.Name = "ReportListBox"
        Me.ReportListBox.Size = New System.Drawing.Size(117, 394)
        Me.ReportListBox.TabIndex = 8
        '
        'FileNumberLbl
        '
        Me.FileNumberLbl.AutoSize = True
        Me.FileNumberLbl.Location = New System.Drawing.Point(447, 61)
        Me.FileNumberLbl.Name = "FileNumberLbl"
        Me.FileNumberLbl.Size = New System.Drawing.Size(0, 13)
        Me.FileNumberLbl.TabIndex = 9
        Me.FileNumberLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ExploreBtn
        '
        Me.ExploreBtn.Location = New System.Drawing.Point(377, 27)
        Me.ExploreBtn.Name = "ExploreBtn"
        Me.ExploreBtn.Size = New System.Drawing.Size(125, 21)
        Me.ExploreBtn.TabIndex = 10
        Me.ExploreBtn.Text = "Show in Explorer"
        Me.ExploreBtn.UseVisualStyleBackColor = True
        '
        'ReportForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(514, 431)
        Me.Controls.Add(Me.ExploreBtn)
        Me.Controls.Add(Me.FileNumberLbl)
        Me.Controls.Add(Me.ReportListBox)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DiscardBtn)
        Me.Controls.Add(Me.SaveBtn)
        Me.Controls.Add(Me.AccComboBox)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ReportForm"
        Me.Text = "Report Text Browser"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents AccComboBox As ComboBox
    Friend WithEvents SaveBtn As Button
    Friend WithEvents DiscardBtn As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents ReportListBox As ListBox
    Friend WithEvents FileNumberLbl As Label
    Friend WithEvents ExploreBtn As Button
End Class
