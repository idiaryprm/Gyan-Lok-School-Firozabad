Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary_Fee.CLS_iDiary_Fee
Imports Microsoft.Reporting.WebForms
Public Class HeadwiseReport
    Inherits System.Web.UI.Page


    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        Try

            If Request.Cookies("UType").Value.ToString.Contains("Fee") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
            End If

        Catch ex As Exception

            If ex.Message.Contains("Object reference not set to an instance of an object") Then
                response.redirect("~/Login.aspx")
            End If

        End Try

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
    End Sub

    'Private Sub EnableDisable_Monthwise(ByVal myVal As Boolean)
    '    cboMonth.Enabled = myVal
    '    txtYear.Enabled = myVal
    'End Sub

    'Private Sub EnableDisable_Termwise(ByVal myVal As Boolean)
    '    cboTermNo.Enabled = myVal
    '    lblTerm.Enabled = myVal
    'End Sub

    'Private Sub EnableDisable_DayWise(ByVal myVal As Boolean)
    '    txtDate.Enabled = myVal
    'End Sub

    Private Sub InitControls()
        LoadMasterInfo(60, cboGroup)
        cboGroup.Items.Add("ALL")
        'optMonthwise.Checked = False
        'LoadMonths(cboMonth)
        'txtYear.Text = Now.Year
        'EnableDisable_Monthwise(False)

        optTermwise.Checked = False
        'LoadFeeTerms(cboTermNo)
        'EnableDisable_Termwise(False)

        'optDaywise.Checked = False
        'txtDate.Text = Now.Date.ToString("dd/MM/yyyy")
        txtFrom.Text = Now.Date.ToString("dd/MM/yyyy")
        txtTo.Text = Now.Date.ToString("dd/MM/yyyy")
        'EnableDisable_DayWise(False)

        GridView1.Columns.Clear()
        GridView1.Visible = False
        btnPrint.Visible = False
        btnExcel.Visible = False

        'optMonthwise.Focus()
    End Sub

    Protected Sub btnView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnView.Click
        If optTermwise.Checked = True Then
            If cboTermNo.Text = "" Then
                lblStatus.Text = "Invalid Term"
                cboTermNo.Focus()
                Exit Sub
            End If
        ElseIf optWeekWise.Checked = True Then
            If txtFrom.Text = "" Then
                lblStatus.Text = "Invalid From Date"
                txtFrom.Focus()
                Exit Sub
            End If
            If txtTo.Text = "" Then
                lblStatus.Text = "Invalid To Date"
                txtTo.Focus()
                Exit Sub
            End If
        End If
        PrepareReport(1)
    End Sub
    Private Sub PrepareReport(Type As Integer)
        Dim FromDate1 As String = txtFrom.Text.Substring(6, 4) & "/" & txtFrom.Text.Substring(3, 2) & "/" & txtFrom.Text.Substring(0, 2)
        Dim ToDate1 As String = txtTo.Text.Substring(6, 4) & "/" & txtTo.Text.Substring(3, 2) & "/" & txtTo.Text.Substring(0, 2)
        Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
        myConn.Open()

        Dim myCommand As New System.Data.SqlClient.SqlCommand

        Dim sqlStr As String = ""
        Dim i As Integer = 0
        Dim LstFeeType As New ListBox
        Dim ConcessionFeeTypeID As Integer = 0, ConcessionFeeTypeName As String = ""

        'Read Concession Fee Type
        sqlStr = "Select ConcessionFeeType, FeetypeName From vwFeeConcessionConfig"
        myCommand.CommandText = sqlStr
        myCommand.Connection = myConn

        Dim ConcessionConfigReader As SqlDataReader = myCommand.ExecuteReader
        While ConcessionConfigReader.Read
            ConcessionFeeTypeID = ConcessionConfigReader(0)
            ConcessionFeeTypeName = ConcessionConfigReader(1)
        End While
        ConcessionConfigReader.Close()


        myCommand.Dispose()
        myConn.Close()
        lblSchoolName.Text = FindSchoolDetails(1)
        If optWeekWise.Checked = True Then
            If Type = 1 Then
                lblTitle.Text = "Headwise Collection Statement: Dt: " & txtFrom.Text & " - " & txtTo.Text
            Else
                lblTitle.Text = "Comprehension Report Statement: Dt: " & txtFrom.Text & " - " & txtTo.Text
            End If
        Else
            If Type = 1 Then
                lblTitle.Text = "Headwise Collection Statement"
            Else
                lblTitle.Text = "Comprehension Report Statement"
            End If

        End If
        If optWeekWise.Checked = True Then
            sqlStr = "select * from vw_FeeDeposit where FeeTypeID<> " & ConcessionFeeTypeID & " and DepositDate between '" & FromDate1 & "' and '" & ToDate1 & "'"
        Else
            sqlStr = "select * from vw_FeeDeposit where FeeTypeID<> " & ConcessionFeeTypeID & ""
            If cboTermNo.Text <> "ALL" Then
                sqlStr += " and TermNo='" & cboTermNo.Text & "'"
            End If
            If cboGroup.Text <> "ALL" Then
                Dim FeeGroupID As Integer = FindMasterID(60, cboGroup.Text)
                sqlStr += " AND FeeGroupID=" & FeeGroupID
            End If
        End If
        Dim ds As New DataSet
        ds = ExecuteQuery_DataSet(sqlStr, "tbl")
        Dim FirstSNo As Integer = 0
        For Each Row As DataRow In ds.Tables(0).Rows
            FirstSNo = Row("CMNO")
            Exit For
        Next

        Dim rds As ReportDataSource = New ReportDataSource()

        rds.Name = "DataSet1" ' Change to what you will be using when creating an objectdatasource
        '    End If

        rds.Value = ds.Tables(0)
        With ReportViewer1   ' Name of the report control on the form
            .Reset()
            .ProcessingMode = ProcessingMode.Local
            .LocalReport.DataSources.Clear()
            .Visible = True
            If Type = 1 Then
                .LocalReport.ReportPath = "rptFeeCollectionDayWise.rdlc"
            Else
                .LocalReport.ReportPath = "rptFeeCollectionTermWise.rdlc"
            End If
            .LocalReport.DataSources.Add(rds)
        End With
        Dim params(1) As Microsoft.Reporting.WebForms.ReportParameter
        params(0) = New Microsoft.Reporting.WebForms.ReportParameter("param", lblTitle.Text, Visible)
        params(1) = New Microsoft.Reporting.WebForms.ReportParameter("FirstSNo", FirstSNo, Visible)


        Me.ReportViewer1.LocalReport.SetParameters(params)
        ReportViewer1.Visible = True

        ReportViewer1.LocalReport.Refresh()

    End Sub
    Public Function ExecuteQuery_DataSet(ByVal strQuery As String, ByVal cTableName As String) As DataSet
        Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        Dim con As New System.Data.SqlClient.SqlConnection(myConnStr)
        Dim SqlCmd = New SqlCommand(strQuery, con)

        Dim da As New SqlDataAdapter()
        da.SelectCommand = SqlCmd
        If con.State <> ConnectionState.Open Then
            con.Open()
        End If
        Dim ds As New DataSet()
        Try
            da.Fill(ds, cTableName)
        Catch ex As Exception
            'HttpContext.Current.Response.Write(" Error Web Msql Error ExecuteQuery : " );
            Throw (ex)
        Finally
            SqlCmd.Connection.Close()
            SqlCmd.Dispose()
            con.Close()
        End Try
        Return ds
    End Function
    Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        lblSchoolName.Visible = True
    End Sub

    Protected Sub btnExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcel.Click

        Dim sw As New System.IO.StringWriter()
        Dim hw As New System.Web.UI.HtmlTextWriter(sw)
        Dim frm As HtmlForm = New HtmlForm()

        Dim filename As String = "HeadwiseExport_" + DateTime.Now.ToString() + ".xls"

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

    'Protected Sub optMonthwise_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optMonthwise.CheckedChanged
    '    If optMonthwise.Checked = True Then
    '        EnableDisable_Monthwise(True)
    '        EnableDisable_Termwise(False)
    '        EnableDisable_DayWise(False)
    '    End If
    'End Sub

    'Protected Sub optTermwise_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optTermwise.CheckedChanged
    '    If optTermwise.Checked = True Then
    '        EnableDisable_Monthwise(False)
    '        EnableDisable_Termwise(True)
    '        EnableDisable_DayWise(False)
    '    End If
    'End Sub

    'Protected Sub optDaywise_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optDaywise.CheckedChanged
    '    If optDaywise.Checked = True Then
    '        EnableDisable_Monthwise(False)
    '        EnableDisable_Termwise(False)
    '        EnableDisable_DayWise(True)
    '    End If
    'End Sub

    Protected Sub cboTermNo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTermNo.SelectedIndexChanged
        If cboTermNo.Text = "" Then
            lblTerm.Text = ""
        Else
            Dim FeeGroupId As Integer = FindMasterID(60, cboGroup.Text)
            lblTerm.Text = LoadFeeTermCaption(LoadTermID(cboTermNo.Text, FeeGroupId), FeeGroupId)
        End If
    End Sub

    Protected Sub cboGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboGroup.SelectedIndexChanged
        If cboGroup.Text = "ALL" Then
            cboTermNo.Items.Clear()
            cboTermNo.Items.Add("I")
            cboTermNo.Items.Add("II")
            cboTermNo.Items.Add("III")

            cboTermNo.Items.Add("IV")
            cboTermNo.Items.Add("ALL")

        Else
            Dim FeeGroupId As Integer = FindMasterID(60, cboGroup.Text)
            LoadFeeTerms(cboTermNo, FeeGroupId)
            cboTermNo.Items.Add("ALL")
        End If
    End Sub

  
    Protected Sub btnTermWise_Click(sender As Object, e As EventArgs) Handles btnTermWise.Click
        If optTermwise.Checked = True Then
            If cboTermNo.Text = "" Then
                lblStatus.Text = "Invalid Term"
                cboTermNo.Focus()
                Exit Sub
            End If
        ElseIf optWeekWise.Checked = True Then
            If txtFrom.Text = "" Then
                lblStatus.Text = "Invalid From Date"
                txtFrom.Focus()
                Exit Sub
            End If
            If txtTo.Text = "" Then
                lblStatus.Text = "Invalid To Date"
                txtTo.Focus()
                Exit Sub
            End If
        End If
        PrepareReport(2)
    End Sub
End Class