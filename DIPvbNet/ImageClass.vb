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
        If (j > mHeight - 1) Or (i > mWidth - 1) Then Return 0
        pos = j * mFwidth + i
        Return ImageB(pos)
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
            '触发数据改变事件
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

End Class
