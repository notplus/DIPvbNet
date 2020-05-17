Imports System.IO
Public Class FrmGeoCorrection
    Private newPoint As Point
    Private mImage As ImageClass
    Private Items As Integer
    Private a() As Double 'a 参数数组
    Private status As Integer '0 - 添加点 1 - 修改点 

    Public Structure tPoint
        Public id As Integer
        Public status As Boolean
        Public col As Double
        Public row As Double
        Public X As Double
        Public Y As Double
        Public vx As Double
        Public vy As Double
    End Structure
    Private pts() As tPoint

    Public Property m_Image() ' 设置图像对象指针属性
        Get
            Return mImage
        End Get
        Set(ByVal value)
            mImage = value
        End Set
    End Property
    Private Sub FrmGeoCorrection_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ListView1.CheckBoxes = True
        ListView1.FullRowSelect = True
        ListView1.GridLines = True

        ListView1.Columns.Add("", 20, HorizontalAlignment.Center)
        ListView1.Columns.Add("点号", 40, HorizontalAlignment.Center)
        ListView1.Columns.Add("图像位置col", 80, HorizontalAlignment.Center)
        ListView1.Columns.Add("图像位置row", 80, HorizontalAlignment.Center)
        ListView1.Columns.Add("目标位置X", 80, HorizontalAlignment.Center)
        ListView1.Columns.Add("目标位置Y", 80, HorizontalAlignment.Center)
        ListView1.Columns.Add("残差X", 80, HorizontalAlignment.Center)
        ListView1.Columns.Add("残差Y", 80, HorizontalAlignment.Center)

        Items = 0

    End Sub

    Private Sub ListView1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles ListView1.MouseDoubleClick
        ListView1.SelectedItems.Item(0).Checked = Not ListView1.SelectedItems.Item(0).Checked
        Dim textbox_loc As Point
        If (e.X - 222) \ 80 > 1 Or e.X < 222 Then
            Exit Sub
        End If
        textbox_loc.X = 222 + 80 * ((e.X - 222) \ 80)
        textbox_loc.Y = 25 + 16 * ((e.Y - 25) \ 16)
        TextBox1.Location = textbox_loc
        TextBox1.Visible = True
        ActiveControl = TextBox1
        'selectedItem = ListView1.SelectedItems.Item(0)
    End Sub

    Private Sub TextBox1_LostFocus(sender As Object, e As EventArgs) Handles TextBox1.LostFocus
        TextBox1.Visible = False
        TextBox1.Text = ""
    End Sub

    Private Sub TextBox1_DoubleClick(sender As Object, e As EventArgs) Handles TextBox1.DoubleClick

    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        OpenFileDialog1.Filter = "Text Files | *.txt"
        OpenFileDialog1.DefaultExt = "txt"
        OpenFileDialog1.ShowDialog()
        If OpenFileDialog1.FileName = "" Or Dir(OpenFileDialog1.FileName) = "" Then
            Exit Sub
        End If

        Dim MyReader As New FileIO.TextFieldParser(OpenFileDialog1.FileName)
        MyReader.TextFieldType = FileIO.FieldType.Delimited
        MyReader.SetDelimiters(",")

        ListView1.Items.Clear()
        Items = 0
        While Not MyReader.EndOfData
            Try
                Dim currentRow = MyReader.ReadFields()
                Dim currentField As String
                Dim iteme As New ListViewItem
                iteme.Checked = True
                Items += 1
                For Each currentField In currentRow
                    iteme.SubItems.Add(currentField)
                    'MsgBox(currentField)
                Next
                iteme.SubItems.Add("")
                iteme.SubItems.Add("")
                ListView1.Items.Add(iteme)
            Catch ex As Microsoft.VisualBasic.
                        FileIO.MalformedLineException
                MsgBox("Line " & ex.Message &
                "is not valid and will be skipped.")
            End Try
        End While
        MyReader.Close()

    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            ListView1.SelectedItems.Item(0).SubItems.Item((TextBox1.Location.X - 222) \ 80 + 4).Text = TextBox1.Text
            ActiveControl = ListView1
        End If
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        SaveFileDialog1.Filter = "Text Files | *.txt"
        SaveFileDialog1.DefaultExt = "txt"
        SaveFileDialog1.ShowDialog()

        If SaveFileDialog1.FileName = "" Or Dir(SaveFileDialog1.FileName) = "" Then
            Exit Sub
        End If

        Using sw As StreamWriter = New StreamWriter(SaveFileDialog1.FileName)
            For i = 0 To Items - 1
                If ListView1.Items(i).Checked = True Then
                    sw.WriteLine(i & "," & ListView1.Items(i).SubItems(2).Text & "," & ListView1.Items(i).SubItems(3).Text &
                                 "," & ListView1.Items(i).SubItems(4).Text & "," & ListView1.Items(i).SubItems(5).Text)
                End If
            Next
        End Using
    End Sub

    Public Sub SetPoint(ByVal mpoint As Point)
        newPoint = mpoint
        If status = 0 Then
            Dim itemm As New ListViewItem
            itemm.Checked = True
            itemm.SubItems.Add(Items + 1)
            itemm.SubItems.Add(mImage.MapToImageX(newPoint.X))
            itemm.SubItems.Add(mImage.MapToImageY(newPoint.Y))
            itemm.SubItems.Add("")
            itemm.SubItems.Add("")
            itemm.SubItems.Add("")
            itemm.SubItems.Add("")
            ListView1.Items.Add(itemm)
            Items += 1
        Else
            ListView1.SelectedItems(0).SubItems(2).Text = mImage.MapToImageX(newPoint.X)
            ListView1.SelectedItems(0).SubItems(3).Text = mImage.MapToImageY(newPoint.Y)
        End If

    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        MainForm.waitClick = True
        MainForm.Panel.Cursor = Cursors.Cross
        status = 0
    End Sub

    Public Function Transfer(ByVal u As Double, ByVal v As Double, ByRef x As Double, ByRef y As Double) As Boolean
        Select Case ToolStripComboBox1.SelectedIndex
            Case 0
                x = a(0) + a(2) * u - a(3) * v
                y = a(1) + a(3) * u + a(2) * v
            Case 1
                x = a(0) + a(1) * u + a(2) * v
                y = a(3) + a(4) * u + a(5) * v
            Case 2
                x = a(0) + a(1) * u + a(2) * v + a(3) * u * v + a(4) * u ^ 2 + a(5) * v ^ 2
                y = a(6) + a(7) * u + a(8) * v + a(9) * u * v + a(10) * u ^ 2 + a(11) * v ^ 2
        End Select
    End Function

    Private Sub ToolStripButton6_Click(sender As Object, e As EventArgs) Handles ToolStripButton6.Click
        ReDim pts(Items - 1)
        Dim ptNo As Integer = Items '总点数，记录输入的控制点总数
        Dim ptUse As Integer = 0 '被使用的点数
        Dim AA(,) As Double 'AA 误差方程式系数和常数项
        Dim BB(,) As Double 'BB 法方程系数 BB=tr(AA)*AA

        Dim CC() As Double 'CC 法方程式常数项 CC=tr(AA)*
        Dim paraNum As Integer 'paraNum 参数的总个数 
        'paraNum 相似变换为4, 一阶仿射为6, 二阶仿射为12

        Dim ptUseIndex(ptNo) As Integer

        Select Case ToolStripComboBox1.SelectedIndex
            Case 0
                paraNum = 4
            Case 1
                paraNum = 6
            Case 2
                paraNum = 12
        End Select

        Dim j As Integer = 0
        Dim k As Integer
        For i = 0 To Items - 1
            pts(i).id = i
            pts(i).status = ListView1.Items(i).Checked
            pts(i).col = Val(ListView1.Items(i).SubItems(2).Text)
            pts(i).row = Val(ListView1.Items(i).SubItems(3).Text)
            pts(i).X = Val(ListView1.Items(i).SubItems(4).Text)
            pts(i).Y = Val(ListView1.Items(i).SubItems(5).Text)

            If ListView1.Items(i).Checked = True Then
                ptUse += 1
                ptUseIndex(j) = i
                j += 1
            End If

        Next


        ReDim AA(ptUse * 2 - 1, paraNum)
        ReDim BB(paraNum - 1, paraNum - 1)
        ReDim a(paraNum - 1)
        ReDim CC(paraNum - 1)
        j = 0

        If ToolStripComboBox2.SelectedIndex = 0 Then
            Select Case ToolStripComboBox1.SelectedIndex '填充法
                Case 0 '4参数模型, 相似变换， Helmut变换
                    For i = 0 To ptUse - 1 '列误差方程式，四参数
                        ' Vx = a(0)*1 + a(1)*0 + a(2)*x - a(3)*y - col
                        ' Vy = a(0)*0 + a(1)*1 + a(2)*y + a(3)*x - row
                        ' a(0)=>x0, a(1)=>y0, a(2)=>a, a(3)=>b
                        AA(i * 2, 0) = 1.0#
                        AA(i * 2, 1) = 0#
                        AA(i * 2, 2) = pts(ptUseIndex(i)).X
                        AA(i * 2, 3) = -pts(ptUseIndex(i)).Y
                        AA(i * 2, 4) = -pts(ptUseIndex(i)).col
                        AA(i * 2 + 1, 0) = 0#
                        AA(i * 2 + 1, 1) = 1.0#
                        AA(i * 2 + 1, 2) = pts(ptUseIndex(i)).Y
                        AA(i * 2 + 1, 3) = pts(ptUseIndex(i)).X
                        AA(i * 2 + 1, 4) = -pts(ptUseIndex(i)).row
                    Next i
                Case 1
                    For i = 0 To ptUse - 1 '列误差方程式，六参数
                        ' Vx = a(0)*1 + a(1)*x + a(2)*y + a(3)*0 + a(4)*0 + a(5)*0 - col
                        ' Vy = a(0)*0 + a(1)*0 + a(2)*0 + a(3)*1 + a(4)*x + a(5)*y - row
                        AA(i * 2, 0) = 1.0#
                        AA(i * 2, 1) = pts(ptUseIndex(i)).X
                        AA(i * 2, 2) = pts(ptUseIndex(i)).Y
                        AA(i * 2, 3) = 0#
                        AA(i * 2, 4) = 0#
                        AA(i * 2, 5) = 0#
                        AA(i * 2, 6) = -pts(ptUseIndex(i)).col
                        AA(i * 2 + 1, 0) = 0#
                        AA(i * 2 + 1, 1) = 0#
                        AA(i * 2 + 1, 2) = 0#
                        AA(i * 2 + 1, 3) = 1.0#
                        AA(i * 2 + 1, 4) = pts(ptUseIndex(i)).X
                        AA(i * 2 + 1, 5) = pts(ptUseIndex(i)).Y
                        AA(i * 2 + 1, 6) = -pts(ptUseIndex(i)).row
                    Next i
                Case 2
                    For i = 0 To ptUse - 1 '列误差方程式，十二参数
                        AA(i * 2, 0) = 1.0#
                        AA(i * 2, 1) = pts(ptUseIndex(i)).X
                        AA(i * 2, 2) = pts(ptUseIndex(i)).Y
                        AA(i * 2, 3) = pts(ptUseIndex(i)).X ^ 2
                        AA(i * 2, 4) = pts(ptUseIndex(i)).Y ^ 2
                        AA(i * 2, 5) = pts(ptUseIndex(i)).X * pts(ptUseIndex(i)).Y
                        AA(i * 2, 6) = 0#
                        AA(i * 2, 7) = 0#
                        AA(i * 2, 8) = 0#
                        AA(i * 2, 9) = 0#
                        AA(i * 2, 10) = 0#
                        AA(i * 2, 11) = 0#
                        AA(i * 2, 12) = -pts(ptUseIndex(i)).col
                        AA(i * 2 + 1, 0) = 0#
                        AA(i * 2 + 1, 1) = 0#
                        AA(i * 2 + 1, 2) = 0#
                        AA(i * 2 + 1, 3) = 0#
                        AA(i * 2 + 1, 4) = 0#
                        AA(i * 2 + 1, 5) = 0#
                        AA(i * 2 + 1, 6) = 1.0#
                        AA(i * 2 + 1, 7) = pts(ptUseIndex(i)).X
                        AA(i * 2 + 1, 8) = pts(ptUseIndex(i)).Y
                        AA(i * 2 + 1, 9) = pts(ptUseIndex(i)).X ^ 2
                        AA(i * 2 + 1, 10) = pts(ptUseIndex(i)).Y ^ 2
                        AA(i * 2 + 1, 11) = pts(ptUseIndex(i)).X * pts(ptUseIndex(i)).Y
                        AA(i * 2 + 1, 12) = -pts(ptUseIndex(i)).row
                    Next i
            End Select
        Else
            Select Case ToolStripComboBox1.SelectedIndex
                Case 0 '4参数模型, 相似变换， Helmut变换
                    For i = 0 To ptUse - 1 '列误差方程式，四参数
                        ' Vx = a(0)*1 + a(1)*0 + a(2)*col - a(3)*row - x
                        ' Vy = a(0)*0 + a(1)*1 + a(2)*row + a(3)*col - y
                        ' a(0)=>x0, a(1)=>y0, a(2)=>a, a(3)=>b
                        AA(i * 2, 0) = 1.0#
                        AA(i * 2, 1) = 0#
                        AA(i * 2, 2) = pts(ptUseIndex(i)).col
                        AA(i * 2, 3) = -pts(ptUseIndex(i)).row
                        AA(i * 2, 4) = -pts(ptUseIndex(i)).X
                        AA(i * 2 + 1, 0) = 0#
                        AA(i * 2 + 1, 1) = 1.0#
                        AA(i * 2 + 1, 2) = pts(ptUseIndex(i)).row
                        AA(i * 2 + 1, 3) = pts(ptUseIndex(i)).col
                        AA(i * 2 + 1, 4) = -pts(ptUseIndex(i)).Y
                    Next i
                Case 1
                    For i = 0 To ptUse - 1 '列误差方程式，六参数
                        ' Vx = a(0)*1 + a(1)*col + a(2)*row + a(3)*0 + a(4)*0 + a(5)*0 - x
                        ' Vy = a(0)*0 + a(1)*0 + a(2)*0 + a(3)*1 + a(4)*col + a(5)*row - y
                        AA(i * 2, 0) = 1.0#
                        AA(i * 2, 1) = pts(ptUseIndex(i)).col
                        AA(i * 2, 2) = pts(ptUseIndex(i)).row
                        AA(i * 2, 3) = 0#
                        AA(i * 2, 4) = 0#
                        AA(i * 2, 5) = 0#
                        AA(i * 2, 6) = -pts(ptUseIndex(i)).X
                        AA(i * 2 + 1, 0) = 0#
                        AA(i * 2 + 1, 1) = 0#
                        AA(i * 2 + 1, 2) = 0#
                        AA(i * 2 + 1, 3) = 1.0#
                        AA(i * 2 + 1, 4) = pts(ptUseIndex(i)).col
                        AA(i * 2 + 1, 5) = pts(ptUseIndex(i)).row
                        AA(i * 2 + 1, 6) = -pts(ptUseIndex(i)).Y
                    Next i
                Case 2
                    For i = 0 To ptUse - 1 '列误差方程式，十二参数
                        AA(i * 2, 0) = 1.0#
                        AA(i * 2, 1) = pts(ptUseIndex(i)).col
                        AA(i * 2, 2) = pts(ptUseIndex(i)).row
                        AA(i * 2, 3) = pts(ptUseIndex(i)).col ^ 2
                        AA(i * 2, 4) = pts(ptUseIndex(i)).row ^ 2
                        AA(i * 2, 5) = pts(ptUseIndex(i)).col * pts(ptUseIndex(i)).row
                        AA(i * 2, 6) = 0#
                        AA(i * 2, 7) = 0#
                        AA(i * 2, 8) = 0#
                        AA(i * 2, 9) = 0#
                        AA(i * 2, 10) = 0#
                        AA(i * 2, 11) = 0#
                        AA(i * 2, 12) = -pts(ptUseIndex(i)).X
                        AA(i * 2 + 1, 0) = 0#
                        AA(i * 2 + 1, 1) = 0#
                        AA(i * 2 + 1, 2) = 0#
                        AA(i * 2 + 1, 3) = 0#
                        AA(i * 2 + 1, 4) = 0#
                        AA(i * 2 + 1, 5) = 0#
                        AA(i * 2 + 1, 6) = 1.0#
                        AA(i * 2 + 1, 7) = pts(ptUseIndex(i)).col
                        AA(i * 2 + 1, 8) = pts(ptUseIndex(i)).row
                        AA(i * 2 + 1, 9) = pts(ptUseIndex(i)).col ^ 2
                        AA(i * 2 + 1, 10) = pts(ptUseIndex(i)).row ^ 2
                        AA(i * 2 + 1, 11) = pts(ptUseIndex(i)).col * pts(ptUseIndex(i)).row
                        AA(i * 2 + 1, 12) = -pts(ptUseIndex(i)).Y
                    Next i
            End Select
        End If

        ' 构成法方程
        For i = 0 To paraNum - 1
            For j = i To paraNum - 1
                BB(i, j) = 0#
                For k = 0 To 2 * ptUse - 1
                    BB(i, j) = BB(i, j) + AA(k, i) * AA(k, j)
                Next
                If i <> j Then BB(j, i) = BB(i, j)
            Next
        Next

        ' 构成法方程式常数
        For i = 0 To paraNum - 1
            CC(i) = 0#
            For k = 0 To 2 * ptUse - 1
                CC(i) = CC(i) + AA(k, i) * AA(k, paraNum)
            Next
        Next

        k = Dinvert(BB, paraNum)

        If k <> 0 Then
            MsgBox("无法求逆阵",, "警告")
            Erase AA
            Erase BB
            Erase CC
            Erase a
            Exit Sub
        End If

        ' 参数求解
        For i = 0 To paraNum - 1
            a(i) = 0#
            For k = 0 To paraNum - 1
                a(i) = a(i) - BB(i, k) * CC(k)
            Next
        Next

        ' 计算残差和方差
        Dim v2v As Double
        Dim X, Y As Double
        Dim u, v As Double
        v2v = 0#

        For i = 0 To ptUse - 1
            If ToolStripComboBox2.SelectedIndex = 0 Then '填充法
                u = pts(ptUseIndex(i)).X
                v = pts(ptUseIndex(i)).Y
            Else
                u = pts(ptUseIndex(i)).col
                v = pts(ptUseIndex(i)).row
            End If

            Transfer(u, v, X, Y)

            If ToolStripComboBox2.SelectedIndex = 0 Then '填充法
                pts(ptUseIndex(i)).vx = pts(ptUseIndex(i)).col - X
                pts(ptUseIndex(i)).vy = pts(ptUseIndex(i)).row - Y
            Else
                pts(ptUseIndex(i)).vx = pts(ptUseIndex(i)).X - X
                pts(ptUseIndex(i)).vy = pts(ptUseIndex(i)).Y - Y
            End If

            v2v = v2v + pts(ptUseIndex(i)).vx ^ 2 + pts(ptUseIndex(i)).vy ^ 2
            ListView1.Items(pts(ptUseIndex(i)).id).SubItems(6).Text = Strings.Format(pts(ptUseIndex(i)).vx, "0.##0")
            ListView1.Items(pts(ptUseIndex(i)).id).SubItems(7).Text = Strings.Format(pts(ptUseIndex(i)).vy, "0.##0")
        Next

        If 2 * ptUse - paraNum = 0 Then
            v2v = 0#
        Else
            v2v = Math.Sqrt(v2v / (2 * ptUse - paraNum))
        End If

        Me.Text = "几何纠正" & "- 方差为" & Strings.Format(v2v, "0.##0")
    End Sub

    Private Sub ToolStripButton7_Click(sender As Object, e As EventArgs) Handles ToolStripButton7.Click
        Dim new_w, new_h As Long

        Dim minX, minY, maxX, maxY As Double

        Dim x, y As Double

        Me.Transfer(0, 0, x, y)
        If x < minX Then
            minX = x
        End If
        If x > maxX Then
            maxX = x
        End If
        If y < minY Then
            minY = y
        End If
        If y > maxY Then
            maxY = y
        End If

        Me.Transfer(mImage.Width() - 1, 0, x, y)
        If x < minX Then
            minX = x
        End If
        If x > maxX Then
            maxX = x
        End If
        If y < minY Then
            minY = y
        End If
        If y > maxY Then
            maxY = y
        End If

        Me.Transfer(0, mImage.Height() - 1, x, y)
        If x < minX Then
            minX = x
        End If
        If x > maxX Then
            maxX = x
        End If
        If y < minY Then
            minY = y
        End If
        If y > maxY Then
            maxY = y
        End If

        Me.Transfer(mImage.Width() - 1, mImage.Height() - 1, x, y)
        If x < minX Then
            minX = x
        End If
        If x > maxX Then
            maxX = x
        End If
        If y < minY Then
            minY = y
        End If
        If y > maxY Then
            maxY = y
        End If

        new_w = maxX - minX
        new_h = maxY - minY
        mImage.GeoCorrect(new_w, new_h, minX, minY, ToolStripComboBox2.SelectedIndex)
        MainForm.DataChange()
    End Sub

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        MainForm.waitClick = True
        MainForm.Panel.Cursor = Cursors.Cross
        status = 1
    End Sub

    Private Sub ToolStripButton5_Click(sender As Object, e As EventArgs) Handles ToolStripButton5.Click
        Items -= 1
        ListView1.Items.Remove(ListView1.SelectedItems(0))
    End Sub
End Class