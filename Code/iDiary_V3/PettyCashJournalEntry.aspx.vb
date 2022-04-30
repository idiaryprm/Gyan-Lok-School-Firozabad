Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class PettyCashJournalEntry
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Accounts") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
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
            'For Grid View Printing. Must have a blank HTM Page (gview.htm)
            'If ViewState("myTable") = True Then


            '    Else
            '    myTable.Rows.Clear()
            'End If
        End If
        If Request.Cookies("UType").Value.ToString.Contains("Accounts-1") = False And Request.Cookies("UType").Value.ToString.Contains("Admin-1") = False Then
            btnSave.Enabled = False
        End If
        'If txtID.Text = "" Then
        '    CreateTable()
        'End If
    End Sub

    Private Sub InitControls()
        txtID.Text = ""
        txtIDOther.Text = ""
        txtVRNo.Text = Request.QueryString("DocType") & GetVrNo(Request.QueryString("DocType"))
        'If txtVRNo.Text.Contains("PV") Then
        '    Literal1.Text = "Payment Voucher"
        lblPaidReceived.Text = "To Whom Paid"
        '    'lblOtherVrNo.Text = "Receipt VR_NO"
        'Else
        '    Literal1.Text = "Receipt Voucher"
        'lblPaidReceived.Text = "From Whom Received"
        '    'lblOtherVrNo.Text = "Payment VR_NO"
        'End If
        txtVRDT.Text = Now.Date.ToString("dd/MM/yyyy")
        txtAmount.Text = ""
        'txtRV.Text = ""
        LoadPettyCashHeads(cboHeadPV)
        LoadPettyCashHeads(cboHeadRV)
        txtPaidReciveDetail.Text = ""
        txtOnWhatAccount.Text = ""
        txtChqNo.Text = ""
        lblStatus.Text = ""
        txtVRNo.Focus()
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

    '   
    '   
    '   

    '    

    '    'Process Head

    '    sqlStr = "Select PCHeadID,PCHeadName From PettyCashHeadMaster where IsCash=0 and IsBank=0 Order By DisplayOrder"
    '    
    '    
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
    '        
    '        
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

    '    
    '    


    '    'If chkPast.Checked = True Then
    '    '-----Get Old Dues------


    '    'Display Fee Total
    '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "MyKey", "ShowTotal();", True)
    '    'myTable.EnableViewState = True
    '    'ViewState("myTable") = True
    'End Sub
    Public Shared Function LoadPettyCashHeads(cbo As DropDownList) As Integer

       
       
       

        Dim sqlstr As String = "Select PCHeadName From PettyCashHeadMaster where IsCash=0 and IsBank=0 Order By PCHeadName"
        
        
        

        Dim myReader As System.Data.SqlClient.SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        cbo.Items.Clear()
        While myReader.Read
            cbo.Items.Add(myReader(0))
        End While
        myReader.Close()

        
        

        Return 0

    End Function
    Public Shared Function GetHeadID(HeadName As String, Optional IsCash As String = "", Optional IsBank As String = "") As Integer

       
       
       
        Dim rv As Integer = 0
        Dim sqlstr As String = "Select PCHeadID From PettyCashHeadMaster where PCHeadName='" & HeadName & "'"
        If IsCash <> "" Then
            sqlstr = "Select PCHeadID From PettyCashHeadMaster where IsCash=1"
        End If
        If IsBank <> "" Then
            sqlstr = "Select PCHeadID From PettyCashHeadMaster where IsBank=1"
        End If
        
        
        

        rv = ExecuteQuery_ExecuteScalar(SqlStr)
        
        

        Return rv

    End Function
    Public Shared Function GetVrNo(DocType As String) As Integer

       
       
       
        Dim rv As Integer = 0
        Dim sqlstr As String = "Select max(Vr_NoNumeric) From PettyCashTransaction where Vr_No like '%" & DocType & "%'"
        
        
        
        Try
            rv = ExecuteQuery_ExecuteScalar(SqlStr)
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
        If txtVRNo.Text = "" Then
            lblStatus.Text = "Invalid VR No..."
            txtVRNo.Focus()
            Exit Sub
        End If
        If txtVRDT.Text = "" Then
            lblStatus.Text = "Invalid VR Date..."
            txtVRDT.Focus()
            Exit Sub
        End If
        Dim VRDT As String = ""
        Try
            VRDT = txtVRDT.Text.Split("/")(2) & "/" & txtVRDT.Text.Split("/")(1) & "/" & txtVRDT.Text.Split("/")(0)
        Catch ex As Exception
            lblStatus.Text = "Invalid VR Date..."
            txtVRDT.Focus()
            Exit Sub
        End Try

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
        If txtPaidReciveDetail.Text = "" Then
            lblStatus.Text = "Invalid To Whome paid/ From Whome Received..."
            txtPaidReciveDetail.Focus()
            Exit Sub
        End If
       

        Dim sqlStr As String = ""
        Dim HeadIDPV As Integer = GetHeadID(cboHeadPV.Text)
        Dim HeadIDRV As Integer = GetHeadID(cboHeadRV.Text)
        Dim VRNONumeric As Integer = txtVRNo.Text.Substring(2, txtVRNo.Text.Length - 2)
       
       
       

        

        'If txtID.Text = "" Then
        '    Save_Log("PETTY INSERT")
        'Else
        '    Save_Log("PETTY UPDATE")
        'End If

        If txtID.Text = "" Then
            sqlStr = "Insert into PettyCashTransaction(Vr_Dt, Vr_No,Vr_NoNumeric, Doc_Code, TransRemark,OnWhatAccount, UserID, EntryDate) Values(" & _
            "'" & VRDT & "'," & _
            "'" & txtVRNo.Text & "'," & _
            "'" & VRNONumeric & "'," & _
            "'PV'," & _
             "'" & txtPaidReciveDetail.Text & "'," & _
             "'" & txtOnWhatAccount.Text & "'," & _
            1 & "," & _
            "'" & Now.Date.ToString("yyyy/MM/dd") & "')"
            
            
            ExecuteQuery_Update(SqlStr)

            sqlStr = "Select Max(TransID) From PettyCashTransaction"
            
            
            txtID.Text = ExecuteQuery_ExecuteScalar(SqlStr)

        Else
            sqlStr = "Update PettyCashTransaction set " & _
           "Vr_Dt='" & VRDT & "'," & _
           "Vr_No='" & txtVRNo.Text & "'," & _
           "Vr_NoNumeric='" & VRNONumeric & "'," & _
           "Doc_Code='PV'," & _
            "TransRemark='" & txtPaidReciveDetail.Text & "'," & _
                  "OnWhatAccount='" & txtOnWhatAccount.Text & "' where TransID=" & txtID.Text
            
            
            ExecuteQuery_Update(SqlStr)

            sqlStr = "Select Max(TransID) From PettyCashTransaction where TransIDOther=" & txtID.Text
            
            
            Dim TmpTransID As Integer = 0
            Try
                TmpTransID = ExecuteQuery_ExecuteScalar(SqlStr)
            Catch ex As Exception

            End Try

            sqlStr = "Delete from PettyCashTransData where TransID=" & TmpTransID
            
            
            ExecuteQuery_Update(SqlStr)

            sqlStr = "Delete from PettyCashTransaction where TransIDOther=" & txtID.Text
            
            
            ExecuteQuery_Update(SqlStr)

        End If

        sqlStr = "Delete from PettyCashTransData where TransID=" & txtID.Text
        
        
        ExecuteQuery_Update(SqlStr)

        Dim DRCR As String = ""
        Dim DRCROther As String = ""
        Dim OtherDocType As String = "RV"
        'If Request.QueryString("DocType") = "PV" Then
        DRCR = "DR"
        DRCROther = "DR"
        'OtherDocType = "RV"
        'Else
        '    DRCR = "CR"
        '    DRCROther = "CR"
        '    OtherDocType = "PV"
        'End If

        sqlStr = "Insert into PettyCashTransData(TransID, HeadID, Description, Amount, DRCR, ChqNo) Values(" & _
       "'" & txtID.Text & "'," & _
       "'" & HeadIDPV & "'," & _
       "''," & _
      Val(txtAmount.Text) & "," & _
