<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.文件ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.打开图像ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.编辑ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.负片ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnu灰度直方图 = New System.Windows.Forms.ToolStripMenuItem()
        Me.灰度图像二值化ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.图像二值化ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.灰度变换ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.直方图规定化ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.加椒盐ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.中值过滤ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.均值过滤ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.卷积过滤ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.梯度滤波ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.代数运算ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.图像和ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.图像减ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.图像乘ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.图像除ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.条形图ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.钟形图ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.网格图ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.网格渐变图ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.圈圈图ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.彩色与调色板ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.编辑调色板ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.彩色通道分解ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.彩色图像转灰度图像ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.几何变换ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.X轴镜像ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Y轴镜像ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.中心镜像ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.旋转ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ResizeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.几何纠正ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripOpen = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripTextBox1 = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.Panel = New System.Windows.Forms.PictureBox()
        Me.中值过滤保持边界ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.均值过滤保持边界ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.Panel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.文件ToolStripMenuItem, Me.编辑ToolStripMenuItem, Me.代数运算ToolStripMenuItem, Me.彩色与调色板ToolStripMenuItem, Me.几何变换ToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(808, 25)
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
        Me.编辑ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.负片ToolStripMenuItem, Me.mnu灰度直方图, Me.灰度图像二值化ToolStripMenuItem, Me.图像二值化ToolStripMenuItem, Me.灰度变换ToolStripMenuItem, Me.直方图规定化ToolStripMenuItem, Me.加椒盐ToolStripMenuItem, Me.中值过滤保持边界ToolStripMenuItem, Me.中值过滤ToolStripMenuItem, Me.均值过滤保持边界ToolStripMenuItem, Me.均值过滤ToolStripMenuItem, Me.卷积过滤ToolStripMenuItem, Me.梯度滤波ToolStripMenuItem})
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
        '直方图规定化ToolStripMenuItem
        '
        Me.直方图规定化ToolStripMenuItem.Name = "直方图规定化ToolStripMenuItem"
        Me.直方图规定化ToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.直方图规定化ToolStripMenuItem.Text = "直方图规定化"
        '
        '加椒盐ToolStripMenuItem
        '
        Me.加椒盐ToolStripMenuItem.Name = "加椒盐ToolStripMenuItem"
        Me.加椒盐ToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.加椒盐ToolStripMenuItem.Text = "加椒盐"
        '
        '中值过滤ToolStripMenuItem
        '
        Me.中值过滤ToolStripMenuItem.Name = "中值过滤ToolStripMenuItem"
        Me.中值过滤ToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.中值过滤ToolStripMenuItem.Text = "中值过滤"
        '
        '均值过滤ToolStripMenuItem
        '
        Me.均值过滤ToolStripMenuItem.Name = "均值过滤ToolStripMenuItem"
        Me.均值过滤ToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.均值过滤ToolStripMenuItem.Text = "均值过滤"
        '
        '卷积过滤ToolStripMenuItem
        '
        Me.卷积过滤ToolStripMenuItem.Name = "卷积过滤ToolStripMenuItem"
        Me.卷积过滤ToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.卷积过滤ToolStripMenuItem.Text = "卷积过滤"
        '
        '梯度滤波ToolStripMenuItem
        '
        Me.梯度滤波ToolStripMenuItem.Name = "梯度滤波ToolStripMenuItem"
        Me.梯度滤波ToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.梯度滤波ToolStripMenuItem.Text = "梯度滤波"
        '
        '代数运算ToolStripMenuItem
        '
        Me.代数运算ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.图像和ToolStripMenuItem, Me.图像减ToolStripMenuItem, Me.图像乘ToolStripMenuItem, Me.图像除ToolStripMenuItem, Me.ToolStripSeparator2, Me.条形图ToolStripMenuItem, Me.钟形图ToolStripMenuItem, Me.网格图ToolStripMenuItem, Me.网格渐变图ToolStripMenuItem, Me.圈圈图ToolStripMenuItem})
        Me.代数运算ToolStripMenuItem.Name = "代数运算ToolStripMenuItem"
        Me.代数运算ToolStripMenuItem.Size = New System.Drawing.Size(68, 21)
        Me.代数运算ToolStripMenuItem.Text = "代数运算"
        '
        '图像和ToolStripMenuItem
        '
        Me.图像和ToolStripMenuItem.Name = "图像和ToolStripMenuItem"
        Me.图像和ToolStripMenuItem.Size = New System.Drawing.Size(145, 22)
        Me.图像和ToolStripMenuItem.Text = "图像和"
        '
        '图像减ToolStripMenuItem
        '
        Me.图像减ToolStripMenuItem.Name = "图像减ToolStripMenuItem"
        Me.图像减ToolStripMenuItem.Size = New System.Drawing.Size(145, 22)
        Me.图像减ToolStripMenuItem.Text = "图像减"
        '
        '图像乘ToolStripMenuItem
        '
        Me.图像乘ToolStripMenuItem.Name = "图像乘ToolStripMenuItem"
        Me.图像乘ToolStripMenuItem.Size = New System.Drawing.Size(145, 22)
        Me.图像乘ToolStripMenuItem.Text = "图像乘"
        '
        '图像除ToolStripMenuItem
        '
        Me.图像除ToolStripMenuItem.Name = "图像除ToolStripMenuItem"
        Me.图像除ToolStripMenuItem.Size = New System.Drawing.Size(145, 22)
        Me.图像除ToolStripMenuItem.Text = "图像除"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(142, 6)
        '
        '条形图ToolStripMenuItem
        '
        Me.条形图ToolStripMenuItem.Name = "条形图ToolStripMenuItem"
        Me.条形图ToolStripMenuItem.Size = New System.Drawing.Size(145, 22)
        Me.条形图ToolStripMenuItem.Text = "条形图"
        '
        '钟形图ToolStripMenuItem
        '
        Me.钟形图ToolStripMenuItem.Name = "钟形图ToolStripMenuItem"
        Me.钟形图ToolStripMenuItem.Size = New System.Drawing.Size(145, 22)
        Me.钟形图ToolStripMenuItem.Text = "钟形图"
        '
        '网格图ToolStripMenuItem
        '
        Me.网格图ToolStripMenuItem.Name = "网格图ToolStripMenuItem"
        Me.网格图ToolStripMenuItem.Size = New System.Drawing.Size(145, 22)
        Me.网格图ToolStripMenuItem.Text = "网格图"
        '
        '网格渐变图ToolStripMenuItem
        '
        Me.网格渐变图ToolStripMenuItem.Name = "网格渐变图ToolStripMenuItem"
        Me.网格渐变图ToolStripMenuItem.Size = New System.Drawing.Size(145, 22)
        Me.网格渐变图ToolStripMenuItem.Text = "网格渐变图"
        '
        '圈圈图ToolStripMenuItem
        '
        Me.圈圈图ToolStripMenuItem.Name = "圈圈图ToolStripMenuItem"
        Me.圈圈图ToolStripMenuItem.Size = New System.Drawing.Size(145, 22)
        Me.圈圈图ToolStripMenuItem.Text = "16*16圈圈图"
        '
        '彩色与调色板ToolStripMenuItem
        '
        Me.彩色与调色板ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.编辑调色板ToolStripMenuItem, Me.彩色通道分解ToolStripMenuItem, Me.彩色图像转灰度图像ToolStripMenuItem})
        Me.彩色与调色板ToolStripMenuItem.Name = "彩色与调色板ToolStripMenuItem"
        Me.彩色与调色板ToolStripMenuItem.Size = New System.Drawing.Size(92, 21)
        Me.彩色与调色板ToolStripMenuItem.Text = "彩色与调色板"
        '
        '编辑调色板ToolStripMenuItem
        '
        Me.编辑调色板ToolStripMenuItem.Name = "编辑调色板ToolStripMenuItem"
        Me.编辑调色板ToolStripMenuItem.Size = New System.Drawing.Size(184, 22)
        Me.编辑调色板ToolStripMenuItem.Text = "编辑调色板"
        '
        '彩色通道分解ToolStripMenuItem
        '
        Me.彩色通道分解ToolStripMenuItem.Name = "彩色通道分解ToolStripMenuItem"
        Me.彩色通道分解ToolStripMenuItem.Size = New System.Drawing.Size(184, 22)
        Me.彩色通道分解ToolStripMenuItem.Text = "彩色通道分解与交换"
        '
        '彩色图像转灰度图像ToolStripMenuItem
        '
        Me.彩色图像转灰度图像ToolStripMenuItem.Name = "彩色图像转灰度图像ToolStripMenuItem"
        Me.彩色图像转灰度图像ToolStripMenuItem.Size = New System.Drawing.Size(184, 22)
        Me.彩色图像转灰度图像ToolStripMenuItem.Text = "彩色图像转灰度图像"
        '
        '几何变换ToolStripMenuItem
        '
        Me.几何变换ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.X轴镜像ToolStripMenuItem, Me.Y轴镜像ToolStripMenuItem, Me.中心镜像ToolStripMenuItem1, Me.旋转ToolStripMenuItem, Me.ResizeToolStripMenuItem, Me.几何纠正ToolStripMenuItem})
        Me.几何变换ToolStripMenuItem.Name = "几何变换ToolStripMenuItem"
        Me.几何变换ToolStripMenuItem.Size = New System.Drawing.Size(68, 21)
        Me.几何变换ToolStripMenuItem.Text = "几何变换"
        '
        'X轴镜像ToolStripMenuItem
        '
        Me.X轴镜像ToolStripMenuItem.Name = "X轴镜像ToolStripMenuItem"
        Me.X轴镜像ToolStripMenuItem.Size = New System.Drawing.Size(124, 22)
        Me.X轴镜像ToolStripMenuItem.Text = "X轴镜像"
        '
        'Y轴镜像ToolStripMenuItem
        '
        Me.Y轴镜像ToolStripMenuItem.Name = "Y轴镜像ToolStripMenuItem"
        Me.Y轴镜像ToolStripMenuItem.Size = New System.Drawing.Size(124, 22)
        Me.Y轴镜像ToolStripMenuItem.Text = "Y轴镜像"
        '
        '中心镜像ToolStripMenuItem1
        '
        Me.中心镜像ToolStripMenuItem1.Name = "中心镜像ToolStripMenuItem1"
        Me.中心镜像ToolStripMenuItem1.Size = New System.Drawing.Size(124, 22)
        Me.中心镜像ToolStripMenuItem1.Text = "中心镜像"
        '
        '旋转ToolStripMenuItem
        '
        Me.旋转ToolStripMenuItem.Name = "旋转ToolStripMenuItem"
        Me.旋转ToolStripMenuItem.Size = New System.Drawing.Size(124, 22)
        Me.旋转ToolStripMenuItem.Text = "旋转"
        '
        'ResizeToolStripMenuItem
        '
        Me.ResizeToolStripMenuItem.Name = "ResizeToolStripMenuItem"
        Me.ResizeToolStripMenuItem.Size = New System.Drawing.Size(124, 22)
        Me.ResizeToolStripMenuItem.Text = "Resize"
        '
        '几何纠正ToolStripMenuItem
        '
        Me.几何纠正ToolStripMenuItem.Name = "几何纠正ToolStripMenuItem"
        Me.几何纠正ToolStripMenuItem.Size = New System.Drawing.Size(124, 22)
        Me.几何纠正ToolStripMenuItem.Text = "几何纠正"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.ToolStripStatusLabel2})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 543)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(808, 22)
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
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripOpen, Me.ToolStripTextBox1, Me.ToolStripSeparator1, Me.ToolStripButton1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 25)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(808, 25)
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
        'Panel
        '
        Me.Panel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel.Location = New System.Drawing.Point(0, 50)
        Me.Panel.Name = "Panel"
        Me.Panel.Size = New System.Drawing.Size(808, 515)
        Me.Panel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.Panel.TabIndex = 2
        Me.Panel.TabStop = False
        '
        '中值过滤保持边界ToolStripMenuItem
        '
        Me.中值过滤保持边界ToolStripMenuItem.Name = "中值过滤保持边界ToolStripMenuItem"
        Me.中值过滤保持边界ToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.中值过滤保持边界ToolStripMenuItem.Text = "中值过滤(保持边界)"
        '
        '均值过滤保持边界ToolStripMenuItem
        '
        Me.均值过滤保持边界ToolStripMenuItem.Name = "均值过滤保持边界ToolStripMenuItem"
        Me.均值过滤保持边界ToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.均值过滤保持边界ToolStripMenuItem.Text = "均值过滤(保持边界)"
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(808, 565)
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
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.Panel, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
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
    Friend WithEvents 代数运算ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 彩色与调色板ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 彩色通道分解ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 编辑调色板ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 图像和ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 图像减ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 图像乘ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 图像除ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents 条形图ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 钟形图ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 网格图ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 网格渐变图ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 圈圈图ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 加椒盐ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 中值过滤ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 均值过滤ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 卷积过滤ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 梯度滤波ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 几何变换ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents X轴镜像ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Y轴镜像ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 中心镜像ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents 旋转ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ResizeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 几何纠正ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 彩色图像转灰度图像ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 中值过滤保持边界ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 均值过滤保持边界ToolStripMenuItem As ToolStripMenuItem
End Class
