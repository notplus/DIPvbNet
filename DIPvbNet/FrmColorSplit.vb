Public Class FrmColorSplit
    Private cImage As ImageClass
    Private rImage As ImageClass
    Private gImage As ImageClass
    Private bImage As ImageClass
    Public Function SetImageClass(ByVal mImage As ImageClass) As Boolean
        cImage = mImage
        mImage.Clone(rImage)
        mImage.Clone(gImage)
        mImage.Clone(bImage)
    End Function
    Private Sub FrmColorSplit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim gNoChange(255), gZero(255) As Byte
        For i = 0 To 255
            gNoChange(i) = i
            gZero(i) = 0
        Next
        rImage.GrayTransform(gNoChange, gZero, gZero)
        gImage.GrayTransform(gZero, gNoChange, gZero)
        bImage.GrayTransform(gZero, gZero, gNoChange)
        PictureBox2.Refresh()
        PictureBox3.Refresh()
        PictureBox4.Refresh()

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
End Class