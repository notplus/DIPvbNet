Module ModuleGlobal

    '/* *******************************************************************
    '
    'SUBROUTINE:      dinvert
    '
    '     INVERT BY CHOLESKY DECOMP
    '
    '     RETURN:   0 - success
    '               1 - matrix not positive definite
    '               2 - Singularity
    '               3 - Can't sqrt a neg. number
    '               4 - Can't divide by zero
    '
    '     WARNING: you must check return value
    '
    '*********************************************************************** */
    '
    Public Function Dinvert(mat(,) As Double, ByVal n As Integer)
        Dim i As Integer, j As Integer, k As Integer
        Dim sum As Double

        '   /* Check for Positive definiteness */
        ' 检查矩阵的正定性
        For i = 0 To n - 1
            If mat(i, i) < 0# Then
                Dinvert = 1
                Exit Function
            End If
            If Math.Abs(mat(i, i)) < 0.000000000001 Then
                Dinvert = 2
                Exit Function
            End If
        Next i
        If n = 1 Then
            mat(0, 0) = 1.0# / mat(0, 0)
            Dinvert = 0
            Exit Function
        End If
        If n = 2 Then
            sum = mat(0, 0)
            mat(0, 0) = mat(1, 1)
            mat(1, 1) = sum
            sum = mat(0, 0) * mat(1, 1) - mat(1, 0) * mat(0, 1)
            If Math.Abs(sum) < 0.000000000001 Then
                Dinvert = 4
                Exit Function
            End If
            mat(0, 0) = mat(0, 0) / sum
            mat(0, 1) = -mat(0, 1) / sum
            mat(1, 0) = -mat(1, 0) / sum
            mat(1, 1) = mat(1, 1) / sum
            Dinvert = 0
            Exit Function
        End If
        '   /* Perform Choleski decomposition */
        For j = 0 To n - 1
            For k = 0 To j - 1
                mat(j, j) = mat(j, j) - mat(j, k) * mat(j, k)
            Next k
            '
            If mat(j, j) < 0# Then
                Dinvert = 3
                Exit Function
            End If
            mat(j, j) = Math.Sqrt(mat(j, j))
            '
            For i = j + 1 To n - 1
                For k = 0 To j - 1
                    mat(i, j) = mat(i, j) - mat(i, k) * mat(j, k)
                Next k
                '
                If Math.Abs(mat(j, j)) < 0.000000000001 Then
                    Dinvert = 4
                    Exit Function
                End If
                mat(i, j) = mat(i, j) / mat(j, j)
            Next i
        Next j
        '
        '   /* Inversion of lower trianglar matrix */
        For j = 0 To n - 1
            mat(j, j) = 1.0# / mat(j, j)
            For i = j + 1 To n - 1
                mat(i, j) = -mat(i, j) * mat(j, j) / mat(i, i)
                For k = j + 1 To i - 1
                    mat(i, j) = mat(i, j) - mat(i, k) * mat(k, j) / mat(i, i)
                Next k
            Next i
        Next j
        '
        '   /* Construction of lower trianglar inverse matrix */
        For j = 0 To n - 1
            For i = j To n - 1
                mat(i, j) = mat(i, i) * mat(i, j)
                For k = i + 1 To n - 1
                    mat(i, j) = mat(i, j) + mat(k, i) * mat(k, j)
                Next k
            Next i
        Next j
        '
        '   /* fill upper diagonal */
        For i = 1 To n - 1
            For j = 0 To i - 1
                mat(j, i) = mat(i, j)
            Next j
        Next i
        Dinvert = 0
        '}
    End Function
    ' End Invert


End Module
