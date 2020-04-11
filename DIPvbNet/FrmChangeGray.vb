Public Class FrmChangeGray
    Private mImage As ImageClass
    Private oImageBitData() As Byte '灰度图像原数据
    Private Sub FrmChangeGray_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RadioButtonNow.Checked = True
        mImage.getBitData(oImageBitData)

    End Sub

    Private Sub OriginHistPanel_Paint(sender As Object, e As PaintEventArgs) Handles OriginHistPanel.Paint
        mImage.DrawHist(e.Graphics, OriginHistPanel.Width, OriginHistPanel.Height, 0)
    End Sub

    Private Sub ChangedHistPanel_Paint(sender As Object, e As PaintEventArgs) Handles ChangedHistPanel.Paint
        mImage.DrawHist(e.Graphics, ChangedHistPanel.Width, ChangedHistPanel.Height, 0)
    End Sub

    Private Sub RadioButtonNow_Click(sender As Object, e As EventArgs) Handles RadioButtonNow.Click
        GroupBoxCurve.Enabled = False
        GroupBoxLinear.Enabled = False
        mImage.modifyBitData(oImageBitData)
        mImage.Calculate_Histogram()
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
        mImage.histEqualize()
        mImage.Calculate_Histogram()
        ChangedHistPanel.Refresh()
        FunctionCurve.Refresh()
    End Sub

    Public Property m_Image() ' 设置图像对象指针属性
        Get
            Return mImage
        End Get
        Set(ByVal value)
            mImage = value
        End Set
    End Property

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        mImage.enableChanges()
        MainForm.Refresh()
        OriginHistPanel.Refresh()
    End Sub

    Private Sub FunctionCurve_Paint(sender As Object, e As PaintEventArgs) Handles FunctionCurve.Paint
        mImage.DrawCurve(e.Graphics, FunctionCurve.Width, FunctionCurve.Height)
    End Sub

    Private Sub FunctionCurve_MouseMove(sender As Object, e As MouseEventArgs) Handles FunctionCurve.MouseMove
        LabelCursorPosition.Text = Int((e.X - 5) / 1.11) & "," & Int((-e.Y + 280.5))
    End Sub
End Class