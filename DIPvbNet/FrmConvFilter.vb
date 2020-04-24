Public Class FrmConvFilter
    Private mImage As ImageClass
    Private txtArray(80) As Integer
    Public Property m_Image() ' 设置图像对象指针属性
        Get
            Return mImage
        End Get
        Set(ByVal value)
            mImage = value
        End Set
    End Property
    Private Sub FrmConvFilter_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim pos As Point
        For i = 0 To 8
            For j = 0 To 8
                Dim txtBox = New TextBox
                pos.X = j * 40 + 4
                pos.Y = i * 24 + 40
                txtBox.Location = pos
                txtBox.Width = 32
                txtBox.Height = 20
                txtBox.TextAlign = HorizontalAlignment.Center
                txtBox.BorderStyle = BorderStyle.FixedSingle
                txtBox.Name = "Txt" & (i * 9 + j + 1)
                txtArray(i * 9 + j) = Controls.Count
                Me.Controls.Add(txtBox)
                AddHandler txtBox.LostFocus, AddressOf TxtBox_LostFocus
                'AddHandler在运行时将事件与事件处理程序相关联。
            Next
        Next
        For i = 3 To 6
            For j = 3 To 6
                Me.Controls.Item(txtArray(i * 9 + j)).Text = "1"
            Next
        Next
        AddHandler NumericUpDown1.ValueChanged, AddressOf FilterSizeChanged
        FilterSizeChanged()
    End Sub

    Private Sub TxtBox_LostFocus()
        Dim n As Integer = NumericUpDown1.Value \ 2
        Dim deno As Integer = 0 '新分母
        For i = 4 - n To 4 + n
            For j = 4 - n To 4 + n
                deno += Int(Me.Controls.Item(txtArray(i * 9 + j)).Text)
            Next
        Next
        TextBox1.Text = deno
    End Sub
    Private Sub FilterSizeChanged()
        Dim n As Integer = NumericUpDown1.Value \ 2
        Dim deno As Integer = 0 '分母
        For i = 0 To 8
            For j = 0 To 8
                If (i < 4 - n Or i > 4 + n) Or (j < 4 - n Or j > 4 + n) Then
                    Me.Controls.Item(txtArray(i * 9 + j)).Text = ""
                    Me.Controls.Item(txtArray(i * 9 + j)).Enabled = False
                Else
                    If Me.Controls.Item(txtArray(i * 9 + j)).Enabled = False Then
                        Me.Controls.Item(txtArray(i * 9 + j)).Enabled = True
                        Me.Controls.Item(txtArray(i * 9 + j)).Text = "1"
                    End If
                    deno += Int(Me.Controls.Item(txtArray(i * 9 + j)).Text)
                End If
            Next
        Next
        If deno = 0 Then
            TextBox1.Text = 1
        Else
            TextBox1.Text = deno
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        Dim filter() As Integer = {-1, 0, 1, -1, 0, 1, -1, 0, 1}
        ChangeFilter(filter)
    End Sub


    Private Function GetFilter() As Integer()
        Dim n As Integer = NumericUpDown1.Value \ 2
        Dim ret(NumericUpDown1.Value * NumericUpDown1.Value - 1) As Integer
        Dim k As Integer = 0
        For i = 4 - n To 4 + n
            For j = 4 - n To 4 + n
                ret(k) = Int(Me.Controls.Item(txtArray(i * 9 + j)).Text)
                k += 1
            Next
        Next
        Return ret
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim filter() As Integer
        filter = GetFilter()
        mImage.ConvFilter(filter, Int(TextBox1.Text), CheckBox1.Checked)
        MainForm.DataChange()
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        Dim filter() As Integer = {-1, -1, -1, 0, 0, 0, 1, 1, 1}
        ChangeFilter(filter)
    End Sub

    Private Function ChangeFilter(ByVal data() As Integer) As Boolean
        Dim n As Integer = NumericUpDown1.Value \ 2
        Dim k As Integer = 0
        Dim deno As Integer = 0

        For i = 4 - n To 4 + n
            For j = 4 - n To 4 + n
                Me.Controls.Item(txtArray(i * 9 + j)).Text = data(k)
                deno += data(k)
                k += 1
            Next
        Next
        If deno = 0 Then
            TextBox1.Text = 1
        Else
            TextBox1.Text = deno
        End If
    End Function

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        Dim filter() As Integer = {-1, -1, 0, -1, 0, 1, 0, 1, 1}
        ChangeFilter(filter)
    End Sub

    Private Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton4.CheckedChanged
        Dim filter() As Integer = {0, 1, 1, -1, 0, 1, -1, -1, 0}
        ChangeFilter(filter)
    End Sub

    Private Sub RadioButton8_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton8.CheckedChanged
        Dim filter() As Integer = {-1, -2, 1, 0, 0, 0, 1, 2, 1}
        ChangeFilter(filter)
    End Sub

    Private Sub RadioButton7_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton7.CheckedChanged
        Dim filter() As Integer = {-1, 0, 1, -2, 0, 2, -1, 0, 1}
        ChangeFilter(filter)
    End Sub

    Private Sub RadioButton6_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton6.CheckedChanged
        Dim filter() As Integer = {-2, -1, 0, -1, 0, 1, 0, 1, 2}
        ChangeFilter(filter)
    End Sub

    Private Sub RadioButton5_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton5.CheckedChanged
        Dim filter() As Integer = {0, -1, 2, -1, 0, 1, -2, -1, 0}
        ChangeFilter(filter)
    End Sub

    Private Sub RadioButton12_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton12.CheckedChanged
        Dim filter() As Integer = {1, 1, 1, 1, 1, 1, 1, 1, 1}
        ChangeFilter(filter)
    End Sub

    Private Sub RadioButton11_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton11.CheckedChanged
        Dim filter() As Integer = {1, 1, 1, 1, 2, 1, 1, 1, 1}
        ChangeFilter(filter)
    End Sub

    Private Sub RadioButton10_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton10.CheckedChanged
        Dim filter() As Integer = {1, 2, 1, 2, 4, 2, 1, 2, 1}
        ChangeFilter(filter)
    End Sub

    Private Sub RadioButton9_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton9.CheckedChanged
        Dim filter() As Integer = {1, 1, 1, 1, 0, 1, 1, 1, 1}
        ChangeFilter(filter)
    End Sub

    Private Sub RadioButton16_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton16.CheckedChanged
        Dim filter() As Integer = {0, -1, 0, -1, 4, -1, 0, -1, 0}
        ChangeFilter(filter)
    End Sub

    Private Sub RadioButton15_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton15.CheckedChanged
        Dim filter() As Integer = {0, -1, 0, -1, 5, -1, 0, -1, 0}
        ChangeFilter(filter)
    End Sub

    Private Sub RadioButton14_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton14.CheckedChanged
        Dim filter() As Integer = {-1, -1, -1, -1, 8, -1, -1, -1, -1}
        ChangeFilter(filter)
    End Sub

    Private Sub RadioButton13_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton13.CheckedChanged
        Dim filter() As Integer = {1, -2, 1, -2, 4, -2, 1, -2, 1}
        ChangeFilter(filter)
    End Sub
End Class