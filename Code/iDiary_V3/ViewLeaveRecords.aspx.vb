Imports iDiary_V3.iDiary.CLS_idiary

Public Class ViewLeaveRecords

    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            InitControls()
        End If
    End Sub

    Private Sub InitControls()
        txtByAdminNo.Text = ""
        chkAttendanceDate.Checked = False

        LoadMasterInfo(2, cboClass)
        chkClass.Checked = False

        cboSection.Text = ""
        chkSection.Checked = False

        chkAttendanceDate.Checked = False
        txtDateFrom.Text = Now.Date.ToString("dd/MM/yyyy")
        txtDateTo.Text = Now.Date.ToString("dd/MM/yyyy")

        gvAttendance.Visible = False

    End Sub
    Protected Sub btnFind_Click(sender As Object, e As ImageClickEventArgs) Handles btnFind.Click
        If chkByAdminNo.Checked = False And chkClass.Checked = False And chkSection.Checked = False And chkAttendanceDate.Checked = False Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Select atleast one criteria to continue...');", True)
            chkByAdminNo.Focus()
            Exit Sub
        End If

        If chkByAdminNo.Checked = True And txtByAdminNo.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Enter Admission no. to continue...');", True)
            chkByAdminNo.Focus()
            Exit Sub
        End If
        If chkClass.Checked = True And cboClass.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Provide atleast one class to continue...');", True)
            cboClass.Focus()
            Exit Sub
        End If
        If chkSection.Checked = True And cboSection.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Provide atleast one Section to continue...');", True)
            cboSection.Focus()
            Exit Sub
        End If

        If chkAttendanceDate.Checked = True Then
            If txtDateFrom.Text = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Attendance From Date...');", True)
                txtDateFrom.Focus()
                Exit Sub
            End If
            If txtDateTo.Text = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Attendance To Date...');", True)
                txtDateTo.Focus()
                Exit Sub
            End If
            Dim Todate As Date = txtDateTo.Text.Substring(6, 4) & "/" & txtDateTo.Text.Substring(3, 2) & "/" & txtDateTo.Text.Substring(0, 2)
            Dim Fromdate As Date = txtDateFrom.Text.Substring(6, 4) & "/" & txtDateFrom.Text.Substring(3, 2) & "/" & txtDateFrom.Text.Substring(0, 2)

            If Todate < Fromdate Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Date Selection...');", True)
                txtDateFrom.Focus()
                Exit Sub
            End If
        End If


        'SQL Query
        Dim sqlStr As String = ""
        sqlStr &= "Select RegNo,ClassRollno, SName, ClassName, SecName, Attdate, IsPresent From vw_Student_Attendance Where ASID=" & Request.Cookies("ASID").Value

        If chkByAdminNo.Checked = True Then sqlStr &= " AND RegNo Like '" & txtByAdminNo.Text & "%' "
        If chkClass.Checked = True Then sqlStr &= " AND ClassName='" & cboClass.Text & "' "
        If chkSection.Checked = True Then sqlStr &= " AND SecName Like '" & cboSection.Text & "%' "
        If chkAttendanceDate.Checked = True Then sqlStr &= " AND Attdate Between '" & txtDateFrom.Text.Substring(6, 4) & "/" & txtDateFrom.Text.Substring(3, 2) & "/" & txtDateFrom.Text.Substring(0, 2) & "' and '" & txtDateTo.Text.Substring(6, 4) & "/" & txtDateTo.Text.Substring(3, 2) & "/" & txtDateTo.Text.Substring(0, 2) & "'"

        If chkAttendanceDate.Checked = True Then
            sqlStr &= " Order By Attdate, SName, RegNo"
        Else
            sqlStr &= " Order By SName, RegNo"
        End If

        gvAttendance.Visible = True
        SqlDataSourceAttendance.SelectCommand = sqlStr
        gvAttendance.DataBind()

        lblAbsent.Text = "Total Absent : " & absent
        lblPresent.Text = "Total Present : " & present

    End Sub

    Dim absent As Integer = 0, present As Integer = 0
    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvAttendance.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim myVal As String = e.Row.Cells(6).Text
            If myVal = "0" Then
                e.Row.Cells(6).Text = "Absent"
                absent = absent + 1
            ElseIf myVal = "1" Then
                e.Row.Cells(6).Text = "Present"
                present = present + 1
            End If
        End If
    End Sub

    Protected Sub cboClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboClass.SelectedIndexChanged
        LoadClassSection("", cboClass.Text, cboSection)
    End Sub
End Class