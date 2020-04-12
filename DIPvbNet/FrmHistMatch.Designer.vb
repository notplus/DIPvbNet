<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmHistMatch
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
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

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.GoalImg = New System.Windows.Forms.PictureBox()
        Me.GoalHist = New System.Windows.Forms.PictureBox()
        Me.OriginalHist = New System.Windows.Forms.PictureBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        CType(Me.GoalImg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GoalHist, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OriginalHist, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GoalImg
        '
        Me.GoalImg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.GoalImg.Location = New System.Drawing.Point(12, 12)
        Me.GoalImg.Name = "GoalImg"
        Me.GoalImg.Size = New System.Drawing.Size(400, 400)
        Me.GoalImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.GoalImg.TabIndex = 0
        Me.GoalImg.TabStop = False
        '
        'GoalHist
        '
        Me.GoalHist.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.GoalHist.Location = New System.Drawing.Point(419, 12)
        Me.GoalHist.Name = "GoalHist"
        Me.GoalHist.Size = New System.Drawing.Size(426, 190)
        Me.GoalHist.TabIndex = 1
        Me.GoalHist.TabStop = False
        '
        'OriginalHist
        '
        Me.OriginalHist.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.OriginalHist.Location = New System.Drawing.Point(419, 229)
        Me.OriginalHist.Name = "OriginalHist"
        Me.OriginalHist.Size = New System.Drawing.Size(426, 183)
        Me.OriginalHist.TabIndex = 2
        Me.OriginalHist.TabStop = False
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(12, 428)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(576, 21)
        Me.TextBox1.TabIndex = 3
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(612, 426)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(104, 23)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "打开目标图片"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(737, 425)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(108, 23)
        Me.Button2.TabIndex = 5
        Me.Button2.Text = "规定化"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'FrmHistMatch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(859, 463)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.OriginalHist)
        Me.Controls.Add(Me.GoalHist)
        Me.Controls.Add(Me.GoalImg)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "FrmHistMatch"
        Me.Text = "FrmHistMatch"
        CType(Me.GoalImg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GoalHist, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OriginalHist, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GoalImg As PictureBox
    Friend WithEvents GoalHist As PictureBox
    Friend WithEvents OriginalHist As PictureBox
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
End Class
