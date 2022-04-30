Imports System.Data.SqlClient
Imports System.Data
Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI.Control
Imports Microsoft.VisualBasic
Imports iDiary_V3.iDiary.CLS_idiary

Namespace iDiary
    Public Class CLS_iDiary_Exam
        Public Shared Function CheckDoubleEntryQuery(ByVal tblName As String, ByVal clmName As String, ByVal insertValue As String) As Integer

            Dim sqlStr As String = "Select Count(*) From " & tblName & " Where " & clmName & "='" & insertValue & "'"
            Dim rv As Integer = 0
            rv = ExecuteQuery_ExecuteScalar(sqlStr)

            Return rv
        End Function
        Public Shared Function CheckDoubleEntrySubject(ByVal tblName As String, ByVal clmName As String, ByVal insertValue As String, GrpID As Integer) As Integer

            Dim sqlStr As String = "Select Count(*) From " & tblName & " Where " & clmName & "='" & insertValue & "' and subGrpID=" & GrpID
            Dim rv As Integer = 0
            rv = ExecuteQuery_ExecuteScalar(sqlStr)

            Return rv
        End Function
        Public Shared Sub LoadClasses(ByRef cboClass As DropDownList, ByVal GroupNo As Integer, SchoolID As Integer)
            Dim sqlStr As String = "Select distinct ClassName,ClassID From vw_classStudent where SchoolID=" & SchoolID
            If GroupNo = 0 Then
            Else
                sqlStr &= " AND ExamGroupID=" & GroupNo
            End If
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            cboClass.Items.Clear()
            cboClass.Items.Add("")
            While myReader.Read
                cboClass.Items.Add(New ListItem(myReader(0), myReader(1)))
            End While
            myReader.Close()
        End Sub

        Public Shared Sub LoadClassSections(ByVal ClassName As String, ByRef cboSection As DropDownList)

            Dim sqlStr As String = "Select Distinct SecName,SecID From vw_ClassStudent Where ClassName='" & ClassName & "'"
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            cboSection.Items.Clear()
            cboSection.Items.Add("")
            While myReader.Read
                Try
                    cboSection.Items.Add(New ListItem(myReader(0), myReader(1)))
                Catch ex As Exception

                End Try
            End While
            myReader.Close()

        End Sub

        Public Shared Sub LoadTerms(ByRef cblTerm As CheckBoxList, type As Integer)
            'type 0 = major, type 1 = minor, type 2 = both
            cblTerm.Items.Clear()
            Dim sqlStr As String = "Select ExamTermID,ExamTermName From ExamTermMaster "
            If type <> 2 Then
                sqlStr &= " Where isMinor=" & type
            End If
            sqlStr &= " order by displayorder"
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                cblTerm.Items.Add(New ListItem(myReader(1), myReader(0)))
            End While
            myReader.Close()
        End Sub
        Public Shared Sub LoadMinorGroups(ByRef cboGroups As DropDownList, MajorGroupID As Integer)
            cboGroups.Items.Clear()
            cboGroups.Items.Add("")
            Dim sqlStr As String = "Select SubGroupName,SubGrpID From ExamSubjectGroupMaster Where isMinorGroup=1 AND MajorGroupID=" & MajorGroupID
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                cboGroups.Items.Add(New ListItem(myReader(0), myReader(1)))
            End While
            myReader.Close()
        End Sub
        Public Shared Sub LoadMinorGroups(ByRef lb As ListBox, MajorGroupID As Integer)
            lb.Items.Clear()
            Dim sqlStr As String = "Select SubGroupName,SubGrpID From ExamSubjectGroupMaster Where isMinorGroup=1 AND MajorGroupID=" & MajorGroupID
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                lb.Items.Add(New ListItem(myReader(0), myReader(1)))
            End While
            myReader.Close()
        End Sub
        Public Shared Sub LoadMinorExamTerms(ByRef cboTerm As DropDownList, SubgrpID As Integer, MajorTermID As Integer)
            'type 0 = major, type 1 = minor, type 2 = both
            cboTerm.Items.Clear()
            Dim sqlStr As String = "Select ExamTermMinor,ExamMinorTermID From vw_ExamTerms Where ExamMajorTermID=" & MajorTermID
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                cboTerm.Items.Add(New ListItem(myReader(0), myReader(1)))
            End While
            myReader.Close()
        End Sub
       
        Public Shared Sub LoadExamTerms(ByRef cblTerm As DropDownList, grpID As Integer, type As Integer)
            'type 0 = major, type 1 = minor, type 2 = both
            cblTerm.Items.Clear()
            Dim sqlStr As String = "Select ExamTermID,ExamTermName From ExamTermMaster "
            If type <> 2 Then
                sqlStr &= " Where isMinor=" & type
            End If
            sqlStr &= " order by displayorder"
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                cblTerm.Items.Add(New ListItem(myReader(1), myReader(0)))
            End While
            myReader.Close()
        End Sub
        Public Shared Sub LoadExamTermsByGroups(ByRef cblTerm As DropDownList, grpID As Integer, type As Integer)
            cblTerm.Items.Clear()
            Dim sqlStr As String = "Select Distinct ExamMajorTermID,ExamTermMajor,DisplayOrderMaj From vw_ExamTerms Where ExamGroupID=" & grpID & ""
            sqlStr &= " order by DisplayOrderMaj,ExamMajorTermID,ExamTermMajor "
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                cblTerm.Items.Add(New ListItem(myReader(1), myReader(0)))
            End While
            myReader.Close()
        End Sub
        Public Shared Sub LoadSubjectGroupExamTermsMajor(ByRef cboTerm As DropDownList, SubgrpID As Integer, ExamGrpID As Integer)
            cboTerm.Items.Clear()
            Dim sqlStr As String = "Select Distinct ExamMajorTermID,ExamTermMajor,DisplayOrderMaj From vw_ExamTerms Where ExamGroupID=" & ExamGrpID & ""
            If SubgrpID <> 0 Then
                sqlStr += " AND SubGrpID=" & SubgrpID
            End If

            sqlStr &= " order by DisplayOrderMaj,ExamMajorTermID,ExamTermMajor "
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                cboTerm.Items.Add(New ListItem(myReader(1), myReader(0)))
            End While
            myReader.Close()
        End Sub
        Public Shared Sub LoadSubjectGroupExamTermsMinor(ByRef cboTerm As DropDownList, TermID As Integer, ExamGrpID As Integer, subGrpID As Integer)
            cboTerm.Items.Clear()
            Dim sqlStr As String = "Select Distinct ExamMinorTermID,ExamTermMinor,DisplayOrderMin From vw_ExamTerms Where SubGrpID=" & subGrpID & " AND  ExamGroupID=" & ExamGrpID & " AND ExamMajorTermID=" & TermID
            sqlStr &= " order by DisplayOrderMin,ExamMinorTermID,ExamTermMinor"
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                cboTerm.Items.Add(New ListItem(myReader(1), myReader(0)))
            End While
            myReader.Close()
        End Sub
        Public Shared Sub LoadSubjectGroups(ByRef cboExamGroup As DropDownList, type As Integer, Optional ExamGroupID As String = "")
            'type 1 = minor, type 0 = major, type 2 = both
            cboExamGroup.Items.Clear()
            cboExamGroup.Items.Add("")
            Dim sqlStr As String = "Select subGrpID,subGroupName From ExamSubjectGroupMaster where subGrpID>0"
            If type <> 2 Then
                sqlStr &= " and isMinorGroup=" & type
            End If
            If ExamGroupID <> "" Then
                sqlStr &= " and ExamGroupID like '%:" & ExamGroupID & ":%'"
            End If

            sqlStr &= " order by displayorder,majorgroupid"
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                cboExamGroup.Items.Add(New ListItem(myReader(1), myReader(0)))
            End While
            myReader.Close()
        End Sub
        Public Shared Sub LoadSubjectGroups(ByRef lb As ListBox, type As Integer, Optional ExamGroupID As String = "")
            'type 1 = minor, type 0 = major, type 2 = both
            lb.Items.Clear()
            ' lb.Items.Add("")
            Dim sqlStr As String = "Select subGrpID,subGroupName From ExamSubjectGroupMaster where subGrpID>0"
            If type <> 2 Then
                sqlStr &= " and isMinorGroup=" & type
            End If
            If ExamGroupID <> "" Then
                sqlStr &= " and ExamGroupID like '%:" & ExamGroupID & ":%'"
            End If

            sqlStr &= " order by displayorder,isMinorGroup"
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                lb.Items.Add(New ListItem(myReader(1), myReader(0)))
            End While
            myReader.Close()
        End Sub

        Public Sub LoadStatus(ByRef cboStatus As DropDownList)

            Dim sqlstr As String = "Select StatusName From StatusMaster"
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
            cboStatus.Items.Clear()
            While myReader.Read
                cboStatus.Items.Add(myReader(0))
            End While
            myReader.Close()
        End Sub

        Public Function CheckPermission(ByVal MyClassName As String, ByVal mySecName As String, ByVal myLoginID As String) As Boolean
            Dim sqlStr As String = ""

            sqlStr = "Select Count(*) From vw_Users Where LoginID='" & myLoginID & "' AND ClassName='" & MyClassName & "' AND SecName='" & mySecName & "'"
            Dim rv As Integer = ExecuteQuery_ExecuteScalar(sqlStr)

            If rv > 0 Then
                Return True
            Else
                Return False
            End If

        End Function

        Public Shared Function CheckPermissionSubject(ByVal MyClassName As String, ByVal mySecName As String, ByVal mySubject As String, ByVal myLoginID As String, ByVal schoolID As Integer) As Boolean
            Dim sqlStr As String = ""
            sqlStr = "Select max(isPermissionApplicable) From vw_UserSubjectPermission Where LoginID='" & myLoginID & "' AND ClassName='" & MyClassName & "' AND SecName='" & mySecName & "' AND SubjectName='" & mySubject & "' and SchoolID=" & schoolID
            Dim rv As Integer = 0
            Try
                rv = ExecuteQuery_ExecuteScalar(sqlStr)
            Catch ex As Exception
                rv = 0
            End Try
            If rv > 0 Then
                Return True
            Else
                Return False
            End If

        End Function
        Public Shared Function CheckExamConfig(type As Integer) As Integer
            Dim sqlStr As String = "Select "
            Select Case type
                Case 1
                    sqlStr += " ActivitySubjectGroupID "
                Case 2
                    sqlStr += " isMarksEntryAllowed "
                Case 3
                    sqlStr += " isProcessingAllowed"
                Case 4
                    sqlStr += " isPermissionApplicable "
            End Select
            sqlStr += " From ExamParams"
            Dim rv As Integer = ExecuteQuery_ExecuteScalar(sqlStr)

            If rv > 0 Then
                Return True
            Else
                Return False
            End If

        End Function

        Public Shared Function LoadSubjectRemarks(ByRef myCbo As DropDownList) As Integer

            Dim sqlstr As String = "Select SubjectRemarkName From SubjectRemarkMaster Order By SubjectRemarkName"
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
            myCbo.Items.Clear()
            myCbo.Items.Add("")
            While myReader.Read
                myCbo.Items.Add(myReader(0))
            End While
            myReader.Close()
            Return 0

        End Function

        Public Shared Function FindSubjectRemarksID(ByVal SubjectRemark As String) As Integer

            Dim sqlstr As String = "Select Max(SubjectRemarkID) From SubjectRemarkMaster Where SubjectRemarkName='" & SubjectRemark & "'"
            Dim rv As Integer = 0
            Try
                rv = ExecuteQuery_ExecuteScalar(sqlstr)
            Catch ex As Exception
                rv = 0
            End Try
            Return rv

        End Function


        Public Shared Function FindSubjectTypeID(ByVal SubjectTypeName As String) As Integer
            Dim sqlstr As String = "Select Max(SubjectTypeID) From SubjectTypeMaster Where SubjectTypeName='" & SubjectTypeName & "'"
            Dim rv As Integer = 0
            Try
                rv = ExecuteQuery_ExecuteScalar(sqlstr)
            Catch ex As Exception
                rv = 0
            End Try

            Return rv
        End Function


        Public Shared Function LoadSubjectClassWise(ByRef myCbo As DropDownList, ByVal ClassName As String, ByVal SecName As String, ByVal ASID As String, Optional ByVal SubGrpID As Integer = 0, Optional ByVal ApplicableInTimeTable As Integer = 0, Optional ByVal SchoolName As String = "") As Integer

            Dim sqlstr As String = ""
            If SubGrpID = 0 Then
                sqlstr = "Select Distinct SubjectName,SubjectID,priority From vw_ExamSubjectMapping Where  ClassName='" & ClassName & "' AND SecName='" & SecName & "' AND ASID=" & ASID
            Else
                sqlstr = "Select Distinct SubjectName,SubjectID,priority From vw_ExamSubjectMapping Where  ClassName='" & ClassName & "' AND SecName='" & SecName & "' AND SubGrpID=" & SubGrpID & " AND ASID=" & ASID
            End If
            If SchoolName <> "" Then
                sqlstr &= " and SchoolID='" & SchoolName & "'"
            End If
            If ApplicableInTimeTable = 1 Then
                sqlstr += " AND ApplicableInTimeTable=1"
            End If
            sqlstr += " Order By Priority"
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
            myCbo.Items.Clear()
            'myCbo.Items.Add("")
            While myReader.Read
                myCbo.Items.Add(New ListItem(myReader(0), myReader(1)))
            End While
            myReader.Close()
            Return 0

        End Function

        Public Shared Function LoadSubjects(ByRef myCbo As DropDownList) As Integer
            Dim sqlstr As String = "Select Distinct SubjectName,SubjectID From vw_ExamSubjects Order By SubjectName"
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
            myCbo.Items.Clear()
            myCbo.Items.Add("")
            While myReader.Read
                myCbo.Items.Add(New ListItem(myReader(0), myReader(1)))
            End While
            myReader.Close()
            Return 0
        End Function

        Public Shared Function LoadRemarkSubjects(ByRef myCbo As DropDownList) As Integer
            Dim sqlstr As String = "Select Distinct SubjectName,SubjectID From vw_ExamSubjectmapping Where EntryType=2 or DisplayType=2 Order By SubjectName"
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
            myCbo.Items.Clear()
            myCbo.Items.Add("")
            While myReader.Read
                myCbo.Items.Add(New ListItem(myReader(0), myReader(1)))
            End While
            myReader.Close()
            Return 0
        End Function


        Public Shared Function LoadSubjectsFromGroups(ByRef myCbo As DropDownList, ByVal SubgrpID As String) As Integer
            Dim sqlstr As String = "Select Distinct SubjectName,SubjectID From vw_ExamSubjects Where SubGrpID='" & SubgrpID & "' Order By SubjectName"
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
            myCbo.Items.Clear()
            myCbo.Items.Add("")
            While myReader.Read
                myCbo.Items.Add(New ListItem(myReader(0), myReader(1)))
            End While
            myReader.Close()
            Return 0
        End Function

        Public Shared Function LoadSubjectsFromGroups(ByRef myLst As ListBox, ByVal SubgrpID As String) As Integer
            Dim sqlstr As String = "Select Distinct SubjectName,SubjectID From vw_ExamSubjects Where SubGrpID='" & SubgrpID & "' Order By SubjectName"
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
            myLst.Items.Clear()
            While myReader.Read
                myLst.Items.Add(New ListItem(myReader(0), myReader(1)))
            End While
            myReader.Close()
            Return 0
        End Function
        Public Shared Function FindSubjectID(ByVal SubjectName As String, ByVal SubjectTypeName As String) As Integer
            Dim sqlstr As String = "Select Max(SubjectID) From vw_ExamSubjects Where SubjectName='" & SubjectName & "' and SubjectTypeName='" & SubjectTypeName & "'"
            Dim rv As Integer = 0
            Try
                rv = ExecuteQuery_ExecuteScalar(sqlstr)
            Catch
                rv = 0
            End Try
            Return rv

        End Function

        Public Shared Function FindSubjectID(ByVal SubjectName As String) As Integer
            Dim sqlstr As String = "Select Max(SubjectID) From vw_ExamSubjects Where SubjectName='" & SubjectName & "'"
            Dim rv As Integer = 0
            Try
                rv = ExecuteQuery_ExecuteScalar(sqlstr)
            Catch
                rv = 0
            End Try
            Return rv
        End Function


        Public Shared Function IsClassTeacher(CSSID As Integer) As Boolean

            Dim rv As Boolean = False
            Dim sqlStr As String = ""
            Dim cnt As Integer = 0
            sqlStr = "Select IsClassTeacher From UserSubjectPermissions Where CSSID=" & CSSID
            Try
                cnt = ExecuteQuery_ExecuteScalar(sqlStr)
            Catch ex As Exception

            End Try

            If cnt = 0 Then
                rv = False
            Else
                rv = True
            End If

            Return rv
        End Function

        Public Shared Function IsSubjectPermissionApplicable(EmpID As Integer, CSSID As Integer, SubID As Integer) As Boolean

            Dim rv As Boolean = False
            Dim sqlStr As String = ""
            Dim cnt As Integer = 0
            sqlStr = "Select IsPermissionApplicable From UserSubjectPermissions Where EmpID=" & EmpID & " And CSSID=" & CSSID & " And SubjectID=" & SubID
            Try
                cnt = ExecuteQuery_ExecuteScalar(sqlStr)
            Catch ex As Exception

            End Try

            If cnt = 0 Then
                rv = False
            Else
                rv = True
            End If

            Return rv
        End Function

        Public Shared Function IsSubjectApplicable(EmpID As Integer, CSSID As Integer, SubID As Integer) As Boolean

            Dim rv As Boolean = False
            Dim sqlStr As String = ""
            Dim cnt As Integer = 0
            sqlStr = "Select IsTeaches From UserSubjectPermissions Where EmpID=" & EmpID & " And CSSID=" & CSSID & " And SubjectID=" & SubID
            Try
                cnt = ExecuteQuery_ExecuteScalar(sqlStr)
            Catch ex As Exception

            End Try

            If cnt = 0 Then
                rv = False
            Else
                rv = True
            End If
            Return rv
        End Function
        Public Shared Function LoadMasterData(ByVal colName As String, ByVal tblName As String, ByRef myLst As ListBox, Optional orderBY As String = "") As Integer
            Dim sqlStr As String = ""
            If orderBY = "" Then
                sqlStr = "Select distinct " & colName & " From " & tblName & " order by " & colName
            Else
                sqlStr = "Select " & colName & " From " & tblName & " order by " & orderBY
            End If
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            myLst.Items.Clear()
            While myReader.Read
                myLst.Items.Add(myReader(0))
            End While
            myReader.Close()
            Return 0
        End Function
        Public Shared Function LoadMasterData(ByVal colName As String, ByVal tblName As String, ByRef myCbo As DropDownList) As Integer
            Dim sqlStr As String = "Select distinct " & colName & " From " & tblName & " order by " & colName
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            myCbo.Items.Clear()
            While myReader.Read
                myCbo.Items.Add(myReader(0))
            End While
            myReader.Close()
            Return 0
        End Function

        Public Shared Sub LoadRemarks(cbo As DropDownList, ByVal SubID As Integer, ClassID As Integer)

            Dim sqlStr As String = "Select RemarkID, remarkName From ExamRemarksMaster where SubjectID=" & SubID & " AND ClassID=" & ClassID

            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            cbo.Items.Clear()
            While myReader.Read
                cbo.Items.Add(New ListItem(myReader(1), myReader(0)))
            End While
            myReader.Close()
        End Sub

        Public Shared Function getMappedSIDwithSubjects(ByVal classID As Integer, ByVal secID As Integer, ByVal SubjectID As Integer) As List(Of Integer)
            Dim lstSid As New List(Of Integer)

            Dim sqlStr As String = "select SID from vw_studentsubjectmapping where classID='" & classID & "' and SecID='" & secID & "' and SubjectID=" & SubjectID
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                lstSid.Add(myReader(0))
            End While
            myReader.Close()
            Return lstSid
        End Function

        Public Shared Function getMappedSIDwithActivity(ByVal SubjectID As Integer, subGrpID As Integer) As List(Of Integer)
            Dim lstSid As New List(Of Integer)

            Dim sqlStr As String = "select distinct sid from [ExamStudentActivityMapping] where SubGrpID=" & subGrpID & " and (Act1subjectID=" & SubjectID & " or Act2subjectID=" & SubjectID & ")"
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                lstSid.Add(myReader(0))
            End While
            myReader.Close()
            Return lstSid
        End Function

        Public Shared Function checkSubSectionExists(ByVal ClassID As Integer, ByVal SecID As Integer) As Integer
            Dim sqlStr As String = "Select Count(SubSecID) From ClassStudent Where SubSecID is not NULL and SubSecID <> 0  AND ClassID=" & ClassID & " AND SecID =" & SecID
            Dim myID As Integer = 0
            Try
                myID = ExecuteQuery_ExecuteScalar(sqlStr)
            Catch ex As Exception
                myID = 0
            End Try

            Return myID
        End Function

        Public Shared Function getExamGroupIDfromClass(ByVal className As String) As Integer
            Dim sqlStr As String = "Select max(ExamGroupID) From Classes Where ClassName='" & className & "'"
            Dim myID As Integer = 0
            Try
                myID = ExecuteQuery_ExecuteScalar(sqlStr)
            Catch ex As Exception
                myID = 0
            End Try

            Return myID
        End Function

        Public Shared Function getExamParams(type As Integer) As String

            Dim sqlStr As String = "Select "
            Select Case type
                Case 1 : sqlStr += " ActivitySubjectGroupID "
                Case 2 : sqlStr += " isMarksEntryAllowed "
                Case 3 : sqlStr += " isProcessingAllowed "
                Case 4 : sqlStr += " isPermissionApplicable "
            End Select
            sqlStr += " from ExamParams"
            Dim rv As String = ""
            Try
                rv = ExecuteQuery_ExecuteScalar(sqlStr)
            Catch ex As Exception
                rv = ""
            End Try
            Return rv
        End Function

        Public Shared Function getGradingForGroup(subGrpID As Integer, examGroupID As Integer) As List(Of String)
            Dim lstGrading As New List(Of String)
            Dim sqlStr As String = ""
            sqlStr = "select distinct Grade from vw_ExamGradeMapping where subGrpID=" & subGrpID & " AND examGroupID=" & examGroupID
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                lstGrading.Add(myReader(0))
            End While
            Return lstGrading
        End Function

        Public Shared Function getSchoolID(schoolName As String) As Integer
            Dim rv As Integer = 0
            Dim sqlStr As String = ""
            sqlStr = "select max(SchoolID) from SchoolMaster where SchoolName='" & schoolName & "'"
            Try
                rv = ExecuteQuery_ExecuteScalar(sqlStr)
            Catch ex As Exception

            End Try
            Return rv
        End Function
    End Class

End Namespace

