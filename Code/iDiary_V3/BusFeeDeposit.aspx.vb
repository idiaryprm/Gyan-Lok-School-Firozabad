Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary_Fee.CLS_iDiary_Fee
Imports System.IO
Imports Microsoft.Reporting.WebForms

Public Class BusFeeDeposit
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
  
    Private Function ShowStudentDetails(type As String) As Integer
        Dim sqlStr As String = ""
        If type = 1 Then
            sqlStr = "Select SID, RegNo, SName, FName, ClassName, SecName,FeeBookNo From vw_Student Where FeeBookNo='" & txtFeeBookNo.Text & "' AND ASID=" & Request.Cookies("ASID").Value
        Else
            sqlStr = "Select SID, RegNo, SName, FName, ClassName, SecName,FeeBookNo From vw_Student Where regNo='" & txtRegNo.Text & "' AND ASID=" & Request.Cookies("ASID").Value
        End If

        Dim myCount As Integer = 0
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            txtSID.Text = myReader("SID")
            txtRegNo.Text = myReader("RegNo")
            txtSName.Text = myReader("SName")
            txtFName.Text = myReader("FName")
            txtClass.Text = myReader("ClassName")
            txtSec.Text = myReader("SecName")
            txtFeeBookNo.Text = myReader("FeeBookNo")
            myCount += 1
        End While
        myReader.Close()
        If txtSID.Text = "" Then

        Else
            LoadFeeTerms(chkTermList, 0, "BusFee")
            FillHistoryGrid()
            FillFeeConfig()
        End If
        Return myCount
    End Function
    Private Sub FillHistoryGrid()
        SqlDataSource2.SelectCommand = "SELECT [BusFeeID],[DepositeDate] as DepositeDate,[BusTermNo] as TermNo,[DepositeAmt] as DepositeAmt,[Concession] as Concession,[FineAmt] as FineAmt from vw_BusFee where SID=" & Val(txtSID.Text) & " AND IsCancel=0 group by [BusFeeID],[DepositeDate],[DepositeAmt],[BusTermNo],[Concession],[FineAmt] order by [DepositeDate] DESC"
        GridView1.Visible = True
        GridView1.DataBind()
        'If GridView1.Rows.Count > 0 Then
        '    lblDepositAmount.Visible = True
        'Else
        '    lblDepositAmount.Visible = False
        'End If
    End Sub
    Private Sub FillFeeConfig()
        SqlDataSource3.SelectCommand = "SELECT BusTermID,BusTermNo,BusTermName FROM BusTermMaster"
        'where FeeGroupID = " & Val(txtFeeGroupID.Text)"
        GridView2.Visible = True
        GridView2.DataBind()

    End Sub
    'If GridView2.Rows.Count > 0 Then
    '    If txtConfigType.Text = "1" Then
    '        lblConfiguredAmount.Text = "Configuration: Student Wise"
    '    Else
    '        lblConfiguredAmount.Text = "Configuration: Fee Group Wise"
    '    End If
    '    lblConfiguredAmount.Visible = True
    'Else
    '    lblConfiguredAmount.Visible = False
    'End If
    'Protected Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click

    '    lblStatus.Text = ""

    '    If ShowStudentDetails() > 0 Then

    '        LoadPreviousPaymentHistory(cboHistory, Val(txtSID.Text), Request.Cookies("ASID").Value)
    '        If cboHistory.Items.Count > 1 Then
    '            btnCompleteHistory.Enabled = True
    '        Else
    '            btnCompleteHistory.Enabled = False
    '        End If

    '        Dim TermNo As Integer = 0, i As Integer = 0
    '        For i = 0 To CheckBoxList1.Items.Count - 1
    '            If CheckBoxList1.Items(i).Selected = True Then
    '                TermNo = CheckBoxList1.Items(i).Text
    '            End If
    '        Next

    '        If TermNo > 0 Then
    '            CreateTable()    'Old Code
    '            If CheckBusFeeDepositExistance(Request.Cookies("ASID").Value, txtFeeBookNo.Text, TermNo) = True Then
    '                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Fee Entry Already Exists for selected Term');", True)
    '                lblStatus.Text = "Fee Entry Already Exists for selected Term."
    '            End If
    '        End If

    '        CheckBoxList1.Focus()
    '    Else    'Student Info Not Found

    '        Dim TempFeeBookNo As String = txtFeeBookNo.Text
    '        InitControls()
    '        txtFeeBookNo.Text = TempFeeBookNo
    '        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Invalid Fee Book No');", True)
    '    End If


    'End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("ActiveTab") = 11
        Response.Cookies("ActiveTab").Value = 11
        Response.Cookies("ActiveTab").Expires = DateTime.Now.AddHours(1)

        If IsPostBack = False Then
            InitControls()
        Else
            'Dim Fine As Double = 0
            'Dim TermNo As Integer = 0, i As Integer = 0
            'For i = 0 To CheckBoxList1.Items.Count - 1
            '    If CheckBoxList1.Items(i).Selected = True Then
            '        TermNo = CheckBoxList1.Items(i).Text
            '        Fine += GetBusDueAmountForTerm(Request.Cookies("ASID").Value, TermNo, txtSID.Text, Now.Date)
            '    End If
            'Next
            'txtBusFine.Text = Fine
            'If TermNo > 0 Then
            '    If ViewState("myTable") = True Then
            '        CreateTable()
            '    End If
            'End If

        End If

        'If chkPast.Checked = True Then
        '    lblFeeDue.Visible = True
        '    lblFeeDue0.Visible = True
        'Else
        '    lblFeeDue.Visible = False
        '    lblFeeDue0.Visible = False

        'End If

        'If cboHistory.SelectedIndex <= 0 Then
        '    btnSlip.Enabled = False
        'Else
        '    btnSlip.Enabled = True
        'End If
        If Request.Cookies("UType").Value.ToString.Contains("Admin") = False And Request.Cookies("UType").Value.ToString.Contains("Bus") = False Then
            btnSave.Enabled = False
            btnRemove.Enabled = False
        End If
    End Sub

    Private Sub InitControls()
        txtChequeBank.Text = ""
        txtChequeDate.Text = Now.Date.ToString("dd/MM/yyyy")
        txtDepositDate.Text = Now.Date.ToString("dd/MM/yyyy")
        txtChequeNo.Text = ""
        txtFeeDepositID.Text = 0
        cboMode.Text = ""
        txtFeeBookNo.Text = ""
        txtRegNo.Text = ""
        txtSName.Text = ""
        txtFName.Text = ""
        txtClass.Text = ""
        txtSec.Text = ""

        'lblTerm.Text = ""

        'txtDD.Text = Now.Date.Day
        'txtMM.Text = Now.Date.Month
        'txtYY.Text = Now.Date.Year

        LoadMasterInfo(12, cboMode)
        Try
            cboMode.Text = FindDefault(12)
        Catch ex As Exception

        End Try
        txtModeRemark.Text = ""
        'cboHistory.Items.Clear()
        'btnCompleteHistory.Enabled = False

        lblStatus.Text = ""
        lblActualAmt.Text = ""
        lblpickuppoint.Text = ""
        txtDepositeAmt.Text = ""
        'myTable.Rows.Clear()

        If Request.Cookies("UType").Value.ToString.Contains("Admin") Then
            btnRemove.Visible = True
        Else
            btnRemove.Visible = False
        End If
        lblActualAmt.Text = ""
        lblpickuppoint.Text = ""
        txtBusFine.Text = ""
        txtDepositeAmt.Text = ""
        txtConcession.Text = ""
        txtFeeBookNo.Focus()

        'chkMultipleEntry.Checked = False

    End Sub

    'Private Sub CreateTable()

    '    myTable.Rows.Clear()

    '    Dim tr1 As New TableRow

    '    Dim td10 As New TableCell
    '    td10.Text = "<B>Fee ID</B>"
    '    td10.HorizontalAlign = HorizontalAlign.Center
    '    tr1.Cells.Add(td10)

    '    Dim td11 As New TableCell
    '    td11.Text = "<B>Fee Type</B>"
    '    td11.HorizontalAlign = HorizontalAlign.Center
    '    tr1.Cells.Add(td11)

    '    Dim td12 As New TableCell
    '    td12.Text = "<B>Actual Amount</B>"
    '    td12.HorizontalAlign = HorizontalAlign.Center
    '    tr1.Cells.Add(td12)

    '    Dim td13 As New TableCell
    '    td13.Text = "<B>Amount to be Deposited</B>"
    '    td13.HorizontalAlign = HorizontalAlign.Center
    '    tr1.Cells.Add(td13)

    '    myTable.Rows.Add(tr1)

    '    Dim sqlStr As String = ""
    '    Dim myTxtBoxNumber As Integer = 1

    '    Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
    '    Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
    '    myConn.Open()

    '    Dim myCommand As New SqlCommand

    '    'Process Late Fee Amount and Type
    '    Dim lstLateFeeTypeID As New ListBox, lstLateFeeAmount As New ListBox
    '    Dim t As Integer = 0
    '    For t = 0 To CheckBoxList1.Items.Count - 1
    '        If CheckBoxList1.Items(t).Selected Then
    '            lstLateFeeTypeID.Items.Add(GetLateFeeIDForTerm(Request.Cookies("ASID").Value, CheckBoxList1.Items(t).Text))
    '            lstLateFeeAmount.Items.Add((GetDueAmountForTerm(Request.Cookies("ASID").Value, CheckBoxList1.Items(t).Text, txtFeeBookNo.Text)))
    '        End If
    '    Next

    '    'Get All FeeTypes
    '    sqlStr = "Select FeeTypeID, FeeTypeName From FeeTypes"
    '    myCommand.CommandText = sqlStr
    '    myCommand.Connection = myConn
    '    Dim myReader As SqlDataReader = myCommand.ExecuteReader

    '    While myReader.Read
    '        Dim trx As New TableRow

    '        Dim tdx0 As New TableCell
    '        tdx0.Text = myReader(0)
    '        tdx0.HorizontalAlign = HorizontalAlign.Center
    '        trx.Cells.Add(tdx0)

    '        Dim tdx1 As New TableCell
    '        tdx1.Text = myReader(1)
    '        tdx1.HorizontalAlign = HorizontalAlign.Center
    '        trx.Cells.Add(tdx1)

    '        Dim txtAAmount As New Label()
    '        txtAAmount.ID = "txtA" & myTxtBoxNumber
    '        txtAAmount.Width = 100
    '        Dim tdx2 As New TableCell
    '        tdx2.Controls.Add(txtAAmount)
    '        tdx2.HorizontalAlign = HorizontalAlign.Center
    '        trx.Cells.Add(tdx2)

    '        Dim txtAmount As New TextBox()
    '        txtAmount.ID = "txtD" & myTxtBoxNumber
    '        txtAmount.Width = 100
    '        txtAmount.Attributes.Add("onchange", "javascript: ShowTotal();")
    '        Dim tdx3 As New TableCell
    '        tdx3.Controls.Add(txtAmount)
    '        tdx3.HorizontalAlign = HorizontalAlign.Center
    '        trx.Cells.Add(tdx3)

    '        myTable.Rows.Add(trx)

    '        myTxtBoxNumber += 1
    '    End While
    '    myReader.Close()

    '    'Retrieve Concession Fee Type Config(Given Additionally During Fee Deposit)
    '    Dim ConcessionFeeTypeID As Integer = GetConcessionFeeID()

    '    Dim i As Integer = 0, j As Integer = 0, myCount As Integer = 0
    '    Dim DefaultFeeTotal As Double = 0, OverallConcessionAmount As Double = 0

    '    For i = 1 To myTable.Rows.Count - 1
    '        Dim myFeeTypeID As String = myTable.Rows(i).Cells(0).Text   'Get FeeTypeID From Table

    '        Dim OldDueAmount As Double = 0
    '        If chkPast.Checked = True Then
    '            '-----Get Old Dues------
    '            Dim OldDueTerm As Integer = 0
    '            For t = 0 To CheckBoxList1.Items.Count - 1
    '                If CheckBoxList1.Items(t).Selected = True Then
    '                    OldDueTerm = CheckBoxList1.Items(t).Text
    '                    Exit For
    '                End If
    '            Next

    '            OldDueAmount = FeeDeposit_PastDuesConsideration(txtSID.Text, OldDueTerm - 1, txtClass.Text, txtSec.Text, myFeeTypeID, Request.Cookies("ASID").Value)
    '        End If

    '        Dim myFeeAmount As Double = 0

    '        For t = 0 To CheckBoxList1.Items.Count - 1
    '            If CheckBoxList1.Items(t).Selected = True Then
    '                myFeeAmount += GetFeeConfigForFeeHead(Request.Cookies("ASID").Value, txtClass.Text, txtSec.Text, myFeeTypeID, CheckBoxList1.Items(t).Text)
    '            End If
    '        Next

    '        CType(myTable.FindControl("txtA" & i), Label).Text = myFeeAmount
    '        CType(myTable.FindControl("txtD" & i), TextBox).Text = myFeeAmount

    '        'Check Whether Current FeeTypeID is AdmissionFee or Not and Whether it is applicable for student or not?
    '        If myFeeTypeID = GetAdmissionFeeID() And AdmissionFeeApplicable(txtSID.Text, Request.Cookies("ASID").Value) = False Then myFeeAmount = 0

    '        'Process Concession
    '        Dim ConcessionAmount As Double = GetConcessionCheck(Request.Cookies("ASID").Value, txtSID.Text, myFeeTypeID, myFeeAmount)
    '        myFeeAmount = myFeeAmount - ConcessionAmount

    '        CType(myTable.FindControl("txtD" & i), TextBox).Text = myFeeAmount

    '        DefaultFeeTotal += myFeeAmount
    '        OverallConcessionAmount += ConcessionAmount

    '        'Process Late Fee Amount
    '        Dim LateFeeAmount As Double = 0
    '        For t = 0 To lstLateFeeTypeID.Items.Count - 1
    '            If myFeeTypeID = lstLateFeeTypeID.Items(t).Text Then
    '                LateFeeAmount += lstLateFeeAmount.Items(t).Text
    '                CType(myTable.FindControl("txtD" & i), TextBox).Text = LateFeeAmount
    '            End If
    '        Next

    '        CType(myTable.FindControl("txtD" & i), TextBox).Text = Val(CType(myTable.FindControl("txtD" & i), TextBox).Text) + OldDueAmount

    '    Next

    '    For i = 1 To myTable.Rows.Count - 1
    '        Dim myFeeTypeID As String = myTable.Rows(i).Cells(0).Text
    '        If myFeeTypeID = ConcessionFeeTypeID Then
    '            CType(myTable.FindControl("txtD" & i), TextBox).Text = OverallConcessionAmount
    '        End If
    '    Next

    '   
    '    myConn.Dispose()

    '    'Display Fee Total
    '    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "MyKey", "ShowTotal();", True)
    '    myTable.EnableViewState = True
    '    ViewState("myTable") = True

    'End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click


        Dim i As Integer = 0
        If txtFeeBookNo.Text.Length <= 0 Then
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Invalid Fee Book No');", True)
            txtFeeBookNo.Focus()
            Exit Sub
        End If
        If txtDepositDate.Text = "" Then
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Invalid Deposit Date');", True)
            txtDepositDate.Focus()
            Exit Sub
        End If

        Dim DeposteDate As String = txtDepositDate.Text.Split("/")(2) & "/" & txtDepositDate.Text.Split("/")(1) & "/" & txtDepositDate.Text.Split("/")(0)
        Try
            Dim a As Date = CDate(DeposteDate)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Invalid Deposit Date');", True)
            txtDepositDate.Focus()
            Exit Sub
        End Try

        If cboMode.SelectedIndex = 0 Then
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Invalid Payment Mode');", True)
            cboMode.Focus()
            Exit Sub
        End If
        Dim depositeamt As Double = 0
        Try
            depositeamt = txtDepositeAmt.Text
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Invalid Deposit Amount');", True)
            txtDepositeAmt.Focus()
            Exit Sub
        End Try

        Dim concession As Double = 0
        Try
            concession = txtConcession.Text
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Invalid Concession Amount');", True)
            txtDepositeAmt.Focus()
        End Try
        Dim FineAmt As Double = 0
        Try
            FineAmt = txtBusFine.Text
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Invalid Fine Amount');", True)
            txtBusFine.Focus()
        End Try

        Dim TermList As String = ""
        For k = 0 To chkTermList.Items.Count - 1
            If chkTermList.Items(k).Selected = True Then
                TermList += chkTermList.Items(k).Text & ","
            End If
        Next
        If TermList = "" Then
            lblStatus.Text = "Please Select atleast one Term"
            Exit Sub
        End If
        TermList = TermList.Substring(0, TermList.Length - 1)

        Dim total As Integer = depositeamt + concession + FineAmt
        If total = 0 Then
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Deposite + Concession + Fine Amount Does Not Zero');", True)
            txtDepositeAmt.Focus()
            Exit Sub
        End If

        'Dim GrandTotal As Integer = total + lblActualAmt.Text
        If Val(lblActualAmt.Text) < depositeamt + concession Then
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Deposite Amount Does Not Greater Than Actual Amount');", True)
            txtDepositeAmt.Focus()
            Exit Sub
        End If
        'If GrandTotal <= 0 Then
        '    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Enter Deposite Amount');", True)
        '    txtDepositeAmt.Focus()
        '    Exit Sub
        'End If


        If cboMode.SelectedIndex = 0 Then
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Invalid Payment Mode');", True)
            cboMode.Focus()
            Exit Sub
        End If
        If cboMode.Text = "Cheque" Then
            If txtChequeNo.Text = "" Then
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Invalid Cheque No');", True)
                txtChequeNo.Focus()
                Exit Sub
            End If
            Dim ChequeDate As String = txtChequeDate.Text.Split("/")(2) & "/" & txtChequeDate.Text.Split("/")(1) & "/" & txtChequeDate.Text.Split("/")(0)
            Try
                Dim a As Date = CDate(ChequeDate)
            Catch ex As Exception
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Invalid Cheque Date');", True)
                txtChequeDate.Focus()
                Exit Sub
            End Try
            If txtChequeBank.Text = "" Then
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Invalid Bank');", True)
                txtChequeBank.Focus()
                Exit Sub
            End If
        End If

        'If cboHistory.Text = "" Then
        '    Save_Log("FEE INSERT")
        'Else
        '    Save_Log("FEE UPDATE")
        'End If
        'For i = 1 To myTable.Rows.Count - 1
        '    If CType(myTable.FindControl("txtD" & i), TextBox).Text.Length <= 0 Then
        '        CType(myTable.FindControl("txtD" & i), TextBox).Text = "0"
        '    End If
        'Next

        Dim PaymentModeID As Integer = FindMasterID(12, cboMode.Text)

        Dim sqlStr As String = ""
        Dim t As Integer = 0

        '''''changes for Overall concesion Change (Amit)
        'get total ount of selected terms (3/4/2)
        Dim count As Integer = 0
        For m As Integer = 0 To chkTermList.Items.Count - 1
            If chkTermList.Items(m).Selected = False Then Continue For
            count = count + 1
        Next
        '''''''''
        Dim locationId As Integer = GetLocationID(lblpickuppoint.Text)
        For t = 0 To chkTermList.Items.Count - 1
            If chkTermList.Items(t).Selected = False Then Continue For
            Dim termid As Integer = FindMasterID(67, chkTermList.Items(t).Text)
            Dim ActualAmt As Double = 0
            Dim DepositAmt As Double = 0
            Try
                'ActualAmt = lblActualAmt.Text.Split("#")(1).ToString()
                ActualAmt = GetDueDetail(FindMasterID(67, chkTermList.Items(t).Text), Request.Cookies("ASID").Value)
                'If txtDepositeAmt.Text > ActualAmt Then
                '    txtDepositeAmt.Text = txtDepositeAmt.Text - ActualAmt
                'End If

                If txtDepositeAmt.Text >= ActualAmt Then
                    DepositAmt = ActualAmt
                    txtDepositeAmt.Text = txtDepositeAmt.Text - DepositAmt
                ElseIf txtDepositeAmt.Text < ActualAmt Then
                    DepositAmt = txtDepositeAmt.Text
                ElseIf Val(txtDepositeAmt.Text) = 0 Then
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Depoist Amount Cannot be Zero...');", True)
                    Exit Sub
                End If
            Catch ex As Exception

            End Try
            If Val(txtFeeDepositID.Text) = 0 And CheckBusFeeDepositExistance(Request.Cookies("ASID").Value, txtSID.Text, termid) = True And chkMultipleEntry.Checked = False Then  'Deny
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Fee Entry for Selected Term already exist...');", True)
                lblStatus.Text = "Fee Entry for Selected Term already exist. Select History to edit an entry."
            ElseIf Val(txtFeeDepositID.Text) = 0 And CheckBusFeeDepositExistance(Request.Cookies("ASID").Value, txtSID.Text, termid) = False Then  'Deny
                Dim CMNO As Integer = GetBusCMNO(Request.Cookies("ASID").Value, Val(txtDepositDate.Text))
                sqlStr = "Insert into BusFeeDeposite (CMNO,ASID,SID,TermNo,DepositeDate,DepositMode,DepositeAmt,ActualAmt,DepositDetails,FineAmt,Concession,LocationID,IsCancel) Values(" & _
                CMNO & "," & Request.Cookies("ASID").Value & "," & Val(txtSID.Text) & "," & termid & "," & _
                "'" & DeposteDate & "'," & _
                PaymentModeID & ",'" & DepositAmt & "','" & ActualAmt & "','" & txtModeRemark.Text & "','" & Val(txtBusFine.Text) & "','" & Val(txtConcession.Text) & "','" & locationId & "',0)"
            ElseIf Val(txtFeeDepositID.Text) = 0 And chkMultipleEntry.Checked = True Then  'Deny
                Dim CMNO As Integer = GetBusCMNO(Request.Cookies("ASID").Value, Val(txtDepositDate.Text))
                sqlStr = "Insert into BusFeeDeposite (CMNO,ASID,SID,TermNo,DepositeDate,DepositMode,DepositeAmt,ActualAmt,DepositDetails,FineAmt,Concession,LocationID,IsCancel) Values(" & _
                CMNO & "," & Request.Cookies("ASID").Value & "," & Val(txtSID.Text) & "," & termid & "," & _
                "'" & DeposteDate & "'," & _
                PaymentModeID & ",'" & DepositAmt & "','" & ActualAmt & "','" & txtModeRemark.Text & "','" & Val(txtBusFine.Text) & "','" & Val(txtConcession.Text) & "','" & locationId & "',0)"

            ElseIf Val(txtFeeDepositID.Text) > 0 Then 'Update
                sqlStr = "Update BusFeeDeposite Set " & _
                "TermNo=" & termid & "," & "DepositeDate='" & DeposteDate & "'," & _
                "DepositMode=" & PaymentModeID & ",DepositeAmt='" & DepositAmt & "',ActualAmt='" & ActualAmt & "'," & "DepositDetails='" & txtModeRemark.Text & "',FineAmt='" & Val(txtBusFine.Text) & "',Concession='" & Val(txtConcession.Text) & "',LocationID='" & locationId & "' Where BusFeeID=" & txtFeeDepositID.Text
            End If

            If sqlStr = "" Then
                'chkTermList.SelectedIndex = -1
                Exit Sub
            End If
            ExecuteQuery_Update(sqlStr)

            Dim FeeDepositID As Integer = 0
            'txtFeeDepositID.Text = 0
            If Val(txtFeeDepositID.Text) = 0 Then
                sqlStr = "Select MAX(BusFeeID) from BusFeeDeposite"
                FeeDepositID = ExecuteQuery_ExecuteScalar(sqlStr)
            Else
                FeeDepositID = Val(txtFeeDepositID.Text)
            End If
            txtFeeDepositID.Text = FeeDepositID

        Next



        '''''''''''''''''''''Entry for Cheque Only
        If cboMode.Text = "Cheque" And txtFeeDepositID.Text = "" Then
            sqlStr = "Select Count(*) From BusFeeCheque Where BusFeeID='" & txtFeeDepositID.Text & "'"

            Dim n As Integer = 0
            n = ExecuteQuery_ExecuteScalar(sqlStr)
            If n = 0 Then
                sqlStr = "INSERT Into BusFeeCheque(BusFeeID,ChequeNo,ChequeBank,ChequeDate,isDishonoured,dishonourDate) Values(" & _
                "'" & Trim(txtFeeDepositID.Text) & "'," & _
                "'" & txtChequeNo.Text & "'," & _
                "'" & txtChequeBank.Text & "'," & _
                "'" & txtChequeDate.Text.Substring(6, 4) & "/" & txtChequeDate.Text.Substring(3, 2) & "/" & txtChequeDate.Text.Substring(0, 2) & "'," & _
                "0," & _
                "'" & Now.Date.ToString("yyyy/MM/dd") & "')"
                ExecuteQuery_Update(sqlStr)
            End If
        End If

        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Fee saved successfully...');", True)
        If chkFeeRcpt.Checked = True Then
            GenFeeReceipt(Val(txtFeeDepositID.Text))
        End If
        chkTermList.SelectedIndex = -1
        PreserveInfo()
        GridView2.Visible = False
        GridView1.Visible = False
        txtFeeBookNo.Focus()

    End Sub
    Protected Function TallyPush(LedgerName As String, Amt As Double, TallyID As Integer, DepositeDate As String) As Boolean
        Dim req As Net.WebRequest = Nothing
        Dim rsp As Net.WebResponse = Nothing
        Dim fileName As String = "<ENVELOPE><HEADER><TALLYREQUEST>Import Data</TALLYREQUEST></HEADER><BODY><IMPORTDATA><REQUESTDESC><REPORTNAME>All Masters</REPORTNAME></REQUESTDESC><REQUESTDATA><TALLYMESSAGE xmlns:UDF=" & ControlChars.Quote & "TallyUDF" & ControlChars.Quote & "><VOUCHER REMOTEID=" & ControlChars.Quote & TallyID & ControlChars.Quote & " VCHTYPE=" & ControlChars.Quote & "Receipt" & ControlChars.Quote & "ACTION=" & ControlChars.Quote & "Create" & ControlChars.Quote & "> <ISOPTIONAL>No</ISOPTIONAL><USEFORGAINLOSS>No</USEFORGAINLOSS><USEFORCOMPOUND>No</USEFORCOMPOUND><VOUCHERTYPENAME>Receipt</VOUCHERTYPENAME><DATE>" & DepositeDate & "</DATE><EFFECTIVEDATE>" & DepositeDate & "</EFFECTIVEDATE><ISCANCELLED>No</ISCANCELLED><USETRACKINGNUMBER>No</USETRACKINGNUMBER><ISPOSTDATED>No</ISPOSTDATED><ISINVOICE>No</ISINVOICE><VOUCHERNUMBER>5</VOUCHERNUMBER><DIFFACTUALQTY>No</DIFFACTUALQTY><NARRATION/><ASPAYSLIP>No</ASPAYSLIP><GUID>7811598w-849g-790n-221j-562536936384-00000002</GUID><REFERENCE>1</REFERENCE><PARTYLEDGERNAME>" & LedgerName & "</PARTYLEDGERNAME><LEDGERENTRIES.LIST><REMOVEZEROENTRIES>No</REMOVEZEROENTRIES><ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE><LEDGERFROMITEM>No</LEDGERFROMITEM><TAXCLASSIFICATIONNAME/><LEDGERNAME>" & LedgerName & "</LEDGERNAME><AMOUNT>" & Amt & "</AMOUNT></LEDGERENTRIES.LIST><LEDGERENTRIES.LIST><REMOVEZEROENTRIES>No</REMOVEZEROENTRIES><ISDEEMEDPOSITIVE>Yes</ISDEEMEDPOSITIVE><LEDGERFROMITEM>No</LEDGERFROMITEM><TAXCLASSIFICATIONNAME/><LEDGERNAME>Cash</LEDGERNAME><AMOUNT>-" & Amt & "</AMOUNT></LEDGERENTRIES.LIST></VOUCHER></TALLYMESSAGE></REQUESTDATA></IMPORTDATA></BODY></ENVELOPE>"


        Dim uri As String = "http://localhost:9002"
        req = Net.WebRequest.Create(uri)
        ' req.Proxy = WebProxy.GetDefaultProxy(); // Enable if using proxy
        req.Method = "POST"
        ' Post method
        req.ContentType = "text/xml"
        ' content type
        req.Headers.Add("Authorization", "Basic reallylongstring")

        Dim writer As New StreamWriter(req.GetRequestStream())
        ' Write the XML text into the stream
        writer.WriteLine(fileName)
        writer.Close()
        ' Send the data to the webserver
        rsp = req.GetResponse()
        GenFeeReceipt(Val(txtSID.Text))
        Return True
        'Label1.Text="Successfully Inserted";
    End Function
    Private Function GetTextFromXMLFile(fileName As String) As String

        Dim reader As New StreamReader(fileName)
        Dim ret As String = reader.ReadToEnd()
        'Label1.Text = ret
        reader.Close()
        Return ret
    End Function
    'Private Sub GenerateFeeSlip(SID As Integer)
    '    Dim sqlStr As String = ""
    '    sqlStr = "Select BusTermName,DepositeAmt,FineAmt,Concession From vw_BusFee where SID = " & SID

    '    Dim IDreader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
    '    Dim FeeDepositAmount As Double = 0
    '    Dim BusFine As Double = 0
    '    Dim Concession As Double = 0
    '    Dim FeeTypeName As String = ""
    '    While IDreader.Read
    '        FeeDepositAmount += Val(IDreader("DepositeAmt"))
    '        Try
    '            FeeTypeName = Val(IDreader("BusTermName"))
    '        Catch ex As Exception

    '        End Try
    '        Try
    '            BusFine += Val(IDreader("FineAmt"))
    '        Catch ex As Exception

    '        End Try
    '        Try
    '            Concession += Val(IDreader("Concession"))
    '        Catch ex As Exception

    '        End Try

    '    End While
    '    IDreader.Close()

    '    Dim totalWord As String = GetNumberAsWords(FeeDepositAmount)
    '    Dim className As String = txtClass.Text & "-" & txtSec.Text
    '    Dim Sql As String = "Select ClassName,SecName,ASName,BusFeeID as FeeDepositID,CMNO,ASID,SID, BusTermName as Installment,locationName as FeeTypeName,sum(DepositeAmt) as FeeDepositAmount,Sum(ActualAmt) as FeeActualAmount,Sum(Concession) as ConcessionAmount,DepositeDate as DepositDate,PMname,SName,FName,RegNo From vw_BusFee Where SID =" & FeeDepositeID & " group by ASName,BusFeeID,CMNO,ASID,SID, BusTermName,DepositeDate,PMname,SName,FName,RegNo,ClassName,SecName,locationName"
    '    Dim ds As New DataSet
    '    ds = ExecuteQuery_DataSet(Sql, "tbl")
    '    Dim rds As ReportDataSource = New ReportDataSource()
    '    rds.Name = "dsReceipt" ' Change to what you will be using when creating an objectdatasource
    '    rds.Value = ds.Tables(0)
    '    With ReportViewer1   ' Name of the report control on the form
    '        .Reset()
    '        .ProcessingMode = ProcessingMode.Local
    '        .LocalReport.DataSources.Clear()
    '        .Visible = True
    '        .LocalReport.ReportPath = "Report/rptFeeReceipt.rdlc"
    '        .LocalReport.DataSources.Add(rds)
    '    End With

    '    Dim params(1) As Microsoft.Reporting.WebForms.ReportParameter
    '    params(0) = New Microsoft.Reporting.WebForms.ReportParameter("TotalWord", totalWord, Visible)
    '    ' params(1) = New Microsoft.Reporting.WebForms.ReportParameter("Installment", Installment, Visible)
    '    params(1) = New Microsoft.Reporting.WebForms.ReportParameter("SchoolName", FindSchoolDetails(1), Visible)

    '    Me.ReportViewer1.LocalReport.SetParameters(params)

    '    ReportViewer1.Visible = True
    '    ReportViewer1.LocalReport.Refresh()
    'End Sub
    Private Sub GenFeeReceipt(FeeDepositeID As Integer)

        Dim sqlStr As String = ""
        Dim feedepositID As String = ""
        Dim TermNo As String = ""
        If txtFeeDepositID.Text = "" Then
            sqlStr = "Select BusFeeID,TermNo From BusFeeDeposite where BusFeeID = " & FeeDepositeID

            Dim IDreader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While IDreader.Read
                feedepositID = feedepositID & IDreader(0) & "','"
                TermNo = TermNo & IDreader(1) & ","
            End While
            IDreader.Close()
            feedepositID = feedepositID.Substring(0, feedepositID.Length - 3)
            TermNo = TermNo.Substring(0, TermNo.Length - 1)
        Else
            feedepositID = Val(txtFeeDepositID.Text)
            For m As Integer = 0 To chkTermList.Items.Count - 1
                If chkTermList.Items(m).Selected = False Then Continue For
                TermNo = TermNo & FindMasterID(67, chkTermList.Items(m).Text) & ","
            Next
            TermNo = TermNo.Substring(0, TermNo.Length - 1)
            'TermNo = TextBox1.Text
        End If
        'sqlStr = "Truncate table rptFeeReceipt"
        'sqlStr = "Insert Into rptFeeReceipt(FeeDepositID,ASID,SID,FeeDepositAmount,FeeActualAmount,BusFine,Concession,depositdate,paymentmode,StudentName,FatherName,AdmissionNo) Select BusFeeID,ASID,SID,DepositeAmt,ActualAmt,FineAmt,Concession,DepositeDate,PMname,SName,FName,RegNo From vw_BusFee Where BusFeeID in ('" & feedepositID & "')"

        Dim FeeDepositeIdArray() As String
        Dim TermNoArray() As String
        Dim IdCount As Integer
        feedepositID = Replace(feedepositID, "','", ",")
        FeeDepositeIdArray = feedepositID.Split(",")
        TermNoArray = TermNo.Split(",")
        For IdCount = 0 To FeeDepositeIdArray.Length - 1

            sqlStr = "Select BusTermName,DepositeAmt,FineAmt,Concession From vw_BusFee where BusFeeID = " & FeeDepositeID

            Dim IDreader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            Dim FeeDepositAmount As Double = 0
            Dim BusFine As Double = 0
            Dim Concession As Double = 0
            Dim FeeTypeName As String = ""
            While IDreader.Read
                FeeDepositAmount += Val(IDreader("DepositeAmt"))
                Try
                    FeeTypeName = Val(IDreader("BusTermName"))
                Catch ex As Exception

                End Try
                Try
                    BusFine += Val(IDreader("FineAmt"))
                Catch ex As Exception

                End Try
                Try
                    Concession += Val(IDreader("Concession"))
                Catch ex As Exception

                End Try

            End While
            IDreader.Close()
            Dim totalWord As String = GetNumberAsWords(FeeDepositAmount)
            Dim className As String = txtClass.Text & "-" & txtSec.Text
            Dim Sql As String = "Select ClassName,SecName,ASName,BusFeeID as FeeDepositID,CMNO,ASID,SID, BusTermName as Installment,locationName as FeeTypeName,sum(DepositeAmt) as FeeDepositAmount,Sum(ActualAmt) as FeeActualAmount,Sum(Concession) as ConcessionAmount,DepositeDate as DepositDate,PMname,SName,FName,RegNo From vw_BusFee Where BusFeeID =" & FeeDepositeID & " group by ASName,BusFeeID,CMNO,ASID,SID, BusTermName,DepositeDate,PMname,SName,FName,RegNo,ClassName,SecName,locationName"
            Dim ds As New DataSet
            ds = ExecuteQuery_DataSet(Sql, "tbl")
            Dim rds As ReportDataSource = New ReportDataSource()
            rds.Name = "dsReceipt" ' Change to what you will be using when creating an objectdatasource
            rds.Value = ds.Tables(0)
            With ReportViewer1   ' Name of the report control on the form
                .Reset()
                .ProcessingMode = ProcessingMode.Local
                .LocalReport.DataSources.Clear()
                .Visible = True
                .LocalReport.ReportPath = "Report/rptFeeReceipt.rdlc"
                .LocalReport.DataSources.Add(rds)
            End With

            Dim params(1) As Microsoft.Reporting.WebForms.ReportParameter
            params(0) = New Microsoft.Reporting.WebForms.ReportParameter("TotalWord", totalWord, Visible)
            ' params(1) = New Microsoft.Reporting.WebForms.ReportParameter("Installment", Installment, Visible)
            params(1) = New Microsoft.Reporting.WebForms.ReportParameter("SchoolName", FindSchoolDetails(1), Visible)

            Me.ReportViewer1.LocalReport.SetParameters(params)

            ReportViewer1.Visible = True
            ReportViewer1.LocalReport.Refresh()

        Next
        'Response.Redirect("FeeReceipt.aspx")
        ' Me.ReportViewer1.LocalReport.ReportPath = "rptFeeReceipt.rdlc"

        'Dim params(8) As Microsoft.Reporting.WebForms.ReportParameter
        'params(0) = New Microsoft.Reporting.WebForms.ReportParameter("StudentName", txtSName.Text, Visible)
        'params(1) = New Microsoft.Reporting.WebForms.ReportParameter("FatherName", txtFName.Text, Visible)
        'params(2) = New Microsoft.Reporting.WebForms.ReportParameter("Class", txtClass.Text & "-" & txtSec.Text, Visible)
        'params(3) = New Microsoft.Reporting.WebForms.ReportParameter("PaymentMode", cboMode.Text, Visible)
        'params(4) = New Microsoft.Reporting.WebForms.ReportParameter("Installment", lblTerm.Text, Visible)
        'params(5) = New Microsoft.Reporting.WebForms.ReportParameter("depositDate", txtDD.Text & "-" & txtMM.Text & "-" & txtYY.Text, Visible)
        'params(6) = New Microsoft.Reporting.WebForms.ReportParameter("total", totalWord, Visible)
        'params(7) = New Microsoft.Reporting.WebForms.ReportParameter("AdmissionNo", txtRegNo.Text, Visible)

        '  Me.ReportViewer1.LocalReport.SetParameters(params)
        'ReportViewer1.Visible = True
        'ReportViewer1.LocalReport.Refresh()
        'ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "OpenWindow", "window.open('FeeReceipt.aspx','_newtab');", True)

    End Sub

    Private Sub PreserveInfo()

        'Dim TempList As New CheckBoxList
        'Dim t As Integer = 0
        'For t = 0 To CheckBoxList1.Items.Count - 1
        '    TempList.Items.Add(CheckBoxList1.Items(t).Selected)
        'Next
        'Dim TempPaymentMode As String = cboMode.Text
        'Dim TempDD As String = txtDD.Text
        'Dim TempMM As String = txtMM.Text
        'Dim TempYY As String = txtYY.Text

        'InitControls()
        txtSearchName.Text = ""
        txtRegNo.Text = ""
        txtFeeBookNo.Text = ""
        txtSName.Text = ""
        txtClass.Text = ""
        txtFName.Text = ""
        txtSec.Text = ""
        lblActualAmt.Text = ""
        lblpickuppoint.Text = ""
        txtBusFine.Text = ""
        txtDepositeAmt.Text = ""
        txtFeeDepositID.Text = 0
        txtConcession.Text = ""
        chkMultipleEntry.Checked = False
        GridView1.SelectedIndex = -1
        txtFeeBookNo.Focus()
        'For t = 0 To CheckBoxList1.Items.Count - 1
        '    CheckBoxList1.Items(t).Selected = TempList.Items(t).Text
        'Next
        'ShowInTextBox()

        ''Try
        ''    LoadFeeTermCaption(TextBox1.Text)
        ''Catch ex As Exception
        ''    lblTerm.Text = ""
        ''End Try
        'cboMode.Text = TempPaymentMode
        'txtDD.Text = TempDD
        'txtMM.Text = TempMM
        'txtYY.Text = TempYY

    End Sub

    Protected Sub cboHistory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        ' chkTermList.SelectedIndex = GridView1.SelectedRow.Cells(2).Text
        'txtDepositeAmt.Text = GridView1.SelectedRow.Cells(3).Text
        'txtConcession.Text = GridView1.SelectedRow.Cells(4).Text
        'txtBusFine.Text = GridView1.SelectedRow.Cells(5).Text
        'Dim t As Integer = 0
        'For t = 0 To chkTermList.Items.Count - 1
        '    chkTermList.Items(t).Selected = False
        'Next
        'For t = 0 To chkTermList.Items.Count - 1
        '    If chkTermList.Items(t).Text = GridView1.SelectedRow.Cells(2).Text Then
        '        chkTermList.Items(t).Selected = True
        '    End If
        'Next
        txtFeeDepositID.Text = GridView1.DataKeys(GridView1.SelectedIndex).Value
        lblActualAmt.Text = GetDueDetail(FindMasterID(67, GridView1.SelectedRow.Cells(2).Text), Request.Cookies("ASID").Value)
        'txtFeeDepositID.Text = GetBusFeeID(txtSID.Text)
        If txtFeeDepositID.Text = "" Then
            btnSave.Enabled = False
            txtFeeDepositID.Focus()

        Else

            '''''''''''CHEQUE Status
            Dim sqlStr As String = ""

            sqlStr = "SELECT count(*) From vw_BusFee Where RegNo='" & txtRegNo.Text & "'"

            Dim tmp As Integer = 0
            tmp = ExecuteQuery_ExecuteScalar(sqlStr)
            ShowHistory(Val(txtFeeDepositID.Text))
            btnSave.Enabled = True
            lblStatus.Text = "You are in EDIT MODE"
            'myTable.Focus()
        End If

    End Sub

    Private Sub ShowHistory(busfeeid As Integer)

        Dim sqlStr As String = ""
        Dim myTxtBoxNumber As Integer = 1

        'Show Summary
        sqlStr = "Select * From vw_BusFee Where BusFeeID=" & busfeeid

        Dim SummaryReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While SummaryReader.Read
            Dim t As Integer = 0
            For t = 0 To chkTermList.Items.Count - 1
                If chkTermList.Items(t).Text = SummaryReader("BusTermNo") Then
                    chkTermList.Items(t).Selected = True
                Else
                    chkTermList.Items(t).Selected = False
                End If
            Next
            'ShowInTextBox()

            Dim DepositDate_History As Date = SummaryReader("DepositeDate")
            txtDepositDate.Text = DepositDate_History.Date
            Try
                txtBusFine.Text = SummaryReader("FineAmt")
            Catch ex As Exception

            End Try
            Try
                lblActualAmt.Text = SummaryReader("ActualAmt")
            Catch ex As Exception

            End Try
            txtDepositeAmt.Text = SummaryReader("DepositeAmt")
            txtConcession.Text = SummaryReader("Concession")
            cboMode.SelectedIndex = SummaryReader("DepositMode")
            txtModeRemark.Text = SummaryReader("DepositDetails")
        End While
        SummaryReader.Close()
        If cboMode.Text = "Cheque" Then
            'ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Please Enter Cheque No.');", True)
            txtChequeNo.Enabled = True
            txtChequeDate.Enabled = True
            txtChequeBank.Enabled = True
            sqlStr = "select ChequeNo,ChequeBank,ChequeDate from BusFeeCheque where BusFeeID=" & busfeeid

            Dim myReaderCheque As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReaderCheque.Read
                txtChequeNo.Text = myReaderCheque("ChequeNo")
                txtChequeBank.Text = myReaderCheque("ChequeBank")
                Dim dt As Date = myReaderCheque("ChequeDate")
                txtChequeDate.Text = dt.ToString("dd/MM/yyyy")
            End While
            'txtChequeNo.Focus()
            myReaderCheque.Close()
        Else
            txtChequeNo.Enabled = False
            txtChequeDate.Enabled = False
            txtChequeBank.Enabled = False
            Exit Sub
        End If
        Dim i As Integer = 0, myCount As Integer = 0
        'For i = 1 To myTable.Rows.Count - 1
        '    myCount = 0
        '    Dim myFeeTypeID As String = myTable.Rows(i).Cells(1).Text
        '    Dim DefaultFeeTotoal As Double = 0

        'sqlStr = "Select DepositAmt From vw_BusFee Where" & _
        '" FeeDepositID=" & Val(cboHistory.Text)
        '' & " AND FeeTypeName='" & myFeeTypeID & "'"

        'myCommand.CommandText = sqlStr
        'myCommand.Connection = myConn

        'Dim myReader1 As SqlDataReader = myCommand.ExecuteReader
        'While myReader1.Read
        '    Try

        '        'DefaultFeeTotoal += myReader1(0)
        '    Catch ex As Exception
        '        'CType(myTable.FindControl("txtD" & i), TextBox).Text = 0
        '    End Try
        '    myCount += 1
        'End While
        ''If myCount <= 0 Then CType(myTable.FindControl("txtD" & i), TextBox).Text = "0"
        'myReader1.Close()

        'Next

        'Display Fee Total
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "MyKey", "ShowTotal();", True)

        'myTable.EnableViewState = True
        'ViewState("myTable") = True

    End Sub

    'Protected Sub btnCompleteHistory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCompleteHistory.Click
    '    btnCompleteHistory.Enabled = False
    '    Response.Redirect("~/FeePaymentHistory.aspx?FeeBookNo=" & txtFeeBookNo.Text)
    'End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        Save_Log("FEE DELETE")
        Dim sqlStr As String = ""

        sqlStr = "Update BusFeeDeposite Set IsCancel=1 Where BusFeeID=" & txtFeeDepositID.Text
        ExecuteQuery_Update(sqlStr)
        InitControls()

        lblStatus.Text = "Record removed successfully..."
        txtFeeBookNo.Focus()
        GridView1.DataBind()
        GridView2.DataBind()
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        SqlDataSource1.SelectCommand = "SELECT RegNo, FeeBookNo, SName, ClassName, SecName, FName, MName, ASID FROM vw_Student WHERE ASID = " & Request.Cookies("ASID").Value & " AND SName Like '%" & txtSearchName.Text & "%'"
        gvSearch.DataBind()
        gvSearch.Visible = True
        If gvSearch.Rows.Count > 0 Then
            Panel1.Visible = True
        Else
            Panel1.Visible = False
        End If
    End Sub

    Protected Sub gvSearch_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvSearch.SelectedIndexChanged
        txtFeeBookNo.Text = gvSearch.SelectedRow.Cells(2).Text
        btnNext_Click(sender, e)
        gvSearch.Visible = False
        txtSearchName.Text = ""
        GridView1.Visible = True
        GridView2.Visible = True
    End Sub

    '---------Added For Multiple Entry----------
    Protected Sub CheckBoxList1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTermList.SelectedIndexChanged
        If txtFeeBookNo.Text = "" Then
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('First Select Fee Book No...');", True)
            txtFeeBookNo.Focus()
            Exit Sub
        End If
        'If cboHistory.Items.Count > 0 Then cboHistory.SelectedIndex = 0 'Reinit History Selection
        ShowInTextBox()
        lblActualAmt.Visible = True
    End Sub

    'Protected Function ReturnTermCount() As Integer
    '    Dim i As Integer = 0, myCount As Integer = 0
    '    For i = 0 To CheckBoxList1.Items.Count - 1
    '        If CheckBoxList1.Items(i).Selected Then myCount += 1
    '    Next
    '    Return myCount
    'End Function
    Private Function GetDepositedAmount(SID As String, TermID As Integer, type As Integer) As Double
        Dim sqlstr As String = ""
        If type = 1 Then
            sqlstr = " Select Sum(DepositeAmt) + Sum(Concession) as FD From vw_BusFee Where SID=" & SID & " and TermNo in (" & TermID & ") and IsCancel=0 Group By SID"
        Else
            sqlstr = " Select Sum(FineAmt) as FD From vw_BusFee Where SID=" & SID & " and TermNo in (" & TermID & ") and IsCancel=0 Group By SID"
        End If
        'sqlstr = " Select Sum(Abs(DepositeAmt))+ Sum(Abs(FineAmt))+ Sum(Abs(Concession)) as FD From vw_BusFee Where SID=" & SID & " and TermNo in (" & TermID & ") and IsCancel=0 Group By SID"
        Dim rv As Double = 0
        Try
            rv = ExecuteQuery_ExecuteScalar(sqlstr)
        Catch ex As Exception

        End Try
        Return rv
    End Function
    Private Sub ShowInTextBox()
        Dim TermCounter As Integer = 0
        Dim name As String = ""
        Dim Fine As Double = 0
        Dim ActualAmt As Double = 0
        Dim TermNo As Integer = 0
        For i = 0 To chkTermList.Items.Count - 1
            If chkTermList.Items(i).Selected Then
                name = name & chkTermList.Items(i).Text & ","
                TermCounter += 1
                TermNo = FindMasterID(67, chkTermList.Items(i).Text)

                Dim DepositedFee As Double = 0
                DepositedFee = GetDepositedAmount(txtSID.Text, TermNo, 1)

                Dim FineDeposited As Double = 0
                FineDeposited = GetDepositedAmount(txtSID.Text, TermNo, 2)

                Dim ActualFeetmp As Double = 0
                ActualFeetmp = GetBusActualAmt(txtSID.Text, TermNo)

                Dim Finetmp As Double = 0
                Finetmp = GetBusDueAmountForTerm(Request.Cookies("ASID").Value, TermNo, txtSID.Text)

                ActualAmt += ActualFeetmp - DepositedFee

                If DepositedFee < ActualFeetmp + Finetmp Then
                    Fine += Finetmp - FineDeposited
                End If
            End If
            'TextBox1.Text = name
        Next
        'Try
        '    TextBox1.Text = TextBox1.Text.Substring(0, TextBox1.Text.Length - 1)
        'Catch ex As Exception
        '    TextBox1.Text = ""
        'End Try

        lblStatus.Text = ""

        'txtDD.Text = Now.Day
        'txtMM.Text = Now.Month
        'txtYY.Text = Now.Year

        'If IsNumeric(TextBox1.Text) = False Then Exit Sub

        'Try
        '    lblTerm.Text = LoadFeeTermCaption(Val(Val(TextBox1.Text)), "BusFee")
        'Catch ex As Exception
        '    lblTerm.Text = "Multi-Term"
        'End Try

        'CreateTable()
        If CheckBusFeeDepositExistance(Request.Cookies("ASID").Value, txtSID.Text, TermNo) = False Then
            btnSave.Enabled = True
        Else
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Bus Fee Entry Already Exists for selected Term');", True)
            lblStatus.Text = "Fee Entry Already Exists for selected Term"
            '  btnSave.Enabled = False
        End If



        lblpickuppoint.Text = GetLocationName(txtSID.Text, TermNo)
        lblActualAmt.Text = ActualAmt
        txtDepositeAmt.Text = ActualAmt
        txtBusFine.Text = Fine
        txtConcession.Text = "0"
        chkTermList.Focus()


        '---------Added For Multiple Entry /End----------

    End Sub

    Protected Sub chkPast_CheckedChanged(sender As Object, e As EventArgs) Handles chkPast.CheckedChanged

    End Sub



    Protected Sub btnSave1_Click(sender As Object, e As EventArgs) Handles btnSlip.Click

        'If Trim(cboHistory.Text) = "" Then
        '    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('No data to print');", True)
        '    Exit Sub
        'End If
        GenFeeReceipt(Val(txtSID.Text))

    End Sub



    Protected Sub GridView2_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView2.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim TermID As String = GridView2.DataKeys(e.Row.RowIndex).Values(0).ToString()
                Try
                    Dim DueTime As String = GetDueTime(TermID, Request.Cookies("ASID").Value)
                    e.Row.Cells(2).Text = DueTime


                Catch ex As Exception

                End Try
                Try
                    Dim DueDetail As String = GetDueDetail(TermID, Request.Cookies("ASID").Value)
                    e.Row.Cells(3).Text = DueDetail
                Catch ex As Exception

                End Try
            End If
        End If
    End Sub
    Public Function GetDueDetail(TermID As Integer, ASID As Integer) As String
        Dim Sqlstr As String = ""
        Dim totalfees As String = 0
        Sqlstr = "Select Amount From BusStudentMap Where  TermNo='" & TermID & "' and busrequired=1 And SID=" & txtSID.Text
        Dim DueConfigReader As SqlDataReader = ExecuteQuery_ExecuteReader(Sqlstr)
        While DueConfigReader.Read
            totalfees = DueConfigReader(0).ToString()
        End While
        DueConfigReader.Close()
        Return totalfees
    End Function

    Public Function GetDueTime(TermID As Integer, ASID As Integer) As String
        Dim Sqlstr As String = ""
        Dim Lastdate As Date = Now.Date
        Sqlstr = "Select min(LastDate) From BusFeeDueConfig Where  TermNo='" & TermID & "' AND ASID=" & ASID
        Dim DueConfigReader As SqlDataReader = ExecuteQuery_ExecuteReader(Sqlstr)
        While DueConfigReader.Read
            Lastdate = DueConfigReader(0).ToString()
        End While
        DueConfigReader.Close()
        Return Lastdate.ToString("dd/MM/yyyy")
    End Function



    'Protected Sub chkMultipleEntry_CheckedChanged(sender As Object, e As EventArgs) Handles chkMultipleEntry.CheckedChanged
    '    cboHistory.SelectedIndex = 0

    'End Sub

    Protected Sub cboMode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboMode.SelectedIndexChanged
        If cboMode.Text = "Cheque" Then
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Please Enter Cheque No.');", True)
            txtChequeNo.Enabled = True
            txtChequeDate.Enabled = True
            txtChequeBank.Enabled = True
            txtChequeNo.Focus()
        Else
            txtChequeNo.Enabled = False
            txtChequeDate.Enabled = False
            txtChequeBank.Enabled = False
            Exit Sub
        End If

    End Sub
    Private Sub ChequeStatus()
        Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
        myConn.Open()

        Dim myCommand As New System.Data.SqlClient.SqlCommand
        Dim sqlStr As String = "Select "
        Dim myCount As Integer = 0
        myCommand.CommandText = sqlStr
        myCommand.Connection = myConn
    End Sub

    Private Sub Save_Log(ByVal type As String)

        Dim log1 As String = ""
        Dim sqlStr As String = ""
        If (type.Contains("INSERT") = True) Then
            If cboMode.Text = "Cash" Then
                log1 = "RegNo : " & txtRegNo.Text & ", Name : " & txtSName.Text & ", Bus Fee : " & txtDepositeAmt.Text & ", Payment Mode: Cash, Date : " & txtDepositDate.Text
            Else
                log1 = "RegNo : " & txtRegNo.Text & ", Name : " & txtSName.Text & ", Bus Fee : " & txtDepositeAmt.Text & ", Cheque NO : " & txtChequeNo.Text & ", Date : " & txtDepositDate.Text
            End If
        Else

            sqlStr = "Select RegNo,Sname,DepositeAmt,DepositMode,DepositeDate from vw_BusFee Where BusFeeID='" & txtFeeDepositID.Text & "'"
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                Dim tmpdate As Date = myReader(4)
                If IsDBNull(myReader(3)) = True Then
                    log1 = "RegNo : " & myReader(0) & ", Name : " & myReader(1) & ", Bus Fee : " & myReader(2) & ", Payment Mode: Cash, Date : " & tmpdate.ToString("dd/MM/yyyy")
                Else
                    log1 = "RegNo : " & myReader(0) & ", Name : " & myReader(1) & ", Bus Fee : " & myReader(2) & ", Cheque NO : " & myReader(3) & ", Date : " & tmpdate.ToString("dd/MM/yyyy")
                End If

            End While
            myReader.Close()
            If (type.Contains("UPDATE") = True) Then
                Dim Feetotal As String = txtDepositeAmt.Text
                log1 += " ####   Update Bus Fee : " & Feetotal & ", Date : " & txtDepositDate.Text
            End If
            If (type.Contains("DELETE") = True) Then
                log1 += " ####   Delete on Date : " & txtDepositDate.Text
            End If
        End If

        sqlStr = "Insert Into Event_log(logTime,EventType,Details,loginId,Visible) Values('" & System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "','" & type & "','" & log1 & "','" & Request.Cookies("UserID").Value & "','1')"
        ExecuteQuery_Update(sqlStr)

    End Sub

    Protected Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If txtFeeDepositID.Text <> "" Then
            Dim TempFeeBookNo As String = txtFeeBookNo.Text
            InitControls()
            txtFeeBookNo.Text = TempFeeBookNo
        End If
        lblStatus.Text = ""

        If ShowStudentDetails("1") > 0 Then

            'LoadPreviousPaymentHistory(cboHistory, Val(txtSID.Text), Request.Cookies("ASID").Value, "BusFee")
            'If cboHistory.Items.Count > 1 Then
            '    btnCompleteHistory.Enabled = True
            'Else
            '    btnCompleteHistory.Enabled = False
            'End If

            Dim TermNo As Integer = 0, i As Integer = 0
            For i = 0 To chkTermList.Items.Count - 1
                If chkTermList.Items(i).Selected = True Then
                    TermNo = chkTermList.Items(i).Text
                End If
            Next

            If TermNo > 0 Then
                'CreateTable()    'Old Code
                If CheckBusFeeDepositExistance(Request.Cookies("ASID").Value, txtSID.Text, TermNo) = True Then
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Bus Fee Already Exists for selected Term');", True)
                    lblStatus.Text = "Bus Fee Already Exists for selected Term."
                End If
            End If

            chkTermList.Focus()
        Else    'Student Info Not Found

            Dim TempFeeBookNo As String = txtFeeBookNo.Text
            InitControls()
            txtFeeBookNo.Text = TempFeeBookNo
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Invalid Reg. No');", True)
        End If
        'lblActualAmt.Text = GetBusLocationAmt(Request.Cookies("ASID").Value, txtSID.Text)
        'txtDepositeAmt.Text = lblActualAmt.Text.Split("-")(1).ToString

    End Sub

    Protected Sub btnregsearch_Click(sender As Object, e As EventArgs) Handles btnregsearch.Click

        lblStatus.Text = ""

        If ShowStudentDetails("2") > 0 Then

            'LoadPreviousPaymentHistory(cboHistory, Val(txtSID.Text), Request.Cookies("ASID").Value, "BusFee")
            'If cboHistory.Items.Count > 1 Then
            '    btnCompleteHistory.Enabled = True
            'Else
            '    btnCompleteHistory.Enabled = False
            'End If

            Dim TermNo As Integer = 0, i As Integer = 0
            For i = 0 To chkTermList.Items.Count - 1
                If chkTermList.Items(i).Selected = True Then
                    TermNo = chkTermList.Items(i).Text
                End If
            Next

            If TermNo > 0 Then
                'CreateTable()    'Old Code
                If CheckBusFeeDepositExistance(Request.Cookies("ASID").Value, txtSID.Text, TermNo) = True Then
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Bus Fee Already Exists for selected Term');", True)
                    lblStatus.Text = "Bus Fee Already Exists for selected Term."
                End If
            End If

            chkTermList.Focus()
        Else    'Student Info Not Found

            Dim TempFeeBookNo As String = txtFeeBookNo.Text
            InitControls()
            txtFeeBookNo.Text = TempFeeBookNo
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Invalid Reg. No');", True)
        End If
        'lblActualAmt.Text = GetBusLocationAmt(Request.Cookies("ASID").Value, txtSID.Text)
        'txtDepositeAmt.Text = lblActualAmt.Text.Split("-")(1).ToString

    End Sub
End Class