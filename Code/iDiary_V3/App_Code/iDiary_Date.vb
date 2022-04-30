Imports Microsoft.VisualBasic

Namespace iDiary_Date

    Public Class CLS_iDiary_Date

        Public Shared rs(100) As String
        Public Shared count As Integer

        Public Shared Function ConvertDateToWords(dd As Integer, mm As Integer, yyyy As Integer) As String
            Dim strDateArray(30) As String
            Dim intDay As Integer
            Dim intMonth As Integer
            Dim intyear As Integer
            Dim strDate As String

            strDateArray(0) = "First"
            strDateArray(1) = "Second"
            strDateArray(2) = "Third"
            strDateArray(3) = "Fourth"
            strDateArray(4) = "Fifth"
            strDateArray(5) = "Sixth"
            strDateArray(6) = "Seventh"
            strDateArray(7) = "Eighth"
            strDateArray(8) = "Ninth"
            strDateArray(9) = "Tenth"
            strDateArray(10) = "Eleventh"
            strDateArray(11) = "Twelfth"
            strDateArray(12) = "Thirteenth"
            strDateArray(13) = "Fourteenth"
            strDateArray(14) = "Fifteenth"
            strDateArray(15) = "Sixteenth"
            strDateArray(16) = "Seventeenth"
            strDateArray(17) = "Eighteenth"
            strDateArray(18) = "Nineteenth"
            strDateArray(19) = "Twentieth"
            strDateArray(20) = "Twenty-First"
            strDateArray(21) = "Twenty-Second"
            strDateArray(22) = "Twenty-Third"
            strDateArray(23) = "Twenty-Fourth"
            strDateArray(24) = "Twenty-Fifth"
            strDateArray(25) = "Twenty-Sixth"
            strDateArray(26) = "Twenty-Seventh"
            strDateArray(27) = "Twenty-Eighth"
            strDateArray(28) = "Twenty-Ninth"
            strDateArray(29) = "Thirtieth"
            strDateArray(30) = "Thirty-First"

            intDay = dd
            intMonth = mm
            intyear = yyyy

            strDate = ""
            strDate = strDateArray(intDay - 1)
            strDate = strDate & " " & MonthName(intMonth, False)

            If intyear < 2000 Then
                strDate = strDate & " " & ConvertWord(Mid(CStr(intyear), 1, 2))
                strDate = strDate & ConvertWord(Mid(CStr(intyear), 3, 2))
            Else
                strDate = strDate & " " & ConvertWord(CStr(intyear))
            End If
            Return strDate
        End Function

        Public Shared Function ConvertWord(ByVal WrdAmt As String) As String

            Dim strtemp As String
            Dim strtemp1 As String
            Dim NumInWords As String
            Dim intNumber As Integer
            NumInWords = ""
            intNumber = Len(WrdAmt)

            If count = 0 Then
                Call initArray()
                count = count + 1
            End If

            Select Case intNumber
                Case 9
                    strtemp = Mid$(WrdAmt, 1, 2)
                    strtemp1 = Mid$(WrdAmt, 3)
                    If Val(strtemp) <> 0 Then
                        NumInWords = rs(Val(strtemp)) & "Crore " & ConvertWord(strtemp1)
                    Else
                        NumInWords = ConvertWord(strtemp1)
                    End If
                Case 8
                    strtemp = Mid$(WrdAmt, 1, 1)
                    strtemp1 = Mid$(WrdAmt, 2)
                    If Val(strtemp) <> 0 Then
                        NumInWords = rs(Val(strtemp)) & "Crore " & ConvertWord(strtemp1)
                    Else
                        NumInWords = ConvertWord(strtemp1)
                    End If
                Case 7
                    strtemp = Mid$(WrdAmt, 1, 2)
                    strtemp1 = Mid$(WrdAmt, 3)
                    If Val(strtemp) <> 0 Then
                        NumInWords = rs(Val(strtemp)) & "Lakh " & ConvertWord(strtemp1)
                    Else
                        NumInWords = ConvertWord(strtemp1)
                    End If
                Case 6
                    strtemp = Mid$(WrdAmt, 1, 1)
                    strtemp1 = Mid$(WrdAmt, 2)
                    If Val(strtemp) <> 0 Then
                        NumInWords = rs(Val(strtemp)) & "Lakh " & ConvertWord(strtemp1)
                    Else
                        NumInWords = ConvertWord(strtemp1)
                    End If
                Case 5
                    strtemp = Mid$(WrdAmt, 1, 2)
                    strtemp1 = Mid$(WrdAmt, 3)
                    If Val(strtemp) <> 0 Then
                        NumInWords = rs(Val(strtemp)) & "Thousand " & ConvertWord(strtemp1)
                    Else
                        NumInWords = ConvertWord(strtemp1)
                    End If
                Case 4
                    strtemp = Mid$(WrdAmt, 1, 1)
                    strtemp1 = Mid$(WrdAmt, 2)
                    If Val(strtemp) <> 0 Then
                        NumInWords = rs(Val(strtemp)) & "Thousand " & ConvertWord(strtemp1)
                    Else
                        NumInWords = ConvertWord(strtemp1)
                    End If
                Case 3
                    strtemp = Mid$(WrdAmt, 1, 1)
                    strtemp1 = Mid$(WrdAmt, 2)
                    If Val(strtemp) <> 0 Then
                        NumInWords = rs(Val(strtemp)) & "Hundred " & ConvertWord(strtemp1)
                    Else
                        NumInWords = ConvertWord(strtemp1)
                    End If
                Case 2
                    strtemp = Mid$(WrdAmt, 1, 2)
                    If Val(strtemp) <> 0 Then
                        NumInWords = rs(Val(strtemp))
                    Else
                        NumInWords = ConvertWord(strtemp1)
                    End If
                Case 1
                    strtemp = Mid$(WrdAmt, 1)
                    If strtemp <> "0" Then NumInWords = rs(Val(strtemp))
                Case 0
                    Exit Function
            End Select
            ConvertWord = NumInWords
        End Function

        Public Shared Sub initArray()
            rs(1) = "One "
            rs(2) = "Two "
            rs(3) = "Three "
            rs(4) = "Four "
            rs(5) = "Five "
            rs(6) = "Six "
            rs(7) = "Seven "
            rs(8) = "Eight "
            rs(9) = "Nine "
            rs(10) = "Ten "
            rs(11) = "Eleven "
            rs(12) = "Twelve "
            rs(13) = "Thirteen "
            rs(14) = "Fourteen "
            rs(15) = "Fifteen "
            rs(16) = "Sixteen "
            rs(17) = "Seventeen "
            rs(18) = "Eighteen "
            rs(19) = "Nineteen "
            rs(20) = "Twenty "
            rs(21) = "Twenty One "
            rs(22) = "Twenty Two "
            rs(23) = "Twenty Three "
            rs(24) = "Twenty Four "
            rs(25) = "Twenty Five "
            rs(26) = "Twenty Six "
            rs(27) = "Twenty Seven "
            rs(28) = "Twenty Eight "
            rs(29) = "Twenty Nine "
            rs(30) = "Thirty "
            rs(31) = "Thirty One "
            rs(32) = "Thirty Two "
            rs(33) = "Thirty Three "
            rs(34) = "Thirty Four "
            rs(35) = "Thirty Five "
            rs(36) = "Thirty Six "
            rs(37) = "Thirty Seven "
            rs(38) = "Thirty Eight "
            rs(39) = "Thirty Nine "
            rs(40) = "Fourty "
            rs(41) = "Fourty One "
            rs(42) = "Fourty Two "
            rs(43) = "Fourty Three "
            rs(44) = "Fourty Four "
            rs(45) = "Fourty Five "
            rs(46) = "Fourty Six "
            rs(47) = "Fourty Seven "
            rs(48) = "Fourty Eight "
            rs(49) = "Fourty Nine "
            rs(50) = "Fifty "
            rs(51) = "Fifty One "
            rs(52) = "Fifty Two "
            rs(53) = "Fifty Three "
            rs(54) = "Fifty Four "
            rs(55) = "Fifty Five "
            rs(56) = "Fifty Six "
            rs(57) = "Fifty Seven "
            rs(58) = "Fifty Eight "
            rs(59) = "Fifty Nine "
            rs(60) = "Sixty "
            rs(61) = "Sixty One "
            rs(62) = "Sixty Two "
            rs(63) = "Sixty Three "
            rs(64) = "Sixty Four "
            rs(65) = "Sixty Five "
            rs(66) = "Sixty Six "
            rs(67) = "Sixty Seven "
            rs(68) = "Sixty Eight "
            rs(69) = "Sixty Nine "
            rs(70) = "Seventy "
            rs(71) = "Seventy One "
            rs(72) = "Seventy Two "
            rs(73) = "Seventy Three "
            rs(74) = "Seventy Four "
            rs(75) = "Seventy Five "
            rs(76) = "Seventy Six "
            rs(77) = "Seventy Seven "
            rs(78) = "Seventy Eight "
            rs(79) = "Seventy Nine "
            rs(80) = "Eighty "
            rs(81) = "Eighty One "
            rs(82) = "Eighty Two "
            rs(83) = "Eighty Three "
            rs(84) = "Eighty Four "
            rs(85) = "Eighty Five "
            rs(86) = "Eighty Six "
            rs(87) = "Eighty Seven "
            rs(88) = "Eighty Eight "
            rs(89) = "Eighty Nine "
            rs(90) = "Ninety "
            rs(91) = "Ninety One "
            rs(92) = "Ninety Two "
            rs(93) = "Ninety Three "
            rs(94) = "Ninety Four "
            rs(95) = "Ninety Five "
            rs(96) = "Ninety Six "
            rs(97) = "Ninety Seven "
            rs(98) = "Ninety Eight "
            rs(99) = "Ninety Nine "
        End Sub

        Public Shared Function getDateYYMMDD(ByVal dtString As String) As String
            ' input is in dd MM yyyy
            Dim dt As Date = Now.Date
            Try
                dt = New Date(dtString.Substring(6, 4), dtString.Substring(3, 2), dtString.Substring(0, 2))
            Catch ex As Exception

            End Try
            Return dt.ToString("yyyy-MM-dd")
        End Function
    End Class

End Namespace