"'" & DRCR & "'," & _
       "'" & txtChqNo.Text & "')"
        
        
        ExecuteQuery_Update(SqlStr)

        '        sqlStr = "Insert into PettyCashTransData(TransID, HeadID, Description, Amount, DRCR, ChqNo) Values(" & _
        '       "'" & txtID.Text & "'," & _
        '       "'" & HeadID & "'," & _
        '       "''," & _
        '      Val(txtAmount.Text) & "," & _
        '"'" & DRCR & "'," & _
        '       "'" & txtChqNo.Text & "')"
        '        
        '        
        '        ExecuteQuery_Update(SqlStr)


        sqlStr = "Insert into PettyCashTransaction(Vr_Dt, Vr_No,Vr_NoNumeric, Doc_Code, TransRemark,OnWhatAccount, UserID, EntryDate,TransIDOther) Values(" & _
        "'" & VRDT & "'," & _
        "'" & txtVRNo.Text & "'," & _
        "'0'," & _
        "'" & OtherDocType & "'," & _
         "'" & txtVRNo.Text & "'," & _
         "'" & txtOnWhatAccount.Text & "'," & _
        1 & "," & _
        "'" & Now.Date.ToString("yyyy/MM/dd") & "'," & _
        "'" & txtID.Text & "')"
        
        
        ExecuteQuery_Update(SqlStr)

        sqlStr = "Select Max(TransID) From PettyCashTransaction"
        
        
        txtIDOther.Text = ExecuteQuery_ExecuteScalar(SqlStr)

        sqlStr = "Delete from PettyCashTransData where TransID=" & txtIDOther.Text
        
        
        ExecuteQuery_Update(SqlStr)

        '        sqlStr = "Insert into PettyCashTransData(TransID, HeadID, Description, Amount, DRCR, ChqNo) Values(" & _
        '       "'" & txtIDOther.Text & "'," & _
        '       "'" & HeadCashBankID & "'," & _
        '       "''," & _
        '      Val(txtAmount.Text) & "," & _
        '"'" & DRCROther & "'," & _
        '       "'" & txtChqNo.Text & "')"
        '        
        '        
        '        ExecuteQuery_Update(SqlStr)
        sqlStr = "Insert into PettyCashTransData(TransID, HeadID, Description, Amount, DRCR, ChqNo) Values(" & _
      "'" & txtIDOther.Text & "'," & _
      "'" & HeadIDRV & "'," & _
      "''," & _
     Val(txtAmount.Text) & "," & _
