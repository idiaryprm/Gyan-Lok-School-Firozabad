Imports System.Data.SqlClient
Imports System.Data
Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI.Control
Imports iDiary_V3.iDiary.CLS_idiary
Namespace iDiary_Fee

    Public Class CLS_iDiary_Fee

        Public Shared Function AdmissionFeeApplicable(ByVal SID As Integer, ASID As Integer) As Boolean
            'Dim sqlStr As String = "Select Count(*) From FeeDuesAdmission Where SID= " & SID & " AND ASID=" & ASID
            Dim sqlStr As String = "Select Count(*) From vw_student Where  SID=" & SID & " and AdmissionDate between (Select DateAdmissionStart from AcademicSession where ASID=" & ASID & ") and (Select DateAdmissionEnd from AcademicSession where ASID=" & ASID & ")"
            Dim rv As Integer = ExecuteQuery_ExecuteScalar(sqlStr)
            If rv = 0 Then
                Return False
            Else
                Return True
            End If

        End Function
        Public Shared Function GetBusFeeID(SID As Integer) As String
            Dim rv As String = 0

            Dim SqlStr As String = "Select BusFeeID From BusFeeDeposite Where SID=" & SID
            rv = ExecuteQuery_ExecuteScalar(SqlStr)
            Return rv
        End Function
        Public Shared Function GetBusActualAmt(SID As Integer, TermNo As Integer) As Double

            Dim rv As Double = 0

            Dim SqlStr As String = "Select Amount From BusStudentMap Where SID=" & SID & " and TermNo=" & TermNo
            rv = ExecuteQuery_ExecuteScalar(SqlStr)
            Return rv
        End Function
        Public Shared Function GetLocationName(SID As Integer, TermNo As Integer) As String
            Dim rv As String = ""
            Dim SqlStr As String = "Select LocationID From BusStudentMap Where SID=" & SID & "  and busrequired=1 and TermNo=" & TermNo
            Dim LocationId As Integer = ExecuteQuery_ExecuteScalar(SqlStr)
            SqlStr = "Select LocationName From LocationMaster Where LocationID=" & LocationId
            Dim LocationName As String = ExecuteQuery_ExecuteScalar(SqlStr)
            rv = LocationName
            Return rv
        End Function
        Public Shared Function GetBusDueAmountForTerm(ByVal ASID As Integer, TermID As Integer, ByVal SID As String, Optional tmpDepositDate As String = "") As Double
            Dim IsBusRequired As Integer = 1

            'Dim LateFeeID As Integer = GetLateFeeIDForTerm(ASID, TermNo)
            Dim DepositDate As Date = Now.Date
            If tmpDepositDate <> "" Then
                DepositDate = tmpDepositDate
            End If
            Dim sqlStr As String = ""
            Dim DueAmount As Double = 0
            sqlStr = " Select BusRequired From BusStudentMap Where SID=" & SID & " and TermNo =" & TermID & ""
            Try
                IsBusRequired = ExecuteQuery_ExecuteScalar(sqlStr)
            Catch ex As Exception

            End Try
            If IsBusRequired = 0 Then
                GoTo 1
            End If

            sqlStr = " Select DepositeDate From vw_BusFee Where SID=" & SID & " and BusTermID =" & TermID & ""
            Dim DateReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While DateReader.Read
                DepositDate = DateReader(0)
            End While
            DateReader.Close()

            'If LateFeeID <= 0 Then
            '    DueAmount = -1
            'Else
            'Get Late Fee Amount for FeeBook No
            Dim LastDate As Date = DepositDate
            Dim TmpLastDate As String = LastDate.ToString("yyyy/MM/dd")
            Dim LateFeeAmount As Double = 0, ProcessingMethod As Integer = 0

            sqlStr = "Select Top (1) LastDate, LateFeeAmount,  ProcessingMethod From BusFeeDueConfig Where TermNo=" & TermID & " And LastDate<='" & TmpLastDate & "' AND ASID=" & ASID & " order by LastDate desc"
            Dim DueConfigReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While DueConfigReader.Read
                LastDate = CDate(DueConfigReader(0))
                LateFeeAmount = DueConfigReader(1)
                'LateFeeType = DueConfigReader(2)
                ProcessingMethod = DueConfigReader(2)
            End While
            DueConfigReader.Close()
            If ProcessingMethod = 1 Then  'Monthly

                Dim TimeDiff As New TimeSpan
                TimeDiff = LastDate - Now
                Dim DiffDays As Integer = Math.Abs(TimeDiff.Days)
                Dim Months As Double = Convert.ToDouble(DiffDays) / 30
                DueAmount = LateFeeAmount * Math.Ceiling(Months)
            ElseIf ProcessingMethod = 2 Then  'Daily
                'Calculate difference between current date and last date
                Dim TimeDiff As New TimeSpan
                TimeDiff = LastDate - Now
                Dim DiffDays As Integer = Math.Abs(TimeDiff.Days)
                DueAmount = (LateFeeAmount * DiffDays)
            Else  'Fix
                DueAmount = LateFeeAmount
            End If


            'Try
            '    DueAmount = ExecuteQuery_ExecuteScalar(SqlStr)
            'Catch ex As Exception
            '    DueAmount = 0
            'End Try
            'End If
