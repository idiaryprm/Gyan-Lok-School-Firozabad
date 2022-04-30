Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary_Fee.CLS_iDiary_Fee
Imports Microsoft.Reporting.WebForms
Public Class PettyStatement
    Inherits System.Web.UI.Page


    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        Try

            If Request.Cookies("UType").Value.ToString.Contains("Account") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
            End If

        Catch ex As Exception

            If ex.Message.Contains("Object reference not set to an instance of an object") Then
                Response.Redirect("~/Login.aspx")
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

    Private Sub InitControls()
        txtFrom.Text = Now.Date.ToString("dd/MM/yyyy")
        txtTo.Text = Now.Date.ToString("dd/MM/yyyy")
        LoadPettyCashHeads(cboHead)
        cboHead.Focus()
    End Sub
    Public Shared Function LoadPettyCashHeads(cbo As DropDownList) As Integer

       
       
       

        Dim sqlstr As String = "Select PCHeadName From PettyCashHeadMaster Order By PCHeadName"
        
        
        

        Dim myReader As System.Data.SqlClient.SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        cbo.Items.Clear()
        cbo.Items.Add("")
        While myReader.Read
            cbo.Items.Add(myReader(0))
        End While
        myReader.Close()

        
        

        Return 0

    End Function
    Public Shared Function GetHeadID(HeadName As String) As Integer

       
       
       
        Dim rv As Integer = 0
        Dim sqlstr As String = "Select PCHeadID From PettyCashHeadMaster where PCHeadName='" & HeadName & "'"
        
        
        

        rv = ExecuteQuery_ExecuteScalar(SqlStr)
        
        

        Return rv
    End Function
    Public Shared Function GetHeadName(HeadID As String) As String

       
       
       
        Dim rv As String = ""
        Dim sqlstr As String = "Select PCHeadName From PettyCashHeadMaster where PCHeadID='" & HeadID & "'"
        
        
        

        rv = ExecuteQuery_ExecuteScalar(SqlStr)
        
        

        Return rv
    End Function
    Public Function GetOpeningBalace(LedgerID As String, FromD As String) As Double
        Dim con As SqlConnection = Nothing
        Dim result As Double = 0
        Dim PVTotal As Double = 0
        Dim RVTotal As Double = 0
        Dim OPBal As Double = 0
        'Dim DRCR As String = ""
        Dim HeadType As Integer = GetLedgerType(LedgerID)

        con = New SqlConnection(ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString())
        'Dim sqlStr As String = "Select Sum(Amount) from vwPettyCashTransaction where DRCR='DR' and Vr_Dt< '" & FromD & "' and PCHeadID=" & LedgerID
        Dim sqlStr As String = "Select Sum(Amount) from vwPettyCashTransaction where Doc_Code='PV' and Vr_Dt< '" & FromD & "' and PCHeadID=" & LedgerID
        Dim cmd As New SqlCommand(sqlStr, con)
        con.Open()
        Try
            PVTotal = cmd.ExecuteScalar
        Catch ex As Exception

        End Try
        sqlStr = "Select Sum(Amount) from vwPettyCashTransaction where Doc_Code='RV' and VR_DT< '" & FromD & "' and PCHeadID=" & LedgerID
        'sqlStr = "Select Sum(Amount) from vwPettyCashTransaction where DRCR='CR' and VR_DT< '" & FromD & "' and PCHeadID=" & LedgerID
        cmd.CommandText = sqlStr

        Try
            RVTotal = cmd.ExecuteScalar
        Catch ex As Exception

        End Try
        sqlStr = "Select OpBal from PettyCashHeadMaster where PCHeadID=" & LedgerID
        cmd.CommandText = sqlStr

        Try
            OPBal = cmd.ExecuteScalar
        Catch ex As Exception

        End Try
        'sqlStr = "Select OPBalDRCR from PettyCashHeadMaster where PCHeadID=" & LedgerID
        'cmd.CommandText = sqlStr

        'Try
        '    DRCR = cmd.ExecuteScalar
        'Catch ex As Exception

        'End Try


        con.Close()
        cmd.Dispose()
        If HeadType = 3 Then
            result = RVTotal - PVTotal + OPBal
        Else
            result = PVTotal - RVTotal + OPBal
        End If
        Return result
    End Function

    Public Function GetLedgerType(LedgerID As String) As Integer
        Dim con As SqlConnection = Nothing


        Dim HeadType As Integer = 0

        con = New SqlConnection(ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString())
        Dim sqlStr As String = "Select TransTypeID from PettyCashHeadMaster where PCHeadID=" & LedgerID
        Dim cmd As New SqlCommand(sqlStr, con)
        con.Open()
      

      
        Try
            HeadType = cmd.ExecuteScalar
        Catch ex As Exception

        End Try

        con.Close()
        cmd.Dispose()
       

        Return HeadType
    End Function
    Public Function GetDisplayOrder(LedgerID As String) As Integer
        Dim con As SqlConnection = Nothing


        Dim HeadType As Integer = 0

        con = New SqlConnection(ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString())
        Dim sqlStr As String = "Select displayOrder from PettyCashHeadMaster where PCHeadID=" & LedgerID
        Dim cmd As New SqlCommand(sqlStr, con)
        con.Open()



        Try
            HeadType = cmd.ExecuteScalar
        Catch ex As Exception

        End Try

        con.Close()
        cmd.Dispose()


        Return HeadType
    End Function

    Protected Sub btnView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnView.Click

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

        Dim FromDate1 As String = ""
        Dim ToDate1 As String = ""
        Try
            FromDate1 = txtFrom.Text.Substring(6, 4) & "/" & txtFrom.Text.Substring(3, 2) & "/" & txtFrom.Text.Substring(0, 2)
        Catch ex As Exception
            lblStatus.Text = "Invalid From Date"
            txtFrom.Focus()
            Exit Sub
        End Try
        Try
            ToDate1 = txtTo.Text.Substring(6, 4) & "/" & txtTo.Text.Substring(3, 2) & "/" & txtTo.Text.Substring(0, 2)
        Catch ex As Exception
            lblStatus.Text = "Invalid To Date"
            txtTo.Focus()
            Exit Sub
        End Try
      
        lblSchoolName.Text = FindSchoolDetails(1)
        If cboHead.Text = "" Then
            lblTitle.Text = "Statement: Dt: " & txtFrom.Text & " - " & txtTo.Text
        Else
            lblTitle.Text = cboHead.Text & " - " & "Statement: Dt: " & txtFrom.Text & " - " & txtTo.Text
        End If

     
        Dim sqlStr As String = "select * from vwPettyCashTransaction where Vr_Dt between '" & FromDate1 & "' and '" & ToDate1 & "' "
        If cboHead.Text <> "" Then
            sqlStr += " and PCHeadName='" & cboHead.Text & "'"
        End If
        'If rbPayment.Checked = True Then
        '    sqlStr += " and Doc_Code='PV'"
        'Else
        '    sqlStr += " and Doc_Code='RV'"
        'End If
        sqlStr += " Order by Vr_Dt"
        Dim ds As New DataSet
        ds = ExecuteQuery_DataSet(sqlStr, "tbl")
        Dim LedgerID As Integer = 0
        If cboHead.Text <> "" Then
            LedgerID = GetHeadID(cboHead.Text)
        End If


        Dim lstClientID As New ListBox

        If Val(LedgerID) > 0 Then
            lstClientID.Items.Add(LedgerID)
        Else
            Dim PrvClientID As Integer = 0
            Dim ClientID As Integer = 0
            For Each Row As DataRow In ds.Tables(0).Rows
                ClientID = Row("PCHeadID")
                If PrvClientID = 0 Then
                    PrvClientID = ClientID
                Else
                    If ClientID <> PrvClientID Then
                        lstClientID.Items.Add(PrvClientID)
                        PrvClientID = ClientID
                    End If
                End If
            Next
            If ClientID <> 0 Then
                lstClientID.Items.Add(ClientID)
            End If
        End If

        For i = 0 To lstClientID.Items.Count - 1
            Dim Amount As Double = GetOpeningBalace(lstClientID.Items(i).ToString, FromDate1)
            'Dim Description As String = "Openning Balance"
            Dim newRow As DataRow = ds.Tables(0).NewRow()
            newRow("PCHeadID") = lstClientID.Items(i).ToString
            newRow("PCHeadName") = GetHeadName(lstClientID.Items(i).ToString)
            newRow("Description") = "Openning Balance"
            newRow("Vr_Dt") = "1900/01/01"
            If Amount < 0 Then
                newRow("DRCR") = "CR"
                Amount = Amount * -1
            Else
                newRow("DRCR") = "DR"
            End If
            newRow("Amount") = Amount

            ds.Tables(0).Rows.Add(newRow)
        Next





        Dim rds As ReportDataSource = New ReportDataSource()

        rds.Name = "DataSetRDLC" ' Change to what you will be using when creating an objectdatasource
        '    End If

        rds.Value = ds.Tables(0)
        With ReportViewer1   ' Name of the report control on the form
            .Reset()
            .ProcessingMode = ProcessingMode.Local
            .LocalReport.DataSources.Clear()
            .Visible = True
            .LocalReport.ReportPath = "rptLedger.rdlc"
            '.LocalReport.ReportPath = "rptLedgerSummary.rdlc"
            .LocalReport.DataSources.Add(rds)
        End With
        Dim params(0) As Microsoft.Reporting.WebForms.ReportParameter
        params(0) = New Microsoft.Reporting.WebForms.ReportParameter("DateRange", lblTitle.Text, Visible)


        Me.ReportViewer1.LocalReport.SetParameters(params)
        ReportViewer1.Visible = True

        ReportViewer1.LocalReport.Refresh()


    End Sub
  
    Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        lblSchoolName.Visible = True
    End Sub

    Protected Sub btnDetail_Click(sender As Object, e As EventArgs) Handles btnDetail.Click
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
        ProcessReport(1)
    End Sub
    Private Sub ProcessReport(type As Integer)
        Dim FromDate1 As String = ""
        Dim ToDate1 As String = ""
        Try
            FromDate1 = txtFrom.Text.Substring(6, 4) & "/" & txtFrom.Text.Substring(3, 2) & "/" & txtFrom.Text.Substring(0, 2)
        Catch ex As Exception
            lblStatus.Text = "Invalid From Date"
            txtFrom.Focus()
            Exit Sub
        End Try
        Try
            ToDate1 = txtTo.Text.Substring(6, 4) & "/" & txtTo.Text.Substring(3, 2) & "/" & txtTo.Text.Substring(0, 2)
        Catch ex As Exception
            lblStatus.Text = "Invalid To Date"
            txtTo.Focus()
            Exit Sub
        End Try

        lblSchoolName.Text = FindSchoolDetails(1)
        If cboHead.Text = "" Then
            If rbPayment.Checked = True Then
                lblTitle.Text = "Payment Statement: Dt: " & txtFrom.Text & " - " & txtTo.Text
            Else
                lblTitle.Text = "Recipt Statement: Dt: " & txtFrom.Text & " - " & txtTo.Text
            End If

        Else
            If rbPayment.Checked = True Then
                lblTitle.Text = cboHead.Text & " - " & "Payment Statement: Dt: " & txtFrom.Text & " - " & txtTo.Text
            Else
                lblTitle.Text = cboHead.Text & " - " & "Receipt Statement: Dt: " & txtFrom.Text & " - " & txtTo.Text
            End If

        End If

        Dim DocType As String = ""
        If rbPayment.Checked = True Then
            DocType = "PV"
        Else
            DocType = "RV"
        End If
        If type = 2 Then
            DocType = "PV"
        End If
        Dim sqlStr As String = "select * from vwPettyCashTransaction where Vr_Dt between '" & FromDate1 & "' and '" & ToDate1 & "' "
        If cboHead.Text <> "" Then
            sqlStr += " and PCHeadName='" & cboHead.Text & "'"
        End If
        'If rbPayment.Checked = True Then
        '    sqlStr += " and Doc_Code='PV'"
        'Else
        '    sqlStr += " and Doc_Code='RV'"
        'End If
        sqlStr += " Order by Vr_Dt"
        Dim ds As New DataSet
        ds = ExecuteQuery_DataSet(sqlStr, "tbl")
        Dim LedgerID As Integer = 0
        If cboHead.Text <> "" Then
            LedgerID = GetHeadID(cboHead.Text)
        End If


        Dim lstClientID As New ListBox
        lstClientID = GetHeadList()
        'If Val(LedgerID) > 0 Then
        '    lstClientID.Items.Add(LedgerID)
        'Else
        '    Dim PrvClientID As Integer = 0
        '    Dim ClientID As Integer = 0
        '    For Each Row As DataRow In ds.Tables(0).Rows
        '        ClientID = Row("PCHeadID")
        '        If PrvClientID = 0 Then
        '            PrvClientID = ClientID
        '        Else
        '            If ClientID <> PrvClientID Then
        '                lstClientID.Items.Add(PrvClientID)
        '                PrvClientID = ClientID
        '            End If
        '        End If
        '    Next
        '    If ClientID <> 0 Then
        '        lstClientID.Items.Add(ClientID)
        '    End If
        'End If

        For i = 0 To lstClientID.Items.Count - 1
            Dim Amount As Double = GetOpeningBalace(lstClientID.Items(i).ToString, FromDate1)
            Dim AmountOther As Double = Amount
            Dim LedgerType As Integer = GetLedgerType(lstClientID.Items(i).ToString)
            Dim DisplayOrder As Integer = GetDisplayOrder(lstClientID.Items(i).ToString)
            'Dim Description As String = "Openning Balance"
            Dim newRow As DataRow = ds.Tables(0).NewRow()
            newRow("PCHeadID") = lstClientID.Items(i).ToString
            newRow("DisplayOrder") = DisplayOrder
            newRow("PCHeadName") = GetHeadName(lstClientID.Items(i).ToString)
            newRow("TransRemark") = "Openning Balance"
            newRow("Vr_Dt") = "1900/01/01"
            newRow("TransTypeID") = LedgerType
            newRow("Doc_Code") = DocType
            If Amount < 0 Then
                newRow("DRCR") = "CR"
                Amount = Amount * -1
            Else
                newRow("DRCR") = "DR"
            End If
            If LedgerType = 3 And DocType = "PV" Then
                Amount = 0
            ElseIf LedgerType = 4 And DocType = "RV" Then
                Amount = 0
            End If
            newRow("Amount") = Amount

            ds.Tables(0).Rows.Add(newRow)


            Dim newRow1 As DataRow = ds.Tables(0).NewRow()
            newRow1("PCHeadID") = lstClientID.Items(i).ToString
            newRow1("DisplayOrder") = DisplayOrder
            newRow1("PCHeadName") = GetHeadName(lstClientID.Items(i).ToString)
            newRow1("TransRemark") = "Openning Balance"
            newRow1("Vr_Dt") = "1900/01/01"
            newRow1("TransTypeID") = LedgerType
            If DocType = "PV" Then
                newRow1("Doc_Code") = "RV"
            Else
                newRow1("Doc_Code") = "PV"
            End If

            If AmountOther < 0 Then
                newRow1("DRCR") = "CR"
                AmountOther = AmountOther * -1
            Else
                newRow1("DRCR") = "DR"
            End If
            If LedgerType = 3 And DocType = "RV" Then
                AmountOther = 0
            ElseIf LedgerType = 4 And DocType = "PV" Then
                AmountOther = 0
            End If
            newRow1("Amount") = AmountOther

            ds.Tables(0).Rows.Add(newRow1)
        Next





        Dim rds As ReportDataSource = New ReportDataSource()

        rds.Name = "DataSetRDLC" ' Change to what you will be using when creating an objectdatasource
        '    End If

        rds.Value = ds.Tables(0)
        With ReportViewer1   ' Name of the report control on the form
            .Reset()
            .ProcessingMode = ProcessingMode.Local
            .LocalReport.DataSources.Clear()
            .Visible = True
            '.LocalReport.ReportPath = "rptLedger.rdlc"
            If type = 1 Then
                .LocalReport.ReportPath = "rptLedgerSummary.rdlc"
            Else
                .LocalReport.ReportPath = "rptLedgerBalanceSheet.rdlc"
            End If
            .LocalReport.DataSources.Add(rds)
        End With

        Dim params(2) As Microsoft.Reporting.WebForms.ReportParameter
        params(0) = New Microsoft.Reporting.WebForms.ReportParameter("DateRange", lblTitle.Text, Visible)
        params(1) = New Microsoft.Reporting.WebForms.ReportParameter("Doc", DocType, Visible)
        params(2) = New Microsoft.Reporting.WebForms.ReportParameter("BSDetail", txtFrom.Text & " - " & txtTo.Text, Visible)

        Me.ReportViewer1.LocalReport.SetParameters(params)
        ReportViewer1.Visible = True

        ReportViewer1.LocalReport.Refresh()
    End Sub
    Private Function GetHeadList() As ListBox
        Dim lstHead As New ListBox
        lstHead.Items.Clear()
       
       
       

        


        Dim sqlStr As String = "Select PCHeadID From PettyCashHeadMaster Order by DisplayOrder "

        
        
        Dim ClassReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While ClassReader.Read
            lstHead.Items.Add(ClassReader(0))
        End While
        ClassReader.Close()
        Return lstHead
    End Function

    Protected Sub btnBalanceSheet_Click(sender As Object, e As EventArgs) Handles btnBalanceSheet.Click
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
        rbPayment.Checked = True
        ProcessReport(2)
    End Sub
End Class