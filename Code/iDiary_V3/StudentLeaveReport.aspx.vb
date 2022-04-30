Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports System.IO

Partial Class Admin_StudentLeaveReport
    Inherits System.Web.UI.Page
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
            End If
        Catch ex As Exception
            Response.Redirect("~/Login.aspx")
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            InitControls()
        Else
            Dim printScript As String = "function PrintGridView() { var gridInsideDiv = document.getElementById('gvDiv');" & _
                    " var printWindow = window.open('gview.htm','PrintWindow','letf=0,top=0,width=150,height=300,toolbar=1,scrollbars=1,status=1');" & _
                    " printWindow.document.write(gridInsideDiv.innerHTML);printWindow.document.close();printWindow.focus();" & _
                    " printWindow.print();printWindow.close();}"
            Me.ClientScript.RegisterStartupScript(Page.[GetType](), "PrintGridView", printScript.ToString(), True)
            btnPrint.Attributes.Add("onclick", "PrintGridView();")
        End If
        'If Request.Cookies("UType").Value.ToString.Contains("Admin-1") = False Then
        '    btnSave.Enabled = False
        'End If


        
    End Sub

    Private Sub InitControls()
        txtDate.Text = Now.Date().ToString("dd/MM/yyyy")

        'txtTime.Text = Now.TimeOfDay.Hours & ":" & Now.TimeOfDay.Minutes
        LoadMasterInfo(2, cboClass)
        ' LoadClassSections()
        'cboStudents.Items.Clear()
        'chkAll.Checked = False

        'chkSMS.Checked = False
        'chkEmail.Checked = False
        cboSection.Items.Clear()
        gvAttendance.Visible = False
        lblStatus.Text = ""
    End Sub


    Protected Sub cboClassSection_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboClass.SelectedIndexChanged
        'LoadClassSection(cboClass.Text, cboSection)
        LoadClassSection("", cboClass.Text, cboSection)
    End Sub

    Protected Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click
        lblStatus.Text = ""
        If Trim(txtDate.Text) = "" Then
            lblStatus.Text = "Please Enter Date..."
            txtDate.Focus()
            Exit Sub
        End If
        If cboClass.Text = "" Then
            lblStatus.Text = "Please choose a class..."
            cboClass.Focus()
            Exit Sub
        End If
        If cboSection.Text = "" Then
            lblStatus.Text = "Please choose a section..."
            cboSection.Focus()
            Exit Sub
        End If

        Dim sqlStr As String = ""
        sqlStr = "Select RegNo,SNAme,FromDate,ToDate,Message,Reason From vw_StudentLeave Where CssName='" & cboClass.Text & " - " & cboSection.Text & "'   AND FromDate <= '" & txtDate.Text.Substring(6, 4) & "/" & txtDate.Text.Substring(3, 2) & "/" & txtDate.Text.Substring(0, 2) & "' AND ToDate >= '" & txtDate.Text.Substring(6, 4) & "/" & txtDate.Text.Substring(3, 2) & "/" & txtDate.Text.Substring(0, 2) & "'"
        SqlDataSource1.SelectCommand = sqlStr
        gvAttendance.Visible = True
        gvAttendance.DataBind()
        If gvAttendance.Rows.Count = 0 Then
            lblStatus.Text = "No Leave found for Class: " & cboClass.Text & " Sec " & cboSection.Text & " ..."
            btnPrint.Visible = False
            btnExcel.Visible = False
        Else
            gvAttendance.Visible = True
            gvAttendance.DataBind()
            lblAbsent.Text = "Total Leaves : " & gvAttendance.Rows.Count
            'lblPresent.Text = "Total Present : " & present
            'btnPrint.Visible = True
            btnExcel.Visible = True
        End If

    End Sub

    Dim absent As Integer = 0, present As Integer = 0
    'Protected Sub gvAttendance_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvAttendance.RowDataBound
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        Dim myVal As String = e.Row.Cells(2).Text
    '        If myVal = "0" Then
    '            e.Row.Cells(2).Text = "Absent"
    '            absent = absent + 1
    '        ElseIf myVal = "1" Then
    '            e.Row.Cells(2).Text = "Present"
    '            present = present + 1
    '        End If
    '    End If
    'End Sub

    Protected Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        Dim sw As New StringWriter()
        Dim hw As New System.Web.UI.HtmlTextWriter(sw)
        Dim frm As HtmlForm = New HtmlForm()

        Dim filename As String = "StudentLeave_" + DateTime.Now.ToString() + ".xls"

        Page.Response.AddHeader("content-disposition", "attachment;filename=" + filename)
        Page.Response.ContentType = "application/vnd.ms-excel"
        Page.Response.Charset = ""
        Page.EnableViewState = False
        frm.Attributes("runat") = "server"
        Controls.Add(frm)
        frm.Controls.Add(gvAttendance)
        frm.RenderControl(hw)
        Response.Write(sw.ToString())
        Response.End()
    End Sub
End Class
