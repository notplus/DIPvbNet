<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.文件ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.打开图像ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.编辑ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.负片ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.上下翻转ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.中心镜像ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.左右翻转ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu灰度直方图 = New System.Windows.Forms.ToolStripMenuItem()
        Me.灰度图像二值化ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.图像二值化ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.灰度变换ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Panel = New System.Windows.Forms.PictureBox()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripOpen = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripTextBox1 = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.直方图规定化ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.Panel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.文件ToolStripMenuItem, Me.编辑ToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(766, 25)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        '文件ToolStripMenuItem
        '
        Me.文件ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.打开图像ToolStripMenuItem})
        Me.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem"
        Me.文件ToolStripMenuItem.Size = New System.Drawing.Size(44, 21)
        Me.文件ToolStripMenuItem.Text = "文件"
        '
        '打开图像ToolStripMenuItem
        '
        Me.打开图像ToolStripMenuItem.Name = "打开图像ToolStripMenuItem"
        Me.打开图像ToolStripMenuItem.Size = New System.Drawing.Size(124, 22)
        Me.打开图像ToolStripMenuItem.Text = "打开图像"
        '
        '编辑ToolStripMenuItem
        '
        Me.编辑ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.负片ToolStripMenuItem, Me.上下翻转ToolStripMenuItem, Me.中心镜像ToolStripMenuItem, Me.左右翻转ToolStripMenuItem, Me.mnu灰度直方图, Me.灰度图像二值化ToolStripMenuItem, Me.图像二值化ToolStripMenuItem, Me.灰度变换ToolStripMenuItem, Me.直方图规定化ToolStripMenuItem})
        Me.编辑ToolStripMenuItem.Name = "编辑ToolStripMenuItem"
        Me.编辑ToolStripMenuItem.Size = New System.Drawing.Size(44, 21)
        Me.编辑ToolStripMenuItem.Text = "编辑"
        '
        '负片ToolStripMenuItem
        '
        Me.负片ToolStripMenuItem.Name = "负片ToolStripMenuItem"
        Me.负片ToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.负片ToolStripMenuItem.Text = "负片"
        '
        '上下翻转ToolStripMenuItem
        '
        Me.上下翻转ToolStripMenuItem.Name = "上下翻转ToolStripMenuItem"
        Me.上下翻转ToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.上下翻转ToolStripMenuItem.Text = "上下翻转"
        '
        '中心镜像ToolStripMenuItem
        '
        Me.中心镜像ToolStripMenuItem.Name = "中心镜像ToolStripMenuItem"
        Me.中心镜像ToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.中心镜像ToolStripMenuItem.Text = "中心镜像"
        '
        '左右翻转ToolStripMenuItem
        '
        Me.左右翻转ToolStripMenuItem.Name = "左右翻转ToolStripMenuItem"
        Me.左右翻转ToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.左右翻转ToolStripMenuItem.Text = "左右翻转"
        '
        'mnu灰度直方图
        '
        Me.mnu灰度直方图.Name = "mnu灰度直方图"
        Me.mnu灰度直方图.Size = New System.Drawing.Size(180, 22)
        Me.mnu灰度直方图.Text = "灰度直方图"
        '
        '灰度图像二值化ToolStripMenuItem
        '
        Me.灰度图像二值化ToolStripMenuItem.Name = "灰度图像二值化ToolStripMenuItem"
        Me.灰度图像二值化ToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.灰度图像二值化ToolStripMenuItem.Text = "灰度图像二值化"
        '
        '图像二值化ToolStripMenuItem
        '
        Me.图像二值化ToolStripMenuItem.Name = "图像二值化ToolStripMenuItem"
        Me.图像二值化ToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.图像二值化ToolStripMenuItem.Text = "图像二值化"
        '
        '灰度变换ToolStripMenuItem
        '
        Me.灰度变换ToolStripMenuItem.Name = "灰度变换ToolStripMenuItem"
        Me.灰度变换ToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.灰度变换ToolStripMenuItem.Text = "灰度变换"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.ToolStripStatusLabel2})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 521)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(766, 22)
        Me.StatusStrip1.TabIndex = 1
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(134, 17)
        Me.ToolStripStatusLabel1.Text = "ToolStripStatusLabel1"
        '
        'ToolStripStatusLabel2
        '
        Me.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
        Me.ToolStripStatusLabel2.Size = New System.Drawing.Size(134, 17)
        Me.ToolStripStatusLabel2.Text = "ToolStripStatusLabel2"
        '
        'Panel
        '
        Me.Panel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel.Location = New System.Drawing.Point(0, 50)
        Me.Panel.Name = "Panel"
        Me.Panel.Size = New System.Drawing.Size(766, 493)
        Me.Panel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.Panel.TabIndex = 2
        Me.Panel.TabStop = False
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripOpen, Me.ToolStripTextBox1, Me.ToolStripSeparator1, Me.ToolStripButton1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 25)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(766, 25)
        Me.ToolStrip1.TabIndex = 3
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripOpen
        '
        Me.ToolStripOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripOpen.Image = CType(resources.GetObject("ToolStripOpen.Image"), System.Drawing.Image)
        Me.ToolStripOpen.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripOpen.Name = "ToolStripOpen"
        Me.ToolStripOpen.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripOpen.Text = "ToolStripOpen"
        '
        'ToolStripTextBox1
        '
        Me.ToolStripTextBox1.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.0!)
        Me.ToolStripTextBox1.Name = "ToolStripTextBox1"
        Me.ToolStripTextBox1.Size = New System.Drawing.Size(100, 25)
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(60, 22)
        Me.ToolStripButton1.Text = "重读图像"
        '
        '直方图规定化ToolStripMenuItem
        '
        Me.直方图规定化ToolStripMenuItem.Name = "直方图规定化ToolStripMenuItem"
        Me.直方图规定化ToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.直方图规定化ToolStripMenuItem.Text = "直方图规定化"
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(766, 543)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.Panel)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "MainForm"
        Me.Text = "数字图像处理基本功能"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        CType(Me.Panel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents Panel As System.Windows.Forms.PictureBox
    Friend WithEvents 文件ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 打开图像ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 编辑ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 负片ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 上下翻转ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 中心镜像ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents 左右翻转ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents mnu灰度直方图 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 灰度图像二值化ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripTextBox1 As ToolStripTextBox
    Friend WithEvents ToolStripStatusLabel2 As ToolStripStatusLabel
    Friend WithEvents 图像二值化ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 灰度变换ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripButton1 As ToolStripButton
    Friend WithEvents ToolStripOpen As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents 直方图规定化ToolStripMenuItem As ToolStripMenuItem
End Class
