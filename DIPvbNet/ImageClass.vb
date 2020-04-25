Public Class ImageClass
    Private mImg As Bitmap ' Bitmap是VB.NET和C#中处理图像的基础类，用于处理由像素数据定义的图像的对象
    ' 此处以指针形式申请, 也就是说，没有初始化变量，没有产生实例
    Private mImageName As String ' 记录打开图像的文件名及路径
    Private mImageType As Integer  '记录图像文件的类型    0：灰度图像；1：彩色图像（RGB）
    Private mPixels As Long, mSize As Long ' 记录图像的总像素个数及存储数据的内存大小（以字节为单位）
    Private mWidth As Long, mHeight As Long ' 图像宽度、高度，以像素为单位
    Private ImageB() As Byte ' 存储灰度图像的数据，一维数组存储。对于8位图像，一个像素即是一个字节。
    '                   每行存储的字节数必须是4的整倍数，需要时添加适当空字节数，确保与文件中记录的数据量完全一致
    Private mFwidth As Long ' 灰度图像内存中一行像素所占用的内存字节数 mFwidth = (mWidth+3)\4*4；
    Private ImageC() As Byte ' 存储彩色图像数据，也是一个一维数组存储。对于24位图像，一个像素
    ' 即是三个字节。每行存储的字节数是像素个数的三倍。每行存储的字节数还需要确保是
    ' 4的整倍数，需要时添加适当字节。
    ' ImageB和ImageC都是一个一维数组，原则上可以合并成一个数组变量，建议改为ImageData()
    Private CSize As Long ' 彩色图像数据的总字节数
    Private Cpos() As Long ' 彩色图像数据每行首字节位置
    Private mCWidth As Long ' 彩色图像每行像素存储的字节数，mCWidth = (mWidth*3+3)\4*4
    Private mHist(255) As Long ' 统计图像的灰度直方图，彩色图像记录亮度
    Private rHist(255) As Integer ' 红色直方图数据
    Private bHist(255) As Integer ' 蓝色直方图数据
    Private gHist(255) As Integer ' 绿色直方图数据
    Private mEntropy As Double, mAverage As Double, mSigma As Double, mMaxGrey As Byte, mMinGrey As Byte
    '下面的这些变量是关于状态及显示所用的
    Private isOpened As Boolean
    Private xView As Double, yView As Double
    Private xWmin As Double, yWmin As Double, xWmax As Double, yWmax As Double
    Private xWmin0 As Double, yWmin0 As Double, xWmax0 As Double, yWmax0 As Double
    Private mLastScale As Double
    ' 界面控件
    Private mPanel As PictureBox ' 绘制图片的控件
    Public pBar As ToolStripProgressBar
    Private matHist As System.Drawing.Drawing2D.Matrix


    ' 读取图像文件。实现图像对象由文件初始化的过程
    Public Function ReadImage(ByVal ImageName As String) As Integer
        If (Dir(ImageName) = "") Then  '检查要读取的图像文件是否存在？
            Return 1
        End If
        If Not (mImg Is Nothing) Then  ' 检查mImg变量是否已经初始化（实例化）
            mImg.Dispose() ' 如果对象已经存在，释放对象(析构对象)所占用的所有资源
        End If
        mImg = New Bitmap(ImageName) ' 调用Bitmap类的一个构造函数，通过图像文件建立新的对象
        getBitMapData() ' 调用本类内定义的方法，初始化自己的内存对象
        ' 设置显示图像的初始位置
        xWmin = 0
        yWmin = 0
        xWmax = mImg.Width - 1    ' 取得图像的宽度
        yWmax = mImg.Height - 1   ' 取得图像的高度
        mImageName = ImageName
        isOpened = True
    End Function

    Private Function getBitMapData() As Boolean
        ' 从BitMap对象里获取图像数据
        ' 在实际处理程序中一般不采用拷贝备份的做法，可以通过获得的数据指针，直接操作就可以了。
        ' 过程是：锁定内存，获取数据的起始地址，根据图像类型操作数据，结束锁定
        If mImg Is Nothing Then Return False ' 图像对象必须存在，否则退出
        Dim i As Integer
        Dim rect As New Rectangle(0, 0, mImg.Width, mImg.Height) ' 设置锁定图像范围的矩形
        Dim bmpData As System.Drawing.Imaging.BitmapData = mImg.LockBits(rect,
            Drawing.Imaging.ImageLockMode.ReadOnly, mImg.PixelFormat) ' 锁定图像数据

        ' BitmapData类指定位图图像的数据。 BitmapData 类由 Bitmap 类的 LockBits 和 UnlockBits 方法使用。
        Dim ptr As IntPtr = bmpData.Scan0 ' 获得图像数据的起始地址

        If mImg.PixelFormat = Imaging.PixelFormat.Format8bppIndexed Then
            ' 如果图像是8位索引图像，256彩色，256灰度图像
            mWidth = mImg.Width     '获得图像宽度
            mHeight = mImg.Height   '获得图像高度
            mFwidth = ((mWidth + 3) \ 4) * 4 '由于图像数据是每行的记录字节数为4的整倍数，估作此调整计算
            ' mWidth + 3 > mFwidth >= mWidth
            mSize = mFwidth * mHeight  ' 图像数据的大小，注意使用的是mFwidth，而不是mWidth变量
            mPixels = mWidth * mHeight ' 图像总像素个数
            ReDim ImageB(mSize - 1) ' 定义一个一维数组，保存图像数据，用于图像数据操作
            System.Runtime.InteropServices.Marshal.Copy(ptr, ImageB, 0, mSize) ' 拷贝数据
            mImageType = 0
        ElseIf mImg.PixelFormat = Imaging.PixelFormat.Format24bppRgb Then
            mWidth = mImg.Width
            mHeight = mImg.Height
            mCWidth = ((mWidth * 3 + 3) \ 4) * 4
            CSize = mCWidth * mHeight
            mPixels = mWidth * mHeight
            ReDim ImageC(CSize - 1)
            ' Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, ImageC, 0, CSize)
            mImageType = 1
            ReDim Cpos(mHeight - 1)  ' Cpos数组纪录每行在ImageC中的起始位置
            For i = 0 To mHeight - 1
                Cpos(i) = i * mCWidth
            Next i
        End If
        mImg.UnlockBits(bmpData) ' 解锁锁定的位图数据
        Return True
    End Function

    Private Function putBitMapData() As Boolean
        If mImg Is Nothing Then Return False
        Dim rect As New Rectangle(0, 0, mImg.Width, mImg.Height)
        Dim bmpData As System.Drawing.Imaging.BitmapData = mImg.LockBits(rect,
            Drawing.Imaging.ImageLockMode.WriteOnly, mImg.PixelFormat)

        ' Get the address of the first line.
        Dim ptr As IntPtr = bmpData.Scan0

        If mImg.PixelFormat = Imaging.PixelFormat.Format8bppIndexed Then
            mSize = mFwidth * mHeight
            System.Runtime.InteropServices.Marshal.Copy(ImageB, 0, ptr, mSize)
        ElseIf mImg.PixelFormat = Imaging.PixelFormat.Format24bppRgb Then
            CSize = mCWidth * mHeight
            ' Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ImageC, 0, ptr, CSize)
        End If
        ' Declare an array to hold the bytes of the bitmap.
        ' This code is specific to a bitmap with 24 bits per pixels.
        ' Unlock the bits.
        mImg.UnlockBits(bmpData)
        Return True
    End Function

    ' 显示图像，已经不使用了。
    Public Sub Display(ByRef e As Graphics, w As Integer, h As Integer)
        e.DrawImage(mImg, 0, 0, w, h)
    End Sub
    'Public Function Display(ByRef e As PictureBox, Optional ByVal flag As Integer = 0) As Integer
    '    Dim g As Graphics
    '    If Not isOpened Then Return 0 ' 检查图像文件是否已经打开
    '    '       g = e.CreateGraphics
    '    '       Dim p As New Point(0, 0)
    '    '       g.DrawImage(mImg, p)  ', TargetRec, srcRect, units)
    '    If flag = 0 Then
    '        mPanel = e
    '        xView = e.Width - e.Margin.Right
    '        yView = e.Height - e.Margin.Bottom
    '        g = e.CreateGraphics
    '    Else
    '        xView = mPanel.Width - mPanel.Margin.Right
    '        yView = mPanel.Height - mPanel.Margin.Bottom
    '        g = mPanel.CreateGraphics
    '    End If
    '    xWmin = 0
    '    yWmin = 0
    '    xWmax = mImg.Width - 1    ' 取得pictureBox在容器中的宽度
    '    yWmax = mImg.Height - 1   ' 取得pictureBox在容器中的高度
    '    ZoomImage(g)
    'End Function

    ' zoomExtent：作用是缩放到图像的最大范围
    Public Sub zoomExtent(ByVal e As Drawing.Graphics)
        If Not isOpened Then Return
        xWmin = 0
        yWmin = 0
        xWmax = mImg.Width - 1    ' 取得图像的宽度
        yWmax = mImg.Height - 1   ' 取得图像的高度
        ZoomImage(e)
    End Sub

    ' 根据指定图像中的显示位置，并根据显示容器的尺寸，确保图像显示比例，实现图像显示
    Public Function ZoomImage(ByVal e As Drawing.Graphics, Optional ByVal flag As Integer = 0) As Integer
        Dim Vx As Double, Vy As Double, wX As Double, wY As Double
        Dim s As Double
        If mImg Is Nothing Then Return 1
        If Not isOpened Then Return 1
        ' 实现图像按比例显示在视图中
        Vx = xView     ' 计算视图（图版）的宽度(要求)
        Vy = yView     ' 计算视图（图版）的高度(要求)
        wX = xWmax - xWmin ' 计算图像中要求显示的拉框宽度
        wY = yWmax - yWmin ' 计算图像中要求显示的拉框高度
        If wX = 0 And wY = 0 Then ' 避免还没有打开图像就执行该函数
            Return 0
        End If
        If (flag = 0) Or (mLastScale < 0.0000000001) Then  ' flag <> 0 使用原来的比例关系绘制图像
            s = wX / Vx             ' 宽度比例
            If s < wY / Vy Then     ' 与高度比例比较，取较大者
                s = wY / Vy
            End If
            mLastScale = s
        Else
            s = mLastScale
        End If
        '  注意： 主要由给定的xWmin,yWmin,xWmax,yWmax确定
        xWmin = xWmin + (wX - Vx * s) / 2.0#  ' 调整显示位置，保证图像居中显示
        xWmax = xWmin + Vx * s
        yWmin = yWmin + (wY - Vy * s) / 2.0#
        yWmax = yWmin + Vy * s

        Dim TargetRec(2) As PointF
        TargetRec(0).X = 0 ' 左上角
        TargetRec(0).Y = 0
        TargetRec(1).X = xView - 1 ' 右上角
        TargetRec(1).Y = 0
        TargetRec(2).X = 0 ' 左下角
        TargetRec(2).Y = yView - 1

        Dim xLeft As Integer = xWmin
        Dim xWidth As Integer = xWmax - xWmin + 1
        Dim yUpper As Integer = yWmin
        Dim yHeight As Integer = yWmax - yWmin + 1

        Dim srcRect As New Rectangle(xLeft, yUpper, xWidth, yHeight)
        Dim units As GraphicsUnit = GraphicsUnit.Pixel

        e.DrawImage(mImg, TargetRec, srcRect, units)

        xWmin0 = xWmin
        xWmax0 = xWmax
        yWmin0 = yWmin
        yWmax0 = yWmax
        ZoomImage = 0
    End Function

    ' zoomInOut处理显示放大缩小操作，flag = 0:zoomIn放大，flag = 1: zoomOut缩小
    Public Sub zoomInOut(Optional ByVal flag As Integer = 0, Optional ByVal MouseX As Single = -100.0, Optional ByVal MouseY As Single = -100.0)
        Dim winWidth As Double, winHeight As Double
        Dim winXc As Double, winYc As Double
        If Not isOpened Then Return
        ' 窗体中心
        winXc = (xWmax + xWmin) / 2.0
        winYc = (yWmax + yWmin) / 2.0
        If MouseX < -10.0 Then
            winXc = 0.0
            winYc = 0.0
        Else
            winXc = winXc - MouseX
            winYc = winYc - MouseY
        End If
        winWidth = xWmax - xWmin
        winHeight = yWmax - yWmin
        If (flag = 0) Then ' Zoom In
            '以窗体中心缩放
            'xWmin = X + x0 * 0.8 - wX * 0.4
            'xWmax = X + x0 * 0.8 + wX * 0.4
            'yWmin = Y + y0 * 0.8 - wY * 0.4
            'yWmax = Y + y0 * 0.8 + wY * 0.4
            ' 以鼠标位置为中心缩放
            xWmin = MouseX + winXc * 0.8 - winWidth * 0.4
            xWmax = MouseX + winXc * 0.8 + winWidth * 0.4
            yWmin = MouseY + winYc * 0.8 - winHeight * 0.4
            yWmax = MouseY + winYc * 0.8 + winHeight * 0.4
        ElseIf (flag = 1) Then ' Zoom Out
            ' 以窗体中心缩放
            'xWmin = X + x0 * 1.25 - wX * 0.625
            'xWmax = X + x0 * 1.25 + wX * 0.625
            'yWmin = Y + y0 * 1.25 - wY * 0.625
            'yWmax = Y + y0 * 1.25 + wY * 0.625
            ' 以下代码是解决以鼠标为缩放中心的问题
            xWmin = MouseX + winXc * 1.25 - winWidth * 0.625
            xWmax = MouseX + winXc * 1.25 + winWidth * 0.625
            yWmin = MouseY + winYc * 1.25 - winHeight * 0.625
            yWmax = MouseY + winYc * 1.25 + winHeight * 0.625
        End If
    End Sub

    Public Sub Negative() ' 图像的负片，同时了解图像数据的组织
        Dim pos As Long
        Dim i As Integer, j As Integer
        If mImg.PixelFormat = Imaging.PixelFormat.Format8bppIndexed Then
            'If mImageType = 0 Then
            For i = 0 To mHeight - 1 ' 每一行
                For j = 0 To mWidth - 1 ' 每一列
                    pos = i * mFwidth + j '用mFwidth = ((mWidth + 3)\4)*4 
                    ' pos：(i,j)像素在一维数组中位置
                    ImageB(pos) = 255 - ImageB(pos)
                Next j
            Next i
            putBitMapData()
        ElseIf mImg.PixelFormat = Imaging.PixelFormat.Format24bppRgb Then
            For i = 0 To mHeight - 1
                For j = 0 To mWidth - 1
                    pos = Cpos(i) + j * 3
                    ImageC(pos) = 255 - ImageC(pos)
                    ImageC(pos + 1) = 255 - ImageC(pos + 1)
                    ImageC(pos + 2) = 255 - ImageC(pos + 2)
                Next j
            Next i
            putBitMapData()
        End If
    End Sub

    Public Sub Gray2WhiteBlack(Threshold As Byte) ' 二值化
        Dim pos As Long
        Dim i As Integer, j As Integer
        'If mImg.PixelFormat = Imaging.PixelFormat.Format8bppIndexed Then
        If mImageType = 0 Then
            For i = 0 To mHeight - 1 ' 每一行
                For j = 0 To mWidth - 1 ' 每一列
                    pos = i * mFwidth + j '用mFwidth = ((mWidth + 3)\4)*4 
                    ' pos：(i,j)像素在一维数组中位置
                    If ImageB(pos) >= Threshold Then
                        ImageB(pos) = 255
                    Else
                        ImageB(pos) = 0
                    End If
                Next j
            Next i
            putBitMapData()
        End If
    End Sub

    Public Sub MirrorX() ' 图像的上下镜像（上下翻转），进一步了解图像数据的组织
        Dim pos As Long, pos1 As Long ' 为计算像素在一维数组中的位置而设
        Dim i As Integer, j As Integer, g As Byte
        '需要实现数据的交换
        If mImg.PixelFormat = Imaging.PixelFormat.Format8bppIndexed Then ' 处理灰度图像或8位索引图像
            For i = 0 To mHeight \ 2 - 1
                ' 运行一半行数就可以了，不然又被交换回去了！
                For j = 0 To mWidth - 1
                    pos = i * mFwidth + j  ' 必须使用mFwidth, 数组中每行像素的存储数据量的字节数为4的整倍数
                    pos1 = (mHeight - i - 1) * mFwidth + j
                    ' pos位置的数据与pos1位置的数据对调，即是(i,j)像素与(mHeight-i-1,j)数据对调，i行与倒数i行对调
                    g = ImageB(pos)
                    ImageB(pos) = ImageB(pos1)
                    ImageB(pos1) = g
                    '  x, y
                    '  temp = y
                    '  y = x
                    '  x = temp

                    '   dim x as byte, y as byte
                    '   x = x + y    X
                    '   y = x - y    X
                    '   x = x - y    X

                Next j
            Next i
        ElseIf mImg.PixelFormat = Imaging.PixelFormat.Format24bppRgb Then
            For i = 0 To (mHeight - 1) \ 2
                ' 运行一半行数就可以了
                For j = 0 To mWidth - 1
                    pos = Cpos(i) + j * 3
                    pos1 = Cpos(mHeight - i - 1) + j * 3
                    g = ImageC(pos)
                    ImageC(pos) = ImageC(pos1)
                    ImageC(pos1) = g
                    g = ImageC(pos + 1)
                    ImageC(pos + 1) = ImageC(pos1 + 1)
                    ImageC(pos1 + 1) = g
                    g = ImageC(pos + 2)
                    ImageC(pos + 2) = ImageC(pos1 + 2)
                    ImageC(pos1 + 2) = g
                Next j
            Next i
        End If
        putBitMapData()
    End Sub

    Public Sub MirrorY() ' 图像的水平镜像（左右翻转），进一步了解图像数据的组织
        Dim pos As Long, pos1 As Long
        Dim i As Integer, j As Integer, g As Byte
        '需要实现数据的交换
        If mImg.PixelFormat = Imaging.PixelFormat.Format8bppIndexed Then
            For i = 0 To mHeight - 1
                For j = 0 To mWidth \ 2 - 1
                    ' 运行半列就可以了，不然又被交换回去了！
                    pos = i * mFwidth + j  ' 必须使用mFwidth, 数组中数据为每行字节数为4的整倍数
                    pos1 = i * mFwidth + mWidth - j - 1
                    g = ImageB(pos)
                    ImageB(pos) = ImageB(pos1)
                    ImageB(pos1) = g
                Next j
            Next i
        ElseIf mImg.PixelFormat = Imaging.PixelFormat.Format24bppRgb Then
            For i = 0 To mHeight - 1
                For j = 0 To mWidth \ 2 - 1
                    ' 运行一半列数就可以了
                    pos = Cpos(i) + j * 3
                    pos1 = Cpos(i) + (mWidth - j - 1) * 3
                    g = ImageC(pos)
                    ImageC(pos) = ImageC(pos1)
                    ImageC(pos1) = g
                    g = ImageC(pos + 1)
                    ImageC(pos + 1) = ImageC(pos1 + 1)
                    ImageC(pos1 + 1) = g
                    g = ImageC(pos + 2)
                    ImageC(pos + 2) = ImageC(pos1 + 2)
                    ImageC(pos1 + 2) = g
                Next j
            Next i
        End If
        putBitMapData()
    End Sub

    Public Sub MirrorO() ' 图像的中心镜像，进一步了解图像数据的组织
        Dim pos As Long, pos1 As Long
        Dim i As Integer, j As Integer, g As Byte
        '需要实现数据的交换
        If mImg.PixelFormat = Imaging.PixelFormat.Format8bppIndexed Then
            For i = 0 To (mHeight - 1) \ 2
                ' 运行一半行数就可以了
                For j = 0 To mWidth - 1
                    If (i = (mHeight - 1 - i)) And (j > mWidth \ 2) Then Continue For
                    ' 处理当行数为奇数时，中间的一行只要交换一半就够了，否则就相当于没有改变中间一行
                    pos = i * mFwidth + j  ' 必须使用mFwidth, 数组中数据为每行字节数为4的整倍数
                    pos1 = (mHeight - i - 1) * mFwidth + mWidth - j - 1
                    g = ImageB(pos)
                    ImageB(pos) = ImageB(pos1)
                    ImageB(pos1) = g
                Next j
            Next i
        ElseIf mImg.PixelFormat = Imaging.PixelFormat.Format24bppRgb Then
            For i = 0 To (mHeight - 1) \ 2
                For j = 0 To mWidth - 1
                    ' 运行一半列数就可以了
                    If (i = (mHeight - i - 1)) And (j > mWidth \ 2) Then Continue For
                    pos = Cpos(i) + j * 3
                    pos1 = Cpos(mHeight - i - 1) + (mWidth - j - 1) * 3
                    g = ImageC(pos)
                    ImageC(pos) = ImageC(pos1)
                    ImageC(pos1) = g
                    g = ImageC(pos + 1)
                    ImageC(pos + 1) = ImageC(pos1 + 1)
                    ImageC(pos1 + 1) = g
                    g = ImageC(pos + 2)
                    ImageC(pos + 2) = ImageC(pos1 + 2)
                    ImageC(pos1 + 2) = g
                Next j
            Next i
        End If
        putBitMapData()
        ' 当行数为奇数时，处理中间一行时只要处理一半就够了，不然会被恢复原始形式
        ' 实际上，mirrorX和mirrorY也存在这个问题，实际上中间一行交换两次，恢复原始形式
        ' 好像少交换了一行或一列而已，感觉不到
    End Sub

    ' 计算图像的直方图及其统计数据
    Public Function Calculate_Histogram() As Boolean
        Dim i As Long, j As Long, pos As Long ' , c As Long, g As Byte
        If mImageType = 0 Then
            'If mImg.PixelFormat = Imaging.PixelFormat.Format8bppIndexed then
            ' 计算灰度图像的直方图数据  mHist(0-255) as long
            ' 直方图计算主要是统计各个灰度值的像素个数，计算过程是个累计过程
            ' 首先必须清空累计数据
            For i = 0 To 255
                mHist(i) = 0  ' dim mHist(255) as long ' 固定大小的数组
            Next i
            'Erase mHist
            ' Position(i) = i * mFwidth
            'For g = 0 To 255
            '    For i = 0 To mHeight - 1
            '        For j = 0 To mWidth - 1
            '            pos = i * mFwidth + j
            '            If ImageB(pos) = g Then
            '                mHist(g) = mHist(g) + 1
            '            End If
            '        Next j
            '    Next i
            'Next g
            For i = 0 To mHeight - 1
                For j = 0 To mWidth - 1
                    pos = i * mFwidth + j
                    'mHist(ImageB(pos)) = mHist(ImageB(pos)) + 1
                    mHist(ImageB(pos)) += 1
                    'Dim g As Byte : For g = 0 To 255 : If ImageB(pos) = g Then : mHist(g) = mHist(g) + 1 : End If : Next g
                Next j
            Next i

            ' 计算统计值
            ' 计算平均灰度
            mAverage = 0.0#
            For i = 1 To 255 ' 利用直方图统计数据计算
                mAverage = mAverage + mHist(i) * CDbl(i)
            Next i
            mAverage = mAverage / mPixels
            '  均方差
            mSigma = 0.0#
            For i = 0 To mHeight - 1
                For j = 0 To mWidth - 1
                    pos = i * mFwidth + j
                    mSigma = mSigma + (mAverage - ImageB(pos)) ^ 2
                Next j
            Next i
            mSigma = Math.Sqrt(mSigma / mPixels)

            '最大灰度
            For i = 255 To 0 Step -1 ' 直方图统计数组中最大的非零序号即为最大灰度
                If mHist(i) <> 0 Then
                    mMaxGrey = i
                    Exit For
                End If
            Next i
            '最小灰度
            For i = 0 To 255 ' 直方图统计数组中最小的非零序号即为最小灰度
                If mHist(i) <> 0 Then
                    mMinGrey = i
                    Exit For
                End If
            Next i
            '    // set entropy
            '   计算图像的熵
            Dim s As Double, p As Double, p2 As Double
            p2 = Math.Log(2.0)
            s = 0.0#
            For i = 0 To 255
                If (mHist(i) <> 0) Then
                    p = CDbl(mHist(i)) / mPixels
                    s = s + p * Math.Log(p) / p2 ' 换底公式计算对数
                End If
            Next i
            mEntropy = -s
        ElseIf mImageType = 1 Then '对于 RGB 全彩色图像
            ' 分别统计红、绿、蓝通道的直方图数据，同时计算灰度值并统计
            Dim cb, cg, cr, g As Byte ' c As Long,
            ' 清零
            For i = 0 To 255
                mHist(i) = 0
                rHist(i) = 0
                gHist(i) = 0
                bHist(i) = 0
            Next i
            For i = 0 To mHeight - 1
                For j = 0 To mWidth - 1
                    pos = Cpos(i) + j * 3
                    cb = ImageC(pos)
                    cg = ImageC(pos + 1)
                    cr = ImageC(pos + 2)
                    g = Fix(cb * 0.114 + cg * 0.587 + cr * 0.299)
                    mHist(g) = mHist(g) + 1
                    rHist(cr) = rHist(cr) + 1
                    gHist(cg) = gHist(cg) + 1
                    bHist(cb) = bHist(cb) + 1
                Next j
            Next i
            'For i = 0 To mPixels - 1
            '    c = i * 3  ' 这样的处理形式是错误的！
            '    cb = ImageC(c)
            '    cg = ImageC(c + 1)
            '    cr = ImageC(c + 2)
            '    g = Fix(cb * 0.114 + cg * 0.587 + cr * 0.299)
            '    mHist(g) = mHist(g) + 1
            '    rHist(cr) = rHist(cr) + 1
            '    gHist(cg) = gHist(cg) + 1
            '    bHist(cb) = bHist(cb) + 1
            'Next i
        End If
        Return True
    End Function

    Public Function DrawHist(ByVal g As Graphics, ByVal w As Integer, ByVal h As Integer, Optional ByVal Style As Integer = 0) As Boolean
        Dim MaxH As Long, i As Long
        ' w 是绘制直方图的画板宽度，h 是绘制直方图的画板高度
        ' MaxH是需要计算直方图数据中的最大值
        ' 基本原则是：横向和纵向需要做适当的变换，使得横坐标范围能容纳0-255灰度级，纵向能容纳0-MaxH。
        ' 在边缘部分应该留有一定的空间
        ' 已知值mHist(0~255) as long，表示0-255灰度值的像素个数
        Dim sx As Single, sy As Single
        Dim x1 As Single, y1 As Single, x2 As Single, y2 As Single
        Dim x0 As Single, y0 As Single
        Dim ft As Font = New Font("Arial Narrow", 8, FontStyle.Regular, GraphicsUnit.Point)
        Dim B As Brush = New SolidBrush(Color.Black)
        'Dim r As RectangleF
        Dim p As New Pen(Color.Red)  '指定课绘制坐标轴线的颜色
        'MaxH = h
        'For x1 = 50 To 200 Step 50
        '    y1 = 0
        '    x2 = x1
        '    y2 = MaxH * 0.025
        '    e.DrawLine(p, x1, y1, x2, y2)
        '    Dim txt As String = x1.ToString("0")
        '    'e.DrawRectangle(p, x2, y2 * 2, 30.0F, -y2 * 2)
        '    e.DrawString(txt, ft, B, x1 - 20, y2 * 2)
        'Next x1
        If mImageType = 0 Then
            ' 求各灰度值像素个数最大的值，显示时确定范围
            MaxH = mHist(0)
            For i = 1 To 255
                If MaxH < mHist(i) Then MaxH = mHist(i)
            Next i '  获得了最大的mHist
            ' 256，h * 0.9   => 270, h
            ' 假定横向的最大为270
            ' 直方图有效区域为（0,5%*h）-（255,10%*h）
            sx = w / 270.0 ' 设定宽度为270
            sy = h / (MaxH * 1.2) ' 高度放大20%
            g.ScaleTransform(sx, -sy) ' 设置比例系数，纵向为负表示把竖轴的原点移到下面
            x0 = 5.0
            y0 = -MaxH * 1.1   ' 图形原点（0,0）上移10%
            g.TranslateTransform(x0, y0) ' 设置坐标偏移
            matHist = g.Transform
            matHist.Invert() ' 保留一个坐标逆变换的矩阵
            ' 测试坐标体系
            'Dim testPT(3) As PointF
            'testPT(0).X = 0
            'testPT(0).Y = 0
            'testPT(1).X = w - 1
            'testPT(1).Y = h - 1
            'testPT(2).X = w - 1
            'testPT(2).Y = 0
            'testPT(3).X = 0
            'testPT(3).Y = h - 1
            'matHist.TransformPoints(testPT)
            'g.DrawLine(New Pen(Color.LightGray), testPT(0), testPT(1))
            'g.DrawLine(New Pen(Color.LightGray), testPT(2), testPT(3))
            'g.DrawRectangle(New Pen(Color.Blue), testPT(0), )
            ' 首先绘制坐标轴，纵横线
            x1 = -3  ' 横轴线起点于-3
            y1 = 0   ' 横轴线
            x2 = 260 '横轴线终止于260 
            y2 = y1 ' 横轴线为水平线
            g.DrawLine(p, x1, y1, x2, y2)  ' 绘制（x1,y1）-（x2,y2）
            x1 = 0 ' 竖轴线
            y1 = MaxH * 1.05   ' 竖轴线起始于下部5%至顶部5%（注意横轴线在离底部5%处）
            x2 = x1
            y2 = -MaxH * 0.025
            g.DrawLine(p, x1, y1, x2, y2)
            x1 = 255 ' 在横线255处画一条小短竖线
            y1 = MaxH * 0.025
            x2 = x1
            y2 = -y1
            g.DrawLine(p, x1, y1, x2, y2)
            For x1 = 50 To 200 Step 50
                y1 = 0
                x2 = x1
                y2 = -MaxH * 0.025
                g.DrawLine(p, x1, y1, x2, y2)
            Next x1
            g.ResetTransform() ' 为了输出文字标记，取消坐标变换
            Dim txt As String
            ' Set format of string.
            Dim drawFormat As New StringFormat
            drawFormat.Alignment = StringAlignment.Center

            For x1 = 0 To 200 Step 50
                x2 = (x1 + x0) * sx
                y2 = -(-MaxH * 0.025 + y0) * sy
                txt = x1.ToString("0")
                g.DrawString(txt, ft, B, x2, y2, drawFormat)
            Next x1
            x1 = 255
            x2 = (x1 + x0) * sx
            y2 = -(-MaxH * 0.025 + y0) * sy
            txt = x1.ToString("0")
            g.DrawString(txt, ft, B, x2, y2, drawFormat)
            g.ScaleTransform(sx, -sy) ' 设置比例系数，纵向为负表示把竖轴的原点移到下面
            g.TranslateTransform(x0, y0) ' 设置坐标偏移
            B.Dispose()
            p.Dispose()
            Dim p1 As New Pen(Color.Black)
            If Style = 0 Then  ' 折线
                x1 = 0
                y1 = mHist(0)
                For i = 0 To 254
                    x2 = i + 1
                    y2 = mHist(i + 1)
                    g.DrawLine(p1, x1, y1, x2, y2)
                    x1 = x2
                    y1 = y2
                Next
            Else
                B = New SolidBrush(Color.Blue)
                For i = 0 To 255 ' 用竖线表示直方图信息
                    x1 = i - 0.5
                    y1 = 0
                    x2 = i + 0.5
                    y2 = mHist(i)
                    g.FillRectangle(B, x1, y1, x2 - x1, y2 - y1)
                Next
                B.Dispose()
            End If
            p1.Dispose()
            Return True
        Else
            ' 求各灰度值像素个数最大的值，显示时确定范围
            MaxH = mHist(0)
            For i = 1 To 255
                If MaxH < mHist(i) Then MaxH = mHist(i)
            Next i
            For i = 0 To 255
                If MaxH < rHist(i) Then MaxH = rHist(i)
            Next i
            For i = 0 To 255
                If MaxH < gHist(i) Then MaxH = gHist(i)
            Next i
            For i = 0 To 255
                If MaxH < bHist(i) Then MaxH = bHist(i)
            Next i '  获得了最大的mHist
            'Dim p As New Pen(Color.Magenta)  '指定课绘制坐标轴线的颜色
            p = New Pen(Color.Magenta)
            sx = w / 270.0 ' 设定宽度为270
            sy = h / (MaxH * 1.2) ' 高度放大20%
            g.ScaleTransform(sx, -sy) ' 设置比例系数，纵向为负表示把竖轴的原点移到下面
            x0 = 5.0
            y0 = -MaxH * 1.1   ' 图形原点（0,0）上移10%
            g.TranslateTransform(x0, y0) ' 设置坐标偏移
            ' 首先绘制坐标轴，纵横线
            x1 = -3  ' 横轴线起点于-3
            y1 = 0   ' 横轴线
            x2 = 260 '横轴线终止于260 
            y2 = y1 ' 横轴线为水平线
            g.DrawLine(p, x1, y1, x2, y2)  ' 绘制（x1,y1）-（x2,y2）
            x1 = 0 ' 竖轴线
            y1 = MaxH * 1.05   ' 竖轴线起始于下部5%至顶部5%（注意横轴线在离底部5%处）
            x2 = x1
            y2 = -MaxH * 0.025
            g.DrawLine(p, x1, y1, x2, y2)
            x1 = 255 ' 在横线255处画一条小短线
            y1 = MaxH * 0.025
            x2 = x1
            y2 = 0
            g.DrawLine(p, x1, y1, x2, y2)
            For x1 = 50 To 200 Step 50
                y1 = 0
                x2 = x1
                y2 = -MaxH * 0.025
                g.DrawLine(p, x1, y1, x2, y2)
            Next x1
            p.Dispose()
            g.ResetTransform()
            Dim txt As String
            ' Set format of string.
            Dim drawFormat As New StringFormat
            drawFormat.Alignment = StringAlignment.Center
            For x1 = 0 To 200 Step 50
                x2 = (x1 + x0) * sx
                y2 = -(-MaxH * 0.025 + y0) * sy
                txt = x1.ToString("0")
                g.DrawString(txt, ft, B, x2, y2, drawFormat)
            Next x1
            x1 = 255
            x2 = (x1 + x0) * sx
            y2 = -(-MaxH * 0.025 + y0) * sy
            txt = x1.ToString("0")
            g.DrawString(txt, ft, B, x2, y2, drawFormat)
            g.ScaleTransform(sx, -sy) ' 设置比例系数，纵向为负表示把竖轴的原点移到下面
            g.TranslateTransform(x0, y0) ' 设置坐标偏移
            p.Dispose()
            p = New Pen(Color.Black)
            x1 = 0
            y1 = mHist(0)
            For i = 0 To 254
                x2 = i + 1
                y2 = mHist(i + 1)
                g.DrawLine(p, x1, y1, x2, y2)
                x1 = x2
                y1 = y2
            Next
            p.Dispose()
            p = New Pen(Color.Red)
            x1 = 0
            y1 = rHist(0)
            For i = 0 To 254
                x2 = i + 1
                y2 = rHist(i + 1)
                g.DrawLine(p, x1, y1, x2, y2)
                x1 = x2
                y1 = y2
            Next
            p.Dispose()
            p = New Pen(Color.Green)
            x1 = 0
            y1 = gHist(0)
            For i = 0 To 254
                x2 = i + 1
                y2 = gHist(i + 1)
                g.DrawLine(p, x1, y1, x2, y2)
                x1 = x2
                y1 = y2
            Next
            p.Dispose()
            p = New Pen(Color.Blue)
            x1 = 0
            y1 = bHist(0)
            For i = 0 To 254
                x2 = i + 1
                y2 = bHist(i + 1)
                g.DrawLine(p, x1, y1, x2, y2)
                x1 = x2
                y1 = y2
            Next
            p.Dispose()
        End If
    End Function

    Public Function getHistNumber(ByVal x As Single) As String
        Dim s As Double, g As Integer
        Dim pt(0) As PointF
        pt(0) = New PointF(x, 0)
        If Not matHist Is Nothing Then
            matHist.TransformPoints(pt)
            g = pt(0).X
        Else
            g = x
        End If
        If g < 0 Or g > 255 Then
            s = -1
        Else
            s = mHist(g)
        End If
        If s >= 0 Then
            Return "(" + g.ToString("0") + "," + s.ToString("0") + ")"
        Else
            Return ""
        End If
    End Function

    Public Function MapToImageX(ByVal x As Single) As Single
        Return x / xView * (xWmax0 - xWmin0) + xWmin0
    End Function

    Public Function MapToImageY(ByVal y As Single) As Single
        Return y / yView * (yWmax0 - yWmin0) + yWmin0
    End Function

    Public Function getGrey(i As Integer, j As Integer) As Byte
        Dim pos As Long
        If ImageType() <> 0 Then Return 0
        If j < 0 Or i < 0 Then Return 0
        If (j > mWidth - 1) Or (i > mHeight - 1) Then Return 0
        'pos = j * mFwidth + i
        pos = i * mFwidth + j
        Return ImageB(pos)
    End Function
    Public Function GetRGB(i As Integer, j As Integer, channel As Char) As Byte
        Dim pos As Long
        If ImageType() <> 1 Then Return 0
        If j < 0 Or i < 0 Then Return 0
        If (j > mWidth - 1) Or (i > mHeight - 1) Then Return 0
        'pos = j * mFwidth + i
        pos = Cpos(i) + j * 3
        Select Case channel
            Case "r"
                pos += 2
            Case "g"
                pos += 1
            Case "b"
                pos += 0
        End Select
        Return ImageC(pos)
    End Function

    Public Property xWinMin() As Double
        Get
            Return xWmin
        End Get
        Set(ByVal value As Double)
            xWmin = value
        End Set
    End Property

    Public Property xWinMax() As Double
        Get
            Return xWmax
        End Get
        Set(ByVal value As Double)
            xWmax = value
        End Set
    End Property

    Public Property yWinMin() As Double
        Get
            Return yWmin
        End Get
        Set(ByVal value As Double)
            yWmin = value
        End Set
    End Property

    Public Property yWinMax() As Double
        Get
            Return yWmax
        End Get
        Set(ByVal value As Double)
            yWmax = value
        End Set
    End Property

    Public Property ViewX() As Double
        Get
            Return xView
        End Get
        Set(ByVal value As Double)
            xView = value
        End Set
    End Property

    Public Property ViewY() As Double
        Get
            Return yView
        End Get
        Set(ByVal value As Double)
            yView = value
        End Set
    End Property

    Public Function isEmpty() As Boolean
        Return isOpened
    End Function

    Public Function ImageType() As Integer
        Return mImageType
    End Function

    Public Function isAvailable() As Boolean
        If mImg Is Nothing Or Not isOpened Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Function Width() As Long
        Return mWidth
    End Function
    Public Function Height() As Long
        Return mHeight
    End Function

    Public Function Pixels() As Long
        Return mPixels
    End Function

    Public Function Entropy() As Double
        Return mEntropy
    End Function

    Public Function Sigma() As Double
        Return mSigma
    End Function

    Public Function MaxGrey() As Long
        Return mMaxGrey
    End Function

    Public Function MinGrey() As Long
        Return mMinGrey
    End Function

    Public Function Average() As Double
        Return mAverage
    End Function

    Public Sub New()
        isOpened = False
    End Sub

    Public Sub Image2BlackWhite(Threshold As Byte)
        Dim pos As Long
        Dim i As Integer, j As Integer
        'If mImg.PixelFormat = Imaging.PixelFormat.Format8bppIndexed Then
        If mImageType = 0 Then
            For i = 0 To mHeight - 1 ' 每一行
                For j = 0 To mWidth - 1 ' 每一列
                    pos = i * mFwidth + j '用mFwidth = ((mWidth + 3)\4)*4 
                    ' pos：(i,j)像素在一维数组中位置
                    If ImageB(pos) > Threshold Then
                        ImageB(pos) = 255
                    Else
                        ImageB(pos) = 0
                    End If
                Next j
            Next i
            putBitMapData()
        End If
    End Sub

    Public Function histEqualize(ByRef transformG() As Byte) As Boolean
        Dim p(255) As Double
        Dim s(255) As Double
        If mImageType = 0 Then
            For i = 0 To 255
                p(i) = mHist(i) / CDbl(mPixels)
            Next
            s(0) = p(0)
            For i = 1 To 255
                s(i) = s(i - 1) + p(i)
            Next
            For i = 0 To 255
                transformG(i) = CInt(s(i) * 255)
            Next
            Return True
        End If

    End Function
    Public Function histEqualize(ByRef transformG() As Byte, ByRef transformGR() As Byte, ByRef transformGG() As Byte, ByRef transformGB() As Byte) As Boolean
        Dim p(255), pr(255), pg(255), pb(255) As Double
        Dim s(255), sr(255), sg(255), sb(255) As Double

        If mImageType = 1 Then
            For i = 0 To 255
                p(i) = mHist(i) / CDbl(mPixels)
                pr(i) = rHist(i) / CDbl(mPixels)
                pg(i) = gHist(i) / CDbl(mPixels)
                pb(i) = bHist(i) / CDbl(mPixels)
            Next
            s(0) = p(0) : sr(0) = pr(0) : sg(0) = pg(0) : sb(0) = pb(0)
            For i = 1 To 255
                s(i) = s(i - 1) + p(i)
                sr(i) = sr(i - 1) + pr(i)
                sg(i) = sg(i - 1) + pg(i)
                sb(i) = sb(i - 1) + pb(i)
            Next
            For i = 0 To 255
                transformG(i) = CInt(s(i) * 255)
                transformGR(i) = CInt(sr(i) * 255)
                transformGG(i) = CInt(sg(i) * 255)
                transformGB(i) = CInt(sb(i) * 255)
            Next
            Return True
        End If


    End Function

    Public Function GrayTransform(ByVal transformG() As Byte) As Boolean
        If mImageType = 0 Then
            For i = 0 To mHeight - 1
                For j = 0 To mWidth - 1
                    ImageB(i * mFwidth + j) = transformG(ImageB(i * mFwidth + j))
                Next
            Next
            putBitMapData()
            Return True
        End If

    End Function
    Public Function GrayTransform(ByVal transformGR() As Byte, ByVal transformGG() As Byte, ByVal transformGB() As Byte) As Boolean
        If mImageType = 1 Then
            Dim pos As Long
            For i = 0 To mHeight - 1
                For j = 0 To mWidth - 1
                    pos = Cpos(i) + j * 3
                    ImageC(pos) = transformGB(ImageC(pos))
                    ImageC(pos + 1) = transformGG(ImageC(pos + 1))
                    ImageC(pos + 2) = transformGR(ImageC(pos + 2))
                Next
            Next
            putBitMapData()
            Return True
        End If

    End Function

    Public Function Clone(ByRef m_image_ptr As ImageClass) As Boolean
        m_image_ptr = New ImageClass(mImg)

        'm_image_ptr.ReadImage(mImageName)
        m_image_ptr.getBitMapData()
        m_image_ptr.Calculate_Histogram()
    End Function

    Public Function GetImageType() As Integer
        Return mImageType
    End Function

    Public Sub New(ByRef a_bitmap As Bitmap)
        mImg = a_bitmap.Clone()
        isOpened = True
        xWmin = 0
        yWmin = 0
        xWmax = mImg.Width - 1    ' 取得图像的宽度
        yWmax = mImg.Height - 1   ' 取得图像的高度
        'getBitMapData()
        'Calculate_Histogram()
    End Sub

    Public Function GetmHist(ByRef ptr() As Long) As Boolean
        Array.Copy(mHist, ptr, 256)
    End Function

    Public Function GetmPixels() As Long
        Return mPixels
    End Function

    Public Function GetPalette() As Imaging.ColorPalette
        Return mImg.Palette
    End Function

    Public Sub SetPalette(ByVal Pal As Imaging.ColorPalette)
        If Not Pal Is Nothing Then
            mImg.Palette = Pal
        End If
    End Sub
    Private Sub SetRGBData(ByVal pos As Long, ByVal r As Byte, ByVal g As Byte, ByVal b As Byte)
        ImageC(pos) = b
        ImageC(pos + 1) = g
        ImageC(pos + 2) = r
    End Sub
    Public Function ExchangeChannel(ByVal way As String) As Boolean
        If mImageType = 1 Then
            Dim pos As Long
            For i = 0 To mHeight - 1
                For j = 0 To mWidth - 1
                    pos = Cpos(i) + j * 3
                    Dim r, g, b As Byte
                    r = ImageC(pos + 2)
                    g = ImageC(pos + 1)
                    b = ImageC(pos)
                    Select Case way
                        Case "RGB"
                            SetRGBData(pos, r, g, b)
                        Case "RBG"
                            SetRGBData(pos, r, b, g)
                        Case "BRG"
                            SetRGBData(pos, b, r, g)
                        Case "BGR"
                            SetRGBData(pos, b, g, r)
                        Case "GBR"
                            SetRGBData(pos, g, b, r)
                        Case "GRB"
                            SetRGBData(pos, g, r, b)
                    End Select
                Next
            Next
            putBitMapData()
            Return True
        End If
    End Function

    'Public Function GetGray(ByVal i As Integer, ByVal j As Integer) As Byte
    '    Dim ret As Byte
    '    If mImageType = 0 Then
    '        If j < 0 Or j >= mWidth Or i < 0 Or i >= mHeight Then
    '            ret = 0
    '        Else
    '            ret = ImageB(i * mFwidth + j)
    '        End If
    '    Else
    '        ret = 0
    '    End If
    '    Return ret
    'End Function

    Public Function AlgebrOper(ByVal imgB As ImageClass, ByVal op As Integer) As Boolean
        If imgB.GetImageType <> mImageType Then
            Return False
        End If
        If mImageType = 1 Then
            Return False
        End If
        Dim jStart, jEnd, iStart, iEnd As Integer
        Dim bjStart, biStart As Integer
        If mWidth > imgB.Width() Then
            jStart = (mWidth - imgB.Width()) / 2
            jEnd = jStart + imgB.Width() - 1
            bjStart = 0
            If mHeight > imgB.Height() Then
                iStart = (mHeight - imgB.Height()) / 2
                iEnd = iStart + imgB.Height() - 1
                biStart = 0
            Else
                iStart = 0
                iEnd = mHeight - 1
                biStart = (imgB.Height() - mHeight) / 2
            End If
        Else
            jStart = 0
            jEnd = mWidth - 1
            bjStart = (imgB.Width() - mWidth) / 2
            If mHeight > imgB.Height() Then
                iStart = (mHeight - imgB.Height()) / 2
                iEnd = iStart + mHeight - 1
                biStart = 0
            Else
                iStart = 0
                iEnd = mHeight - 1
                biStart = (imgB.Width() - mWidth) / 2
            End If
        End If

        Dim tempDoubleArray(mSize - 1) As Single
        Array.Copy(ImageB, tempDoubleArray, mSize - 1)

        Dim pos As Long
        Select Case op
            Case 0
                For i = iStart To iEnd
                    For j = jStart To jEnd
                        pos = i * mFwidth + j
                        'ImageB(pos) = NormValue(CDbl(ImageB(pos)) + imgB.getGrey(biStart + i, bjStart + j))
                        tempDoubleArray(pos) = CDbl(ImageB(pos)) + imgB.getGrey(biStart + i, bjStart + j)
                    Next
                Next
            Case 1
                For i = iStart To iEnd
                    For j = jStart To jEnd
                        pos = i * mFwidth + j
                        'ImageB(pos) = NormValue(CDbl(ImageB(pos)) - imgB.getGrey(biStart + i, bjStart + j))
                        tempDoubleArray(pos) = CDbl(ImageB(pos)) - imgB.getGrey(biStart + i, bjStart + j)
                    Next
                Next
            Case 2
                For i = iStart To iEnd
                    For j = jStart To jEnd
                        pos = i * mFwidth + j
                        'ImageB(pos) = NormValue(CDbl(ImageB(pos)) * imgB.getGrey(biStart + i, bjStart + j))
                        tempDoubleArray(pos) = CDbl(ImageB(pos)) * imgB.getGrey(biStart + i, bjStart + j)
                    Next
                Next
            Case 3
                For i = iStart To iEnd
                    For j = jStart To jEnd
                        If imgB.getGrey(biStart + i, bjStart + j) <> 0 Then
                            pos = i * mFwidth + j
                            'ImageB(pos) = NormValue(CDbl(ImageB(pos)) / imgB.getGrey(biStart + i, bjStart + j))
                            tempDoubleArray(pos) = CDbl(ImageB(pos)) / imgB.getGrey(biStart + i, bjStart + j)
                        Else
                            'ImageB(pos) = 255
                            tempDoubleArray(pos) = 9999
                        End If
                    Next
                Next
        End Select

        NormArray2ImageB(tempDoubleArray)
        putBitMapData()
    End Function
    Public Function NormValue(ByVal num As Single) As Byte
        If num > 255 Then
            Return CByte(255)
        ElseIf num < 0 Then
            Return CByte(0)
        Else
            Return CByte(num)
        End If
    End Function
    Public Function CutoffArray2ImageB(ByVal arr() As Single) As Boolean '将传入的数组元素小于0=0，大于255=255，并赋给ImageB
        Dim pos As Long
        For i = 0 To mHeight - 1
            For j = 0 To mWidth - 1
                pos = i * mFwidth + j
                ImageB(pos) = NormValue(arr(pos))
            Next
        Next
    End Function
    Public Function CutoffArray2ImageC(ByVal arr() As Single) As Boolean '将传入的数组元素小于0=0，大于255=255，并赋给ImageC
        Dim pos As Long
        For i = 0 To mHeight - 1
            For j = 0 To mWidth - 1
                pos = Cpos(i) + j * 3
                ImageC(pos) = NormValue(arr(pos))
                ImageC(pos + 1) = NormValue(arr(pos + 1))
                ImageC(pos + 2) = NormValue(arr(pos + 2))
            Next
        Next
    End Function

    Public Function NormArray2ImageB(ByRef arr() As Single) As Boolean '将传入的数组规范到0-255，并赋给ImageB
        Dim gmax, gmin As Double
        Dim pos As Long
        gmax = arr(0)
        gmin = arr(0)
        For i = 0 To mHeight - 1
            For j = 0 To mWidth - 1
                pos = i * mFwidth + j
                If arr(pos) > gmax Then
                    gmax = arr(pos)
                End If
                If arr(pos) < gmin Then
                    gmin = arr(pos)
                End If
            Next
        Next

        For i = 0 To mHeight - 1
            For j = 0 To mWidth - 1
                pos = i * mFwidth + j
                ImageB(pos) = CByte((arr(pos) - gmin) / (gmax - gmin) * 255)
            Next
        Next
    End Function
    Public Function NormArray2ImageC(ByRef arr() As Single) As Boolean '将传入的数组规范到0-255，并赋给ImageC
        Dim rgmax, rgmin As Double
        Dim ggmax, ggmin As Double
        Dim bgmax, bgmin As Double
        Dim pos As Long
        rgmax = arr(0)
        rgmin = arr(0)
        For i = 0 To mHeight - 1
            For j = 0 To mWidth - 1
                pos = Cpos(i) + j * 3
                If arr(pos + 2) > rgmax Then
                    rgmax = arr(pos + 2)
                End If
                If arr(pos + 2) < rgmin Then
                    rgmin = arr(pos + 2)
                End If
                If arr(pos + 1) > ggmax Then
                    ggmax = arr(pos + 1)
                End If
                If arr(pos + 1) < ggmin Then
                    ggmin = arr(pos + 1)
                End If
                If arr(pos) > bgmax Then
                    bgmax = arr(pos)
                End If
                If arr(pos) < bgmin Then
                    bgmin = arr(pos)
                End If
            Next
        Next

        For i = 0 To mHeight - 1
            For j = 0 To mWidth - 1
                pos = Cpos(i) + j * 3
                ImageC(pos + 2) = CByte((arr(pos + 2) - rgmin) / (rgmax - rgmin) * 255)
                ImageC(pos + 1) = CByte((arr(pos + 1) - ggmin) / (ggmax - ggmin) * 255)
                ImageC(pos) = CByte((arr(pos) - bgmin) / (bgmax - bgmin) * 255)
            Next
        Next
    End Function
    Public Function CreateImage(ByVal cols As Integer, ByVal rows As Integer, ByVal op As Integer) As Boolean
        '该部分代码从示例程序反编译修改得到，关于图像生成的相关函数资料没有找到，暂时采取此种方式
        mImg = New Bitmap(cols, rows, Drawing.Imaging.PixelFormat.Format8bppIndexed)
        getBitMapData()

        Select Case op
            Case 0 '条形图
                Dim num As Integer = CInt(Math.Round(CDbl(mHeight) / 10.0))
                For i As Integer = 0 To mHeight - 1
                    Dim g As Single = CSng((Math.Sin(CDbl((CSng(i) / CSng(num))) * 6.28) + 1.2) / 2.4 * 255.0)
                    For j As Integer = 0 To mHeight - 1
                        Dim pos As Long = i * mFwidth + j
                        ImageB(pos) = CByte(g)
                    Next
                Next
            Case 1
                Dim num6 As Double = Math.Pow(CDbl(mHeight) / 2.0, 2.0) + Math.Pow(CDbl(mWidth) / 2.0, 2.0) / 10.0
                Dim num As Integer = CInt(Math.Round(CDbl(mHeight) / 2.0))
                Dim num7 As Integer = CInt(Math.Round(CDbl(mWidth) / 2.0))
                For i As Integer = 0 To mHeight - 1
                    For j As Integer = 0 To mWidth - 1
                        Dim g As Single
                        Dim num3 As Single = CSng((Math.Pow(CDbl((i - num)), 2.0) + Math.Pow(CDbl((j - num7)), 2.0)))
                        g = CSng((Math.Exp(CDbl((-CDbl(num3))) / num6) * 255.0))
                        Dim pos As Long = i * mFwidth + j
                        ImageB(pos) = CByte(g)
                    Next
                Next
            Case 2
                For i As Integer = 0 To mHeight - 1
                    Dim num As Integer = CInt(Math.Round(CDbl(i) / 20.0))
                    For j As Integer = 0 To mWidth - 1
                        Dim num7 As Integer = CInt(Math.Round(CDbl(j) / 20.0))
                        Dim pos As Long = CLng(i * mFwidth + j)

                        If (num7 And 1 And (num And 1)) <> 0 Then
                            ImageB(pos) = 0
                        Else
                            ImageB(pos) = 255
                        End If
                    Next
                Next
            Case 3
                Dim num15 As Single = CSng(CDbl(mWidth) / 5.0)
                For i As Integer = 0 To mHeight - 1
                    Dim num17 As Integer = mWidth - 1
                    For j As Integer = 0 To num17
                        Dim num3 As Single
                        Dim pos As Long
                        num3 = CSng(((Math.Sin(CDbl((CSng(i) / num15)) * 6.28) * Math.Sin(CDbl((CSng(j) / num15)) * 6.28) + 1.0) / 2.0 * 255.0))
                        pos = CLng((i * mFwidth + j))
                        ImageB(pos) = NormValue(num3)
                    Next
                Next
            Case 4
                Dim num18 As Integer = 0
                Dim num19 As Integer = 0
                Dim num20 As Integer = 240
                Dim num21 As Integer = 240
                Dim num22 As Integer = 0
                While num20 - num18 > 0 Or num21 - num19 > 0
                    Dim num23 As Integer = num19
                    Dim num24 As Integer = num21
                    For j As Integer = num23 To num24 Step 16
                        Dim num25 As Long
                        Dim num26 As Long

                        num25 = CLng(num18)
                        num26 = CLng((num18 + 15))

                        For num27 As Long = num25 To num26
                            Dim num28 As Long
                            Dim num29 As Long

                            num28 = CLng(j)
                            num29 = CLng((j + 15))

                            For num30 As Long = num28 To num29
                                Dim num5 As Long = num27 * CLng(mFwidth) + num30
                                ImageB(CInt(num5)) = CByte(num22)
                            Next
                        Next
                        num22 += 1
                    Next
                    num18 += 16
                    Dim num31 As Integer = num18
                    Dim num32 As Integer = num20
                    For i As Integer = num31 To num32 Step 16
                        Dim num33 As Long
                        Dim num34 As Long

                        num33 = CLng(i)
                        num34 = CLng((i + 15))

                        For num27 As Long = num33 To num34
                            Dim num35 As Long
                            Dim num36 As Long

                            num35 = CLng(num21)
                            num36 = CLng((num21 + 15))

                            For num30 As Long = num35 To num36
                                Dim num5 As Long = num27 * CLng(mFwidth) + num30
                                ImageB(CInt(num5)) = CByte(num22)
                            Next
                        Next
                        num22 += 1
                    Next
                    num21 -= 16
                    Dim num37 As Integer = num21
                    Dim num38 As Integer = num19
                    For j As Integer = num37 To num38 Step -16
                        Dim num39 As Long
                        Dim num40 As Long

                        num39 = CLng(num20)
                        num40 = CLng((num20 + 15))

                        For num27 As Long = num39 To num40
                            Dim num41 As Long
                            Dim num42 As Long

                            num41 = CLng(j)
                            num42 = CLng((j + 15))

                            For num30 As Long = num41 To num42
                                Dim num5 As Long = num27 * CLng(mFwidth) + num30
                                ImageB(CInt(num5)) = CByte(num22)
                            Next
                        Next
                        num22 += 1
                    Next
                    num20 -= 16
                    Dim num43 As Integer = num20
                    Dim num44 As Integer = num18
                    For i As Integer = num43 To num44 Step -16
                        Dim num45 As Long
                        Dim num46 As Long

                        num45 = CLng(i)
                        num46 = CLng((i + 15))

                        For num27 As Long = num45 To num46
                            Dim num47 As Long
                            Dim num48 As Long

                            num47 = CLng(num19)
                            num48 = CLng((num19 + 15))

                            For num30 As Long = num47 To num48
                                Dim num5 As Long = num27 * CLng(mFwidth) + num30
                                ImageB(CInt(num5)) = CByte(num22)
                            Next
                        Next
                        num22 += 1
                    Next
                    num19 += 16
                End While
        End Select

        SetGrayPalette()
        putBitMapData()
    End Function

    Public Function SetGrayPalette() As Boolean
        If mImageType = 0 Then
            Dim palette As Imaging.ColorPalette = mImg.Palette
            For i = 0 To palette.Entries.Length - 1
                palette.Entries(i) = Color.FromArgb(255, i, i, i)
            Next
            mImg.Palette = palette
            Return True
        End If
    End Function

    Public Function AddSaltNoise() As Boolean '添加椒盐噪声
        Dim i, j As Integer
        Dim pos As Long
        If mImageType <> 0 Then Return False
        For k = 0 To mPixels * 0.01
            i = Rnd() * (mHeight - 1)
            j = Rnd() * (mWidth - 1)
            pos = i * mFwidth + j
            ImageB(pos) = IIf(Rnd() < 0.5, 255, 0)
        Next
        putBitMapData()
        Return True
    End Function

    Public Function GetNxNPixels(ByVal i As Integer, ByVal j As Integer, ByVal n As Integer) As Byte()
        '返回灰度图像NxN像素信息，边界使用0填充,(i,j)为中心的NxN像素,N=1,3,5,7,9...
        Dim ret(n * n - 1) As Byte
        Dim fi, fj As Integer
        For ii = 0 To n - 1
            For jj = 0 To n - 1
                fi = i - n \ 2 + ii '取图像的第i行像素起始处
                fj = j - n \ 2 + jj '取图像的第j列像素起始处
                If fi < 0 Or fj < 0 Or fi > mHeight - 1 Or fj > mWidth - 1 Then '超出边界用0填充
                    ret(ii * n + jj) = 0
                Else
                    ret(ii * n + jj) = getGrey(fi, fj)
                End If
            Next
        Next
        Return ret
    End Function

    Public Function GetNxNPixels(ByVal i As Integer, ByVal j As Integer, ByVal channel As Char, ByVal n As Integer) As Byte()
        '返回RGB图像NxN像素信息，边界使用边缘值填充,(i,j)为中心的NxN像素,N=1,3,5,7,9...
        Dim ret(n * n - 1) As Byte
        Dim fi, fj As Integer
        For ii = 0 To n - 1
            For jj = 0 To n - 1
                fi = i - n \ 2 + ii '取图像的第i行像素起始处
                fj = j - n \ 2 + jj '取图像的第j列像素起始处
                If fi < 0 Or fj < 0 Or fi > mHeight - 1 Or fj > mWidth - 1 Then '超出边界用0填充
                    ret(ii * n + jj) = 0
                Else
                    ret(ii * n + jj) = GetRGB(fi, fj, channel)
                End If
            Next
        Next
        Return ret
    End Function

    Public Function ArrayMultiplySum(ByVal array_int As Integer(), ByVal array_byte As Byte()) As Integer
        Dim ret As Single = 0
        If array_int.Length <> array_byte.Length Then Return 0
        For i = 0 To array_int.Length - 1
            ret += array_int(i) * array_byte(i)
        Next
        Return ret
    End Function

    Private Function SetGray(ByVal i As Integer, ByVal j As Integer, ByVal value As Byte) As Boolean
        If mImageType <> 0 Then Return False
        Dim pos As Long
        pos = i * mFwidth + j
        ImageB(pos) = value
    End Function

    Public Function MedianFilter() As Boolean '中值过滤
        Dim tempImg As New ImageClass
        Dim g As Byte
        Clone(tempImg)
        If mImageType = 0 Then
            Dim mV(8) As Byte
            For i = 0 To mHeight - 1
                For j = 0 To mWidth - 1
                    '获取二维中值滤波器模板
                    mV(0) = tempImg.getGrey(i - 2, j)
                    mV(1) = tempImg.getGrey(i - 1, j)
                    For ii = 2 To 6
                        mV(ii) = tempImg.getGrey(i, j + ii - 4)
                    Next
                    mV(7) = tempImg.getGrey(i + 1, j)
                    mV(8) = tempImg.getGrey(i + 2, j)
                    '排序模板中的像素
                    For u = 0 To 4
                        For v = u + 1 To 8
                            If mV(u) > mV(v) Then
                                g = mV(u)
                                mV(u) = mV(v)
                                mV(v) = g
                            End If
                        Next
                    Next
                    SetGray(i, j, mV(4))
                Next
            Next
            putBitMapData()
            Return True
        End If
    End Function

    Public Function MeanFilter() As Boolean '均值过滤
        Dim tempImg As New ImageClass
        Dim sum As Integer
        Clone(tempImg)
        If mImageType = 0 Then
            Dim mV() As Byte
            For i = 0 To mHeight - 1
                For j = 0 To mWidth - 1
                    mV = tempImg.GetNxNPixels(i, j, 3) '获取二维均值滤波器模板
                    sum = 0
                    For u = 0 To mV.Length - 1
                        sum += mV(u)
                    Next
                    Dim g As Byte = CByte(sum / mV.Length)
                    SetGray(i, j, CByte(sum / mV.Length))
                Next
            Next
            putBitMapData()
            Return True
        End If
    End Function

    Public Function ConvFilter(ByVal filter() As Integer, ByVal deno As Integer, ByVal bind_flag As Boolean) As Boolean
        Dim g As Integer

        If mImageType = 0 Then
            Dim newdata(mSize - 1) As Single
            Dim pos As Long
            For i = 0 To mHeight - 1
                For j = 0 To mWidth - 1
                    Dim mv() As Byte = GetNxNPixels(i, j, Math.Sqrt(filter.Length))
                    g = ArrayMultiplySum(filter, mv)
                    pos = i * mFwidth + j
                    newdata(pos) = g / deno
                Next
            Next
            If bind_flag Then
                NormArray2ImageB(newdata)
            Else
                CutoffArray2ImageB(newdata)
            End If
            putBitMapData()
        ElseIf mImageType = 1 Then
            Dim new_rgb_data(CSize - 1) As Single
            Dim pos As Long
            Dim gr, gg, gb As Single
            For i = 0 To mHeight - 1
                For j = 0 To mWidth - 1
                    pos = Cpos(i) + j * 3
                    Dim mr() As Byte = GetNxNPixels(i, j, "r", Math.Sqrt(filter.Length))
                    Dim mg() As Byte = GetNxNPixels(i, j, "g", Math.Sqrt(filter.Length))
                    Dim mb() As Byte = GetNxNPixels(i, j, "b", Math.Sqrt(filter.Length))
                    gr = ArrayMultiplySum(filter, mr)
                    gg = ArrayMultiplySum(filter, mg)
                    gb = ArrayMultiplySum(filter, mb)
                    new_rgb_data(pos) = gb
                    new_rgb_data(pos + 1) = gg
                    new_rgb_data(pos + 2) = gr
                Next
            Next
            If bind_flag Then
                NormArray2ImageC(new_rgb_data)
            Else
                CutoffArray2ImageC(new_rgb_data)
            End If
            putBitMapData()
        End If

    End Function

    Public Function GradientFilter(ByVal way As Integer, ByVal bind_flag As Boolean) As Boolean
        Dim pos As Long

        If mImageType = 0 Then
            Dim new_data(mSize - 1) As Single
            For i = 0 To mHeight - 1
                For j = 0 To mWidth - 1
                    pos = i * mFwidth + j
                    Select Case way
                        Case 1 'G(i,j)=|f(i,j+1)-f(i,j)|+|f(i+1,j)-f(i,j)|
                            new_data(pos) = Math.Abs(CSng(getGrey(i, j + 1)) - getGrey(i, j)) + Math.Abs(CSng(getGrey(i + 1, j)) - getGrey(i, j))
                        Case 2 'G(i,j)=max(|f(i,j+1)-f(i,j)|,|f(i+1,j)-f(i,j)|)
                            new_data(pos) = Math.Max(Math.Abs(CSng(getGrey(i, j + 1)) - getGrey(i, j)), Math.Abs(CSng(getGrey(i + 1, j)) - getGrey(i, j)))
                        Case 3 'G(i,j)=sqrt(|f(i,j+1)-f(i,j)|^2+|f(i+1,j)-f(i,j)|^2)
                            new_data(pos) = Math.Sqrt((CSng(getGrey(i, j + 1)) - getGrey(i, j)) ^ 2 + (CSng(getGrey(i + 1, j)) - getGrey(i, j)) ^ 2)
                        Case 4 'G(i,j)=|f(i+1,j+1)-f(i,j)|+|f(i+1,j)-f(i,j+1)|
                            new_data(pos) = Math.Abs(CSng(getGrey(i + 1, j + 1)) - getGrey(i, j)) + Math.Abs(CSng(getGrey(i + 1, j)) - getGrey(i, j + 1))
                        Case 5 'G(i,j)=max(|f(i+1,j+1)-f(i,j)|+|f(i+1,j)-f(i,j+1)|)
                            new_data(pos) = Math.Max(Math.Abs(CSng(getGrey(i + 1, j + 1)) - getGrey(i, j)), Math.Abs(CSng(getGrey(i + 1, j)) - getGrey(i, j + 1)))
                        Case 6 'G(i,j)=sqrt(|f(i+1,j+1)-f(i,j)|^2+|f(i+1,j)-f(i,j+1)|^2)
                            new_data(pos) = Math.Sqrt((CSng(getGrey(i + 1, j + 1)) - getGrey(i, j)) ^ 2 + (CSng(getGrey(i + 1, j)) - getGrey(i, j + 1)) ^ 2)
                    End Select
                Next
            Next
            If bind_flag Then
                NormArray2ImageB(new_data)
            Else
                CutoffArray2ImageB(new_data)
            End If
            putBitMapData()
        ElseIf mImageType = 1 Then
            Dim new_rgb(CSize - 1) As Single
            For i = 0 To mHeight - 1
                For j = 0 To mWidth - 1
                    pos = Cpos(i) + j * 3
                    Select Case way
                        Case 1 'G(i,j)=|f(i,j+1)-f(i,j)|+|f(i+1,j)-f(i,j)|
                            new_rgb(pos) = Math.Abs(CSng(GetRGB(i, j + 1, "b")) - GetRGB(i, j, "b")) + Math.Abs(CSng(GetRGB(i + 1, j, "b")) - GetRGB(i, j, "b"))
                            new_rgb(pos + 1) = Math.Abs(CSng(GetRGB(i, j + 1, "g")) - GetRGB(i, j, "g")) + Math.Abs(CSng(GetRGB(i + 1, j, "g")) - GetRGB(i, j, "g"))
                            new_rgb(pos + 2) = Math.Abs(CSng(GetRGB(i, j + 1, "r")) - GetRGB(i, j, "r")) + Math.Abs(CSng(GetRGB(i + 1, j, "r")) - GetRGB(i, j, "r"))

                        Case 2 'G(i,j)=max(|f(i,j+1)-f(i,j)|,|f(i+1,j)-f(i,j)|)
                            new_rgb(pos) = Math.Max(Math.Abs(CSng(GetRGB(i, j + 1, "b")) - GetRGB(i, j, "b")), Math.Abs(CSng(GetRGB(i + 1, j, "b")) - GetRGB(i, j, "b")))
                            new_rgb(pos + 1) = Math.Max(Math.Abs(CSng(GetRGB(i, j + 1, "g")) - GetRGB(i, j, "g")), Math.Abs(CSng(GetRGB(i + 1, j, "g")) - GetRGB(i, j, "g")))
                            new_rgb(pos + 2) = Math.Max(Math.Abs(CSng(GetRGB(i, j + 1, "r")) - GetRGB(i, j, "r")), Math.Abs(CSng(GetRGB(i + 1, j, "r")) - GetRGB(i, j, "r")))

                        Case 3 'G(i,j)=sqrt(|f(i,j+1)-f(i,j)|^2+|f(i+1,j)-f(i,j)|^2)
                            new_rgb(pos) = Math.Sqrt((CSng(GetRGB(i, j + 1, "b")) - GetRGB(i, j, "b")) ^ 2 + (CSng(GetRGB(i + 1, j, "b")) - GetRGB(i, j, "b")) ^ 2)
                            new_rgb(pos + 1) = Math.Sqrt((CSng(GetRGB(i, j + 1, "g")) - GetRGB(i, j, "g")) ^ 2 + (CSng(GetRGB(i + 1, j, "g")) - GetRGB(i, j, "g")) ^ 2)
                            new_rgb(pos + 2) = Math.Sqrt((CSng(GetRGB(i, j + 1, "r")) - GetRGB(i, j, "r")) ^ 2 + (CSng(GetRGB(i + 1, j, "r")) - GetRGB(i, j, "r")) ^ 2)

                        Case 4 'G(i,j)=|f(i+1,j+1)-f(i,j)|+|f(i+1,j)-f(i,j+1)|
                            new_rgb(pos) = Math.Abs(CSng(GetRGB(i + 1, j + 1, "b")) - GetRGB(i, j, "b")) + Math.Abs(CSng(GetRGB(i + 1, j, "b")) - GetRGB(i, j + 1, "b"))
                            new_rgb(pos + 1) = Math.Abs(CSng(GetRGB(i + 1, j + 1, "g")) - GetRGB(i, j, "g")) + Math.Abs(CSng(GetRGB(i + 1, j, "g")) - GetRGB(i, j + 1, "g"))
                            new_rgb(pos + 2) = Math.Abs(CSng(GetRGB(i + 1, j + 1, "r")) - GetRGB(i, j, "r")) + Math.Abs(CSng(GetRGB(i + 1, j, "r")) - GetRGB(i, j + 1, "r"))

                        Case 5 'G(i,j)=max(|f(i+1,j+1)-f(i,j)|+|f(i+1,j)-f(i,j+1)|)
                            new_rgb(pos) = Math.Max(Math.Abs(CSng(GetRGB(i + 1, j + 1, "b")) - GetRGB(i, j, "b")), Math.Abs(CSng(GetRGB(i + 1, j, "b")) - GetRGB(i, j + 1, "b")))
                            new_rgb(pos + 1) = Math.Max(Math.Abs(CSng(GetRGB(i + 1, j + 1, "g")) - GetRGB(i, j, "g")), Math.Abs(CSng(GetRGB(i + 1, j, "g")) - GetRGB(i, j + 1, "g")))
                            new_rgb(pos + 2) = Math.Max(Math.Abs(CSng(GetRGB(i + 1, j + 1, "r")) - GetRGB(i, j, "r")), Math.Abs(CSng(GetRGB(i + 1, j, "r")) - GetRGB(i, j + 1, "r")))

                        Case 6 'G(i,j)=sqrt(|f(i+1,j+1)-f(i,j)|^2+|f(i+1,j)-f(i,j+1)|^2)
                            new_rgb(pos) = Math.Sqrt((CSng(GetRGB(i + 1, j + 1, "b")) - GetRGB(i, j, "b")) ^ 2 + (CSng(GetRGB(i + 1, j, "b")) - GetRGB(i, j + 1, "b")) ^ 2)
                            new_rgb(pos + 1) = Math.Sqrt((CSng(GetRGB(i + 1, j + 1, "g")) - GetRGB(i, j, "g")) ^ 2 + (CSng(GetRGB(i + 1, j, "g")) - GetRGB(i, j + 1, "g")) ^ 2)
                            new_rgb(pos + 2) = Math.Sqrt((CSng(GetRGB(i + 1, j + 1, "r")) - GetRGB(i, j, "r")) ^ 2 + (CSng(GetRGB(i + 1, j, "r")) - GetRGB(i, j + 1, "r")) ^ 2)

                    End Select
                Next
            Next
            If bind_flag Then
                NormArray2ImageC(new_rgb)
            Else
                CutoffArray2ImageC(new_rgb)
            End If
            putBitMapData()
        End If

    End Function

    Public Function ConvertToGrayImageWithChannel(ByVal channel As Integer) As Boolean 'channel 0 B, 1 G, 2 R
        If mImageType = 0 Then Return False
        mImg = New Bitmap(mWidth, mHeight, Drawing.Imaging.PixelFormat.Format8bppIndexed)
        getBitMapData()
        'mFwidth = (mWidth + 3) \ 4 * 4
        'mImageType = 0
        Dim posB, posC As Long
        For i = 0 To mHeight - 1
            For j = 0 To mWidth - 1
                posB = i * mFwidth + j
                posC = Cpos(i) + j * 3
                ImageB(posB) = ImageC(posC + channel)
            Next
        Next
        putBitMapData()
        Return True
    End Function

    Public Function SetBluePalette() As Boolean
        If mImageType = 1 Then Return False
        Dim m_palette As Imaging.ColorPalette
        m_palette = GetPalette()
        For i = 0 To m_palette.Entries.Length - 1
            m_palette.Entries(i) = Color.FromArgb(255, 0, 0, i)
        Next
        SetPalette(m_palette)
        putBitMapData()
    End Function
    Public Function SetGreenPalette() As Boolean
        If mImageType = 1 Then Return False
        Dim m_palette As Imaging.ColorPalette
        m_palette = GetPalette()
        For i = 0 To m_palette.Entries.Length - 1
            m_palette.Entries(i) = Color.FromArgb(255, 0, i, 0)
        Next
        SetPalette(m_palette)
        putBitMapData()
    End Function
    Public Function SetRedPalette() As Boolean
        If mImageType = 1 Then Return False
        Dim m_palette As Imaging.ColorPalette
        m_palette = GetPalette()
        For i = 0 To m_palette.Entries.Length - 1
            m_palette.Entries(i) = Color.FromArgb(255, i, 0, 0)
        Next
        SetPalette(m_palette)
        putBitMapData()
    End Function


End Class
