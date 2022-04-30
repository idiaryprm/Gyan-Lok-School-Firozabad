Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports Microsoft.Reporting.WebForms
Imports iDiary_V3.iDiary_Fee.CLS_iDiary_Fee
Public Class PettyCashTransactions
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Petty Cash") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
            End If
        Catch ex As Exception
            Response.Redirect("~/Login.aspx")
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("ActiveTab") = 8
        Response.Cookies("ActiveTab").Value = 8
        Response.Cookies("ActiveTab").Expires = DateTime.Now.AddHours(1)
        If IsPostBack = False Then
            InitControls()
        Else
            'For Grid View Printing. Must have a blank HTM Page (gview.htm)
            'If ViewState("myTable") = True Then


            '    Else
            '    myTable.Rows.Clear()
            'End If
        End If
        If Request.Cookies("UType").Value.ToString.Contains("Petty Cash-1") = False And Request.Cookies("UType").Value.ToString.Contains("Admin-1") = False Then
            btnSave.Enabled = False
        End If
        'If txtID.Text = "" Then
        'CreateTable()
        'End If
    End Sub

    Private Sub InitControls()
        txtID.Text = ""
        txtIDOther.Text = ""
        'txtVRNo.Text = Request.QueryString("DocType") &

        lblVr_No.Text = ""
        txtVRNo.Text = ""
        If txtVRNo.Text.Contains("PV") Then
            Literal1.Text = "Petty Cash Transactions"
            'lblPaidReceived.Text = "To Whom Paid"
            'lblOtherVrNo.Text = "Receipt VR_NO"
        Else
            Literal1.Text = "Petty Cash Transactions"
            'lblPaidReceived.Text = "From Whom Received"
            'lblOtherVrNo.Text = "Payment VR_NO"
        End If
        txtVRDT.Text = Now.Date.ToString("dd/MM/yyyy")
        LoadPettyCashHeads(cboPettyCashHead)
        txtAmount.Text = ""
        txtFeeBookNo.Text = ""
        txtSID.Text = ""
        txtSName.Text = ""
        txtClass.Text = ""
        txtRemarks.Text = ""
        txtFName.Text = ""
        txtRegNO.Text = ""
        GridView1.Visible = False
        txtVRNo.Enabled = True
        btnSlip.Visible = False
        btnprint.Visible = False
        btnDelete.Visible = False
        lblSchoolName.Text = ""
        txtClassGroupID.Text = ""
        'ReportViewer1.Visible = False
        'txtRV.Text = ""
        'txtChqNo.Text = ""
        'If rbBank.Checked = True Then
        '    txtChqNo.Enabled = True
        'Else
        '    txtChqNo.Enabled = False
        'End If
        'LoadPettyCashHeads(cboHeadDR)
        'LoadMasterInfo(55, cboVendor)
        'txtPaidReciveDetail.Text = ""
        'txtOnWhatAccount.Text = ""
        lblStatus.Text = ""
        txtFeeBookNo.Focus()
    End Sub
    'Private Sub CreateTable()
    '    myTable.Rows.Clear()

    '    Dim tr1 As New TableRow

    '    Dim td10 As New TableCell
    '    td10.Text = "<B>ID</B>"
    '    td10.HorizontalAlign = HorizontalAlign.Center
    '    tr1.Cells.Add(td10)

    '    Dim td11 As New TableCell
    '    td11.Text = "<B>Head</B>"
    '    td11.HorizontalAlign = HorizontalAlign.Center
    '    tr1.Cells.Add(td11)

    '    Dim td12 As New TableCell
    '    td12.Text = "<B>Amount</B>"
    '    td12.HorizontalAlign = HorizontalAlign.Center
    '    tr1.Cells.Add(td12)

    '    myTable.Rows.Add(tr1)

    '    Dim sqlStr As String = ""
    '    Dim myTxtBoxNumber As Integer = 1







    '    'Process Head

    '    sqlStr = "Select PCHeadID,PCHeadName From PettyCashHeadMaster where IsCash=0 and IsBank=0 Order By DisplayOrder"


    '    Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)

    '    While myReader.Read
    '        Dim trx As New TableRow

    '        Dim tdx0 As New TableCell
    '        tdx0.Text = myReader(0)
    '        tdx0.HorizontalAlign = HorizontalAlign.Center
    '        trx.Cells.Add(tdx0)

    '        Dim tdx1 As New TableCell
    '        tdx1.Text = myReader(1)
    '        tdx1.HorizontalAlign = HorizontalAlign.Left
    '        trx.Cells.Add(tdx1)

    '        Dim txtAmount1 As New TextBox()
    '        txtAmount1.ID = "txtA" & myTxtBoxNumber
    '        'txtAmount1.Enabled = False
    '        txtAmount1.Width = 100
    '        txtAmount1.Attributes.Add("onchange", "javascript: ShowTotal();")
    '        Dim tdx3 As New TableCell
    '        tdx3.Controls.Add(txtAmount1)
    '        tdx3.HorizontalAlign = HorizontalAlign.Center
    '        trx.Cells.Add(tdx3)

    '        myTable.Rows.Add(trx)

    '        myTxtBoxNumber += 1
    '    End While
    '    myReader.Close()
    '    Dim TmpTransID As Integer = 0
    '    If txtID.Text <> "" Then
    '        sqlStr = "Select Max(TransID) From PettyCashTransaction where TransIDOther=" & txtID.Text


    '        Try
    '            TmpTransID = ExecuteQuery_ExecuteScalar(SqlStr)
    '        Catch ex As Exception

    '        End Try
    '    End If
    '    For i = 1 To myTable.Rows.Count - 1
    '        Dim myHeadID As String = myTable.Rows(i).Cells(0).Text   'Get FeeTypeID From Table
    '        Dim myAmount As Double = 0
    '        If txtID.Text <> "" Then
    '            myAmount = GetHeadAmount(TmpTransID, myHeadID)
    '        Else
    '            myAmount = 0
    '        End If
    '        CType(myTable.FindControl("txtA" & i), TextBox).Text = Math.Ceiling(myAmount)
    '    Next





    '    'If chkPast.Checked = True Then
    '    '-----Get Old Dues------


    '    'Display Fee Total
    '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "MyKey", "ShowTotal();", True)
    '    'myTable.EnableViewState = True
    '    'ViewState("myTable") = True
    'End Sub
    Public Shared Function LoadPettyCashHeads(cbo As DropDownList) As Integer





        Dim sqlstr As String = "Select PCHeadName From PettyCashHeadMaster Order By DisplayOrder"




        Dim myReader As System.Data.SqlClient.SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
        cbo.Items.Clear()
        cbo.Items.Add("")
        While myReader.Read
            cbo.Items.Add(myReader(0))
        End While
        myReader.Close()




        Return 0

    End Function
    Public Shared Function GetHeadID(HeadName As String, Optional IsCash As String = "", Optional IsBank As String = "") As Integer




        Dim rv As Integer = 0
        Dim sqlstr As String = "Select PCHeadID From PettyCashHeadMaster where PCHeadName='" & HeadName & "'"
        'If IsCash <> "" Then
        '    sqlstr = "Select PCHeadID From PettyCashHeadMaster where IsCash=1"
        'End If
        'If IsBank <> "" Then
        '    sqlstr = "Select PCHeadID From PettyCashHeadMaster where IsBank=1"
        'End If




        rv = ExecuteQuery_ExecuteScalar(sqlstr)



        Return rv

    End Function
    Public Shared Function GetVrNo(DocType As String, ClassGroupID As Integer) As Integer




        Dim rv As Integer = 0
        Dim sqlstr As String = "Select max(Vr_NoNumeric) From PettyCashTransaction where Doc_Code='" & DocType & "' and ClassGroupID=" & ClassGroupID



        Try
            rv = ExecuteQuery_ExecuteScalar(sqlstr)
        Catch ex As Exception

        End Try



        Return rv + 1

    End Function
    Private Sub PerformGridBind()
        'SqlDataSource1.SelectCommand &= " AND TransDate='" & txtDate.Text.Substring(6, 4) & "/" & txtDate.Text.Substring(3, 2) & "/" & txtDate.Text.Substring(0, 2) & "'"
        'GridView1.DataBind()
        'Dim i As Integer = 0
        'For i = 0 To GridView1.Rows.Count - 1
        '    If GridView1.Rows(i).Cells(6).Text = "&amp;nbsp;" Then GridView1.Rows(i).Cells(6).Text = ""
        'Next
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        'If txtVRNo.Text = "" Then
        '    lblStatus.Text = "Invalid VR No..."
        '    txtVRNo.Focus()
        '    Exit Sub
        'End If
        If Val(txtSID.Text) = 0 Then
            lblStatus.Text = "Please Select Student..."
            txtFeeBookNo.Focus()
            Exit Sub
        End If
        If txtVRDT.Text = "" Then
            lblStatus.Text = "Invalid Deposit Date..."
            txtVRDT.Focus()
            Exit Sub
        End If
        Dim VRDT As Date = Now.Date
        Try
            VRDT = txtVRDT.Text.Split("/")(2) & "/" & txtVRDT.Text.Split("/")(1) & "/" & txtVRDT.Text.Split("/")(0)
        Catch ex As Exception
            lblStatus.Text = "Invalid Deposit Date..."
            txtVRDT.Focus()
            Exit Sub
        End Try
        If cboPettyCashHead.Text = "" Then
            lblStatus.Text = "Please Select Petty Cash Head..."
            cboPettyCashHead.Focus()
            Exit Sub
        End If
        'If txtPaidReciveDetail.Text = "" Then
        '    lblStatus.Text = "Invalid To Whome paid/ From Whome Received..."
        '    txtPaidReciveDetail.Focus()
        '    Exit Sub
        'End If
        'If rbBank.Checked = True And txtChqNo.Text = "" Then
        'lblStatus.Text = "Invalid Chq No..."
        'txtChqNo.Focus()
        'Exit Sub
        'Else
        'txtChqNo.Text = ""
        'End If
        If IsNumeric(txtAmount.Text) = False Or Val(txtAmount.Text) = 0 Then
            lblStatus.Text = "Invalid Amount..."
            txtAmount.Focus()
            Exit Sub
        End If
        'If cboHead.Text = "" Then
        '    lblStatus.Text = "Invalid Head..."
        '    cboHead.Focus()
        '    Exit Sub
        'End If
        'Dim TmpAmount As Double = 0
        'For i = 1 To myTable.Rows.Count - 1
        '    TmpAmount += Val(CType(myTable.FindControl("txtA" & i), TextBox).Text)
        'Next
        'If Val(txtAmount.Text) <> TmpAmount Then
        '    lblStatus.Text = "Both side Amount are not equal..."
        '    txtAmount.Focus()
        '    Exit Sub
        'End If

        Dim sqlStr As String = ""
        'Dim TransTypeID As Integer = FindMasterID(17, cboTrans.Text)
        '' '' ''Dim HeadCashBankID As Integer = 0
        '' '' ''If rbCash.Checked = True Then
        '' '' ''    HeadCashBankID = GetHeadID("", "Yes", "")
        '' '' ''Else
        '' '' ''    HeadCashBankID = GetHeadID("", "", "Yes")
        '' '' ''End If
        'Dim HeadID As Integer = GetHeadID(cboHead.Text)
        'Dim HeadIDDR As Integer = GetHeadID(cboHeadDR.Text)
        '   Dim VRNONumeric As Integer = 0
        'txtVRNo.Text.Substring(2, txtVRNo.Text.Length - 2)
        Dim SID As Integer = Val(txtSID.Text)
        Dim PCHeadID As Integer = FindPettyCashHeadID(cboPettyCashHead.Text, "Income")
        Dim TransAmount As Double = Val(txtAmount.Text)
      
        'If txtID.Text = "" Then
        '    Save_Log("PETTY INSERT")
        'Else
        '    Save_Log("PETTY UPDATE")
        'End If

        If txtID.Text = "" Then
            'txtVRNo.Text = Request.QueryString("DocType") &
            lblVr_No.Text = GetVrNo(Request.QueryString("DocType"), txtClassGroupID.Text)
            txtVRNo.Text = getVr_NoNumeric(Val(lblVr_No.Text))

            sqlStr = "Insert into PettyCashTransaction(SID,PCHeadID,TransAmount,InWords,Vr_Dt,ClassGroupID,Vr_No,Vr_NoNumeric,Doc_Code,TransRemark,OnWhatAccount,UserID,EntryDate,IsTransDeleted) Values(" & _
