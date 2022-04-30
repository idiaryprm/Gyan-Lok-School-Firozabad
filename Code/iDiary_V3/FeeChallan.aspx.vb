Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary_Fee.CLS_iDiary_Fee
Imports Microsoft.Reporting.WebForms

Public Class FeeChallan
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
                Response.Redirect("~/idiary/Login.aspx")
            End If

        End Try

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cookies("ActiveTab").Value = 3
        If IsPostBack = False Then
            InitControls()
            Dim FeeTypes() As String = GetFeeTypeConfigID().Split("$")
            txtAdmissionFeeID.Text = FeeTypes(0)
            txtLateFeeID.Text = FeeTypes(1)
            txtConveyanceFeeID.Text = FeeTypes(2)
            txtTutionFeeID.Text = FeeTypes(3)
        Else
            'For Grid View Printing. Must have a blank HTM Page (gview.htm)
            'If ViewState("myTable") = True Then
            If cboTerm.Text <> "-" And cboTerm.Text <> "" And txtRegno.Text <> "" And cboBusTerm.Text <> "All" Then
                CreateTable()
            End If
        End If
    End Sub

    Private Sub InitControls()
        txtChallanDate.Text = Now.Date.ToString("dd/MM/yyyy")
        LoadClass(cboClass, 0)
        cboClass.Items.Add("ALL")
        cboSection.Items.Clear()
        LoadMasterInfo(10, cboStatus)
        LoadFeeTerms(cboBusTerm, 1, "BusFee")
        txtDueDate.Text = Now.Date.ToString("dd/MM/yyyy")
        txtRegno.Focus()
        'cboClass.Focus()
    End Sub

    Protected Sub cboClass_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboClass.SelectedIndexChanged
        LoadClassSection("", cboClass.Text, cboSection)
        cboSection.Items.Add("ALL")
        cboClass.Focus()
    End Sub
    Private Function CheckRegNo(Regno As String) As Boolean
       
       
       

        Dim sqlstr As String = ""
        

        sqlstr = "Select Count(*) From vw_student where regno='" & Regno & "'"
        
        
        Dim rv As Integer = 0
        rv = ExecuteQuery_ExecuteScalar(SqlStr)
        
        
        If rv > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Function GetDueDate(FeeGroupID As Integer, TermID As Integer) As String
       
       
       

        Dim sqlstr As String = ""
        

        Dim LastDate As String = ""
        Dim LateFeeAmount As Double = 0, LateFeeType As Integer = 0, ProcessingMethod As Integer = 0
        sqlstr = "Select  Top(1) LastDate, LateFeeAmount, LateFeeType, ProcessingMethod From vwFeeDueConfig Where TermID=" & TermID & " and FeeGroupID=" & FeeGroupID & " AND ASID=" & Request.Cookies("ASID").Value
       
        sqlstr += " order by LastDate"
        
        
        Dim DueConfigReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While DueConfigReader.Read
            LastDate = DueConfigReader(0)
            LateFeeAmount = DueConfigReader(1)
            LateFeeType = DueConfigReader(2)
            ProcessingMethod = DueConfigReader(3)
        End While
        DueConfigReader.Close()
        
        
        If LastDate = "" Then
            Return "NA"
        Else
            Return LastDate
        End If
    End Function
    Private Sub PrepareReport(FeeChallanId As String)
        Dim FeeGroupId As Integer = txtFeeGroupID.Text
        Dim TermID As Integer = LoadTermID(cboTerm.Text, FeeGroupId)
        Dim sqlStr As String = ""

        sqlStr = "Select * from vw_FeeChallanIndivisual where FeeChallanAmount>0  and FeeChallanID in (" & FeeChallanId & ")"
        Dim DueDate As String = GetDueDate(FeeGroupId, TermID)

        If optIndivisual.Checked = True Then
            sqlStr &= " and RegNo='" & txtRegno.Text & "'"
        End If

        Dim ds As New DataSet
        ds = ExecuteQuery_DataSet(sqlStr, "tbl")
        Dim rds As ReportDataSource = New ReportDataSource()
        rds.Name = "DataSet1" ' Change to what you will be using when creating an objectdatasource
        rds.Value = ds.Tables(0)
        With ReportViewer1   ' Name of the report control on the form
            .Reset()
            .ProcessingMode = ProcessingMode.Local
            .LocalReport.DataSources.Clear()
            .Visible = True
            .LocalReport.ReportPath = "Report/rptFeeChallanIndivisual.rdlc"
            .LocalReport.DataSources.Add(rds)
        End With
        Dim params(1) As Microsoft.Reporting.WebForms.ReportParameter

        params(0) = New Microsoft.Reporting.WebForms.ReportParameter("Dated", txtChallanDate.Text, True)
        params(1) = New Microsoft.Reporting.WebForms.ReportParameter("DueDate", GetDueDate(FeeGroupId, TermID), True)
        Me.ReportViewer1.LocalReport.SetParameters(params)
        ReportViewer1.Visible = True
        ReportViewer1.LocalReport.Refresh()
    End Sub
    Protected Sub btnViewSummaryList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnViewChallan.Click
        If cboTerm.Text = "" Then
            lblStatus.Text = "Invalid Fee Term..."
            cboTerm.Focus()
            Exit Sub
        End If
        If cboBusTerm.Text = "All" Then
            lblStatus.Text = "Invalid Bus Fee Term..."
            cboBusTerm.Focus()
            Exit Sub
        End If
        If txtChallanDate.Text = "" Then
            lblStatus.Text = "Invalid Challan Date..."
            txtChallanDate.Focus()
            Exit Sub
        End If
        If optIndivisual.Checked = True Then
            If txtRegno.Text = "" Or CheckRegNo(txtRegno.Text) = False Then
                lblStatus.Text = "Invalid Reg No...."
                txtRegno.Focus()
                Exit Sub
            End If
        End If
        If optClassWise.Checked = True Then
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
            If cboStatus.Text = "" Then
                lblStatus.Text = "Invalid Status..."
                cboStatus.Focus()
                Exit Sub
            End If
        End If
        If txtDueDate.Text = "" Then
            lblStatus.Text = "Invalid Due Date..."
            txtDueDate.Focus()
            Exit Sub
        End If
        Dim DueDate As String = ""
        Try
            DueDate = txtDueDate.Text.Split("/")(2) & "/" & txtDueDate.Text.Split("/")(1) & "/" & txtDueDate.Text.Split("/")(0)
        Catch ex As Exception
            lblStatus.Text = "Invalid Due Date..."
            txtDueDate.Focus()
            Exit Sub
        End Try
        lblStatus.Text = ""



        Dim FeeGroupId As Integer = 0
        If optIndivisual.Checked = True Then
            FeeGroupId = txtFeeGroupID.Text
        End If

        Dim TermID As Integer = LoadTermID(cboTerm.Text, FeeGroupId)

        Dim sqlStr As String = ""
        Dim ChallanDate As String = txtChallanDate.Text.Split("/")(2) & "/" & txtChallanDate.Text.Split("/")(1) & "/" & txtChallanDate.Text.Split("/")(0)
        Dim FeeChallanID As Integer = 0
        Dim FeeChallanIDList As String = ""


        If optIndivisual.Checked = True Then
            sqlStr = "Insert into FeeChallan(SID,TermID,ChallanDate,GenerationDate,Remarks,isDeposit,DueDate) Values(" & _
                               Val(txtSID.Text) & "," & TermID & ",'" & ChallanDate & "','" & Now.Date.ToString("yyyy/MM/dd") & "','',0,'" & DueDate & "')"


            ExecuteQuery_Update(sqlStr)
            sqlStr = "Select MAX(FeeChallanID) from FeeChallan"


            FeeChallanID = ExecuteQuery_ExecuteScalar(sqlStr)
            FeeChallanIDList &= FeeChallanID & ","

            sqlStr = "Delete From FeeChallanDetails Where FeeChallanID=" & FeeChallanID


            ExecuteQuery_Update(sqlStr)


            For i = 1 To myTable.Rows.Count - 1

                Dim ChallanAmount As Double = Val(CType(myTable.FindControl("txtD" & i), TextBox).Text)
                Dim myFeeTypeID As String = myTable.Rows(i).Cells(0).Text   'Get FeeTypeID From Table
                Dim FeeActualAmount As Double = 0
                FeeActualAmount = GetFeeConfigForFeeHead(Request.Cookies("ASID").Value, Val(txtFeeGroupID.Text), myFeeTypeID, TermID)

                sqlStr = "Insert into FeeChallanDetails (FeeChallanID, FeeTypeID, FeeChallanAmount, FeeWaveOffAmount, FeeConfigAmount) Values(" & _
                FeeChallanID & "," & _
                myFeeTypeID & "," & _
                ChallanAmount & "," & _
                FeeActualAmount - ChallanAmount & "," & _
                FeeActualAmount & ")"



                ExecuteQuery_Update(sqlStr)
            Next
        Else
            Dim lstSID As New ListBox
            Dim lstFeeConfigType As New ListBox
            Dim lstFeeGroupID As New ListBox
            Dim lstAdminDate As New ListBox
            lstSID.Items.Clear()
            lstFeeGroupID.Items.Clear()
            lstAdminDate.Items.Clear()


            sqlStr = "Select Distinct SID,FeeGroupID,AdmissionDate,FeeConfigType from vw_Student where StatusName='" & cboStatus.Text & "' AND ASID=" & Request.Cookies("ASID").Value
            'FeeTypeID in (" & TmpFeeHeadID & ") and 
            If optClassWise.Checked = True Then
                If cboClass.Text <> "ALL" Then
                    sqlStr &= " and ClassName='" & cboClass.Text & "'"
                End If
                If cboSection.Text <> "ALL" Then
                    sqlStr &= " and SecName='" & cboSection.Text & "'"
                End If
            End If
            'sqlStr &= " Order by DisplayOrder"


            Dim SIDReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While SIDReader.Read
                lstSID.Items.Add(SIDReader(0))
                lstFeeGroupID.Items.Add(SIDReader(1))
                Try
                    lstAdminDate.Items.Add(SIDReader(2))
                Catch ex As Exception
                    lstAdminDate.Items.Add("2015/01/01")
                End Try
                Try
                    lstFeeConfigType.Items.Add(SIDReader(3))
                Catch ex As Exception
                    lstFeeConfigType.Items.Add("0")
                End Try
            End While
            SIDReader.Close()

            For i = 0 To lstSID.Items.Count - 1
                TermID = LoadTermID(cboTerm.Text, Val(lstFeeGroupID.Items(i).Text))
                Dim adminDate As Date = lstAdminDate.Items(i).Text
                Dim Duedate1 As Date = adminDate
                Try
                    Duedate1 = GetDueDate(Val(lstFeeGroupID.Items(i).Text), TermID)
                Catch ex As Exception

                End Try
                Dim TmpDueDate As String = ""
                If Duedate1 > adminDate.AddDays(9) Then
                    TmpDueDate = Duedate1.ToString("yyyy/MM/dd")
                Else
                    TmpDueDate = adminDate.AddDays(9).ToString("yyyy/MM/dd")
                End If
                sqlStr = "Insert into FeeChallan(SID,TermID,ChallanDate,GenerationDate,Remarks,isDeposit,DueDate) Values(" & _
                                  Val(lstSID.Items(i).Text) & "," & TermID & ",'" & ChallanDate & "','" & Now.Date.ToString("yyyy/MM/dd") & "','',0,'" & TmpDueDate & "')"


                ExecuteQuery_Update(sqlStr)
                sqlStr = "Select MAX(FeeChallanID) from FeeChallan"


                FeeChallanID = ExecuteQuery_ExecuteScalar(sqlStr)
                FeeChallanIDList &= FeeChallanID & ","

                sqlStr = "Delete From FeeChallanDetails Where FeeChallanID=" & FeeChallanID


                ExecuteQuery_Update(sqlStr)
                Dim lstFeeTypeID As New ListBox
                lstFeeTypeID.Items.Clear()

                sqlStr = "Select FeeTypeID, FeeTypeName From FeeTypes"


                Dim SIDReaderTmp As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
                While SIDReaderTmp.Read
                    lstFeeTypeID.Items.Add(SIDReaderTmp("FeeTypeID"))
                End While
                SIDReaderTmp.Close()

                Dim ConcessionAmount As Double = 0
                For j = 0 To lstFeeTypeID.Items.Count - 1

                    Dim ChallanAmount As Double = 0
                    If Val(lstFeeConfigType.Items(i).Text) = 0 Then
                        ChallanAmount = GetFeeConfigForFeeHead(Request.Cookies("ASID").Value, Val(lstFeeGroupID.Items(i).Text), Val(lstFeeTypeID.Items(j).Text), TermID)
                    Else
                        ChallanAmount = GetFeeConfigForFeeHead(Request.Cookies("ASID").Value, 0, Val(lstFeeTypeID.Items(j).Text), TermID, "", Val(lstSID.Items(i).Text))
                    End If


                    Dim myFeeTypeID As String = Val(lstFeeTypeID.Items(j).Text) 'Get FeeTypeID From Table
                    Dim FeeActualAmount As Double = ChallanAmount

                    If myFeeTypeID = txtConveyanceFeeID.Text Then
                        FeeActualAmount = GetBusActualAmt(Val(lstSID.Items(i).Text), cboBusTerm.Text)
                        ChallanAmount = FeeActualAmount
                    Else

                    End If
                    'Process Concession
                    Dim MonthID As String = GetMonthID(Val(txtFeeGroupID.Text), LoadTermID(cboTerm.Text, FeeGroupId))
                    Dim ConcessionAmounttmp As Double = GetConcessionAmount(txtSID.Text, myFeeTypeID, MonthID)
                    FeeActualAmount = FeeActualAmount - ConcessionAmounttmp
                    ConcessionAmount += ConcessionAmounttmp

                    ChallanAmount = FeeActualAmount

                    Dim LateFeeAmount As Double = 0
                    Dim OldDueAmount As Double = 0

                    If myFeeTypeID = txtLateFeeID.Text Then
                        Dim OldDueTerm As Integer = 0
                        OldDueTerm = LoadTermID(cboTerm.Text, FeeGroupId)
                        ChallanAmount = GetDueAmountForTerm(Request.Cookies("ASID").Value, LoadTermID(cboTerm.Text, FeeGroupId), "", "", Val(lstSID.Items(i).Text))
                        FeeActualAmount = ChallanAmount
                    End If

                    sqlStr = "Insert into FeeChallanDetails (FeeChallanID, FeeTypeID, FeeChallanAmount, FeeWaveOffAmount, FeeConfigAmount) Values(" & _
                    FeeChallanID & "," & _
                    myFeeTypeID & "," & _
                    ChallanAmount & "," & _
                    FeeActualAmount - ChallanAmount & "," & _
                    FeeActualAmount & ")"




                    ExecuteQuery_Update(sqlStr)


                Next
                'sqlStr = "Insert into FeeChallanDetails (FeeChallanID, FeeTypeID, FeeChallanAmount, FeeWaveOffAmount, FeeConfigAmount) Values(" & _
                '   FeeChallanID & "," & _
                '   txtConcessionFeeID.text & "," & _
                '   ConcessionAmount & "," & _
                '   ConcessionAmount & "," & _
                '   ConcessionAmount & ")"

                '
                '
                'ExecuteQuery_Update(SqlStr)
            Next
        End If



        'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('HI');", True)
        'ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Fee saved successfully...');", True)
        FeeChallanIDList = FeeChallanIDList.Substring(0, FeeChallanIDList.Length - 1)
        PrepareReport(FeeChallanIDList)
    End Sub
    Private Sub GetStudentDetail(RegNo As String)
        Dim sqlStr As String = ""
       
       
       

        
        sqlStr = "Select SID,FeeGroupID,ClassName,SecName,ASID From vw_Student where Regno='" & RegNo & "' and ASID=" & Request.Cookies("ASID").Value
        
        
        Dim DueConfigReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While DueConfigReader.Read
            txtSID.Text = DueConfigReader("SID")
            txtFeeGroupID.Text = DueConfigReader("FeeGroupID")

            'txtClassName.Text = DueConfigReader("ClassName")
            'txtSecName.Text = DueConfigReader("SecName")
            'txtASID.Text = DueConfigReader("ASID")
            'FlagFine = True
        End While
        DueConfigReader.Close()

        
        
    End Sub
    Public Shared Function LoadClass(ByRef myCbo As DropDownList, FeeGroupID As Integer) As Integer

        'Dim sqlStr As String = "Select Distinct ClassName From vw_ClassStudent where  FeeGroupID=" & FeeGroupID & " Order by ClassName"
        Dim sqlStr As String = "Select ClassName From Classes Order by DisplayOrder"
       
       
       

        
        
        

        Dim myReader As System.Data.SqlClient.SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        myCbo.Items.Clear()
        myCbo.Items.Add("")

        While myReader.Read
            myCbo.Items.Add(myReader(0))
        End While
        myReader.Close()

        
        

        Return 0
    End Function

    
    Private Sub GetFeeGroup(RegNo As String)
        Try
            Dim sqlStr As String = "Select FeeGroupID,FeeGroupName,SID,SName,FName,ClassName,SecName,AdmissionYear,FeeConfigType From vw_Student where  Regno='" & RegNo & "' and ASID=" & Request.Cookies("ASID").Value & " Order by DisplayOrder"
           
           
           

            
            
            
            Dim FeeGroupId As String = ""
            Dim myReader As System.Data.SqlClient.SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                FeeGroupId = myReader(0).ToString
                txtFeeGroupID.Text = FeeGroupId
                txtSID.Text = myReader("SID").ToString
                lblSName.Text = myReader("SName").ToString
                lblFather.Text = myReader("FName").ToString
                lblClass.Text = myReader("ClassName").ToString & "-" & myReader("SecName").ToString
                Dim a As Date = myReader("AdmissionYear").ToString
                txtAdminDate.Text = a.ToString("yyyy/MM/dd")
                Try
                    txtfeeconfigtype.text = myReader("FeeConfigType").ToString
                Catch ex As Exception
                    txtfeeconfigtype.text = "0"
                End Try
            End While
            myReader.Close()

            
            
            LoadClass(cboClass, FeeGroupId)
            cboClass.Items.Add("ALL")
            LoadFeeTerms(cboTerm, FeeGroupId)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub CreateTable()

        myTable.Rows.Clear()

        Dim tr1 As New TableRow

        Dim td10 As New TableCell
        td10.Text = "<B>Fee ID</B>"
        td10.HorizontalAlign = HorizontalAlign.Center
        tr1.Cells.Add(td10)

        Dim td11 As New TableCell
        td11.Text = "<B>Fee Type</B>"
        td11.HorizontalAlign = HorizontalAlign.Center
        tr1.Cells.Add(td11)

        Dim td12 As New TableCell
        td12.Text = "<B>Actual Amount</B>"
        td12.HorizontalAlign = HorizontalAlign.Center
        tr1.Cells.Add(td12)

        Dim td13 As New TableCell
        td13.Text = "<B>Amount to be Deposited</B>"
        td13.HorizontalAlign = HorizontalAlign.Center
        tr1.Cells.Add(td13)

        myTable.Rows.Add(tr1)

        Dim sqlStr As String = ""
        Dim myTxtBoxNumber As Integer = 1

       
       
       

        

        'Process Late Fee Amount and Type
        Dim lstLateFeeAmount As New ListBox
        Dim t As Integer = 0

        lstLateFeeAmount.Items.Add((GetDueAmountForTerm(Request.Cookies("ASID").Value, LoadTermID(cboTerm.Text, Val(txtFeeGroupID.Text)), "", txtRegno.Text)))
        sqlStr = "Select FeeTypeID, FeeTypeName From FeeTypes"
        
        
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)

        While myReader.Read
            Dim trx As New TableRow

            Dim tdx0 As New TableCell
            tdx0.Text = myReader(0)
            tdx0.HorizontalAlign = HorizontalAlign.Center
            trx.Cells.Add(tdx0)

            Dim tdx1 As New TableCell
            tdx1.Text = myReader(1)
            tdx1.HorizontalAlign = HorizontalAlign.Center
            trx.Cells.Add(tdx1)

            Dim txtAAmount As New Label()
            txtAAmount.ID = "txtA" & myTxtBoxNumber
            txtAAmount.Width = 100
            Dim tdx2 As New TableCell
            tdx2.Controls.Add(txtAAmount)
            tdx2.HorizontalAlign = HorizontalAlign.Center
            trx.Cells.Add(tdx2)

            Dim txtAmount As New TextBox()
            txtAmount.ID = "txtD" & myTxtBoxNumber
            txtAmount.Width = 100
            txtAmount.Attributes.Add("onchange", "javascript: ShowTotal();")
            Dim tdx3 As New TableCell
            tdx3.Controls.Add(txtAmount)
            tdx3.HorizontalAlign = HorizontalAlign.Center
            trx.Cells.Add(tdx3)

            myTable.Rows.Add(trx)

            myTxtBoxNumber += 1
        End While
        myReader.Close()
        If txtFeeConfigType.Text = "0" Then
            'Retrieve Concession Fee Type Config(Given Additionally During Fee Deposit)

            Dim i As Integer = 0, j As Integer = 0, myCount As Integer = 0
            Dim DefaultFeeTotal As Double = 0, OverallConcessionAmount As Double = 0

            For i = 1 To myTable.Rows.Count - 1
                Dim myFeeTypeID As String = myTable.Rows(i).Cells(0).Text   'Get FeeTypeID From Table
                Dim myFeeAmount As Double = 0

                myFeeAmount += GetFeeConfigForFeeHead(Request.Cookies("ASID").Value, Val(txtFeeGroupID.Text), myFeeTypeID, LoadTermID(cboTerm.Text, Val(txtFeeGroupID.Text)))

                If myFeeTypeID = txtAdmissionFeeID.Text And AdmissionFeeApplicable(txtSID.Text, Request.Cookies("ASID").Value) = False Then
                    myFeeAmount = 0
                End If
                If myFeeTypeID = txtConveyanceFeeID.Text Then
                    Dim ConveyanceAmount As Double = 0
                    ConveyanceAmount = GetBusActualAmt(txtSID.Text, cboBusTerm.Text)
                    myFeeAmount = ConveyanceAmount
                End If
                CType(myTable.FindControl("txtA" & i), Label).Text = myFeeAmount
                CType(myTable.FindControl("txtD" & i), TextBox).Text = myFeeAmount

                'Process Concession
                Dim MonthID As String = GetMonthID(Val(txtFeeGroupID.Text), LoadTermID(cboTerm.Text, Val(txtFeeGroupID.Text)))
                Dim ConcessionAmount As Double = GetConcessionAmount(txtSID.Text, myFeeTypeID, MonthID)
                myFeeAmount = myFeeAmount - ConcessionAmount

                CType(myTable.FindControl("txtD" & i), TextBox).Text = myFeeAmount

                DefaultFeeTotal += myFeeAmount
                OverallConcessionAmount += ConcessionAmount

                'Process Late Fee Amount
                Dim LateFeeAmount As Double = 0
                Dim OldDueAmount As Double = 0
                For t = 0 To lstLateFeeAmount.Items.Count - 1
                    If myFeeTypeID = txtLateFeeID.Text Then
                        Dim OldDueTerm As Integer = 0
                        OldDueTerm = LoadTermID(cboTerm.Text, Val(txtFeeGroupID.Text))
                        OldDueAmount = GetDueAmountForTerm(Request.Cookies("ASID").Value, OldDueTerm, "", "", txtSID.Text)
                        LateFeeAmount += lstLateFeeAmount.Items(t).Text
                        CType(myTable.FindControl("txtD" & i), TextBox).Text = LateFeeAmount
                    End If
                Next
            Next
            'For i = 1 To myTable.Rows.Count - 1
            '    Dim myFeeTypeID As String = myTable.Rows(i).Cells(0).Text
            '    If myFeeTypeID = txtConcessionFeeTypeID Then
            '        CType(myTable.FindControl("txtD" & i), TextBox).Text = OverallConcessionAmount
            '    End If
            'Next
        Else
            For i = 1 To myTable.Rows.Count - 1
                Dim myFeeTypeID As String = myTable.Rows(i).Cells(0).Text   'Get FeeTypeID From Table
                Dim myFeeAmount As Double = 0

                If myFeeTypeID = txtConveyanceFeeID.Text Then
                    Dim ConveyanceAmount As Double = 0
                    ConveyanceAmount = GetBusActualAmt(txtSID.Text, cboBusTerm.Text)
                    'GetBusDueAmountForTerm(Request.Cookies("ASID").Value, cboBusTerm.Text, txtSID.Text, Now.Date) + 
                    myFeeAmount = ConveyanceAmount
                Else
                    myFeeAmount = GetFeeConfigForFeeHead(Request.Cookies("ASID").Value, 0, myFeeTypeID, LoadTermID(cboTerm.Text, Val(txtFeeGroupID.Text)), "", txtSID.Text)
                    'GetFeeConfigForFeeHeadStudent(txtSID.Text, myFeeTypeID, LoadTermID(cboTerm.Text, Val(txtFeeGroupID.Text)))
                    'CType(myTable.FindControl("txtD" & i), TextBox).Text = ConveyanceAmount
                End If
                CType(myTable.FindControl("txtA" & i), Label).Text = myFeeAmount
                CType(myTable.FindControl("txtD" & i), TextBox).Text = myFeeAmount
                'Process Late Fee Amount
                Dim LateFeeAmount As Double = 0
                Dim OldDueAmount As Double = 0
                For t = 0 To lstLateFeeAmount.Items.Count - 1
                    If myFeeTypeID = txtLateFeeID.Text Then
                        Dim OldDueTerm As Integer = 0
                        OldDueTerm = LoadTermID(cboTerm.Text, Val(txtFeeGroupID.Text))
                        OldDueAmount = +FeeDeposit_PastDuesConsideration(txtSID.Text, OldDueTerm, txtFeeGroupID.Text, myFeeTypeID, Request.Cookies("ASID").Value)
                        LateFeeAmount += lstLateFeeAmount.Items(t).Text
                        CType(myTable.FindControl("txtD" & i), TextBox).Text = LateFeeAmount
                    End If
                Next
                'If OldDueAmount > 0 Then
                '    lblFeeDue.Text = "Due Amount: " & OldDueAmount
                'Else
                '    lblFeeDue.Text = ""
                'End If
                'If chkPast.Checked = True Then
                '    CType(myTable.FindControl("txtD" & i), TextBox).Text = Val(CType(myTable.FindControl("txtD" & i), TextBox).Text) + OldDueAmount
                'End If
                '
            Next
        End If


        
        


        'If chkPast.Checked = True Then
        '-----Get Old Dues------


        'Display Fee Total
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "MyKey", "ShowTotal();", True)
        'myTable.EnableViewState = True
        'ViewState("myTable") = True
    End Sub

    Protected Sub txtRegno_TextChanged(sender As Object, e As EventArgs) Handles txtRegno.TextChanged
        If txtRegno.Text <> "" Then
            GetFeeGroup(txtRegno.Text)
        End If
    End Sub

    Protected Sub btnRegNext_Click(sender As Object, e As EventArgs) Handles btnRegNext.Click
        'If cboGroup.Text = "" Then
        '    lblStatus.Text = "Invalid Fee Group..."
        '    cboGroup.Focus()
        '    Exit Sub
        'End If
        If cboTerm.Text = "" Then
            lblStatus.Text = "Invalid Fee Term..."
            cboTerm.Focus()
            Exit Sub
        End If
        If cboBusTerm.Text = "All" Then
            lblStatus.Text = "Invalid Bus Fee Term..."
            cboBusTerm.Focus()
            Exit Sub
        End If
        If txtChallanDate.Text = "" Then
            lblStatus.Text = "Invalid Challan Date..."
            txtChallanDate.Focus()
            Exit Sub
        End If
        If optIndivisual.Checked = True Then
            If txtRegno.Text = "" Or CheckRegNo(txtRegno.Text) = False Then
                lblStatus.Text = "Invalid Reg No...."
                txtRegno.Focus()
                Exit Sub
            End If
        End If
        Dim FeeGroupId As Integer = txtFeeGroupID.Text
        'FindMasterID(60, cboGroup.Text)
        Dim TermID As Integer = LoadTermID(cboTerm.Text, FeeGroupId)

        Dim adminDate As Date = txtAdminDate.Text
        Dim Duedate As Date = adminDate
        Try
            Duedate = GetDueDate(FeeGroupId, TermID)
        Catch ex As Exception

        End Try
        If Duedate > adminDate.AddDays(9) Then
            txtDueDate.Text = Duedate.ToString("dd/MM/yyyy")
        Else
            txtDueDate.Text = adminDate.AddDays(9).ToString("dd/MM/yyyy")
        End If
        CreateTable()
    End Sub
    Protected Sub GridView2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView2.SelectedIndexChanged
        txtRegno.Text = GridView2.SelectedRow.Cells(1).Text
        GetFeeGroup(txtRegno.Text)
        GridView2.Visible = False
    End Sub
    Protected Sub btnNameSearch_Click1(sender As Object, e As EventArgs) Handles btnNameSearch.Click
        SqlDataSource2.SelectCommand = "SELECT RegNo, SName, ClassName, SecName FROM vw_Student WHERE ASID = " & Request.Cookies("ASID").Value & " AND SName Like '%" & txtName.Text & "%'"
        GridView2.DataBind()
        GridView2.Visible = True

    End Sub
End Class

