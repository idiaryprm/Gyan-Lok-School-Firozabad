Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary_Fee.CLS_iDiary_Fee
Imports Microsoft.Reporting.WebForms

Public Class BusFeeDueList
    Inherits System.Web.UI.Page


    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Bus-1") Or Request.Cookies("UType").Value.ToString.Contains("Admin-1") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
            End If
        Catch ex As Exception
            Response.Redirect("~/Login.aspx")
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("ActiveTab") = 11
        Response.Cookies("ActiveTab").Value = 11
        Response.Cookies("ActiveTab").Expires = DateTime.Now.AddHours(1)
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

    Private Sub InitControls()

        LoadMasterInfo(71, cboSchoolName, Request.Cookies("SchoolIDs").Value)
        LoadMasterInfo(2, cboClass, cboSchoolName.Text)
        cboClass.Items.Add("ALL")
        cboSection.Items.Clear()
        LoadMasterInfo(10, cboStatus)
        LoadFeeTerms(cboTerm, 1, "BusFee")

        GridView1.Visible = True
        btnViewDetails.Enabled = False
        btnPrint.Enabled = False
        btnExcel.Enabled = False
        btnSendSMS.Enabled = False

        cboClass.Focus()

    End Sub

    Protected Sub cboClass_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboClass.SelectedIndexChanged
        LoadClassSection(cboSchoolName.Text, cboClass.Text, cboSection)
        cboSection.Items.Add("ALL")
        cboClass.Focus()
    End Sub

    Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        lblSchoolName.Visible = True
    End Sub

    Protected Sub btnExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcel.Click

        Dim sw As New System.IO.StringWriter()
        Dim hw As New System.Web.UI.HtmlTextWriter(sw)
        Dim frm As HtmlForm = New HtmlForm()

        Dim filename As String = "BusFeeDueList_" + DateTime.Now.ToString() + ".xls"

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

    Protected Sub btnViewDetails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnViewDetails.Click

        GridView1.Visible = False
        btnPrint.Enabled = False
        btnExcel.Enabled = False
        lblSchoolName.Visible = False
        lblTitle.Visible = False
        btnPrint.Enabled = True

    End Sub

    Protected Sub btnViewSummaryList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnViewSummaryList.Click

        GridView1.Visible = False
        btnPrint.Enabled = False
        btnExcel.Enabled = False
        btnSendSMS.Enabled = False

        If cboStatus.Text = "" Then
            lblStatus.Text = "Invalid Status..."
            cboStatus.Focus()
            Exit Sub
        End If
        If cboTerm.Text = "" Then
            lblStatus.Text = "Invalid Term..."
            cboTerm.Focus()
            Exit Sub
        End If
        If cboSchoolName.Text = "" Then
            lblStatus.Text = "Invalid School Name..."
            cboSchoolName.Focus()
            Exit Sub
        End If
        If cboClass.Text = "" Then
            lblStatus.Text = "Invalid Class..."
            cboClass.Focus()
            Exit Sub
        End If
        If cboSection.Text = "" Then
            lblStatus.Text = "Invalid Section..."
            cboSection.Focus()
            Exit Sub
        End If
        'ALL-XXX (Not Allowed...)
        If cboClass.Text = "ALL" And cboSection.Text <> "ALL" Then
            lblStatus.Text = "Invalid Section (Please Select --ALL--)"
            cboSection.Focus()
            Exit Sub
        End If
        'XXX-ALL (Not Allowed...)
        If cboClass.Text <> "ALL" And cboSection.Text = "ALL" Then
            lblStatus.Text = "Invalid Class (Please Select --ALL--)"
            cboClass.Focus()
            Exit Sub
        End If

        lblStatus.Text = ""

        'Dim sqlStr As String = "", CollegeNote As String = ""
        'Dim i As Integer = 0
        'Dim lstClass As New ListBox, lstSection As New ListBox

        'lstClass.Items.Clear()
        'lstSection.Items.Clear()

        'Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        'Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
        'myConn.Open()

        '

        'If cboClass.Text = "ALL" And cboSection.Text = "ALL" Then   ''ALL-ALL (Allowed)

        '    sqlStr = "Select ClassName,SecName From vw_Class_Section Order By DisplayOrder, ClassName, secName"
        '    
        '    
        '    Dim ClassReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        '    While ClassReader.Read
        '        lstClass.Items.Add(ClassReader(0))
        '        lstSection.Items.Add(ClassReader(1))
        '    End While
        '    ClassReader.Close()

        'ElseIf cboClass.Text <> "ALL" And cboSection.Text <> "ALL" Then     ''XXX-XXX(Allowed)

        '    lstClass.Items.Add(cboClass.Text)
        '    lstSection.Items.Add(cboSection.Text)

        'End If

        ''Clear Report Table Contents
        'sqlStr = "Delete From rptFeeDue"
        '
        '
        'ExecuteQuery_Update(SqlStr)

        ''Loop for ALL / Selected Class-Section (Save Students and Corresponding Configured Fee)
        'For i = 0 To lstClass.Items.Count - 1

        '    'Move List of Students in Current Class-Section-ASID-Status to Report Table                
        '    sqlStr = "Insert into rptFeeDue (SID,RegNo, FeeBookNo, SName,FName,ClassName,SecName) " & _
        '    " Select SID,RegNo, FeeBookNo, SName,FName,ClassName,SecName From vw_Student " & _
        '    " Where ClassName='" & lstClass.Items(i).Text & "' and " & _
        '    " SecName='" & lstSection.Items(i).Text & "' and " & _
        '    " StatusName='" & cboStatus.Text & "' and " & _
        '    " ASID=" & Request.Cookies("ASID").Value

        '    
        '    
        '    ExecuteQuery_Update(SqlStr)

        'Next

        'sqlStr = "Update rptFeeDue Set FeeDueAmount=0 where FeeDueAmount is null"
        '
        '
        'ExecuteQuery_Update(SqlStr)

        'sqlStr = "Update rptFeeDue Set FeeDepositedAmount=0 Where FeeDepositedAmount is null"
        '
        '
        'ExecuteQuery_Update(SqlStr)

        'sqlStr = "Update rptFeeDue Set FeeConfigAmount=0 Where FeeConfigAmount is null"
        '
        '
        'ExecuteQuery_Update(SqlStr)

        'lstClass.Items.Clear()
        'lstSection.Items.Clear()

        ''Select All Students from Report Table
        'Dim LstSID As New ListBox
        'sqlStr = "Select SID, ClassName, SecName From rptFeeDue "
        '
        '

        'Dim StudReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        'While StudReader.Read
        '    LstSID.Items.Add(StudReader(0))
        '    lstClass.Items.Add(StudReader(1))
        '    lstSection.Items.Add(StudReader(2))
        'End While
        'StudReader.Close()

        'Dim TotalConfig As Double = 0, TotalDeposit As Double = 0

        ''For All Students in Rpeort Table Find Config Fee, Deposited Fee, Already Marked Late Fee and Fine (Term-wise)
        'For i = 0 To LstSID.Items.Count - 1

        '    Dim FeeCount As Integer = 0
        '    Dim ConfigAmount As Double = 0, DepositedAmount As Double = 0
        '    Try
        '        ConfigAmount = cboTerm.Text * (GetBusLocationAmt(Request.Cookies("ASID").Value, LstSID.Items(i).Text, 1).Split("-")(1).ToString)
        '    Catch ex As Exception

        '    End Try


        '        TotalConfig += ConfigAmount

        '        'Find Deposited Fee (All including Concession)
        '    sqlStr = " Select Sum(Abs(DepositeAmt))+ Sum(Abs(FineAmt)) as FD From vw_BusFee Where SID=" & LstSID.Items(i).Text & " and TermNo <=" & cboTerm.Text & " Group By SID"

        '        
        '        

        '        FeeCount = 0
        '        Dim FeeReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)

        '        While FeeReader.Read
        '            FeeCount += 1
        '            DepositedAmount = FeeReader("FD")
        '        End While
        '        FeeReader.Close()
        '    TotalDeposit += DepositedAmount
        '    Dim Fine As Double = 0
        '    Dim z As Integer = 0
        '    For z = 1 To cboTerm.Text
        '        Dim depositeDate As String = ""
        '        sqlStr = " Select DepositeDate From vw_BusFee Where SID=" & LstSID.Items(i).Text & " and TermNo =" & z & ""
        '        
        '        
        '        Dim DateReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        '        While DateReader.Read
        '            depositeDate = DateReader(0)
        '        End While
        '        DateReader.Close()
        '        Try
        '            Fine += GetBusDueAmountForTerm(Request.Cookies("ASID").Value, z, LstSID.Items(i).Text, depositeDate)
        '        Catch ex As Exception

        '        End Try
        '    Next

        '    sqlStr = "Update rptFeeDue Set FeeConfigAmount=" & TotalConfig & ",FeeDepositedAmount=" & TotalDeposit & ",Fine='" & Fine & "',CollegeNote='" & cboTerm.Text & "' Where SID=" & LstSID.Items(i).Text
        '    
        '    
        '    ExecuteQuery_Update(SqlStr)
        '    TotalConfig = 0
        '    TotalDeposit = 0
        '    ConfigAmount = 0
        '    DepositedAmount = 0
        'Next    'End of student List

        ''Find the Difference and Update Report Table
        'sqlStr = "Update rptFeeDue Set FeeDueAmount=FeeDueAmount + FeeConfigAmount - FeeDepositedAmount + Fine"
        '
        '
        'ExecuteQuery_Update(SqlStr)

        ''Remove All Entries not belonging to Dues
        'sqlStr = "Delete From rptFeeDue Where FeeDueAmount<=0"
        '
        '
        'ExecuteQuery_Update(SqlStr)

        '
        '

        ''Header Contents
        'lblSchoolName.Text = FindSchoolDetails(1)
        'lblTitle.Text = "Bus Fee Due List for As On " & Now.Date.Day & "/" & Now.Date.Month & "/" & Now.Date.Year

        ''Bind DataGrid
        'GridView1.DataBind()

        ''If No Content, Hide Print Button
        'If GridView1.Rows.Count > 0 Then
        '    lblSchoolName.Visible = True
        '    lblTitle.Visible = True
        '    btnViewDetails.Enabled = True
        '    btnPrint.Enabled = True
        '    btnExcel.Enabled = True
        '    btnSendSMS.Enabled = True
        '    GridView1.Visible = True
        'Else
        '    lblSchoolName.Visible = False
        '    lblTitle.Visible = False
        '    btnViewDetails.Enabled = False
        '    btnPrint.Enabled = False
        '    btnExcel.Enabled = False
        '    btnSendSMS.Enabled = False
        '    GridView1.Visible = False
        'End If

        ''Show Total in Footer
        'Dim DueTotal As Double = 0

        'For i = 0 To GridView1.Rows.Count - 1
        '    DueTotal += Val(GridView1.Rows(i).Cells(6).Text)
        'Next

        'If GridView1.Rows.Count > 0 Then
        '    GridView1.FooterRow.Cells(6).Text = DueTotal
        '    GridView1.FooterRow.Cells(6).Font.Bold = True
        'Else
        '    'GridView1.FooterRow.Cells(6).Text = 0
        'End If
        PrepareDuesReport()
    End Sub
    Private Sub PrepareDuesReport()
        Dim sql As String = ""
        Dim MyHeader As String = ""
        Dim ReportPath As String = "Report/"

        sql = "Select  *  From vw_Student"
        sql += " where SchoolName='" & cboSchoolName.Text & "' and StatusName='" & cboStatus.Text & "' AND ASID=" & Request.Cookies("ASID").Value
        If cboClass.Text <> "ALL" Then
            sql += " And ClassName='" & cboClass.Text & "' "
        End If
        If cboClass.Text <> "ALL" And cboSection.Text <> "ALL" Then
            sql += " And SecName='" & cboSection.Text & "' "
        ElseIf cboClass.Text <> "ALL" And cboSection.Text = "ALL" Then
            sql += " And SecID in (Select Distinct SecID From vw_ClassStudent where ClassName='" & cboClass.Text & "')"
        End If

        'sql += " Order by DisplayOrder"
        Dim ds As New DataSet
        ds = ExecuteQuery_DataSet(sql, "tbl")
        ds.Tables(0).Columns.Add("DueAmount", System.Type.GetType("System.Decimal"))

        Dim ConfigAmount As Double = 0, DepositedAmount As Double = 0
        Dim Fine As Double = 0



        For Each Row As DataRow In ds.Tables(0).Rows
            Dim SID As Integer = Row("SID")
            ConfigAmount = 0
            DepositedAmount = 0
            Fine = 0
            Dim TermID As String = ""
            For t = 1 To cboTerm.SelectedIndex
                TermID = cboTerm.Items(t).Value & ","
                Try
                    Fine += GetBusDueAmountForTerm(Request.Cookies("ASID").Value, cboTerm.Items(t).Value, SID)
                Catch ex As Exception

                End Try
            Next
            TermID = TermID.Substring(0, TermID.Length - 1)
            Dim FeeCount As Integer = 0
            Try
                ConfigAmount = GetBusConfigAmount(SID, TermID, 1)
                'cboTerm.Text * (GetBusLocationAmt(Request.Cookies("ASID").Value, LstSID.Items(i).Text, 1).Split("-")(1).ToString)
            Catch ex As Exception

            End Try
            Try
                DepositedAmount = GetBusConfigAmount(SID, TermID, 2)
            Catch ex As Exception

            End Try

            Row("DueAmount") = ConfigAmount - DepositedAmount + Fine
        Next

        Dim rds As ReportDataSource = New ReportDataSource()
        rds.Name = "DataSet1" ' Change to what you will be using when creating an objectdatasource
        rds.Value = ds.Tables(0)
        With ReportViewer1   ' Name of the report control on the form
            .Reset()
            .ProcessingMode = ProcessingMode.Local
            .LocalReport.DataSources.Clear()
            .Visible = True
            MyHeader = "Bus Due List Upto Term: " & cboTerm.SelectedItem.Text
            ReportPath += "rptBusFeeDue.rdlc"
            .LocalReport.ReportPath = ReportPath

            .LocalReport.DataSources.Add(rds)
        End With

        Dim params(1) As Microsoft.Reporting.WebForms.ReportParameter
        params(0) = New Microsoft.Reporting.WebForms.ReportParameter("param", MyHeader, Visible)
        params(1) = New Microsoft.Reporting.WebForms.ReportParameter("SchoolName", FindSchoolDetails(1), Visible)
        Me.ReportViewer1.LocalReport.SetParameters(params)
        ReportViewer1.Visible = True

        ReportViewer1.LocalReport.Refresh()

    End Sub
    Private Function GetBusConfigAmount(SID As String, TermID As Integer, type As Integer) As Double
        Dim sqlstr As String = ""
        If type = 1 Then 'Config Amount
            sqlstr = "Select sum(Amount) From BusstudentMap Where TermNo in (" & TermID & ")  and BusRequired=1 and SID=" & SID
        Else '2 for Deposit Amount 
            sqlstr = " Select Sum(Abs(DepositeAmt))+ Sum(Abs(FineAmt))+ Sum(Abs(Concession)) as FD From vw_BusFee Where SID=" & SID & " and TermNo in (" & TermID & ")  and IsCancel=0 Group By SID"
        End If
        'sqlstr = "Select ConveyanceTypeID From locationName Where locationName='" & LocationName
        Dim rv As Double = 0
        Try
            rv = ExecuteQuery_ExecuteScalar(sqlstr)
        Catch ex As Exception

        End Try
        Return rv
    End Function

    Protected Sub cboSchoolName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSchoolName.SelectedIndexChanged
        LoadMasterInfo(2, cboClass, cboSchoolName.Text)
        cboClass.Items.Add("ALL")
        cboSchoolName.Focus()
    End Sub
End Class

