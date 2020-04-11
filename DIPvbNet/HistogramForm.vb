Public Class HistogramForm
    Private pImage As ImageClass ' 申明一个图像的指针变量，在启用绘制直方图时（show窗体），指定图像对象
    Dim HistStyle As Integer
    Dim mat As System.Drawing.Drawing2D.Matrix

    Public Property m_Image() ' 设置图像对象指针属性
        Get
            Return pImage
        End Get
        Set(ByVal value)
            pImage = value
        End Set
    End Property

    Private Sub HistogramForm_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        HistStyle = 0
    End Sub

    Private Sub HistPanel_Click(sender As System.Object, e As System.EventArgs) Handles HistPanel.Click
        ' 改变直方图绘制形式
        If HistStyle = 0 Then
            HistStyle = 1
        Else
            HistStyle = 0
        End If
        HistPanel.Refresh()
    End Sub

    Private Sub HistPanel_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles HistPanel.MouseMove
        If Not pImage Is Nothing Then
            If pImage.ImageType = 0 Then
                ToolStripStatusLabel2.Text = "鼠标位置=" & e.X & "," & e.Y & "  信息=" & pImage.getHistNumber(e.X)
            End If
        End If
    End Sub

    Private Sub HistPanel_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles HistPanel.Paint
        If Not pImage Is Nothing Then
            Me.Text = "直方图"
            If pImage.ImageType = 0 Then
                pImage.DrawHist(e.Graphics, HistPanel.Width, HistPanel.Height, HistStyle) ' - StatusBar.Height)
                ToolStripStatusLabel1.Text = "熵=" + pImage.Entropy().ToString("0.00") + ",方差=" + pImage.Sigma().ToString("0.00") + ",最大=" + pImage.MaxGrey().ToString("0") + ",最小=" + pImage.MinGrey().ToString("0") + ",平均=" + pImage.Average().ToString("0.00")
            ElseIf pImage.ImageType = 1 Then
                pImage.DrawHist(e.Graphics, HistPanel.Width, HistPanel.Height, HistStyle) ' - StatusBar.Height)
            Else
                Dim f1 As Font = New Font("宋体", 24)
                Dim b1 As Brush = New SolidBrush(Color.Red)
                e.Graphics.DrawString("当前只支持灰度图像和全彩色图像的直方图！", f1, b1, 10, 50)
                f1.Dispose()
                b1.Dispose()
            End If
        End If
    End Sub

    Private Sub HistogramForm_Resize(sender As Object, e As System.EventArgs) Handles Me.Resize
        HistPanel.Refresh()
    End Sub

    Public Sub RefreshHist()
        HistPanel.Refresh()
    End Sub

End Class