Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary_Fee.CLS_iDiary_Fee
Imports System.IO
Imports Microsoft.Reporting.WebForms
Imports System.Drawing

Public Class FeeDeposit
    Inherits System.Web.UI.Page
    Private Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
    End Sub
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        Try
            If Request.Cookies("UType").Value.ToString.Contains("Fee") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
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

    Private Function ShowStudentDetails(type As Integer) As Integer
        txtSID.Text = ""
        btnPrint.Visible = False
        'btnExcel.Visible = False
        Dim AdmissionDate As Date = Now.Date

        Dim sqlStr As String = ""
        If type = 1 Then
            sqlStr = "Select SID, RegNo,FeeBookNo, SName, FName, ClassName, SecName,FeeGroupID,AdmissionDate,FeeGroupName,FeeConfigType,ClassGroupID,SchoolName From vw_Student Where RegNo='" & txtRegNo.Text & "' AND ASID=" & Request.Cookies("ASID").Value
        Else
            sqlStr = "Select SID, RegNo,FeeBookNo, SName, FName, ClassName, SecName,FeeGroupID,AdmissionDate,FeeGroupName,FeeConfigType,ClassGroupID,SchoolName From vw_Student Where FeeBookNo='" & txtFeeBookNo.Text & "' AND ASID=" & Request.Cookies("ASID").Value
        End If
        Dim myCount As Integer = 0
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            txtSID.Text = myReader("SID")
            txtRegNo.Text = myReader("RegNo")
            txtFeeBookNo.Text = myReader("FeeBookNo")
            txtSName.Text = myReader("SName")
            txtFName.Text = myReader("FName")
            txtClass.Text = myReader("ClassName")
            Try
                txtSchooName.Text = myReader("SchoolName")
            Catch ex As Exception

            End Try
            Try
                txtClassGroup.Text = myReader("ClassGroupID")
            Catch ex As Exception

            End Try

            txtSec.Text = myReader("SecName")
            txtFeeGroupID.Text = myReader("FeeGroupID")
            lblFeeGroupName.Text = "Fee Group: " & myReader("FeeGroupName")
            Try
                AdmissionDate = myReader("AdmissionDate")
            Catch ex As Exception

            End Try
            Try
                txtConfigType.Text = myReader("FeeConfigType")
            Catch ex As Exception
                txtConfigType.Text = "0"
            End Try
            txtAdmissionDate.Text = AdmissionDate.ToString("yyyy/MM/dd")
            myCount += 1
        End While
        myReader.Close()
        If txtSID.Text = "" Then Return 0

        Dim TermID() As String = LoadFeeTerms(chkTermList, Val(txtFeeGroupID.Text)).Split(",")
        cboTermID.Items.Clear()
        For i = 0 To TermID.Count - 1
            'cboTermID.Items.Add(TermID(i))
            Try
                cboTermID.Items.Add(New ListItem(TermID(i), GetMonthID(txtFeeGroupID.Text, TermID(i))))
            Catch ex As Exception

            End Try
        Next


        If txtSID.Text <> "" Then
            If AdmissionFeeApplicable(txtSID.Text, Request.Cookies("ASID").Value) = False Then
                txtAdminFeeApplicable.Text = 0
            Else
                txtAdminFeeApplicable.Text = 1
            End If
            'If CheckStudentConfig(txtSID.Text) = True Then
            '    txtConfigType.Text = "1"
            'Else
            '    txtConfigType.Text = "0"
            'End If
            'btnPrint.Visible = True
            'btnExcel.Visible = True
        End If
        chkAllTerm.Visible = True
        FillHistoryGrid()
        FillFeeConfig()
        Return myCount
    End Function
    Private Function ShowChallanDetails(ChallanID As Integer) As Integer
        Dim sqlStr As String = ""
        sqlStr = "Select * from vw_FeeChallanIndivisual where FeeChallanAmount>0 and FeeChallanID = '" & ChallanID & "'"
        Dim myCount As Integer = 0
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        Dim TermName As String = ""
        While myReader.Read
            txtSID.Text = myReader("SID")
            txtRegNo.Text = myReader("RegNo")
            txtFeeBookNo.Text = myReader("FeeBookNo")
            txtSName.Text = myReader("SName")
            txtFName.Text = myReader("FName")
            txtClass.Text = myReader("ClassName")
            txtSec.Text = myReader("SecName")
            txtFeeGroupID.Text = myReader("FeeGroupID")
            LoadFeeTerms(chkTermList, Val(txtFeeGroupID.Text))
            TermName = myReader("TermNo")
            myCount += 1
        End While
        For i = 0 To chkTermList.Items.Count - 1
            If chkTermList.Items(i).Text = TermName Then
                chkTermList.Items(i).Selected = True
            Else
                chkTermList.Items(i).Selected = False
            End If
        Next
        myReader.Close()
        txtDepositDate.Focus()
        Return myCount
    End Function

    Protected Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click

        If txtFeeDepositID.Text <> "" Then
            Dim TempFeeBookNo As String = txtFeeBookNo.Text
            InitControls()
            txtFeeBookNo.Text = TempFeeBookNo
        End If
        lblStatus.Text = ""
        Dim type As Integer = 0
        If sender.ID = "btnNextRegNo" Then
            type = 1
        End If
        If ShowStudentDetails(type) > 0 Then
            Dim TermNo As Integer = 0, i As Integer = 0
            For i = 0 To chkTermList.Items.Count - 1
                If chkTermList.Items(i).Selected = True Then
                    TermNo = chkTermList.Items(i).Text
                End If
            Next
            If TermNo > 0 Then
                GvCreateTable()
                If CheckFeeDepositExistance(Request.Cookies("ASID").Value, txtFeeBookNo.Text, TermNo) = True Then
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Fee Entry Already Exists for selected Term');", True)
                    lblStatus.Text = "Fee Entry Already Exists for selected Term."
                End If
            End If

            chkTermList.Focus()
        Else    'Student Info Not Found

            Dim TempFeeBookNo As String = txtFeeBookNo.Text
            InitControls()
            txtFeeBookNo.Text = TempFeeBookNo
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Invalid Fee Book No');", True)
        End If
        ReportViewer1.Visible = False

    End Sub
    Private Sub FillHistoryGrid()
        SqlDataSource2.SelectCommand = "SELECT [FeeDepositID],[DepositDate], Sum([FeeDepositAmount]) as DepositAmount,sum(ConcessionAmount) as ConcessionAmount from vw_FeeDeposit where SID=" & Val(txtSID.Text) & " group by [FeeDepositID],[DepositDate] order by [DepositDate] DESC"
        GridView1.Visible = True
        GridView1.DataBind()
        If GridView1.Rows.Count > 0 Then
            lblDepositAmount.Visible = True
        Else
            lblDepositAmount.Visible = False
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cookies("ActiveTab").Value = 3
        Response.Cookies("ActiveTab").Expires = DateTime.Now.AddHours(1)
        Session("ActiveTab") = 3
        If IsPostBack = False Then
            InitControls()
            Dim FeeTypes() As String = GetFeeTypeConfigID().Split("$")
            txtAdmissionFeeID.Text = FeeTypes(0)
            txtLateFeeID.Text = FeeTypes(1)
            txtConveyanceFeeID.Text = FeeTypes(2)
            txtTutionFeeID.Text = FeeTypes(3)
            txtArrearFeeID.Text = FeeTypes(4)
            txtExcessFeeID.Text = FeeTypes(5)
        Else

            'For Grid View Printing. Must have a blank HTM Page (gview.htm)
            'Dim printScript As String = "function PrintGridView() { var gridInsideDiv = document.getElementById('gvDiv');" & _
            '" var printWindow = window.open('gview.htm','PrintWindow','letf=0,top=0,width=150,height=300,toolbar=1,scrollbars=1,status=1');" & _
            '" printWindow.document.write(gridInsideDiv.innerHTML);printWindow.document.close();printWindow.focus();" & _
            '" printWindow.print();printWindow.close();}"
            'Me.ClientScript.RegisterStartupScript(Page.[GetType](), "PrintGridView", printScript.ToString(), True)
            'btnPrint.Attributes.Add("onclick", "PrintGridView();")
        End If


        If Request.Cookies("UType").Value.ToString.Contains("Admin-1") = False And Request.Cookies("UType").Value.ToString.Contains("Fee-1") = False Then
            btnSave.Enabled = False
            btnRemove.Enabled = False
        End If
    End Sub

    Private Sub InitControls()
        txtChequeBank.Text = ""
        txtChequeDate.Text = Now.Date.ToString("dd/MM/yyyy")
        txtDepositDate.Text = Now.Date.ToString("dd/MM/yyyy")
        txtChequeNo.Text = ""
        txtFeeDepositID.Text = ""
        txtFeeBookNo.Text = ""
        txtRegNo.Text = ""
        txtSName.Text = ""
        txtSName.Text = ""
        'txtFeeGroupID.Text = ""
        'txtSID.Text = ""
        txtFName.Text = ""
        txtClass.Text = ""
        txtSchooName.Text = ""
        txtSec.Text = ""
        LoadMasterInfo(72, cboBank)
        cboBank.Text = FindDefault(72)
        Dim BankID As Integer = FindMasterID(72, cboBank.Text)
        LoadFeeBankBranch(BankID, cboBranch)
        Try
            cboBranch.Text = GetDefaultFeeBankBranch(BankID)
        Catch ex As Exception

        End Try
        'cboBranch.Items.Clear()
        LoadMasterInfo(12, cboMode)
        Try
            cboMode.Text = FindDefault(12)
        Catch ex As Exception

        End Try
        'If cboMode.Text = "Cheque" Then
        '    txtChequeNo.Enabled = True
        '    txtChequeDate.Enabled = True
        '    txtChequeBank.Enabled = True
        'Else
        '    txtChequeNo.Enabled = False
        '    txtChequeDate.Enabled = False
        '    txtChequeBank.Enabled = False
        'End If
        CheckPaymentMode()
        txtModeRemark.Text = ""
        'btnCompleteHistory.Enabled = False
        lblStatus.Text = ""
        txtChallanNo.Text = ""
        GridView1.SelectedIndex = -1
        GridView1.Visible = False
        GridView2.Visible = False
        lblConfiguredAmount.Visible = False
        lblDepositAmount.Visible = False
        chkTermList.Items.Clear()
        chkAllTerm.Visible = False
        btnSlip.Visible = False
        btnRemove.Visible = False
        chkMultipleEntry.Checked = False
        chkMultipleEntry.Enabled = True
        chkTermList.Enabled = True
        'myTable.BackColor = Drawing.Color.White
        GvMyTable.BackColor = Drawing.Color.White
        If ReportViewer1.Visible = False Then
            btnprint.Visible = False
        Else
            btnprint.Visible = True
        End If

        'btnExcel.Visible = False
        GvMyTable.Visible = False
        btnSave.Visible = False
        chkMultipleEntry.Visible = False
        chkFeeRcpt.Visible = False
        btnSlip.Visible = False
        chkFeeRcpt.Checked = True
        lblFeeGroupName.Text = ""
        txtClassGroup.Text = ""
        txtFeeBookNo.Focus()
    End Sub

     Protected Sub chkAllTerm_CheckedChanged(sender As Object, e As EventArgs) Handles chkAllTerm.CheckedChanged
        For i = 0 To chkTermList.Items.Count - 1
            If chkAllTerm.Checked = True Then
                chkTermList.Items(i).Selected = True
            Else
                chkTermList.Items(i).Selected = False
            End If
        Next
        ShowInTextBox()

        '        CheckGvMyTable()
    End Sub

    Private Sub GvCreateTable()

        Dim sqlstr As String = ""
        Dim rv As String = ""
        If txtChallanNo.Text <> "" Then
            rv = CheckChallanIDExistance(txtChallanNo.Text)
        End If

        If txtChallanNo.Text = "" Or rv <> "" Then

            'Retrieve Concession Fee Type Config(Given Additionally During Fee Deposit)
            If txtFeeDepositID.Text = "" Then
                Dim i As Integer = 0, j As Integer = 0, myCount As Integer = 0
                Dim TermCounter As Integer = 0
                Dim TermList As String = ""
                Dim MonthID As String = ""
                For t = 0 To chkTermList.Items.Count - 1
                    If chkTermList.Items(t).Selected = True Then
                        MonthID += cboTermID.Items(t).Value & ","
                        TermList += cboTermID.Items(t).Text & ","
                        TermCounter += 1
                    End If
                Next

                If MonthID <> "" Then
                    MonthID = MonthID.Substring(0, MonthID.Length - 1)
                End If
                If TermList.Contains(",") = True Then
                    TermList = TermList.Substring(0, TermList.Length - 1)
                End If

                For Each gvr As GridViewRow In GvMyTable.Rows
                    Dim myFeeTypeID As String = GvMyTable.DataKeys(gvr.RowIndex).Value.ToString()
                    'myTable.Rows(i).Cells(0).Text   'Get FeeTypeID From Table
                    If myFeeTypeID = txtLateFeeID.Text Then
                        Continue For
                    End If
                    Dim myFeeAmount As Double = 0
                    Dim ConcessionAmount As Double = 0

                    Dim FeeAmounttmp As Double = 0
                    If myFeeTypeID = txtAdmissionFeeID.Text And Val(txtAdminFeeApplicable.Text) = 0 Then 'Check Addmission Fee Applicable
                        myFeeAmount = 0
                        ConcessionAmount = 0
                    ElseIf myFeeTypeID = txtArrearFeeID.Text Then
                        myFeeAmount = GetArrearAmount()
                        ConcessionAmount = 0
                    ElseIf myFeeTypeID = txtExcessFeeID.Text Then
                        myFeeAmount = 0
                        ConcessionAmount = 0
                    Else
                        If MonthID <> "" Then
                            If txtConfigType.Text = "1" Then
                                FeeAmounttmp = GetFeeConfigForFeeHead(Request.Cookies("ASID").Value, 0, myFeeTypeID, MonthID, "", Val(txtSID.Text))
                            Else
                                FeeAmounttmp = GetFeeConfigForFeeHead(Request.Cookies("ASID").Value, Val(txtFeeGroupID.Text), myFeeTypeID, MonthID)
                            End If
                            myFeeAmount += FeeAmounttmp
                            ConcessionAmount += GetConcessionAmount(txtSID.Text, myFeeTypeID, MonthID)
                        End If
                    End If
                    Dim chkSelect As CheckBox = DirectCast(gvr.FindControl("chkSelect"), CheckBox)
                    Dim txtActualConcession As TextBox = DirectCast(gvr.FindControl("txtActualConcession"), TextBox)
                    Dim txtDepositAmount As TextBox = DirectCast(gvr.FindControl("txtDepositAmount"), TextBox)
                    Dim lblActualAmount As Label = DirectCast(gvr.FindControl("lblActualAmount"), Label)

                    lblActualAmount.Text = myFeeAmount
                    myFeeAmount = myFeeAmount - ConcessionAmount
                    txtDepositAmount.Text = myFeeAmount
                    txtActualConcession.Text = ConcessionAmount
                    If Val(lblActualAmount.Text) > 0 Then
                        chkSelect.Checked = True
                    Else
                        chkSelect.Checked = False
                    End If
                Next

                For Each gvr As GridViewRow In GvMyTable.Rows
                    Dim myFeeTypeID As String = GvMyTable.DataKeys(gvr.RowIndex).Value.ToString()
                    If myFeeTypeID = txtLateFeeID.Text Then
                        Dim chkSelect As CheckBox = DirectCast(gvr.FindControl("chkSelect"), CheckBox)

                        Dim txtActualConcession As TextBox = DirectCast(gvr.FindControl("txtActualConcession"), TextBox)
                        Dim txtDepositAmount As TextBox = DirectCast(gvr.FindControl("txtDepositAmount"), TextBox)
                        Dim lblActualAmount As Label = DirectCast(gvr.FindControl("lblActualAmount"), Label)
                        If TermList = "" Then
                            chkSelect.Checked = False
                            lblActualAmount.Text = 0
                            txtDepositAmount.Text = 0
                            txtActualConcession.Text = 0
                        Else
                            Dim DeposteDate As String = Now.Date.ToString("yyyy/MM/dd")
                            Try
                                DeposteDate = txtDepositDate.Text.Split("/")(2) & "/" & txtDepositDate.Text.Split("/")(1) & "/" & txtDepositDate.Text.Split("/")(0)
                            Catch ex As Exception

                            End Try

                            lblActualAmount.Text = GetLateFeeAmount(Val(txtSID.Text), Val(txtFeeGroupID.Text), txtAdmissionDate.Text, TermList, txtAdmissionFeeID.Text, txtLateFeeID.Text, Request.Cookies("ASID").Value, Val(txtConfigType.Text), DeposteDate)
                            txtDepositAmount.Text = lblActualAmount.Text
                            txtActualConcession.Text = 0
                            If Val(lblActualAmount.Text) > 0 Then
                                chkSelect.Checked = True
                            Else
                                chkSelect.Checked = False
                            End If
                        End If

                    End If
                Next
                CheckGvMyTable()

            End If
            btnSave.Visible = True
            chkFeeRcpt.Visible = True
            chkMultipleEntry.Visible = True
        Else
            'Dim LateFeeExist As Integer = 0
            'Dim ConcessionFeeExist As Integer = 0
            'sqlStr = "Select * from vw_FeeChallanIndivisual where FeeChallanAmount>0 and FeeChallanID ='" & txtChallanNo.Text & "'"
            'Dim myCount As Integer = 0

            'Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            ''Dim FeeGroupID As Integer = 0
            'Dim TermName As String = ""
            'While myReader.Read
            '    Dim trx As New TableRow

            '    Dim tdx0 As New TableCell
            '    tdx0.Text = myReader("FeeTypeID")
            '    If myReader("FeeTypeID") = txtLateFeeID.Text Then
            '        LateFeeExist = 1
            '    End If
            '    'If myReader("FeeTypeID") = ConcessionFeeTypeID Then
            '    '    ConcessionFeeExist = 1
            '    'End If

            '    tdx0.HorizontalAlign = HorizontalAlign.Center
            '    trx.Cells.Add(tdx0)

            '    Dim tdx1 As New TableCell
            '    tdx1.Text = myReader("FeeTypeName")
            '    tdx1.HorizontalAlign = HorizontalAlign.Center
            '    trx.Cells.Add(tdx1)

            '    Dim txtAAmount As New Label()
            '    txtAAmount.ID = "txtA" & myTxtBoxNumber
            '    txtAAmount.Width = 100
            '    Dim tdx2 As New TableCell
            '    txtAAmount.Text = myReader("FeeChallanAmount")
            '    tdx2.Controls.Add(txtAAmount)
            '    tdx2.HorizontalAlign = HorizontalAlign.Center
            '    trx.Cells.Add(tdx2)

            '    Dim txtAmount As New TextBox()
            '    txtAmount.ID = "txtD" & myTxtBoxNumber
            '    txtAmount.Width = 100
            '    txtAmount.CssClass = "textbox"
            '    txtAmount.Attributes.Add("onchange", "javascript: ShowTotal();")
            '    Dim tdx3 As New TableCell
            '    txtAmount.Text = myReader("FeeChallanAmount")
            '    tdx3.Controls.Add(txtAmount)
            '    tdx3.HorizontalAlign = HorizontalAlign.Center
            '    trx.Cells.Add(tdx3)
            '    myTable.Rows.Add(trx)
            '    myTxtBoxNumber += 1
            '    myCount += 1
            'End While
            'myReader.Close()
            'If LateFeeExist = 0 Then
            '    Dim trx As New TableRow

            '    Dim tdx0 As New TableCell
            '    tdx0.Text = txtLateFeeID.Text
            '    tdx0.HorizontalAlign = HorizontalAlign.Center
            '    trx.Cells.Add(tdx0)

            '    Dim tdx1 As New TableCell
            '    tdx1.Text = GetFeeTypeName(txtLateFeeID.Text)
            '    tdx1.HorizontalAlign = HorizontalAlign.Center
            '    trx.Cells.Add(tdx1)

            '    Dim txtAAmount As New Label()
            '    txtAAmount.ID = "txtA" & myTxtBoxNumber
            '    txtAAmount.Width = 100
            '    Dim tdx2 As New TableCell
            '    txtAAmount.Text = 0
            '    tdx2.Controls.Add(txtAAmount)
            '    tdx2.HorizontalAlign = HorizontalAlign.Center
            '    trx.Cells.Add(tdx2)

            '    Dim txtAmount As New TextBox()
            '    txtAmount.ID = "txtD" & myTxtBoxNumber
            '    txtAmount.Width = 100
            '    txtAmount.CssClass = "textbox"
            '    txtAmount.Attributes.Add("onchange", "javascript: ShowTotal();")
            '    Dim tdx3 As New TableCell
            '    txtAmount.Text = 0
            '    tdx3.Controls.Add(txtAmount)
            '    tdx3.HorizontalAlign = HorizontalAlign.Center
            '    trx.Cells.Add(tdx3)
            '    myTable.Rows.Add(trx)
            '    myTxtBoxNumber += 1
            '    myCount += 1
            'End If
            'If ConcessionFeeExist = 0 Then
            '    Dim trx As New TableRow
            '    Dim txtAAmount As New Label()
            '    txtAAmount.ID = "txtA" & myTxtBoxNumber
            '    txtAAmount.Width = 100
            '    Dim tdx2 As New TableCell
            '    txtAAmount.Text = 0
            '    tdx2.Controls.Add(txtAAmount)
            '    tdx2.HorizontalAlign = HorizontalAlign.Center
            '    trx.Cells.Add(tdx2)

            '    Dim txtAmount As New TextBox()
            '    txtAmount.ID = "txtD" & myTxtBoxNumber
            '    txtAmount.Width = 100
            '    txtAmount.Attributes.Add("onchange", "javascript: ShowTotal();")
            '    Dim tdx3 As New TableCell
            '    txtAmount.Text = 0
            '    tdx3.Controls.Add(txtAmount)
            '    tdx3.HorizontalAlign = HorizontalAlign.Center
            '    trx.Cells.Add(tdx3)
            '    myTable.Rows.Add(trx)
            '    myTxtBoxNumber += 1
            '    myCount += 1
            'End If
        End If

        'Display Fee Total
        'ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "MyKey", "ShowTotal();", True)
        'myTable.EnableViewState = True
        'ViewState("myTable") = True

    End Sub
    Protected Sub chkSelect_CheckedChanged(sender As Object, e As EventArgs)
        CheckGvMyTable()
    End Sub
    Private Sub CheckGvMyTable()
        Dim TotalActualFee As Double = 0
        Dim ToalConcession As Double = 0
        Dim TotalDeposit As Double = 0
        For Each gvr As GridViewRow In GvMyTable.Rows
            Dim chkSelect As CheckBox = DirectCast(gvr.FindControl("chkSelect"), CheckBox)
            Dim txtActualConcession As TextBox = DirectCast(gvr.FindControl("txtActualConcession"), TextBox)
            Dim txtDepositeAmount As TextBox = DirectCast(gvr.FindControl("txtDepositAmount"), TextBox)
            Dim lblActualAmount As Label = DirectCast(gvr.FindControl("lblActualAmount"), Label)
            If chkSelect.Checked = True Then
                txtDepositeAmount.Enabled = True
                txtActualConcession.Enabled = True
            Else
                txtDepositeAmount.Enabled = False
                txtActualConcession.Enabled = False
            End If

            If chkSelect.Checked = True Then
                TotalActualFee += Val(lblActualAmount.Text)
                ToalConcession += Val(txtActualConcession.Text)
                TotalDeposit += Val(txtDepositeAmount.Text)
            End If
        Next
        GvMyTable.FooterRow.Cells(1).Text = " Total :"
        GvMyTable.FooterRow.Cells(2).Text = TotalActualFee
        GvMyTable.FooterRow.Cells(3).Text = TotalDeposit
        GvMyTable.FooterRow.Cells(4).Text = ToalConcession
    End Sub
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim i As Integer = 0
        If txtFeeBookNo.Text.Length <= 0 Then
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Invalid Fee Book No');", True)
            txtFeeBookNo.Focus()
            Exit Sub
        End If
        If cboBank.Text = "" Then
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Please Select Bank');", True)
            cboBank.Focus()
            Exit Sub
        End If
        If cboBranch.Text = "" Then
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Please Select Branch');", True)
            cboBranch.Focus()
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

        Dim TermList As String = ""
        For k = 0 To chkTermList.Items.Count - 1
            If chkTermList.Items(k).Selected = True Then
                TermList += cboTermID.Items(k).Text & ","
            End If
        Next
        If TermList = "" Then
            lblStatus.Text = "Please Select atleast one Term"
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Please Select atleast one Term');", True)
            Exit Sub
        End If
        TermList = TermList.Substring(0, TermList.Length - 1)


        If cboMode.SelectedIndex = 0 Then
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Invalid Payment Mode');", True)
            cboMode.Focus()
            Exit Sub
        End If
        If cboMode.Text = "Cheque" Or cboMode.Text = "DD" Or cboMode.Text = "RTGS" Or cboMode.Text = "NEFT" Then
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
        Dim mycount As Integer = 0
        For Each gvr As GridViewRow In GvMyTable.Rows
            Dim chkSelect As CheckBox = DirectCast(gvr.FindControl("chkSelect"), CheckBox)
            If chkSelect.Checked = True Then
                mycount += 1
            End If
        Next
        If mycount = 0 Or GvMyTable.Visible = False Then
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Please Select at least one Fee Type to deposit');", True)
            'txtChequeBank.Focus()
            Exit Sub
        End If

        'For i = 1 To myTable.Rows.Count - 1
        '    If CType(myTable.FindControl("txtD" & i), TextBox).Text.Length <= 0 Then
        '        CType(myTable.FindControl("txtD" & i), TextBox).Text = "0"
        '    End If
        '    If CType(myTable.FindControl("txtC" & i), TextBox).Text.Length <= 0 Then
        '        CType(myTable.FindControl("txtC" & i), TextBox).Text = "0"
        '    End If
        'Next

        Dim PaymentModeID As Integer = FindMasterID(12, cboMode.Text)
        Dim FeeBankID As Integer = FindMasterID(72, cboBank.Text)
        Dim FeeBranchID As Integer = FindMasterID(73, cboBranch.Text)
        Dim FeeDepositID As Integer = 0
        Dim t As Integer = 0
        'Dim TallyCompanyName As String = FindDefault(59)


        Dim sqlStr As String = ""
        Dim CMNO As Integer = GetCMNO(Request.Cookies("ASID").Value, DeposteDate, Val(txtClassGroup.Text))
        Dim RCPTNO As String = CMNO & "/" & Request.Cookies("ASName").Value.Substring(2, 2)

        If txtFeeDepositID.Text = "" And CheckFeeDepositExistance(Request.Cookies("ASID").Value, txtFeeBookNo.Text, TermList) = True Then  'Deny
            lblStatus.Text = "Fee Entry for Selected Term already exist..."
        ElseIf txtFeeDepositID.Text = "" And CheckFeeDepositExistance(Request.Cookies("ASID").Value, txtFeeBookNo.Text, TermList) = False Then  'Deny
            sqlStr = "Insert into FeeDeposit(CMNO,RCPTNO,ClassGroupID, ASID, SID, DepositDate, DepositMode, DepositDetails, isDeposit, BranchID,IsFeeDeleted,UserID,EntryDate,FeeBankID,FeeBranchID) Values(" & _
          CMNO & ",'" & RCPTNO & "','" & Val(txtClassGroup.Text) & "'," & Request.Cookies("ASID").Value & "," & Val(txtSID.Text) & "," & _
        "'" & DeposteDate & "'," & PaymentModeID & "," & "'" & txtModeRemark.Text & "',1,'',0," & Request.Cookies("UserID").Value & ",'" & DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") & "','" & FeeBankID & "','" & FeeBranchID & "')"
        ElseIf Val(txtFeeDepositID.Text) > 0 Then 'Update
            'DeleteTallyPush(Val(cboHistory.Text), TallyCompanyName)
            sqlStr = "Update FeeDeposit Set " & _
            "DepositDate='" & DeposteDate & "'," & "DepositMode=" & PaymentModeID & "," & "FeeBankID=" & FeeBankID & "," & "FeeBranchID=" & FeeBranchID & "," & "DepositDetails='" & txtModeRemark.Text & "' Where FeeDepositID=" & Val(txtFeeDepositID.Text)
        End If
        If chkMultipleEntry.Checked = True And txtFeeDepositID.Text = "" Then
            'Dim CMNO As Integer = GetCMNO(Request.Cookies("ASID").Value, DeposteDate, Val(txtClassGroup.Text))
            'Dim RCPTNO As String = CMNO & "/" & Request.Cookies("ASName").Value.Substring(2, Request.Cookies("ASName").Value.Length - 1)
            sqlStr = "Insert into FeeDeposit(CMNO,RCPTNO,ClassGroupID, ASID, SID, DepositDate, DepositMode, DepositDetails, isDeposit, BranchID,IsFeeDeleted,UserID,EntryDate,FeeBankID,FeeBranchID) Values(" & _
                  CMNO & ",'" & RCPTNO & "','" & Val(txtClassGroup.Text) & "'," & Request.Cookies("ASID").Value & "," & Val(txtSID.Text) & "," & _
                  "'" & DeposteDate & "'," & PaymentModeID & "," & "'" & txtModeRemark.Text & "',1,'',0," & Request.Cookies("UserID").Value & ",'" & DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") & "','" & FeeBankID & "','" & FeeBranchID & "')"
        End If
        If sqlStr = "" Then
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Fee Entry for Selected Term already exist...');", True)
            lblStatus.Text = "Fee Entry for Selected Term already exist..."
            Exit Sub
        Else
            If txtFeeDepositID.Text = "" Then
                Save_Log("FEE INSERT")
            Else
                Save_Log("FEE UPDATE")
            End If
            ExecuteQuery_Update(sqlStr)
        End If
        lblStatus.Text = ""
        If txtFeeDepositID.Text = "" Then
            sqlStr = "Select MAX(FeeDepositID) from FeeDeposit where RCPTNO='" & RCPTNO & "'"
            FeeDepositID = ExecuteQuery_ExecuteScalar(sqlStr)
        Else
            FeeDepositID = Val(txtFeeDepositID.Text)
        End If
        sqlStr = "Delete From FeeDepositDetails Where FeeDepositID=" & FeeDepositID
        ExecuteQuery_Update(sqlStr)

        Dim TermID As Integer = 0
        For t = 0 To chkTermList.Items.Count - 1
            If chkTermList.Items(t).Selected = True Then
                TermID = cboTermID.Items(t).Text
                Dim LateFeeAmount As Double = 0 'GetDueAmountForTerm(Request.Cookies("ASID").Value, CheckBoxList1.Items(t).Text, txtFeeBookNo.Text)

                For Each gvr As GridViewRow In GvMyTable.Rows
                    Dim myFeeTypeID As String = GvMyTable.DataKeys(gvr.RowIndex).Value.ToString()

                    Dim chkSelect As CheckBox = DirectCast(gvr.FindControl("chkSelect"), CheckBox)
                    If chkSelect.Checked = True Then


                        Dim txtActualConcession As TextBox = DirectCast(gvr.FindControl("txtActualConcession"), TextBox)
                        Dim txtDepositAmount As TextBox = DirectCast(gvr.FindControl("txtDepositAmount"), TextBox)
                        Dim lblActualAmount As Label = DirectCast(gvr.FindControl("lblActualAmount"), Label)

                        Dim OriginalFeeAmount As Double = Val(txtDepositAmount.Text)
                        Dim OriginalConcessionAmount As Double = Val(txtActualConcession.Text)

                        Dim myFeeAmount As Double = OriginalFeeAmount
                        Dim myConcessionAmount As Double = OriginalConcessionAmount

                        Dim FeeActualAmount As Double = 0
                        Dim ConcessionActualAmount As Double = 0

                        Dim MonthID As String = cboTermID.Items(t).Value

                        If myFeeTypeID = txtAdmissionFeeID.Text Then
                            If txtConfigType.Text = "1" Then
                                FeeActualAmount = GetFeeConfigForFeeHead(Request.Cookies("ASID").Value, 0, myFeeTypeID, MonthID, "", Val(txtSID.Text))
                            Else
                                If Val(txtAdminFeeApplicable.Text) = 1 Then
                                    FeeActualAmount = GetFeeConfigForFeeHead(Request.Cookies("ASID").Value, Val(txtFeeGroupID.Text), myFeeTypeID, MonthID)
                                End If
                            End If
                        ElseIf myFeeTypeID = txtLateFeeID.Text Then
                            FeeActualAmount = GetLateFeeAmount(Val(txtSID.Text), Val(txtFeeGroupID.Text), txtAdmissionDate.Text, TermID, txtAdmissionFeeID.Text, txtLateFeeID.Text, Request.Cookies("ASID").Value, Val(txtConfigType.Text))
                        Else
                            If txtConfigType.Text = "1" Then
                                FeeActualAmount = GetFeeConfigForFeeHead(Request.Cookies("ASID").Value, 0, myFeeTypeID, MonthID, "", Val(txtSID.Text))
                            Else
                                FeeActualAmount = GetFeeConfigForFeeHead(Request.Cookies("ASID").Value, Val(txtFeeGroupID.Text), myFeeTypeID, MonthID)
                            End If

                        End If

                        If FeeActualAmount > 0 Then
                            ConcessionActualAmount = GetConcessionAmount(txtSID.Text, myFeeTypeID, MonthID)
                        End If

                        If OriginalFeeAmount >= FeeActualAmount - ConcessionActualAmount Then
                            txtDepositAmount.Text = ConcessionActualAmount + OriginalFeeAmount - FeeActualAmount
                            OriginalFeeAmount = FeeActualAmount - ConcessionActualAmount
                        Else
                            txtDepositAmount.Text = 0
                        End If
                        If OriginalConcessionAmount >= ConcessionActualAmount Then
                            txtActualConcession.Text = OriginalConcessionAmount - ConcessionActualAmount
                            OriginalConcessionAmount = ConcessionActualAmount
                        Else
                            txtActualConcession.Text = 0
                        End If
                        sqlStr = "Insert into FeeDepositDetails(FeedEpositDetailsID,FeeDepositID,FeeTypeID,FeeDepositAmount,FeeActualAmount,ConcessionAmount,ConcessionActualAmount,IsPushed,TermID) Values(" & _
        "'" & FeeDepositID & "-" & myFeeTypeID & "-" & TermID & "'," & _
        FeeDepositID & "," & _
                        myFeeTypeID & "," & _
                        OriginalFeeAmount & "," & _
                        FeeActualAmount & "," & _
                        OriginalConcessionAmount & "," & _
                        ConcessionActualAmount & _
                        ",0," & TermID & ")"

                        If OriginalFeeAmount <> 0 Or OriginalConcessionAmount <> 0 Then
                            ExecuteQuery_Update(sqlStr)
                        End If
                    End If
                Next

            End If
        Next



        'If Amount is left after Alll term config then add multi entry
        For Each gvr As GridViewRow In GvMyTable.Rows
            Dim myFeeTypeID As String = GvMyTable.DataKeys(gvr.RowIndex).Value.ToString()

            Dim chkSelect As CheckBox = DirectCast(gvr.FindControl("chkSelect"), CheckBox)
            If chkSelect.Checked = True Then
                Dim txtActualConcession As TextBox = DirectCast(gvr.FindControl("txtActualConcession"), TextBox)
                Dim txtDepositAmount As TextBox = DirectCast(gvr.FindControl("txtDepositAmount"), TextBox)
                Dim lblActualAmount As Label = DirectCast(gvr.FindControl("lblActualAmount"), Label)

                Dim OriginalFeeAmount As Double = Val(txtDepositAmount.Text)
                Dim OriginalConcessionAmount As Double = Val(txtActualConcession.Text)
                If OriginalFeeAmount > 0 Or OriginalConcessionAmount > 0 Then
                    sqlStr = "Insert into FeeDepositDetails(FeedEpositDetailsID,FeeDepositID,FeeTypeID,FeeDepositAmount,FeeActualAmount,ConcessionAmount,ConcessionActualAmount,IsPushed,TermID,ExtraEntry) Values(" & _
    "'" & FeeDepositID & "-" & myFeeTypeID & "-" & TermID & "-E" & "'," & _
    FeeDepositID & "," & _
                    myFeeTypeID & "," & _
                    OriginalFeeAmount & "," & _
                    0 & "," & _
                    OriginalConcessionAmount & "," & _
                    0 & _
                    ",0," & TermID & ",1)"
                    ExecuteQuery_Update(sqlStr)
                End If
            End If
        Next

        '''''''''''''''''''''Update Challan Details
        If txtChallanNo.Text <> "" Then
            sqlStr = "Update FeeChallan Set isDeposit=1 where FeeChallanID=" & txtChallanNo.Text
            ExecuteQuery_Update(sqlStr)
        End If

        '''''''''''''''''''''Entry for Cheque Only
        If cboMode.Text = "Cheque" Or cboMode.Text = "DD" Or cboMode.Text = "RTGS" Or cboMode.Text = "NEFT" Then
            sqlStr = "Update FeeDeposit" & _
                                " set ChequeNo='" & txtChequeNo.Text & "'," & _
                    "ChequeBank='" & txtChequeBank.Text & "'," & _
                    "ChequeDate='" & txtChequeDate.Text.Substring(6, 4) & "/" & txtChequeDate.Text.Substring(3, 2) & "/" & txtChequeDate.Text.Substring(0, 2) & "'," & _
                    "FeeBankID='" & FeeBankID & "'," & _
                    "FeeBranchID='" & FeeBranchID & "'," & _