"'" & DRCROther & "'," & _
      "'" & txtChqNo.Text & "')"
        
        
        ExecuteQuery_Update(SqlStr)



        
        
        Dim tmpdate As String = txtVRDT.Text
        InitControls()
        lblStatus.Text = "Data has been saved"
        txtVRDT.Text = tmpdate
        'GenReport()

        'RetainInput()

        'End If
    End Sub

    'Private Sub GenReport()
    '    Dim Type As String = cboTrans.Text & "Voucher"


    '    Dim amountWord As String = iDiary_Fee.CLS_iDiary_Fee.GetNumberAsWords(Val(txtAmount.Text)) & " only"
    '    Dim params(7) As Microsoft.Reporting.WebForms.ReportParameter
    '    params(0) = New Microsoft.Reporting.WebForms.ReportParameter("forName", txtName.Text, Visible)
    '    params(1) = New Microsoft.Reporting.WebForms.ReportParameter("date", txtDate.Text, Visible)
    '    params(2) = New Microsoft.Reporting.WebForms.ReportParameter("receiptno", txtID.Text, Visible)
    '    params(3) = New Microsoft.Reporting.WebForms.ReportParameter("Type", Type, Visible)
    '    params(4) = New Microsoft.Reporting.WebForms.ReportParameter("particular", cboHeadDR.Text & " - " & txtRemark.Text, Visible)
    '    params(5) = New Microsoft.Reporting.WebForms.ReportParameter("amount", txtAmount.Text, Visible)
    '    params(6) = New Microsoft.Reporting.WebForms.ReportParameter("amountWord", amountWord, Visible)
    '    params(7) = New Microsoft.Reporting.WebForms.ReportParameter("AccountDetails", txtAccount.Text, Visible)

    '    Me.ReportViewer1.LocalReport.SetParameters(params)
    '    ReportViewer1.Visible = True
    '    ReportViewer1.LocalReport.Refresh()

    'End Sub

    'Private Sub RetainInput()
    '    Dim TempDate As String = txtDate.Text
    '    Dim TempTransType As String = cboTrans.Text
    '    InitControls()
    '    txtDate.Text = TempDate
    '    cboTrans.Text = TempTransType
    '    LoadPettyCashHeads(cboTrans.Text, cboHead)
    '    lblStatus.Text = "Record saved successfully..."
    'End Sub



    'Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
    '    txtID.Text = GridView1.SelectedRow.Cells(1).Text
    '    txtAmount.Text = GridView1.SelectedRow.Cells(7).Text
    '    txtRemark.Text = GridView1.SelectedRow.Cells(8).Text
    '    txtName.Text = GridView1.SelectedRow.Cells(5).Text
    '    txtAccount.Text = GridView1.SelectedRow.Cells(6).Text
    '    If txtRemark.Text = "&nbsp;" Then txtRemark.Text = ""
    'End Sub

    Protected Sub btnSave0_Click(sender As Object, e As EventArgs) Handles btnSlip.Click
        If Trim(txtID.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('No data to print...');", True)
            Exit Sub
        End If
        'GenReport()
        'RetainInput()

    End Sub

    Private Function IsInventoryItem(itemName As String) As Boolean
        Dim IsInventory As Boolean = False
       
       
       
        
        Dim sqlStr As String = "Select IsInventory from PettyCashHeadMaster Where PCHeadName='" & itemName & "'"
        
        
        IsInventory = ExecuteQuery_ExecuteScalar(sqlStr)
        
        
        Return IsInventory
    End Function
    Private Sub Save_Log(ByVal type As String)
       
       
       
        
        Dim sqlStr As String = "Select VRDT,TransTypeName,PCHeadName,Amount,PCHeadName from vwPettyCashTransaction Where TransID='" & txtID.Text & "'"
        
        
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        Dim log1 As String = ""
        While myReader.Read
            log1 = "Transaction Date : " & myReader(0) & ", Type : " & myReader(1) & ", Head : " & myReader(2) & " Amount : " & myReader(3) & " Name Of : " & myReader(4)
        End While

        myReader.Close()
        'log1 += " ####   Transaction Date : " & txtVRDT.Text & ", Type : " & cboTrans.Text & ", Head : " & cboHead.Text & " Amount : " & txtAmount.Text & " Name Of : " & txtName.Text

        sqlStr = "Insert Into Event_log(logTime,EventType,Details,loginId,Visible) Values('" & System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "','" & type & "','" & log1 & "','" & Request.Cookies("UserID").Value & "','1')"
        
        
        ExecuteQuery_Update(SqlStr)
        
        
    End Sub

    Protected Sub btnNext_Click(sender As Object, e As ImageClickEventArgs) Handles btnNext.Click

        Try
            Dim sqlStr As String = "Select * From vwPettyCashTransaction where Vr_No='" & txtVRNo.Text & "' and Doc_Code='PV'"
           
           
           

            
            
            

            Dim myReader As System.Data.SqlClient.SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                txtID.Text = myReader("TransID").ToString
                Dim a As Date = myReader("Vr_Dt")
                txtVRDT.Text = a.ToString("dd/MM/yyyy")
                txtAmount.Text = myReader("AMount")
                'txtRV.Text = myReader("Vr_NoOther")
                txtChqNo.Text = myReader("ChqNo")
                txtPaidReciveDetail.Text = myReader("TransRemark")
                txtOnWhatAccount.Text = myReader("OnWhatAccount")
                'If myReader("IsCash") = 1 Then
                '    rbCash.Checked = True
                'End If
                'If myReader("IsBank") = 1 Then
                '    rbBank.Checked = True
                'End If
                'If myReader("IsBank") = 0 And myReader("IsCash") = 0 Then
                cboHeadPV.Text = myReader("PCHeadName")
                'End If
            End While
            myReader.Close()
            sqlStr = "Select Max(TransID) From PettyCashTransaction where TransIDOther=" & txtID.Text
            
            
            Dim TmpTransID As Integer = 0
            Try
                TmpTransID = ExecuteQuery_ExecuteScalar(SqlStr)
            Catch ex As Exception

            End Try
            GetOtherHead(TmpTransID)
            'CreateTable()
            'GetDRCR("DR")
            
            

        Catch ex As Exception

        End Try
        lblStatus.Text = ""
    End Sub
    Private Sub GetOtherHead(TransID As Integer)
        Try
            Dim sqlStr As String = "Select * From vwPettyCashTransaction where TransID=" & TransID
           
           
           

            
            
            
            Dim FeeGroupId As String = ""
            Dim myReader As System.Data.SqlClient.SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                cboHeadRV.Text = myReader("PCHeadName")
            End While
            myReader.Close()

            
            

        Catch ex As Exception

        End Try
    End Sub
    Private Function GetHeadAmount(TransID As Integer, HeadID As Integer) As Double
        Dim Amount As Double = 0
        Try
            Dim sqlStr As String = "Select Amount From vwPettyCashTransaction where PCHeadID=" & HeadID & " and  TransID=" & TransID
           
           
           

            
            
            

            Try
                Amount = ExecuteQuery_ExecuteScalar(SqlStr)
            Catch ex As Exception

            End Try

            
            

        Catch ex As Exception

        End Try
        Return Amount
    End Function
End Class