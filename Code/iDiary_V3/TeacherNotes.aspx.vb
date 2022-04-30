Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Public Class TeacherNotes
    Inherits System.Web.UI.Page

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If DDLTeacherName.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Select Teacher ..');", True)
            DDLTeacherName.Focus()
            Return
        End If
        If txtDate.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Enter Date ..');", True)
            txtDate.Focus()
            Return
        End If
        If txtDescription.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Enter Description ..');", True)
            txtDescription.Focus()
            Return
        End If
       
       
       
        
        Dim sql As String = ""
        If txtNoteID.Text = "" Then
            sql = "Insert into TeacherNote (TeacherID,TNDate,TNDesc) Values ("
            sql += "" & DDLTeacherName.SelectedValue & ","
            sql += "'" & txtDate.Text.Substring(6, 4) & "/" & txtDate.Text.Substring(3, 2) & "/" & txtDate.Text.Substring(0, 2) & "',"
            sql += "'" & txtDescription.Text & "')"

            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Note Added Successfully ..');", True)
        Else
            sql = "Update TeacherNote Set "
            sql += "TeacherID=" & DDLTeacherName.SelectedValue & ","
            sql += "TNDate='" & txtDate.Text.Substring(6, 4) & "/" & txtDate.Text.Substring(3, 2) & "/" & txtDate.Text.Substring(0, 2) & "',"
            sql += "TNDesc='" & txtDescription.Text & "'"
            sql += " where TNID=" & txtNoteID.Text & ""

            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Note Updated Successfully ..');", True)

        End If
        ExecuteQuery_Update(sql)
        If txtNoteID.Text = "" Then
         Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Note Added Successfully ..');", True)
        Else
         Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Note Updated Successfully ..');", True)
        End If
        'fn.ExecuteUpdate(sql)
        FillGrid()
        'DBGrid.DataSource = tn.FillGrid()
        'DBGrid.DataBind()
        'DBGrid.Columns(2).Visible = False
    End Sub
    Private Sub Clear()
        txtNoteID.Text = ""
        DDLTeacherName.Text = ""
        txtDate.Text = Now.Date.ToString("dd/MM/yyyy")
        txtDescription.Text = ""
        DDLTeacherName.Focus()

    End Sub

    Private Sub FillGrid()
        Dim sql As String = "Select TNID,EmpID,EmpName,TNDate,TNDesc From vw_TeacherNote"
        sql += " where EmpID='" & DDLTeacherName.SelectedValue & "' and TNDate = '" & txtDate.Text.Substring(6, 4) & "/" & txtDate.Text.Substring(3, 2) & "/" & txtDate.Text.Substring(0, 2) & "'order by TNID,TNDate desc"

        SqlDataSource1.SelectCommand = sql
        DBGrid.DataSource = SqlDataSource1
        DBGrid.Columns(1).Visible = True
        DBGrid.DataBind()
        DBGrid.Columns(1).Visible = False
        Clear()
    End Sub
    Protected Sub DBGrid_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DBGrid.SelectedIndexChanged
        txtNoteID.Text = DBGrid.SelectedRow.Cells(1).Text.ToString()
        DDLTeacherName.Text = DBGrid.SelectedRow.Cells(2).Text
        txtDate.Text = DBGrid.SelectedRow.Cells(4).Text.ToString()
        txtDescription.Text = DBGrid.SelectedRow.Cells(5).Text.ToString()
    End Sub
    Protected Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
       
       
       
        
        Dim sql As String = "Delete From TeacherNote"
        sql += " where TNID=" & txtNoteID.Text & ""

        ExecuteQuery_Update(sql)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Note Deleted Successfully ..');", True)
        FillGrid()
    End Sub
    Protected Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        If DDLTeacherName.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Select Teacher ..');", True)
            DDLTeacherName.Focus()
            Return
        End If
        If txtDate.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Enter Date ..');", True)
            txtDate.Focus()
            Return
        End If
        FillGrid()

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'If Me.IsPostBack = False Then
        If Not IsPostBack Then
            FillTeacher(DDLTeacherName, "")
            txtDate.Text = Now.Date.ToString("dd/MM/yyyy")
            DDLTeacherName.Focus()
        End If
    End Sub
    Public Sub FillTeacher(ByVal cmb As DropDownList, ByVal type As String)
        Dim sql As String = ""
        sql = "Select EmpID, EmpName From EmployeeMaster order by EmpName"
        FillComboBox(sql, "EmpName", "EmpID", cmb, "", Trim(type))
    End Sub
    Public Sub FillComboBox(ByVal SQLstring As String, ByVal TextField As String, ByVal ValueField As String, ByVal cmb As DropDownList, ByVal selectOption As String, ByVal type As String)
        Dim ds As New DataSet()
        Dim dt As New DataTable()
        Try
            ds = ExecuteQuery_DataSet(SQLstring, "tbl")
            dt = ds.Tables(0)

            Dim dr As DataRow = ds.Tables(0).NewRow()
            'If selectOption.Trim().Length > 0 Then
            If selectOption.ToLower() = "" Then
                dr(TextField) = ""
                'If type = "Admin" Then
                dt.Rows.InsertAt(dr, 0)
                'End If

            Else
                'dr(TextField) = selectOption
                'If type = "Admin" Then
                '    dt.Rows.InsertAt(dr, 0)
                'End If
                dr(TextField) = ""
                'If type = "Admin" Then
                dt.Rows.InsertAt(dr, 0)


            End If
            'End If
            cmb.DataSource = dt
            cmb.DataTextField = TextField
            If ValueField.Length > 0 Then
                cmb.DataValueField = ValueField
            End If
            cmb.DataBind()
        Catch
            Throw
        End Try
    End Sub
End Class