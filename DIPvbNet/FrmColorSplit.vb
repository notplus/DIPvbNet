Public Class FrmColorSplit
    Private cImage As ImageClass
    Private rImage As ImageClass
    Private gImage As ImageClass
    Private bImage As ImageClass
    'Private oImage As ImageClass
    Private flag As Boolean
    Private lastExchangeInverse As String '上次变换的逆变换
    Private rflag As Boolean
    Private gflag As Boolean
    Private bflag As Boolean
    Public Function SetImageClass(ByVal mImage As ImageClass) As Boolean
        mImage.Clone(cImage)
        mImage.Clone(rImage)
        mImage.Clone(gImage)
        mImage.Clone(bImage)
        'mImage.Clone(oImage)
    End Function
    Private Sub FrmColorSplit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Dim gNoChange(255), gZero(255) As Byte
        'flag = True
        'For i = 0 To 255
        '    gNoChange(i) = i
        '    gZero(i) = 0
        'Next
        'rImage.GrayTransform(gNoChange, gZero, gZero)
        'gImage.GrayTransform(gZero, gNoChange, gZero)
        'bImage.GrayTransform(gZero, gZero, gNoChange)
        bImage.ConvertToGrayImageWithChannel(0)
        gImage.ConvertToGrayImageWithChannel(1)
        rImage.ConvertToGrayImageWithChannel(2)
        bImage.SetBluePalette()
        gImage.SetGreenPalette()
        rImage.SetRedPalette()
        bflag = True
        gflag = True
        rflag = True

        PictureBox2.Refresh()
        PictureBox3.Refresh()
        PictureBox4.Refresh()
        lastExchangeInverse = "RGB"
    End Sub


    Private Sub PictureBox2_Paint(sender As Object, e As PaintEventArgs) Handles PictureBox2.Paint
        If rImage.isAvailable Then
            rImage.Display(e.Graphics, PictureBox2.Width, PictureBox2.Height)
        End If
    End Sub

    Private Sub PictureBox3_Paint(sender As Object, e As PaintEventArgs) Handles PictureBox3.Paint
        If gImage.isAvailable Then
            gImage.Display(e.Graphics, PictureBox3.Width, PictureBox3.Height)
        End If
    End Sub

    Private Sub PictureBox4_Paint(sender As Object, e As PaintEventArgs) Handles PictureBox4.Paint
        If bImage.isAvailable Then
            bImage.Display(e.Graphics, PictureBox4.Width, PictureBox4.Height)
        End If
    End Sub

    Private Sub PictureBox1_Paint(sender As Object, e As PaintEventArgs) Handles PictureBox1.Paint
        If cImage.isAvailable Then
            cImage.Display(e.Graphics, PictureBox1.Width, PictureBox1.Height)
            'cImage.zoomExtent(e.Graphics)
        End If
    End Sub

    Private Sub FrmColorSplit_Click(sender As Object, e As EventArgs) Handles Me.Click
        GroupBox1.Visible = flag
        flag = Not flag
    End Sub

    Private Sub RadioButton1_Click(sender As Object, e As EventArgs) Handles RadioButton1.Click
        cImage.ExchangeChannel(lastExchangeInverse)
        cImage.ExchangeChannel("RGB")
        lastExchangeInverse = "RGB" 'To BGR
        PictureBox1.Refresh()
    End Sub
    Private Sub RadioButton2_Click(sender As Object, e As EventArgs) Handles RadioButton2.Click
        cImage.ExchangeChannel(lastExchangeInverse) '做上次变换的逆变换使图像转为原始图像
        cImage.ExchangeChannel("RBG") '进行本次变换
        lastExchangeInverse = "RBG"
        PictureBox1.Refresh()
    End Sub
    Private Sub RadioButton3_Click(sender As Object, e As EventArgs) Handles RadioButton3.Click
        cImage.ExchangeChannel(lastExchangeInverse)
        cImage.ExchangeChannel("BRG")
        lastExchangeInverse = "GBR"
        PictureBox1.Refresh()
    End Sub
    Private Sub RadioButton4_Click(sender As Object, e As EventArgs) Handles RadioButton4.Click
        cImage.ExchangeChannel(lastExchangeInverse)
        cImage.ExchangeChannel("BGR")
        lastExchangeInverse = "BGR"
        PictureBox1.Refresh()
    End Sub
    Private Sub RadioButton5_Click(sender As Object, e As EventArgs) Handles RadioButton5.Click
        cImage.ExchangeChannel(lastExchangeInverse)
        cImage.ExchangeChannel("GRB")
        lastExchangeInverse = "GRB"
        PictureBox1.Refresh()
    End Sub
    Private Sub RadioButton6_Click(sender As Object, e As EventArgs) Handles RadioButton6.Click
        cImage.ExchangeChannel(lastExchangeInverse)
        cImage.ExchangeChannel("GBR")
        lastExchangeInverse = "BRG"
        PictureBox1.Refresh()
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        rflag = Not rflag
        If rflag Then
            rImage.SetGrayPalette()
        Else
            rImage.SetRedPalette()
        End If
        PictureBox2.Refresh()
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        gflag = Not gflag
        If gflag Then
            gImage.SetGrayPalette()
        Else
            gImage.SetGreenPalette()
        End If
        PictureBox3.Refresh()
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        bflag = Not bflag
        If bflag Then
            bImage.SetGrayPalette()
        Else
            bImage.SetBluePalette()
        End If
        PictureBox4.Refresh()
    End Sub
End Class