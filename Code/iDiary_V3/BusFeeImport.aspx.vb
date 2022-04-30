Imports System.Data.SqlClient
Imports Microsoft.Office.Interop
Imports System.IO
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary_Fee.CLS_iDiary_Fee
Public Class ImportBusFee
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
        LoadClasses()
        cboSection.Items.Clear()
        'LoadTerms()
        LoadStatus()
        lblStatus.Text = ""
        btnImportFee.Enabled = False
        btnCheckExcel.Enabled = True
        cboClass.Focus()
    End Sub

    Private Sub LoadClasses()
        Dim sqlStr As String = "Select ClassName From Classes Order by DisplayOrder"

       
       
       

        
        
        
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        cboClass.Items.Clear()
        cboClass.Items.Add("")
        While myReader.Read
            cboClass.Items.Add(myReader(0))
        End While
        myReader.Close()
        
        
    End Sub

    Protected Sub cboClass_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboClass.SelectedIndexChanged
        LoadClassSections()
        LoadFeeTerms(cboTerm, GetFeeGroupID(cboClass.Text))
        cboSection.Focus()
    End Sub

    Private Sub LoadClassSections()
       
       
       

        Dim sqlStr As String = "Select SecName From vw_Class_Section Where ClassName='" & cboClass.Text & "'"
        
        
        
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        cboSection.Items.Clear()
        cboSection.Items.Add("")
        While myReader.Read
            Try
                cboSection.Items.Add(myReader(0))
            Catch ex As Exception

            End Try
        End While
        myReader.Close()
        

        
    End Sub

    Private Sub LoadTerms()
        cboTerm.Items.Clear()
        cboTerm.Items.Add("")
        cboTerm.Items.Add("1")
        cboTerm.Items.Add("2")
        cboTerm.Items.Add("3")
    End Sub

    Private Sub LoadStatus()
       
       
       

        Dim sqlstr As String = "Select StatusName From StatusMaster"
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
        cboStatus.Items.Clear()
        While myReader.Read
            cboStatus.Items.Add(myReader(0))
        End While
        myReader.Close()
        

        
    End Sub


    'Protected Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
    '    If cboClass.SelectedIndex <= 0 Then
    '        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Select Class');", True)
    '        'lblStatus.Text = "Invalid Class..."
    '        cboClass.Focus()
    '        Exit Sub
    '    End If

    '    If cboSection.SelectedIndex <= 0 Then
    '        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Select Section');", True)
    '        cboSection.Focus()
    '        Exit Sub
    '    End If
    '    If cboTerm.SelectedIndex <= 0 Then
    '        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Select Term');", True)
    '        cboTerm.Focus()
    '        Exit Sub
    '    End If
    '    If cboStatus.Text = "" Then
    '        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Select Status');", True)
    '        cboStatus.Focus()
    '        Exit Sub
    '    End If
    '    Dim fileTest As String = Server.MapPath("~/Excel Files/Group" & Request.QueryString("Group") & ".xlsx")
    '    Dim oExcel As Object
    '    oExcel = CreateObject("Excel.Application")
    '    oExcel.Workbooks.Open(fileTest)
    '    Dim oBook As Excel.Workbook
    '    Dim oSheet As Excel.Worksheet
    '    oBook = oExcel.ActiveWorkbook
    '    oSheet = oExcel.Worksheets(1)
    '    oSheet.Name = cboClass.Text & "_" & cboSection.Text & "_" & cboTerm.Text
    '   
    '   
    '   
    '    'Dim sqlStr As String = ""
    '    Dim sqlStr As String = "Select SID,SName from vw_Student  where ClassName='" & cboClass.Text & "' and SecName ='" & cboSection.Text & "' and ASID=" & Request.Cookies("ASID").Value & " And StatusName='" & cboStatus.Text & "' order by SName"
    '    
    '    
    '    
    '    Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
    '    Dim row As Integer = 6
    '    While myReader.Read
    '        Try
    '            oSheet.Range("A" & row).Value = row - 5
    '            oSheet.Range("A" & row).Locked = True
    '            oSheet.Range("B" & row).Value = myReader(0)
    '            oSheet.Range("B" & row).Locked = True
    '            oSheet.Range("C" & row).Value = myReader(1)
    '            oSheet.Range("C" & row).Locked = True
    '            row += 1
    '        Catch ex As Exception

    '        End Try
    '    End While
    '    myReader.Close()
    '    
    '    
    '    

    '    'save
    '    oExcel.DisplayAlerts = False
    '    oBook.SaveAs(Server.MapPath("~/Excel Files/" & cboClass.Text & "_" & cboSection.Text & "_" & cboTerm.Text & ".xls"), 1) ' 1== xls
    '    oBook.Close()
    '    oBook = Nothing
    '    Response.Redirect("~/Excel Files/" & cboClass.Text & "_" & cboSection.Text & "_" & cboTerm.Text & ".xls")
    'End Sub

    Protected Sub btnCheckExcel_Click(sender As Object, e As EventArgs) Handles btnCheckExcel.Click
        'If cboClass.SelectedIndex <= 0 Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Select Class');", True)
        '    'lblStatus.Text = "Invalid Class..."
        '    cboClass.Focus()
        '    Exit Sub
        'End If

        'If cboSection.SelectedIndex <= 0 Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Select Section');", True)
        '    cboSection.Focus()
        '    Exit Sub
        'End If
        'If cboTerm.SelectedIndex <= 0 Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Select Term');", True)
        '    cboTerm.Focus()
        '    Exit Sub
        'End If
        'If cboStatus.Text = "" Then
        '    lblStatus.Text = "Invalid Status..."
        '    cboStatus.Focus()
        '    Exit Sub
        'End If
        Dim fp1 As String = myFile.PostedFile.FileName
        If fp1 = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Select Excel File');", True)
            myFile.Focus()
            Exit Sub
        End If
        Dim fileExt As String = System.IO.Path.GetExtension(myFile.FileName)
        If fileExt = ".xls" Or fileExt = ".xlsx" Then
        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid File Selection, Please Select Excel File');", True)
            myFile.Focus()
            Exit Sub
        End If
        Dim fn1 As String = fp1.Substring(fp1.LastIndexOf("\\") + 1)
        Dim sp1 As String = ""
        sp1 = Server.MapPath("~/Excel Files/") & myFile.FileName
        txtFileName.Text = myFile.FileName
        myFile.PostedFile.SaveAs(sp1)

        ' Read Excel File

        Dim oExcel As New Excel.Application
        Try
            oExcel.Workbooks.Open(sp1)
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Save as currunt Execel in Excel(.xlsx) Format');", True)
            Exit Sub
        End Try

        Dim oBook As Excel.Workbook
        Dim oSheet As Excel.Worksheet
        oBook = oExcel.ActiveWorkbook
        oSheet = oExcel.Worksheets(1)
        'Session("ExcelFile") = myFile
        'If oSheet.Name <> cboClass.Text & "_" & cboSection.Text & "_" & cboTerm.Text Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please select Excel File of class " & cboClass.Text & "-" & cboSection.Text & " and trem " & cboTerm.Text & "');", True)
        '    ReleaseExcel(oBook)
        '    myFile.Focus()
        '    Exit Sub
        'End If
        Dim i As Integer = 14
        While (1)
            i += 1
            If oSheet.Range("B" & i).Value = Nothing Then
                Exit While
            End If
        End While

        Dim RowNo As Integer = oSheet.Range("A" & i - 1).Value
        'Dim ColExcel As Array = ColArray()
        'Try
        'LstWrongEntry.Items.Clear()
        DeleteTmpFeeReject()
        'For Column = 2 To ColExcel.LongLength - 1
        For row = 15 To RowNo + 14
            Dim DepositDate As String = Trim(oSheet.Range("B" & row).Value)
            Dim Details As String = Trim(oSheet.Range("C" & row).Value)
            Dim Amount As Double = 0
            Dim Flag As Boolean = True
            Try
                Flag = False
                Amount = Trim(oSheet.Range("F" & row).Value)
            Catch ex As Exception
                Flag = True
            End Try
            Dim BranchID As String = ""
            Dim RegNo As String = ""
            Dim DepositMode As Integer = 0

            If ValidateDetail(Details) <> "" Or Flag = True Then
                AddTmpFeeReject(Trim(oSheet.Range("A" & row).Value), Trim(oSheet.Range("B" & row).Value), Trim(oSheet.Range("C" & row).Value), Trim(oSheet.Range("F" & row).Value))
                'LstWrongEntry.Items.Add(Trim(oSheet.Range("A" & row).Value) & "  " & Trim(oSheet.Range("B" & row).Value) & "  " & Trim(oSheet.Range("C" & row).Value) & "  Amount:" & Trim(oSheet.Range("F" & row).Value))
            Else
                'KAILAS/By Cash#ID6732013#KINJAL SI
                'BranchID/Mode#Regno#SName
                'Dim a As String() = Details.Split("#")
                'If a(0).Contains("/") = True Then
                '    BranchID = a(0).Split("/")(0)
                'Else
                '    BranchID = "Sanjay Place"
                'End If
                'If a(0).Contains("Cash") Then
                '    DepositMode = 1
                'Else
                '    DepositMode = 2
                'End If
                'If a(1).Contains("/") = False Then
                '    Dim tmp As Integer = a(1).Substring(2, a(1).Length - 6)
                '    Dim tmpyear As Integer = a(1).Substring(a(1).Length - 5, 4)
                '    RegNo = tmp.ToString("00000") & "/" & tmpyear
                'End If

            End If
        Next

        'Next
        'btnCheckExcel.Enabled = False
        GridView1.DataSource = SqlDataSource1
        GridView1.DataBind()
        btnImportFee.Enabled = True
        btnCheckExcel.Enabled = False

        ReleaseExcel(oBook)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Excel Validate Succesfully, Please Import Fee');", True)
        'Catch ex As Exception
        '    ReleaseExcel(oBook)
        'End Try
    End Sub
    Private Sub AddTmpFeeReject(SNo As String, Dated As String, Details As String, Amount As String)
        
       
       
       
        Dim sqlStr As String = "Insert into tmpFeeReject values ('" & SNo & "','" & Dated & "','" & Details & "','" & Amount & "',2)"
        
        
        ExecuteQuery_Update(SqlStr)
        
        
    End Sub
    Private Sub DeleteTmpFeeReject()
        
       
       
       
        Dim sqlStr As String = "Truncate table tmpFeeReject"
        
        
        ExecuteQuery_Update(SqlStr)
        
        
    End Sub
    Private Function ChangeDate(ByVal txtDate As String)
        'Dim issuedDate As String
        If txtDate.Contains("/") Then
            If txtDate.Split("/")(2).Length = 2 Then
                txtDate = "20" & txtDate.Split("/")(2) & "/" & txtDate.Split("/")(1) & "/" & txtDate.Split("/")(0)
            Else
                txtDate = txtDate.Split("/")(2) & "/" & txtDate.Split("/")(1) & "/" & txtDate.Split("/")(0)
            End If
        Else
            If txtDate.Split("-")(2).Length = 2 Then
                txtDate = "20" & txtDate.Split("-")(2) & "/" & txtDate.Split("-")(1) & "/" & txtDate.Split("-")(0)
            Else
                txtDate = txtDate.Split("-")(2) & "/" & txtDate.Split("-")(1) & "/" & txtDate.Split("-")(0)
            End If
        End If
        Return txtDate
    End Function
    Private Function GetSID(RegNo As String) As String

       
       
       
        Dim SID As Integer = 0
        Dim ClassID As Integer = 0
        Dim SecID As Integer = 0
        Dim SName As String = ""
        
        Dim sqlStr As String = "Select SID,ClassID,SecID,SName From vw_Student Where RegNo='" & RegNo & "' AND ASID=" & Request.Cookies("ASID").Value
        
        
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            SID = myReader(0)
            ClassID = myReader(1)
            SecID = myReader(2)
            SName = myReader(3)
        End While
        myReader.Close()
        
        
        Return SID & "/" & ClassID & "/" & SecID & "/" & SName
    End Function
    Private Sub ImportFee(PaymentModeID As Integer, RegNo As String, Dated As String, BranchID As String, ExcelAmount As Double, ChequeNo As String, SNo As String, Details As String)
        Dim SubmitAmount As Double = ExcelAmount

        Dim TempRv As String = GetSID(RegNo)
        Dim SID As Integer = TempRv.Split("/")(0)
        Dim ClassID As Integer = TempRv.Split("/")(1)
        Dim SecID As Integer = TempRv.Split("/")(2)
        Dim SName As String = TempRv.Split("/")(3)
        Dim locationId As Integer = GetStudentLocationID(SID)
        Dim ActualAmt As Double = GetStudentLocationAmount(locationId)
        
        Dim sqlStr As String = ""
        Dim DepositeAmt As Double = 0
        Dim BusFine As Double = 0
        If ExcelAmount > ActualAmt Then
            DepositeAmt = ActualAmt
            BusFine = ExcelAmount - DepositeAmt
        Else
            DepositeAmt = ExcelAmount
        End If
        'Dim DepositDate As String = Dated.Split("/")(2) & "/" & Dated.Split("/")(1) & "/" & Dated.Split("/")(0)
        Dim TermID As Integer = 0
        If SID > 0 Then
            'If CheckFeeImport(SID, Dated) = True Then
            'Else
           
           
           
            sqlStr = "Select Max(TermNo)+1 from BusFeeDeposite where SID=" & SID
            
            
            Try
                TermID = ExecuteQuery_ExecuteScalar(SqlStr)
            Catch ex As Exception
                TermID = 1
            End Try
            sqlStr = "Insert into BusFeeDeposite (SID,TermNo,DepositeDate,DepositMode,DepositeAmt,ActualAmt,DepositDetails,FineAmt,LocationID,BranchID) Values(" & _
            SID & "," & TermID & "," & _
            "'" & Dated & "'," & _
            PaymentModeID & ",'" & DepositeAmt & "','" & ActualAmt & "','','" & BusFine & "','" & locationId & "','" & BranchID & "')"

            
            
            ExecuteQuery_Update(SqlStr)

            Dim FeeDepositID As Integer = 0

            sqlStr = "Select MAX(BusFeeID) from BusFeeDeposite where SID=" & SID
            
            
            FeeDepositID = ExecuteQuery_ExecuteScalar(SqlStr)
            If PaymentModeID = 2 Then
                sqlStr = "INSERT Into BusFeeCheque(FeeDepositID,ChequeNo,isDishonoured) Values(" & _
                    "'" & FeeDepositID & "'," & _
                    "'" & ChequeNo & "'," & _
                                  "0)"
                
                
                ExecuteQuery_Update(SqlStr)
            End If

            
            
            
            Save_Log(PaymentModeID, RegNo, SName, SubmitAmount, ChequeNo, "INSERT")
            'End If
        Else
        AddTmpFeeReject(SNo, ChangeDate(Dated), Details, ExcelAmount)
        End If
        'ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Fee saved successfully...');", True)
    End Sub
    Private Sub Save_Log(Mode As Integer, RegNo As String, Sname As String, Amount As Double, ChequeNo As String, ByVal type As String)
       
       
       
        
        Dim log1 As String = ""
        Dim sqlStr As String = ""
        Dim Dated As String = Now.Date.ToString("dd/MM/yyyy")
        If (type.Contains("INSERT") = True) Then
            If Mode = 1 Then
                log1 = "Import Bus Fee of RegNo : " & RegNo & ", Name : " & Sname & ", Fee Sum : " & Amount & ", Payment Mode: Cash, Date : " & Dated
            Else
                log1 = "Import Bus Fee RegNo : " & RegNo & ", Name : " & Sname & ", Fee Sum : " & Amount & ", Cheque NO : " & ChequeNo & ", Date : " & Dated
            End If
        End If

        sqlStr = "Insert Into Event_log(logTime,EventType,Details,loginId,Visible) Values('" & System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "','" & type & "','" & log1 & "','" & Request.Cookies("UserID").Value & "','1')"
        
        
        ExecuteQuery_Update(SqlStr)
        
        
    End Sub
    Private Function GetLateFee(SID As Integer, TermID As Integer, ClassID As Integer, SecID As Integer)
       
       
       
        
        Dim sqlStr As String = ""
        sqlStr = "Select Count(*) From FeeDeposit Where isDeposit=1 AND SID=" & SID & " and TermID =" & TermID
        
        
        Dim FeeDeposit As Integer = 0
        FeeDeposit = ExecuteQuery_ExecuteScalar(SqlStr)
        Dim tmpNowDate As Date = Now.Date
        Dim LastDate As Date = Now.Date
        Dim LateFeeAmount As Double = 0, LateFeeType As Integer = 0, ProcessingMethod As Integer = 0

        sqlStr = "Select LateFeeAmount From vwFeeDueConfig Where ClassID='" & ClassID & "' and SecName='" & SecID & "' and TermID=" & TermID & " AND ASID=" & Request.Cookies("ASID").Value
        If FeeDeposit > 0 Then
            sqlStr += " And LastDate<= (Select Max(DepositDate) From FeeDeposit Where isDeposit=1 AND SID=" & SID & " and TermID =" & TermID & ")"
        Else
            sqlStr += " And LastDate<='" & tmpNowDate & "'"
        End If

        
        LateFeeAmount = ExecuteQuery_ExecuteScalar(SqlStr)
        
        
        Return LateFeeAmount
    End Function
    Public Shared Function GetFeeConfig(ByVal ASID As Integer, ByVal ClassID As String, ByVal SecID As String, ByVal TermID As Integer, AdmisionFeeID As Integer) As DataSet

       
       
       
        
        Dim SqlStr As String = ""

        SqlStr = "Select FeeTypeID,FeeAmount From FeeConfig Where ASID=" & ASID & " AND " & _
        " ClassID='" & ClassID & "' AND SecID='" & SecID & "' And TermNo = " & TermID & " and FeeTypeID<>" & AdmisionFeeID & " order by FeeAmount DESC"
        Dim ds As New DataSet
        ds = ExecuteQuery_DataSet(SqlStr, "t")

        Return ds

    End Function
    Public Function ColArray() As Array
        Dim SubjectLimit As Integer = 0
        If Request.QueryString("Group") = 0 Then
            SubjectLimit = 29
        ElseIf Request.QueryString("Group") = 1 Then
            SubjectLimit = 75
        End If

        Dim array(SubjectLimit) As String
        For i = 0 To 25
            array(i) = Chr(i + 65)
        Next
        Try
            For i = 26 To 51
                array(i) = "A" & Chr(i + 39)
            Next
        Catch ex As Exception

        End Try
        Try
            For i = 52 To 75
                array(i) = "B" & Chr(i + 13)
            Next
        Catch ex As Exception

        End Try
        Return array
    End Function
    Public Sub ReleaseExcel(oBook As Excel.Workbook)
        Dim proc As System.Diagnostics.Process
        oBook = Nothing
        GC.Collect()
        For Each proc In System.Diagnostics.Process.GetProcessesByName("EXCEL")
            proc.Kill()
        Next
    End Sub
    Private Function ValidateDetail(Detail As String) As String
        Dim rv As String = ""
        Dim BranchID As String = ""
        Dim PaymentMode As String = ""
        Dim RegNo As String = ""
        Dim SName As String = ""
        Dim DetailArr As String() = Detail.Split("#")
        If Detail.Contains("#") = False Then
            rv = "Invalid"
        End If
        Return rv
    End Function
    Public Function GetNoOfStudents() As Integer
       
       
       
        'Dim sqlStr As String = ""
        Dim sqlStr As String = "Select Count(*) from vw_Student  where ClassName='" & cboClass.Text & "' and SecName ='" & cboSection.Text & "' and ASID=" & Request.Cookies("ASID").Value & " And StatusName='" & cboStatus.Text & "'"
        
        
        
        Dim rv As Integer = ExecuteQuery_ExecuteScalar(SqlStr)
        
        
        
        Return rv
    End Function
    Public Function CheckFeeImport(SID As Integer, depositDate As String) As Boolean
       
       
       
        'Dim sqlStr As String = ""
        Dim sqlStr As String = "Select Count(*) from BusFeeDeposite  where SID='" & SID & "' and DepositeDate ='" & depositDate & "'"
        ' & "' and ASID=" & Request.Cookies("ASID").Value & " And StatusName='" & cboStatus.Text & "'"
        
        
        
        Dim rv As Integer = ExecuteQuery_ExecuteScalar(SqlStr)
        
        
        
        If rv > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Protected Sub btnImportMarks_Click(sender As Object, e As EventArgs) Handles btnImportFee.Click
        Dim sp1 As String = ""
        sp1 = Server.MapPath("~/Excel Files/") & txtFileName.Text
        ' & ".xlsx"
        'txtFileName.Text = myFile.FileName
        'myFile.PostedFile.SaveAs(sp1)
        ' Read Excel File

        Dim oExcel As New Excel.Application
        Try
            oExcel.Workbooks.Open(sp1)
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Save as currunt Execel in Excel(.xlsx) Format');", True)
            Exit Sub
        End Try

        Dim oBook As Excel.Workbook
        Dim oSheet As Excel.Worksheet
        oBook = oExcel.ActiveWorkbook
        oSheet = oExcel.Worksheets(1)

        Dim i As Integer = 14
        While (1)
            i += 1
            If oSheet.Range("B" & i).Value = Nothing Then
                Exit While
            End If
        End While

        Dim RowNo As Integer = i - 1
        'oSheet.Range("A" & i - 1).Value
        'Dim ColExcel As Array = ColArray()
        'Try
        'LstWrongEntry.Items.Clear()
        'For Column = 2 To ColExcel.LongLength - 1
        For row = 15 To RowNo
            Dim SNo As String = Trim(oSheet.Range("A" & row).Value)
            Dim DepositDate As String = ChangeDate(Trim(oSheet.Range("B" & row).Value))
            Dim Details As String = Trim(oSheet.Range("C" & row).Value)
            Dim ChequeNo As String = Trim(oSheet.Range("D" & row).Value)
            Dim Amount As Double = 0
            Dim Flag As Boolean = True
            Try
                Flag = False
                Amount = Trim(oSheet.Range("F" & row).Value)
            Catch ex As Exception
                Flag = True
            End Try
            Dim BranchID As String = ""
            Dim RegNo As String = ""
            Dim DepositMode As Integer = 0

            If ValidateDetail(Details) <> "" Or Flag = True Then
                'LstWrongEntry.Items.Add(Trim(oSheet.Range("A" & row).Value) & "  " & Trim(oSheet.Range("B" & row).Value) & "  " & Trim(oSheet.Range("C" & row).Value) & "  Amount:" & Trim(oSheet.Range("F" & row).Value))
            Else
                'KAILAS/By Cash#ID6732013#KINJAL SI
                'BranchID/Mode#Regno#SName
                Dim a As String() = Details.Split("#")
                If a(0).Contains("/") = True Then
                    BranchID = a(0).Split("/")(0)
                Else
                    BranchID = "Sanjay Place"
                End If
                If a(0).Contains("Cash") Then
                    DepositMode = 1
                Else
                    DepositMode = 2
                End If
                If a(1).Contains("/") = False Then
                    Dim tmp As Integer = a(1).Substring(2, a(1).Length - 6)
                    Dim tmpyear As Integer = a(1).Substring(a(1).Length - 4, 4)
                    RegNo = tmp.ToString("00000") & "/" & tmpyear
                End If

            End If

            ImportFee(DepositMode, RegNo, DepositDate, BranchID, Amount, ChequeNo, SNo, Details)
        Next
        GridView1.DataSource = SqlDataSource1
        GridView1.DataBind()
        InitControls()
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Fee Imports Succesfully');", True)
    End Sub
    Protected Sub btnPrintExcel_Click(sender As Object, e As EventArgs) Handles btnPrintExcel.Click

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
End Class