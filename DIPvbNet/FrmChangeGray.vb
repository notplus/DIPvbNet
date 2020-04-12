Public Class FrmChangeGray
    Private oImage As ImageClass
    Private tImage As ImageClass
    Private tImageType As Integer
    ' 灰度变换
    Private transformG(255) As Byte
    Private transformGR(255) As Byte
    Private transformGG(255) As Byte
    Private transformGB(255) As Byte

    Private Sub FrmChangeGray_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RadioButtonNow.Checked = True
    End Sub
    Public Function SetImageClass(ByVal mImage As ImageClass) As Boolean
        oImage = mImage
        mImage.Clone(tImage)
        tImageType = tImage.GetImageType
    End Function
    Private Sub OriginHistPanel_Paint(sender As Object, e As PaintEventArgs) Handles OriginHistPanel.Paint
        oImage.DrawHist(e.Graphics, OriginHistPanel.Width, OriginHistPanel.Height, 0)
    End Sub

    Private Sub ChangedHistPanel_Paint(sender As Object, e As PaintEventArgs) Handles ChangedHistPanel.Paint
        If RadioButtonNow.Checked = True Then
            oImage.DrawHist(e.Graphics, ChangedHistPanel.Width, ChangedHistPanel.Height, 0)
        Else
            tImage.DrawHist(e.Graphics, ChangedHistPanel.Width, ChangedHistPanel.Height, 0)
        End If
    End Sub

    Private Sub RadioButtonNow_Click(sender As Object, e As EventArgs) Handles RadioButtonNow.Click
        GroupBoxCurve.Enabled = False
        GroupBoxLinear.Enabled = False
        For i = 0 To 255
            transformG(i) = i
            transformGR(i) = i
            transformGG(i) = i
            transformGB(i) = i
        Next
        'oImage.Clone(tImage)
        OriginHistPanel.Refresh()
        ChangedHistPanel.Refresh()
        FunctionCurve.Refresh()
    End Sub

    Private Sub RadioButtonLinear_Click(sender As Object, e As EventArgs) Handles RadioButtonLinear.Click
        GroupBoxCurve.Enabled = False
        GroupBoxLinear.Enabled = True
    End Sub

    Private Sub RadioButtonCurve_Click(sender As Object, e As EventArgs) Handles RadioButtonCurve.Click
        GroupBoxCurve.Enabled = True
        GroupBoxLinear.Enabled = False

    End Sub

    Private Sub RadioButtonEqualize_Click(sender As Object, e As EventArgs) Handles RadioButtonEqualize.Click
        GroupBoxCurve.Enabled = False
        GroupBoxLinear.Enabled = False
        oImage.Clone(tImage)
        'tImage = oImage
        If tImageType = 0 Then
            tImage.histEqualize(transformG)
            tImage.GrayTransform(transformG)
        ElseIf tImageType = 1 Then
            tImage.histEqualize(transformG, transformGR, transformGG, transformGB)
            tImage.GrayTransform(transformGR, transformGG, transformGB)
        End If

        tImage.Calculate_Histogram()
        ChangedHistPanel.Refresh()
        FunctionCurve.Refresh()
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        RadioButtonNow.Checked = True
        For i = 0 To 255
            transformG(i) = i
            transformGR(i) = i
            transformGG(i) = i
            transformGB(i) = i
        Next
        'oImage = tImage
        tImage.Clone(oImage)
        oImage.Calculate_Histogram()
        MainForm.ChangeImageClass(tImage)
        MainForm.Refresh()
        FunctionCurve.Refresh()
        ChangedHistPanel.Refresh()
        OriginHistPanel.Refresh()

    End Sub

    Private Sub FunctionCurve_Paint(sender As Object, e As PaintEventArgs) Handles FunctionCurve.Paint
        Dim sx As Single, sy As Single
        Dim x1 As Single, y1 As Single, x2 As Single, y2 As Single
        Dim x0 As Single, y0 As Single
        Dim ft As Font = New Font("Arial Narrow", 8, FontStyle.Regular, GraphicsUnit.Point)
        Dim B As Brush = New SolidBrush(Color.Black)
        'Dim r As RectangleF
        Dim p As New Pen(Color.Red)  '指定课绘制坐标轴线的颜色
        Dim g As Graphics
        g = e.Graphics
        If tImageType = 0 Then
            ' 256，h * 0.9   => 270, h
            ' 假定横向的最大为270
            ' 直方图有效区域为（0,5%*h）-（255,10%*h）
            sx = FunctionCurve.Width / 270.0 ' 设定宽度为270
            sy = FunctionCurve.Height / (255 * 1.2) ' 高度放大20%
            g.ScaleTransform(sx, -sy) ' 设置比例系数，纵向为负表示把竖轴的原点移到下面
            x0 = 5.0
            y0 = -255 * 1.1   ' 图形原点（0,0）上移10%
            g.TranslateTransform(x0, y0) ' 设置坐标偏移

            ' 首先绘制坐标轴，纵横线
            x1 = -3  ' 横轴线起点于-3
            y1 = 0   ' 横轴线
            x2 = 260 '横轴线终止于260 
            y2 = y1 ' 横轴线为水平线
            g.DrawLine(p, x1, y1, x2, y2)  ' 绘制（x1,y1）-（x2,y2）
            x1 = 0 ' 竖轴线
            y1 = 255 * 1.05   ' 竖轴线起始于下部5%至顶部5%（注意横轴线在离底部5%处）
            x2 = x1
            y2 = -255 * 0.025
            g.DrawLine(p, x1, y1, x2, y2)

            B.Dispose()
            p.Dispose()
            Dim p1 As New Pen(Color.Black)

            x1 = 0
            y1 = transformG(0)
            For i = 0 To 254
                x2 = i + 1
                y2 = transformG(i + 1)
                g.DrawLine(p1, x1, y1, x2, y2)
                x1 = x2
                y1 = y2
            Next
            p1.Dispose()
        ElseIf tImageType = 1 Then
            p = New Pen(Color.Magenta)
            sx = FunctionCurve.Width / 270.0 ' 设定宽度为270
            sy = FunctionCurve.Width / (255 * 1.2) ' 高度放大20%
            g.ScaleTransform(sx, -sy) ' 设置比例系数，纵向为负表示把竖轴的原点移到下面
            x0 = 5.0
            y0 = -255 * 1.1   ' 图形原点（0,0）上移10%
            g.TranslateTransform(x0, y0) ' 设置坐标偏移
            ' 首先绘制坐标轴，纵横线
            x1 = -3  ' 横轴线起点于-3
            y1 = 0   ' 横轴线
            x2 = 260 '横轴线终止于260 
            y2 = y1 ' 横轴线为水平线
            g.DrawLine(p, x1, y1, x2, y2)  ' 绘制（x1,y1）-（x2,y2）
            x1 = 0 ' 竖轴线
            y1 = 255 * 1.05   ' 竖轴线起始于下部5%至顶部5%（注意横轴线在离底部5%处）
            x2 = x1
            y2 = -255 * 0.025
            g.DrawLine(p, x1, y1, x2, y2)

            p.Dispose()
            p = New Pen(Color.Black)
            x1 = 0
            y1 = transformG(0)
            For i = 0 To 254
                x2 = i + 1
                y2 = transformG(i + 1)
                g.DrawLine(p, x1, y1, x2, y2)
                x1 = x2
                y1 = y2
            Next
            p.Dispose()
            p = New Pen(Color.Red)
            x1 = 0
            y1 = transformGR(0)
            For i = 0 To 254
                x2 = i + 1
                y2 = transformGR(i + 1)
                g.DrawLine(p, x1, y1, x2, y2)
                x1 = x2
                y1 = y2
            Next
            p.Dispose()
            p = New Pen(Color.Green)
            x1 = 0
            y1 = transformGG(0)
            For i = 0 To 254
                x2 = i + 1
                y2 = transformGG(i + 1)
                g.DrawLine(p, x1, y1, x2, y2)
                x1 = x2
                y1 = y2
            Next
            p.Dispose()
            p = New Pen(Color.Blue)
            x1 = 0
            y1 = transformGB(0)
            For i = 0 To 254
                x2 = i + 1
                y2 = transformGB(i + 1)
                g.DrawLine(p, x1, y1, x2, y2)
                x1 = x2
                y1 = y2
            Next
            p.Dispose()
        End If
    End Sub

    Private Sub FunctionCurve_MouseMove(sender As Object, e As MouseEventArgs) Handles FunctionCurve.MouseMove
        LabelCursorPosition.Text = Int(e.X / 1.11 - 5) & "," & Int(-e.Y / 0.98 + 280.5) '为简化代码，使用固定变换
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim a, b, c As Double
        oImage.Clone(tImage)
        a = Val(TextBox7.Text)
        b = Val(TextBox8.Text)
        c = Val(TextBox9.Text)
        Dim gg As Double
        If RadioButtonExp.Checked = True Then
            If tImageType = 0 Then
                For i = 0 To 255
                    transformG(i) = RegularValue(b ^ (c * (i - a)) - 1)
                Next
                tImage.GrayTransform(transformG)
            ElseIf tImageType = 1 Then
                For i = 0 To 255
                    transformG(i) = RegularValue(b ^ (c * (i - a)) - 1)
                    transformGR(i) = RegularValue(b ^ (c * (i - a)) - 1)
                    transformGG(i) = RegularValue(b ^ (c * (i - a)) - 1)
                    transformGB(i) = RegularValue(b ^ (c * (i - a)) - 1)
                Next
                tImage.GrayTransform(transformG)
            End If
        ElseIf RadioButtonLog.Checked = True Then
            If tImageType = 0 Then
                For i = 0 To 255
                    transformG(i) = RegularValue(a + Math.Log(i + 1) / b * Math.Log(c))
                Next
                tImage.GrayTransform(transformG)
            ElseIf tImageType = 1 Then
                For i = 0 To 255
                    transformG(i) = RegularValue(a + Math.Log(i + 1) / b * Math.Log(c))
                    transformGR(i) = RegularValue(a + Math.Log(i + 1) / b * Math.Log(c))
                    transformGG(i) = RegularValue(a + Math.Log(i + 1) / b * Math.Log(c))
                    transformGB(i) = RegularValue(a + Math.Log(i + 1) / b * Math.Log(c))
                Next
                tImage.GrayTransform(transformGR, transformGG, transformGB)
            End If
        End If

        tImage.Calculate_Histogram()
        ChangedHistPanel.Refresh()
        FunctionCurve.Refresh()
    End Sub
    Public Function RegularValue(ByVal num As Double) As Byte
        If num > 255 Then
            Return CByte(255)
        ElseIf num < 0 Then
            Return CByte(0)
        Else
            Return CByte(num)
        End If
    End Function

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        oImage.Clone(tImage)
        Dim a, b, c, d, mf, mg As Byte
        a = Val(TextBox1.Text)
        b = Val(TextBox2.Text)
        c = Val(TextBox3.Text)
        d = Val(TextBox4.Text)
        mf = Val(TextBox5.Text)
        mg = Val(TextBox6.Text)

        If tImageType = 0 Then
            For i = 0 To a - 1
                transformG(i) = c / a * i
            Next
            For i = a To b - 1
                transformG(i) = (d - c) / (b - a) * (i - a) + c
            Next
            For i = b To mf - 1
                transformG(i) = (mg - d) / (mf - b) * (i - b) + d
            Next
            tImage.GrayTransform(transformG)
        ElseIf tImageType = 1 Then
            For i = 0 To a - 1
                transformG(i) = c / a * i
                transformGR(i) = c / a * i
                transformGG(i) = c / a * i
                transformGB(i) = c / a * i
            Next
            For i = a To b - 1
                transformG(i) = (d - c) / (b - a) * (i - a) + c
                transformGR(i) = (d - c) / (b - a) * (i - a) + c
                transformGG(i) = (d - c) / (b - a) * (i - a) + c
                transformGB(i) = (d - c) / (b - a) * (i - a) + c
            Next
            For i = b To mf - 1
                transformG(i) = (mg - d) / (mf - b) * (i - b) + d
                transformGR(i) = (mg - d) / (mf - b) * (i - b) + d
                transformGG(i) = (mg - d) / (mf - b) * (i - b) + d
                transformGB(i) = (mg - d) / (mf - b) * (i - b) + d
            Next
            tImage.GrayTransform(transformGR, transformGG, transformGB)
        End If

        tImage.Calculate_Histogram()
        ChangedHistPanel.Refresh()
        FunctionCurve.Refresh()
    End Sub
End Class