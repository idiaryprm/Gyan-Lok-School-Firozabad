Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary_Fee.CLS_iDiary_Fee

Public Class FeeOnlineImport
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        Try

            If Request.Cookies("UType").Value.ToString.Contains("Fee") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("AccessDenied.aspx")
            End If

        Catch ex As Exception

            If ex.Message.Contains("Object reference not set to an instance of an object") Then
                Response.Redirect("Logout.aspx")
            End If

        End Try

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
    End Sub

    Private Sub InitControls()
        Dim FeeTypes() As String = GetFeeTypeConfigID().Split("$")
        txtAdmissionFeeID.Text = FeeTypes(0)
        txtLateFeeID.Text = FeeTypes(1)
        txtConveyanceFeeID.Text = FeeTypes(2)
        txtTutionFeeID.Text = FeeTypes(3)
        'LoadMasterInfo(1, cboASession)
        'GridView1.DataBind()
    End Sub
    'Private Sub LoadClasses()
    '    Dim sqlStr As String = "Select ClassName From Classes order by DisplayOrder"

    '   
    '   
    '   

    '    
    '    
    '    
    '    Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
    '    cboClass.Items.Clear()
    '    cboClass.Items.Add("")
    '    While myReader.Read
    '        cboClass.Items.Add(myReader(0))
    '    End While
    '    myReader.Close()
    '    
    '    
    'End Sub
    'Private Sub LoadSections()
    '   
    '   
    '   

    '    Dim sqlStr As String = "Select SecName From vw_Class_Section Where ClassName='" & cboClass.Text & "'"
    '    
    '    
    '    
    '    Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
    '    cboSection.Items.Clear()
    '    cboSection.Items.Add("")
    '    While myReader.Read
    '        Try
    '            cboSection.Items.Add(myReader(0))
    '        Catch ex As Exception

    '        End Try
    '    End While
    '    myReader.Close()
    '    

    '    
    'End Sub
    'Private Sub LoadStatus()
    '   
    '   
    '   

    '    Dim sqlstr As String = "Select StatusName From StatusMaster"
    '    (sqlstr, myConn)
    '    Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
    '    cboStatus.Items.Clear()
    '    While myReader.Read
    '        cboStatus.Items.Add(myReader(0))
    '    End While
    '    myReader.Close()
    '    

    '    
    'End Sub
    Protected Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        'If cboClass.Text = "" Then
        '    lblStatus.Text = "Please Select Class..."
        '    cboClass.Focus()
        '    Exit Sub
        'End If
        'If cboSection.Text = "" Then
        '    lblStatus.Text = "Please Select Section..."
        '    cboSection.Focus()
        '    Exit Sub
        'End If
        'If cboStatus.Text = "" Then
        '    lblStatus.Text = "Please Select Status..."
        '    cboStatus.Focus()
        '    Exit Sub
        'End If
        If cboPaymentStatus.SelectedIndex > 0 Then
            btnAdd.Visible = False
            GridView1.Columns(0).Visible = False
            chkCheckAll.Visible = False
        Else
            btnAdd.Visible = True
            GridView1.Columns(0).Visible = True
            chkCheckAll.Visible = True
        End If
        lblStatus.Text = ""
        BindGrid()
      

        'Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        'Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
        'myConn.Open()

        'Dim sqlStr As String = ""
        '

        'Dim i As Integer = 0

        'For i = 0 To GridView1.Rows.Count - 1

        '    sqlStr = "Select Count(*) From vw_Student Where RegNo='" & GridView1.Rows(i).Cells(2).Text & "' AND ASID <> " & Request.Cookies("ASID").Value
        '    
        '    
        '    Dim RecordExist As Integer = 0
        '    RecordExist = ExecuteQuery_ExecuteScalar(SqlStr)

        '    If RecordExist > 0 Then
        '        GridView1.Rows(i).Visible = False
        '        Continue For
        '    End If

        '    Dim chk As CheckBox = DirectCast(GridView1.Rows(i).FindControl("chkSelect"), CheckBox)

        '    sqlStr = "Select Count(*) From FeeDuesAdmission Where ASID=" & Request.Cookies("ASID").Value & " AND SID=" & GetSID(GridView1.Rows(i).Cells(2).Text, Request.Cookies("ASID").Value)
        '    
        '    

        '    If ExecuteQuery_ExecuteScalar(SqlStr) <= 0 Then
        '        chk.Checked = False
        '    Else
        '        chk.Checked = True
        '    End If
        'Next


    End Sub
    Private Sub BindGrid()
        SqlDataSource1.SelectCommand = "SELECT FDOID,RegNo,FeeBookNo,SName,FName,ClassName,SecName,TermNo,TransactionID,PaymentDate, Amount,PaymentResponse FROM [vw_FeeDepositOnline] where IsImport=" & cboPaymentStatus.SelectedIndex & " Order by PaymentDate DESC"
        GridView1.DataSource = SqlDataSource1
        GridView1.DataBind()
    End Sub
    Protected Sub chkCheckAll_CheckedChanged(sender As Object, e As EventArgs) Handles chkCheckAll.CheckedChanged
        Dim myVal As Boolean = False
        Dim i As Integer = 0, myCount As Integer = 0

        If chkCheckAll.Checked = True Then myVal = True

        For i = 0 To GridView1.Rows.Count - 1

            DirectCast(GridView1.Rows(i).FindControl("chkSelect"), CheckBox).Checked = myVal

            myCount += 1
        Next
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click

       
       
       

        Dim sqlStr As String = ""
        

        Dim i As Integer = 0
        For i = 0 To GridView1.Rows.Count - 1
            Dim FDOID As String = GridView1.DataKeys(i).Values(0).ToString()
            Dim chk As CheckBox = DirectCast(GridView1.Rows(i).FindControl("chkSelect"), CheckBox)
            If chk.Checked = True And GridView1.Rows(i).Visible = True Then
                sqlStr = " Select ASID,SID, TermID, TransactionID, PaymentID, RefNo, PaymentDate, Amount, PaymentResponse, IsImport,FeeGroupID,ClassName,SecName,SName,RegNo From vw_FeeDepositOnline Where  FDOID=" & FDOID
                '& " and TermNo <=" & cboBusTerm.Text & " Group By SID"
                
                
                Dim ASID As String = ""
                Dim SID As String = ""
                Dim TermID As String = ""
                Dim PaymentDate As Date = Now.Date
                Dim Amount As Double = 0
                Dim PaymentMode As Integer = 3
                Dim FeeGroupID As Integer = 0

                Dim ClassName As String = ""
                Dim SecName As String = ""
                Dim SName As String = ""
                Dim RegNo As String = ""
                Dim FeeReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
                While FeeReader.Read
                    ASID = FeeReader("ASID")
                    SID = FeeReader("SID")
                    TermID = FeeReader("TermID")
                    PaymentDate = FeeReader("PaymentDate")
                    Amount = FeeReader("Amount")
                    FeeGroupID = FeeReader("FeeGroupID")

                    ClassName = FeeReader("ClassName")
                    SecName = FeeReader("SecName")
                    SName = FeeReader("SName")
                    RegNo = FeeReader("RegNo")
                End While
                FeeReader.Close()
                Dim CMNO As Integer = GetCMNO(ASID, PaymentDate, 0)
                ImportFee(FDOID, ASID, PaymentMode, SID, PaymentDate.ToString("yyyy/MM/dd"), Amount, TermID, CMNO, "", "", "", FeeGroupID, ClassName, SecName, SName, RegNo)
                ChangeVerifyStatus(FDOID)
            End If
        Next
        System.Threading.Thread.Sleep(500)
        
        
        InitControls()
        lblStatus.Text = "Online Transactions has been Verified..."
        BindGrid()
    End Sub
    Private Sub ImportFee(FDOID As Integer, ASID As String, PaymentModeID As Integer, SID As String, Dated As String, ExcelAmount As Double, TermID As Integer, CMNO As String, ChequeNo As String, Details As String, BranchID As String, FeeGroupID As Integer, ClassName As String, SecNAme As String, SNAme As String, Regno As String)
        Dim SubmitAmount As Double = ExcelAmount
        

        Dim sqlStr As String = ""
       
       
       

        sqlStr = "Insert into FeeDeposit (CMNO, ASID, SID, TermID, DepositDate, DepositMode, DepositDetails, isDeposit, BranchID, FDOID) Values(" & _
                CMNO & "," & ASID & "," & SID & "," & TermID & "," & _
                "'" & Dated & "'," & _
                PaymentModeID & "," & "'" & Details & "',1,'" & BranchID & "','" & FDOID & "')"

        
        
        ExecuteQuery_Update(SqlStr)

        Dim FeeDepositID As Integer = 0

        sqlStr = "Select MAX(FeeDepositID) from FeeDeposit where SID=" & SID
        
        
        FeeDepositID = ExecuteQuery_ExecuteScalar(SqlStr)

      


        Dim ds As DataSet = GetFeeConfig(ASID, FeeGroupID, TermID, txtAdmissionFeeID.Text)

        'Process Fee Deposit Details (Similar to Create Table for identiying individual Fee Entry for Term)

        Dim LateFeeAmount As Double = GetLateFee(ASID, SID, TermID, ClassName, SecNAme, Dated)
        For i = 0 To ds.Tables(0).Rows.Count - 1

            Dim myFeeTypeID As String = ds.Tables(0).Rows(i).Item("FeeTypeID")
            Dim FeeActualAmount As Double = ds.Tables(0).Rows(i).Item("FeeAmount")
            Dim OriginalFeeAmount As Double = 0
            If ExcelAmount >= FeeActualAmount Then
                ExcelAmount = ExcelAmount - FeeActualAmount
                OriginalFeeAmount = FeeActualAmount
            Else
                OriginalFeeAmount = ExcelAmount
                ExcelAmount = 0
            End If

            sqlStr = "Insert into FeeDepositDetails Values(" & _
            FeeDepositID & "," & _
            myFeeTypeID & "," & _
            OriginalFeeAmount & "," & _
            FeeActualAmount & ",0)"

            
            
            ExecuteQuery_Update(SqlStr)
           Next

        'Added for Concession Amount Manually Entered and Greater than Auto Calculated Concession
        If ExcelAmount > 0 Then
            sqlStr = "Insert into FeeDepositDetails Values(" & _
                       FeeDepositID & "," & _
                       txtLateFeeID.Text & "," & _
                       ExcelAmount & "," & _
                       LateFeeAmount & ",0)"

            
            
            ExecuteQuery_Update(SqlStr)
        End If
        If PaymentModeID = 2 Then

            sqlStr = "INSERT Into FeeCheque(FeeDepositID,ChequeNo,isDishonoured) Values(" & _
                "'" & FeeDepositID & "'," & _
                "'" & ChequeNo & "'," & _
                              "0)"
            
            
            ExecuteQuery_Update(SqlStr)
        End If

        
        
        
        Save_Log(PaymentModeID, Regno, SNAme, SubmitAmount, ChequeNo, "INSERT")

        'ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Fee saved successfully...');", True)
    End Sub
    Private Sub ChangeVerifyStatus(FDOID As Integer)
       
       
       
        
        Dim SqlStr As String = ""

        SqlStr = "Update FeeDepositOnline set IsImport=1 Where FDOID=" & FDOID

        
        
        ExecuteQuery_Update(SqlStr)
        
        
    End Sub
  
    Private Function GetLateFee(ASID As Integer, SID As Integer, TermID As Integer, ClassName As String, SecName As String, PaymentDate As String) As Double
        Dim Dated As Date = Now.Date.ToString("yyyy/MM/dd")
       
       
       

        
        Dim tmpNowDate As String = PaymentDate
        'Now.Date.ToString("yyyy/MM/dd")
        Dim LastDate As Date = Now.Date
        Dim LateFeeAmount As Double = 0, LateFeeType As Integer = 0, ProcessingMethod As Integer = 0
        Dim Sqlstr As String = ""
        Sqlstr = "Select LastDate, LateFeeAmount, LateFeeType, ProcessingMethod From vwDueConfigClassSec Where ClassName='" & ClassName & "' and SecName='" & SecName & "' and TermID=" & TermID & " AND ASID=" & ASID
        Sqlstr += " And LastDate<='" & tmpNowDate & "'"

        Dim DueConfigReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
        While DueConfigReader.Read
            LastDate = CDate(DueConfigReader(0))
            LateFeeAmount = DueConfigReader(1)
            LateFeeType = DueConfigReader(2)
            ProcessingMethod = DueConfigReader(3)
            'FlagFine = True
        End While
        DueConfigReader.Close()

        If ProcessingMethod = 1 Then  'Monthly
            Dim TimeDiff As New TimeSpan
            TimeDiff = LastDate - Now
            Dim DiffDays As Integer = Math.Abs(TimeDiff.Days)
            Dim Months As Double = Convert.ToDouble(DiffDays) / 30
            LateFeeAmount = LateFeeAmount * Math.Ceiling(Months)
        ElseIf ProcessingMethod = 2 Then  'Daily
            'Calculate difference between current date and last date
            Dim TimeDiff As New TimeSpan
            TimeDiff = LastDate - Now
            Dim DiffDays As Integer = Math.Abs(TimeDiff.Days)
            LateFeeAmount = (LateFeeAmount * DiffDays)
        Else  'Fix
        End If
        
        
        Return LateFeeAmount
    End Function
    Private Sub Save_Log(Mode As Integer, RegNo As String, Sname As String, Amount As Double, ChequeNo As String, ByVal type As String)
       
       
       
        
        Dim log1 As String = ""
        Dim sqlStr As String = ""
        Dim Dated As String = Now.Date.ToString("dd/MM/yyyy")
        If (type.Contains("INSERT") = True) Then
            If Mode = 1 Then
                log1 = "Online Fee Verify of RegNo : " & RegNo & ", Name : " & Sname & ", Fee Sum : " & Amount & ", Payment Mode: Cash, Date : " & Dated
            Else
                log1 = "Online Fee Verify of RegNo : " & RegNo & ", Name : " & Sname & ", Fee Sum : " & Amount & ", Cheque NO : " & ChequeNo & ", Date : " & Dated
            End If
        End If

        sqlStr = "Insert Into Event_log(logTime,EventType,Details,loginId,Visible) Values('" & System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "','" & type & "','" & log1 & "','" & Request.Cookies("UserID").Value & "','1')"
        
        
        ExecuteQuery_Update(SqlStr)
        
        
    End Sub
End Class