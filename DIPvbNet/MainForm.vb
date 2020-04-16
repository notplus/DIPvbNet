Public Class MainForm
    Private mImage As New ImageClass ' 图像处理类的一个窗体中实例，可以被称为对象。一般需要被直接初始化，如果没有在声明变量时实例化，则必须在使用时注意判断，需要时实例化
    Dim ImageName As String ' 记录当前处理图像的路径
    Dim isZoomAll As Boolean ' 一个开关变量，说明放大处理形式
    Private RubberColor As Color, NeedZoomOut As Boolean
    Private mx0 As Single, my0 As Single, mox As Single, moy As Single ' 这些变量在鼠标拉窗口时记录鼠标位置
    Private cmx As Single, cmy As Single ', w As Integer, h As Integer
    Private picMX0 As Single, picMY0 As Single
    Private isZoom As Boolean
    Private winX0 As Single, winY0 As Single
    Dim theRectangle As New Rectangle(New Point(0, 0), New Size(0, 0))
    Dim startPoint As Point

    Private Sub 打开图像ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 打开图像ToolStripMenuItem.Click
        Call OpenImage()
    End Sub

    Private Sub Panel_DoubleClick(sender As Object, e As System.EventArgs) Handles Panel.DoubleClick
        isZoomAll = True
        If mImage.isAvailable Then Panel.Refresh()
        isZoomAll = False
    End Sub

    Private Sub Panel_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Panel.MouseDown
        If ((e.Button = Windows.Forms.MouseButtons.Left) And ((Control.ModifierKeys And Keys.Shift) = Keys.Shift)) Then
            picMX0 = e.X  ' 保存鼠标位置
            picMY0 = e.Y
            Panel.Cursor = Cursors.Cross
            isZoom = True
        End If
        ' by using the PointToScreen method to convert form coordinates to screen coordinates.
        Dim sControl As Control = CType(sender, Control)
        ' Calculate the startPoint by using the PointToScreen method.
        startPoint = sControl.PointToScreen(New Point(e.X, e.Y))
    End Sub

    Private Sub Panel_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Panel.MouseMove
        Dim i As Integer, j As Integer
        cmx = e.X
        cmy = e.Y
        If Not mImage.isAvailable Then Exit Sub
        If mImage.ViewX > 0 And mImage.ViewY > 0 Then
            j = mImage.MapToImageX(e.X)
            i = mImage.MapToImageY(e.Y)
            ToolStripStatusLabel1.Text = "鼠标在：" & j.ToString("0") & "," & i.ToString("0") & "," & mImage.getGrey(i, j)
        End If
        If (e.Button = Windows.Forms.MouseButtons.Left) And isZoom Then
            ControlPaint.DrawReversibleFrame(theRectangle, RubberColor, FrameStyle.Thick)
            ' Calculate the endpoint and dimensions for the new rectangle, 
            ' again using the PointToScreen method.
            Dim endPoint As Point = CType(sender, Control).PointToScreen(New Point(e.X, e.Y))
            Dim sWidth As Integer = endPoint.X - startPoint.X
            Dim sHeight As Integer = endPoint.Y - startPoint.Y
            theRectangle = New Rectangle(startPoint.X, startPoint.Y, sWidth, sHeight)

            ' Draw the new rectangle by calling DrawReversibleFrame again.  
            ControlPaint.DrawReversibleFrame(theRectangle, RubberColor, FrameStyle.Thick)
        End If
    End Sub

    Private Sub Panel_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Panel.MouseUp
        If (e.Button = Windows.Forms.MouseButtons.Right) Then
            mImage.zoomInOut(1, mImage.MapToImageX(e.X), mImage.MapToImageY(e.Y))
            Panel.Refresh()
        ElseIf (e.Button = Windows.Forms.MouseButtons.Left) And isZoom Then
            ControlPaint.DrawReversibleFrame(theRectangle, RubberColor, FrameStyle.Thick)
            ' Reset the rectangle.
            theRectangle = New Rectangle(0, 0, 0, 0)

            mx0 = picMX0
            my0 = picMY0
            mox = e.X
            moy = e.Y
            Dim xx0 As Single = IIf(mx0 > mox, mox, mx0)
            Dim yy0 As Single = IIf(my0 > moy, moy, my0)
            Dim xx1 As Single = IIf(mx0 > mox, mx0, mox)
            Dim yy1 As Single = IIf(my0 > moy, my0, moy)
            If Math.Abs(xx1 - xx0) > 5.0 Or Math.Abs(yy1 - yy0) > 5.0 Then
                mImage.xWinMin = mImage.MapToImageX(xx0)
                mImage.yWinMin = mImage.MapToImageY(yy0)
                mImage.xWinMax = mImage.MapToImageX(xx1)
                mImage.yWinMax = mImage.MapToImageY(yy1)
                '利用Refresh()调用paint, 而不是mImage.ZoomImage(g)
                Panel.Refresh()
            Else
                mImage.zoomInOut(0, mImage.MapToImageX(e.X), mImage.MapToImageY(e.Y))
                Panel.Refresh()
            End If
        End If
        Panel.Cursor = Cursors.Default
        isZoom = False

    End Sub

    Private Sub Panel_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel.Paint
        If Not mImage.isAvailable Then Return
        mImage.ViewX = Panel.Width - Panel.Margin.Right
        mImage.ViewY = Panel.Height - Panel.Margin.Bottom
        e.Graphics.Clear(Color.White)

        ' 用最简单的形式显示图像
        'mImage.Display(e.Graphics, Panel.Width, Panel.Height)

        ' 依照比例显示，可能会是放大或缩小，移动等
        If (isZoomAll) Then
            mImage.zoomExtent(e.Graphics)
        Else
            mImage.ZoomImage(e.Graphics, IIf(NeedZoomOut, 0, 1))
            NeedZoomOut = True
        End If
    End Sub

    Private Sub MainForm_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        isZoomAll = False
        isZoom = False
        RubberColor = Color.Cyan
        NeedZoomOut = True
        ImageName = Application.StartupPath & "\lena.bmp"
        If Dir(ImageName) = "" Then Exit Sub
        'If mImage Is Nothing Then mImage = New ImageClass
        mImage.ReadImage(ImageName)
        ToolStripStatusLabel2.Text = "图像大小：" & mImage.Width & "*" & mImage.Height
        'mImage.Display(Panel.CreateGraphics, mImage.Width, mImage.Height)
    End Sub

    Private Sub MainForm_MouseWheel(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseWheel
        If (e.Delta > 0) Then
            mImage.zoomInOut(1, mImage.MapToImageX(cmx), mImage.MapToImageY(cmy))
        Else
            mImage.zoomInOut(0, mImage.MapToImageX(cmx), mImage.MapToImageY(cmy))
        End If
        Panel.Refresh()
    End Sub

    Private Sub MainForm_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        NeedZoomOut = False ' 改变窗口大小时，可以保持比例关系
        Panel.Refresh()
    End Sub

    Private Sub 负片ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 负片ToolStripMenuItem.Click
        mImage.Negative()
        Panel.Refresh()
        If HistogramForm.Visible Then
            mImage.Calculate_Histogram()
            HistogramForm.RefreshHist()
        End If
    End Sub

    Private Sub 图像二值化ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 图像二值化ToolStripMenuItem.Click
        mImage.Image2BlackWhite(150)
        Panel.Refresh()
        If HistogramForm.Visible Then
            mImage.Calculate_Histogram()
            HistogramForm.RefreshHist()
        End If
    End Sub

    Private Sub 中心镜像ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 中心镜像ToolStripMenuItem.Click
        mImage.MirrorO()
        Panel.Refresh()
    End Sub

    Private Sub Panel_Click(sender As Object, e As EventArgs) Handles Panel.Click

    End Sub

    Private Sub 灰度变换ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 灰度变换ToolStripMenuItem.Click
        mImage.Calculate_Histogram()
        If Not FrmChangeGray.Visible Then
            FrmChangeGray.SetImageClass(mImage)
            FrmChangeGray.Show(Me)
        Else
            FrmChangeGray.Refresh()
        End If
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        mImage.ReadImage(ImageName) '有可能造成内存泄漏
        ToolStripStatusLabel2.Text = "图像大小：" & mImage.Width & "*" & mImage.Height

        Panel.Refresh()

        If HistogramForm.Visible Then
            mImage.Calculate_Histogram()
            HistogramForm.Refresh()
        End If
    End Sub

    Private Sub 上下翻转ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 上下翻转ToolStripMenuItem.Click
        mImage.MirrorX()
        Panel.Refresh()
    End Sub

    Private Sub 直方图规定化ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 直方图规定化ToolStripMenuItem.Click
        mImage.Calculate_Histogram()
        If Not FrmHistMatch.Visible Then
            FrmHistMatch.SetImageClass(mImage)
            FrmHistMatch.Show(Me)
        Else
            FrmHistMatch.Refresh()
        End If
    End Sub

    Private Sub 彩色通道分解ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 彩色通道分解ToolStripMenuItem.Click
        mImage.Calculate_Histogram()
        If Not FrmColorSplit.Visible Then
            FrmColorSplit.SetImageClass(mImage)
            FrmColorSplit.Show(Me)
        Else
            FrmColorSplit.Refresh()
        End If
    End Sub

    Private Sub 编辑调色板ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 编辑调色板ToolStripMenuItem.Click
        If Not FrmEditPalette.Visible Then
            FrmEditPalette.SetImageClass(mImage)
            FrmEditPalette.Show(Me)
        Else
            FrmEditPalette.Refresh()
        End If
    End Sub

    Private Sub 图像和ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 图像和ToolStripMenuItem.Click

    End Sub

    Private Sub 左右翻转ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 左右翻转ToolStripMenuItem.Click
        mImage.MirrorY()
        Panel.Refresh()
    End Sub

    Private Sub ToolStripOpen_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripOpen.Click
        Call OpenImage()
    End Sub

    Private Sub OpenImage()
        If mImage Is Nothing Or Not mImage.isEmpty Then
            mImage.ReadImage(Application.StartupPath & "\lena.bmp")
        Else
            OpenFileDialog1.ShowDialog()
            If OpenFileDialog1.FileName = "" Or Dir(OpenFileDialog1.FileName) = "" Then
                Return
            Else
                mImage.ReadImage(OpenFileDialog1.FileName)
            End If
        End If
        ToolStripStatusLabel2.Text = "图像大小：" & mImage.Width & "*" & mImage.Height
        ImageName = OpenFileDialog1.FileName
        Panel.Refresh()

        If HistogramForm.Visible Then
            mImage.Calculate_Histogram()
            HistogramForm.Refresh()
        End If
    End Sub

    Private Sub mnu灰度直方图_Click(sender As System.Object, e As System.EventArgs) Handles mnu灰度直方图.Click
        mImage.Calculate_Histogram()
        If Not HistogramForm.Visible Then
            HistogramForm.m_Image = mImage
            HistogramForm.Show(Me)
        Else
            HistogramForm.Refresh()
        End If
    End Sub

    Private Sub 灰度图像二值化ToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles 灰度图像二值化ToolStripMenuItem.Click
        Dim threshold As Int16
        threshold = Val(ToolStripTextBox1.Text)
        If threshold = 0 Then threshold = 150
        mImage.Gray2WhiteBlack(threshold)
        Panel.Refresh()
        If HistogramForm.Visible Then
            mImage.Calculate_Histogram()
            HistogramForm.Refresh()
        End If
    End Sub

    Private Sub MainForm_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        'mImage.Display(Panel.CreateGraphics, mImage.Width, mImage.Height)
    End Sub

    Public Function ChangeImageClass(ByRef newImageClass As ImageClass) As Boolean
        mImage = newImageClass
    End Function
End Class
