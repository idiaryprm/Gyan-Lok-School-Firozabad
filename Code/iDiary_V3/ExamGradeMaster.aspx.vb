Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports iDiary_V3.iDiary.CLS_idiary

Public Class ExamGradeMaster
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Initcontrols()
        Else

        End If
    End Sub
    Public Sub Initcontrols()
        txtgradename.Focus()

        FillDataBox()
        btnSave.Visible = False
        ' lblGrade.Visible = False
        txtgradename.Text = ""
        txtnoofgrade.Text = ""
    End Sub

    Protected Sub btnProceed_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnProceed.Click
        If txtgradename.Text = "" Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Please Provide Grade Name')", True)
            txtgradename.Focus()
            Exit Sub
        End If
        If txtnoofgrade.Text = "" Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Please Enter No of Grade Scales')", True)
            txtgradename.Focus()
            Exit Sub
        End If
        gvCreateMyTable()
        btnSave.Visible = True
        ' lblGrade.Visible = True
    End Sub

    Private Sub gvCreateMyTable(Optional ByVal gradeID = 0)
        gvGrade.Visible = True
        If gradeID = 0 Then ' empty 
            Dim dt As New DataTable()
            dt.Clear()
            dt.Columns.Add("DisplayOrder")

            For i = 1 To Val(txtnoofgrade.Text)
                Dim dr As DataRow = dt.NewRow()
                dr("DisplayOrder") = i
                dt.Rows.Add(dr)
            Next

            gvGrade.DataSource = dt
            gvGrade.DataBind()

            For Each gvr As GridViewRow In gvGrade.Rows
                DirectCast(gvr.FindControl("lblSR"), Label).Text = gvGrade.DataKeys(gvr.RowIndex).Value
            Next

        Else    'show existing
            Dim sqlStr As String = "Select * from ExamGradeDetails where GradeID=" & gradeID & " Order By DisplayOrder"
            Dim ds As DataSet = ExecuteQuery_DataSet(sqlStr, "tbl")
            gvGrade.DataSource = ds.Tables(0)
            gvGrade.DataBind()

            For Each gvr As GridViewRow In gvGrade.Rows
                DirectCast(gvr.FindControl("lblSR"), Label).Text = ds.Tables(0).Rows(gvr.RowIndex)("DisplayOrder")
                DirectCast(gvr.FindControl("txtLValue"), TextBox).Text() = ds.Tables(0).Rows(gvr.RowIndex)("LowerValue")
                DirectCast(gvr.FindControl("txtUValue"), TextBox).Text() = ds.Tables(0).Rows(gvr.RowIndex)("UpperValue")
                DirectCast(gvr.FindControl("txtGrade"), TextBox).Text() = ds.Tables(0).Rows(gvr.RowIndex)("Grade")
                DirectCast(gvr.FindControl("txtGradePoint"), TextBox).Text() = ds.Tables(0).Rows(gvr.RowIndex)("GradePoint")
            Next
        End If

    End Sub
    Protected Sub btnokk_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        GvSaveData()
        Initcontrols()
    End Sub
 
    Protected Sub lstGrades_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lstGrades.SelectedIndexChanged
        txtgradename.Text = lstGrades.SelectedItem.Text
        lblGradeID.Text = GetGradeID(txtgradename.Text)
        Dim query As String = "SELECT COUNT(DisplayOrder) FROM ExamGradeDetails WHERE GradeID='" & lblGradeID.Text & "'"
        Dim count As Int32 = CType(ExecuteQuery_ExecuteScalar(query), Int32)
        txtnoofgrade.Text = count
        gvCreateMyTable(lblGradeID.Text)
        btnSave.Visible = True
    End Sub
    Protected Sub FillDataBox()
        Dim sqlstr As String = "SELECT Gradeid,GradeName FROM [ExamGradeMaster]"
        lstGrades.Items.Clear()
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
        While myReader.Read
            lstGrades.Items.Add(New ListItem(myReader(1), myReader(0)))
        End While
        myReader.Close()

    End Sub
    Private Sub GvSaveData()

        Dim sqlstr As String = ""
        Dim displayOrder As Integer = 0
        Dim LValue As Double = 0
        Dim UValue As Double = 0
        Dim Grade As String = ""
        Dim GP As String = ""
        Dim gradeName As String = txtgradename.Text, GradeMasterID As Integer = 0
        If lstGrades.SelectedIndex = -1 Then
            If CheckDuplicateEntry(gradename, "ExamGradeMaster", "GradeName") = 0 Then
            Else
                ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Grade Name Already Exists')", True)
                txtgradename.Text = ""
                Exit Sub
            End If

            sqlstr = "Insert Into ExamGradeMaster values('" & gradename & "')"
            ExecuteQuery_Update(sqlstr)
            sqlstr = "Select Max(Gradeid) From ExamGradeMaster"
            GradeMasterID = ExecuteQuery_ExecuteScalar(sqlstr)
        Else
            GradeMasterID = lblGradeID.Text
            sqlstr = "Update ExamGradeMaster Set GradeName='" & txtgradename.Text & "' Where gradeID=" & GradeMasterID
            ExecuteQuery_Update(sqlstr)
        End If

        sqlstr = "Delete From ExamGradeDetails Where GradeID=" & GradeMasterID
        ExecuteQuery_Update(sqlstr)

        For Each gvr As GridViewRow In gvGrade.Rows
            displayOrder = DirectCast(gvr.FindControl("lblSR"), Label).Text
            LValue = DirectCast(gvr.FindControl("txtLValue"), TextBox).Text
            UValue = DirectCast(gvr.FindControl("txtUValue"), TextBox).Text
            Grade = DirectCast(gvr.FindControl("txtGrade"), TextBox).Text
            GP = DirectCast(gvr.FindControl("txtGradePoint"), TextBox).Text

            sqlstr = "insert into [ExamGradeDetails] (GradeID,UpperValue,LowerValue,Grade,GradePoint,DisplayOrder) values( " & _
                "'" & GradeMasterID & "','" & UValue & "','" & LValue & "','" & Grade & "','" & GP & "','" & displayOrder & "')"
            ExecuteQuery_Update(sqlstr)
        Next
    End Sub
    Protected Sub txtLValue_TextChanged(sender As Object, e As EventArgs)
        Dim txtLVal As TextBox = TryCast(sender, TextBox)
        Dim gvRowIndex As Integer = Convert.ToInt32(txtLVal.Attributes("RowIndex"))
        CheckGvMyTable(gvRowIndex)
    End Sub
    Private Sub CheckGvMyTable(gvRowIndex As Integer)
        gvGrade.Visible = True
        Dim LValue As Double = 0
        Dim UValue As Double = 0
        Dim lastLValue As Double = 0
        DirectCast(gvGrade.Rows(0).FindControl("txtUValue"), TextBox).Text = 100
        If gvRowIndex > 0 Then
            If gvRowIndex = gvGrade.Rows.Count - 1 Then
                DirectCast(gvGrade.Rows(gvGrade.Rows.Count - 1).FindControl("txtLValue"), TextBox).Text = 0
            End If
            LValue = Val(DirectCast(gvGrade.Rows(gvRowIndex - 1).FindControl("txtLValue"), TextBox).Text)
            UValue = LValue - 0.01
            DirectCast(gvGrade.Rows(gvRowIndex).FindControl("txtUValue"), TextBox).Text = UValue
        Else
        End If
        DirectCast(gvGrade.Rows(gvRowIndex).FindControl("txtUValue"), TextBox).Focus()

    End Sub
    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNew.Click
        gvGrade.Visible = False
        txtgradename.Text = ""
        txtnoofgrade.Text = ""
        lstGrades.SelectedIndex = -1
        '  dt.Visible = False
        ' lblGrade.Visible = False
        btnSave.Visible = False
    End Sub

    Public Function GetGradeID(ByVal GradeName As String) As Integer
        Dim GradeID As Integer = 0
        Dim sqlstr As String = "Select Gradeid from ExamGradeMaster Where GradeName='" & GradeName & "'"
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
        While myReader.Read
            GradeID = myReader(0)
        End While
        myReader.Close()
        Return GradeID
    End Function

End Class