"'" & SID & "'," & _
            "'" & PCHeadID & "'," & _
            "'" & TransAmount & "'," & _
            "'" & GetNumberAsWords(TransAmount) & "'," & _
"'" & VRDT.ToString("yyyy/MM/dd") & "'," & _
"'" & txtClassGroupID.Text & "'," & _
            "'" & txtVRNo.Text & "'," & _
                        "'" & lblVr_No.Text & "'," & _
            "'" & Request.QueryString("DocType") & "'," & _
             "'" & txtRemarks.Text & "'," & _
             "''," & _
            Request.Cookies("UserID").Value & "," & _
            "'" & Now.Date.ToString("yyyy/MM/dd") & "',0)"
            ExecuteQuery_Update(sqlStr)
            sqlStr = "Select Max(TransID) From PettyCashTransaction where Vr_No='" & txtVRNo.Text & "' and ClassGroupID=" & txtClassGroupID.Text
            txtID.Text = ExecuteQuery_ExecuteScalar(sqlStr)
            Save_Log("PETTY CASH INSERT")
        Else
            sqlStr = "Update PettyCashTransaction set " & _
            "SID='" & SID & "'," & _
            "PCHeadID='" & PCHeadID & "'," & _
            "TransAmount='" & TransAmount & "'," & _
           "Vr_Dt='" & VRDT.ToString("yyyy/MM/dd") & "'," & _
           "Vr_No='" & txtVRNo.Text & "'," & _
           "Vr_NoNumeric='" & lblVr_No.Text & "'," & _
           "Doc_Code='" & Request.QueryString("DocType") & "'," & _
            "TransRemark='" & SQLFixup(txtRemarks.Text) & "'," & _
                  "OnWhatAccount='' where TransID=" & txtID.Text
            ExecuteQuery_Update(sqlStr)
            Save_Log("PETTY CASH UPDATE")
        End If

       
        If chkFeeRcpt.Checked = True Then
            GenReport(txtID.Text)
        End If

        Dim tmpdate As String = txtVRDT.Text
        InitControls()
        If chkFeeRcpt.Checked Then
            btnprint.Visible = True
        Else
            btnprint.Visible = False
        End If
        lblStatus.Text = "Petty Cash Details has been saved"
        txtVRDT.Text = tmpdate
    End Sub

    Protected Sub btnSave0_Click(sender As Object, e As EventArgs) Handles btnSlip.Click
        If Trim(txtID.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('No data to print...');", True)
            Exit Sub
        End If
        GenReport(txtID.Text)
        'RetainInput()

    End Sub
    Private Sub GenReport(TransID As Integer)
        Dim sqlStr As String = ""

        sqlStr = "Select * from Params"
        Dim SchoolName As String = ""
        Dim Address As String = ""
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)

        While myReader.Read
            SchoolName = myReader("SchoolName")
            Address = myReader("SchoolDetails")
        End While
        myReader.Close()
        '.........................................................................................................................

        Dim totalWord As String = GetNumberAsWords(txtAmount.Text)
        'Dim className As String = txtClass.Text & "-" & txtSec.Text

        Dim Sql As String = "Select * From vwPettyCashTransaction Where TransID =" & TransID & ""
        Dim ds As New DataSet
        ds = ExecuteQuery_DataSet(Sql, "tbl")
        Dim rds As ReportDataSource = New ReportDataSource()
        rds.Name = "dsFeeCollection" ' Change to what you will be using when creating an objectdatasource
        rds.Value = ds.Tables(0)
        With ReportViewer1   ' Name of the report control on the form
            .Reset()
            .ProcessingMode = ProcessingMode.Local
            .LocalReport.DataSources.Clear()
            .Visible = True
            .LocalReport.ReportPath = "Report/rptPettyCashReceipt.rdlc"
            .LocalReport.DataSources.Add(rds)
        End With

        Dim params(2) As Microsoft.Reporting.WebForms.ReportParameter

        params(0) = New Microsoft.Reporting.WebForms.ReportParameter("SchoolAddress", Address, Visible)
        params(1) = New Microsoft.Reporting.WebForms.ReportParameter("SchoolName", SchoolName, Visible)
        params(2) = New Microsoft.Reporting.WebForms.ReportParameter("TotalWords", totalWord, Visible)

        Me.ReportViewer1.LocalReport.SetParameters(params)

        ReportViewer1.Visible = True
        btnprint.Visible = True
        ReportViewer1.LocalReport.Refresh()
    End Sub
    Private Function IsInventoryItem(itemName As String) As Boolean
        Dim IsInventory As Boolean = False




        Dim sqlStr As String = "Select IsInventory from PettyCashHeadMaster Where PCHeadName='" & itemName & "'"


        IsInventory = ExecuteQuery_ExecuteScalar(sqlStr)


        Return IsInventory
    End Function
    Private Sub Save_Log(ByVal type As String)




        Dim sqlStr As String = "Select VR_DT,PCHeadName,TransAmount,SName from vwPettyCashTransaction Where TransID=" & txtID.Text
        'Vr_No='" & txtVRNo.Text & "' and ClassGroupID=" & txtClassGroupID.Text


        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        Dim log1 As String = ""
        While myReader.Read
            log1 = "Transaction Date : " & myReader(0) & ", Head : " & myReader(1) & " Amount : " & myReader(2) & " Name Of : " & myReader(3)
        End While

        myReader.Close()
        'log1 += " ####   Transaction Date : " & txtVRDT.Text & ", Type : " & cboTrans.Text & ", Head : " & cboHead.Text & " Amount : " & txtAmount.Text & " Name Of : " & txtName.Text

        'sqlStr = "Insert Into Event_log(logTime,EventType,Details,loginId,Visible) Values('" & System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "','" & type & "','" & log1 & "','" & Request.Cookies("UserID").Value & "','1')"
        sqlStr = "Insert Into Event_log(logTime,EventType,Details,UserId,Visible) Values('" & System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "','" & type & "','" & log1 & "','" & Request.Cookies("UserID").Value & "','1')"

        ExecuteQuery_Update(sqlStr)


    End Sub


    Private Function GetHeadAmount(TransID As Integer, HeadID As Integer) As Double
        Dim Amount As Double = 0
        Try
            Dim sqlStr As String = "Select Amount From vwPettyCashTransaction where PCHeadID=" & HeadID & " and  TransID=" & TransID








            Try
                Amount = ExecuteQuery_ExecuteScalar(sqlStr)
            Catch ex As Exception

            End Try




        Catch ex As Exception

        End Try
        Return Amount
    End Function



    Protected Sub btnFeeBookNext_Click(sender As Object, e As EventArgs) Handles btnFeeBookNext.Click
        Dim type As Integer = 2
        If sender.ID = "btnNextRegNo" Then
            type = 1
        End If
        If type = 2 Then
            If txtFeeBookNo.Text = "" Then
                lblStatus.Text = "Please Enter Fee Book No"
                txtFeeBookNo.Focus()
                Exit Sub
            End If
        Else
            If txtRegNO.Text = "" Then
                lblStatus.Text = "Please Enter Reg No"
                txtRegNO.Focus()
                Exit Sub
            End If
        End If
        
        lblStatus.Text = ""
       
        ShowStudentDetails(type)
        cboPettyCashHead.Focus()
    End Sub

    Protected Sub btnNextRegNo_Click(sender As Object, e As EventArgs) Handles btnNextRegNo.Click
        btnFeeBookNext_Click(sender, e)
    End Sub
    Private Function ShowStudentDetails(type As Integer) As Integer
        If type <> 3 Then
            txtSID.Text = ""
        End If

        Dim AdmissionDate As Date = Now.Date

        Dim sqlStr As String = ""
        If type = 1 Then
            sqlStr = "Select SID, RegNo,FeeBookNo, SName, FName, ClassName, SecName,FeeGroupID,AdmissionDate,FeeGroupName,FeeConfigType,ClassGroupID,SchoolName From vw_Student Where RegNo='" & txtRegNO.Text & "' AND ASID=" & Request.Cookies("ASID").Value
        ElseIf type = 2 Then
            sqlStr = "Select SID, RegNo,FeeBookNo, SName, FName, ClassName, SecName,FeeGroupID,AdmissionDate,FeeGroupName,FeeConfigType,ClassGroupID,SchoolName From vw_Student Where FeeBookNo='" & txtFeeBookNo.Text & "' AND ASID=" & Request.Cookies("ASID").Value
        ElseIf type = 3 Then
            sqlStr = "Select SID, RegNo,FeeBookNo, SName, FName, ClassName, SecName,FeeGroupID,AdmissionDate,FeeGroupName,FeeConfigType,ClassGroupID,SchoolName From vw_Student Where SID='" & txtSID.Text & "'"
        End If
        Dim myCount As Integer = 0
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            txtSID.Text = myReader("SID")
            txtRegNO.Text = myReader("RegNo")
            txtFeeBookNo.Text = myReader("FeeBookNo")
            txtSName.Text = myReader("SName")
            txtFName.Text = myReader("FName")
            txtClass.Text = myReader("ClassName") & "/" & myReader("SecName")
            txtClassGroupID.Text = myReader("ClassGroupID")
            lblSchoolName.Text = myReader("SchoolName")
            'txtSec.Text =
            'txtFeeGroupID.Text = myReader("FeeGroupID")
            'lblFeeGroupName.Text = "Fee Group: " & myReader("FeeGroupName")
            'Try
            '    AdmissionDate = myReader("AdmissionDate")
            'Catch ex As Exception

            'End Try
            'Try
            '    txtConfigType.Text = myReader("FeeConfigType")
            'Catch ex As Exception
            '    txtConfigType.Text = "0"
            'End Try
            'txtAdmissionDate.Text = AdmissionDate.ToString("yyyy/MM/dd")
            myCount += 1
        End While
        myReader.Close()
        If txtSID.Text = "" Then
            Return 0
        Else
            Try
                cboPettyCashHead.SelectedIndex = 0
            Catch ex As Exception

            End Try
            If lblVr_No.Text = "" Then
                lblVr_No.Text = GetVrNo(Request.QueryString("DocType"), txtClassGroupID.Text)
                txtVRNo.Text = getVr_NoNumeric(lblVr_No.Text)

            End If
            
        End If

        ReportViewer1.Visible = False
        btnprint.Visible = False
        FillHistoryGrid()

        Return myCount
    End Function
    Private Sub FillHistoryGrid()
        SqlDataSource2.SelectCommand = "SELECT * from vwPettyCashTransaction where SID=" & Val(txtSID.Text) & "  order by [Vr_Dt] DESC"
        GridView1.Visible = True
        GridView1.DataBind()

    End Sub

    Protected Sub btnSNameNext_Click(sender As Object, e As EventArgs) Handles btnSNameNext.Click
        SqlDataSource1.SelectCommand = "SELECT RegNo, FeeBookNo, SName, ClassName, SecName, FName, MName, ASID FROM vw_Student WHERE ASID = " & Request.Cookies("ASID").Value & " AND SName Like '%" & txtSName.Text & "%'"
        gvSearch.DataBind()
        gvSearch.Visible = True

    End Sub

    Protected Sub gvSearch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvSearch.SelectedIndexChanged
        txtFeeBookNo.Text = gvSearch.SelectedRow.Cells(2).Text
        btnFeeBookNext_Click(sender, e)
        gvSearch.Visible = False
        cboPettyCashHead.Focus()
    End Sub

    Protected Sub btnNext_Click(sender As Object, e As EventArgs)
        'SqlDataSource2.SelectCommand = "SELECT * from vwPettyCashTransaction where Vr_No='" & txtVRNo.Text & "'"
        'GridView1.Visible = True
        'GridView1.DataBind()


    End Sub

    Protected Sub btnNext_Click1(sender As Object, e As EventArgs) Handles btnNext.Click
        FillVrDetails(txtVRNo.Text, 1)
    End Sub
    Private Sub FillVrDetails(Vr_No As String, type As Integer)
        txtSID.Text = ""

        Dim AdmissionDate As Date = Now.Date

        Dim sqlStr As String = ""
        If type = 1 Then
            sqlStr = "SELECT * from vwPettyCashTransaction where Vr_No='" & Vr_No & "'  order by SchoolName"
            SqlDataSource2.SelectCommand = sqlStr
            '"SELECT * from vwPettyCashTransaction where SID=" & Val(txtSID.Text) & "  order by [Vr_Dt] DESC"
            GridView1.Visible = True
            GridView1.DataBind()
            If GridView1.Rows.Count = 0 Then
                lblStatus.Text = "Vr_No does not exist"
            End If
        Else
            Dim ClassGroupID As Integer = 0
            Dim SchoolName As String = ""
            Dim PCHeadName As String = ""
            sqlStr = "SELECT * from vwPettyCashTransaction where TransID='" & Vr_No & "'"
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                txtSID.Text = myReader("SID")
                txtID.Text = myReader("TransID")
                PCHeadName = myReader("PCHeadName")
                Dim a As Date = myReader("Vr_Dt")
                txtVRDT.Text = a.ToString("dd/MM/yyyy")
                txtAmount.Text = myReader("TransAmount")
                txtRemarks.Text = myReader("TransRemark")
                lblVr_No.Text = myReader("Vr_NoNumeric")
                txtVRNo.Text = myReader("Vr_No")
                ClassGroupID = myReader("ClassGroupID")
                SchoolName = myReader("SchoolName")
            End While
            myReader.Close()
            ReportViewer1.Visible = False
            btnprint.Visible = False
            If txtSID.Text <> "" Then
                ShowStudentDetails(3)
                txtClassGroupID.Text = ClassGroupID
                lblSchoolName.Text = SchoolName
                cboPettyCashHead.Text = PCHeadName
                txtVRNo.Enabled = False
                lblStatus.Text = "You are in Edit Mode"

                btnSlip.Visible = True
                If Request.Cookies("UType").Value.ToString.Contains("Admin") = True Then
                    btnDelete.Visible = True
                Else
                    btnDelete.Visible = False
                End If

            Else
                lblStatus.Text = "Vr_No does not exist"
                btnSlip.Visible = False
                btnDelete.Visible = False
            End If
        End If
    End Sub
    Private Sub FillSchoolByPettyCashHead()
        Dim sqlStr As String = ""

        sqlStr = "SELECT * from vwPettyCashHead where PCHeadNAme='" & cboPettyCashHead.Text & "'"
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            Try
                Dim ClassGroupID As Integer = 0
                Try
                    ClassGroupID = myReader("ClassGroupID")
                    If ClassGroupID > 0 Then
                        txtClassGroupID.Text = ClassGroupID
                        lblSchoolName.Text = myReader("SchoolName")
                        lblVr_No.Text = GetVrNo(Request.QueryString("DocType"), txtClassGroupID.Text)
                        txtVRNo.Text = getVr_NoNumeric(lblVr_No.Text)
                    End If

                Catch ex As Exception

                End Try


            Catch ex As Exception

            End Try
            
        End While
        myReader.Close()
    End Sub
    Private Function getVr_NoNumeric(ByVal Vr_No As String) As String
        Dim rv As String = ""
        Dim asName As String = ""
        Try
            asName = Request.Cookies("ASName").Value.Split("-")(0).Substring(2, 2)
        Catch ex As Exception

        End Try
        rv = Val(Vr_No) & "/" & asName
        Return rv
    End Function
    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        txtID.Text = GridView1.DataKeys(GridView1.SelectedIndex).Value
        FillVrDetails(txtID.Text, 2)
    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim sqlStr As String = ""
        Save_Log("PETTY CASH DELETE")
        sqlStr = "update PettyCashTransaction set IsTransDeleted=1 where TransID='" & txtID.Text & "'"
        ExecuteQuery_Update(sqlStr)
        lblStatus.Text = "Entry has been removed..."
        InitControls()
    End Sub

    Protected Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        InitControls()
    End Sub

    Protected Sub cboPettyCashHead_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPettyCashHead.SelectedIndexChanged
        FillSchoolByPettyCashHead()
    End Sub
End Class