Public Class FrmEditPalette
    Private oImage As ImageClass '原图像指针
    Private NewPalette As Imaging.ColorPalette
    Private pImage As ImageClass
    Public Function SetImageClass(ByVal mImage As ImageClass) As Boolean
        oImage = mImage
        mImage.Clone(pImage)
        MainForm.ChangeImageClass(pImage) '改变当前显示图像
    End Function
    Private Sub PictureBox_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click, PictureBox2.Click, PictureBox3.Click, PictureBox4.Click, PictureBox5.Click, PictureBox6.Click, PictureBox7.Click, PictureBox8.Click, PictureBox9.Click, PictureBox10.Click, PictureBox11.Click, PictureBox12.Click, PictureBox13.Click, PictureBox14.Click, PictureBox15.Click, PictureBox16.Click, PictureBox17.Click, PictureBox18.Click, PictureBox19.Click, PictureBox20.Click, PictureBox21.Click, PictureBox22.Click, PictureBox23.Click, PictureBox24.Click, PictureBox25.Click, PictureBox26.Click, PictureBox27.Click, PictureBox28.Click, PictureBox29.Click, PictureBox30.Click, PictureBox31.Click, PictureBox32.Click, PictureBox33.Click, PictureBox34.Click, PictureBox35.Click, PictureBox36.Click, PictureBox37.Click, PictureBox38.Click, PictureBox39.Click, PictureBox40.Click, PictureBox41.Click, PictureBox42.Click, PictureBox43.Click, PictureBox44.Click, PictureBox45.Click, PictureBox46.Click, PictureBox47.Click, PictureBox48.Click, PictureBox49.Click, PictureBox50.Click, PictureBox51.Click, PictureBox52.Click, PictureBox53.Click, PictureBox54.Click, PictureBox55.Click, PictureBox56.Click, PictureBox57.Click, PictureBox58.Click, PictureBox59.Click, PictureBox60.Click, PictureBox61.Click, PictureBox62.Click, PictureBox63.Click, PictureBox64.Click, PictureBox65.Click, PictureBox66.Click, PictureBox67.Click, PictureBox68.Click, PictureBox69.Click, PictureBox70.Click, PictureBox71.Click, PictureBox72.Click, PictureBox73.Click, PictureBox74.Click, PictureBox75.Click, PictureBox76.Click, PictureBox77.Click, PictureBox78.Click, PictureBox79.Click, PictureBox80.Click, PictureBox81.Click, PictureBox82.Click, PictureBox83.Click, PictureBox84.Click, PictureBox85.Click, PictureBox86.Click, PictureBox87.Click, PictureBox88.Click, PictureBox89.Click, PictureBox90.Click, PictureBox91.Click, PictureBox92.Click, PictureBox93.Click, PictureBox94.Click, PictureBox95.Click, PictureBox96.Click, PictureBox97.Click, PictureBox98.Click, PictureBox99.Click, PictureBox100.Click, PictureBox101.Click, PictureBox102.Click, PictureBox103.Click, PictureBox104.Click, PictureBox105.Click, PictureBox106.Click, PictureBox107.Click, PictureBox108.Click, PictureBox109.Click, PictureBox110.Click, PictureBox111.Click, PictureBox112.Click, PictureBox113.Click, PictureBox114.Click, PictureBox115.Click, PictureBox116.Click, PictureBox117.Click, PictureBox118.Click, PictureBox119.Click, PictureBox120.Click, PictureBox121.Click, PictureBox122.Click, PictureBox123.Click, PictureBox124.Click, PictureBox125.Click, PictureBox126.Click, PictureBox127.Click, PictureBox128.Click, PictureBox129.Click, PictureBox130.Click, PictureBox131.Click, PictureBox132.Click, PictureBox133.Click, PictureBox134.Click, PictureBox135.Click, PictureBox136.Click, PictureBox137.Click, PictureBox138.Click, PictureBox139.Click, PictureBox140.Click, PictureBox141.Click, PictureBox142.Click, PictureBox143.Click, PictureBox144.Click, PictureBox145.Click, PictureBox146.Click, PictureBox147.Click, PictureBox148.Click, PictureBox149.Click, PictureBox150.Click, PictureBox151.Click, PictureBox152.Click, PictureBox153.Click, PictureBox154.Click, PictureBox155.Click, PictureBox156.Click, PictureBox157.Click, PictureBox158.Click, PictureBox159.Click, PictureBox160.Click, PictureBox161.Click, PictureBox162.Click, PictureBox163.Click, PictureBox164.Click, PictureBox165.Click, PictureBox166.Click, PictureBox167.Click, PictureBox168.Click, PictureBox169.Click, PictureBox170.Click, PictureBox171.Click, PictureBox172.Click, PictureBox173.Click, PictureBox174.Click, PictureBox175.Click, PictureBox176.Click, PictureBox177.Click, PictureBox178.Click, PictureBox179.Click, PictureBox180.Click, PictureBox181.Click, PictureBox182.Click, PictureBox183.Click, PictureBox184.Click, PictureBox185.Click, PictureBox186.Click, PictureBox187.Click, PictureBox188.Click, PictureBox189.Click, PictureBox190.Click, PictureBox191.Click, PictureBox192.Click, PictureBox193.Click, PictureBox194.Click, PictureBox195.Click, PictureBox196.Click, PictureBox197.Click, PictureBox198.Click, PictureBox199.Click, PictureBox200.Click, PictureBox201.Click, PictureBox202.Click, PictureBox203.Click, PictureBox204.Click, PictureBox205.Click, PictureBox206.Click, PictureBox207.Click, PictureBox208.Click, PictureBox209.Click, PictureBox210.Click, PictureBox211.Click, PictureBox212.Click, PictureBox213.Click, PictureBox214.Click, PictureBox215.Click, PictureBox216.Click, PictureBox217.Click, PictureBox218.Click, PictureBox219.Click, PictureBox220.Click, PictureBox221.Click, PictureBox222.Click, PictureBox223.Click, PictureBox224.Click, PictureBox225.Click, PictureBox226.Click, PictureBox227.Click, PictureBox228.Click, PictureBox229.Click, PictureBox230.Click, PictureBox231.Click, PictureBox232.Click, PictureBox233.Click, PictureBox234.Click, PictureBox235.Click, PictureBox236.Click, PictureBox237.Click, PictureBox238.Click, PictureBox239.Click, PictureBox240.Click, PictureBox241.Click, PictureBox242.Click, PictureBox243.Click, PictureBox244.Click, PictureBox245.Click, PictureBox246.Click, PictureBox247.Click, PictureBox248.Click, PictureBox249.Click, PictureBox250.Click, PictureBox251.Click, PictureBox252.Click, PictureBox253.Click, PictureBox254.Click, PictureBox255.Click, PictureBox256.Click
        Dim clr As Color
        Dim idx As Integer
        Dim picbox As PictureBox = CType(sender, PictureBox)
        idx = (picbox.Location.X - 13) / 26 + (picbox.Location.Y - 13) / 26 * 16
        If ColorDialog1.ShowDialog(Me) Then
            clr = ColorDialog1.Color
            NewPalette.Entries(idx) = clr
            picbox.BackColor = clr
            If CheckBox1.Checked Then
                pImage.SetPalette(NewPalette)
                MainForm.Refresh()
            End If
        End If
    End Sub

    Private Sub FrmEditPalette_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        NewPalette = pImage.GetPalette
        If oImage.GetImageType = 1 Then
            MsgBox("全彩色图像无调色板",, "提示")
            Close()
        End If
        Dim idx As Integer
        For i = 0 To Me.Controls.Count - 1 ''获取包含在控件内的控件的集合元素数。
            If TypeOf Me.Controls(i) Is PictureBox Then '如果是PictureBox控件
                idx = (Me.Controls(i).Location.X - 13) / 26 + (Me.Controls(i).Location.Y - 13) / 26 * 16
                Me.Controls(i).BackColor = NewPalette.Entries(idx)
            End If

        Next i
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            pImage.SetPalette(NewPalette)
            MainForm.Refresh()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        pImage.Clone(oImage)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        MainForm.ChangeImageClass(oImage)
        MainForm.Refresh()
        Close()
    End Sub
End Class