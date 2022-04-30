Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports System.IO
Imports Microsoft.Reporting.WebForms

Public Class StudentEnquiryReport
    Inherits System.Web.UI.Page

    Dim sqlstr As String = ""
    Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
    Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
    Dim mycommand As New SqlCommand

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Admission") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
            End If
        Catch ex As Exception
            Response.Redirect("~/Login.aspx")
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("ActiveTab") = 1
        If IsPostBack = False Then

            InitControls()
        Else
            'For Grid View Printing. Must have a blank HTM Page (gview.htm)
            Dim printScript As String = "function PrintGridView() { var gridInsideDiv = document.getElementById('gvDiv');" & _
            " var printWindow = window.open('gview.htm','PrintWindow','letf=0,top=0,width=150,height=300,toolbar=1,scrollbars=1,status=1');" & _
            " printWindow.document.write(gridInsideDiv.innerHTML);printWindow.document.close();printWindow.focus();" & _
            " printWindow.print();printWindow.close();}"
            Me.ClientScript.RegisterStartupScript(Page.[GetType](), "PrintGridView", printScript.ToString(), True)
            btnPrint.Attributes.Add("onclick", "PrintGridView();")
        End If
        ''If Request.Cookies("UType").Value.ToString.Contains("Student-1") = False And Request.Cookies("UType").Value.ToString.Contains("Admin-1") = False Then
        ''    btnConverttoAdmission.Enabled = False
        ''End If
    End Sub

    Protected Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click
        'If cboStatus.Text = "Accepted" Then
        '    btnConverttoAdmission.Visible = True
        '    btnConverttoAdmission.Enabled = False
        'Else
        '    btnConverttoAdmission.Visible = False
        'End If
        If optMonthwise.Checked = True Then
            If cboMonth.Text = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Select Month...');", True)
                cboMonth.Focus()
                Exit Sub
            End If
        End If
        SqlDataSource1.SelectCommand = "SELECT * FROM [vw_StudentEnquiry] Where ASID='" & Request.Cookies("ASID").Value & "' and "
        If optMonthwise.Checked = True Then
            GridView1.Visible = True
            SqlDataSource1.SelectCommand += "Year(enquiryYear)='" & txtYear.Text & "' And Month(enquiryYear)='" & cboMonth.SelectedIndex & "'"
            GridView1.DataBind()

        ElseIf optDaywise.Checked = True Then
            GridView1.Visible = True
            SqlDataSource1.SelectCommand += "enquiryYear='" & txtDate.Text.Substring(6, 4) & "/" & txtDate.Text.Substring(3, 2) & "/" & txtDate.Text.Substring(0, 2) & "'"
            GridView1.DataBind()
        ElseIf optStatuswise.Checked = True Then
            GridView1.Visible = True
            SqlDataSource1.SelectCommand += "statusName='" & cboStatus.Text & "'"
            GridView1.DataBind()
        ElseIf optEnquiryType.Checked = True Then
            GridView1.Visible = True
            SqlDataSource1.SelectCommand += "TypeName='" & cboEnquiryType.Text & "'"
            GridView1.DataBind()
        ElseIf optClassWise.Checked = True Then
            GridView1.Visible = True
            SqlDataSource1.SelectCommand += "ClassName='" & cboClass.Text & "'"
            GridView1.DataBind()
        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please choose atleast one option.');", True)
            Exit Sub
        End If
        GridView1.Columns(0).Visible = False 'chkSelect
        GridView1.Columns(8).Visible = False 'FeeBookNo
        GridView1.Columns(9).Visible = False 'txtRegNo
        GridView1.Columns(10).Visible = False 'chkICard
        GridView1.Columns(11).Visible = False 'chkAddmissionFee
        'GridView1.Columns(12).Visible = False 'btnAddmission
        ReportViewer1.Visible = False
        'GridView1.Columns(7).Visible = False     Status
        btnPrintForm.Visible = False
        btnFeeLabel.Visible = False
        btnSave.Visible = False
        btnPrint.Visible = False
        btnExcel.Visible = False
        Panel1.Visible = False

        GridView1.DataBind()
        If optStatuswise.Checked = True And GridView1.Rows.Count > 0 Then
            If cboStatus.Text = "Called" Then
                GridView1.Columns(0).Visible = True 'chkSelect
                sqlstr = "Select TemplateMessage from SMSTemplates where TemplateCode LIKE 'Enquiry101'"
            ElseIf cboStatus.Text = "Selected" Then
                GridView1.Columns(0).Visible = True 'chkSelect
                GridView1.Columns(8).Visible = True 'FeeBookNo
                GridView1.Columns(9).Visible = True 'txtRegNo
                GridView1.Columns(10).Visible = True 'chkICard
                GridView1.Columns(11).Visible = True 'chkAddmissionFee
                'GridView1.Columns(12).Visible = True 'btnAddmission
                btnFeeLabel.Visible = True
                sqlstr = "Select TemplateMessage from SMSTemplates where TemplateCode LIKE 'Enquiry102'"
            End If
            If sqlstr <> "" Then
                Dim sms As String = ExecuteQuery_ExecuteScalar(sqlstr)
                txtMessage.Text = sms
                txtSmsDate.Text = Now.Date.ToString("dd/MM/yyyy")
                txtTime.Text = DateTime.Now.ToString("HH:mm tt")
                Panel1.Visible = True
                btnPrintForm.Visible = True
            End If
        End If
        If GridView1.Rows.Count > 0 Then
            btnPrint.Visible = True
            btnExcel.Visible = True
            btnSave.Visible = True
        End If

    End Sub

    Private Sub InitControls()
        txtDate.Text = Now.Date.ToString("dd/MM/yyyy")
        txtYear.Text = Now.Date.ToString("yyyy")
        LoadMasterInfo(47, cboStatus)
        LoadMasterInfo(48, cboEnquiryType)
        LoadMasterInfo(2, cboClass)
        LoadMonths(cboMonth)
        GridView1.Visible = False
        'btnConverttoAdmission.Visible = False
        Panel1.Visible = False
        'Panel2.Visible = False
        btnSave.Visible = False
        btnPrint.Visible = False
        btnExcel.Visible = False
        btnPrintForm.Visible = False
        btnFeeLabel.Visible = False
    End Sub

    'Protected Sub cboStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboStatus.SelectedIndexChanged

    'End Sub

    'Protected Sub btnConverttoAdmission_Click(sender As Object, e As EventArgs) Handles btnConverttoAdmission.Click
    '    Dim FeeBookIssued As Integer = 0
    '    If txtFeeBookNo.Text <> "" Then
    '        FeeBookIssued = 1
    '    End If
    '    sqlstr = "Update StudentEnquiry SET FeeBookNo='" & txtFeeBookNo.Text & "', RegNo='" & txtSrNo.Text & "',IsFeeBookAssigned=" & FeeBookIssued & " Where enquiryNo='" & lblFormNo.Text & "'"
    '    ExecuteQuery_Update(sqlstr)
    '    lblSName.Text = ""
    '    lblFormNo.Text = ""
    '    Panel1.Visible = False
    '    GridView1.SelectedIndex = -1
    '    btnPrintForm.Visible = True
    '    btnFeeLabel.Visible = True
    'End Sub

    Private Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim AID As String = e.Row.Cells(1).Text
                Try
                    Dim EnquiryID As Integer = Convert.ToInt32(GridView1.DataKeys(e.Row.RowIndex).Values(0))
                    Dim StausName As String = Convert.ToString(GridView1.DataKeys(e.Row.RowIndex).Values(1))
                    Dim FeeBookNo As String = ""
                    Dim RegNo As String = ""
                    Try
                        FeeBookNo = Convert.ToString(GridView1.DataKeys(e.Row.RowIndex).Values(2))
                    Catch ex As Exception

                    End Try
                    Try
                        RegNo = Convert.ToString(GridView1.DataKeys(e.Row.RowIndex).Values(3))
                    Catch ex As Exception

                    End Try
                    Dim IsAdminFeeDeposit As Integer = 0
                    Dim IsIcardAssigned As Integer = 0
                    Try
                        IsAdminFeeDeposit = Convert.ToInt32(GridView1.DataKeys(e.Row.RowIndex).Values(4))
                    Catch ex As Exception

                    End Try
                    Try
                        IsIcardAssigned = Convert.ToInt32(GridView1.DataKeys(e.Row.RowIndex).Values(5))
                    Catch ex As Exception

                    End Try
                    'Dim EnqStatus() As String = GetAdmissionStatus(AID).Split("$")
                    'Dim chkSelect As CheckBox = DirectCast(e.Row.Cells(7).Controls(0), CheckBox)
                    Dim ddlStatus As DropDownList = e.Row.FindControl("ddlStatus")  ' DirectCast(GridView1.Rows(e.Row.RowIndex).FindControl("chkSelect"), CheckBox)
                    Dim txtFeeBookNo As TextBox = e.Row.FindControl("txtFeeBookNo")
                    Dim txtRegNo As TextBox = e.Row.FindControl("txtRegNo")
                    Dim chkICard As CheckBox = e.Row.FindControl("chkICard")
                    Dim chkAdminFee As CheckBox = e.Row.FindControl("chkAdmissionFee")

                    ddlStatus.Text = StausName
                    txtFeeBookNo.Text = FeeBookNo
                    txtRegNo.Text = RegNo
                    If IsIcardAssigned = 0 Then
                        chkICard.Checked = False
                    Else
                        chkICard.Checked = True
                    End If
                    If IsAdminFeeDeposit = 0 Then
                        chkAdminFee.Checked = False
                    Else
                        chkAdminFee.Checked = True
                    End If
                Catch ex As Exception

                End Try
                ' Add the column index as the event argument parameter

            End If
        End If
    End Sub
    Public Function GetAdmissionStatus(formNo As String) As String

        Dim StausName As String = ""
        Dim FeeBookNo As String = ""
        Dim RegNo As String = ""
        Dim IsAdminFeeDeposit As Integer = 0
        Dim IsIcardAssigned As Integer = 0
        Dim Sqlstr As String = ""

        Sqlstr = "Select StatusName,FeeBookNo, RegNo, IsAdminFeeDeposit, IsIcardAssigned From vw_StudentEnquiry Where EnquiryNo='" & formNo & "' and ASID='" & Request.Cookies("ASID").Value & "'"
        'Try
        Dim myreader As SqlDataReader = ExecuteQuery_ExecuteReader(Sqlstr)
        While myreader.Read
            StausName = myreader("StatusName")
            Try
                FeeBookNo = myreader("FeeBookNo")
            Catch ex As Exception

            End Try
            Try
                RegNo = myreader("RegNo")
            Catch ex As Exception

            End Try
            Try
                IsAdminFeeDeposit = myreader("IsAdminFeeDeposit")
            Catch ex As Exception

            End Try
            Try
                IsIcardAssigned = myreader("IsIcardAssigned")
            Catch ex As Exception

            End Try
        End While
        myreader.Close()
        'StausName = ExecuteQuery_ExecuteScalar(Sqlstr)
        'Catch ex As Exception

        'End Try

        Return StausName & "$" & FeeBookNo & "$" & RegNo & "$" & IsAdminFeeDeposit & "$" & IsIcardAssigned
    End Function
    'Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
    '    'Dim ddlStatus As DropDownList = e.Row.FindControl("ddlStatus")

    '    Panel1.Visible = True
    '    btnConverttoAdmission.Visible = True
    '    lblFormNo.Text = GridView1.SelectedRow.Cells(1).Text
    '    lblSName.Text = GridView1.SelectedRow.Cells(2).Text
    '    txtFeeBookNo.Focus()
    'End Sub

    Protected Sub GridView1_Sorting(sender As Object, e As GridViewSortEventArgs) Handles GridView1.Sorting
        GridView1.SelectedIndex = -1
        If optMonthwise.Checked = True Then
            GridView1.Visible = True
            SqlDataSource1.SelectCommand = "SELECT [EnquiryNo], [Sname], [Fname],[ClassName], [MobNo], [address], [enquiryYear],[StatusName] FROM [vw_StudentEnquiry] Where Year(enquiryYear)='" & txtYear.Text & "' And Month(enquiryYear)='" & cboMonth.SelectedIndex & "'"
            GridView1.DataBind()

        ElseIf optDaywise.Checked = True Then
            GridView1.Visible = True
            SqlDataSource1.SelectCommand = "SELECT [EnquiryNo], [Sname], [Fname],[ClassName], [MobNo], [address], [enquiryYear],[StatusName] FROM [vw_StudentEnquiry] Where enquiryYear='" & txtDate.Text.Substring(6, 4) & "/" & txtDate.Text.Substring(3, 2) & "/" & txtDate.Text.Substring(0, 2) & "'"
            GridView1.DataBind()
        ElseIf optStatuswise.Checked = True Then
            GridView1.Visible = True
            SqlDataSource1.SelectCommand = "SELECT [EnquiryNo], [Sname], [Fname],[ClassName], [MobNo], [address], [enquiryYear],[StatusName] FROM [vw_StudentEnquiry] Where statusName='" & cboStatus.Text & "'"
            GridView1.DataBind()
        ElseIf optEnquiryType.Checked = True Then
            GridView1.Visible = True
            SqlDataSource1.SelectCommand = "SELECT [EnquiryNo], [Sname], [Fname],[ClassName], [MobNo], [address], [enquiryYear],[StatusName] FROM [vw_StudentEnquiry] Where TypeName='" & cboEnquiryType.Text & "'"
            GridView1.DataBind()
        ElseIf optClassWise.Checked = True Then
            GridView1.Visible = True
            SqlDataSource1.SelectCommand = "SELECT [EnquiryNo], [Sname], [Fname],[ClassName], [MobNo], [address], [enquiryYear],[StatusName] FROM [vw_StudentEnquiry] Where ClassName='" & cboClass.Text & "'"
            GridView1.DataBind()
        End If


    End Sub

    Protected Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        Dim sw As New StringWriter()
        Dim hw As New System.Web.UI.HtmlTextWriter(sw)
        Dim frm As HtmlForm = New HtmlForm()

        Dim filename As String = "SearchResult_" + DateTime.Now.ToString() + ".xls"

        Page.Response.AddHeader("content-disposition", "attachment;filename=" + filename)
        Page.Response.ContentType = "application/vnd.ms-excel"
        Page.Response.Charset = ""
        Page.EnableViewState = False
        frm.Attributes("runat") = "server"
        Controls.Add(frm)

        frm.Controls.Add(GridView1)

        frm.RenderControl(hw)
        Response.Write(sw.ToString())
        Response.End()
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim enqNO As String = "", statusName As String = "", statusID As Integer = 0
        Dim FeeBookNo As String = "'"
        Dim RegNo As String = "'"
        If GridView1.Columns(8).Visible = True Then
            For i = 0 To GridView1.Rows.Count - 1
                Try
                    Dim txtFeeBookNo As TextBox = DirectCast(GridView1.Rows(i).FindControl("txtFeeBookNo"), TextBox)
                    Dim txtRegNo As TextBox = DirectCast(GridView1.Rows(i).FindControl("txtRegNo"), TextBox)
                    If txtFeeBookNo.Text <> "" Then
                        FeeBookNo += txtFeeBookNo.Text & "','"
                    End If
                    If txtRegNo.Text <> "" Then
                        RegNo += txtRegNo.Text & "','"
                    End If
                Catch ex As Exception

                End Try
            Next
            If FeeBookNo <> "'" Then
                FeeBookNo = FeeBookNo.Substring(0, FeeBookNo.Length - 2)
            End If
            If RegNo <> "'" Then
                RegNo = RegNo.Substring(0, RegNo.Length - 2)
            End If
            If FeeBookNo = "'" And RegNo = "'" Then
                sqlstr = "select FeeBookNo,RegNo from Student where ASID='" & Request.Cookies("ASID").Value & "'"
                If FeeBookNo <> "'" And RegNo <> "'" Then
                    sqlstr += " and (FeeBookNo in (" & FeeBookNo & ") or RegNo in (" & RegNo & "))"
                ElseIf FeeBookNo <> "'" And RegNo = "'" Then
                    sqlstr += " and FeeBookNo in (" & FeeBookNo & ")"
                ElseIf FeeBookNo = "'" And RegNo <> "'" Then
                    sqlstr += " and  RegNo in (" & RegNo & ")"
                End If
                Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
                FeeBookNo = ""
                While myReader.Read
                    FeeBookNo += myReader("FeeBookNo") & ","
                    RegNo += myReader("RegNo") & ","
                End While
                myReader.Close()
                Dim Count As Integer = 0
                If FeeBookNo <> "" Then
                    FeeBookNo = FeeBookNo.Substring(0, FeeBookNo.Length - 1)
                    Dim tmpFeeBookNo() As String = FeeBookNo.Split(",")
                    For j = 0 To tmpFeeBookNo.Count - 1
                        For i = 0 To GridView1.Rows.Count - 1
                            Dim txtFeeBookNo As TextBox = DirectCast(GridView1.Rows(i).FindControl("txtFeeBookNo"), TextBox)
                            If txtFeeBookNo.Text = tmpFeeBookNo(j) And txtFeeBookNo.Text <> "" Then
                                GridView1.Rows(i).BackColor = Drawing.Color.OrangeRed
                                Count = 0
                            End If
                        Next
                    Next
                End If
                If RegNo <> "" Then
                    RegNo = RegNo.Substring(0, RegNo.Length - 1)
                    Dim tmpRegNo() As String = RegNo.Split(",")
                    For j = 0 To tmpRegNo.Count - 1
                        For i = 0 To GridView1.Rows.Count - 1
                            Dim txtRegNo As TextBox = DirectCast(GridView1.Rows(i).FindControl("txtRegNo"), TextBox)
                            If txtRegNo.Text = tmpRegNo(j) And txtRegNo.Text <> "" Then
                                GridView1.Rows(i).BackColor = Drawing.Color.OrangeRed
                                Count = 1
                            End If
                        Next
                    Next
                End If

                If Count = 1 Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Entries!!!   Please Check Fee Book No or Admission No of Colored rows...');", True)
                    Exit Sub
                End If
            End If
        End If
        FeeBookNo = ""
        RegNo = ""
        For i = 0 To GridView1.Rows.Count - 1
            Try
                Dim ddlStatus As DropDownList = DirectCast(GridView1.Rows(i).FindControl("ddlStatus"), DropDownList)
                Dim txtFeeBookNo As TextBox = DirectCast(GridView1.Rows(i).FindControl("txtFeeBookNo"), TextBox)
                Dim txtRegNo As TextBox = DirectCast(GridView1.Rows(i).FindControl("txtRegNo"), TextBox)
                Dim chkICard As CheckBox = DirectCast(GridView1.Rows(i).FindControl("chkICard"), CheckBox)
                Dim chkAdminFee As CheckBox = DirectCast(GridView1.Rows(i).FindControl("chkAdmissionFee"), CheckBox)
                statusName = ddlStatus.Text
                statusID = FindMasterID(47, statusName)
                enqNO = GridView1.Rows(i).Cells(1).Text

                FeeBookNo = txtFeeBookNo.Text
                RegNo = txtRegNo.Text
                Dim IsAdminFeeDeposit As Integer = 0
                Dim IsIcardAssigned As Integer = 0

                If chkICard.Checked = True Then
                    IsIcardAssigned = 1
                End If
                If chkAdminFee.Checked = True Then
                    IsAdminFeeDeposit = 1
                End If
                sqlstr = "Update StudentEnquiry Set EnquiryStatusID=" & statusID & ""
                If GridView1.Columns(8).Visible = True Then 'FeeBookNo
                    sqlstr += ",FeeBookNo='" & FeeBookNo & "',RegNo='" & RegNo & "',IsAdminFeeDeposit='" & IsAdminFeeDeposit & "',IsIcardAssigned=" & IsIcardAssigned & ""
                End If
                sqlstr += " Where EnquiryNo='" & enqNO & "' and ASID='" & Request.Cookies("ASID").Value & "'"
                ExecuteQuery_Update(sqlstr)
            Catch ex As Exception

            End Try
        Next
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Record has been saved succesfully...');", True)

        For i = 0 To GridView1.Rows.Count - 1
            GridView1.Rows(i).BackColor = Drawing.Color.White
        Next

    End Sub

    Protected Sub btnPrintForm_Click(sender As Object, e As EventArgs) Handles btnPrintForm.Click
        GetReport(1)
        'Panel2.Visible = True
    End Sub
    Private Sub GetReport(type As Integer)
        Dim EnquiryNo As String = "'"
        For i = 0 To GridView1.Rows.Count - 1
            Dim chk As CheckBox = DirectCast(GridView1.Rows(i).FindControl("chkSelect"), CheckBox)
            If chk.Checked = True And GridView1.Rows(i).Visible = True Then
                'Dim SID As Integer = Convert.ToInt32(GridView1.Rows(i).Cells(0).Text)
                EnquiryNo += GridView1.Rows(i).Cells(1).Text & "','"
            End If
        Next
        If EnquiryNo = "'" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Select atleast one Student to print Call Letter...');", True)
            Exit Sub
        Else
            EnquiryNo = EnquiryNo.Substring(0, EnquiryNo.Length - 2)
        End If
        PrepareCallLetterReport(EnquiryNo, type)
    End Sub
    Private Sub PrepareCallLetterReport(EnquiryNo As String, type As Integer)
        Dim sql As String = ""
        Dim i As Integer = 0
        Dim MyHeader As String = ""
        Dim ReportPath As String = "Report/"


        sql = "Select * From vw_StudentEnquiry Where EnquiryNo in (" & EnquiryNo & ")"
        sql += " Order by SName"

        Dim ds As New DataSet
        ds = ExecuteQuery_DataSet(sql, "tbl")

        Dim rds As ReportDataSource = New ReportDataSource()
        rds.Name = "DataSet1" ' Change to what you will be using when creating an objectdatasource
        rds.Value = ds.Tables(0)
        With ReportViewer1   ' Name of the report control on the form
            .Reset()
            .ProcessingMode = ProcessingMode.Local
            .LocalReport.DataSources.Clear()
            .Visible = True
            If type = 1 Then
                ReportPath += "rptCallLetter.rdlc"
            Else
                ReportPath += "rptFeeLabelEnquiry.rdlc"
            End If
            .LocalReport.ReportPath = ReportPath
            .LocalReport.DataSources.Add(rds)
        End With
        MyHeader = "Addmission Call Letter"
        Dim params(1) As Microsoft.Reporting.WebForms.ReportParameter
        params(0) = New Microsoft.Reporting.WebForms.ReportParameter("param", MyHeader, Visible)
        params(1) = New Microsoft.Reporting.WebForms.ReportParameter("SchoolName", FindSchoolDetails(1), Visible)
        Me.ReportViewer1.LocalReport.SetParameters(params)
        ReportViewer1.Visible = True
        ReportViewer1.LocalReport.Refresh()
    End Sub
    Protected Sub btnSendSMS_Click(sender As Object, e As EventArgs) Handles btnSendSMS.Click
        If Trim(txtDate.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Date is required...');", True)
            txtDate.Focus()
            Exit Sub
        End If
        If Trim(txtTime.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Time is required...');", True)
            txtTime.Focus()
            Exit Sub
        End If
        Dim SenderID As String = ""
        sqlstr = "Select SMSSender From SMSSender"
        Dim SMSReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
        While SMSReader.Read
            SenderID = SMSReader(0)
        End While
        SMSReader.Close()
        Dim sms As String = txtMessage.Text
        For i = 0 To GridView1.Rows.Count - 1
            Dim chk As CheckBox = DirectCast(GridView1.Rows(i).FindControl("chkSelect"), CheckBox)
            If chk.Checked = True And GridView1.Rows(i).Visible = True Then
                'Dim SID As Integer = Convert.ToInt32(GridView1.Rows(i).Cells(0).Text)
                Dim SName As String = GridView1.Rows(i).Cells(2).Text

                Dim MobNo = GridView1.Rows(i).Cells(5).Text
                sms = sms.Replace("(*)", SName)
                sms = sms.Replace("dd/MM/yyyy", txtDate.Text)
                sms = sms.Replace("HH:mm", txtTime.Text)

                SendMySMS(SenderID, MobNo, sms)
            End If

        Next

        'sms = sms.Replace("(*)", txtSname.Text)
        'sms = sms.Replace("dd/MM/yyyy", txtDate.Text)
        'sms = sms.Replace("HH:mm", txtTime.Text)

        'SendMySMS(SenderID, txtMobNo.Text, sms)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('SMS has been Sent...');", True)
    End Sub

    Protected Sub btnFeeLabel_Click(sender As Object, e As EventArgs) Handles btnFeeLabel.Click
        GetReport(2)
    End Sub
End Class