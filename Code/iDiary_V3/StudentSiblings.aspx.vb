Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary_Student.CLS_iDiary_Student
Imports iDiary_V3.iDiary_Fee.CLS_iDiary_Fee
Imports System.IO
Imports Microsoft.Reporting.WebForms
Imports iDiary_V3.iDiary_Date.CLS_iDiary_Date
'Imports System.String

Public Class StudentSiblings
    Inherits System.Web.UI.Page
    Shared dt As New DataTable
    Shared dt1 As New DataTable
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Student") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
            End If
        Catch ex As Exception
            Response.Redirect("~/Login.aspx")
        End Try
    End Sub

    Private Sub StudentMaster_InitComplete(sender As Object, e As EventArgs) Handles Me.InitComplete

    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("ActiveTab") = 2
        If IsPostBack = False Then InitControls()


        Dim u As String = Request.Cookies("UID").Value
        If Request.Cookies("UType").Value.ToString.Contains("Student-1") = False And Request.Cookies("UType").Value.ToString.Contains("Admin-1") = False Then
            btnSave.Enabled = False

        End If
    End Sub

    Private Sub InitControls()
        Try
            dt.Columns.Add(New DataColumn("StudentID"))
            dt.Columns.Add(New DataColumn("RegNo"))
            dt.Columns.Add(New DataColumn("SName"))
            dt.Columns.Add(New DataColumn("SchoolName"))
            dt.Columns.Add(New DataColumn("ClassName"))
            dt.Columns.Add(New DataColumn("SecName"))
            dt.Columns.Add(New DataColumn("FName"))
            dt.Columns.Add(New DataColumn("MobNo"))

            dt1.Columns.Add(New DataColumn("StudentID"))
            dt1.Columns.Add(New DataColumn("RegNo"))
            dt1.Columns.Add(New DataColumn("SName"))
            dt1.Columns.Add(New DataColumn("SchoolName"))
            dt1.Columns.Add(New DataColumn("ClassName"))
            dt1.Columns.Add(New DataColumn("SecName"))
            dt1.Columns.Add(New DataColumn("FName"))
            dt1.Columns.Add(New DataColumn("MobNo"))
        Catch ex As Exception

        End Try
        dt.Rows.Clear()
        txtSName.Text = ""
        lblStatus.Text = ""
        txtSchoolName.Text = ""
        txtClass.Text = ""
        txtFName.Text = ""
        txtSAdmission.Text = ""
        txtSbAdmission.Text = ""
        txtSbName.Text = ""
        txtStudentID.Text = ""
        gvSibling.DataSource = dt1
        gvSibling.DataBind()
        gvSibling.Visible = False
        gvStudent.Visible = False
        lblSiblings.Visible = False
        txtSAdmission.Focus()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If gvSibling.Visible = False Or gvSibling.Rows.Count < 1 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Add Siblings in Sibling List...');", True)
            lblStatus.Text = "Please Add Siblings in Sibling List..."
            Exit Sub
        End If

        'Update Command
        'Dim sqlstr = "Update StudentBasicInfo Set " & _

        Dim SibStudentID As String = ""
        For i = 0 To gvSibling.Rows.Count - 1
            SibStudentID += gvSibling.Rows(i).Cells(1).Text & ","
        Next
        SibStudentID = SibStudentID & txtStudentID.Text
        Dim sqlstr = "Update StudentBasicInfo Set onlychild = 1, SiblingStudentID='" & SibStudentID & "' where StudentID in(" & SibStudentID & ")"
        ExecuteQuery_Update(sqlstr)
        InitControls()
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Siblings has been Saves Successfully...');", True)
        lblStatus.Text = "Siblings has been Saves Successfully..."
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        InitControls()
    End Sub

    Protected Sub btnSbAdm_Click1(sender As Object, e As EventArgs) Handles btnSbAdm.Click
        If txtSbAdmission.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Sibling Admission No');", True)
            txtSbAdmission.Focus()
            Exit Sub
        End If

        SqlDataSourcesiblings.SelectCommand = "SELECT StudentID,RegNo, SName, ClassName, SecName,FName,MobNo,CSSID,SchoolName FROM vw_Student WHERE ASID = " & Request.Cookies("ASID").Value & " AND RegNo='" & txtSbAdmission.Text & "'"
        gvStudent.DataBind()
        If gvStudent.Rows.Count = 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Reg No.');", True)
            txtSAdmission.Focus()
            Exit Sub
        End If
        gvStudent.Visible = True
    End Sub

    Protected Sub gvStudent_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvStudent.SelectedIndexChanged
        Dim StudentID As Integer = gvStudent.SelectedRow.Cells(1).Text
        If StudentID = txtStudentID.Text Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Selected Student Allready Added in Sibling list...');", True)
            lblStatus.Text = "Selected Student Can't be Added in Sibling list..."
            Exit Sub
        End If
        Dim RecordExist As Integer = 0
        For i = 0 To gvSibling.Rows.Count - 1
            If StudentID = gvSibling.Rows(i).Cells(1).Text Then
                RecordExist = 1
                Exit For
            End If
        Next
        If RecordExist = 1 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Selected Student Allready Added in Sibling list...');", True)
            lblStatus.Text = "Selected Student Allready Added in Sibling list..."
            Exit Sub
        End If
        lblStatus.Text = ""
        Dim RowValues As Object() = {gvStudent.SelectedRow.Cells(1).Text, gvStudent.SelectedRow.Cells(2).Text, gvStudent.SelectedRow.Cells(3).Text, gvStudent.SelectedRow.Cells(4).Text, gvStudent.SelectedRow.Cells(5).Text, gvStudent.SelectedRow.Cells(6).Text, gvStudent.SelectedRow.Cells(7).Text, gvStudent.SelectedRow.Cells(8).Text}

        'create new data row
        Dim dRow As DataRow
        dRow = dt.Rows.Add(RowValues)
        dt.AcceptChanges()
        gvSibling.DataSource = dt
        gvSibling.DataBind()
        gvSibling.Visible = True
        gvStudent.Visible = False
        'txtSName.Text = ""
        'txtSAdmission.Text = ""
        If gvSibling.Rows.Count > 0 Then
            lblSiblings.Visible = True
        Else
            lblSiblings.Visible = False
        End If
        txtSbAdmission.Text = ""
        txtSbName.Text = ""
    End Sub

    Protected Sub btnSbName_Click(sender As Object, e As EventArgs) Handles btnSbName.Click
        If Trim(txtSName.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Student Name');", True)
            txtSName.Focus()
            Exit Sub
        End If
        SqlDataSourcesiblings.SelectCommand = "SELECT StudentID,RegNo, SName, ClassName, SecName,FName,MobNo,CSSID,SchoolName FROM vw_Student WHERE ASID = " & Request.Cookies("ASID").Value & " AND SName like '%" & txtSbName.Text & "%'"
        gvStudent.DataBind()
        gvStudent.Visible = True
    End Sub

    Protected Sub gvSibling_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvSibling.RowDeleting
        Dim rowIndex As Integer = Convert.ToInt32(e.RowIndex)
        dt.Rows.RemoveAt(rowIndex)
        gvSibling.DataSource = dt
        gvSibling.DataBind()
    End Sub

    Protected Sub GridView2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView2.SelectedIndexChanged
        lblStatus.Text = ""
        txtSAdmission.Text = GridView2.SelectedRow.Cells(1).Text
        txtSName.Text = GridView2.SelectedRow.Cells(2).Text
        txtSchoolName.Text = GridView2.SelectedRow.Cells(3).Text
        txtClass.Text = GridView2.SelectedRow.Cells(4).Text & "-" & GridView2.SelectedRow.Cells(5).Text
        txtFName.Text = GridView2.SelectedRow.Cells(6).Text
        txtStudentID.Text = GridView2.SelectedValue.ToString
        GridView2.Visible = False
        Dim ASID As Integer = Request.Cookies("ASID").Value
        Dim SibStudentID As String = ExecuteQuery_ExecuteScalar("select SiblingStudentID  from vw_Student Where Studentid=" & txtStudentID.Text & " AND ASID=" & ASID)
        If SibStudentID = "" Then
            lblStatus.Text = " No Siblings to display."
            Exit Sub
        End If
       
       
       
        dt.Clear()
        Dim sqlStr As String = "Select StudentID,RegNo,SName,ClassName,SecName,FName,MobNo,SchoolName FROM vw_Student  Where ASID=" & ASID & " AND StudentID IN (" & SibStudentID & ") AND StudentID <> " & txtStudentID.Text
      
        dt = ExecuteQuery_DataSet(sqlStr, "t").Tables(0)
        gvSibling.DataSource = dt
        gvSibling.DataBind()
        gvSibling.Visible = True
        lblSiblings.Visible = True
        
    End Sub

    Protected Sub btnSName_Click(sender As Object, e As EventArgs) Handles btnSName.Click
        SqlDataSource2.SelectCommand = "SELECT StudentID,RegNo, SName, ClassName, SecName,FName,SchoolName FROM vw_Student WHERE ASID = " & Request.Cookies("ASID").Value & " AND SName Like '%" & txtSName.Text & "%'"
        GridView2.DataBind()
        GridView2.Visible = True

    End Sub

    Protected Sub btnSAdmission_Click(sender As Object, e As EventArgs) Handles btnSAdmission.Click
        If txtSAdmission.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Admission No');", True)
            txtSAdmission.Focus()
            Exit Sub
        End If
        SqlDataSource2.SelectCommand = "SELECT StudentID,RegNo, SName, ClassName, SecName,FName,SchoolName FROM vw_Student WHERE ASID = " & Request.Cookies("ASID").Value & " AND regno Like '%" & txtSAdmission.Text & "%'"
        GridView2.DataBind()
        GridView2.Visible = True

    End Sub
End Class