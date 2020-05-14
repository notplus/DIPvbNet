Public Class FrmResize
    Private mImage As ImageClass
    Private w, h As Long

    Public Property m_Image() ' 设置图像对象指针属性
        Get
            Return mImage
        End Get
        Set(ByVal value)
            mImage = value
        End Set
    End Property

    Private Sub FrmResize_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If RadioButton1.Checked = True Then
            w = mImage.Width / 2
            h = mImage.Height / 2
        ElseIf RadioButton2.Checked = True Then
            w = mImage.Width * 2
            h = mImage.Height * 2
        ElseIf RadioButton3.Checked = True Then
            w = mImage.Width * NumericUpDown1.Value / 100
            h = mImage.Height * NumericUpDown1.Value / 100
        ElseIf RadioButton4.Checked = True Then
            w = Val(TextBox1.Text)
            h = Val(TextBox2.Text)
        End If
        mImage.Resize(h, w)
        MainForm.DataChange()
    End Sub

End Class