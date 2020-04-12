Public Class FrmHistMatch
    Private oImage As ImageClass
    Private gImage As New ImageClass

    Public Function SetImageClass(ByVal mImage As ImageClass) As Boolean
        oImage = mImage
    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        OpenFileDialog1.ShowDialog()
        If OpenFileDialog1.FileName = "" Or Dir(OpenFileDialog1.FileName) = "" Then
            Return
        Else
            gImage.ReadImage(OpenFileDialog1.FileName)
        End If
        TextBox1.Text = OpenFileDialog1.FileName
        GoalImg.Load(OpenFileDialog1.FileName)
        GoalImg.Refresh()
        gImage.Calculate_Histogram()
        GoalHist.Refresh()
    End Sub

    Private Sub FrmHistMatch_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub OriginalHist_Paint(sender As Object, e As PaintEventArgs) Handles OriginalHist.Paint
        oImage.DrawHist(e.Graphics, OriginalHist.Width, OriginalHist.Height, 0)
    End Sub

    Private Sub GoalHist_Paint(sender As Object, e As PaintEventArgs) Handles GoalHist.Paint
        If gImage.isAvailable Then
            gImage.DrawHist(e.Graphics, GoalHist.Width, GoalHist.Height, 0)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim originalHist(255) As Long
        Dim goalHist(255) As Long
        Dim h1e(255) As Long
        Dim h2e(255) As Long

        oImage.GetmHist(originalHist)
        gImage.GetmHist(goalHist)

        Dim p1(255), p2(255) As Double
        Dim s1(255), s2(255) As Double
        Dim g1(255), g2(255), g(255) As Byte
        For i = 0 To 255
            p1(i) = originalHist(i) / CDbl(oImage.GetmPixels)
            p2(i) = goalHist(i) / CDbl(gImage.GetmPixels)
            h1e(i) = 0
            h2e(i) = 0
        Next
        s1(0) = p1(0) : s2(0) = p2(0)
        For i = 1 To 255
            s1(i) = s1(i - 1) + p1(i)
            s2(i) = s2(i - 1) + p2(i)
        Next
        For i = 0 To 255
            g1(i) = CInt(s1(i) * 255)
            g2(i) = CInt(s2(i) * 255)
        Next
        For i = 0 To 255
            h1e(g1(i)) += originalHist(i)
            h2e(g2(i)) += goalHist(i)
        Next

        Dim tempG As Byte
        Dim minDiff As Single
        For i = 0 To 255
            tempG = 0
            minDiff = Math.Abs(s2(tempG) * 255 - g1(i))
            For j = 0 To 255
                Dim tempDiff As Single
                tempDiff = Math.Abs(s2(j) * 255 - g1(i))
                If minDiff > tempDiff Then
                    minDiff = tempDiff
                    tempG = j
                End If
            Next
            g(i) = tempG
        Next
        If oImage.GetImageType = 0 Then
            oImage.GrayTransform(g)
        ElseIf oImage.GetImageType = 1 Then
            oImage.GrayTransform(g, g, g)
        End If

        MainForm.Refresh()
    End Sub
End Class