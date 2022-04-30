Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary_Fee.CLS_iDiary_Fee

Partial Class Parent_FeeHistory
    Inherits System.Web.UI.Page
    Dim FeeConfigType As Boolean = False
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("SID").Value <> Nothing Then
                'Allow
            Else
                Response.Redirect("ParentLogin.aspx", False)
            End If
        Catch ex As Exception
            Response.Redirect("ParentLogin.aspx")
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
    End Sub

    Private Sub InitControls()
        txtSID.Text = Request.Cookies("SID").Value
        GetStudentDetail()
        LoadMasterInfo(1, cboSession)   ''Load Academic Sessions
        cboSession.Focus()
        'GetDefaultSession()
        Dim FeeTypes() As String = GetFeeTypeConfigID().Split("$")
        txtAdmissionFeeID.Text = FeeTypes(0)
        cboSession.SelectedIndex = cboSession.Items.Count - 1
        Try
            GetFeesDetails()
        Catch ex As Exception

        End Try
        'If GridView1.Rows.Count = 0 Then
        '    GridView1.Visible = False
        'ElseIf GridView2.Rows.Count = 0 Then
        '    GridView2.Visible = False
        'End If
    End Sub
    'Private Sub GetDefaultSession()
    '    Dim sqlStr As String = ""
    '    sqlStr = "Select LastASName From vw_Student Where SID=" & Request.Cookies("SID").Value
    '    Try
    '        cboSession.Text = ExecuteQuery_ExecuteScalar(sqlStr)
    '        Dim mySessionID As Integer = 0
    '        mySessionID = FindMasterID(1, cboSession.Text)
    '        'ClearReportData()
    '        'PrepareHistory(mySessionID)
    '        'FillFeeConfig(mySessionID)
    '    Catch ex As Exception

    '    End Try
    'End Sub
    
    Private Sub GetStudentDetail()
        Dim sqlStr As String = ""
        sqlStr = "Select ClassName,SecName,ASID,FeeGroupID From vw_Student where SID=" & txtSID.Text
        Dim DueConfigReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While DueConfigReader.Read
            txtFeeGroupID.Text = DueConfigReader("FeeGroupID")
            'txtCategoryArmyID.Text = DueConfigReader("CategoryArmyID")
            txtClassName.Text = DueConfigReader("ClassName")
            txtSecName.Text = DueConfigReader("SecName")
            txtASID.Text = DueConfigReader("ASID")
            'FlagFine = True
            Try
                txtConfigType.Text = DueConfigReader("FeeConfigType")
            Catch ex As Exception
                txtConfigType.Text = "0"
            End Try
        End While
        DueConfigReader.Close()
        'FeeConfigType = CheckStudentConfig(Val(txtSID.Text))
    End Sub

    Protected Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
        If cboSession.Text = "" Then
            lblStatus.Text = "Please Select Session"
            cboSession.Focus()
            Exit Sub
        End If
        GetFeesDetails()
    End Sub
    Public Sub GetFeesDetails()
        'Dim mySessionID As Integer = 0
        'mySessionID = FindMasterID(1, cboSession.Text)
        'ClearReportData()
        'PrepareHistory(mySessionID)
        'FillFeeConfig(mySessionID)
        FillHistoryGrid()
        SqlDataSource3.SelectCommand = "SELECT TermID,[TermNo],TermName FROM TermMaster where FeeGroupID=" & Val(txtFeeGroupID.Text)
        GridView2.DataBind()

        If GridView2.FooterRow.Cells(3).Text = "0" Then
            GridView2.Visible = False
        Else
            gvdiv.Visible = True
            GridView2.Visible = True
        End If
    End Sub

    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim FeeDepositID As String = GridView1.DataKeys(e.Row.RowIndex).Values(0).ToString()

                Dim TermList As String = ""
                Dim Sqlstr As String = ""
                Sqlstr = "Select distinct(TermNo) From vw_FeeDeposit Where FeeDepositID=" & FeeDepositID & " ANd ASID=(select ASID from AcademicSession where ASNAme='" & cboSession.Text & "')"
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

    Dim totAmt As Double = 0, TotDueAmt As Double = 0, TotConcessionAmt As Double = 0, TotExcessAmt As Double = 0, TotDepositAmt As Double = 0
    Protected Sub GridView2_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView2.RowDataBound
        Dim ASID As String = FindMasterID(1, cboSession.Text)
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim TermID As String = GridView2.DataKeys(e.Row.RowIndex).Values(0).ToString()
                If txtConfigType.Text = "1" Then
                    Dim TermAmount As Double = GetFeeConfigForFeeHead(ASID, 0, 0, GetMonthID(Val(txtFeeGroupID.Text), TermID), "", Val(txtSID.Text))
                    e.Row.Cells(3).Text = TermAmount
                Else
                    Dim AdmissionFee As Double = GetFeeConfigForFeeHead(ASID, Val(txtFeeGroupID.Text), txtAdmissionFeeID.Text, GetMonthID(Val(txtFeeGroupID.Text), TermID))
                    Dim TermAmount As Double = GetFeeConfigForFeeHead(ASID, Val(txtFeeGroupID.Text), 0, GetMonthID(Val(txtFeeGroupID.Text), TermID))
                    If Val(txtAdminFeeApplicable.Text) = 0 Then
                        TermAmount = TermAmount - AdmissionFee
                    End If
                    e.Row.Cells(3).Text = TermAmount
                    totAmt += TermAmount
                End If
                Try
                    Dim DueDetail As String = GetDueDetail(TermID, Val(txtFeeGroupID.Text), ASID)
                    Dim busterm As Integer = e.Row.RowIndex + 1
                    Dim BusFee As Double = GetBusActualAmt(txtSID.Text, busterm)
                    e.Row.Cells(3).Text = Convert.ToDouble(e.Row.Cells(3).Text) + BusFee
                    e.Row.Cells(2).Text = DueDetail
                Catch ex As Exception

                End Try
                'Dim Abhi As String = GridView1.Rows(e.Row.RowIndex).Cells(1).Text
                Try
                    Dim DepositAmttmp As String = ""
                    Dim DepositAmt As Double = 0, DueAmt As Double = 0, ConcessionAmt As Double = 0, ExcessAmt As Double = 0
                    DepositAmttmp = GetFeeDepositeForStudent(txtSID.Text, 0, "'" & e.Row.Cells(1).Text & "'", txtExcessFeeID.Text)
                    DepositAmt = DepositAmttmp.Split("/")(0)
                    ConcessionAmt = DepositAmttmp.Split("/")(1)
                    ExcessAmt = DepositAmttmp.Split("/")(2)

                    DueAmt = Val(e.Row.Cells(3).Text) - (DepositAmt + ConcessionAmt) + ExcessAmt

                    e.Row.Cells(4).Text = GridView1.Rows(e.Row.RowIndex).Cells(1).Text
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
    Private Sub FillHistoryGrid()
        SqlDataSource2.SelectCommand = "SELECT [FeeDepositID],[DepositDate], Sum([FeeDepositAmount]) as DepositAmount,sum(ConcessionAmount) as ConcessionAmount from vw_FeeDeposit where SID=" & Val(txtSID.Text) & " AND ASID=(select ASID from AcademicSession where ASNAme='" & cboSession.Text & "') group by [FeeDepositID],[DepositDate] order by [DepositDate] DESC"
        GridView1.Visible = True
        GridView1.DataBind()
    End Sub

    'Private Sub FillFeeConfig(ASID As Integer)

    '    'SqlDataSource2.SelectCommand = "SELECT [TermNo], Sum(FeeAmount) as Amount FROM [vw_FeeConfig] where ClassName='" & txtClassName.Text & "' and SecName='" & txtSecName.Text & "' group by TermNo"
    '    '    '& " and FeeGroupID=" & txtFeeGroupID.Text & " and FeeTypeID<>" & AdminFeeID & " and CategoryArmyID=" & txtCategoryArmyID.Text & " group by TermID,termno,TermName"
    '    'GridView1.DataBind()
    'End Sub
    'Protected Sub GridView2_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView2.RowDataBound
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        If e.Row.RowType = DataControlRowType.DataRow Then

    '            Dim TermID As String = GridView2.DataKeys(e.Row.RowIndex).Values(0).ToString()

    '            Try
    '                Dim DueDetail As String = GetDueDetail(TermID, txtClassName.Text, txtSecName.Text, txtASID.Text)
    '                Dim busterm As Integer = e.Row.RowIndex + 1
    '                Dim BusFee As Double = 0
    '                'GetBusActualAmt(txtSID.Text, busterm)
    '                'GetBusDueAmountForTerm(Request.Cookies("ASID").Value, cboBusTerm.Text, txtSID.Text, Now.Date) + 
    '                e.Row.Cells(3).Text = Convert.ToDouble(e.Row.Cells(3).Text) + BusFee
    '                Dim Dues() As String = DueDetail.Split("-")
    '                Dim DueDate As String = Dues(0)
    '                Dim Fine As Double = Dues(1)
    '                Dim DepositAmt As Double = Dues(2)
    '                e.Row.Cells(2).Text = DueDate
    '                e.Row.Cells(4).Text = Fine
    '                e.Row.Cells(5).Text = DepositAmt
    '                Dim NetAmount As Double = Fine + Convert.ToDouble(e.Row.Cells(3).Text) - DepositAmt
    '                e.Row.Cells(6).Text = NetAmount
    '                Dim button As Button = DirectCast(e.Row.Cells(7).Controls(0), Button)
    '                If NetAmount = 0 Or NetAmount < 0 Then
    '                    button.Visible = False
    '                Else
    '                    button.Visible = True
    '                End If
    '            Catch ex As Exception

    '            End Try
    '            ' Add the column index as the event argument parameter

    '        End If
    '    End If
    'End Sub
    'Public Function GetDueDetail(TermID As Integer, ClassName As String, SecName As String, ASID As Integer) As String
    '    Dim Dated As Date = Now.Date.ToString("yyyy/MM/dd")

    '    Dim tmpNowDate As String = Now.Date.ToString("yyyy/MM/dd")
    '    Dim LastDate As Date = Now.Date
    '    Dim LateFeeAmount As Double = 0, LateFeeType As Integer = 0, ProcessingMethod As Integer = 0
    '    Dim Sqlstr As String = ""
    '    Sqlstr = "Select LastDate, LateFeeAmount, LateFeeType, ProcessingMethod From vwDueConfigClassSec Where ClassName='" & ClassName & "' and SecName='" & SecName & "' and TermID=" & TermID & " AND ASID=" & ASID
    '    Sqlstr += " And LastDate<='" & tmpNowDate & "'"
    '    Dim DueConfigReader As SqlDataReader = ExecuteQuery_ExecuteReader(Sqlstr)
    '    While DueConfigReader.Read
    '        LastDate = CDate(DueConfigReader(0))
    '        LateFeeAmount = DueConfigReader(1)
    '        LateFeeType = DueConfigReader(2)
    '        ProcessingMethod = DueConfigReader(3)
    '        'FlagFine = True
    '    End While
    '    DueConfigReader.Close()

    '    If ProcessingMethod = 1 Then  'Monthly
    '        Dim TimeDiff As New TimeSpan
    '        TimeDiff = LastDate - Now
    '        Dim DiffDays As Integer = Math.Abs(TimeDiff.Days)
    '        Dim Months As Double = Convert.ToDouble(DiffDays) / 30
    '        LateFeeAmount = LateFeeAmount * Math.Ceiling(Months)
    '      ElseIf ProcessingMethod = 2 Then  'Daily
    '        'Calculate difference between current date and last date
    '        Dim TimeDiff As New TimeSpan
    '        TimeDiff = LastDate - Now
    '        Dim DiffDays As Integer = Math.Abs(TimeDiff.Days)
    '        LateFeeAmount = (LateFeeAmount * DiffDays)
    '       Else  'Fix
    '    End If


    '    Sqlstr = "Select top(1) LastDate, LateFeeAmount, LateFeeType, ProcessingMethod From vwDueConfigClassSec Where ClassName='" & ClassName & "' and SecName='" & SecName & "' and TermID=" & TermID & " AND ASID=" & ASID
    '    Sqlstr += " And LastDate>='" & tmpNowDate & "'"
    '    Dim DueConfigReader1 As SqlDataReader = ExecuteQuery_ExecuteReader(Sqlstr)
    '    While DueConfigReader1.Read
    '        LastDate = CDate(DueConfigReader1(0))
    '        'FlagFine = True
    '    End While
    '    DueConfigReader1.Close()

    '    Sqlstr = "Select ConcessionFeeType From vwFeeConcessionConfig"
    '    Dim ConcessionFeeTypeID As Integer = 0
    '    Try
    '        ConcessionFeeTypeID = ExecuteQuery_ExecuteScalar(Sqlstr)
    '    Catch ex As Exception

    '    End Try

    '    Sqlstr = "select Sum(FeeDepositAmount) as FD from vw_FeeDeposit Where SID=" & txtSID.Text & " and TermID=" & TermID & " and FeeTypeID<>" & ConcessionFeeTypeID
    '    Dim DepositAmount As Double = 0
    '    Try
    '        DepositAmount = ExecuteQuery_ExecuteScalar(Sqlstr)
    '    Catch ex As Exception
    '        DepositAmount = 0
    '    End Try

    '    Return LastDate.ToString("dd/MM/yyyy") & "-" & LateFeeAmount & "-" & DepositAmount
    'End Function

    'Protected Sub GridView2_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView2.RowCommand
    '    If e.CommandName.ToString() = "PayOnline" Then
    '        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
    '        Dim TermID As Integer = GridView2.DataKeys(index).Value.ToString()
    '        Dim selectedRowIndex As Integer = Convert.ToInt32(e.CommandArgument.ToString())
    '        Dim NetAmount As Double = GridView2.Rows(selectedRowIndex).Cells(6).Text
    '        Response.Redirect("Pay.aspx?SID=" & txtSID.Text & "&Amt=" & NetAmount & "&TID=" & TermID)
    '    End If
    'End Sub

    'Private Sub FillFeeConfig()
    '    SqlDataSource3.SelectCommand = "SELECT TermID,[TermNo],TermName FROM TermMaster where FeeGroupID=" & Val(txtFeeGroupID.Text)
    '    GridView2.DataBind()
    '    GridView2.Visible = True
    'End Sub

    'Private Sub ClearReportData()
    '    Dim sqlStr As String = ""
    '    sqlStr = "Delete from rptFeeHistory where SID=" & txtSID.Text
    '    ExecuteQuery_Update(sqlStr)
    'End Sub
    'Private Sub PrepareHistory(ASID As Integer)

    '    Dim lstFeeDepositID As New ListBox
    '    Dim InsertSQl As New ListBox
    '    Dim OldDepositID As Integer = 0

    '    lstFeeDepositID.Items.Clear()
    '    InsertSQl.Items.Clear()

    '    Dim sqlStr As String = "", TempSQL As String = ""

    '    sqlStr = "Delete from rptFeeHistory"
    '    ExecuteQuery_Update(sqlStr)

    '    'Read Concession Fee Type
    '    sqlStr = "Select ConcessionFeeType, FeetypeName From vwFeeConcessionConfig"

    '    Dim ConcessionConfigReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
    '    Dim ConcessionFeeTypeID As Integer = 0
    '    Dim ConcessionFeeTypeName As String = ""

    '    While ConcessionConfigReader.Read
    '        ConcessionFeeTypeID = ConcessionConfigReader(0)
    '        ConcessionFeeTypeName = ConcessionConfigReader(1)
    '    End While
    '    ConcessionConfigReader.Close()

    '    sqlStr = "Select SID, RegNo, FeeBookNo, SName, FName, ClassName, SecName, FeeDepositID, DepositDate, PMName, DepositDetails, TermNo From vw_FeeDeposit Where SID='" & txtSID.Text & "' AND FeeTypeName<>'" & ConcessionFeeTypeName & "' AND ASID=" & ASID

    '    Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
    '    While myReader.Read

    '        If myReader("FeeDepositID") <> OldDepositID Then
    '            lstFeeDepositID.Items.Add(myReader("FeeDepositID"))

    '            TempSQL = "Insert into rptFeeHistory (SID,DepositDate, TermNo, DepositAmount, DepositMode, DepositDetails) Values (" & _
    '            txtSID.Text & "," & _
    '            "'" & CDate(myReader("DepositDate")).Month & "/" & CDate(myReader("DepositDate")).Day & "/" & CDate(myReader("DepositDate")).Year & "'," & _
    '           "'" & myReader("TermNo") & "'," & _
    '            -1 & "," & _
    '            "'" & myReader("PMName") & "'," & _
    '            "'" & myReader("DepositDetails") & "')"
    '            InsertSQl.Items.Add(TempSQL)
    '        End If

    '        OldDepositID = myReader("FeeDepositID")
    '    End While
    '    myReader.Close()

    '    Dim i As Integer = 0
    '    For i = 0 To lstFeeDepositID.Items.Count - 1

    '        sqlStr = "Select Sum(FeeDepositAmount) From FeeDepositDetails Where FeeDepositID=" & lstFeeDepositID.Items(i).Text & " and FeeTypeID<>" & ConcessionFeeTypeID
    '        Dim DepositAmount As Double = 0
    '        Try
    '            DepositAmount = ExecuteQuery_ExecuteScalar(sqlStr)
    '        Catch ex As Exception
    '            DepositAmount = 0
    '        End Try

    '        InsertSQl.Items(i).Text = InsertSQl.Items(i).Text.Replace("-1", DepositAmount)
    '        ExecuteQuery_Update(InsertSQl.Items(i).Text)

    '    Next

    '    System.Threading.Thread.Sleep(500)

    '    SqlDataSource1.SelectCommand = "SELECT [DepositDate], [TermNo], [DepositAmount], [DepositMode], [DepositDetails] FROM [rptFeeHistory] where SID=" & txtSID.Text & " Order by DepositDate"
    '    GridView1.DataBind()

    '    If GridView1.Rows.Count <= 0 Then
    '        'lblStatus.Text = "No record Found for Computer Code: " & txtFeeBookNo.Text
    '        lblStatus.Visible = True
    '    Else
    '        lblStatus.Visible = False
    '    End If
    'End Sub
End Class