1:
            Return DueAmount

        End Function
        
        Public Shared Function LoadFeeTypes(ByRef myChk As CheckBoxList) As Integer

            Dim sqlStr As String = ""

            sqlStr = "Select FeeTypeName From FeeTypes where Concession=1 order by FeeOrder"

            myChk.Items.Clear()
            'myChk.Items.Add("")
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                myChk.Items.Add(myReader("FeeTypeName"))
                'lblTerm.Text = myReader("TermName")
            End While
            myReader.Close()
            Return 0
        End Function
        Public Shared Function LoadFeeTerms(ByRef myChk As CheckBoxList, FeeGroupID As Integer, Optional BusFee As String = "") As String
            Dim sqlStr As String = ""

            Dim TermID As String = ""

            If BusFee = "" Then
                If FeeGroupID > 0 Then
                    sqlStr = "Select TermID,TermNo From TermMaster Where FeeGroupID=" & FeeGroupID & " order by DisplayOrder"
                Else
                    sqlStr = "Select Distinct TermNo,DisplayOrder From TermMaster order by DisplayOrder"
                End If

                myChk.Items.Clear()
                'myChk.Items.Add("")
                Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
                While myReader.Read
                    If FeeGroupID > 0 Then
                        myChk.Items.Add(myReader("TermNo"))
                        TermID += myReader("TermID") & ","
                    Else
                        myChk.Items.Add(myReader("TermNo"))
                    End If
                End While
                myReader.Close()
                If TermID <> "" Then
                    TermID = TermID.Substring(0, TermID.Length - 1)
                End If
            Else
                sqlStr = "Select BusTermID,BusTermNo From BusTermMaster order by DispOrder"
                myChk.Items.Clear()
                'myChk.Items.Add("")
                Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
                While myReader.Read
                    If FeeGroupID > 0 Then
                        myChk.Items.Add(myReader("BusTermNo"))
                        TermID += myReader("BusTermID") & ","
                    Else
                        myChk.Items.Add(myReader("BusTermNo"))
                    End If
                End While
                myReader.Close()
                If TermID <> "" Then
                    TermID = TermID.Substring(0, TermID.Length - 1)
                End If
            End If
            Return TermID
        End Function
        Public Shared Function GetBusCMNO(ByVal ASID As Integer, ByVal Dated As String) As Integer

            Dim rv As Integer = 0
            Dim SqlStr As String = ""
            SqlStr = "Select max(CMNO) From BusFeeDeposite Where " & _
         "ASID=" & ASID & " AND DepositDate<='" & Dated & "'"
            Try
                rv = ExecuteQuery_ExecuteScalar(SqlStr)
            Catch ex As Exception

            End Try
            SqlStr = "update BusFeeDeposite set CMNO=CMNO+1 Where CMNO>" & rv
            ExecuteQuery_Update(SqlStr)
            rv = rv + 1
            Return rv
        End Function
        Public Shared Function GetCMNO(ByVal ASID As Integer, ByVal Dated As String, ClassGroupID As Integer) As Integer
            Dim rv As Integer = 0
            Dim SqlStr As String = ""
            SqlStr = "Select max(CMNO) From FeeDeposit Where " & _
         "ASID=" & ASID & " AND ClassGroupID=" & ClassGroupID
            'DepositDate<='" & Dated & "'"
            Try
                rv = ExecuteQuery_ExecuteScalar(SqlStr)
            Catch ex As Exception

            End Try
            'SqlStr = "update FeeDeposit set CMNO=CMNO+1 Where CMNO>" & rv
            'ExecuteQuery_Update(SqlStr)
            rv = rv + 1
            Return rv
        End Function
        Public Shared Function LoadFeeTerms(ByRef myChk As DropDownList, FeeGroupID As Integer, Optional BusFee As String = "") As Integer

            Dim sqlStr As String = ""
            If BusFee = "" Then
                sqlStr = "Select TermNo From TermMaster Where FeeGroupID=" & FeeGroupID & " order by DisplayOrder"
                myChk.Items.Clear()
                myChk.Items.Add("")
                Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
                While myReader.Read
                    myChk.Items.Add(myReader("TermNo"))
                    'lblTerm.Text = myReader("TermName")
                End While
                myReader.Close()
            Else
                sqlStr = "Select BusTermID,BusTermNo From BusTermMaster order by DispOrder"


                myChk.Items.Clear()
                myChk.Items.Add("")
                Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
                While myReader.Read
                    'myChk.Items.Add(myReader("BusTermNo"), )
                    myChk.Items.Add(New ListItem(myReader("BusTermNo"), myReader("BusTermID")))
                    'lblTerm.Text = myReader("TermName")
                End While
                myReader.Close()
            End If




            Return 0
        End Function

        Public Shared Function GetLocationID(LocationName As String) As Integer



            Dim rv As Integer = 0
            Dim sqlStr As String = "Select LocationID From LocationMaster Where LocationName='" & LocationName & "'"



            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                Try
                    rv = myReader(0)
                Catch ex As Exception

                End Try

            End While
            myReader.Close()


            Return rv
        End Function
        Public Shared Function GetStudentLocationID(SID As Integer) As Integer



            Dim rv As Integer = 0
            Dim sqlStr As String = "Select LocationID From BusStudentMap Where SID='" & SID & "'"



            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                Try
                    rv = myReader(0)
                Catch ex As Exception

                End Try

            End While
            myReader.Close()


            Return rv
        End Function
        Public Shared Function GetStudentLocationAmount(LocationID As Integer) As Integer



            Dim rv As Integer = 0
            Dim sqlStr As String = "Select BusFee From LocationMaster Where LocationID='" & LocationID & "'"



            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                Try
                    rv = myReader(0)
                Catch ex As Exception

                End Try

            End While
            myReader.Close()


            Return rv
        End Function

        Public Shared Function CheckBusFeeDepositExistance(ByVal ASID As Integer, ByVal SID As String, ByVal TermNo As Integer) As Boolean





            Dim rv As Boolean



            Dim SqlStr As String = "Select Count(*) From vw_BusFee Where " & _
            "ASID=" & ASID & " AND SID='" & SID & "' AND TermNo=" & TermNo & " AND IsCancel=0"




            Dim FeeDepositExist As Integer = ExecuteQuery_ExecuteScalar(SqlStr)




            If FeeDepositExist <= 0 Then
                rv = False
            Else
                rv = True
            End If

            Return rv
        End Function

        Public Shared Function LoadFeeConcessionTypeMaster(ByRef myCbo As DropDownList) As Integer






            Dim sqlStr As String = "Select FCTypeName From FeeConcessionTypeMaster"


            Dim MyReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            myCbo.Items.Clear()
            myCbo.Items.Add("")
            While MyReader.Read
                myCbo.Items.Add(MyReader(0))
            End While
            MyReader.Close()





            Return 0
        End Function

        Public Shared Function LoadFeeConcessionTypeMaster(ByRef myLst As ListBox) As Integer






            Dim sqlStr As String = "Select FCTypeName From FeeConcessionTypeMaster"


            Dim MyReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            myLst.Items.Clear()

            While MyReader.Read
                myLst.Items.Add(MyReader(0))
            End While
            MyReader.Close()





            Return 0
        End Function

        Public Shared Function FindFeeConcessionTypeMasterID(ByRef FeeConcessionTypeName As String) As Integer






            Dim sqlStr As String = "Select FCTID From FeeConcessionTypeMaster Where FCTypeName='" & FeeConcessionTypeName & "'"


            Dim rv As Integer = 0
            Try
                rv = ExecuteQuery_ExecuteScalar(SqlStr)
            Catch ex As Exception
                rv = 0
            End Try



            Return rv
        End Function

        Public Shared Function ConcessionAllowed(ByVal FeeTypeID As Integer) As Boolean





            Dim sqlStr As String = "Select MAX(Concession) from FeeTypes Where FeeTypeID=" & FeeTypeID


            Dim rv As Boolean = ExecuteQuery_ExecuteScalar(SqlStr)



            Return rv
        End Function

        Public Shared Function TotalFeeTerms(Optional BusFee As String = "") As Integer
            Dim sqlStr As String = ""
            If BusFee = "" Then
                sqlStr = "Select Max(FeeTerms) From Params"
            Else
                sqlStr = "Select Max(BusFeeTerms) From Params"
            End If

            Dim FeeTerms As Integer = 0
            Try
                FeeTerms = ExecuteQuery_ExecuteScalar(SqlStr)
            Catch ex As Exception
                FeeTerms = 0
            End Try
            Return FeeTerms

        End Function

        Public Shared Function LoadFeeTermCaption(ByVal TermID As Integer, Optional BusFee As String = "") As String
            Dim rv As String = ""




            Dim sqlStr As String = ""
            If BusFee = "" Then
                sqlStr = "Select TermName From TermMaster where TermID ='" & TermID & "'"
            Else
                sqlStr = "Select BusTermName From BusTermMaster where BusTermID ='" & TermID & "'"
            End If

            ' and FeeGroupID=" & FeeGroupID




            rv = ExecuteQuery_ExecuteScalar(SqlStr)




            Return rv
        End Function
        Public Shared Function LoadFeeTermCaptionTmp(ByVal TermNo As Integer) As String
            Dim rv As String = ""
            'Need to Change as per school details
            If TotalFeeTerms("BusFee") = 4 Then

                Select Case TermNo
                    Case 1 : rv = "APR-JUN"
                    Case 2 : rv = "JUL-SEP"
                    Case 3 : rv = "OCT-DEC"
                    Case 4 : rv = "JAN-MAR"
                    Case Else : rv = ""
                End Select

            ElseIf TotalFeeTerms("BusFee") = 12 Then

                Select Case TermNo
                    Case 1 : rv = "APR"
                    Case 2 : rv = "MAY"
                    Case 3 : rv = "JUNE"
                    Case 4 : rv = "JULY"
                    Case 5 : rv = "AUG"
                    Case 6 : rv = "SEP"
                    Case 7 : rv = "OCT"
                    Case 8 : rv = "NOV"
                    Case 9 : rv = "DEC"
                    Case 10 : rv = "JAN"
                    Case 11 : rv = "FEB"
                    Case 12 : rv = "MAR"
                    Case Else : rv = ""
                End Select
            ElseIf TotalFeeTerms("BusFee") = 3 Then

                Select Case TermNo
                    Case 1 : rv = "July"
                    Case 2 : rv = "Sep"
                    Case 3 : rv = "Dec"
                    Case Else : rv = ""
                End Select

            Else
                rv = "ERROR"
            End If

            Return rv
        End Function
        Public Shared Function LoadTermID(ByVal TermNo As String, FeeGroupID As Integer) As Integer

            Dim sqlStr As String = "Select TermId From TermMaster where TermNo= '" & TermNo & "' and FeeGroupID=" & FeeGroupID




            Dim TermID As Integer = 0




            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                TermID = myReader("TermId")
            End While
            myReader.Close()


            Return TermID

        End Function
        Public Shared Function GetFeeTypeName(FeeTypeID As Integer) As String

            Dim sqlStr As String = "Select FeeTypeName From FeeTypes where FeeTypeID= '" & FeeTypeID & "'"




            Dim rv As String = ""



            Try
                rv = ExecuteQuery_ExecuteScalar(SqlStr)
            Catch ex As Exception

            End Try



            Return rv

        End Function

        Public Shared Function LoadFeeDepositHistory(ByVal SID As Integer, ByVal ASID As Integer, ByRef myCbo As DropDownList) As Integer






            Dim sqlStr As String = "Select FeeDepositID From FeeDeposit Where SID=" & SID & " AND ASID=" & ASID & " Order By FeeDepositID"



            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)

            myCbo.Items.Clear()
            myCbo.Items.Add("")

            While myReader.Read
                myCbo.Items.Add(myReader(0))
            End While
            myReader.Close()




            Return 0
        End Function

        Public Shared Function GetBusLocationAmt(ByVal ASID As Integer, SID As Integer, TermCounter As Integer) As String





            Dim rv As String = ""



            Dim SqlStr As String = "Select LocationID From BusStudentMap Where SID=" & SID



            Dim LocationId As Integer = ExecuteQuery_ExecuteScalar(SqlStr)


            SqlStr = "Select LocationName From LocationMaster Where LocationID=" & LocationId



            Dim LocationName As String = ExecuteQuery_ExecuteScalar(SqlStr)

            SqlStr = "Select BusFee From LocationMaster Where locationID=" & LocationId

            ' 

            rv = LocationName & "-" & (ExecuteQuery_ExecuteScalar(SqlStr) * TermCounter)







            Return rv
        End Function

        Public Shared Function LoadPreviousPaymentHistory(ByRef myCbo As DropDownList, ByVal SID As Integer, ByVal ASID As Integer, Optional BusFee As String = "") As Integer





            Dim sqlStr As String = "Select FeeDepositID From FeeDeposit Where SID=" & SID & " AND ASID=" & ASID & " Order By FeeDepositID DESC"
            If BusFee <> "" Then
                sqlStr = "Select BusFeeID From BusFeeDeposite Where SID=" & SID & " Order By BusFeeID DESC"
            End If


            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)

            myCbo.Items.Clear()
            myCbo.Items.Add("")

            While myReader.Read
                myCbo.Items.Add(myReader(0))
            End While
            myReader.Close()




            Return 0

        End Function

        'Public Shared Function ProcessFeeCollectionReport(ByVal FromDate As String, ByVal ToDate As String, ByVal ASID As Integer) As Integer
        '    Dim sqlStr As String = ""

        '    myCommand.CommandText = "Delete From RptFeeCollection"

        '    ExecuteQuery_Update(SqlStr)

        '    'Retrieve Concession Fee Type
        '    Dim ConcessionFeeTypeID As Integer = 0, ConcessionFeeName As String = ""
        '    sqlStr = "Select * From vwFeeConcessionConfig"


        '    Dim ConfigReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        '    While ConfigReader.Read
        '        ConcessionFeeTypeID = ConfigReader(0)
        '        ConcessionFeeName = ConfigReader(1)
        '    End While
        '    ConfigReader.Close()

        '    sqlStr = "Insert into rptFeeCollection (FeeDepositDate, SID, FeeBookNo, SName, ClassName, SecName, FeeAmount,FeeDepositID) " & _
        '    " Select  DepositDate, SID, FeeBookNo, SName, ClassName, SecName, Sum(FeeDepositAmount),FeeDepositID From vw_FeeDeposit " & _
        '    " Where isDeposit=1 AND DepositDate Between '" & FromDate & "' AND '" & ToDate & "' " & _
        '    " AND ASID=" & ASID & " AND FeeTypeName<>'" & ConcessionFeeName & "' " & _
        '    " Group By DepositDate, [SID], FeeBookNo, SName, ClassName, SecName,FeeDepositID"



        '    ExecuteQuery_Update(SqlStr)



        '    Return 0
        'End Function

        'Public Shared Function ProcessHeadwiseReport(ByVal FromDate As String, ByVal ToDate As String, ByVal ASID As Integer) As String

        '    Dim sqlStr As String = ""
        '    Dim i As Integer = 0
        '    Dim LstFeeType As New ListBox

        '    sqlStr = "Select FeetypeName From FeeTypes Order By FeeTypeName"



        '    Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        '    While myReader.Read
        '        LstFeeType.Items.Add(myReader(0))
        '    End While
        '    myReader.Close()

        '    sqlStr = "Select CONVERT(VARCHAR(10), xx.DepositDate, 103) AS [Deposit Date],"

        '    For i = 0 To LstFeeType.Items.Count - 1
        '        sqlStr &= "(SELECT SUM(vw_FeeDeposit.FeeDepositAmount) FROM vw_FeeDeposit, FeeTypes " & _
        '                    " WHERE vw_FeeDeposit.FeeTypeName ='" & LstFeeType.Items(i).Text & "' " & _
        '                    " AND vw_FeeDeposit.FeeTypeName=FeeTypes.FeeTypeName " & _
        '                    " AND vw_FeeDeposit.DepositDate=xx.DepositDate " & _
        '                    " AND ASID=" & ASID & ") AS [" & LstFeeType.Items(i).Text & "],"
        '    Next

        '    sqlStr = sqlStr.Substring(0, sqlStr.Length - 1)
        '    sqlStr &= " FROM vw_FeeDeposit xx " & _
        '    " Where DepositDate BETWEEN '" & FromDate & "' and '" & ToDate & "' " & _
        '    " GROUP BY xx.DepositDate"




        '    Return sqlStr
        'End Function

        'Public Shared Function PrepareConcessionReport(ByVal ASID As Integer) As Integer

        '    Dim sqlStr As String = ""






        '    sqlStr = "Delete from rptConcession"


        '    ExecuteQuery_Update(SqlStr)

        '    'Retrieve Concession Fee Type (Given Additionally During Fee Deposit)
        '    Dim ConcessionFeeTypeID As Integer = 0, ConcessionFeeName As String = ""
        '    sqlStr = "Select * From vwFeeConcessionConfig"


        '    Dim ConfigReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        '    While ConfigReader.Read
        '        ConcessionFeeTypeID = ConfigReader(0)
        '        ConcessionFeeName = ConfigReader(1)
        '    End While
        '    ConfigReader.Close()

        '    sqlStr = "Insert into rptConcession (RegNo, FeeBookNo, SName, FName, ClassName, SecName, TermNo, ConcessionDate, Amount, Remark) "
        '    sqlStr &= "Select RegNo, FeeBookNo, SName, FName, ClassName, SecName, TermNo, DepositDate, Abs(FeeDepositAmount), FeeTypeName From vw_FeeDeposit Where FeeTypeName='" & ConcessionFeeName & "' AND ASID=" & ASID


        '    ExecuteQuery_Update(SqlStr)

        '    sqlStr = "Update rptConcession Set Remark='Concessioned Amount '"


        '    ExecuteQuery_Update(SqlStr)

        '    sqlStr = "Delete From rptConcession Where Amount<=0"


        '    ExecuteQuery_Update(SqlStr)




        '    Return 0
        'End Function
        Public Shared Function GetFeeGroupID(ByVal ClassName As String) As Integer

            Dim sqlStr As String = "Select FeeGroupID From Classes where ClassName='" & ClassName & "'"









            Dim myID As Integer = 0
            Try
                myID = ExecuteQuery_ExecuteScalar(sqlStr)
            Catch ex As Exception
                myID = 0
            End Try

            Return myID

        End Function
        Public Shared Function GetMonthID(ByVal FeeGroupID As Integer, ByVal TermID As Integer, Optional TermsNos As String = "") As String

            Dim sqlStr As String = "Select MonthID From TermMaster where FeeGroupID='" & FeeGroupID & "' and TermID=" & TermID

            If TermsNos <> "" Then
                sqlStr = "Select MonthID From TermMaster where FeeGroupID='" & FeeGroupID & "' and TermNo in (" & TermsNos & ")"
            End If

            Dim myID As String = ""
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                myID += myReader(0) & ","
            End While
            myReader.Close()
            If myID = "" Then
                myID = 0
            Else
                myID = myID.Substring(0, myID.Length - 1)
            End If



            Return myID

        End Function


        Public Shared Function CheckChallanIDExistance(ByVal ChallanID As String) As String





            Dim rv As String = ""
            Dim FeeDepositExist As Integer = -1



            Dim SqlStr As String = "Select isDeposit From FeeChallan Where FeeChallanID=" & ChallanID




            Try
                FeeDepositExist = ExecuteQuery_ExecuteScalar(SqlStr)
            Catch ex As Exception

            End Try




            If FeeDepositExist = -1 Then
                Return "Challan ID is Invalid"
            ElseIf FeeDepositExist = 0 Then
                Return ""
            Else
                Return "Challan Allready Deposited"
            End If
        End Function
        Public Shared Function CheckFeeDepositExistance(ByVal ASID As Integer, ByVal FeeBookNo As String, ByVal TermID As String) As Boolean
            Dim rv As Boolean

            Dim SqlStr As String = "Select Count(*) From vw_FeeDeposit Where " & _
            "ASID=" & ASID & " AND FeeBookNo='" & FeeBookNo & "' AND TermID in (" & TermID & ")"
            Dim FeeDepositExist As Integer = 0
            Try
                FeeDepositExist = ExecuteQuery_ExecuteScalar(SqlStr)
            Catch ex As Exception

            End Try
            If FeeDepositExist <= 0 Then
                rv = False
            Else
                rv = True
            End If

            Return rv
        End Function
        Public Shared Function GetFeeConfigForFeeHead(ByVal ASID As Integer, FeeGroupID As Integer, ByVal FeeTypeID As Integer, ByVal MonthID As String, Optional PartofDueProcess As String = "", Optional sid As Integer = 0) As Double

            Dim SqlStr As String = ""

            SqlStr = "Select sum(FeeAmount) From vw_FeeConfig Where ASID=" & ASID & " AND "
            If FeeGroupID > 0 Then
                SqlStr += " FeeGroupID=" & FeeGroupID
            Else
                SqlStr += " SID=" & sid
            End If
            If FeeTypeID > 0 Then
                SqlStr += " AND FeeTypeID=" & FeeTypeID
            End If
            If MonthID <> "" Then
                SqlStr += " AND MonthID in (" & MonthID & ")"
            End If
            If PartofDueProcess <> "" Then
                SqlStr += " AND PartOfDueProcess=1"
            End If


            Dim myFeeAmount As Double = 0
            Try
                myFeeAmount = ExecuteQuery_ExecuteScalar(SqlStr)
            Catch ex As Exception
                myFeeAmount = 0
            End Try




            Return myFeeAmount

        End Function
        Public Shared Function GetFeeDepositeForStudent(ByVal SID As Integer, ByVal FeeTypeID As Integer, TermNo As String, Optional ExcessFeeID As String = "", Optional IDTYpe As String = "") As String

            Dim SqlStr As String = ""
            If IDTYpe <> "" Then
                SqlStr = "Select FeeDepositAmount, ConcessionAmount From vw_FeeDeposit Where SID=" & SID & " and TermNO in (" & TermNo & ")"
            Else
                SqlStr = "Select FeeDepositAmount, ConcessionAmount From vw_FeeDeposit Where SID=" & SID & " and TermID in (" & TermNo & ")"
            End If

            If FeeTypeID > 0 Then
                SqlStr += " and FeeTypeID=" & FeeTypeID & ""
            End If

            Dim myFeeAmount As Double = 0
            Dim ExecccFeeAmount As Double = 0
            Dim myConcessionAmount As Double = 0
            Dim myreader As SqlDataReader = ExecuteQuery_ExecuteReader(SqlStr)
            While myreader.Read

                Try
                    myFeeAmount += myreader(0)
                    If myreader(0) < 0 Then
                        myConcessionAmount += myreader(0) * -1
                    End If

                Catch ex As Exception

                End Try
                Try
                    myConcessionAmount += myreader(1)
                Catch ex As Exception

                End Try
            End While
            myreader.Close()

            If ExcessFeeID <> "" Then
                If IDTYpe <> "" Then
                    SqlStr = "Select sum(FeeDepositAmount) From vw_FeeDeposit Where SID=" & SID & " and TermNo in (" & TermNo & ")"
                Else
                    SqlStr = "Select sum(FeeDepositAmount) From vw_FeeDeposit Where SID=" & SID & " and TermID in (" & TermNo & ")"
                End If

                SqlStr += " and FeeTypeID=" & ExcessFeeID & ""
                Try
                    ExecccFeeAmount = ExecuteQuery_ExecuteScalar(SqlStr)
                Catch ex As Exception

                End Try
            End If

            Return myFeeAmount & "/" & myConcessionAmount & "/" & ExecccFeeAmount

        End Function


        'Public Shared Function GetFeeDepositeForStudent(ByVal SID As Integer, ByVal FeeTypeID As Integer, TermNo As String, Optional ExcessFeeID As String = "") As String

        '    Dim SqlStr As String = ""

        '    SqlStr = "Select FeeDepositAmount, ConcessionAmount From vw_FeeDeposit Where SID=" & SID & " and TermNo in (" & TermNo & ")"
        '    If FeeTypeID > 0 Then
        '        SqlStr += " and FeeTypeID=" & FeeTypeID & ""
        '    End If

        '    Dim myFeeAmount As Double = 0
        '    Dim ExecccFeeAmount As Double = 0
        '    Dim myConcessionAmount As Double = 0
        '    Dim myreader As SqlDataReader = ExecuteQuery_ExecuteReader(SqlStr)
        '    While myreader.Read

        '        Try
        '            myFeeAmount += myreader(0)
        '            If myreader(0) < 0 Then
        '                myConcessionAmount += myreader(0) * -1
        '            End If

        '        Catch ex As Exception

        '        End Try
        '        Try
        '            myConcessionAmount += myreader(1)
        '        Catch ex As Exception

        '        End Try
        '    End While
        '    myreader.Close()

        '    If ExcessFeeID <> "" Then

        '        SqlStr = "Select sum(FeeDepositAmount) From vw_FeeDeposit Where SID=" & SID & " and TermNo in (" & TermNo & ")"
        '        SqlStr += " and FeeTypeID=" & ExcessFeeID & ""
        '        Try
        '            ExecccFeeAmount = ExecuteQuery_ExecuteScalar(SqlStr)
        '        Catch ex As Exception

        '        End Try
        '    End If

        '    Return myFeeAmount & "/" & myConcessionAmount & "/" & ExecccFeeAmount

        'End Function

        Public Shared Function CheckStudentConfigtmp(SID As Integer) As Boolean

            Dim SqlStr As String = ""

            SqlStr = "Select Count(*) From FeeConfig Where SID=" & SID



            Dim rv As Integer = 0
            Try
                rv = ExecuteQuery_ExecuteScalar(SqlStr)
            Catch ex As Exception
                rv = 0
            End Try



            If rv = 0 Then
                Return False
            Else
                Return True
            End If
            'Return rv

        End Function

        Public Shared Function GetTallyLedgerHead(ByVal FeeID As Integer) As String

            Dim SqlStr As String = ""
            Dim rv As String = ""

            SqlStr = "Select TallyHeadName From FeeTypes Where FeeTypeID=" & FeeID

            Try
                rv = ExecuteQuery_ExecuteScalar(SqlStr)
            Catch ex As Exception

            End Try

            Return rv

        End Function
        Public Shared Function GetConcessionEntry(ByVal SID As Integer) As Integer

            Dim rv As Integer = 0
            Dim SqlStr As String = ""

            SqlStr = "Select Count(*) From FeeStudentConcession Where SID=" & SID

            rv = ExecuteQuery_ExecuteScalar(SqlStr)

            Return rv
        End Function
        Public Shared Function GetConcessionCheck(ByVal ConcessionHeadName As String, ByVal FeeTypeID As Integer, ByVal myFeeAmount As Double) As Double
            Dim SqlStr As String = ""
            Dim FCTypeAmount As Double = 0
            Dim FCFeeType As String = ""
            Dim FCType As Integer = 0
            Dim i As Integer = 0

            SqlStr = "Select FCTypeAmount, FCFeeType, FCType From FeeConcessionTypeMaster Where  FCTypeName='" & ConcessionHeadName & "'"

            Dim ConcReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While ConcReader.Read
                Try
                    FCTypeAmount = ConcReader(0)
                    FCFeeType = ConcReader(1)
                    FCType = ConcReader(2)
                Catch ex As Exception

                End Try
            End While
            ConcReader.Close()
            Dim ConcessionAmount As Double = 0

            If FCFeeType <> "" Then
                Dim FeeTypeList() As String = FCFeeType.Split(",")
                For i = 0 To FeeTypeList.Count - 1
                    If FeeTypeID = FeeTypeList(i).ToString Then  'For selected fee type concession is allowed
                        If FCType = 1 Then    'Percentage
                            ConcessionAmount = Math.Round(((myFeeAmount * FCTypeAmount) / 100), 2)
                        Else    'Fixed
                            ConcessionAmount = FCTypeAmount
                        End If
                    End If
                Next
            End If
            If myFeeAmount < ConcessionAmount Then
                ConcessionAmount = myFeeAmount
            End If

            Return ConcessionAmount
        End Function
        Public Shared Function GetConcessionAmount(ByVal SID As Integer, ByVal FeeTypeID As Integer, MonthID As String) As Double
            Dim rv As Double = 0
            Dim SqlStr As String = ""

            SqlStr = "Select Sum(ConcessionAmount) From FeeStudentConcession Where SID='" & SID & "' and FeeTypeID=" & FeeTypeID & " and MonthID in (" & MonthID & ")"
            Try
                rv = ExecuteQuery_ExecuteScalar(SqlStr)
            Catch ex As Exception
            End Try
            Return rv
        End Function

        Public Shared Function GetFeeTypeConfigID() As String

            Dim sqlStr As String = ""
           
            Dim AdmissionFeeID As Integer = 0
            Dim LateFeeID As Integer = 0
            Dim ConveyanceFeeID As Integer = 0
            Dim TutionFeeID As Integer = 0
            Dim ArrearFeeID As Integer = 0
            Dim ExcessFeeID As Integer = 0
            sqlStr = "Select AddmissionFeeID,LateFeeID,ConveyanceFeeID,TutionFeeID,ArrearFeeID,ExcessFeeID From Params"
            
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                Try
                    AdmissionFeeID = myReader(0)
                Catch ex As Exception

                End Try
                Try
                    LateFeeID = myReader(1)
                Catch ex As Exception

                End Try
                Try
                    ConveyanceFeeID = myReader(2)
                Catch ex As Exception

                End Try
                Try
                    TutionFeeID = myReader(3)
                Catch ex As Exception

                End Try
                Try
                    ArrearFeeID = myReader(4)
                Catch ex As Exception

                End Try
                Try
                    ExcessFeeID = myReader(5)
                Catch ex As Exception

                End Try
            End While
            myReader.Close()

            Return AdmissionFeeID & "$" & LateFeeID & "$" & ConveyanceFeeID & "$" & TutionFeeID & "$" & ArrearFeeID & "$" & ExcessFeeID

        End Function

        'Public Shared Function GetLateFeeAmount(SID As Integer, FeeGroupID As Integer, AdmissionDate As String, TermIDs As String, AdmissionFeeID As Integer, LateFeeID As Integer, ASID As Integer, FeeConfigType As Integer, Optional DepositeDate As String = "", Optional AdminFeeApplicable As Integer = -1, Optional MonthID As String = "") As String

        '    Dim chkTerm() As String = TermIDs.Split(",")

        '    Dim sqlstr As String = ""
        '    Dim TotalConfig As Double = 0, TotalDeposit As Double = 0
        '    Dim StudentConfigType As Integer = FeeConfigType

        '    Dim ConfigAmount As Double = 0, DepositedAmount As Double = 0
        '    Dim TermDue As Double = 0
        '    Dim AdminFeeTag As Boolean = False

        '    If StudentConfigType = 0 Then
        '        If AdminFeeApplicable <> -1 Then
        '            AdminFeeTag = AdminFeeApplicable
        '        Else
        '            AdminFeeTag = AdmissionFeeApplicable(SID, ASID)
        '        End If
        '    End If

        '    For k = 0 To chkTerm.Count - 1
        '        Dim TermID As Integer = 0
        '        If IsNumeric(chkTerm(k)) = True Then
        '            TermID = chkTerm(k)
        '        Else
        '            TermID = LoadTermID(chkTerm(k).ToString, FeeGroupID)
        '        End If
        '        'LoadTermID(chkTerm(k).ToString, FeeGroupID)
        '        ConfigAmount = 0
        '        DepositedAmount = 0

        '        Dim FeeDepositDate As Date = Now.Date
        '        sqlstr = "Select max(DepositDate) From vw_FeeDeposit Where isDeposit=1 AND SID=" & SID & " and TermID =" & TermID


        '        Try
        '            FeeDepositDate = ExecuteQuery_ExecuteScalar(sqlstr)
        '        Catch ex As Exception
        '            If DepositeDate <> "" Then
        '                FeeDepositDate = DepositeDate
        '            End If
        '        End Try


        '        Dim LastDate As Date = Now.Date
        '        Dim LateFeeAmount As Double = 0, ProcessingMethod As Integer = 0

        '        sqlstr = "Select LastDate, LateFeeAmount, ProcessingMethod From FeeDueConfig Where FeeGroupID='" & FeeGroupID & "' and TermID=" & TermID & " AND ASID=" & ASID
        '        sqlstr += " And LastDate<='" & FeeDepositDate.ToString("yyyy/MM/dd") & "'"


        '        Dim DueConfigReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
        '        While DueConfigReader.Read
        '            LastDate = CDate(DueConfigReader(0))
        '            LateFeeAmount = DueConfigReader(1)
        '            ProcessingMethod = DueConfigReader(2)
        '        End While
        '        DueConfigReader.Close()

        '        'For New Admission
        '        If CType(AdmissionDate, Date) > LastDate Then

        '            Dim nextlastdate As Date = Now.Date
        '            sqlstr = "Select LastDate, LateFeeAmount, ProcessingMethod From FeeDueConfig Where FeeGroupID='" & FeeGroupID & "' and TermID=" & TermID & " AND ASID=" & ASID
        '            sqlstr += " And LastDate>'" & AdmissionDate & "'"

        '            Dim DueConfigReader1 As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
        '            While DueConfigReader1.Read
        '                nextlastdate = CDate(DueConfigReader1(0))
        '                Exit While
        '            End While
        '            DueConfigReader1.Close()

        '            If nextlastdate >= FeeDepositDate Then
        '            Else
        '                LateFeeAmount = 0
        '            End If

        '            sqlstr = "Select LastDate, LateFeeAmount,  ProcessingMethod From FeeDueConfig Where FeeGroupID='" & FeeGroupID & "' and TermID=" & TermID & " AND ASID=" & ASID
        '            sqlstr += " And LastDate>'" & AdmissionDate & "' And LastDate<='" & FeeDepositDate.ToString("yyyy/MM/dd") & "'"


        '            Dim DueConfigReader2 As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
        '            While DueConfigReader2.Read
        '                LastDate = CDate(DueConfigReader2(0))
        '                LateFeeAmount = DueConfigReader2(1)
        '                ProcessingMethod = DueConfigReader2(2)
        '                Exit While
        '                'FlagFine = True
        '            End While
        '            DueConfigReader2.Close()
        '        End If

        '        Dim tmpMonthID As String = ""
        '        If MonthID <> "" Then
        '            tmpMonthID = MonthID
        '        Else
        '            tmpMonthID = GetMonthID(FeeGroupID, TermID)
        '        End If
        '        If StudentConfigType = 0 Then
        '            ConfigAmount = GetFeeConfigForFeeHead(ASID, FeeGroupID, 0, tmpMonthID, "Yes")
        '            If AdminFeeTag = False Then
        '                ConfigAmount = ConfigAmount - GetFeeConfigForFeeHead(ASID, FeeGroupID, AdmissionFeeID, tmpMonthID, "Yes")
        '            End If
        '        Else
        '            ConfigAmount = GetFeeConfigForFeeHead(ASID, 0, 0, tmpMonthID, "Yes", SID)
        '        End If
        '        TotalConfig += ConfigAmount

        '        Dim DepositAmttmp As String = GetFeeDepositeForStudent(SID, 0, "'" & chkTerm(k).ToString & "'")
        '        DepositedAmount = Convert.ToDouble(DepositAmttmp.Split("/")(0)) + Convert.ToDouble(DepositAmttmp.Split("/")(1))
        '        TotalDeposit += DepositedAmount

        '        Dim DueAmount As Double = ConfigAmount - DepositedAmount

        '        'Calcuate  according to processing method
        '        If ProcessingMethod = 1 Then  'Monthly
        '            Dim TimeDiff As New TimeSpan
        '            TimeDiff = LastDate - FeeDepositDate
        '            Dim DiffDays As Integer = Math.Abs(TimeDiff.Days)
        '            Dim Months As Double = Convert.ToDouble(DiffDays) / 30
        '            LateFeeAmount = LateFeeAmount * Math.Ceiling(Months)
        '        ElseIf ProcessingMethod = 2 Then  'Daily
        '            Dim TimeDiff As New TimeSpan
        '            TimeDiff = LastDate - FeeDepositDate
        '            Dim DiffDays As Integer = Math.Abs(TimeDiff.Days)
        '            LateFeeAmount = (LateFeeAmount * DiffDays)
        '        Else  'Fix

        '        End If
        '        TermDue += LateFeeAmount
        '        'TotalDueAmount += TermDue
        '    Next


        '    Return TermDue
        'End Function

        Public Shared Function GetLateFeeAmount(SID As Integer, FeeGroupID As Integer, AdmissionDate As String, TermIDs As String, AdmissionFeeID As Integer, LateFeeID As Integer, ASID As Integer, FeeConfigType As Integer, Optional DepositeDate As String = "", Optional AdminFeeApplicable As Integer = -1, Optional MonthID As String = "") As String

            Dim chkTerm() As String = TermIDs.Split(",")

            Dim sqlstr As String = ""
            Dim TotalConfig As Double = 0, TotalDeposit As Double = 0
            Dim StudentConfigType As Integer = FeeConfigType

            Dim ConfigAmount As Double = 0, DepositedAmount As Double = 0
            Dim TermDue As Double = 0
            Dim AdminFeeTag As Boolean = False

            If StudentConfigType = 0 Then
                If AdminFeeApplicable <> -1 Then
                    AdminFeeTag = AdminFeeApplicable
                Else
                    AdminFeeTag = AdmissionFeeApplicable(SID, ASID)
                End If
            End If

            For k = 0 To chkTerm.Count - 1
                Dim TermID As Integer = 0
                If IsNumeric(chkTerm(k)) = True Then
                    TermID = chkTerm(k)
                Else
                    'termType = 1
                    TermID = LoadTermID(chkTerm(k).ToString, FeeGroupID)
                End If

                ConfigAmount = 0
                DepositedAmount = 0

                Dim tmpMonthID As String = ""
                If MonthID <> "" Then
                    tmpMonthID = MonthID
                Else
                    tmpMonthID = GetMonthID(FeeGroupID, TermID)
                End If
                If StudentConfigType = 0 Then
                    ConfigAmount = GetFeeConfigForFeeHead(ASID, FeeGroupID, 0, tmpMonthID, "Yes")
                    If AdminFeeTag = False Then
                        ConfigAmount = ConfigAmount - GetFeeConfigForFeeHead(ASID, FeeGroupID, AdmissionFeeID, tmpMonthID, "Yes")
                    End If
                Else
                    ConfigAmount = GetFeeConfigForFeeHead(ASID, 0, 0, tmpMonthID, "Yes", SID)
                End If
                TotalConfig += ConfigAmount

                Dim DepositAmttmp As String = ""

                'GetFeeDepositeForStudent(SID, 0, "'" & TermID & "'")
                DepositAmttmp = GetFeeDepositeForStudent(SID, 0, "'" & chkTerm(k).ToString & "'", "", "Yes")
             
                DepositedAmount = Convert.ToDouble(DepositAmttmp.Split("/")(0)) + Convert.ToDouble(DepositAmttmp.Split("/")(1))
                TotalDeposit += DepositedAmount

                Dim DueAmount As Double = ConfigAmount - DepositedAmount








                Dim LastDate As Date = Now.Date
                Dim LateFeeAmount As Double = 0, ProcessingMethod As Integer = 0
                Dim FeeDepositDate As Date = Now.Date


                If DueAmount <= 0 Then
                    sqlstr = "Select max(DepositDate) From vw_FeeDeposit Where isDeposit=1 AND SID=" & SID & " and TermNo ='" & chkTerm(k).ToString & "'"
                    Try
                        FeeDepositDate = ExecuteQuery_ExecuteScalar(sqlstr)
                    Catch ex As Exception
                        If DepositeDate <> "" Then
                            FeeDepositDate = DepositeDate
                        End If
                    End Try

                    sqlstr = "Select LastDate, LateFeeAmount, ProcessingMethod From FeeDueConfig Where FeeGroupID='" & FeeGroupID & "' and TermID=" & TermID & " AND ASID=" & ASID
                    sqlstr += " And LastDate<='" & FeeDepositDate.ToString("yyyy/MM/dd") & "'"


                    Dim DueConfigReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
                    While DueConfigReader.Read
                        LastDate = CDate(DueConfigReader(0))
                        LateFeeAmount = DueConfigReader(1)
                        ProcessingMethod = DueConfigReader(2)
                    End While
                    DueConfigReader.Close()

                    'For New Admission
                    If CType(AdmissionDate, Date) > LastDate Then

                        Dim nextlastdate As Date = Now.Date
                        sqlstr = "Select LastDate, LateFeeAmount, ProcessingMethod From FeeDueConfig Where FeeGroupID='" & FeeGroupID & "' and TermID=" & TermID & " AND ASID=" & ASID
                        sqlstr += " And LastDate>'" & AdmissionDate & "'"

                        Dim DueConfigReader1 As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
                        While DueConfigReader1.Read
                            nextlastdate = CDate(DueConfigReader1(0))
                            Exit While
                        End While
                        DueConfigReader1.Close()

                        If nextlastdate >= FeeDepositDate Then
                        Else
                            LateFeeAmount = 0
                        End If

                        sqlstr = "Select LastDate, LateFeeAmount,  ProcessingMethod From FeeDueConfig Where FeeGroupID='" & FeeGroupID & "' and TermID=" & TermID & " AND ASID=" & ASID
                        sqlstr += " And LastDate>'" & AdmissionDate & "' And LastDate<='" & FeeDepositDate.ToString("yyyy/MM/dd") & "'"


                        Dim DueConfigReader2 As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
                        While DueConfigReader2.Read
                            LastDate = CDate(DueConfigReader2(0))
                            LateFeeAmount = DueConfigReader2(1)
                            ProcessingMethod = DueConfigReader2(2)
                            Exit While
                            'FlagFine = True
                        End While
                        DueConfigReader2.Close()
                    End If


                Else



                    'sqlstr = "Select max(DepositDate) From vw_FeeDeposit Where isDeposit=1 AND SID=" & SID & " and TermID =" & TermID
                    'Try
                    '    FeeDepositDate = ExecuteQuery_ExecuteScalar(sqlstr)
                    'Catch ex As Exception
                    If DepositeDate <> "" Then
                        FeeDepositDate = DepositeDate
                    End If
                    'End Try

                    sqlstr = "Select LastDate, LateFeeAmount, ProcessingMethod From FeeDueConfig Where FeeGroupID='" & FeeGroupID & "' and TermID=" & TermID & " AND ASID=" & ASID
                    sqlstr += " And LastDate<='" & FeeDepositDate.ToString("yyyy/MM/dd") & "'"


                    Dim DueConfigReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
                    While DueConfigReader.Read
                        LastDate = CDate(DueConfigReader(0))
                        LateFeeAmount = DueConfigReader(1)
                        ProcessingMethod = DueConfigReader(2)
                    End While
                    DueConfigReader.Close()

                    'For New Admission
                    If CType(AdmissionDate, Date) > LastDate Then

                        Dim nextlastdate As Date = Now.Date
                        sqlstr = "Select LastDate, LateFeeAmount, ProcessingMethod From FeeDueConfig Where FeeGroupID='" & FeeGroupID & "' and TermID=" & TermID & " AND ASID=" & ASID
                        sqlstr += " And LastDate>'" & AdmissionDate & "'"

                        Dim DueConfigReader1 As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
                        While DueConfigReader1.Read
                            nextlastdate = CDate(DueConfigReader1(0))
                            Exit While
                        End While
                        DueConfigReader1.Close()

                        If nextlastdate >= FeeDepositDate Then
                        Else
                            LateFeeAmount = 0
                        End If

                        sqlstr = "Select LastDate, LateFeeAmount,  ProcessingMethod From FeeDueConfig Where FeeGroupID='" & FeeGroupID & "' and TermID=" & TermID & " AND ASID=" & ASID
                        sqlstr += " And LastDate>'" & AdmissionDate & "' And LastDate<='" & FeeDepositDate.ToString("yyyy/MM/dd") & "'"


                        Dim DueConfigReader2 As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
                        While DueConfigReader2.Read
                            LastDate = CDate(DueConfigReader2(0))
                            LateFeeAmount = DueConfigReader2(1)
                            ProcessingMethod = DueConfigReader2(2)
                            Exit While
                            'FlagFine = True
                        End While
                        DueConfigReader2.Close()
                    End If



                End If







                'Calcuate  according to processing method
                If ProcessingMethod = 1 Then  'Monthly
                    Dim TimeDiff As New TimeSpan
                    TimeDiff = LastDate - FeeDepositDate
                    Dim DiffDays As Integer = Math.Abs(TimeDiff.Days)
                    Dim Months As Double = Convert.ToDouble(DiffDays) / 30
                    LateFeeAmount = LateFeeAmount * Math.Ceiling(Months)
                ElseIf ProcessingMethod = 2 Then  'Daily
                    Dim TimeDiff As New TimeSpan
                    TimeDiff = LastDate - FeeDepositDate
                    Dim DiffDays As Integer = Math.Abs(TimeDiff.Days)
                    LateFeeAmount = (LateFeeAmount * DiffDays)
                Else  'Fix

                End If
                TermDue += LateFeeAmount
                'TotalDueAmount += TermDue
            Next


            Return TermDue
        End Function



        Public Shared Function GetDueAmountForTerm(ByVal ASID As Integer, TermID As Integer, ByVal FeeBookNo As String, Optional RegNo As String = "", Optional SID As String = "") As Double
            Dim FeeTypes() As String = GetFeeTypeConfigID().Split("$")
            Dim LateFeeID As Integer = FeeTypes(1)

            Dim sqlStr As String = ""
            Dim DueAmount As Double = 0

            If LateFeeID <= 0 Then
                DueAmount = -1
            Else
                'Get Late Fee Amount for FeeBook No
                If RegNo = "" Then
                    sqlStr = "Select Max(Fine) From vwFeeDues Where ASID=" & ASID & " AND TermID=" & TermID & " AND FeeBookNo='" & FeeBookNo & "' AND FeeTypeID=" & LateFeeID
                Else
                    sqlStr = "Select Max(Fine) From vwFeeDues Where ASID=" & ASID & " AND TermID=" & TermID & " AND RegNo='" & RegNo & "' AND FeeTypeID=" & LateFeeID
                End If
                If SID <> "" Then
                    sqlStr = "Select Max(Fine) From vwFeeDues Where ASID=" & ASID & " AND TermID=" & TermID & " AND SID='" & SID & "' AND FeeTypeID=" & LateFeeID
                End If



                Try
                    DueAmount = ExecuteQuery_ExecuteScalar(sqlStr)
                Catch ex As Exception
                    DueAmount = 0
                End Try
            End If




            Return DueAmount

        End Function

        Public Shared Function ProcessDues(ByRef lstClass As ListBox, ByRef lstSection As ListBox, ByVal myType As Integer, ByVal RegNo As String, ByVal ASID As Integer, ByVal TermNo As Integer, Optional ByVal StatusName As String = "") As Integer

            'myType=1 =>Individual, 2: Group

            Dim sqlStr As String = "", CollegeNote As String = ""
            Dim i As Integer = 0

            'Clear Report Table Contents
            sqlStr = "Delete From rptFeeDue"


            ExecuteQuery_Update(sqlStr)

            'Loop for ALL / Selected Class-Section (Save Students and Corresponding Configured Fee)
            For i = 0 To lstClass.Items.Count - 1

                If myType = 1 Then

                    sqlStr = "Insert into rptFeeDue (SID,RegNo, FeeBookNo, SName,FName,ClassName,SecName) " & _
                    " Select SID,RegNo, FeeBookNo, SName,FName,ClassName,SecName From vw_Student " & _
                    " Where RegNo='" & RegNo & "' and " & _
                    " ASID=" & ASID

                ElseIf myType = 2 Then

                    'Move List of Students in Current Class-Section-ASID-Status to Report Table                
                    sqlStr = "Insert into rptFeeDue (SID,RegNo, FeeBookNo, SName,FName,ClassName,SecName) " & _
                    " Select SID,RegNo, FeeBookNo, SName,FName,ClassName,SecName From vw_Student " & _
                    " Where ClassName='" & lstClass.Items(i).Text & "' and " & _
                    " SecName='" & lstSection.Items(i).Text & "' and " & _
                    " StatusName='" & StatusName & "' and " & _
                    " ASID=" & ASID

                End If



                ExecuteQuery_Update(SqlStr)

            Next

            sqlStr = "Update rptFeeDue Set FeeDueAmount=0 where FeeDueAmount is null"


            ExecuteQuery_Update(SqlStr)

            sqlStr = "Update rptFeeDue Set FeeDepositedAmount=0 Where FeeDepositedAmount is null"


            ExecuteQuery_Update(SqlStr)

            sqlStr = "Update rptFeeDue Set FeeConfigAmount=0 Where FeeConfigAmount is null"


            ExecuteQuery_Update(SqlStr)

            lstClass.Items.Clear()
            lstSection.Items.Clear()

            'Select All Students from Report Table
            Dim LstSID As New ListBox
            sqlStr = "Select SID, ClassName, SecName From rptFeeDue "



            Dim StudReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While StudReader.Read
                LstSID.Items.Add(StudReader(0))
                lstClass.Items.Add(StudReader(1))
                lstSection.Items.Add(StudReader(2))
            End While
            StudReader.Close()

            Dim TotalConfig As Double = 0, TotalDeposit As Double = 0
            Dim FeeTypes() As String = GetFeeTypeConfigID().Split("$")
            Dim AdmissionFeeID As Integer = FeeTypes(0)
            'For All Students in Rpeort Table Find Config Fee, Deposited Fee, Already Marked Late Fee and Fine (Term-wise)
            For i = 0 To LstSID.Items.Count - 1

                Dim FeeCount As Integer = 0
                Dim ConfigAmount As Double = 0, DepositedAmount As Double = 0

                Dim T1Amount As Double = 0, T2Amount As Double = 0, T3Amount As Double = 0
                Dim T4Amount As Double = 0, T5Amount As Double = 0, T6Amount As Double = 0
                Dim T7Amount As Double = 0, T8Amount As Double = 0, T9Amount As Double = 0
                Dim T10Amount As Double = 0, T11Amount As Double = 0, T12Amount As Double = 0

                Dim T1Config As Double = 0, T2Config As Double = 0, T3Config As Double = 0
                Dim T4Config As Double = 0, T5Config As Double = 0, T6Config As Double = 0
                Dim T7Config As Double = 0, T8Config As Double = 0, T9Config As Double = 0
                Dim T10Config As Double = 0, T11Config As Double = 0, T12Config As Double = 0

                TotalConfig = 0
                TotalDeposit = 0

                'Find Admission Due
                Dim AdminFeeTag As Boolean = False
                Dim AdminFeeConfig As Double = 0, AdminFeeAmount As Double = 0

                Dim AdminFeeID As Integer = AdmissionFeeID

                sqlStr = "Select Count(SID) From FeeDuesAdmission Where ASID=" & ASID & " and SID=" & LstSID.Items(i).Text


                If ExecuteQuery_ExecuteScalar(sqlStr) > 0 Then
                    AdminFeeTag = True
                Else
                    AdminFeeTag = False
                End If

                If AdminFeeTag = True Then
                    'Find Admission Fee
                    sqlStr = "Select SUM(FeeAmount) From vw_FeeConfig Where " & _
                    " ClassName='" & lstClass.Items(i).Text & "' and " & _
                    " SecName='" & lstSection.Items(i).Text & "' and " & _
                    " ASID=" & ASID & " and " & _
                    " FeeTypeId=" & AdminFeeID & " AND TermNo>0"




                    AdminFeeConfig = ExecuteQuery_ExecuteScalar(SqlStr)
                    TotalConfig += AdminFeeConfig

                End If

                Dim t As Integer = 0
                '' ''Dim ReAdmitFee As Double = 500  'For readmission if fee not deposited for two quarters consecutively

                For t = 1 To TermNo

                    ConfigAmount = 0
                    DepositedAmount = 0

                    '-----------------------------------------------
                    'Find LateFee and LastDate for adding Late Fine
                    '-----------------------------------------------
                    Dim LastDate As Date = Now.Date
                    Dim LateFeeAmount As Double = 0, LateFeeType As Integer = 0, ProcessingMethod As Integer = 0

                    sqlStr = "Select LastDate, LateFeeAmount, LateFeeType, ProcessingMethod From FeeDueConfig Where TermNo=" & t & " AND ASID=" & ASID


                    Dim DueConfigReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
                    While DueConfigReader.Read
                        LastDate = CDate(DueConfigReader(0))
                        LateFeeAmount = DueConfigReader(1)
                        LateFeeType = DueConfigReader(2)
                        ProcessingMethod = DueConfigReader(3)
                    End While
                    DueConfigReader.Close()

                    'Find Configured Fee for Current SID, Class-Section-ASID-TermNo-PartOfDueProcess
                    sqlStr = "Select SUM(FeeAmount) From vw_FeeConfig Where " & _
                    " ClassName='" & lstClass.Items(i).Text & "' and " & _
                    " SecName='" & lstSection.Items(i).Text & "' and " & _
                    " ASID=" & ASID & " and " & _
                    " PartOfDueProcess=1 and TermNo=" & t




                    Try
                        ConfigAmount = ExecuteQuery_ExecuteScalar(SqlStr)
                    Catch ex As Exception
                        ConfigAmount = 0
                    End Try

                    Dim TotalTerms As Integer = TotalFeeTerms()

                    If TotalTerms = 4 Then

                        Select Case t
                            Case 1 : T1Config = ConfigAmount
                            Case 2 : T2Config = ConfigAmount
                            Case 3 : T3Config = ConfigAmount
                            Case 4 : T4Config = ConfigAmount
                        End Select

                    ElseIf TotalTerms = 12 Then

                        Select Case t
                            Case 1 : T1Config = ConfigAmount
                            Case 2 : T2Config = ConfigAmount
                            Case 3 : T3Config = ConfigAmount
                            Case 4 : T4Config = ConfigAmount
                            Case 5 : T5Config = ConfigAmount
                            Case 6 : T6Config = ConfigAmount
                            Case 7 : T7Config = ConfigAmount
                            Case 8 : T8Config = ConfigAmount
                            Case 9 : T9Config = ConfigAmount
                            Case 10 : T10Config = ConfigAmount
                            Case 11 : T11Config = ConfigAmount
                            Case 12 : T12Config = ConfigAmount
                        End Select

                    End If

                    TotalConfig += ConfigAmount

                    'Find Deposited Fee (All including Concession)
                    sqlStr = " Select SID, Sum(Abs(FeeDepositAmount)) FD From vw_FeeDeposit Where " & _
                    " SID=" & LstSID.Items(i).Text & " and TermNo =" & t & " Group By SID"




                    FeeCount = 0
                    Dim FeeReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)

                    While FeeReader.Read
                        FeeCount += 1
                        DepositedAmount = FeeReader("FD")
                    End While
                    FeeReader.Close()

                    'If Fee Deosited then Update Report Table
                    If FeeCount > 0 Then

                        If TotalTerms = 4 Then

                            Select Case t
                                Case 1 : T1Amount = DepositedAmount
                                Case 2 : T2Amount = DepositedAmount
                                Case 3 : T3Amount = DepositedAmount
                                Case 4 : T4Amount = DepositedAmount
                            End Select

                        ElseIf TotalTerms = 12 Then

                            Select Case t
                                Case 1 : T1Amount = DepositedAmount
                                Case 2 : T2Amount = DepositedAmount
                                Case 3 : T3Amount = DepositedAmount
                                Case 4 : T4Amount = DepositedAmount
                                Case 5 : T5Amount = DepositedAmount
                                Case 6 : T6Amount = DepositedAmount
                                Case 7 : T7Amount = DepositedAmount
                                Case 8 : T8Amount = DepositedAmount
                                Case 9 : T9Amount = DepositedAmount
                                Case 10 : T10Amount = DepositedAmount
                                Case 11 : T11Amount = DepositedAmount
                                Case 12 : T12Amount = DepositedAmount
                            End Select

                        End If

                    End If

                    TotalDeposit += DepositedAmount

                    Dim ReadmissionOn As Boolean = False

                    If ReadmissionOn = False Then

                        If ConfigAmount - DepositedAmount > 0 And Now.Date > LastDate And TotalDeposit < TotalConfig Then

                            Dim LateEntryExists As Integer = 0
                            sqlStr = "Select Count(*) From FeeDues Where SID=" & LstSID.Items(i).Text & " AND TermNo=" & t


                            LateEntryExists = ExecuteQuery_ExecuteScalar(SqlStr)

                            If LateEntryExists <= 0 Then
                                'Calcuate  according to processing method

                                If ProcessingMethod = 1 Then  'Monthly
                                    sqlStr = "Insert into FeeDues Values(" & _
                                    ASID & "," & LstSID.Items(i).Text & "," & t & "," & LateFeeType & "," & LateFeeAmount & ",'') "
                                ElseIf ProcessingMethod = 2 Then  'Daily
                                    'Calculate difference between current date and last date
                                    Dim TimeDiff As New TimeSpan
                                    TimeDiff = LastDate - Now
                                    Dim DiffDays As Integer = Math.Abs(TimeDiff.Days)
                                    LateFeeAmount = LateFeeAmount * DiffDays
                                    If LateFeeAmount > 150 Then
                                        LateFeeAmount = 150
                                    End If
                                    sqlStr = "Insert into FeeDues Values(" & _
                                    ASID & "," & LstSID.Items(i).Text & "," & t & "," & LateFeeType & "," & LateFeeAmount & ",'') "
                                End If


                                ExecuteQuery_Update(SqlStr)

                            End If
                        End If

                    End If

                Next    'End of t

                ''''''''AMIT'''''
                ' term no should be choosen to which this due is being evaluated

                sqlStr = " Select SID, Sum(DueAmount) LF From FeeDues Where " & _
                " SID=" & LstSID.Items(i).Text & " and " & _
                " ASID =" & ASID & " and " & _
                " TermNo <= " & TermNo & _
                " Group By SID"




                Dim LateReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
                While LateReader.Read
                    TotalConfig += LateReader(1)
                End While
                LateReader.Close()

                CollegeNote = TermNo.ToString

                sqlStr = "Update rptFeeDue Set FeeConfigAmount=" & TotalConfig & ",FeeDepositedAmount=" & TotalDeposit & ",CollegeNote='" & CollegeNote & "' Where SID=" & LstSID.Items(i).Text


                ExecuteQuery_Update(SqlStr)

            Next    'End of student List

            'Find the Difference and Update Report Table
            sqlStr = "Update rptFeeDue Set FeeDueAmount=FeeDueAmount + FeeConfigAmount - FeeDepositedAmount "


            ExecuteQuery_Update(SqlStr)

            'Remove All Entries not belonging to Dues
            sqlStr = "Delete From rptFeeDue Where FeeDueAmount<=0"


            ExecuteQuery_Update(SqlStr)

            Return 0

        End Function
        Public Shared Function GetFeeConfig(ByVal ASID As Integer, ByVal FeeGroupID As String, ByVal TermID As Integer, AdmisionFeeID As Integer) As DataSet
            Dim SqlStr As String = ""
            SqlStr = "Select FeeTypeID,FeeAmount From FeeConfig Where ASID=" & ASID & " AND " & _
            " FeeGroupID='" & FeeGroupID & "' AND  TermID = " & TermID & " and FeeTypeID<>" & AdmisionFeeID & " order by FeeAmount DESC"
            Dim ds As New DataSet
            ds = ExecuteQuery_DataSet(SqlStr, "t")
            Return ds
        End Function
        Public Shared Function FeeDeposit_PastDuesConsideration(ByVal SID As Integer, ByVal Termid As Integer, ByVal FeeGroupID As Integer, ByVal FeetypeID As Integer, ByVal ASID As Integer) As Double

            Dim sqlStr As String = ""

            Dim FeeTypes() As String = GetFeeTypeConfigID().Split("$")
            Dim AdmissionFeeID As Integer = FeeTypes(0)
            Dim OldDueConfig As Double = 0

            sqlStr = "Select sum(FeeAmount) From vw_FeeConfig Where ASID=" & ASID & " AND " & _
           " FeeGroupID=" & FeeGroupID & " AND FeeTypeID = " & FeetypeID & _
           " AND TermID=" & Termid

            'sqlStr = "Select Sum(FeeAmount) From vw_FeeConfig Where ClassName='" & ClassName & "' AND SecName='" & SecName & "' AND ASID=" & ASID & " and TermID <" & TermNo


            Try
                OldDueConfig = ExecuteQuery_ExecuteScalar(SqlStr)
            Catch ex As Exception
                OldDueConfig = 0
            End Try

            If FeetypeID = AdmissionFeeID And AdmissionFeeApplicable(SID, ASID) = False Then
                OldDueConfig = 0
            End If

            Dim OldDueDeposit As Double = 0
            sqlStr = "Select Sum(FeeDepositAmount) From vw_FeeDeposit Where SID=" & SID & " AND ASID=" & ASID & " AND FeeTypeID<>" & FeetypeID & " AND TermID <" & Termid


            Try
                OldDueDeposit = ExecuteQuery_ExecuteScalar(SqlStr)
            Catch ex As Exception
                OldDueDeposit = 0
            End Try

            Return OldDueConfig - OldDueDeposit

        End Function
        Public Shared Function LoadClassByGroup(ByRef myCbo As DropDownList, FeeGroupID As Integer) As Integer

            Dim sqlStr As String = "Select distinct ClassName From vw_classstudent"
            If FeeGroupID > 0 Then
                sqlStr += " where FeeGroupID = " & FeeGroupID
            End If

            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            myCbo.Items.Clear()
            myCbo.Items.Add("")

            While myReader.Read
                myCbo.Items.Add(myReader(0))
            End While
            myReader.Close()

            Return 0
        End Function


        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '''''''''''''''''MODULE FOR CONVERTING RUPEE TO WORD''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        Private Shared TensWord As String() = New String() {String.Empty, String.Empty, "Twenty", "Thirty", "Forty", "Fifty", _
           "Sixty", "Seventy", "Eighty", "Ninety"}
        Private Shared OnesWord As String() = New String() {"Zero", "One", "Two", "Three", "Four", "Five", _
            "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", _
            "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", _
            "Eighteen", "Nineteen"}
        Private Shared GroupNames As String() = New String() {"lakh", "thousand", ""}

        Public Shared Function GetNumberAsWords(number As Decimal) As String
            Dim sb As New StringBuilder()
            If number > 9999999 Then
                Dim crores As Decimal = CLng(number) / 10000000
                sb.Append(GetNumberAsWords(crores) & Convert.ToString((If(crores = 1, " crore", " crores "))))
                number = number Mod 10000000
            End If

            Dim numberString As String = number.ToString("00,00,000", System.Globalization.CultureInfo.GetCultureInfo("hi-IN"))
            Dim numberParts As String() = numberString.Split(","c)
            For i As Integer = 0 To numberParts.Length - 1
                If Integer.Parse(numberParts(i)) = 0 Then
                    Continue For
                End If
                Dim groupAsWords As String = groupInWords(numberParts(i))
                If sb.Length <> 0 AndAlso numberParts(i).Length = 3 AndAlso Not groupAsWords.Contains(" and ") Then
                    sb.Append(" and ")
                End If
                sb.Append((groupAsWords & Convert.ToString(" ")) + GroupNames(i))
                sb.Append(If(numberParts(i).Length = 2 AndAlso numberParts(i) <> "01", "s ", " "))
            Next
            Return sb.ToString().Trim()
        End Function

        Private Shared Function groupInWords(numGroup As String) As String
            Dim groupWords As String = ""
            If numGroup.Length = 3 Then
                If numGroup(0) <> "0"c Then
                    Dim hundreds As Integer = Integer.Parse(numGroup.Substring(0, 1))
                    groupWords = OnesWord(hundreds) + " hundred "
                    If Integer.Parse(numGroup.Substring(1)) <> 0 Then
                        groupWords += "and "
                    End If
                End If
                numGroup = numGroup.Substring(1)
            End If
            Dim tens As Integer = Integer.Parse(numGroup.Substring(0, 1))
            If tens > 1 Then
                groupWords += TensWord(tens) + " "
                numGroup = numGroup.Substring(1)
            End If
            Dim ones As Integer = Integer.Parse(numGroup)
            If ones > 0 Then
                groupWords += OnesWord(ones)
            End If
            Return groupWords
        End Function
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ''''''''''''''''''''''''''''''''''''''''''''
        Public Shared Function LoadClassByClassGroup(ByRef myCbo As DropDownList, FeeGroupID As Integer) As Integer

            Dim sqlStr As String = "Select distinct ClassName,DisplayOrder From Classes"
            If FeeGroupID > 0 Then
                sqlStr += " where ClassGroupID = " & FeeGroupID
            End If
            sqlStr += " Order by DisplayOrder"
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            myCbo.Items.Clear()
            myCbo.Items.Add("")

            While myReader.Read
                myCbo.Items.Add(myReader(0))
            End While
            myReader.Close()

            Return 0
        End Function

    End Class

End Namespace

