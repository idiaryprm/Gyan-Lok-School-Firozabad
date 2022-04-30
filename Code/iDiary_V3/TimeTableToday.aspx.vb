Imports iDiary_V3.iDiary.CLS_idiary
Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_iDiary_Exam


Public Class TimeTableToday
    Inherits System.Web.UI.Page

    Dim sqlStr As String = ""
   


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            InitControls()
        End If
    End Sub

    Private Sub InitControls()
        cboDays.Text = Now.Date.DayOfWeek.ToString
        gvTimeTable.Visible = False
        LoadMasterInfo(2, cboClass)
        cboSection.Items.Clear()
        SqlDataSourceEMP.SelectCommand = "SELECT [AttDate], [EmpID], [Att], [EmpName], [Mob] FROM [vw_Employee_Attendance] WHERE EmpCatName='Teaching' AND Att=0 AND AttDate='" & Now.Date.ToString("yyyy/MM/dd") & "'"
        GridView1.DataBind()
    End Sub

    Private Sub LoadSubjects(cboMy As DropDownList)
        Dim ClassID As Integer = FindMasterID(2, cboClass.Text)
        Dim SecID As Integer = 0
       
       

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
        Dim ClassID As Integer = FindMasterID(2, cboClass.Text)
        Dim SecID As Integer = 0
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

    Private Sub LoadAbsentees()



        sqlStr = "SELECT EmpName FROM [vw_Employee_Attendance] WHERE EmpCatName='Teaching' AND Att=0 AND Attdate='" & Now.Date & "'"

        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        'cboMy.Items.Clear()
        'cboMy.Items.Add(" ")
        'While myReader.Read
        '    cboMy.Items.Add(myReader(0))
        'End While
        myReader.Close()


    End Sub


    Public Shared Function FindControlRecursive(root As Control, id As String) As Control
        If root.ID = id Then
            Return root
        End If

        Return root.Controls.Cast(Of Control)().[Select](Function(c) FindControlRecursive(c, id)).FirstOrDefault(Function(c) c IsNot Nothing)
    End Function


    Private Function substituteTeacher(periodID As Integer, teacherID As Integer, secID As Integer, dayID As Integer) As String


        Dim tmpLst As New List(Of String)
        'sqlStr = "select empname from vw_Employees where EmpCatName='Teaching' and StatusName='Working' except  SELECT [EmpName] FROM [vw_Time_table]  WHERE teacherID <> '" & teacherID & "' AND attdate='2015-03-09'  AND  secid='" & secID & "' and dayID='" & dayID & "'and periodID='" & periodID & "' and att=1"
        sqlStr = "select empname from vw_Employees where EmpCatName='Teaching' and StatusName='Working' and empid <>'" & teacherID & "' except  SELECT [EmpName] FROM [vw_Time_table]  WHERE attdate='" & Now.Date.ToString("yyyy/MM/dd") & "'  AND  secid='" & secID & "' and dayID='" & dayID & "'and periodID='" & periodID & "' and att=1"

        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            tmpLst.Add(myReader("empname"))
        End While
        myReader.Close()




        Dim rnd As New Random
        '  Dim randomIndex As Integer = rnd.Next(0, tmpLst.Count - 1)
        Return tmpLst.Item(rnd.Next(0, tmpLst.Count - 1)).ToString
    End Function

  
   
    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If (e.CommandName = "getSchedule") Then
            ' Retrieve the row index stored in the CommandArgument property.
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)

            ' Retrieve the row that contains the button 
            ' from the Rows collection.
            Dim row As GridViewRow = GridView1.Rows(index)
            lblSchedule.Text = "Schedule for " & row.Cells(0).Text
            Dim empID As Integer = FindEmployeeID(2, "Teaching", row.Cells(0).Text)
            SqlDataSourceTT.SelectCommand = "SELECT [ClassName], [SecName], [periodID], [SubjectName], [EmpName], [Att], [AttDate] FROM [vw_Time_table] WHERE teacherID='" & empID & "' AND attdate='" & Now.Date.ToString("yyyy/MM/dd") & "'  AND dayID='" & Now.DayOfWeek & "' ORDER BY dayID"
            GridView2.DataBind()

        End If
    End Sub

   
    Protected Sub btnSubstitute_Click(sender As Object, e As EventArgs) Handles btnSubstitute.Click
        If GridView2.Rows.Count <= 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert(s'Please get the schedule of a teacher first.');", True)
            Exit Sub
        End If

        Dim table1 As DataTable = New DataTable("Table")
        table1.Columns.Add("period")
        table1.Columns.Add("Teacher")
        Dim subTeacher As String = ""
        Dim empID As Integer = FindEmployeeID(2, "Teaching", lblSchedule.Text.Replace("Schedule for ", ""))
        Dim SecID As Integer = 0
        Dim dayID As Integer = cboDays.SelectedIndex
        Dim teacherLst As New List(Of String)
        Dim periodLst As New List(Of String)
        For i = 0 To GridView2.Rows.Count - 1
            SecID = 0
            'FindSectionID(GridView2.Rows(i).Cells(0).Text, GridView2.Rows(i).Cells(1).Text)
            subTeacher = substituteTeacher(GridView2.Rows(i).Cells(2).Text, empID, SecID, dayID)
            If teacherLst.Contains(subTeacher) Then
                i = i - 1
                Continue For
            End If
            teacherLst.Add(subTeacher)
            periodLst.Add(GridView2.Rows(i).Cells(2).Text)
        Next

        For i = 0 To teacherLst.Count - 1
            table1.Rows.Add(periodLst.Item(i), teacherLst.Item(i))
        Next
        ' Create a DataSet. Put both tables in it.
        Dim ds As DataSet = New DataSet("Table")
        ds.Tables.Add(table1)
        ' set1.Tables.Add(table2)
        GridView4.Visible = True
        GridView4.DataSource = ds.Tables("Table")
        GridView4.DataBind()
        '  GridView4.Visible = True
    End Sub

    Private Function getSubstituteTeachers() As DropDownList


        Return Nothing
    End Function
    Protected Sub GridView5_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView5.RowDataBound
       
        'myConn.Open()
        'If e.Row.RowType = DataControlRowType.DataRow Then
        '    '' 
        '    Dim ddl = DirectCast(e.Row.FindControl("cboTeachers"), DropDownList)
        '    Dim CountryId As Integer = Convert.ToInt32(e.Row.Cells(0).Text)
        '    Dim cmd As New SqlCommand("select * from State where CountryID=" & CountryId, myConn)
        '    Dim da As New SqlDataAdapter(cmd)
        '    Dim ds As New DataSet()
        '    da.Fill(ds)
        '    
        '    ddl.DataSource = ds
        '    ddl.DataTextField = "StateName"
        '    ddl.DataValueField = "StateID"
        '    ddl.DataBind()
        '    ddl.Items.Insert(0, New ListItem("--Select--", "0"))
        'End If
    End Sub

    Protected Sub GridView2_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView2.RowDataBound
       
       
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim ddl = DirectCast(e.Row.FindControl("cboTeachers"), DropDownList)
            '   Dim EmpID As Integer = FindEmployeeID(2, "Teaching", e.Row.Cells(0).Text)
            Dim sqlstr As String = "SELECT EmpName FROM [vw_Employee_Attendance] WHERE EmpCatName='Teaching'"
            Dim ds As New DataSet()
            ds = ExecuteQuery_DataSet(sqlStr, "t")
            
            ddl.DataSource = ds
            ddl.DataTextField = "EmpName"
            ddl.DataValueField = "EmpName"
            ddl.DataBind()
            ddl.Items.Insert(0, New ListItem("--Select--", "0"))
        End If
    End Sub
End Class