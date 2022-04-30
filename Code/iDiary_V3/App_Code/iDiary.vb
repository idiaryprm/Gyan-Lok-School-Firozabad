Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Data
Imports System.Collections.Generic
Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.IO

Namespace iDiary

    Public Class CLS_idiary

        'Student Birthday SMS Template (Replace <*> by Name of the student
        Public Const StudentBirthdaySMSMessage As String = "Dear <*>, We wish you a Very Happy Birthday and God's abundant blessings. "
        'Fee Dues Notification Template (Replace <*> by Name of the Student, <**> by Term No
        Public Const FeeReminderSMSMessage As String = "Dear Parent, fee of <*> for the <**> qtr has not been paid. Kindly pay the dues; if paid pls verify in the Office, Principal"


        Public Shared Function LoadMasterInfo(ByVal MasterType As Integer, ByRef myCbo As DropDownList, Optional mv As String = "") As Integer

            '   Master Information IDs and Related Information
            '   ----------------------------------------------
            '   1   ->  Academic Session
            '   2   ->  Classes
            '   3   ->  Sections    (Not To be used Alone)
            '   4   ->  House
            '   5   ->  Religion
            '   6   ->  Caste
            '   7   ->  Occupation
            '   8   ->  Department
            '   9   ->  Designation 
            '   10  ->  Status
            '   11  ->  Fee Types
            '   12  ->  Payment Modes 
            '   13  ->  Transport
            '   14  ->  Grades
            '   15  ->  Character (TC)
            '   16  ->  Blood Group
            '   17  ->  Petty Cash Transaction Type
            '   18  ->  Exam Terms
            '   19  ->  SMS Sender
            '   20  ->  Pay Scale
            '   21  ->  Grade Pay / AGP
            '   22  ->  Bank
            '   23  ->  Earnings
            '   24  ->  Deductions
            '   25  ->  Department-Payroll
            '   26  ->  Designation-Payroll
            '   27  ->  Qualification
            '   28  ->  Nationality
            '   29  ->  Status_Payroll
            '   30  ->  Employee Category
            '   31  ->  Both Salary Head (Earning and Deductions)
            '   32  ->  Leave Type Master
            '   33  ->  Employee Session Master
            '   34  ->  Student Category Master
            '   35  ->  State Master
            '   36  ->  Study Medium master
            '   37  ->  Board Master
            '   38  ->  Location Master
            '   39  ->  Bus Route Master
            '   40  ->  conveyance Mode Master
            '   41  ->  Language Master
            '   42  ->  Vaccination Master
            '   43  ->  Severity Master
            '   44  ->  Allergy Master
            '   45  ->  Bus Master
            '   46  ->  Bus Driver Master
            '   49  ->  Scholastic B
            '   50  ->  CoScholastic Area
            '   51  ->  CoScholastic Activity
            '   52  ->  Health & Physical Education
            '   55  ->  VendorInventory
            '   56  ->  item Unit
            '   57  ->  inventory Store
            '   58  ->  item master
            '   59  ->  Term master
            '   60  ->  Fee Group master

            '   64  ->  EmployeeType
            '   65  ->  Library Membership
            Dim sqlStr As String = ""

            Select Case MasterType
                Case 1 : sqlStr = "Select ASName From AcademicSession"
                Case 2 : sqlStr = "Select distinct ClassName,ClassDisplayOrder From vw_ClassStudent where SchoolName='" & mv & "' Order By ClassDisplayOrder,ClassName"
                Case 3 : sqlStr = "Select SecName From Sections order by DisplayOrder"
                Case 4 : sqlStr = "Select HouseName From House"
                Case 5 : sqlStr = "Select RelName From Religion"
                Case 6 : sqlStr = "Select CasteName From Caste"
                Case 7 : sqlStr = "Select OccName From Occupation"
                Case 8 : sqlStr = "Select DeptName From Department"
                Case 9 : sqlStr = "Select DesgName From Designation"
                Case 10 : sqlStr = "Select StatusName From StatusMaster"
                Case 11 : sqlStr = "Select FeeTypeName From FeeTypes"
                Case 12 : sqlStr = "Select PMName From PaymentModes"
                Case 13 : sqlStr = "Select TransportName From TransportMaster"
                Case 14 : sqlStr = "Select GradeName From GradeMaster"
                Case 15 : sqlStr = "Select CharName From CharacterMaster"
                Case 16 : sqlStr = "Select BGName From BloodGroup"
                Case 17 : sqlStr = "Select TransTypeName From PettyCashTransactionMaster"
                Case 18 : sqlStr = "Select ExamTermName From ExamTermMaster"
                Case 19 : sqlStr = "Select SMSSender From SMSSender"
                Case 20 : sqlStr = "Select payScaleName From PayScale"
                Case 21 : sqlStr = "Select AGPName From GradePay"
                Case 22 : sqlStr = "Select BankName From Bank"
                Case 23 : sqlStr = "Select HeadName From SalaryHeadMaster Where HeadType=1"
                Case 24 : sqlStr = "Select HeadName From SalaryHeadMaster Where HeadType=2"
                Case 25 : sqlStr = "Select DeptName From Department_Payroll"
                Case 26 : sqlStr = "Select DesgName From Designation_Payroll"
                Case 27 : sqlStr = "Select QualName From Qualifications"
                Case 28 : sqlStr = "Select NatName From Nationality"
                Case 29 : sqlStr = "Select StatusName From Status_Payroll"
                Case 30 : sqlStr = "Select EmpCatName From EmployeeCategory"
                Case 31 : sqlStr = "Select HeadName From SalaryHeadMaster Order By HeadType, HeadName"
                Case 32 : sqlStr = "Select LeaveName From LeaveMaster"
                Case 33 : sqlStr = "Select EmpASName From EmployeeSessionMaster"
                Case 34 : sqlStr = "Select CategoryName From categoryMaster"
                Case 35 : sqlStr = "Select StateName From StateMaster"
                Case 36 : sqlStr = "Select MediumName From StudyMediumMaster"
                Case 37 : sqlStr = "Select BoardName From BoardMaster"
                Case 38 : sqlStr = "Select LocationName From LocationMaster"
                Case 39 : sqlStr = "Select RouteName From BusRouteMaster"
                Case 40 : sqlStr = "Select conveyanceName From conveyanceModeMaster"
                Case 41 : sqlStr = "Select LanguageName From LanguageMaster"
                Case 42 : sqlStr = "Select vacCode From VaccinationMaster"
                Case 43 : sqlStr = "Select SeverityName From SeverityMaster"
                Case 44 : sqlStr = "Select alrgName From AllergyMaster"
                Case 45 : sqlStr = "Select busName From BusMaster"
                Case 46 : sqlStr = "Select driverName From busDriverMaster"
                Case 47 : sqlStr = "Select StatusName From EnquiryStatus"
                Case 48 : sqlStr = "Select TypeName From EnquiryType"
                Case 49 : sqlStr = "Select CoScholasticSubName From SubjectCoScholasticMaster where type=1 order by CoScholasticSubID"
                Case 50 : sqlStr = "Select CoScholasticSubName From SubjectCoScholasticMaster where type=2 order by CoScholasticSubID"
                Case 51 : sqlStr = "Select CoScholasticSubName From SubjectCoScholasticMaster where type=3 order by CoScholasticSubID"
                Case 52 : sqlStr = "Select CoScholasticSubName From SubjectCoScholasticMaster where type=4 order by CoScholasticSubID"
                Case 53 : sqlStr = "Select Division From ExamResultDivision"
                Case 54 : sqlStr = "Select Result From ResultType"
                Case 55 : sqlStr = "Select VendorName From VendorInventory"
                Case 56 : sqlStr = "Select unitName From itemUnitMaster"
                Case 57 : sqlStr = "Select StoreName From inventoryStoreMaster"
                Case 58 : sqlStr = "Select itemName From itemMaster"
                Case 59 : sqlStr = "Select TermName From TermMaster"
                Case 60 : sqlStr = "Select FeeGroupName From FeeGroupMaster order by DisplayOrder"
                Case 61 : sqlStr = "Select CategoryArmyName From categoryArmyMaster"
                Case 62 : sqlStr = "Select SubSecName From SubSecMaster order by DisplayOrder"
                Case 63 : sqlStr = "Select CSSName From ClassStudent"
                Case 64 : sqlStr = "Select EmpTypeName From EmployeeType"
                Case 65 : sqlStr = "Select MemberShipName From LibraryMemberShipMaster "
                Case 66 : sqlStr = "Select MaritalStatusName From MaritalStatusMaster order by DisplayOrder"
                Case 67 : sqlStr = "Select BusTermName From BusTermMaster"
                Case 68 : sqlStr = "Select BusTermNo From BusTermMaster"
                Case 69 : sqlStr = "Select ConveyanceTypeName From BusTypeMaster"
                Case 70 : sqlStr = "Select ClassGroupName From ClassGroups"
                Case 71 : sqlStr = "Select SchoolName From SchoolMaster where SchoolID in(" & mv & ")"
                Case 72 : sqlStr = "Select FeeBankName From FeeBankMaster order by FeeBankName"
                Case 74 : sqlStr = "Select ClassName From Classes order by DisplayOrder"

                Case 101 : sqlStr = "Select ExamGroupName From vw_ExamGroups where SchoolName='" & mv & "' order by DisplayOrder,ExamGroupName "
                Case 102 : sqlStr = "Select ExamTermName From ExamTermMaster order by DisplayOrder,ExamTermName"
                Case 103 : sqlStr = "Select subGroupName From ExamSubjectGroupMaster order by DisplayOrder,subGroupName"
                Case 104 : sqlStr = "select ExamTermName from ExamTermMaster where isMinor=0 order by DisplayOrder,ExamTermName"
                Case 105 : sqlStr = "select ExamTermName from ExamTermMaster where isMinor=1 order by DisplayOrder,ExamTermName"

            End Select

            Dim myReader As System.Data.SqlClient.SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            myCbo.Items.Clear()
            If MasterType = 71 Or MasterType = 64 Or MasterType = 30 Or MasterType = 22 Or MasterType = 21 Or MasterType = 29 Or MasterType = 20 Or MasterType = 27 Or MasterType = 26 Or MasterType = 25 Or MasterType = 66 Or MasterType = 5 Or MasterType = 6 Or MasterType = 4 Or MasterType = 41 Or MasterType = 34 Or MasterType = 35 Or MasterType = 16 Or MasterType = 10 Or MasterType = 7 Or MasterType = 8 Or MasterType = 9 Or MasterType = 37 Then
            Else
                myCbo.Items.Add("")
            End If
            If MasterType = 5 Then
                myCbo.Items.Add(" ")
            End If
            While myReader.Read
                myCbo.Items.Add(myReader(0))
            End While
            myReader.Close()

            Return 0
        End Function
        Public Shared Function ExecuteQuery_Update(strQuery As String) As Integer
            Dim myConnStr As String = ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
            Dim myConn As New SqlConnection(myConnStr)
            myConn.Open()
            Dim myCommand As New SqlCommand
            myCommand.Connection = myConn
            myCommand.CommandText = strQuery
            Try
                myCommand.ExecuteNonQuery()
            Catch ex As Exception
            Finally
                myConn.Close()
                myCommand.Dispose()
                myConn.Dispose()
            End Try
            Return 0
        End Function
        Public Shared Function ExecuteQuery_ExecuteScalar(strQuery As String) As String
            Dim myConnStr As String = ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
            Dim myConn As New SqlConnection(myConnStr)
            myConn.Open()
            Dim myCommand As New SqlCommand
            myCommand.Connection = myConn
            myCommand.CommandText = strQuery
            Dim rv As String = ""
            Try
                rv = myCommand.ExecuteScalar()
            Catch ex As Exception
            Finally
                myConn.Close()
                myCommand.Dispose()
                myConn.Dispose()
            End Try

            Return rv
        End Function
        Public Shared Function ExecuteQuery_ExecuteReader(strQuery As String) As SqlDataReader
            Dim myConnStr As String = ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
            Dim myConn As New SqlConnection(myConnStr)
            myConn.Open()
            Dim myCommand As New SqlCommand
            myCommand.Connection = myConn
            myCommand.CommandText = strQuery
            Dim rv As SqlDataReader
            Try
                rv = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Catch ex As Exception
            Finally
                myCommand.Dispose()
            End Try
            Return rv
        End Function
        Public Shared Function ExecuteQuery_DataSet(ByVal strQuery As String, ByVal cTableName As String) As DataSet
            Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
            Dim con As New System.Data.SqlClient.SqlConnection(myConnStr)
            Dim SqlCmd = New SqlCommand(strQuery, con)
            SqlCmd.CommandTimeout = 3000000

            Dim da As New SqlDataAdapter()
            da.SelectCommand = SqlCmd
            If con.State <> ConnectionState.Open Then
                con.Open()
            End If
            Dim ds As New DataSet()
            Try
                da.Fill(ds, cTableName)
            Catch ex As Exception
                'HttpContext.Current.Response.Write(" Error Web Msql Error ExecuteQuery : " );
                Throw (ex)
            Finally
                SqlCmd.Connection.Close()
                SqlCmd.Dispose()
                con.Close()
            End Try
            Return ds
        End Function
        Public Shared Function GetMessage(MsgSub As String) As String
            Dim sqlStr As String = "Select MessageTemplateDesc From MessageTemplates where  MessageSubject like '%" & MsgSub & "'"
            Dim rv As String = ""
            rv = ExecuteQuery_ExecuteScalar(sqlStr)
            Return rv
        End Function
        Public Shared Function FindEmployeeID(ByVal EmpCode As String) As Integer
            Dim sqlStr As String = "Select EmpID From vwEmployees Where EmpCode='" & EmpCode & "' "
            Dim rv As Integer = 0
            Try
                rv = ExecuteQuery_ExecuteScalar(SqlStr)
            Catch ex As Exception
                rv = 0
            End Try
            Return rv
        End Function
        Public Shared Function CheckDuplicateEntry(ByVal MasterName As String, ByVal TableName As String, ByVal ColumnName As String) As Integer
            Dim sqlStr As String = "Select Count(*) From " & TableName & " Where " & ColumnName & "='" & MasterName & "' "
            Dim rv As Integer = ExecuteQuery_ExecuteScalar(sqlStr)
            If rv > 0 Then
                Return True
            Else
                Return False
            End If
        End Function
        
        Public Shared Function FindNextSno(ByVal year As String, type As Integer) As Integer
            Dim sqlStr As String = ""
            '1 Character Certificate
            '2 TC
            If type = 1 Then
                sqlStr = "SELECT MAX(CONVERT(INT, SUBSTRING(SNo, 6,LEN(SNo)-5)) ) + 1 FROM CharacterCertificateStudent WHERE SNo like '" & year & "%'"
            Else
                sqlStr = "SELECT MAX(CONVERT(INT, SUBSTRING(TCNo, 1,LEN(TCNo)-5)) ) + 1 FROM TC WHERE TCNo like '%" & year & "'"
            End If

            Dim myID As Integer = 0
            Try
                myID = ExecuteQuery_ExecuteScalar(sqlStr)
            Catch ex As Exception
                myID = 1
            End Try

            Return myID

        End Function
        Public Shared Function GetHindiNo(ByVal d As Integer) As String
            Dim HindiNoArray() As String = _
            {"", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", _
            "Eleven", "Twelve", "Thirteen", "Forteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Ninteen", "Twenty", _
            "Twenty one", "Twenty two", "Twenty three", "Twenty four", "Twenty five", "Twenty six", "Twenty seven", "Twenty eight", "Twenty nine", "Thirty", _
            "Thirty one", "Thirty two", "Thirty three", "Thirty four", "Thirty five", "Thirty six", "Thirty seven", "Thirty eight", "Thirty nine", "Forty", _
            "Forty one", "Forty two", "Forty three", "Forty four", "Forty five", "Forty six", "Forty seven", "Forty eiggt", "Forty nine", "Fifty", _
            "Fifty one", "Fifty two", "Fifty three", "Fifty four", "Fifty five", "Fifty six", "Fifty seven", "Fifty eight", "Fifty nine", "Sixty", _
            "Sixty one", "Sixty two", "Sixty three", "Sixty four", "Sixty five", "Sixty six", "Sixty seven", "Sixty eight", "Sixty nine", "Seventy", _
            "Seventy one", "Seventy two", "Seventy three", "Seventy four", "Seventy five", "Seventy six", "Seventy seven", "Seventy eight", "Seventy nine", "Eighty", _
            "Eighty one", "Eighty two", "Eighty three", "Eighty four", "Eighty five", "Eighty six", "Eighty seven", "Eighty eight", "Eighty nine", "Ninety", _
            "Ninety one", "Ninety two", "Ninety three", "Ninety four", "Ninety five", "Ninety six", "Ninety seven", "Ninety eight", "Ninety nine"}
            Return HindiNoArray(d)
        End Function
        Public Shared Function GetMonth(ByVal d As Integer) As String
            Dim HindiArray() As String = _
            {"", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"}
            Return HindiArray(d)
        End Function
        Public Shared Function CheckCertificateSno(ByVal Sno As String, type As Integer) As Boolean
            Dim sqlStr As String = ""
            If type = 1 Then
                sqlStr = "Select count(*) from CharacterCertificateStudent where SNo='" & Sno & "'"
            ElseIf type = 2 Then
                sqlStr = "Select count(*) from oldTCdata where TCNo='" & Sno & "'"
            Else
                sqlStr = "Select count(*) from TC where TCNo='" & Sno & "'"
            End If

            Dim myID As Integer = 0
            myID = ExecuteQuery_ExecuteScalar(sqlStr)
            If myID > 0 Then
                Return True
            Else
                Return False
            End If
        End Function
        Public Shared Function LoadFeeBankBranch(ByVal BankID As Integer, ByRef mycbo As ListBox) As Integer
            Dim sqlstr As String = "Select Distinct FeeBankBranchName From FeeBankBranchMaster Where FeeBankID='" & BankID & "' Order By FeeBankBranchName asc"

            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
            mycbo.Items.Clear()
            While myReader.Read
                mycbo.Items.Add(myReader(0))
            End While
            myReader.Close()

            Return 0

        End Function
        Public Shared Function LoadFeeBankBranch(ByVal BankID As Integer, ByRef mycbo As DropDownList) As Integer
            Dim sqlstr As String = "Select Distinct FeeBankBranchName From FeeBankBranchMaster Where FeeBankID='" & BankID & "' Order By FeeBankBranchName asc"

            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
            mycbo.Items.Clear()
            mycbo.Items.Add("")
            While myReader.Read
                mycbo.Items.Add(myReader(0))
            End While
            myReader.Close()

            Return 0

        End Function
        Public Shared Function GetDefaultFeeBankBranch(ByVal BankID As String) As String
            Dim sqlstr As String = "Select top (1) FeeBankBranchName From FeeBankBranchMaster Where FeeBankID='" & BankID & "'"

            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
            Dim rv As String = ""
            While myReader.Read
                rv = myReader(0)
            End While
            myReader.Close()

            Return rv

        End Function
        Public Shared Function LoadCity(ByVal StateID As Integer, ByRef mycbo As ListBox) As Integer
            Dim sqlstr As String = "Select Distinct CityName From CityMaster Where StateID='" & StateID & "' and IsDeleted=0 Order By CityName asc"

            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
            mycbo.Items.Clear()
            While myReader.Read
                mycbo.Items.Add(myReader(0))
            End While
            myReader.Close()

            Return 0

        End Function



        Public Shared Function LoadMasterInfo(ByVal MasterType As Integer, ByRef myLst As ListBox, Optional mv As String = "") As Integer

            Dim sqlStr As String = ""

            Select Case MasterType
                Case 1 : sqlStr = "Select ASName From AcademicSession"
                Case 2 : sqlStr = "Select ClassName From Classes Order By DisplayOrder"
                Case 3 : sqlStr = "Select SecName From Sections order by DisplayOrder"
                Case 4 : sqlStr = "Select HouseName From House"
                Case 5 : sqlStr = "Select RelName From Religion"
                Case 6 : sqlStr = "Select CasteName From Caste"
                Case 7 : sqlStr = "Select OccName From Occupation"
                Case 8 : sqlStr = "Select DeptName From Department"
                Case 9 : sqlStr = "Select DesgName From Designation"
                Case 10 : sqlStr = "Select StatusName From StatusMaster"
                Case 11 : sqlStr = "Select FeeTypeName From FeeTypes"
                Case 12 : sqlStr = "Select PMName From PaymentModes"
                Case 13 : sqlStr = "Select TransportName From TransportMaster"
                Case 14 : sqlStr = "Select GradeName From GradeMaster"
                Case 15 : sqlStr = "Select CharName From CharacterMaster"
                Case 16 : sqlStr = "Select BGName From BloodGroup"
                Case 17 : sqlStr = "Select TransTypeName From PettyCashTransactionMaster"
                Case 18 : sqlStr = "Select ExamTermName From ExamTermMaster"
                Case 19 : sqlStr = "Select SMSSender From SMSSender"
                Case 20 : sqlStr = "Select payScaleName From PayScale"
                Case 21 : sqlStr = "Select AGPName From GradePay"
                Case 22 : sqlStr = "Select BankName From Bank"
                Case 23 : sqlStr = "Select HeadName From SalaryHeadMaster Where HeadType=1"
                Case 24 : sqlStr = "Select HeadName From SalaryHeadMaster Where HeadType=2"
                Case 25 : sqlStr = "Select DeptName From Department_Payroll"
                Case 26 : sqlStr = "Select DesgName From Designation_Payroll"
                Case 27 : sqlStr = "Select QualName From Qualifications"
                Case 28 : sqlStr = "Select NatName From Nationality"
                Case 29 : sqlStr = "Select StatusName From Status_Payroll"
                Case 30 : sqlStr = "Select EmpCatName From EmployeeCategory"
                Case 31 : sqlStr = "Select HeadName From SalaryHeadMaster Order By HeadType, HeadName"
                Case 32 : sqlStr = "Select LeaveName From LeaveMaster"
                Case 33 : sqlStr = "Select EmpASName From EmployeeSessionMaster"
                Case 34 : sqlStr = "Select CategoryName From categoryMaster"
                Case 35 : sqlStr = "Select StateName From StateMaster"
                Case 36 : sqlStr = "Select MediumName From StudyMediumMaster"
                Case 37 : sqlStr = "Select BoardName From BoardMaster"
                Case 38 : sqlStr = "Select LocationName From LocationMaster"
                Case 39 : sqlStr = "Select RouteName From BusRouteMaster"
                Case 40 : sqlStr = "Select conveyanceName From conveyanceModeMaster"
                Case 41 : sqlStr = "Select LanguageName From LanguageMaster"
                Case 42 : sqlStr = "Select vacCode From VaccinationMaster"
                Case 43 : sqlStr = "Select SeverityName From SeverityMaster"
                Case 44 : sqlStr = "Select alrgName From AllergyMaster"
                Case 45 : sqlStr = "Select busName From BusMaster"
                Case 46 : sqlStr = "Select driverName From busDriverMaster"
                Case 47 : sqlStr = "Select StatusName From EnquiryStatus"
                Case 48 : sqlStr = "Select TypeName From EnquiryType"
                Case 49 : sqlStr = "Select CoScholasticSubName From SubjectCoScholasticMaster where type=1 order by CoScholasticSubID"
                Case 50 : sqlStr = "Select CoScholasticSubName From SubjectCoScholasticMaster where type=2 order by CoScholasticSubID"
                Case 51 : sqlStr = "Select CoScholasticSubName From SubjectCoScholasticMaster where type=3 order by CoScholasticSubID"
                Case 52 : sqlStr = "Select CoScholasticSubName From SubjectCoScholasticMaster where type=4 order by CoScholasticSubID"
                Case 53 : sqlStr = "Select Division From ExamResultDivision"
                Case 54 : sqlStr = "Select Result From ResultType"
                Case 55 : sqlStr = "Select VendorName From VendorInventory"
                Case 56 : sqlStr = "Select unitName From itemUnitMaster"
                Case 57 : sqlStr = "Select StoreName From inventoryStoreMaster"
                Case 58 : sqlStr = "Select itemName From itemMaster"
                Case 59 : sqlStr = "Select TermName From TermMaster"
                Case 60 : sqlStr = "Select FeeGroupName From FeeGroupMaster order by DisplayOrder"
                Case 61 : sqlStr = "Select CategoryArmyName From categoryArmyMaster"
                Case 62 : sqlStr = "Select SubSecName From SubSecMaster order by DisplayOrder"
                Case 63 : sqlStr = "Select CSSName From ClassStudent inner join SchoolMaster on ClassStudent.SchoolID=SchoolMaster.SchoolID where SchoolName='" & mv & "' order by ClassStudent.ClassID,ClassStudent.SecID"
                Case 64 : sqlStr = "Select EmpTypeName From EmployeeType"
                Case 65 : sqlStr = "Select MemberShipName From LibraryMemberShipMaster "
                Case 66 : sqlStr = "Select MaritalStatusName From MaritalStatusMaster order by DisplayOrder"
                Case 67 : sqlStr = "Select BusTermName From BusTermMaster"
                    'Case 68 : sqlStr = "Select ConveyanceTypeName From BusTypeMaster"
                Case 69 : sqlStr = "Select ConveyanceTypeName From BusTypeMaster"

                Case 71 : sqlStr = "Select SchoolName From SchoolMaster"
                Case 72 : sqlStr = "Select FeeBankName From FeeBankMaster order by FeeBankName"



                Case 101 : sqlStr = "Select ExamGroupName From vw_ExamGroups where SchoolName='" & mv & "' order by DisplayOrder,ExamGroupName "
                Case 102 : sqlStr = "Select ExamTermName From ExamTermMaster order by DisplayOrder,ExamTermName"
                Case 103 : sqlStr = "Select subGroupName From ExamSubjectGroupMaster order by DisplayOrder,subGroupName"

            End Select

            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            myLst.Items.Clear()
            While myReader.Read
                myLst.Items.Add(myReader(0))
            End While
            myReader.Close()

            Return 0

        End Function
        Public Shared Function CheckDoubleEntry(ByVal MasterType As Integer, ByVal MasterName As String) As Integer

            Dim sqlStr As String = ""
            Dim rv As Integer = 0

            Select Case MasterType
                Case 1 : sqlStr = "Select Count(*) From AcademicSession Where ASName='" & MasterName & "'"
                Case 2 : sqlStr = "Select Count(*) From Classes Where ClassName='" & MasterName & "'"
                Case 3 : sqlStr = "Select Count(*) From Sections Where SecName='" & MasterName & "'"
                Case 4 : sqlStr = "Select Count(*) From House Where HouseName='" & MasterName & "'"
                Case 5 : sqlStr = "Select Count(*) From Religion Where RelName='" & MasterName & "'"
                Case 6 : sqlStr = "Select Count(*) From Caste Where CasteName='" & MasterName & "'"
                Case 7 : sqlStr = "Select Count(*) From Occupation Where OccName='" & MasterName & "'"
                Case 8 : sqlStr = "Select Count(*) From Department Where DeptName='" & MasterName & "'"
                Case 9 : sqlStr = "Select Count(*) From Designation Where DesgName='" & MasterName & "'"
                Case 10 : sqlStr = "Select Count(*) From StatusMaster Where StatusName='" & MasterName & "'"
                Case 11 : sqlStr = "Select Count(*) From FeeTypes Where FeeTypeName='" & MasterName & "'"
                Case 12 : sqlStr = "Select Count(*) From PaymentModes Where PMName='" & MasterName & "'"
                Case 13 : sqlStr = "Select Count(*) From TransportMaster Where TransportName='" & MasterName & "'"
                Case 14 : sqlStr = "Select Count(*) From GradeMaster Where GradeName='" & MasterName & "'"
                Case 15 : sqlStr = "Select Count(*) From CharacterMaster Where CharName='" & MasterName & "'"
                Case 16 : sqlStr = "Select Count(*) From BloodGroup Where BGName='" & MasterName & "'"
                Case 17 : sqlStr = "Select Count(*) From PettyCashTransactionMaster Where TransTypeName='" & MasterName & "'"
                Case 18 : sqlStr = "Select Count(*) From ExamTermMaster Where ExamTermName='" & MasterName & "'"
                Case 19 : sqlStr = "Select Count(*) From SMSSender Where SMSSender='" & MasterName & "'"
                Case 20 : sqlStr = "Select Count(*) From PayScale  Where PayScaleName='" & MasterName & "'"
                Case 21 : sqlStr = "Select Count(*) From GradePay Where AGPName='" & MasterName & "'"
                Case 22 : sqlStr = "Select Count(*) From Bank Where BankName='" & MasterName & "'"
                Case 23 : sqlStr = "Select Count(*) From SalaryHeadMaster Where HeadName='" & MasterName & "' AND HeadType=1"
                Case 24 : sqlStr = "Select Count(*) From SalaryHeadMaster Where HeadName='" & MasterName & "' AND HeadType=2"
                Case 25 : sqlStr = "Select Count(*) From Department_Payroll Where DeptName='" & MasterName & "'"
                Case 26 : sqlStr = "Select Count(*) From Designation_Payroll Where DesgName='" & MasterName & "'"
                Case 27 : sqlStr = "Select Count(*) From Qualifications Where QualName='" & MasterName & "'"
                Case 28 : sqlStr = "Select Count(*) From Nationality Where NatName='" & MasterName & "'"
                Case 29 : sqlStr = "Select Count(*) From Status_Payroll Where StatusName='" & MasterName & "'"
                Case 30 : sqlStr = "Select Count(*) From EmployeeCategory Where EmpCatName='" & MasterName & "'"
                Case 31 : sqlStr = "Select Count(*) From SalaryHeadMaster Where HeadName='" & MasterName & "'"
                Case 32 : sqlStr = "Select Count(*) From LeaveMaster Where LeaveName='" & MasterName & "'"
                Case 33 : sqlStr = "Select Count(*) From EmployeeSessionMaster Where EmpASName ='" & MasterName & "'"
                Case 34 : sqlStr = "Select Count(*) From CategoryMaster Where CategoryName ='" & MasterName & "'"
                Case 35 : sqlStr = "Select Count(*) From StateMaster Where StateName ='" & MasterName & "'"
                Case 36 : sqlStr = "Select Count(*) From StudyMediumMaster Where MediumName ='" & MasterName & "'"
                Case 37 : sqlStr = "Select Count(*) From BoardMaster Where BoardName ='" & MasterName & "'"
                Case 38 : sqlStr = "Select Count(*) From LocationMaster Where LocationName ='" & MasterName & "'"
                Case 39 : sqlStr = "Select Count(*) From BusRouteMaster Where routeName ='" & MasterName & "'"
                Case 40 : sqlStr = "Select Count(*) From conveyanceModeMaster Where conveyanceName ='" & MasterName & "'"
                Case 41 : sqlStr = "Select Count(*) From LanguageMaster Where LanguageName ='" & MasterName & "'"
                Case 42 : sqlStr = "Select Count(*) From VaccinationMaster Where vacCode='" & MasterName & "'"
                Case 43 : sqlStr = "Select Count(*) From SeverityMaster Where SeverityName='" & MasterName & "'"
                Case 44 : sqlStr = "Select Count(*) From AllergyMaster Where alrgName='" & MasterName & "'"
                Case 45 : sqlStr = "Select Count(*) From BusMaster Where busName='" & MasterName & "'"
                Case 46 : sqlStr = "Select Count(*) From busDriverMaster Where driverName='" & MasterName & "'"
                Case 47 : sqlStr = "Select Count(*) From EnquiryStatus Where StatusName='" & MasterName & "'"
                Case 48 : sqlStr = "Select Count(*) From EnquiryType Where TypeName='" & MasterName & "'"
                Case 49 : sqlStr = "Select Count(*) From SubjectCoScholasticMaster where CoScholasticSubName = N'" & MasterName & "'"
                Case 53 : sqlStr = "Select Count(*) From ExamResultDivision where Division =N'" & MasterName & "'"
                Case 54 : sqlStr = "Select Count(*)  From ResultType where Result =N'" & MasterName & "'"
                Case 55 : sqlStr = "Select Count(*) From VendorInventory where VendorName='" & MasterName & "'"
                Case 56 : sqlStr = "Select Count(*) From itemUnitMaster where unitName='" & MasterName & "'"
                Case 57 : sqlStr = "Select Count(*) From inventoryStoreMaster where storeName='" & MasterName & "'"
                Case 58 : sqlStr = "Select Count(*) From itemMaster where itemName='" & MasterName & "'"
                Case 59 : sqlStr = "Select Count(*) From TermMaster where TermName='" & MasterName & "'"
                Case 60 : sqlStr = "Select Count(*) From FeeGroupMaster where FeeGroupName='" & MasterName & "'"
                Case 61 : sqlStr = "Select Count(*) From CategoryArmyMaster Where CategoryArmyName ='" & MasterName & "'"
                Case 62 : sqlStr = "Select Count(*) From SubSecMaster Where SubSecName ='" & MasterName & "'"
                Case 63 : sqlStr = "Select Count(*) From ClassStudent Where CSSName='" & MasterName & "'"
                Case 64 : sqlStr = "Select Count(*) From EmployeeType Where EmpTypeName='" & MasterName & "'"
                Case 65 : sqlStr = "Select Count(*) From LibraryMemberShipMaster Where MemberShipName ='" & MasterName & "'"
                Case 66 : sqlStr = "Select Count(*) From  MaritalStatusMaster Where MaritalStatusName ='" & MasterName & "'"
                Case 69 : sqlStr = "Select Count(*) From  BusTypeMaster Where ConveyanceTypeName ='" & MasterName & "'"
                Case 70 : sqlStr = "Select Count(*) From CityMaster Where CityName='" & MasterName & "'"
                Case 71 : sqlStr = "Select Count(*) From SchoolMaster where SchoolName='" & MasterName & "'"
                Case 72 : sqlStr = "Select Count(*) From FeeBankMaster where FeeBankName='" & MasterName & "'"
                Case 73 : sqlStr = "Select Count(*) From FeeBankBranchMaster where FeeBankBranhName='" & MasterName & "'"

                Case 101 : sqlStr = "Select count(*) From ExamGroups Where ExamGroupName='" & MasterName & "'"
                Case 102 : sqlStr = "Select Count(*) From ExamTermMaster Where ExamTermName='" & MasterName & "'"
                Case 103 : sqlStr = "Select Count(*) From ExamSubjectGroupMaster Where subGroupName='" & MasterName & "'"

            End Select
            Try
                rv = ExecuteQuery_ExecuteScalar(sqlStr)
            Catch ex As Exception

            End Try
            Return rv
        End Function

        Public Shared Function FindMasterID(ByVal MasterType As Integer, ByVal MasterName As String, Optional SchoolID As Integer = 0) As Integer

            Dim sqlStr As String = ""

            Select Case MasterType
                Case 1 : sqlStr = "Select Max(ASID) From AcademicSession Where ASName='" & MasterName & "'"
                Case 2 : sqlStr = "Select Max(ClassID) From Classes Where ClassName='" & MasterName & "'"
                Case 3 : sqlStr = "Select Max(SecID) From Sections Where SecName='" & MasterName & "'"
                Case 4 : sqlStr = "Select Max(HouseID) From House Where HouseName='" & MasterName & "'"
                Case 5 : sqlStr = "Select Max(RelID) From Religion Where RelName='" & MasterName & "'"
                Case 6 : sqlStr = "Select Max(CasteID) From Caste Where CasteName='" & MasterName & "'"
                Case 7 : sqlStr = "Select Max(OccID) From Occupation Where OccName='" & MasterName & "'"
                Case 8 : sqlStr = "Select Max(DeptID) From Department Where DeptName='" & MasterName & "'"
                Case 9 : sqlStr = "Select Max(DesgID) From Designation Where DesgName='" & MasterName & "'"
                Case 10 : sqlStr = "Select Max(StatusID) From StatusMaster Where StatusName='" & MasterName & "'"
                Case 11 : sqlStr = "Select Max(FeeTypeID) From FeeTypes Where FeeTypeName='" & MasterName & "'"
                Case 12 : sqlStr = "Select Max(PMID) From PaymentModes Where PMName='" & MasterName & "'"
                Case 13 : sqlStr = "Select Max(TransportID) From TransportMaster Where TransportName='" & MasterName & "'"
                Case 14 : sqlStr = "Select Max(GradeID) From GradeMaster Where GradeName='" & MasterName & "'"
                Case 15 : sqlStr = "Select Max(CharID) From CharacterMaster Where CharName='" & MasterName & "'"
                Case 16 : sqlStr = "Select Max(BGID) From BloodGroup Where BGName='" & MasterName & "'"
                Case 17 : sqlStr = "Select Max(TransTypeID) From PettyCashTransactionMaster Where TransTypeName='" & MasterName & "'"
                Case 18 : sqlStr = "Select Max(ExamTermID) From ExamTermMaster Where ExamTermName='" & MasterName & "'"
                Case 19 : sqlStr = "Select Max(SMSSenderID) From SMSSender Where SMSSender='" & MasterName & "'"
                Case 20 : sqlStr = "Select Max(payScaleID) From PayScale  Where PayScaleName='" & MasterName & "'"
                Case 21 : sqlStr = "Select Max(AGPID) From GradePay Where AGPName='" & MasterName & "'"
                Case 22 : sqlStr = "Select Max(BankID) From Bank Where BankName='" & MasterName & "'"
                Case 23 : sqlStr = "Select Max(HeadID) From SalaryHeadMaster Where HeadName='" & MasterName & "' AND HeadType=1"
                Case 24 : sqlStr = "Select Max(HeadID) From SalaryHeadMaster Where HeadName='" & MasterName & "' AND HeadType=2"
                Case 25 : sqlStr = "Select Max(DeptID) From Department_Payroll Where DeptName='" & MasterName & "'"
                Case 26 : sqlStr = "Select Max(DesgID) From Designation_Payroll Where DesgName='" & MasterName & "'"
                Case 27 : sqlStr = "Select Max(QualID) From Qualifications Where QualName='" & MasterName & "'"
                Case 28 : sqlStr = "Select Max(NatID) From Nationality Where NatName='" & MasterName & "'"
                Case 29 : sqlStr = "Select Max(StatusID) From Status_Payroll Where StatusName='" & MasterName & "'"
                Case 30 : sqlStr = "Select Max(EmpCatID) From EmployeeCategory Where EmpCatName='" & MasterName & "'"
                Case 31 : sqlStr = "Select Max(HeadID) From SalaryHeadMaster Where HeadName='" & MasterName & "'"
                Case 32 : sqlStr = "Select Max(LeaveID) From LeaveMaster Where LeaveName='" & MasterName & "'"
                Case 33 : sqlStr = "Select Max(EmpASID) From EmployeeSessionMaster Where EmpASName ='" & MasterName & "'"
                Case 34 : sqlStr = "Select Max(CategoryID) From CategoryMaster Where CategoryName ='" & MasterName & "'"
                Case 35 : sqlStr = "Select Max(StateID) From StateMaster Where StateName ='" & MasterName & "'"
                Case 36 : sqlStr = "Select Max(MediumID) From StudyMediumMaster Where MediumName ='" & MasterName & "'"
                Case 37 : sqlStr = "Select Max(BoardID) From BoardMaster Where BoardName ='" & MasterName & "'"
                Case 38 : sqlStr = "Select Max(LocationID) From LocationMaster Where LocationName ='" & MasterName & "'"
                Case 39 : sqlStr = "Select Max(routeID) From BusRouteMaster Where routeName ='" & MasterName & "'"
                Case 40 : sqlStr = "Select Max(conveyanceID) From conveyanceModeMaster Where conveyanceName ='" & MasterName & "'"
                Case 41 : sqlStr = "Select Max(LanguageID) From LanguageMaster Where LanguageName ='" & MasterName & "'"
                Case 42 : sqlStr = "Select Max(vacID) From VaccinationMaster Where vacCode='" & MasterName & "'"
                Case 43 : sqlStr = "Select Max(sevID) From SeverityMaster Where SeverityName='" & MasterName & "'"
                Case 44 : sqlStr = "Select Max(alrgID) From AllergyMaster Where alrgName='" & MasterName & "'"
                Case 45 : sqlStr = "Select Max(busID) From BusMaster Where busName='" & MasterName & "'"
                Case 46 : sqlStr = "Select (driverID) From busDriverMaster Where driverName='" & MasterName & "'"
                Case 47 : sqlStr = "Select (EnquiryStatusID) From EnquiryStatus Where StatusName='" & MasterName & "'"
                Case 48 : sqlStr = "Select (EnquiryTypeID) From EnquiryType Where TypeName='" & MasterName & "'"
                Case 49 : sqlStr = "Select Max(CoScholasticSubID) From SubjectCoScholasticMaster where CoScholasticSubName = N'" & MasterName & "'"
                Case 53 : sqlStr = "Select Max(ExamResultDivisionID) From ExamResultDivision where Division =N'" & MasterName & "'"
                Case 54 : sqlStr = "Select Max(ResultTypeID)  From ResultType where Result =N'" & MasterName & "'"
                Case 55 : sqlStr = "Select Max(VendorID) From VendorInventory where VendorName='" & MasterName & "'"
                Case 56 : sqlStr = "Select Max(iuID) From itemUnitMaster where unitName='" & MasterName & "'"
                Case 57 : sqlStr = "Select Max(storeID) From inventoryStoreMaster where storeName='" & MasterName & "'"
                Case 58 : sqlStr = "Select Max(itemID) From itemMaster where itemName='" & MasterName & "'"
                Case 59 : sqlStr = "Select Max(TermID) From TermMaster where TermName='" & MasterName & "'"
                Case 60 : sqlStr = "Select Max(FGID) From FeeGroupMaster where FeeGroupName='" & MasterName & "'"
                Case 61 : sqlStr = "Select Max(CategoryArmyID) From CategoryArmyMaster Where CategoryArmyName ='" & MasterName & "'"
                Case 62 : sqlStr = "Select Max(SubSecID) From SubSecMaster Where SubSecName ='" & MasterName & "'"
                Case 63 : sqlStr = "Select Max(CSSID) From ClassStudent Where CSSName='" & MasterName & "'"
                Case 64 : sqlStr = "Select Max(empTypeID) From EmployeeType Where EmpTypeName='" & MasterName & "'"
                Case 65 : sqlStr = "Select Max(LibraryMemID) From LibraryMemberShipMaster Where MemberShipName ='" & MasterName & "'"
                Case 66 : sqlStr = "Select Max(MaritalStatusID) From MaritalStatusMaster Where MaritalStatusName ='" & MasterName & "'"
                Case 67 : sqlStr = "Select Max(BusTermID) From BusTermMaster Where BusTermNo ='" & MasterName & "'"
                Case 69 : sqlStr = "Select Max(ConveyanceTypeID) From BusTypeMaster Where ConveyanceTypeName ='" & MasterName & "'"
                Case 70 : sqlStr = "Select Max(ClassGroupID) From ClassGroups Where ClassGroupName ='" & MasterName & "'"
                Case 71 : sqlStr = "Select Max(SchoolID) From SchoolMaster where SchoolName='" & MasterName & "'"
                Case 72 : sqlStr = "Select Max(FeeBankID) From FeeBankMaster where FeeBankName='" & MasterName & "'"
                Case 73 : sqlStr = "Select Max(FeeBankBranchID) From FeeBankBranchMaster where FeeBankBranchName='" & MasterName & "'"

                Case 101 : sqlStr = "Select Max(examGroupID) From ExamGroups Where ExamGroupName='" & MasterName & "'"
                Case 102 : sqlStr = "Select Max(ExamTermID) From ExamTermMaster Where ExamTermName='" & MasterName & "'"
                Case 103 : sqlStr = "Select Max(subGrpID) From ExamSubjectGroupMaster Where subGroupName='" & MasterName & "'"
                Case 114 : sqlStr = "Select Max(examGroupID) From vw_classstudent Where Classname='" & MasterName & "'"
            End Select
            If SchoolID <> 0 Then
                sqlStr = sqlStr & " and SchoolID=" & SchoolID
            End If

            Dim myID As Integer = 0
            Try
                myID = ExecuteQuery_ExecuteScalar(sqlStr)
            Catch ex As Exception
                myID = 0
            End Try

            Return myID

        End Function
        Public Shared Function FindDefault(ByVal MasterType As Integer) As String

            Dim sqlStr As String = ""

            Select Case MasterType
                'Case 1 : sqlStr = "Select Max(ASID) From AcademicSession Where ASName=N'" & MasterName & "'"
                'Case 2 : sqlStr = "Select Max(ClassID) From Classes Where ClassName=N'" & MasterName & "'"
                'Case 3 : sqlStr = "Select Max(SecID) From Sections Where SecName=N'" & MasterName & "'"
                Case 4 : sqlStr = "Select HouseName From House Where IsDefault=1"
                Case 5 : sqlStr = "Select RelName From Religion Where IsDefault=1"
                Case 6 : sqlStr = "Select CasteName From Caste Where IsDefault=1"
                Case 7 : sqlStr = "Select OccName From Occupation Where IsDefault=1"
                Case 8 : sqlStr = "Select DeptName From Department Where IsDefault=1"
                Case 9 : sqlStr = "Select DesgName From Designation Where IsDefault=1"
                Case 10 : sqlStr = "Select StatusName From StatusMaster Where IsDefault=1"
                    'Case 11 : sqlStr = "Select Max(FeeTypeID) From FeeTypes Where FeeTypeName=N'" & MasterName & "'"
                Case 12 : sqlStr = "Select PMName From PaymentModes Where  IsDefault=1"
                Case 13 : sqlStr = "Select TransportName From TransportMaster Where IsDefault=1"
                    'Case 14 : sqlStr = "Select Max(GradeID) From GradeMaster Where GradeName=N'" & MasterName & "'"
                    'Case 15 : sqlStr = "Select Max(CharID) From CharacterMaster Where CharName=N'" & MasterName & "'"
                Case 16 : sqlStr = "Select BGName From BloodGroup Where IsDefault=1"
                    'Case 17 : sqlStr = "Select Max(TransTypeID) From PettyCashTransactionMaster Where TransTypeName=N'" & MasterName & "'"
                    'Case 18 : sqlStr = "Select Max(ExamTermID) From ExamTermMaster Where ExamTermName=N'" & MasterName & "'"
                    'Case 19 : sqlStr = "Select Max(SMSSenderID) From SMSSender Where SMSSender=N'" & MasterName & "'"
                Case 20 : sqlStr = "Select PayScaleName From PayScale  Where IsDefault=1"
                Case 21 : sqlStr = "Select AGPName From GradePay Where IsDefault=1"
                Case 22 : sqlStr = "Select BankName From Bank Where IsDefault=1"
                    'Case 23 : sqlStr = "Select Max(HeadID) From SalaryHeadMaster Where HeadName=N'" & MasterName & "' AND HeadType=1"
                    'Case 24 : sqlStr = "Select Max(HeadID) From SalaryHeadMaster Where HeadName=N'" & MasterName & "' AND HeadType=2"
                Case 25 : sqlStr = "Select DeptName From Department_Payroll Where IsDefault=1"
                Case 26 : sqlStr = "Select DesgName From Designation_Payroll Where IsDefault=1"
                Case 27 : sqlStr = "Select QualName From Qualifications Where IsDefault=1"
                Case 28 : sqlStr = "Select NatName From Nationality Where IsDefault=1"
                Case 29 : sqlStr = "Select StatusName From Status_Payroll Where IsDefault=1"
                Case 30 : sqlStr = "Select EmpCatName From EmployeeCategory Where IsDefault=1"
                    'Case 31 : sqlStr = "Select Max(HeadID) From SalaryHeadMaster Where HeadName=N'" & MasterName & "'"
                    'Case 32 : sqlStr = "Select SubCasteName From SubCaste where IsDefault=1"
                    'Case 33 : sqlStr = "Select Max(EmpASID) From EmployeeSessionMaster Where EmpASName =N'" & MasterName & "'"
                    'Case 34 : sqlStr = "Select Max(PostOfficeID)  From PostOffice where PostOfficeName =N'" & MasterName & "'"
                Case 34 : sqlStr = "Select CategoryName  From CategoryMaster where IsDefault=1"
                Case 35 : sqlStr = "Select StateName From StateMaster  where IsDefault=1"
                Case 36 : sqlStr = "Select MediumName From StudyMediumMaster Where IsDefault=1"
                Case 37 : sqlStr = "Select BoardName From BoardMaster Where IsDefault=1"
                    'Case 38 : sqlStr = "Select Max(CommonExamID)  From CommonExam where CommonExam = N'" & MasterName & "'"
                    'Case 39 : sqlStr = "Select Max(ExamResultDivisionID) From ExamResultDivision where Division =N'" & MasterName & "'"
                    'Case 40 : sqlStr = "Select Max(ResultTypeID)  From ResultType where Result =N'" & MasterName & "'"
                Case 41 : sqlStr = "Select LanguageName From LanguageMaster Where IsDefault=1"
                Case 45 : sqlStr = "Select CityName From CityMaster Where IsDefault=1"
                    'Case 42 : sqlStr = "Select Max(LeaveID) From LeaveMaster Where LeaveName=N'" & MasterName & "'"
                    'Case 43 : sqlStr = "Select Max(CoScholasticSubID) From SubjectCoScholasticMaster where CoScholasticSubName = N'" & MasterName & "'"
                Case 59 : sqlStr = "Select TallyCompanyName From Params"
                Case 61 : sqlStr = "Select CategoryArmyName  From CategoryArmyMaster where IsDefault=1"
                Case 64 : sqlStr = "Select EmpTypeName From EmployeeType Where IsDefault=1"
                Case 66 : sqlStr = "Select MaritalStatusName From MaritalStatusMaster Where IsDefault=1"
                Case 67 : sqlStr = "Select SMSSender From SMSSender Where IsDefault=1"
                Case 69 : sqlStr = "Select ConveyanceTypeName From BusTypeMaster Where IsDefault=1"
                Case 72 : sqlStr = "Select FeeBankName From FeeBankMaster Where IsDefault=1"
            End Select

            Dim rv As String = ""
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                Try
                    rv = myReader(0)
                Catch ex As Exception
                    rv = ""
                End Try

            End While
            myReader.Close()

            Return rv

        End Function
        Public Shared Function FindMasterName(ByVal MasterType As Integer, ByVal MasterID As Integer) As String

            Dim sqlStr As String = ""
            Dim rv As String = ""

            Select Case MasterType
                'Case 1 : sqlStr = "Select ASName From AcademicSession"
                'Case 2 : sqlStr = "Select ClassName From Classes Order By DisplayOrder "
                'Case 3 : sqlStr = "Select SecName From Sections"
                'Case 4 : sqlStr = "Select HouseName From House"
                'Case 5 : sqlStr = "Select RelName From Religion"
                'Case 6 : sqlStr = "Select CasteName From Caste"
                'Case 7 : sqlStr = "Select OccName From Occupation"
                'Case 8 : sqlStr = "Select DeptName From Department"
                'Case 9 : sqlStr = "Select DesgName From Designation"
                'Case 10 : sqlStr = "Select StatusName From StatusMaster"
                Case 11 : sqlStr = "Select FeeTypeName From FeeTypes Where FeeTypeID=" & MasterID
                    'Case 12 : sqlStr = "Select PMName From PaymentModes"
                    'Case 13 : sqlStr = "Select TransportName From TransportMaster"
                    'Case 14 : sqlStr = "Select GradeName From GradeMaster"
                    'Case 15 : sqlStr = "Select CharName From CharacterMaster"
                    'Case 16 : sqlStr = "Select BGName From BloodGroup"
                    'Case 17 : sqlStr = "Select TransTypeName From PettyCashTransactionMaster"
                    'Case 18 : sqlStr = "Select ExamTermName From ExamTermMaster"
                    'Case 19 : sqlStr = "Select SMSSender From SMSSender"
                    'Case 20 : sqlStr = "Select payScaleName From PayScale"
                    'Case 21 : sqlStr = "Select AGPName From GradePay"
                    'Case 22 : sqlStr = "Select BankName From Bank"
                    'Case 23 : sqlStr = "Select HeadName From SalaryHeadMaster Where HeadType=1"
                    'Case 24 : sqlStr = "Select HeadName From SalaryHeadMaster Where HeadType=2"
                    'Case 25 : sqlStr = "Select DeptName From Department_Payroll"
                    'Case 26 : sqlStr = "Select DesgName From Designation_Payroll"
                    'Case 27 : sqlStr = "Select QualName From Qualifications"
                    'Case 28 : sqlStr = "Select NatName From Nationality"
                    'Case 29 : sqlStr = "Select StatusName From Status_Payroll"
                    'Case 30 : sqlStr = "Select EmpCatName From EmployeeCategory"
                    'Case 31 : sqlStr = "Select HeadName From SalaryHeadMaster Order By HeadType, HeadName"
                Case 45 : sqlStr = "Select BusName From BusMaster Where BusID=" & MasterID
                Case 64 : sqlStr = "Select EmpTypeName From EmployeeType Where empTypeID=" & MasterID
                Case 69 : sqlStr = "Select ConveyanceTypeName From BusTypeMaster Where ConveyanceTypeID=" & MasterID

            End Select


            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                rv = myReader(0)
            End While
            myReader.Close()

            Return rv
        End Function


        Public Shared Function ShowLeaveBalance(ByVal EmpID As Integer, ByVal EmpASID As Integer, ByVal LeaveID As Integer) As Double

            Dim LeaveCount As Double = 0, CarryForward As Integer = 0
            Dim MaxLeaves As Double = 0, LeaveAvailed As Double = 0

            Dim sqlStr As String = ""

            sqlStr = "Select CarryForward From LeaveMaster Where LeaveID=" & LeaveID
            Dim CFReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While CFReader.Read
                CarryForward = CFReader(0)
            End While
            CFReader.Close()

            sqlStr = "Select * From vw_EmployeeLeaves Where EmpID=" & EmpID & " AND LeaveID=" & LeaveID

            If CarryForward = 0 Then
                'Search in Current Session
                sqlStr &= " AND EmpASID=" & EmpASID
            ElseIf CarryForward = 1 Then
                'Do Nothing Search in All Sessions
            End If

            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                MaxLeaves = myReader("MaxLimit")
            End While
            myReader.Close()

            'Calculate Leave Availed
            sqlStr = "Select count(*) From EmployeeAttendance Where EmpID=" & EmpID & " AND LeaveID=" & LeaveID
            If CarryForward = 0 Then
                'Search LeaveAvailaed in Current Session
                sqlStr &= " AND EmpASID=" & EmpASID
            ElseIf CarryForward = 1 Then
                'Do Nothing. Search in All Sessions
            End If

            Try
                LeaveAvailed = ExecuteQuery_ExecuteScalar(sqlStr)
            Catch ex As Exception
                LeaveAvailed = 0
            End Try

            LeaveCount = MaxLeaves - LeaveAvailed

            Return LeaveCount
        End Function

        Public Shared Function LoadEmployees(SearchType As Integer, SearchValue As String, myLst As ListBox) As Integer
            '   Search Type 
            '   0   ->  All
            '   1   ->  DepartmentWise
            '   2   ->  Employee Category-wise

            Dim sqlStr As String = "Select EmpName From vw_Employees "
            If SearchType = 0 Then
                'Do nothing
            ElseIf SearchType = 1 Then
                sqlStr &= "Where DeptName='" & SearchValue & "'"
            ElseIf SearchType = 2 Then
                sqlStr &= "Where EmpCatName='" & SearchValue & "'"
            End If


            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            myLst.Items.Clear()
            While myReader.Read
                myLst.Items.Add(myReader(0))
            End While
            myReader.Close()

            Return 0

        End Function

        Public Shared Function LoadEmployees(SearchType As Integer, SearchValue As String, myCbo As DropDownList) As Integer
            '   Search Type 
            '   0   ->  All
            '   1   ->  DepartmentWise
            '   2   ->  Employee Category-wise

            Dim sqlStr As String = "Select EmpName From vw_Employees "
            If SearchType = 0 Then
                'Do nothing
            ElseIf SearchType = 1 Then
                sqlStr &= "Where DeptName='" & SearchValue & "'"
            ElseIf SearchType = 2 Then
                sqlStr &= "Where EmpCatName='" & SearchValue & "'"
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

        Public Shared Function FindEmployeeID(ByVal SearchType As Integer, ByVal SearchValue As String, ByVal EmpName As String) As Integer

            Dim sqlStr As String = "Select EmpID From vw_Employees Where EmpName Like '%" & EmpName & "%' "
            If SearchType = 0 Then
                'Do nothing
            ElseIf SearchType = 1 Then
                sqlStr &= "AND DeptName='" & SearchValue & "'"
            ElseIf SearchType = 2 Then
                sqlStr &= "AND EmpCatName='" & SearchValue & "'"
            End If

            Dim rv As Integer = 0
            Try
                rv = ExecuteQuery_ExecuteScalar(sqlStr)
            Catch ex As Exception
                rv = 0
            End Try

            Return rv

        End Function
        Public Shared Function FindEmployeeName(ByVal EmpCode As String) As String

            Dim sqlStr As String = "Select  EmpName From vw_Employees Where EmpCode='" & EmpCode & "'"
            Dim rv As String = ""
            Try
                rv = ExecuteQuery_ExecuteScalar(sqlStr)
            Catch ex As Exception
                rv = ""
            End Try

            Return rv

        End Function
        Public Shared Function FindCodefromEmployeeID(ByVal EmpID As String) As String

            Dim sqlStr As String = "Select  EmpCode From vw_Employees Where EmpID='" & EmpID & "'"
            Dim rv As String = ""
            Try
                rv = ExecuteQuery_ExecuteScalar(sqlStr)
            Catch ex As Exception
                rv = ""
            End Try

            Return rv

        End Function
        Public Shared Function FindClassGroupID(ByVal ClassName As String) As String

            Dim sqlStr As String = "Select  ClassGroupID From Classes Where ClassName='" & ClassName & "'"
            Dim rv As String = ""
            Try
                rv = ExecuteQuery_ExecuteScalar(sqlStr)
            Catch ex As Exception
                rv = ""
            End Try

            Return rv

        End Function
        Public Shared Function FindEmployeeIDfromCode(ByVal EmpCode As String) As Integer

            Dim sqlStr As String = "Select  MAX(EmpID) From vw_Employees Where EmpCode='" & EmpCode & "'"
            Dim rv As Integer = 0
            Try
                rv = ExecuteQuery_ExecuteScalar(sqlStr)
            Catch ex As Exception
                rv = 0
            End Try

            Return rv

        End Function

        Public Shared Function GetNewEmpID() As Integer


            Dim sqlStr As String = "Select Max(EmpID) From EmployeeMaster"
            Dim rv As Integer = 0
            Try
                rv = ExecuteQuery_ExecuteScalar(sqlStr)
            Catch ex As Exception
                rv = 0
            End Try
            Return rv + 1
        End Function

        Public Shared Function FeeBookSameAsSRNo() As Integer
            Dim sqlStr As String = "Select MAX(SRAndFeeBookSame) from Params"
            Dim rv As Integer = 0
            Try
                rv = ExecuteQuery_ExecuteScalar(sqlStr)
            Catch ex As Exception
                rv = 0
            End Try

            Return rv
        End Function

        Public Shared Function IsTemplateCodeExist(ByVal myCode As String) As Boolean


            Dim sqlStr As String = "Select Count(*) from SMSTemplates Where TemplateCode='" & myCode & "'"
            Dim rv As Integer = ExecuteQuery_ExecuteScalar(sqlStr)
            If rv <= 0 Then
                Return False
            Else
                Return True
            End If
        End Function

        Public Shared Function FindTemplateMessage(ByVal myCode As String) As String
            Dim sqlStr As String = "Select TemplateMessage from SMSTemplates Where TemplateCode='" & myCode & "'"
            Dim rv As String = ""
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                Try
                    rv = myReader(0)
                Catch ex As Exception
                    rv = "Some Error in Finding Template Message..."
                End Try
            End While
            myReader.Close()
            Return rv
        End Function

        Public Shared Function LoadSMSTemplate(ByRef MyLst As ListBox) As Integer
            Dim sqlStr As String = "Select TemplateCode from SMSTemplates"
            MyLst.Items.Clear()

            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                MyLst.Items.Add(myReader(0))
            End While
            myReader.Close()

            Return 0
        End Function

        Public Shared Function LoadSMSTemplate(ByRef MyCbo As DropDownList) As Integer
            Dim sqlStr As String = "Select TemplateCode from SMSTemplates"
            MyCbo.Items.Clear()
            MyCbo.Items.Add("")

            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                MyCbo.Items.Add(myReader(0))
            End While
            myReader.Close()

            Return 0
        End Function

        Public Shared Function SMSFaciltyOpted() As Boolean
            Dim sqlStr As String = "Select MAX(SMSFacility) from Params"
            Dim rv As Boolean = ExecuteQuery_ExecuteScalar(sqlStr)

            Return rv
        End Function

        Public Shared Function GetSMSConfig(ByVal myParam As Integer) As String
            '   1  -    Server URL
            '   2   -   Port
            '   3  -    User Name
            '   2   -   Password
            Dim sqlStr As String = "SELECT "
            Select Case myParam
                Case 1 : sqlStr &= "SMSURL "
                Case 2 : sqlStr &= "SMSPort "
                Case 3 : sqlStr &= "SMSUser "
                Case 4 : sqlStr &= "SMSPass "
            End Select
            sqlStr &= " From SMSConfig"
            Dim rv As String = ""
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                rv = myReader(0)
            End While
            myReader.Close()

            Return rv
        End Function

        Public Shared Function TotalSMSAvailable() As String
            'Wroking Link Can be Directly run on browser
            'http://sms6.routesms.com:8080/bulksms/bulksms?username=tinfotech&password=tinf1234&type=0&dlr=0&destination=9639325975,9639712972&source=SFELIX&message=Dear+Abhi+there+will+be+a+staff+meeting+at7AM+Regards%2c+Principal
            Dim rv As String = ""

            Dim WebRequest As Net.WebRequest 'object for WebRequest
            Dim WebResonse As Net.WebResponse 'object for WebResponse
            Dim Server As String = GetSMSConfig(1)
            Dim Port As String = GetSMSConfig(2)
            Dim UserName As String = GetSMSConfig(3)
            Dim Password As String = GetSMSConfig(4)
            Dim WebResponseString As String = ""
            Dim type As Integer = 0
            Dim URL As String = "http://bulksms.idiary.in/sms_api/balanceinfo.php?username=" & UserName & "&password=" & Password & ""

                   
                    WebRequest = Net.HttpWebRequest.Create(URL) 'Hit URL Link
                    WebRequest.Timeout = 25000
                    Try
                        WebResonse = WebRequest.GetResponse 'Get Response
                        Dim reader As IO.StreamReader = New IO.StreamReader(WebResonse.GetResponseStream)
                        'Read Response and store in variable
                        WebResponseString = reader.ReadToEnd()
                        WebResonse.Close()
                        rv = WebResponseString
                    Catch ex As Exception
                        WebResponseString = "Request Timeout" 'If any exception occur.
                        rv = WebResponseString
                    End Try
            Return rv

        End Function

        '''''''''''''''''''''''''''SMS SENDING with fix no of contacts at a time'''''''''''''AMIT

        Public Shared Function SendMySMS(ByVal SenderName As String, ByVal RecipientList As List(Of String), ByVal SMSMessage As String) As String
            'Wroking Link Can be Directly run on browser
            'http://sms6.routesms.com:8080/bulksms/bulksms?username=tinfotech&password=tinf1234&type=0&dlr=0&destination=9639325975,9639712972&source=SFELIX&message=Dear+Abhi+there+will+be+a+staff+meeting+at7AM+Regards%2c+Principal
            Dim rv As String = ""

            Dim WebRequest As Net.WebRequest 'object for WebRequest
            Dim WebResonse As Net.WebResponse 'object for WebResponse

            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' ''''''''
            ' DEFINE PARAMETERS USED IN URL
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' ''''''''

            'To what server you need to connect to for submission
            'i.e. Dim Server As String = "smpp1.spheredge.com"
            Dim Server As String = GetSMSConfig(1)
            'Port that is to be used like 8080 or 8000
            Dim Port As String = GetSMSConfig(2)
            'Username that is to be used for submission
            'i.e. Dim UserName As String = "tester"
            Dim UserName As String = GetSMSConfig(3)
            ' password that is to be used along with username
            'i.e. Dim Password As String = "password"
            Dim Password As String = GetSMSConfig(4)
            'What type of the message that is to be sent.
            '0:means plain text
            '1:means flash
            '2:means Unicode (Message content should be in Hex)
            '6:means Unicode Flash(Message content should be in Hex)
            Dim type As Integer = 0
            'Message content that is to be transmitted

            Dim Message As String = SMSMessage
            'Url Encode message
            Message = HttpUtility.UrlEncode(Message)
            If (type = 2) Or (type = 6) Then
                Message = ConvertToUnicode(Message)
            End If
            'Message = Message.Replace(" ", "%20")
            'Message = Message.Replace("'", "%27")
            'Require DLR or not
            '0:means DLR is not Required
            '1:means DLR is Required
            Dim DLR As Integer = 0
            'Sender Id to be used for submitting the message
            'i.e. Dim SenderName As String = "test"
            Dim Source As String = SenderName '"SFELIX"
            'Destinations to which message is to be sent For submitting more than(one)
            'destination at once destinations should be comma separated Like
            '91999000123,91999000124

            Dim num As Integer = 40 ''''''''''''''''no of contacts in one go
            Dim Destination As String = ""
            Dim Mycount As Integer = RecipientList.Count
            Dim tmp As String = ""
            For i = 1 To Mycount
                Destination &= RecipientList.Item(i - 1) & ","
                ' i += 1
                If i Mod num = 0 Or i = Mycount Then
                    Destination = Destination.Substring(0, Destination.Length - 1)
                    Destination = Destination.Replace("&nbsp;", "NA")
                    'Dim Destination As String = RecipientList '"9897925900"
                    '''''''CODE COMPLETE TO DEFINE PARAMETER''''''''''''''''
                    Dim WebResponseString As String = ""

                    ''''''''''''''''''''''''''''''''''''''''''''''''  COMMENTED FOR NOT SENDING SMS
                    ''''''''''''''''''''''''''''''''''''''''''''''''
                    '        Dim URL As String = "http://bhashsms.com/api/sendmsg.php?user=" & UserName & "&pass=" & Password & "&sender=" & SenderName & "&phone=" & Destination & _
                    '"&text=" & Message & "&priority=ndnd&stype=normal"
                    Dim URL As String = "http://bulksms.idiary.in/sms_api/sendsms.php?username=" & UserName & _
                 "&password=" & Password & "&mobile=" & Destination & "&sendername=" & Source & "&message=" & Message & "; "
                    WebRequest = Net.HttpWebRequest.Create(URL) 'Hit URL Link
                    WebRequest.Timeout = 25000

                    tmp += Destination
                    Try
                        WebResonse = WebRequest.GetResponse 'Get Response
                        Dim reader As IO.StreamReader = New IO.StreamReader(WebResonse.GetResponseStream)
                        'Read Response and store in variable
                        WebResponseString = reader.ReadToEnd()
                        WebResonse.Close()
                        rv = WebResponseString
                        SMSResponceInDB(rv, Message, Destination)
                    Catch ex As Exception
                        WebResponseString = "Request Timeout" 'If any exception occur.
                        rv = WebResponseString
                    End Try
                    Destination = ""
                End If
            Next

            Dim tmp2 As String = tmp
            ''Dim Destination As String = RecipientList '"9897925900"
            ''''''''CODE COMPLETE TO DEFINE PARAMETER''''''''''''''''
            'Dim WebResponseString As String = ""
            'Dim URL As String = "http://" & Server & ":" & Port & "/bulksms/bulksms?username=" & UserName & _
            '"&password=" & Password & "&type=" & type & "&dlr=" & DLR & "&destination=" & Destination & _
            '"&source=" & Source & "&message=" & Message & ""
            'WebRequest = Net.HttpWebRequest.Create(URL) 'Hit URL Link
            'WebRequest.Timeout = 25000

            'Try
            '    WebResonse = WebRequest.GetResponse 'Get Response
            '    Dim reader As IO.StreamReader = New IO.StreamReader(WebResonse.GetResponseStream)
            '    'Read Response and store in variable
            '    WebResponseString = reader.ReadToEnd()
            '    WebResonse.Close()
            '    rv = WebResponseString
            'Catch ex As Exception
            '    WebResponseString = "Request Timeout" 'If any exception occur.
            '    rv = WebResponseString
            'End Try

            Return rv

        End Function
        Public Shared Sub SMSResponceInDB(ByVal SMSResponce As String, Message As String, Destination As String)
            'responce of sms Code|MobileNo:SMSUniqueNo separated by ,
            'SMSResponce = "1706|NA,1701|919411082815:7dac224a-cab2-4800-9742-f5774d6f9319,1701|919045929210:03c89ad5-e338-4bc0-b214-a35e0024ec67"

            Dim strMob() As String
            strMob = SMSResponce.Split(",")
            Dim DestinationMob() As String
            Dim DestinationMob1 As New List(Of String)
            DestinationMob = Destination.Split(",")
            For j = 0 To DestinationMob.Count - 1
                DestinationMob1.Add(DestinationMob(j).ToString)
            Next

            Dim SMSDate As String = Now.Date.Month.ToString("00") & "/" & Now.Date.Day.ToString("00") & "/" & Now.Date.Year.ToString("0000")

            Dim sqlStr As String = ""
            For count = 0 To strMob.Length - 1
                Dim MobileNo As String = ""
                Try
                    If strMob(count).ToString.Contains(":") = True Then
                        MobileNo = strMob(count).ToString.Substring(5, InStr(strMob(count).ToString, ":") - 6)
                    Else
                        MobileNo = strMob(count).ToString.Substring(5, strMob(count).ToString.Length - 5)
                    End If
                Catch ex As Exception

                End Try
                Dim DLRStatusCode As String = ""
                Try
                    DLRStatusCode = strMob(count).ToString.Substring(0, 4)
                Catch ex As Exception

                End Try
                sqlStr = "Insert into SMSDLR (SMSDate, MobileNo, DLRStatusCode, Message) Values (" & _
                    "'" & SMSDate & "'," & _
                    "'" & MobileNo & "'," & _
                    "'" & DLRStatusCode & "'," & _
                    "'" & SQLFixup(Message) & "')"

                ExecuteQuery_Update(sqlStr)
                DestinationMob1.RemoveAt(count)
            Next
            For count = 0 To DestinationMob1.Count - 1
                Dim MobileNo As String = DestinationMob1(count).ToString
                'If strMob(count).ToString.Contains(":") = True Then
                '    MobileNo = strMob(count).ToString.Substring(5, InStr(strMob(count).ToString, ":") - 6)
                'Else
                '    MobileNo = strMob(count).ToString.Substring(5, strMob(count).ToString.Length - 5)
                'End If
                Dim DLRStatusCode As String = 1026
                sqlStr = "Insert into SMSDLR (SMSDate, MobileNo, DLRStatusCode, Message) Values (" & _
                    "'" & SMSDate & "'," & _
                    "'" & MobileNo & "'," & _
                    "'" & DLRStatusCode & "'," & _
                    "'" & SQLFixup(Message) & "')"

                ExecuteQuery_Update(sqlStr)
            Next
        End Sub
        Public Shared Function SaveMessagetoDB(ByVal lstReceipient As List(Of String), ByVal SenderName As String, ByVal lstselectedID As List(Of String), ByVal ReceipientType As Integer, ByVal SMSMessage As String, ByVal userID As Integer, ByVal SenderN As String) As String
            'ReceipientType  0-student, 1-employee,2-individual
            Dim sqlStr As String = ""

            For i As Integer = 0 To lstselectedID.Count - 1
                If ReceipientType = 0 Then
                    sqlStr = "Insert into msgSentFromAdmin (msgRecipient,sentDate, sentTime, Subject, Body, SID,EmpID,SenderName, IsRead) Values (" & _
               Val(lstReceipient.Item(i)) & "," & _
               "'" & Now.Year & "/" & Now.Month & "/" & Now.Day & "'," & _
               "'" & Now.TimeOfDay.Hours & ":" & Now.TimeOfDay.Minutes & "'," & _
               "'" & SenderName & "'," & _
               "'" & SMSMessage & "'," & _
               Val(lstselectedID.Item(i)) & "," & _
               userID & "," & _
               "'" & SenderN & "'," & _
               "0" & _
               ")"
                ElseIf ReceipientType = 1 Then
                    sqlStr = "Insert into msgSentFromAdmin (msgRecipient,sentDate, sentTime, Subject, Body, EmpID,UserID,SenderName, IsRead) Values (" & _
                        Val(lstReceipient.Item(i)) & "," & _
               "'" & Now.Year & "/" & Now.Month & "/" & Now.Day & "'," & _
               "'" & Now.TimeOfDay.Hours & ":" & Now.TimeOfDay.Minutes & "'," & _
               "'" & SenderName & "'," & _
               "'" & SMSMessage & "'," & _
               Val(lstselectedID.Item(i)) & "," & _
               userID & "," & _
               "'" & SenderN & "'," & _
               "0" & _
               ")"
                Else
                    sqlStr = "Insert into msgSentFromAdmin (msgRecipient,sentDate, sentTime, Subject, Body, UserID,SenderName, IsRead) Values (" & _
                        Val(lstReceipient.Item(i)) & "," & _
               "'" & Now.Year & "/" & Now.Month & "/" & Now.Day & "'," & _
               "'" & Now.TimeOfDay.Hours & ":" & Now.TimeOfDay.Minutes & "'," & _
               "'" & SenderName & "'," & _
               "'" & SMSMessage & "'," & _
               userID & "," & _
               "'" & SenderN & "'," & _
               "0" & _
               ")"
                End If
                ''''''''''''for msg subject now sender is being used
                ExecuteQuery_Update(sqlStr)
            Next
            Return 0
        End Function

        Public Shared Function SQLFixup(TextIn As String) As String
            Dim Temp As String
            Temp = ReplaceStr(TextIn, "'", "''", 0)
            ' keyword = ReplaceStr(Temp, "|", "' & chr(124) & '", 0)
            'keyword = ReplaceStr(keyword, ">", "\>", 0)
            'keyword = ReplaceStr(keyword, "<", "&lt;", 0)
            'keyword = ReplaceStr(keyword, "{", "\{", 0)
            'keyword = ReplaceStr(keyword, "}", "\}", 0)
            Return Temp
        End Function
        Public Shared Function ReplaceStr(TextIn As String, ByVal SearchStr As String, ByVal Replacement As String, ByVal CompMode As Integer) As String
            Dim WorkText As String, Pointer As Integer
            If TextIn = "" Then
                ReplaceStr = ""
            Else
                WorkText = TextIn
                Pointer = InStr(1, WorkText, SearchStr, CompMode)
                Do While Pointer > 0
                    WorkText = Left(WorkText, Pointer - 1) & Replacement & _
                               Mid(WorkText, Pointer + Len(SearchStr))
                    Pointer = InStr(Pointer + Len(Replacement), WorkText, _
                                    SearchStr, CompMode)
                Loop
                ReplaceStr = WorkText
            End If
            Return ReplaceStr
        End Function

        Public Shared Function SendMySMS(ByVal SenderName As String, ByVal RecipientList As String, ByVal SMSMessage As String) As String
            'Wroking Link Can be Directly run on browser 
            Dim rv As String = ""

            Dim WebRequest As Net.WebRequest 'object for WebRequest
            Dim WebResonse As Net.WebResponse 'object for WebResponse

            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' ''''''''
            ' DEFINE PARAMETERS USED IN URL
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' ''''''''

            'To what server you need to connect to for submission
            'i.e. Dim Server As String = "smpp1.spheredge.com"
            Dim Server As String = GetSMSConfig(1) '"sms6.routesms.com"
            'Port that is to be used like 8080 or 8000
            Dim Port As String = GetSMSConfig(2) '"8080"
            'Username that is to be used for submission
            'i.e. Dim UserName As String = "tester"
            Dim UserName As String = GetSMSConfig(3) '"tinfotech"
            ' password that is to be used along with username
            'i.e. Dim Password As String = "password"
            Dim Password As String = GetSMSConfig(4) '"tinf1234"
            'What type of the message that is to be sent.
            '0:means plain text
            '1:means flash
            '2:means Unicode (Message content should be in Hex)
            '6:means Unicode Flash(Message content should be in Hex)
            Dim type As Integer = 0
            'Message content that is to be transmitted

            Dim Message As String = SMSMessage
            'Url Encode message
            Message = HttpUtility.UrlEncode(Message)
            If (type = 2) Or (type = 6) Then
                Message = ConvertToUnicode(Message)
            End If
            'Message = Message.Replace(" ", "%20")
            'Message = Message.Replace("'", "%27")
            'Require DLR or not
            '0:means DLR is not Required
            '1:means DLR is Required
            Dim DLR As Integer = 0
            'Sender Id to be used for submitting the message
            'i.e. Dim SenderName As String = "test"
            Dim Source As String = SenderName '"SFELIX"
            'Destinations to which message is to be sent For submitting more than(one)
            'destination at once destinations should be comma separated Like
            '91999000123,91999000124
            Dim Destination As String = RecipientList '"9897925900"
            '''''''CODE COMPLETE TO DEFINE PARAMETER''''''''''''''''
            Dim WebResponseString As String = ""
            'http://bhashsms.com/api/sendmsg.php?user=teamtisa1&pass=********&sender=Sender ID&phone=MobileNo1,MobileNo2..&text=Test SMS&priority=Priority&stype=smstype
            'http://bhashsms.com/api/sendmsg.php?user=teamtisa1&pass=123456789&sender=TISAAD&phone=8445990782&text=TEST&priority=Priority&stype=smstype


            'http://bhashsms.com/api/sendmsg.php?user=teamtisa1&pass=123456789&sender=TISAAD&phone=8445990782&text=TEST&priority=ndnd&stype=normal

            'Dim URL As String = "http://bhashsms.com/api/sendmsg.php?user=" & UserName & "&pass=" & Password & "&sender=" & SenderName & "&phone=" & Destination & _
            '"&text=" & Message & "&priority=ndnd&stype=normal"


            Dim URL As String = "http://bulksms.idiary.in/sms_api/sendsms.php?username=" & UserName & _
                 "&password=" & Password & "&mobile=" & Destination & "&sendername=" & Source & "&message=" & Message & "; "

            WebRequest = Net.HttpWebRequest.Create(URL) 'Hit URL Link
            WebRequest.Timeout = 25000

            Try
                WebResonse = WebRequest.GetResponse 'Get Response
                Dim reader As IO.StreamReader = New IO.StreamReader(WebResonse.GetResponseStream)
                'Read Response and store in variable
                WebResponseString = reader.ReadToEnd()
                WebResonse.Close()
                rv = WebResponseString
                SMSResponceInDB(rv, Message, Destination)
            Catch ex As Exception
                WebResponseString = "Request Timeout" 'If any exception occur.
                rv = WebResponseString
            End Try

            Return rv

        End Function

        'Function To Convert String to Unicode if MessageType=2 and 6.
        Public Shared Function ConvertToUnicode(ByVal str As String) As String
            Dim ArrayOFBytes() As Byte = System.Text.Encoding.Unicode.GetBytes(str)
            Dim UnicodeString As String = ""
            Dim v As Integer
            For v = 0 To ArrayOFBytes.Length - 1
                If v Mod 2 = 0 Then
                    Dim t As Integer = ArrayOFBytes(v)
                    ArrayOFBytes(v) = ArrayOFBytes(v + 1)
                    ArrayOFBytes(v + 1) = t
                End If
            Next
            For v = 0 To ArrayOFBytes.Length - 1
                Dim c As String = Hex$(ArrayOFBytes(v))
                If c.Length = 1 Then
                    c = "0" & c
                End If
                UnicodeString = UnicodeString & c
            Next
            Return UnicodeString
        End Function

        <System.Web.Script.Services.ScriptMethod(), _
        System.Web.Services.WebMethod()> _
        Public Shared Function ShowNames(prefixText As String) As List(Of String)
            Dim sqlStr As String = "Select SName From Student Where SName Like '%" & prefixText & "%'" ' Where ASID=" & ASID
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            Dim MyNames As New List(Of String)
            While myReader.Read
                MyNames.Add(myReader(0))
            End While
            myReader.Close()
            Return MyNames
        End Function

        Public Shared Function LastSRNoUsed(ByVal ASID As Integer) As String
            Dim rv As String = ""
            Dim sqlStr As String = ""
            'sqlStr = "Select Max(sid),StudentID From Student Where ASID=" & ASID & ""
            sqlStr = "Select RegNo From vw_Student Where SID=(Select Max(sid) From vw_Student Where ASID=" & ASID & ")"
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                rv = myReader(0)
            End While
            myReader.Close()

            Return rv
        End Function

        Public Shared Function GetSID(ByVal myAdminNo As String, ByVal ASID As Integer) As Integer
            Dim sqlStr As String = "Select Max(SID) From Student Where RegNo='" & myAdminNo & "' AND ASID=" & ASID
            Dim rv As Integer = 0
            Try
                rv = ExecuteQuery_ExecuteScalar(sqlStr)
            Catch ex As Exception

            End Try
            Return rv
        End Function
        Public Shared Function LoadSchool(ByRef myChk As CheckBoxList) As Integer

            Dim sqlStr As String = ""

            sqlStr = "Select SchoolName From SchoolMaster"

            myChk.Items.Clear()
            'myChk.Items.Add("")
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                myChk.Items.Add(myReader("SchoolName"))
                'lblTerm.Text = myReader("TermName")
            End While
            myReader.Close()
            Return 0
        End Function
        Public Shared Function SaveLastUsedASID(ByVal ASID As Integer, ByVal UserID As String) As Integer
            Dim sqlStr As String = ""
            sqlStr = "Update Users Set ASID=" & ASID & " where UserID='" & UserID & "'"
            ExecuteQuery_Update(sqlStr)
            Return 0
        End Function


        Public Shared Function FindSchoolDetails(ByVal myInputType As Integer, Optional ClassGroupID As Integer = 0) As String '1-SchoolName, 2-School Details
            Dim sqlStr As String = ""
            If myInputType = 1 Then
                If ClassGroupID = 0 Then
                    sqlStr = "Select SchoolName From Params"
                Else
                    sqlStr = "Select SchoolName From ClassGroups where ClassGroupID='" & ClassGroupID & "'"
                End If

            ElseIf myInputType = 2 Then
                sqlStr = "Select SchoolDetails From Params"
            End If

            Dim rv As String = ""
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                Try
                    rv = myReader(0)
                Catch ex As Exception
                    rv = ""
                End Try
            End While
            myReader.Close()

            Return rv

        End Function
        Public Shared Function FindSchoolDetails1(ByVal myInputType As Integer, Optional SchoolID As Integer = 0) As String '1-SchoolName, 2-School Details
            Dim sqlStr As String = ""

            If SchoolID = 0 Then
                sqlStr = "Select SchoolName From SchoolMaster where SchoolID='" & myInputType & "'"
            Else
                sqlStr = "Select SchoolAddress From SchoolMaster where SchoolID='" & myInputType & "'"
            End If



            Dim rv As String = ""
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                Try
                    rv = myReader(0)
                Catch ex As Exception
                    rv = ""
                End Try
            End While
            myReader.Close()

            Return rv

        End Function
        Public Shared Function LoadClassSection(ByVal ClassName As String, ByRef mycbo As DropDownList) As Integer
            Dim sqlstr As String = "Select Distinct SecName From vw_ClassStudent Where ClassName='" & ClassName & "' Order By SecName"

            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
            mycbo.Items.Clear()
            mycbo.Items.Add("")
            While myReader.Read
                mycbo.Items.Add(myReader(0))
            End While
            myReader.Close()

            Return 0

        End Function
        Public Shared Function LoadClassSection(ByRef myLst As ListBox) As Integer
            Dim sqlstr As String = "Select SecName From Sections Order By DisplayOrder"
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
            myLst.Items.Clear()
            While myReader.Read
                myLst.Items.Add(myReader(0))
            End While
            myReader.Close()

            Return 0

        End Function
        Public Shared Function GetAge(SmallDate As String, GreaterDate As String) As String
            Dim DateNow As Date = New DateTime(GreaterDate.Substring(6, 4), GreaterDate.Substring(3, 2), GreaterDate.Substring(0, 2))
            Dim DateDOB As Date = New DateTime(SmallDate.Substring(6, 4), SmallDate.Substring(3, 2), SmallDate.Substring(0, 2))
            Dim result As TimeSpan = DateNow.Subtract(DateDOB)
            Dim days As Integer = result.TotalDays
            Dim NumberOfYears As Integer = Math.Floor(days / 365)
            days = days - (NumberOfYears * 365)
            Dim NumberOfMonths As Integer = Math.Floor(days / 30)
            days = days - (NumberOfMonths * 30)
            Dim Age As String = ""
            If NumberOfYears = 0 Then
            Else
                Age += NumberOfYears & " Yrs "
            End If
            If NumberOfMonths = 0 Then
            Else
                Age += NumberOfMonths & " Months "
            End If
            If days = 0 Then
            Else
                Age += days & " Days"
            End If
            Return Age
        End Function
        Public Shared Function GetAgeOnDate() As String
            Dim sqlstr As String = "Select DOBAgeOnDate From Params"
            Dim rv As String = ""
            Dim a As Date = ExecuteQuery_ExecuteScalar(sqlstr)
            rv = a.ToString("dd/MM/yyyy")

            Return rv
        End Function
        Public Shared Function LoadClassSection(ByVal SchoolName As String, ByVal ClassName As String, ByRef mycbo As DropDownList) As Integer
            Dim sqlstr As String = "Select Distinct SecName,SectionDisplyOrder From vw_ClassStudent Where ClassName='" & ClassName & "'"
            If SchoolName = "" Or SchoolName = "ALL" Then
            Else
                sqlstr += " and SchoolName='" & SchoolName & "'"
            End If
            sqlstr += " Order By SectionDisplyOrder,SecName"
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
            mycbo.Items.Clear()
            mycbo.Items.Add("")
            While myReader.Read
                mycbo.Items.Add(myReader(0))
            End While
            myReader.Close()

            Return 0

        End Function
        Public Shared Function LoadStateCity(ByVal StateID As Integer, ByRef mycbo As DropDownList) As Integer
            Dim sqlstr As String = "Select Distinct CityName From CityMaster Where StateID='" & StateID & "' Order By CityName asc"

            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
            mycbo.Items.Clear()
            mycbo.Items.Add("")
            While myReader.Read
                mycbo.Items.Add(myReader(0))
            End While
            myReader.Close()

            Return 0

        End Function
        Public Shared Function LoadClassSubSection(SchoolName As String, ByVal ClassName As String, SecName As String, ByRef mycbo As DropDownList) As Integer

            Dim sqlstr As String = "Select distinct SubSecName,SubSecDisplayOrder From vw_ClassStudent Where ClassName='" & ClassName & "' and Secname='" & SecName & "'"
            If SchoolName = "" Or SchoolName = "ALL" Then
            Else
                sqlstr += " and SchoolName='" & SchoolName & "'"
            End If
            sqlstr += " Order By SubSecDisplayOrder,SubSecName"
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
            mycbo.Items.Clear()
            mycbo.Items.Add("")
            While myReader.Read
                Try
                    mycbo.Items.Add(myReader(0))
                Catch ex As Exception

                End Try

            End While
            myReader.Close()

            Return 0

        End Function

        Public Shared Function FindCSSID(SchoolName As String, ByVal ClassName As String, ByVal SecName As String, SubSecName As String) As Integer
            Dim sqlStr As String = "Select Max(CSSID) From vw_ClassStudent Where SchoolName='" & SchoolName & "' and SecName='" & SecName & "' AND ClassName='" & ClassName & "'"
            If SubSecName <> "" Then
                sqlStr += " And SubSecName='" & SubSecName & "'"
            Else
                sqlStr += " And SubSecName is null"
            End If
            Dim myid As Integer = 0
            Try
                myid = ExecuteQuery_ExecuteScalar(sqlStr)
            Catch ex As Exception
                myid = 0
            End Try

            Return myid
        End Function

        Public Shared Function UpdateClassDisplayOrder(ByRef lstClass As ListBox) As Integer

            Dim sqlstr As String = ""
            Dim i As Integer = 0

            For i = 0 To lstClass.Items.Count - 1
                sqlstr = "Update Classes Set DisplayOrder=" & i + 1 & " Where ClassName='" & lstClass.Items(i).Text & "'"
                ExecuteQuery_Update(sqlstr)
            Next

            Return 0

        End Function

        Public Shared Function FindTCNo() As Integer

            Dim sqlStr As String = "Select Max(TCID) From TC"
            Dim rv As Integer = 0
            Try
                rv = ExecuteQuery_ExecuteScalar(sqlStr)
            Catch ex As Exception
                rv = 0
            End Try

            Return rv

        End Function
        Public Shared Function ValidateDate(ByVal DD As Integer, ByVal MM As Integer, ByVal YYYY As Integer) As Boolean
            If YYYY < 0 Or YYYY > 9999 Then
                Return False
            End If

            If MM < 0 Or MM > 12 Then
                Return False
            End If

            Dim myDays As Integer = 0

            Select Case MM

                Case 1
                    If DD < 1 Or DD > 31 Then
                        Return False
                    End If

                Case 2
                    If Date.IsLeapYear(YYYY) = True Then
                        If DD < 1 Or DD > 29 Then
                            Return False
                        End If
                    Else
                        If DD < 1 Or DD > 28 Then
                            Return False
                        End If
                    End If

                Case 3
                    If DD < 1 Or DD > 31 Then
                        Return False
                    End If

                Case 4
                    If DD < 1 Or DD > 30 Then
                        Return False
                    End If

                Case 5
                    If DD < 1 Or DD > 31 Then
                        Return False
                    End If

                Case 6
                    If DD < 1 Or DD > 30 Then
                        Return False
                    End If

                Case 7
                    If DD < 1 Or DD > 31 Then
                        Return False
                    End If

                Case 8
                    If DD < 1 Or DD > 31 Then
                        Return False
                    End If

                Case 9
                    If DD < 1 Or DD > 30 Then
                        Return False
                    End If

                Case 10
                    If DD < 1 Or DD > 31 Then
                        Return False
                    End If

                Case 11
                    If DD < 1 Or DD > 30 Then
                        Return False
                    End If

                Case 12
                    If DD < 1 Or DD > 31 Then
                        Return False
                    End If

            End Select

            Return True

        End Function

        '--------------Fee Related--------------
        Public Shared Sub LoadMonths(chkMonth As CheckBoxList)
            chkMonth.Items.Clear()
            chkMonth.Items.Add("Apr")
            chkMonth.Items.Add("May")
            chkMonth.Items.Add("Jun")
            chkMonth.Items.Add("Jul")
            chkMonth.Items.Add("Aug")
            chkMonth.Items.Add("Sep")
            chkMonth.Items.Add("Oct")
            chkMonth.Items.Add("Nov")
            chkMonth.Items.Add("Dec")
            chkMonth.Items.Add("Jan")
            chkMonth.Items.Add("Feb")
            chkMonth.Items.Add("Mar")
        End Sub
        Public Shared Function LoadMonths(ByRef myCbo As DropDownList) As Integer
            myCbo.Items.Add("")
            myCbo.Items.Add("Jan")
            myCbo.Items.Add("Feb")
            myCbo.Items.Add("Mar")
            myCbo.Items.Add("Apr")
            myCbo.Items.Add("May")
            myCbo.Items.Add("Jun")
            myCbo.Items.Add("Jul")
            myCbo.Items.Add("Aug")
            myCbo.Items.Add("Sep")
            myCbo.Items.Add("Oct")
            myCbo.Items.Add("Nov")
            myCbo.Items.Add("Dec")
            Return 0
        End Function

        Public Shared Function LoadYears(ByRef myCbo As DropDownList) As Integer
            myCbo.Items.Clear()
            myCbo.Items.Add("")
            Dim i As Integer = 0
            For i = Now.Date.Year To Now.Date.AddYears(-10).Year Step -1
                myCbo.Items.Add(i)
            Next
            Return 0
        End Function


        Public Shared Function LoadPettyCashHeads(ByVal TransactionType As String, ByRef myLst As ListBox) As Integer
            Dim sqlstr As String = "Select PCHeadName From vwPettyCashHead Where TransTypeName='" & TransactionType & "' Order By PCHeadName"

            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
            myLst.Items.Clear()
            While myReader.Read
                myLst.Items.Add(myReader(0))
            End While
            myReader.Close()

            Return 0

        End Function

        Public Shared Function LoadPettyCashHeads(ByVal TransactionType As String, ByRef myCbo As DropDownList) As Integer
            Dim sqlstr As String = "Select PCHeadName From vwPettyCashHead Where TransTypeName='" & TransactionType & "' Order By PCHeadName"
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
            myCbo.Items.Clear()
            ' myCbo.Items.Add("")
            While myReader.Read
                myCbo.Items.Add(myReader(0))
            End While
            myReader.Close()

            Return 0

        End Function

        Public Shared Function FindPettyCashHeadID(ByVal PettyCashHeadName As String, ByVal TransTypeName As String) As Integer
            Dim sqlStr As String = "Select Max(PCHeadID) From vwPettyCashHead Where PCHeadName='" & PettyCashHeadName & "' AND TransTypeName='" & TransTypeName & "' "

            Dim myID As Integer = 0
            Try
                myID = ExecuteQuery_ExecuteScalar(sqlStr)
            Catch ex As Exception
                myID = 0
            End Try
            Return myID
        End Function

        Public Shared Function ClearPettyCashStatementTable() As Integer

            Dim sqlStr As String = "Delete From rptPettyCashStatements"

            ExecuteQuery_Update(sqlStr)

            Return 0

        End Function
        Public Shared Sub insertSyncLog(ByVal dbQuery As String, ByVal qrType As String, ByVal userID As Integer)
            'qrtype   I-insert; D-delete; U-update
            Dim sqlStr As String = ""
            sqlStr = "Insert into Sync_Log(dbQuery,qType,qrTimeStamp,UserID,isProcessed) Values('" & SQLFixup(dbQuery) & "','" & qrType & "','" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "','" & userID & "',0)"
            ExecuteQuery_Update(sqlStr)
        End Sub
    End Class
End Namespace