Imports Microsoft.VisualBasic

Public Class Validation

    Public Sub ValidateBlankFieldsInTable(ByRef myTable As Table, ByVal MaxFieldName As String, ByVal ObtainedFieldName As String)
        'In case any field is blank, conver it to 00 and if required calculate grade also
        Dim i As Integer = 0
        For i = 1 To myTable.Rows.Count - 1
            Dim MM As String = CType(myTable.FindControl(MaxFieldName & i), TextBox).Text
            If MM.Length <= 0 Or IsDBNull(MM) Or Trim(MM) = "" Then
                CType(myTable.FindControl(MaxFieldName & i), TextBox).Text = "00"
            End If
            Dim MF As String = CType(myTable.FindControl(ObtainedFieldName & i), TextBox).Text
            If MF.Length <= 0 Or IsDBNull(MF) Or Trim(MF) = "" Then
                CType(myTable.FindControl(ObtainedFieldName & i), TextBox).Text = "00"
            End If
        Next
    End Sub

    Private Function ValidateMarksAgainstMaxMarks(ByRef myTable As Table, ByVal MaxFieldName As String, ByVal ObtainedFieldName As String) As Integer
        Dim i As Integer = 0
        Dim rv As Integer = -1 '-1: All Fine, >=0: Something Wrong

        For i = 1 To myTable.Rows.Count - 1
            Dim MM As Double = Val(CType(myTable.FindControl(MaxFieldName & i), TextBox).Text)
            Dim MF As Double = Val(CType(myTable.FindControl(ObtainedFieldName & i), TextBox).Text)
            If MF > MM Then
                rv = i
                Exit For
            End If
        Next

        Return rv
    End Function

End Class
