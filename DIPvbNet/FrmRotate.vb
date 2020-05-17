Public Class FrmRotate
    Private mImage As ImageClass

    Public Property m_Image() ' 设置图像对象指针属性
        Get
            Return mImage
        End Get
        Set(ByVal value)
            mImage = value
        End Set
    End Property

    Private Sub FrmRotate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RadioButton1.Checked = True
        RadioButton3.Checked = True
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim type_size, type_way As Integer
        type_size = IIf(RadioButton1.Checked, 0, 1)
        type_way = IIf(RadioButton3.Checked, 0, 1)
        Dim angle As Double
        angle = Val(TextBox1.Text)
        mImage.Rotate(type_size, type_way, angle)
        MainForm.DataChange()
    End Sub
End Class