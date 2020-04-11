<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmChangeGray
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
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

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.ChangedHistPanel = New System.Windows.Forms.PictureBox()
        Me.OriginHistPanel = New System.Windows.Forms.PictureBox()
        Me.FunctionCurve = New System.Windows.Forms.PictureBox()
        Me.RadioButtonNow = New System.Windows.Forms.RadioButton()
        Me.RadioButtonEqualize = New System.Windows.Forms.RadioButton()
        Me.RadioButtonCurve = New System.Windows.Forms.RadioButton()
        Me.RadioButtonLinear = New System.Windows.Forms.RadioButton()
        Me.GroupBoxCurve = New System.Windows.Forms.GroupBox()
        Me.DomainUpDown1 = New System.Windows.Forms.DomainUpDown()
        Me.RadioButton6 = New System.Windows.Forms.RadioButton()
        Me.RadioButton5 = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.TextBox6 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.GroupBoxLinear = New System.Windows.Forms.GroupBox()
        Me.LabelCursorPosition = New System.Windows.Forms.Label()
        CType(Me.ChangedHistPanel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OriginHistPanel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FunctionCurve, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBoxCurve.SuspendLayout()
        Me.GroupBoxLinear.SuspendLayout()
        Me.SuspendLayout()
        '
        'ChangedHistPanel
        '
        Me.ChangedHistPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ChangedHistPanel.Location = New System.Drawing.Point(12, 12)
        Me.ChangedHistPanel.Name = "ChangedHistPanel"
        Me.ChangedHistPanel.Size = New System.Drawing.Size(300, 200)
        Me.ChangedHistPanel.TabIndex = 0
        Me.ChangedHistPanel.TabStop = False
        '
        'OriginHistPanel
        '
        Me.OriginHistPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.OriginHistPanel.Location = New System.Drawing.Point(319, 321)
        Me.OriginHistPanel.Name = "OriginHistPanel"
        Me.OriginHistPanel.Size = New System.Drawing.Size(300, 200)
        Me.OriginHistPanel.TabIndex = 1
        Me.OriginHistPanel.TabStop = False
        '
        'FunctionCurve
        '
        Me.FunctionCurve.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.FunctionCurve.Location = New System.Drawing.Point(319, 12)
        Me.FunctionCurve.Name = "FunctionCurve"
        Me.FunctionCurve.Size = New System.Drawing.Size(300, 300)
        Me.FunctionCurve.TabIndex = 2
        Me.FunctionCurve.TabStop = False
        '
        'RadioButtonNow
        '
        Me.RadioButtonNow.AutoSize = True
        Me.RadioButtonNow.Location = New System.Drawing.Point(12, 241)
        Me.RadioButtonNow.Name = "RadioButtonNow"
        Me.RadioButtonNow.Size = New System.Drawing.Size(95, 16)
        Me.RadioButtonNow.TabIndex = 3
        Me.RadioButtonNow.TabStop = True
        Me.RadioButtonNow.Text = "当前图像数据"
        Me.RadioButtonNow.UseVisualStyleBackColor = True
        '
        'RadioButtonEqualize
        '
        Me.RadioButtonEqualize.AutoSize = True
        Me.RadioButtonEqualize.Location = New System.Drawing.Point(12, 263)
        Me.RadioButtonEqualize.Name = "RadioButtonEqualize"
        Me.RadioButtonEqualize.Size = New System.Drawing.Size(95, 16)
        Me.RadioButtonEqualize.TabIndex = 4
        Me.RadioButtonEqualize.TabStop = True
        Me.RadioButtonEqualize.Text = "直方图均衡化"
        Me.RadioButtonEqualize.UseVisualStyleBackColor = True
        '
        'RadioButtonCurve
        '
        Me.RadioButtonCurve.AutoSize = True
        Me.RadioButtonCurve.Location = New System.Drawing.Point(12, 285)
        Me.RadioButtonCurve.Name = "RadioButtonCurve"
        Me.RadioButtonCurve.Size = New System.Drawing.Size(71, 16)
        Me.RadioButtonCurve.TabIndex = 5
        Me.RadioButtonCurve.TabStop = True
        Me.RadioButtonCurve.Text = "曲线函数"
        Me.RadioButtonCurve.UseVisualStyleBackColor = True
        '
        'RadioButtonLinear
        '
        Me.RadioButtonLinear.AutoSize = True
        Me.RadioButtonLinear.Location = New System.Drawing.Point(12, 410)
        Me.RadioButtonLinear.Name = "RadioButtonLinear"
        Me.RadioButtonLinear.Size = New System.Drawing.Size(95, 16)
        Me.RadioButtonLinear.TabIndex = 6
        Me.RadioButtonLinear.TabStop = True
        Me.RadioButtonLinear.Text = "分段线性变换"
        Me.RadioButtonLinear.UseVisualStyleBackColor = True
        '
        'GroupBoxCurve
        '
        Me.GroupBoxCurve.Controls.Add(Me.DomainUpDown1)
        Me.GroupBoxCurve.Controls.Add(Me.RadioButton6)
        Me.GroupBoxCurve.Controls.Add(Me.RadioButton5)
        Me.GroupBoxCurve.Location = New System.Drawing.Point(29, 307)
        Me.GroupBoxCurve.Name = "GroupBoxCurve"
        Me.GroupBoxCurve.Size = New System.Drawing.Size(99, 100)
        Me.GroupBoxCurve.TabIndex = 7
        Me.GroupBoxCurve.TabStop = False
        Me.GroupBoxCurve.Text = "曲线参数"
        '
        'DomainUpDown1
        '
        Me.DomainUpDown1.Location = New System.Drawing.Point(7, 69)
        Me.DomainUpDown1.Name = "DomainUpDown1"
        Me.DomainUpDown1.Size = New System.Drawing.Size(79, 21)
        Me.DomainUpDown1.TabIndex = 2
        Me.DomainUpDown1.Text = "0"
        '
        'RadioButton6
        '
        Me.RadioButton6.AutoSize = True
        Me.RadioButton6.Location = New System.Drawing.Point(7, 44)
        Me.RadioButton6.Name = "RadioButton6"
        Me.RadioButton6.Size = New System.Drawing.Size(47, 16)
        Me.RadioButton6.TabIndex = 1
        Me.RadioButton6.TabStop = True
        Me.RadioButton6.Text = "指数"
        Me.RadioButton6.UseVisualStyleBackColor = True
        '
        'RadioButton5
        '
        Me.RadioButton5.AutoSize = True
        Me.RadioButton5.Location = New System.Drawing.Point(7, 21)
        Me.RadioButton5.Name = "RadioButton5"
        Me.RadioButton5.Size = New System.Drawing.Size(47, 16)
        Me.RadioButton5.TabIndex = 0
        Me.RadioButton5.TabStop = True
        Me.RadioButton5.Text = "对数"
        Me.RadioButton5.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(17, 12)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "a="
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(32, 14)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(33, 21)
        Me.TextBox1.TabIndex = 9
        Me.TextBox1.Text = "50"
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(32, 41)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(33, 21)
        Me.TextBox2.TabIndex = 10
        Me.TextBox2.Text = "240"
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(103, 16)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(33, 21)
        Me.TextBox3.TabIndex = 11
        Me.TextBox3.Text = "0"
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(103, 40)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(33, 21)
        Me.TextBox4.TabIndex = 12
        Me.TextBox4.Text = "255"
        '
        'TextBox5
        '
        Me.TextBox5.Location = New System.Drawing.Point(195, 14)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(33, 21)
        Me.TextBox5.TabIndex = 13
        Me.TextBox5.Text = "0"
        '
        'TextBox6
        '
        Me.TextBox6.Location = New System.Drawing.Point(195, 40)
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.Size = New System.Drawing.Size(33, 21)
        Me.TextBox6.TabIndex = 14
        Me.TextBox6.Text = "255"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 44)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(17, 12)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "b="
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(74, 19)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(23, 12)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "ga="
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(74, 43)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(23, 12)
        Me.Label4.TabIndex = 17
        Me.Label4.Text = "gb="
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(142, 19)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(47, 12)
        Me.Label5.TabIndex = 18
        Me.Label5.Text = "输出g0="
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(142, 43)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(47, 12)
        Me.Label6.TabIndex = 19
        Me.Label6.Text = "输出g1="
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(166, 321)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(105, 46)
        Me.Button1.TabIndex = 20
        Me.Button1.Text = "应用"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'GroupBoxLinear
        '
        Me.GroupBoxLinear.Controls.Add(Me.TextBox1)
        Me.GroupBoxLinear.Controls.Add(Me.TextBox3)
        Me.GroupBoxLinear.Controls.Add(Me.Label6)
        Me.GroupBoxLinear.Controls.Add(Me.Label3)
        Me.GroupBoxLinear.Controls.Add(Me.Label4)
        Me.GroupBoxLinear.Controls.Add(Me.Label5)
        Me.GroupBoxLinear.Controls.Add(Me.Label2)
        Me.GroupBoxLinear.Controls.Add(Me.Label1)
        Me.GroupBoxLinear.Controls.Add(Me.TextBox6)
        Me.GroupBoxLinear.Controls.Add(Me.TextBox2)
        Me.GroupBoxLinear.Controls.Add(Me.TextBox5)
        Me.GroupBoxLinear.Controls.Add(Me.TextBox4)
        Me.GroupBoxLinear.Location = New System.Drawing.Point(29, 431)
        Me.GroupBoxLinear.Name = "GroupBoxLinear"
        Me.GroupBoxLinear.Size = New System.Drawing.Size(242, 69)
        Me.GroupBoxLinear.TabIndex = 21
        Me.GroupBoxLinear.TabStop = False
        '
        'LabelCursorPosition
        '
        Me.LabelCursorPosition.AutoSize = True
        Me.LabelCursorPosition.Location = New System.Drawing.Point(343, 31)
        Me.LabelCursorPosition.Name = "LabelCursorPosition"
        Me.LabelCursorPosition.Size = New System.Drawing.Size(23, 12)
        Me.LabelCursorPosition.TabIndex = 22
        Me.LabelCursorPosition.Text = "0,0"
        '
        'FrmChangeGray
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(631, 541)
        Me.Controls.Add(Me.LabelCursorPosition)
        Me.Controls.Add(Me.GroupBoxLinear)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.GroupBoxCurve)
        Me.Controls.Add(Me.RadioButtonLinear)
        Me.Controls.Add(Me.RadioButtonCurve)
        Me.Controls.Add(Me.RadioButtonEqualize)
        Me.Controls.Add(Me.RadioButtonNow)
        Me.Controls.Add(Me.FunctionCurve)
        Me.Controls.Add(Me.OriginHistPanel)
        Me.Controls.Add(Me.ChangedHistPanel)
        Me.Name = "FrmChangeGray"
        Me.Text = "FrmChangeGray"
        CType(Me.ChangedHistPanel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OriginHistPanel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FunctionCurve, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBoxCurve.ResumeLayout(False)
        Me.GroupBoxCurve.PerformLayout()
        Me.GroupBoxLinear.ResumeLayout(False)
        Me.GroupBoxLinear.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ChangedHistPanel As PictureBox
    Friend WithEvents OriginHistPanel As PictureBox
    Friend WithEvents FunctionCurve As PictureBox
    Friend WithEvents RadioButtonNow As RadioButton
    Friend WithEvents RadioButtonEqualize As RadioButton
    Friend WithEvents RadioButtonCurve As RadioButton
    Friend WithEvents RadioButtonLinear As RadioButton
    Friend WithEvents GroupBoxCurve As GroupBox
    Friend WithEvents DomainUpDown1 As DomainUpDown
    Friend WithEvents RadioButton6 As RadioButton
    Friend WithEvents RadioButton5 As RadioButton
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents TextBox5 As TextBox
    Friend WithEvents TextBox6 As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents GroupBoxLinear As GroupBox
    Friend WithEvents LabelCursorPosition As Label
End Class
