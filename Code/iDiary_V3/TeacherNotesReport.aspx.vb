Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Public Class TeacherNotesReport
    Inherits System.Web.UI.Page
    Protected Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        If txtDateFrom.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Enter Date From ..');", True)
            txtDateFrom.Focus()
            Return
        End If
        If txtDateTo.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Enter Date To ..');", True)
            txtDateTo.Focus()
            Return
        End If
        Dim StartDate As Date = New Date(txtDateFrom.Text.Substring(6, 4), txtDateFrom.Text.Substring(3, 2), txtDateFrom.Text.Substring(0, 2))
        Dim EndDate As Date = New Date(txtDateTo.Text.Substring(6, 4), txtDateTo.Text.Substring(3, 2), txtDateTo.Text.Substring(0, 2))
        If StartDate > EndDate Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('To Date can not less than From Date ..');", True)

            txtDateTo.Focus()
            Return
        End If
        If DDLTeacherName.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Select Teacher ..');", True)
            DDLTeacherName.Focus()
            Return
        End If


        FillGrid()
    End Sub
    Private Sub Clear()
        DDLTeacherName.Text = ""
        txtDateFrom.Text = ""
        txtDateTo.Text = ""
    End Sub

    Private Sub FillGrid()
        Dim sql As String = "Select TNID,EmpID,EmpName,TNDate,TNDesc From vw_TeacherNote"
        If DDLTeacherName.Text = "All" Then
            sql += " where TNDate between '" & txtDateFrom.Text.Substring(6, 4) & "/" & txtDateFrom.Text.Substring(3, 2) & "/" & txtDateFrom.Text.Substring(0, 2) & "' and '" & txtDateTo.Text.Substring(6, 4) & "/" & txtDateTo.Text.Substring(3, 2) & "/" & txtDateTo.Text.Substring(0, 2) & "' order by TNDate desc"
        Else
            sql += " where EmpID='" & DDLTeacherName.SelectedValue & "' and TNDate between '" & txtDateFrom.Text.Substring(6, 4) & "/" & txtDateFrom.Text.Substring(3, 2) & "/" & txtDateFrom.Text.Substring(0, 2) & "' and '" & txtDateTo.Text.Substring(6, 4) & "/" & txtDateTo.Text.Substring(3, 2) & "/" & txtDateTo.Text.Substring(0, 2) & "' order by TNDate desc"
        End If


        SqlDataSource1.SelectCommand = sql
        DBGrid.DataSource = SqlDataSource1
        DBGrid.DataBind()
        Clear()
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            FillTeacher(DDLTeacherName, "")
            DDLTeacherName.Items.Add("All")
            txtDateFrom.Focus()
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