"isDishonoured=0 where FeeDepositID=" & FeeDepositID
            ExecuteQuery_Update(sqlStr)
        End If


        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Fee saved successfully...');", True)

        If chkFeeRcpt.Checked = True Then
            GenFeeReceipt(FeeDepositID)
        End If
        Dim tmpDate As String = txtDepositDate.Text
        Dim tmpPaymentMode As String = cboMode.Text
        Dim tmpBank As String = cboBank.Text
        Dim tmpBranch As String = cboBranch.Text
        InitControls()
        txtDepositDate.Text = tmpDate
        cboMode.Text = tmpPaymentMode
        CheckPaymentMode()
        txtFeeBookNo.Focus()

        cboBank.Text = tmpBank
        LoadFeeBankBranch(FeeBankID, cboBranch)
        Try
            cboBranch.Text = tmpBranch
        Catch ex As Exception

        End Try

    End Sub
    Protected Function TallyPush1(LedgerName As String, Amt As Double, TallyID As String, DepositeDate As String, StudentDetails As String, CompanyName As String) As Boolean
        Dim req As Net.WebRequest = Nothing
        Dim rsp As Net.WebResponse = Nothing
        Dim fileName As String = "<ENVELOPE><HEADER><TALLYREQUEST>Import Data</TALLYREQUEST></HEADER><BODY><IMPORTDATA><REQUESTDESC><REPORTNAME>All Masters</REPORTNAME><STATICVARIABLES><SVCURRENTCOMPANY>" & CompanyName & "</SVCURRENTCOMPANY></STATICVARIABLES></REQUESTDESC><REQUESTDATA><TALLYMESSAGE xmlns:UDF=" & ControlChars.Quote & "TallyUDF" & ControlChars.Quote & "><VOUCHER REMOTEID=" & ControlChars.Quote & TallyID & ControlChars.Quote & " VCHTYPE=" & ControlChars.Quote & "Receipt" & ControlChars.Quote & "ACTION=" & ControlChars.Quote & "Create" & ControlChars.Quote & "> <ISOPTIONAL>No</ISOPTIONAL><USEFORGAINLOSS>No</USEFORGAINLOSS><USEFORCOMPOUND>No</USEFORCOMPOUND><VOUCHERTYPENAME></VOUCHERTYPENAME><DATE>" & DepositeDate & "</DATE><EFFECTIVEDATE>" & DepositeDate & "</EFFECTIVEDATE><ISCANCELLED>No</ISCANCELLED><USETRACKINGNUMBER>No</USETRACKINGNUMBER><ISPOSTDATED>No</ISPOSTDATED><ISINVOICE>No</ISINVOICE><VOUCHERNUMBER>" & TallyID & "</VOUCHERNUMBER><DIFFACTUALQTY>No</DIFFACTUALQTY><NARRATION/><ASPAYSLIP>No</ASPAYSLIP><GUID>" & TallyID & "</GUID><NARRATION>" & StudentDetails & "</NARRATION><REFERENCE>1</REFERENCE><PARTYLEDGERNAME>" & LedgerName & "</PARTYLEDGERNAME><LEDGERENTRIES.LIST><REMOVEZEROENTRIES>No</REMOVEZEROENTRIES><ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE><LEDGERFROMITEM>No</LEDGERFROMITEM><TAXCLASSIFICATIONNAME/><LEDGERNAME>" & LedgerName & "</LEDGERNAME><AMOUNT>" & Amt & "</AMOUNT></LEDGERENTRIES.LIST><LEDGERENTRIES.LIST><REMOVEZEROENTRIES>No</REMOVEZEROENTRIES><ISDEEMEDPOSITIVE>Yes</ISDEEMEDPOSITIVE><LEDGERFROMITEM>No</LEDGERFROMITEM><TAXCLASSIFICATIONNAME/><LEDGERNAME>Cash</LEDGERNAME><AMOUNT>-" & Amt & "</AMOUNT></LEDGERENTRIES.LIST></VOUCHER></TALLYMESSAGE></REQUESTDATA></IMPORTDATA></BODY></ENVELOPE>"

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
        Return True
        'Label1.Text="Successfully Inserted";
    End Function
    Protected Function DeleteTallyPush1(FeeDipositeID As Integer, CompanyName As String) As Boolean
        Dim sqlStr As String = ""
        Dim flag As Boolean = False
        sqlStr = "Select FeedEpositDetailsID, DepositDate From vw_FeeDeposit where FeeDepositID=" & FeeDipositeID

        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        ' myLst.Items.Clear()
        While myReader.Read
            Dim TallyID As String = "IDR" & myReader(0)
            Dim DepositeDate As String = ""
            Dim tempDepositeDate As Date = myReader(1)
            Dim dd As String = tempDepositeDate.Day.ToString("00")
            Dim mm As String = tempDepositeDate.Month.ToString("00")
            Dim yy As String = tempDepositeDate.Year.ToString("0000")
            DepositeDate = yy & mm & dd
            'DepositeDate = tempDepositeDate.ToString("yyyymmdd")
            Dim req As Net.WebRequest = Nothing
            Dim rsp As Net.WebResponse = Nothing
            Dim fileName As String = "<ENVELOPE><HEADER><TALLYREQUEST>Import Data</TALLYREQUEST></HEADER><BODY><IMPORTDATA><REQUESTDESC><REPORTNAME>All Masters</REPORTNAME><STATICVARIABLES><SVCURRENTCOMPANY>" & CompanyName & "</SVCURRENTCOMPANY></STATICVARIABLES></REQUESTDESC><REQUESTDATA><TALLYMESSAGE xmlns:UDF=" & ControlChars.Quote & "TallyUDF" & ControlChars.Quote & "><VOUCHER REMOTEID=" & ControlChars.Quote & TallyID & ControlChars.Quote & " VCHTYPE=" & ControlChars.Quote & "Receipt" & ControlChars.Quote & "ACTION=" & ControlChars.Quote & "Delete" & ControlChars.Quote & "> <VOUCHERTYPENAME>Receipt</VOUCHERTYPENAME><DATE>" & DepositeDate & "</DATE><GUID>" & TallyID & "</GUID></VOUCHER></TALLYMESSAGE></REQUESTDATA></IMPORTDATA></BODY></ENVELOPE>"

            Dim uri As String = "http://localhost:9002"
            req = Net.WebRequest.Create(uri)
            ' req.Proxy = WebProxy.GetDefaultProxy(); // Enable if using proxy
            req.Method = "POST"
            ' Post method
            req.ContentType = "text/xml"
            ' content type
            req.Headers.Add("Authorization", "Basic reallylongstring")
            Try
                Dim writer As New StreamWriter(req.GetRequestStream())
                ' Write the XML text into the stream
                writer.WriteLine(fileName)
                writer.Close()
                ' Send the data to the webserver
                rsp = req.GetResponse()
                flag = True
            Catch ex As Exception
                flag = False

            End Try
        End While
        myReader.Close()
        Return flag
        'Label1.Text="Successfully Inserted";
    End Function
    Private Function GetTextFromXMLFile(fileName As String) As String

        Dim reader As New StreamReader(fileName)
        Dim ret As String = reader.ReadToEnd()
        'Label1.Text = ret
        reader.Close()
        Return ret
    End Function
    Private Sub GenFeeReceipt(FeeDepositeID As Integer)
        'btnprint.Visible = True
        Dim sqlStr As String = ""
        Dim Installment As String = ""
        Dim TermNo As String = ""
        Dim SchoolID = 0
      
        'If txtFeeDepositID.Text = "" Then
        Dim TermNo1 = "("
        sqlStr = "Select distinct TermName,TermNo From vw_FeeDeposit where FeeDepositID = " & FeeDepositeID & " order by TermNo"
        Dim IDreader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While IDreader.Read
            Installment += IDreader(0) & ","
            TermNo1 &= IDreader(1) & ","
        End While
        IDreader.Close()
        Installment = Installment.Substring(0, Installment.Length - 1)
        TermNo1 = TermNo1.Substring(0, TermNo1.Length - 1)
        TermNo1 &= ")"
        Dim Installmenttmp() As String = Installment.Split("-")
        Try
            Installment = Installmenttmp(0).ToString & "-" & Installmenttmp(Installmenttmp.Count - 1).ToString
        Catch ex As Exception

        End Try
        sqlStr = "Select Distinct SchoolID From vw_FeeDeposit where FeeDepositID = " & FeeDepositeID & ""
        IDreader = ExecuteQuery_ExecuteReader(sqlStr)
        While IDreader.Read
            SchoolID = IDreader(0)
        End While
        IDreader.Close()

        ''................................................vikash..........17/06/2016.........................................
        'sqlStr = ""
        'sqlstr = "Select * from Params"
        Dim SchoolName As String = ""
        Dim Address As String = ""
        'Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)

        'While myReader.Read

        SchoolName = FindSchoolDetails1(SchoolID, 0)
        Val(txtClassGroup.Text)
        Address = FindSchoolDetails1(SchoolID, 1)
        'End While
        'myReader.Close()
        '.........................................................................................................................

        sqlStr = "Select Sum(FeeDepositAmount) From vw_FeeDeposit where FeeDepositID = " & FeeDepositeID
        Dim treader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        Dim total As Integer = 0
        While treader.Read
            total = Val(treader(0))
        End While
        treader.Close()

        Dim totalWord As String = GetNumberAsWords(total)
        Dim className As String = txtClass.Text & "-" & txtSec.Text
        Dim lr As New LocalReport()
        Dim Sql As String = "Select ClassName,SecName,ASName,FeeDepositID,ClassGroupID,RCPTNO,CMNO,ASID,SID, FeeTypeName,sum(FeeDepositAmount) as FeeDepositAmount,Sum(FeeActualAmount) as FeeActualAmount,Sum(ConcessionAmount) as ConcessionAmount,DepositDate,PMname,SName,FName,RegNo,PMName,ChequeNo From vw_FeeDeposit Where FeeDepositID =" & FeeDepositeID & " group by ASName,FeeDepositID,ClassGroupID,RCPTNO,CMNO,ASID,SID, FeeTypeName,DepositDate,PMname,SName,FName,RegNo,ClassName,SecName,PMName,ChequeNo"
        Dim ds As New DataSet
        ds = ExecuteQuery_DataSet(Sql, "tbl")
        Dim rd As New ReportDataSource("dsReceipt", ds.Tables(0))
        lr.DataSources.Add(rd)
        If Val(txtClassGroup.Text) = "2" Then
            lr.ReportPath = "Report/rptFeeReceiptKG.rdlc"
        Else
            lr.ReportPath = "Report/rptFeeReceiptKG.rdlc"
        End If
        'lr.ReportPath = ReportViewer1.LocalReport.ReportPath
        Dim params(3) As Microsoft.Reporting.WebForms.ReportParameter
        params(0) = New Microsoft.Reporting.WebForms.ReportParameter("TotalWord", totalWord, Visible)
        params(1) = New Microsoft.Reporting.WebForms.ReportParameter("Installment", Installment & TermNo1, Visible)
        params(2) = New Microsoft.Reporting.WebForms.ReportParameter("SchoolName", SchoolName, Visible)
        params(3) = New Microsoft.Reporting.WebForms.ReportParameter("SchoolAddress", Address, Visible)

        lr.SetParameters(params)
        'Dim reportType As String = id
        Dim mimeType As String
        Dim encoding As String
        Dim fileNameExtension As String
        Dim deviceInfo As String = (Convert.ToString("<DeviceInfo>" + "  <OutputFormat>") & "pdf") + "</OutputFormat>" + "  <PageWidth>8.27in</PageWidth>" + "  <PageHeight>4.0685in</PageHeight>" + "  <MarginTop>0.2in</MarginTop>" + "  <MarginLeft>.5in</MarginLeft>" + "  <MarginRight>.2in</MarginRight>" + "  <MarginBottom>0.2in</MarginBottom>" + "</DeviceInfo>"
        'Dim deviceInfo As String = (Convert.ToString("<DeviceInfo>" + "  <OutputFormat>") & "pdf") + "</OutputFormat>" + "  <PageWidth>8.27in</PageWidth>" + "  <PageHeight>11.67in</PageHeight>" + "  <MarginTop>0.2in</MarginTop>" + "  <MarginLeft>.5in</MarginLeft>" + "  <MarginRight>0in</MarginRight>" + "  <MarginBottom>0.2in</MarginBottom>" + "</DeviceInfo>"
        'If Val(txtClassGroup.Text) = "2" Then
        '    deviceInfo = (Convert.ToString("<DeviceInfo>" + "  <OutputFormat>") & "pdf") + "</OutputFormat>" + "  <PageWidth>10.03937in</PageWidth>" + "  <PageHeight>6in</PageHeight>" + "  <MarginTop>0in</MarginTop>" + "  <MarginLeft>0in</MarginLeft>" + "  <MarginRight>0in</MarginRight>" + "  <MarginBottom>0in</MarginBottom>" + "</DeviceInfo>"
        'Else
        deviceInfo = (Convert.ToString("<DeviceInfo>" + "  <OutputFormat>") & "pdf") + "</OutputFormat>" + "  <PageWidth>8.27in</PageWidth>" + "  <PageHeight>11.67in</PageHeight>" + "  <MarginTop>0.1in</MarginTop>" + "  <MarginLeft>0.1in</MarginLeft>" + "  <MarginRight>0in</MarginRight>" + "  <MarginBottom>0.1in</MarginBottom>" + "</DeviceInfo>"
        'End If

        Dim warnings As Warning()
        Dim streams As String()
        Dim renderedBytes As Byte()

        renderedBytes = lr.Render("pdf", deviceInfo, mimeType, encoding, fileNameExtension, streams, warnings)
        Using fs As FileStream = System.IO.File.Create(Server.MapPath("~") & "rcpt.pdf")
            fs.Write(renderedBytes, 0, renderedBytes.Length)
        End Using
        Dim url As String = "rcpt.pdf"
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "OpenWindow", "window.open('" & url & "','_newtab');", True)
        
    End Sub
    Private Sub FillEditDetail()
        Dim sqlStr As String = ""
        sqlStr = "SELECT count(*) From FeeDeposit Where FeeDepositID='" & txtFeeDepositID.Text & "' AND isDishonoured=1"
        Dim tmp As Integer = 0
        tmp = ExecuteQuery_ExecuteScalar(sqlStr)
        If tmp > 0 Then
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('This Cheque is disHonoured...');", True)
            Exit Sub
        End If
        ShowHistory()
        btnSave.Enabled = True
        lblStatus.Text = "You are in EDIT MODE"
        GvMyTable.Focus()
    End Sub
    Private Sub ShowHistory()

        Dim sqlStr As String = ""
        Dim myTxtBoxNumber As Integer = 1

        For t = 0 To chkTermList.Items.Count - 1
            chkTermList.Items(t).Selected = False
        Next
        Dim FeeBankName As String = ""
        Dim FeeBankID As Integer = 0
        Dim FeeBankBranchName As String = ""
        'Show Summary
        sqlStr = "Select * From vw_FeeDeposit Where FeeDepositID=" & Val(txtFeeDepositID.Text)
        Dim SummaryReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While SummaryReader.Read
            Dim t As Integer = 0
            For t = 0 To chkTermList.Items.Count - 1
                If chkTermList.Items(t).Text = SummaryReader("TermNo") Then
                    chkTermList.Items(t).Selected = True
                End If
            Next

            Dim DepositDate_History As Date = SummaryReader("DepositDate")
            txtDepositDate.Text = DepositDate_History.ToString("dd/MM/yyyy")
            cboMode.Text = SummaryReader("PMName")
            txtModeRemark.Text = SummaryReader("DepositDetails")
            FeeBankID = SummaryReader("FeeBankID")
            FeeBankName = SummaryReader("FeeBankName")
            FeeBankBranchName = SummaryReader("FeeBankBranchName")
            If cboMode.Text = "Cheque" Or cboMode.Text = "DD" Or cboMode.Text = "RTGS" Or cboMode.Text = "NEFT" Then
                txtChequeNo.Enabled = True
                txtChequeDate.Enabled = True
                txtChequeBank.Enabled = True
                Try
                    txtChequeNo.Text = SummaryReader("ChequeNo")
                Catch ex As Exception

                End Try
                Try
                    txtChequeBank.Text = SummaryReader("ChequeBank")
                Catch ex As Exception

                End Try
                Try
                    Dim dt As Date = SummaryReader("ChequeDate")
                    txtChequeDate.Text = dt.ToString("dd/MM/yyyy")
                Catch ex As Exception

                End Try
            Else
                txtChequeNo.Enabled = False
                txtChequeDate.Enabled = False
                txtChequeBank.Enabled = False
            End If
        End While
        SummaryReader.Close()
        If FeeBankName <> "" Then
            cboBank.Text = FeeBankName
            LoadFeeBankBranch(FeeBankID, cboBranch)
            cboBranch.Text = FeeBankBranchName
        End If

        Dim TermList As String = ""
        Dim MonthID As String = ""
        For t = 0 To chkTermList.Items.Count - 1
            If chkTermList.Items(t).Selected = True Then
                'MonthID += GetMonthID(Val(txtFeeGroupID.Text), cboTermID.Items(t).Text) & ","
                MonthID += cboTermID.Items(t).Value & ","
                TermList += cboTermID.Items(t).Text & ","
            End If
        Next
        If MonthID <> "" Then
            MonthID = MonthID.Substring(0, MonthID.Length - 1)
        End If
        If TermList.Contains(",") = True Then
            TermList = TermList.Substring(0, TermList.Length - 1)
        End If


        GvMyTable.Visible = True
        For Each gvr As GridViewRow In GvMyTable.Rows
            Dim myFeeTypeID As String = GvMyTable.DataKeys(gvr.RowIndex).Value.ToString()

            Dim chkSelect As CheckBox = DirectCast(gvr.FindControl("chkSelect"), CheckBox)
            Dim txtActualConcession As TextBox = DirectCast(gvr.FindControl("txtActualConcession"), TextBox)
            Dim txtDepositAmount As TextBox = DirectCast(gvr.FindControl("txtDepositAmount"), TextBox)
            Dim lblActualAmount As Label = DirectCast(gvr.FindControl("lblActualAmount"), Label)

            Dim myActualFeeAmount As Double = 0
            If myFeeTypeID = txtAdmissionFeeID.Text Then
                If txtConfigType.Text = "1" Then
                    myActualFeeAmount += GetFeeConfigForFeeHead(Request.Cookies("ASID").Value, 0, myFeeTypeID, MonthID, "", Val(txtSID.Text))
                Else
                    If Val(txtAdminFeeApplicable.Text) = 0 Then
                        myActualFeeAmount = 0
                    End If
                End If

            ElseIf myFeeTypeID = txtLateFeeID.Text Then
                myActualFeeAmount = GetLateFeeAmount(Val(txtSID.Text), Val(txtFeeGroupID.Text), txtAdmissionDate.Text, TermList, txtAdmissionFeeID.Text, txtLateFeeID.Text, Request.Cookies("ASID").Value, Val(txtConfigType.Text))
            Else
                If txtConfigType.Text = "1" Then
                    myActualFeeAmount += GetFeeConfigForFeeHead(Request.Cookies("ASID").Value, 0, myFeeTypeID, MonthID, "", Val(txtSID.Text))
                Else
                    myActualFeeAmount += GetFeeConfigForFeeHead(Request.Cookies("ASID").Value, Val(txtFeeGroupID.Text), myFeeTypeID, MonthID)
                End If
            End If
            lblActualAmount.Text = myActualFeeAmount
        Next

        sqlStr = "Select sum(FeeDepositAmount) as FeeDepositAmount,sum(ConcessionAmount) as ConcessionAmount,FeeTypeID From vw_FeeDeposit Where" & _
            " FeeDepositID=" & Val(txtFeeDepositID.Text) & " group by FeeTypeID order by FeeTypeID"
        ' & " AND FeeTypeID='" & myFeeTypeID & "'"

        Dim myReader1 As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader1.Read
            For Each gvr As GridViewRow In GvMyTable.Rows
                Dim myFeeTypeID As String = GvMyTable.DataKeys(gvr.RowIndex).Value.ToString()
                If myFeeTypeID = myReader1("FeeTypeID") Then
                    Dim chkSelect As CheckBox = DirectCast(gvr.FindControl("chkSelect"), CheckBox)
                    Dim txtActualConcession As TextBox = DirectCast(gvr.FindControl("txtActualConcession"), TextBox)
                    Dim txtDepositAmount As TextBox = DirectCast(gvr.FindControl("txtDepositAmount"), TextBox)
                    Dim lblActualAmount As Label = DirectCast(gvr.FindControl("lblActualAmount"), Label)
                    Try
                        txtDepositAmount.Text = myReader1(0)
                    Catch ex As Exception
                        txtDepositAmount.Text = 0
                    End Try
                    Try
                        txtActualConcession.Text = myReader1(1)
                    Catch ex As Exception
                        txtActualConcession.Text = 0
                    End Try
                    If Val(txtDepositAmount.Text) <> 0 Or Val(txtActualConcession.Text) <> 0 Then
                        chkSelect.Checked = True
                    Else
                        chkSelect.Checked = False
                    End If
                    Exit For

                End If
            Next
        End While
        myReader1.Close()
        CheckGvMyTable()


        'Display Fee Total
        'ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "MyKey", "ShowTotal();", True)

    End Sub

    Protected Sub btnCompleteHistory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCompleteHistory.Click
        btnCompleteHistory.Enabled = False
        Response.Redirect("~/FeePaymentHistory.aspx?FeeBookNo=" & txtFeeBookNo.Text)
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        If txtFeeDepositID.Text = "" Then
            lblStatus.Text = "Please select a record to remove fro Deposite Details..."
            Exit Sub
        End If
        'Dim TallyCompanyName As String = FindDefault(59)
        'DeleteTallyPush(Val(cboHistory.Text), TallyCompanyName)
        Save_Log("FEE DELETE")

        Dim sqlStr As String = ""

        sqlStr = "Update FeeDeposit set IsFeeDeleted=1 Where FeeDepositID=" & Val(txtFeeDepositID.Text)
        ExecuteQuery_Update(sqlStr)

        Dim tmpDate As String = txtDepositDate.Text
        Dim tmpPaymentMode As String = cboMode.Text
        InitControls()
        txtDepositDate.Text = tmpDate
        cboMode.Text = tmpPaymentMode

        lblStatus.Text = "Record removed successfully..."
        ReportViewer1.Visible = False
        txtFeeBookNo.Focus()
    End Sub


    Protected Sub gvSearch_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvSearch.SelectedIndexChanged
        txtFeeBookNo.Text = gvSearch.SelectedRow.Cells(2).Text
        btnNext_Click(sender, e)
        gvSearch.Visible = False
    End Sub

    '---------Added For Multiple Entry----------
    Protected Sub CheckBoxList1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTermList.SelectedIndexChanged
        If txtFeeBookNo.Text = "" Then
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('First Select Fee Book No...');", True)
            txtFeeBookNo.Focus()
            Exit Sub
        End If
        If txtFeeDepositID.Text = "" Then

        End If
        'Try
        '    ShowInTextBox(chkTermList.SelectedItem.Text)
        'Catch ex As Exception
        '    ShowInTextBox()
        'End Try
        ShowInTextBox()

    End Sub
    Private Sub FillFeeConfig()
        SqlDataSource3.SelectCommand = "SELECT TermID,[TermNo],TermName FROM TermMaster where FeeGroupID=" & Val(txtFeeGroupID.Text) & " order by DisplayOrder"
        GridView2.DataBind()
        GridView2.Visible = True
        If GridView2.Rows.Count > 0 Then
            If txtConfigType.Text = "1" Then
                lblConfiguredAmount.Text = "Configuration: Student Wise"
            Else
                lblConfiguredAmount.Text = "Configuration: Fee Group Wise"
            End If
            lblConfiguredAmount.Visible = True
        Else
            lblConfiguredAmount.Visible = False
        End If
    End Sub
    Protected Function ReturnTermCount() As Integer
        Dim i As Integer = 0, myCount As Integer = 0
        For i = 0 To chkTermList.Items.Count - 1
            If chkTermList.Items(i).Selected Then myCount += 1
        Next
        Return myCount
    End Function

    Private Sub ShowInTextBox()

        Dim chkterm As String = ""
        For i = 0 To chkTermList.Items.Count - 1
            If chkTermList.Items(i).Selected Then
                chkterm += LoadTermID(chkTermList.Items(i).Text, Val(txtFeeGroupID.Text)) & ","
            End If
        Next
        Try
            chkterm = chkterm.Substring(0, chkterm.Length - 1)
        Catch ex As Exception

        End Try
        If chkterm = "" Then
            GvMyTable.Visible = False
        Else
            GvMyTable.Visible = True
        End If
        lblStatus.Text = ""
        GvCreateTable()
        If CheckFeeDepositExistance(Request.Cookies("ASID").Value, txtFeeBookNo.Text, chkterm) = False Then
            btnSave.Enabled = True
        Else
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Fee Entry Already Exists for selected Term');", True)
            lblStatus.Text = "Fee Entry Already Exists for selected Term"
            '  btnSave.Enabled = False
        End If

        chkTermList.Focus()
        '---------Added For Multiple Entry /End----------

    End Sub
    Protected Sub btnSave1_Click(sender As Object, e As EventArgs) Handles btnSlip.Click

        If Trim(txtFeeDepositID.Text) = "" Then
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('No data to print');", True)
            Exit Sub
        End If
        GenFeeReceipt(Val(txtFeeDepositID.Text))
        'btnprint.Visible = True
    End Sub

    Protected Sub cboMode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboMode.SelectedIndexChanged
        CheckPaymentMode()
    End Sub
    Private Sub CheckPaymentMode()
        If cboMode.Text = "Cheque" Or cboMode.Text = "DD" Or cboMode.Text = "RTGS" Or cboMode.Text = "NEFT" Then
            'ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Please Enter Cheque No.');", True)
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
    Private Sub Save_Log(ByVal type As String)
        Dim log1 As String = ""
        Dim sqlStr As String = ""
        If (type.Contains("INSERT") = True) Then
            If cboMode.Text = "Cash" Then
                log1 = "RegNo : " & txtRegNo.Text & ", Name : " & txtSName.Text & ", Fee Sum : " & Request.Form("txtTotal") & ", Payment Mode: Cash, Date : " & txtDepositDate.Text
            Else
                log1 = "RegNo : " & txtRegNo.Text & ", Name : " & txtSName.Text & ", Fee Sum : " & Request.Form("txtTotal") & ", Cheque NO : " & txtChequeNo.Text & ", Date : " & txtDepositDate.Text
            End If
        Else
            sqlStr = "Select RegNo,Sname,Sum(feedepositamount),ChequeNo,DepositDate from vw_FeeDeposit Where FeeDepositID='" & Val(txtFeeDepositID.Text) & "' group by regno,SName,ChequeNo,DepositDate "

            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)

            While myReader.Read
                Dim tmpdate As Date = myReader(4)
                If IsDBNull(myReader(3)) = True Then
                    log1 = "RegNo : " & myReader(0) & ", Name : " & myReader(1) & ", Fee Sum : " & myReader(2) & ", Payment Mode: Cash, Date : " & tmpdate.ToString("dd/MM/yyyy")
                Else
                    log1 = "RegNo : " & myReader(0) & ", Name : " & myReader(1) & ", Fee Sum : " & myReader(2) & ", Cheque NO : " & myReader(3) & ", Date : " & tmpdate.ToString("dd/MM/yyyy")
                End If

            End While
            myReader.Close()
            If (type.Contains("UPDATE") = True) Then
                Dim Feetotal As String = Request.Form("txtTotal")
                log1 += " ####   Update Fee Total : " & Feetotal & ", Date : " & txtDepositDate.Text
            End If
            If (type.Contains("DELETE") = True) Then
                log1 += " ####   Delete on Date : " & txtDepositDate.Text
            End If
        End If

        sqlStr = "Insert Into Event_log(logTime,EventType,Details,UserId,Visible) Values('" & System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "','" & type & "','" & log1 & "','" & Request.Cookies("UserID").Value & "','1')"
        ExecuteQuery_Update(sqlStr)
    End Sub

    Protected Sub btnNextChallan_Click(sender As Object, e As EventArgs) Handles btnNextChallan.Click
        If Trim(txtChallanNo.Text) = "" Then
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Please Enter Challan ID');", True)
            txtChallanNo.Focus()
            Exit Sub
        End If

        Dim rv As String = CheckChallanIDExistance(txtChallanNo.Text)
        If rv <> "" Then
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('" & rv & "');", True)
            txtChallanNo.Focus()
            'myTable.EnableViewState = True
            'ViewState("myTable") = True
            Exit Sub
        End If
        If txtChallanNo.Text <> "" Then
            ShowChallanDetails(txtChallanNo.Text)
        End If
    End Sub

    Protected Sub btnNextName_Click(sender As Object, e As EventArgs) Handles btnNextName.Click

        SqlDataSource1.SelectCommand = "SELECT RegNo, FeeBookNo, SName, ClassName, SecName, FName, MName, ASID,SchoolName FROM vw_Student WHERE ASID = " & Request.Cookies("ASID").Value & " AND SName Like '%" & txtSName.Text & "%'"
        gvSearch.DataBind()
        gvSearch.Visible = True
        If gvSearch.Rows.Count > 0 Then
            Panel1.Visible = True
        Else
            Panel1.Visible = False
        End If
    End Sub

    Dim totAmt As Double = 0, TotDueAmt As Double = 0, TotConcessionAmt As Double = 0, TotExcessAmt As Double = 0, TotDepositAmt As Double = 0, totFineAmt As Double = 0
    Protected Sub GridView2_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView2.RowDataBound
        ' '' '' ''If e.Row.RowType = DataControlRowType.DataRow Then
        ' '' '' ''    If e.Row.RowType = DataControlRowType.DataRow Then

        ' '' '' ''        Dim TermID As String = GridView2.DataKeys(e.Row.RowIndex).Values(0).ToString()
        ' '' '' ''        If txtConfigType.Text = "1" Then

        ' '' '' ''            'GetMonthID(Val(txtFeeGroupID.Text), TermID)
        ' '' '' ''            Dim TermAmount As Double = GetFeeConfigForFeeHead(Request.Cookies("ASID").Value, 0, 0, cboTermID.Items(e.Row.RowIndex).Value, "", Val(txtSID.Text))
        ' '' '' ''            Dim LateFeeAmount As Double = GetLateFeeAmount(Val(txtSID.Text), Val(txtFeeGroupID.Text), txtAdmissionDate.Text, TermID, txtAdmissionFeeID.Text, txtLateFeeID.Text, Request.Cookies("ASID").Value, Val(txtConfigType.Text), "", Val(txtAdminFeeApplicable.Text), cboTermID.Items(e.Row.RowIndex).Value)
        ' '' '' ''            e.Row.Cells(3).Text = TermAmount
        ' '' '' ''            e.Row.Cells(4).Text = LateFeeAmount
        ' '' '' ''            totAmt += Val(e.Row.Cells(3).Text)
        ' '' '' ''            totFineAmt += Val(e.Row.Cells(4).Text)

        ' '' '' ''        Else
        ' '' '' ''            Dim TermAmount As Double = GetFeeConfigForFeeHead(Request.Cookies("ASID").Value, Val(txtFeeGroupID.Text), 0, cboTermID.Items(e.Row.RowIndex).Value)
        ' '' '' ''            If Val(txtAdminFeeApplicable.Text) = 0 Then
        ' '' '' ''                Dim AdmissionFee As Double = GetFeeConfigForFeeHead(Request.Cookies("ASID").Value, Val(txtFeeGroupID.Text), txtAdmissionFeeID.Text, cboTermID.Items(e.Row.RowIndex).Value)
        ' '' '' ''                TermAmount = TermAmount - AdmissionFee
        ' '' '' ''            End If
        ' '' '' ''            Dim LateFeeAmount As Double = GetLateFeeAmount(Val(txtSID.Text), Val(txtFeeGroupID.Text), txtAdmissionDate.Text, TermID, txtAdmissionFeeID.Text, txtLateFeeID.Text, Request.Cookies("ASID").Value, Val(txtConfigType.Text), "", Val(txtAdminFeeApplicable.Text), cboTermID.Items(e.Row.RowIndex).Value)
        ' '' '' ''            e.Row.Cells(3).Text = TermAmount
        ' '' '' ''            '+ LateFeeAmount
        ' '' '' ''            e.Row.Cells(4).Text = LateFeeAmount
        ' '' '' ''            totAmt += Val(e.Row.Cells(3).Text)
        ' '' '' ''            totFineAmt += Val(e.Row.Cells(4).Text)
        ' '' '' ''        End If
        ' '' '' ''        Try
        ' '' '' ''            Dim DueDetail As String = GetDueDetail(TermID, Val(txtFeeGroupID.Text), Request.Cookies("ASID").Value)
        ' '' '' ''            Dim busterm As Integer = e.Row.RowIndex + 1
        ' '' '' ''            Dim BusFee As Double = 0
        ' '' '' ''            'GetBusActualAmt(txtSID.Text, busterm)
        ' '' '' ''            e.Row.Cells(3).Text = Convert.ToDouble(e.Row.Cells(3).Text) + BusFee
        ' '' '' ''            e.Row.Cells(2).Text = DueDetail
        ' '' '' ''        Catch ex As Exception

        ' '' '' ''        End Try
        ' '' '' ''        Try
        ' '' '' ''            Dim DepositAmttmp As String = ""
        ' '' '' ''            Dim DepositAmt As Double = 0, DueAmt As Double = 0, ConcessionAmt As Double = 0, ExcessAmt As Double = 0
        ' '' '' ''            DepositAmttmp = GetFeeDepositeForStudent(txtSID.Text, 0, "'" & e.Row.Cells(1).Text & "'", txtExcessFeeID.Text)
        ' '' '' ''            DepositAmt = DepositAmttmp.Split("/")(0)
        ' '' '' ''            ConcessionAmt = DepositAmttmp.Split("/")(1)
        ' '' '' ''            ExcessAmt = DepositAmttmp.Split("/")(2)

        ' '' '' ''            DueAmt = Val(e.Row.Cells(3).Text) + Val(e.Row.Cells(4).Text) - (DepositAmt + ConcessionAmt) + ExcessAmt
        ' '' '' ''            'e.Row.Cells(4).Text = GridView1.Rows(e.Row.RowIndex).Cells(1).Text
        ' '' '' ''            e.Row.Cells(5).Text = DepositAmt
        ' '' '' ''            e.Row.Cells(6).Text = ConcessionAmt
        ' '' '' ''            e.Row.Cells(7).Text = ExcessAmt
        ' '' '' ''            e.Row.Cells(8).Text = DueAmt
        ' '' '' ''            TotDueAmt += DueAmt
        ' '' '' ''            TotDepositAmt += DepositAmt
        ' '' '' ''            TotConcessionAmt += ConcessionAmt
        ' '' '' ''            TotExcessAmt += ExcessAmt
        ' '' '' ''        Catch ex As Exception

        ' '' '' ''        End Try
        ' '' '' ''    End If
        ' '' '' ''End If

        ' '' '' ''If e.Row.RowType = DataControlRowType.Footer Then
        ' '' '' ''    e.Row.Cells(2).Text = "TOTAL :"
        ' '' '' ''    '   e.Row.Cells(2).Font.Bold = True
        ' '' '' ''    e.Row.Cells(3).Text = totAmt
        ' '' '' ''    e.Row.Cells(4).Text = totFineAmt
        ' '' '' ''    e.Row.Cells(5).Text = TotDepositAmt
        ' '' '' ''    e.Row.Cells(6).Text = TotConcessionAmt
        ' '' '' ''    e.Row.Cells(7).Text = TotExcessAmt
        ' '' '' ''    e.Row.Cells(8).Text = TotDueAmt
        ' '' '' ''End If


        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim TermID As String = GridView2.DataKeys(e.Row.RowIndex).Values(0).ToString()
                If txtConfigType.Text = "1" Then

                    'GetMonthID(Val(txtFeeGroupID.Text), TermID)
                    Dim TermAmount As Double = GetFeeConfigForFeeHead(Request.Cookies("ASID").Value, 0, 0, cboTermID.Items(e.Row.RowIndex).Value, "", Val(txtSID.Text))
                    Dim LateFeeAmount As Double = GetLateFeeAmount(Val(txtSID.Text), Val(txtFeeGroupID.Text), txtAdmissionDate.Text, e.Row.Cells(1).Text, txtAdmissionFeeID.Text, txtLateFeeID.Text, Request.Cookies("ASID").Value, Val(txtConfigType.Text), "", Val(txtAdminFeeApplicable.Text), cboTermID.Items(e.Row.RowIndex).Value)
                    e.Row.Cells(3).Text = TermAmount
                    e.Row.Cells(4).Text = LateFeeAmount
                    totAmt += Val(e.Row.Cells(3).Text)
                    totFineAmt += Val(e.Row.Cells(4).Text)

                Else
                    Dim TermAmount As Double = GetFeeConfigForFeeHead(Request.Cookies("ASID").Value, Val(txtFeeGroupID.Text), 0, cboTermID.Items(e.Row.RowIndex).Value)
                    If Val(txtAdminFeeApplicable.Text) = 0 Then
                        Dim AdmissionFee As Double = GetFeeConfigForFeeHead(Request.Cookies("ASID").Value, Val(txtFeeGroupID.Text), txtAdmissionFeeID.Text, cboTermID.Items(e.Row.RowIndex).Value)
                        TermAmount = TermAmount - AdmissionFee
                    End If
                    Dim LateFeeAmount As Double = GetLateFeeAmount(Val(txtSID.Text), Val(txtFeeGroupID.Text), txtAdmissionDate.Text, e.Row.Cells(1).Text, txtAdmissionFeeID.Text, txtLateFeeID.Text, Request.Cookies("ASID").Value, Val(txtConfigType.Text), "", Val(txtAdminFeeApplicable.Text), cboTermID.Items(e.Row.RowIndex).Value)
                    e.Row.Cells(3).Text = TermAmount
                    '+ LateFeeAmount
                    e.Row.Cells(4).Text = LateFeeAmount
                    totAmt += Val(e.Row.Cells(3).Text)
                    totFineAmt += Val(e.Row.Cells(4).Text)
                End If
                Try
                    Dim DueDetail As String = GetDueDetail(TermID, Val(txtFeeGroupID.Text), Request.Cookies("ASID").Value)
                    Dim busterm As Integer = e.Row.RowIndex + 1
                    Dim BusFee As Double = 0
                    'GetBusActualAmt(txtSID.Text, busterm)
                    e.Row.Cells(3).Text = Convert.ToDouble(e.Row.Cells(3).Text) + BusFee
                    e.Row.Cells(2).Text = DueDetail
                Catch ex As Exception

                End Try
                Try
                    Dim DepositAmttmp As String = ""
                    Dim DepositAmt As Double = 0, DueAmt As Double = 0, ConcessionAmt As Double = 0, ExcessAmt As Double = 0
                    DepositAmttmp = GetFeeDepositeForStudent(txtSID.Text, 0, "'" & e.Row.Cells(1).Text & "'", txtExcessFeeID.Text, "yes")
                    DepositAmt = DepositAmttmp.Split("/")(0)
                    ConcessionAmt = DepositAmttmp.Split("/")(1)
                    ExcessAmt = DepositAmttmp.Split("/")(2)

                    DueAmt = Val(e.Row.Cells(3).Text) + Val(e.Row.Cells(4).Text) - (DepositAmt + ConcessionAmt) + ExcessAmt
                    'e.Row.Cells(4).Text = GridView1.Rows(e.Row.RowIndex).Cells(1).Text
                    e.Row.Cells(5).Text = DepositAmt
                    e.Row.Cells(6).Text = ConcessionAmt
                    e.Row.Cells(7).Text = ExcessAmt
                    e.Row.Cells(8).Text = DueAmt
                    TotDueAmt += DueAmt
                    TotDepositAmt += DepositAmt
                    TotConcessionAmt += ConcessionAmt
                    TotExcessAmt += ExcessAmt
                Catch ex As Exception

                End Try
            End If
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(2).Text = "TOTAL :"
            '   e.Row.Cells(2).Font.Bold = True
            e.Row.Cells(3).Text = totAmt
            e.Row.Cells(4).Text = totFineAmt
            e.Row.Cells(5).Text = TotDepositAmt
            e.Row.Cells(6).Text = TotConcessionAmt
            e.Row.Cells(7).Text = TotExcessAmt
            e.Row.Cells(8).Text = TotDueAmt
        End If


    End Sub
    Public Function GetDueDetail(TermID As Integer, FeeGroupID As Integer, ASID As Integer) As String
        Dim Dated As Date = Now.Date.ToString("yyyy/MM/dd")
        Dim tmpNowDate As String = Now.Date.ToString("yyyy/MM/dd")
        Dim LastDate As Date = Now.Date
        Dim Sqlstr As String = ""
        Sqlstr = "Select min(LastDate) From FeeDueConfig Where  FeeGroupID='" & FeeGroupID & "' and TermID=" & TermID & " AND ASID=" & ASID
        Dim DueConfigReader As SqlDataReader = ExecuteQuery_ExecuteReader(Sqlstr)
        While DueConfigReader.Read
            LastDate = CDate(DueConfigReader(0))
        End While
        DueConfigReader.Close()
        Return LastDate.ToString("dd/MM/yyyy")
    End Function

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        txtFeeDepositID.Text = GridView1.DataKeys(GridView1.SelectedIndex).Value
        FillEditDetail()
        chkTermList.Enabled = False
        'myTable.BackColor = Drawing.ColorTranslator.FromHtml("#4DE427")
        GvMyTable.BackColor = Drawing.ColorTranslator.FromHtml("#4DE427")
        '(77, 228, 39, 0.38)
        btnSlip.Visible = True
        btnRemove.Visible = True
        btnSave.Visible = True
        btnSlip.Visible = True
        chkMultipleEntry.Checked = False
        chkMultipleEntry.Enabled = False
        chkFeeRcpt.Visible = True
        chkFeeRcpt.Checked = True

    End Sub

    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim FeeDepositID As String = GridView1.DataKeys(e.Row.RowIndex).Values(0).ToString()

                Dim TermList As String = ""
                Dim Sqlstr As String = ""
                Sqlstr = "Select distinct(TermNo) From vw_FeeDeposit Where FeeDepositID=" & FeeDepositID
                Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(Sqlstr)
                While myReader.Read
                    TermList += myReader(0) & ","
                End While
                myReader.Close()
                Try
                    TermList = TermList.Substring(0, TermList.Length - 1)
                Catch ex As Exception

                End Try
                e.Row.Cells(2).Text = TermList
            End If
        End If
    End Sub

    Private Function GetArrearAmount() As Double
        Dim TermCount As Integer = 0
        Dim t As Integer = 0
        Dim Arrear As Double
        For t = 0 To chkTermList.Items.Count - 1
            If chkTermList.Items(t).Selected = True Then
                TermCount = t
            End If
        Next
        For i = 0 To TermCount - 1
            If (Val(GridView2.Rows(i).Cells(4).Text) + Val(GridView2.Rows(i).Cells(5).Text)) > 0 Then
                Arrear += Val(GridView2.Rows(i).Cells(7).Text)
            End If
        Next
        Return Arrear
    End Function

    'Protected Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
    '    Dim sw As New StringWriter()
    '    Dim hw As New System.Web.UI.HtmlTextWriter(sw)
    '    Dim frm As HtmlForm = New HtmlForm()

    '    Dim filename As String = "FeeDetails_" + DateTime.Now.ToString() + ".xls"

    '    Page.Response.AddHeader("content-disposition", "attachment;filename=" + filename)
    '    Page.Response.ContentType = "application/vnd.ms-excel"
    '    Page.Response.Charset = ""
    '    Page.EnableViewState = False
    '    frm.Attributes("runat") = "server"
    '    Controls.Add(frm)
    '    frm.Controls.Add(GridView2)
    '    frm.RenderControl(hw)
    '    Response.Write(sw.ToString())
    '    Response.End()
    'End Sub


    Protected Sub txtDepositAmount_TextChanged(sender As Object, e As EventArgs)
        CheckGvMyTable()
    End Sub
    Protected Sub txtActualConcession_TextChanged(sender As Object, e As EventArgs)
        Dim txtConcession As TextBox = TryCast(sender, TextBox)
        Dim gvRowIndex As Integer = Convert.ToInt32(txtConcession.Attributes("RowIndex"))
        Dim TotalConcession As Double = 0
        Dim TotalOldConcession As Double = Val(GvMyTable.FooterRow.Cells(4).Text)


        For Each gvr1 As GridViewRow In GvMyTable.Rows
            Dim chkSelect As CheckBox = DirectCast(gvr1.FindControl("chkSelect"), CheckBox)
            Dim txtActualConcession As TextBox = DirectCast(gvr1.FindControl("txtActualConcession"), TextBox)
            If chkSelect.Checked = True Then
                If gvr1.RowIndex <> gvRowIndex Then
                    TotalConcession += Val(txtActualConcession.Text)
                End If
            End If
        Next

        Dim gvr As GridViewRow = GvMyTable.Rows(gvRowIndex)
        'Dim txtActualConcession As TextBox = DirectCast(gvr.FindControl("txtActualConcession"), TextBox)
        Dim txtDepositeAmount As TextBox = DirectCast(gvr.FindControl("txtDepositAmount"), TextBox)
        Try
            txtDepositeAmount.Text = Val(txtDepositeAmount.Text) - Val(txtConcession.Text) - TotalConcession + TotalOldConcession
            If Val(txtDepositeAmount.Text) < 0 Then
                txtDepositeAmount.Text = 0
            End If
        Catch ex As Exception

        End Try


        CheckGvMyTable()
    End Sub

    Protected Sub btnNextRegNo_Click(sender As Object, e As EventArgs) Handles btnNextRegNo.Click
        btnNext_Click(sender, e)
    End Sub

    Protected Sub cboBank_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboBank.SelectedIndexChanged
        Dim BankID As Integer = FindMasterID(72, cboBank.Text)
        LoadFeeBankBranch(BankID, cboBranch)
        cboBank.Focus()
    End Sub
End Class