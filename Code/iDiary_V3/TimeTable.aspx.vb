Imports iDiary_V3.iDiary.CLS_idiary
Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_iDiary_Exam

Public Class TimeTable
    Inherits System.Web.UI.Page

    Dim sqlStr As String = ""
   


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            InitControls()
        End If
    End Sub

    Private Sub InitControls()
      
        gvTimeTable.Visible = False
        LoadMasterInfo(2, cboClass)
        cboSection.Items.Clear()
    End Sub

    Protected Sub cboPeriod1_SelectedIndexChanged(ByVal sender As Object, e As EventArgs) Handles cboPeriod1.SelectedIndexChanged, cboPeriod2.SelectedIndexChanged, cboPeriod3.SelectedIndexChanged, cboPeriod4.SelectedIndexChanged, cboPeriod5.SelectedIndexChanged, cboPeriod6.SelectedIndexChanged, cboPeriod7.SelectedIndexChanged, cboPeriod8.SelectedIndexChanged
        Dim cntrl As String = sender.ToString
        If sender Is cboPeriod1 Then
            LoadTeachers(cboTeacher1, cboPeriod1.Text)
            cboPeriod1.Focus()
        ElseIf sender Is cboPeriod2 Then
            LoadTeachers(cboTeacher2, cboPeriod2.Text)
            cboPeriod2.Focus()
        ElseIf sender Is cboPeriod3 Then
            LoadTeachers(cboTeacher3, cboPeriod3.Text)
            cboPeriod3.Focus()
        ElseIf sender Is cboPeriod4 Then
            LoadTeachers(cboTeacher4, cboPeriod4.Text)
            cboPeriod4.Focus()
        ElseIf sender Is cboPeriod5 Then
            LoadTeachers(cboTeacher5, cboPeriod5.Text)
            cboPeriod5.Focus()
        ElseIf sender Is cboPeriod6 Then
            LoadTeachers(cboTeacher6, cboPeriod6.Text)
            cboPeriod6.Focus()
        ElseIf sender Is cboPeriod7 Then
            LoadTeachers(cboTeacher7, cboPeriod7.Text)
            cboPeriod7.Focus()
        ElseIf sender Is cboPeriod8 Then
            LoadTeachers(cboTeacher8, cboPeriod8.Text)
            cboPeriod8.Focus()
        End If

    End Sub


    Private Sub LoadSubjects(cboMy As DropDownList)
        Dim ClassID As Integer = FindMasterID(2, cboClass.Text)
        Dim SecID As Integer = 0
        '0
       
       

        sqlStr = "Select SubjectName From vwSubjectMapping Where ClassID=" & ClassID & " AND SecID=" & SecID
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        cboMy.Items.Clear()
        cboMy.Items.Add(" ")
        While myReader.Read
            cboMy.Items.Add(myReader(0))
        End While
        myReader.Close()
        
        
    End Sub

    Private Sub LoadTeachers(cboMy As DropDownList, ByVal subjectName As String)
        Dim ClassID As Integer = FindMasterID(2, cboClass.Text)
        Dim SecID As Integer = 0
        '0
        Dim subjectID As Integer = FindSubjectID(subjectName)
       
       

        sqlStr = "SELECT EmpName FROM vw_SubjectTeacherMapping WHERE SubjectID=" & subjectID

        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        cboMy.Items.Clear()
        cboMy.Items.Add(" ")
        While myReader.Read
            cboMy.Items.Add(myReader(0))
        End While
        myReader.Close()


    End Sub

    Protected Sub cboClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboClass.SelectedIndexChanged
        LoadClassSection("", cboClass.Text, cboSection)
    End Sub


    Protected Sub cboSection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSection.SelectedIndexChanged
        LoadSubjects(cboPeriod1)
        LoadSubjects(cboPeriod2)
        LoadSubjects(cboPeriod3)
        LoadSubjects(cboPeriod4)
        LoadSubjects(cboPeriod5)
        LoadSubjects(cboPeriod6)
        LoadSubjects(cboPeriod7)
        LoadSubjects(cboPeriod8)


        Dim ClassID As Integer = FindMasterID(2, cboClass.Text)
        Dim SecID As Integer = 0
        '0
        gvTimeTable.Visible = True
        SqlDataSourceTimeTable.SelectCommand = "Select * from TimeTable Where classID=" & ClassID & " AND secID=" & SecID & ""
        gvTimeTable.DataBind()
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        lblStatus.Text = ""
        Dim ClassID As Integer = FindMasterID(2, cboClass.Text)
        Dim SecID As Integer = 0
        '0




        sqlStr = "DELETE from timetable where classID=" & ClassID & " AND secID=" & SecID & " AND dayID=" & cboDays.SelectedIndex & ""


        ExecuteQuery_Update(SqlStr)

        '''''''''''''''''''''''''''different table for IDs
        sqlStr = "DELETE from timetableID where classID=" & ClassID & " AND secID=" & SecID & " AND dayID=" & cboDays.SelectedIndex & ""


        ExecuteQuery_Update(SqlStr)



        ''''''''''''''''''''''''IDs
        Dim teacherID As Integer = 0
        Dim subjectID As Integer = 0
        Dim flag As Integer = 0
        For i = 1 To 8
            subjectID = FindSubjectID(CType(FindControlRecursive(Page.Page, "cboPeriod" & i), DropDownList).Text)
            teacherID = FindEmployeeID(2, "Teaching", CType(FindControlRecursive(Page.Page, "cboTeacher" & i), DropDownList).Text)

            sqlStr = "select 1 from timetableID where periodID=" & i & " AND dayID=" & cboDays.SelectedIndex & " AND teacherID=" & teacherID


            flag = ExecuteQuery_ExecuteScalar(sqlStr)

            If flag = 1 Then          '''''''''entry exists
                lblStatus.Text = "Mapping of " & CType(FindControlRecursive(Page.Page, "cboPeriod" & i), DropDownList).Text & " and " & CType(FindControlRecursive(Page.Page, "cboTeacher" & i), DropDownList).Text & " Already exists !"
                Exit Sub
            End If
            sqlStr = "INSERT INTO timeTableID(classID,SecID,DayID,periodID,subjectID,teacherID)"
            sqlStr &= "VALUES('" & ClassID & "','" & SecID & "','" & cboDays.SelectedIndex & "','" & i & "','" & subjectID & "', '" & teacherID & "')"



            ExecuteQuery_Update(SqlStr)

        Next

        ''''''''''''''VIEW
        sqlStr = "INSERT INTO TimeTable(classID,SecID,DayID,"
        For i As Integer = 1 To 8
            sqlStr &= "per" & i & "sub, per" & i & "Teacher,"
        Next
        sqlStr = sqlStr.Substring(0, sqlStr.Length - 1) & " ) VALUES ('" & ClassID & "','" & SecID & "','" & cboDays.SelectedIndex & "','" & (cboPeriod1.Text) & "', '" & (cboTeacher1.Text) & "', '" & (cboPeriod2.Text) & "', '" & (cboTeacher2.Text) & "', '" & (cboPeriod3.Text) & "', '" & (cboTeacher3.Text) & "', '" & (cboPeriod4.Text) & "', '" & (cboTeacher4.Text) & "',"
        sqlStr &= "'" & (cboPeriod5.Text) & "', '" & (cboTeacher5.Text) & "', '" & (cboPeriod6.Text) & "', '" & (cboTeacher6.Text) & "', '" & (cboPeriod7.Text) & "', '" & (cboTeacher7.Text) & "', '" & (cboPeriod8.Text) & "', '" & (cboTeacher8.Text) & "')"



        ExecuteQuery_Update(SqlStr)




        SqlDataSourceTimeTable.SelectCommand = "Select * from TimeTable Where classID=" & ClassID & " AND secID=" & SecID & ""
        gvTimeTable.DataBind()
    End Sub

    Protected Sub cboDays_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDays.SelectedIndexChanged
        Dim ClassID As Integer = FindMasterID(2, cboClass.Text)
        Dim SecID As Integer = 0
        Dim dayID As Integer = cboDays.SelectedIndex



        sqlStr = "Select "
        For i As Integer = 1 To 8
            sqlStr &= "per" & i & "sub, per" & i & "Teacher,"
        Next
        sqlStr = sqlStr.Substring(0, sqlStr.Length - 1)
        sqlStr &= " FROM TimeTable Where DayID=" & dayID & " AND ClassID=" & ClassID & " AND secID=" & SecID

        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)

        While myReader.Read
            cboPeriod1.Text = myReader("per1Sub")
            LoadTeachers(cboTeacher1, cboPeriod1.Text)
            cboTeacher1.Text = myReader("per1Teacher")

            cboPeriod2.Text = myReader("per2Sub")
            LoadTeachers(cboTeacher2, cboPeriod2.Text)
            cboTeacher2.Text = myReader("per2Teacher")

            cboPeriod3.Text = myReader("per3Sub")
            LoadTeachers(cboTeacher3, cboPeriod3.Text)
            cboTeacher3.Text = myReader("per3Teacher")

            cboPeriod4.Text = myReader("per4Sub")
            LoadTeachers(cboTeacher4, cboPeriod4.Text)
            cboTeacher4.Text = myReader("per4Teacher")

            cboPeriod5.Text = myReader("per5Sub")
            LoadTeachers(cboTeacher5, cboPeriod5.Text)
            cboTeacher5.Text = myReader("per5Teacher")

            cboPeriod6.Text = myReader("per6Sub")
            LoadTeachers(cboTeacher6, cboPeriod6.Text)
            cboTeacher6.Text = myReader("per6Teacher")

            cboPeriod7.Text = myReader("per7Sub")
            LoadTeachers(cboTeacher7, cboPeriod7.Text)
            cboTeacher7.Text = myReader("per7Teacher")

            cboPeriod8.Text = myReader("per8Sub")
            LoadTeachers(cboTeacher8, cboPeriod8.Text)
            cboTeacher8.Text = myReader("per8Teacher")

        End While
        myReader.Close()



        gvTimeTable.Visible = True
        SqlDataSourceTimeTable.SelectCommand = "Select * from TimeTable Where classID=" & ClassID & " AND secID=" & SecID & ""
        gvTimeTable.DataBind()
    End Sub

    Protected Sub gvTimeTable_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvTimeTable.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim myVal As String = e.Row.Cells(0).Text
            If myVal = "1" Then
                e.Row.Cells(0).Text = "Monday"
            ElseIf myVal = "2" Then
                e.Row.Cells(0).Text = "Tuesday"
            ElseIf myVal = "3" Then
                e.Row.Cells(0).Text = "Wednesday"
            ElseIf myVal = "4" Then
                e.Row.Cells(0).Text = "Thursday"
            ElseIf myVal = "5" Then
                e.Row.Cells(0).Text = "Friday"
            ElseIf myVal = "6" Then
                e.Row.Cells(0).Text = "Saturday"
            End If
        End If
    End Sub

    Public Shared Function FindControlRecursive(root As Control, id As String) As Control
        If root.ID = id Then
            Return root
        End If

        Return root.Controls.Cast(Of Control)().[Select](Function(c) FindControlRecursive(c, id)).FirstOrDefault(Function(c) c IsNot Nothing)
    End Function
End Class