Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary_Fee.CLS_iDiary_Fee
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html.simpleparser

Public Class FeeConfig
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Fee") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
            End If
        Catch ex As Exception
            Response.Redirect("~/Login.aspx")
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
            'myTable.Rows.Clear()
            If cboFeeGroup.Text <> "" Then
                CreateTable(Request.Cookies("ASID").Value)
            Else
                myTable.Rows.Clear()
            End If
        End If
    End Sub

    Private Sub InitControls()

        LoadMasterInfo(60, cboFeeGroup)
        lblStatus.Text = ""
        btnSave.Visible = False
        btnImportFee.Visible = False
        myTable.Rows.Clear()
        cboFeeGroup.Focus()
    End Sub
    'Private Sub CreateTable()
    '    Dim FeeGroupID As Integer = FindMasterID(60, cboFeeGroup.Text)
    '    'Dim CatArmyID As Integer = FindMasterID(61, cboCategoryArmy.Text)
    '    myTable.Rows.Clear()

    '    Dim tr1 As New TableRow

    '    Dim td10 As New TableCell
    '    td10.Text = "<B>Fee ID</B>"
    '    td10.HorizontalAlign = HorizontalAlign.Center
    '    tr1.Cells.Add(td10)

    '    Dim td11 As New TableCell
    '    td11.Text = "<B>Head/Month</B>"
    '    td11.HorizontalAlign = HorizontalAlign.Center
    '    tr1.Cells.Add(td11)

    '    Dim td12 As New TableCell
    '    td12.Text = "<B>Apr</B>"
    '    td12.HorizontalAlign = HorizontalAlign.Center
    '    tr1.Cells.Add(td12)

    '    Dim td13 As New TableCell
    '    td13.Text = "<B>May</B>"
    '    td13.HorizontalAlign = HorizontalAlign.Center
    '    tr1.Cells.Add(td13)

    '    Dim td14 As New TableCell
    '    td14.Text = "<B>Jun</B>"
    '    td14.HorizontalAlign = HorizontalAlign.Center
    '    tr1.Cells.Add(td14)

    '    Dim td15 As New TableCell
    '    td15.Text = "<B>Jul</B>"
    '    td15.HorizontalAlign = HorizontalAlign.Center
    '    tr1.Cells.Add(td15)



    '    Dim td16 As New TableCell
    '    td16.Text = "<B>Aug</B>"
    '    td16.HorizontalAlign = HorizontalAlign.Center
    '    tr1.Cells.Add(td16)

    '    Dim td17 As New TableCell
    '    td17.Text = "<B>Sep</B>"
    '    td17.HorizontalAlign = HorizontalAlign.Center
    '    tr1.Cells.Add(td17)

    '    Dim td18 As New TableCell
    '    td18.Text = "<B>Oct</B>"
    '    td18.HorizontalAlign = HorizontalAlign.Center
    '    tr1.Cells.Add(td18)

    '    Dim td19 As New TableCell
    '    td19.Text = "<B>Nov</B>"
    '    td19.HorizontalAlign = HorizontalAlign.Center
    '    tr1.Cells.Add(td19)


    '    Dim td20 As New TableCell
    '    td20.Text = "<B>Dec</B>"
    '    td20.HorizontalAlign = HorizontalAlign.Center
    '    tr1.Cells.Add(td20)

    '    Dim td21 As New TableCell
    '    td21.Text = "<B>Jan</B>"
    '    td21.HorizontalAlign = HorizontalAlign.Center
    '    tr1.Cells.Add(td21)

    '    Dim td22 As New TableCell
    '    td22.Text = "<B>Feb</B>"
    '    td22.HorizontalAlign = HorizontalAlign.Center
    '    tr1.Cells.Add(td22)

    '    Dim td23 As New TableCell
    '    td23.Text = "<B>Mar</B>"
    '    td23.HorizontalAlign = HorizontalAlign.Center
    '    tr1.Cells.Add(td23)

    '    Dim td24 As New TableCell
    '    td24.Text = "<B>Total</B>"
    '    td24.HorizontalAlign = HorizontalAlign.Center
    '    tr1.Cells.Add(td24)


    '    myTable.Rows.Add(tr1)

    '    Dim sqlStr As String = ""
    '    Dim myTxtBoxNumber As Integer = 1

    '    'Process Late Fee Amount and Type
    '    'Dim lstLateFeeTypeID As New ListBox, lstLateFeeAmount As New ListBox
    '    Dim t As Integer = 0

    '    sqlStr = "Select FeeTypeID, FeeTypeName From FeeTypes where FeeTypeId<>" & txtLateFeeID.Text
    '    Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)

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

    '        'Apr
    '        Dim txtAmount4 As New TextBox()
    '        txtAmount4.ID = "txtA04" & myTxtBoxNumber
    '        txtAmount4.Width = 40
    '        txtAmount4.Attributes.Add("onchange", "javascript: ShowTotal();")
    '        Dim tdtxtAmount4 As New TableCell
    '        tdtxtAmount4.Controls.Add(txtAmount4)
    '        tdtxtAmount4.HorizontalAlign = HorizontalAlign.Center
    '        trx.Cells.Add(tdtxtAmount4)

    '        'May
    '        Dim txtAmount5 As New TextBox()
    '        txtAmount5.ID = "txtA05" & myTxtBoxNumber
    '        txtAmount5.Width = 40
    '        txtAmount5.Attributes.Add("onchange", "javascript: ShowTotal();")
    '        Dim tdtxtAmount5 As New TableCell
    '        tdtxtAmount5.Controls.Add(txtAmount5)
    '        tdtxtAmount5.HorizontalAlign = HorizontalAlign.Center
    '        trx.Cells.Add(tdtxtAmount5)

    '        'Jun
    '        Dim txtAmount6 As New TextBox()
    '        txtAmount6.ID = "txtA06" & myTxtBoxNumber
    '        txtAmount6.Width = 40
    '        txtAmount6.Attributes.Add("onchange", "javascript: ShowTotal();")
    '        Dim tdtxtAmount6 As New TableCell
    '        tdtxtAmount6.Controls.Add(txtAmount6)
    '        tdtxtAmount6.HorizontalAlign = HorizontalAlign.Center
    '        trx.Cells.Add(tdtxtAmount6)

    '        'Jul
    '        Dim txtAmount7 As New TextBox()
    '        txtAmount7.ID = "txtA07" & myTxtBoxNumber
    '        txtAmount7.Width = 40
    '        txtAmount7.Attributes.Add("onchange", "javascript: ShowTotal();")
    '        Dim tdtxtAmount7 As New TableCell
    '        tdtxtAmount7.Controls.Add(txtAmount7)
    '        tdtxtAmount7.HorizontalAlign = HorizontalAlign.Center
    '        trx.Cells.Add(tdtxtAmount7)

    '        'Aug
    '        Dim txtAmount8 As New TextBox()
    '        txtAmount8.ID = "txtA08" & myTxtBoxNumber
    '        txtAmount8.Width = 40
    '        txtAmount8.Attributes.Add("onchange", "javascript: ShowTotal();")
    '        Dim tdtxtAmount8 As New TableCell
    '        tdtxtAmount8.Controls.Add(txtAmount8)
    '        tdtxtAmount8.HorizontalAlign = HorizontalAlign.Center
    '        trx.Cells.Add(tdtxtAmount8)

    '        'Sep
    '        Dim txtAmount9 As New TextBox()
    '        txtAmount9.ID = "txtA09" & myTxtBoxNumber
    '        txtAmount9.Width = 40
    '        txtAmount9.Attributes.Add("onchange", "javascript: ShowTotal();")
    '        Dim tdtxtAmount9 As New TableCell
    '        tdtxtAmount9.Controls.Add(txtAmount9)
    '        tdtxtAmount9.HorizontalAlign = HorizontalAlign.Center
    '        trx.Cells.Add(tdtxtAmount9)

    '        'Oct
    '        Dim txtAmount10 As New TextBox()
    '        txtAmount10.ID = "txtA10" & myTxtBoxNumber
    '        txtAmount10.Width = 40
    '        txtAmount10.Attributes.Add("onchange", "javascript: ShowTotal();")
    '        Dim tdtxtAmount10 As New TableCell
    '        tdtxtAmount10.Controls.Add(txtAmount10)
    '        tdtxtAmount10.HorizontalAlign = HorizontalAlign.Center
    '        trx.Cells.Add(tdtxtAmount10)

    '        'Nov
    '        Dim txtAmount11 As New TextBox()
    '        txtAmount11.ID = "txtA11" & myTxtBoxNumber
    '        txtAmount11.Width = 40
    '        txtAmount11.Attributes.Add("onchange", "javascript: ShowTotal();")
    '        Dim tdtxtAmount11 As New TableCell
    '        tdtxtAmount11.Controls.Add(txtAmount11)
    '        tdtxtAmount11.HorizontalAlign = HorizontalAlign.Center
    '        trx.Cells.Add(tdtxtAmount11)

    '        'Dec
    '        Dim txtAmount12 As New TextBox()
    '        txtAmount12.ID = "txtA12" & myTxtBoxNumber
    '        txtAmount12.Width = 40
    '        txtAmount12.Attributes.Add("onchange", "javascript: ShowTotal();")
    '        Dim tdtxtAmount12 As New TableCell
    '        tdtxtAmount12.Controls.Add(txtAmount12)
    '        tdtxtAmount12.HorizontalAlign = HorizontalAlign.Center
    '        trx.Cells.Add(tdtxtAmount12)

    '        'Jan
    '        Dim txtAmount1 As New TextBox()
    '        txtAmount1.ID = "txtA01" & myTxtBoxNumber
    '        txtAmount1.Width = 40
    '        txtAmount1.Attributes.Add("onchange", "javascript: ShowTotal();")
    '        Dim tdtxtAmount1 As New TableCell
    '        tdtxtAmount1.Controls.Add(txtAmount1)
    '        tdtxtAmount1.HorizontalAlign = HorizontalAlign.Center
    '        trx.Cells.Add(tdtxtAmount1)

    '        'Feb
    '        Dim txtAmount2 As New TextBox()
    '        txtAmount2.ID = "txtA02" & myTxtBoxNumber
    '        txtAmount2.Width = 40
    '        txtAmount2.Attributes.Add("onchange", "javascript: ShowTotal();")
    '        Dim tdtxtAmount2 As New TableCell
    '        tdtxtAmount2.Controls.Add(txtAmount2)
    '        tdtxtAmount2.HorizontalAlign = HorizontalAlign.Center
    '        trx.Cells.Add(tdtxtAmount2)

    '        'Mar
    '        Dim txtAmount3 As New TextBox()
    '        txtAmount3.ID = "txtA03" & myTxtBoxNumber
    '        txtAmount3.Width = 40
    '        txtAmount3.Attributes.Add("onchange", "javascript: ShowTotal();")
    '        Dim tdtxtAmount3 As New TableCell
    '        tdtxtAmount3.Controls.Add(txtAmount3)
    '        tdtxtAmount3.HorizontalAlign = HorizontalAlign.Center
    '        trx.Cells.Add(tdtxtAmount3)

    '        'Total
    '        Dim txtAmountTotal As New TextBox()
    '        txtAmountTotal.ID = "txtTotal" & myTxtBoxNumber
    '        txtAmountTotal.Width = 50
    '        txtAmountTotal.BackColor = Drawing.Color.Yellow
    '        txtAmountTotal.Attributes.Add("onchange", "javascript: ShowTotal();")
    '        Dim tdtxtAmountTotal As New TableCell
    '        tdtxtAmountTotal.Controls.Add(txtAmountTotal)
    '        tdtxtAmountTotal.HorizontalAlign = HorizontalAlign.Center
    '        trx.Cells.Add(tdtxtAmountTotal)

    '        myTable.Rows.Add(trx)

    '        myTxtBoxNumber += 1
    '    End While
    '    myReader.Close()

    '    'Retrieve Concession Fee Type Config(Given Additionally During Fee Deposit)

    '    Dim i As Integer = 0, j As Integer = 0, myCount As Integer = 0
    '    Dim DefaultFeeTotal As Double = 0, OverallConcessionAmount As Double = 0
    '    Dim lastFeeTypeID As Integer = 0

    '    sqlStr = "Select sum(FeeAmount),monthID,FeeTypeID  From vw_FeeConfig Where ASID=" & Request.Cookies("ASID").Value & " AND FeeGroupID=" & Val(FeeGroupID) & " group by monthID,FeeTypeID order by feetypeID,monthID"

    '    Dim myreaderConfig As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
    '    While myreaderConfig.Read
    '        Dim myFeeAmount As Double = 0
    '        Dim myFeeTypeIDtmp As Integer = 0
    '        Dim MonthID As Integer = 0
    '        myFeeAmount = myreaderConfig(0)
    '        myFeeTypeIDtmp = myreaderConfig("FeeTypeID")
    '        MonthID = myreaderConfig("monthID")
    '        If lastFeeTypeID <> myFeeTypeIDtmp Then
    '            DefaultFeeTotal = 0
    '            lastFeeTypeID = myFeeTypeIDtmp
    '        End If
    '        For i = 1 To myTable.Rows.Count - 1
    '            Dim myFeeTypeID As Integer = myTable.Rows(i).Cells(0).Text   'Get FeeTypeID From Table
    '            If myFeeTypeID = myFeeTypeIDtmp Then
    '                For j = 1 To 12
    '                    If myFeeTypeID = txtConveyanceFeeID.Text Or myFeeTypeID = txtLateFeeID.Text Then
    '                        CType(myTable.FindControl("txtA" & j.ToString("00") & i), TextBox).Enabled = False
    '                    Else
    '                        If MonthID = j Then
    '                            CType(myTable.FindControl("txtA" & j.ToString("00") & i), TextBox).Text = myFeeAmount
    '                            DefaultFeeTotal += myFeeAmount
    '                        End If
    '                    End If
    '                Next
    '            End If
    '            If myFeeTypeID = myFeeTypeIDtmp Then
    '                CType(myTable.FindControl("txtTotal" & i), TextBox).Text = DefaultFeeTotal
    '            End If
    '        Next
    '    End While
    '    myreaderConfig.Close()
    '    btnSave.Visible = True
    '    btnImportFee.Visible = True
    'End Sub



    Private Sub CreateTable(ByVal ASID As Integer)
        Dim FeeGroupID As Integer = FindMasterID(60, cboFeeGroup.Text)
        'Dim CatArmyID As Integer = FindMasterID(61, cboCategoryArmy.Text)
        myTable.Rows.Clear()

        Dim tr1 As New TableRow

        Dim td10 As New TableCell
        td10.Text = "<B>Fee ID</B>"
        td10.HorizontalAlign = HorizontalAlign.Center
        tr1.Cells.Add(td10)

        Dim td11 As New TableCell
        td11.Text = "<B>Head/Month</B>"
        td11.HorizontalAlign = HorizontalAlign.Center
        tr1.Cells.Add(td11)

        Dim td12 As New TableCell
        td12.Text = "<B>Apr</B>"
        td12.HorizontalAlign = HorizontalAlign.Center
        tr1.Cells.Add(td12)

        Dim td13 As New TableCell
        td13.Text = "<B>May</B>"
        td13.HorizontalAlign = HorizontalAlign.Center
        tr1.Cells.Add(td13)

        Dim td14 As New TableCell
        td14.Text = "<B>Jun</B>"
        td14.HorizontalAlign = HorizontalAlign.Center
        tr1.Cells.Add(td14)

        Dim td15 As New TableCell
        td15.Text = "<B>Jul</B>"
        td15.HorizontalAlign = HorizontalAlign.Center
        tr1.Cells.Add(td15)



        Dim td16 As New TableCell
        td16.Text = "<B>Aug</B>"
        td16.HorizontalAlign = HorizontalAlign.Center
        tr1.Cells.Add(td16)

        Dim td17 As New TableCell
        td17.Text = "<B>Sep</B>"
        td17.HorizontalAlign = HorizontalAlign.Center
        tr1.Cells.Add(td17)

        Dim td18 As New TableCell
        td18.Text = "<B>Oct</B>"
        td18.HorizontalAlign = HorizontalAlign.Center
        tr1.Cells.Add(td18)

        Dim td19 As New TableCell
        td19.Text = "<B>Nov</B>"
        td19.HorizontalAlign = HorizontalAlign.Center
        tr1.Cells.Add(td19)


        Dim td20 As New TableCell
        td20.Text = "<B>Dec</B>"
        td20.HorizontalAlign = HorizontalAlign.Center
        tr1.Cells.Add(td20)

        Dim td21 As New TableCell
        td21.Text = "<B>Jan</B>"
        td21.HorizontalAlign = HorizontalAlign.Center
        tr1.Cells.Add(td21)

        Dim td22 As New TableCell
        td22.Text = "<B>Feb</B>"
        td22.HorizontalAlign = HorizontalAlign.Center
        tr1.Cells.Add(td22)

        Dim td23 As New TableCell
        td23.Text = "<B>Mar</B>"
        td23.HorizontalAlign = HorizontalAlign.Center
        tr1.Cells.Add(td23)

        Dim td24 As New TableCell
        td24.Text = "<B>Total</B>"
        td24.HorizontalAlign = HorizontalAlign.Center
        tr1.Cells.Add(td24)


        myTable.Rows.Add(tr1)

        Dim sqlStr As String = ""
        Dim myTxtBoxNumber As Integer = 1

        'Process Late Fee Amount and Type
        'Dim lstLateFeeTypeID As New ListBox, lstLateFeeAmount As New ListBox
        Dim t As Integer = 0

        sqlStr = "Select FeeTypeID, FeeTypeName From FeeTypes where FeeTypeId<>" & txtLateFeeID.Text
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

            'Apr
            Dim txtAmount4 As New TextBox()
            txtAmount4.ID = "txtA04" & myTxtBoxNumber
            txtAmount4.Width = 40
            txtAmount4.Attributes.Add("onchange", "javascript: ShowTotal();")
            Dim tdtxtAmount4 As New TableCell
            tdtxtAmount4.Controls.Add(txtAmount4)
            tdtxtAmount4.HorizontalAlign = HorizontalAlign.Center
            trx.Cells.Add(tdtxtAmount4)

            'May
            Dim txtAmount5 As New TextBox()
            txtAmount5.ID = "txtA05" & myTxtBoxNumber
            txtAmount5.Width = 40
            txtAmount5.Attributes.Add("onchange", "javascript: ShowTotal();")
            Dim tdtxtAmount5 As New TableCell
            tdtxtAmount5.Controls.Add(txtAmount5)
            tdtxtAmount5.HorizontalAlign = HorizontalAlign.Center
            trx.Cells.Add(tdtxtAmount5)

            'Jun
            Dim txtAmount6 As New TextBox()
            txtAmount6.ID = "txtA06" & myTxtBoxNumber
            txtAmount6.Width = 40
            txtAmount6.Attributes.Add("onchange", "javascript: ShowTotal();")
            Dim tdtxtAmount6 As New TableCell
            tdtxtAmount6.Controls.Add(txtAmount6)
            tdtxtAmount6.HorizontalAlign = HorizontalAlign.Center
            trx.Cells.Add(tdtxtAmount6)

            'Jul
            Dim txtAmount7 As New TextBox()
            txtAmount7.ID = "txtA07" & myTxtBoxNumber
            txtAmount7.Width = 40
            txtAmount7.Attributes.Add("onchange", "javascript: ShowTotal();")
            Dim tdtxtAmount7 As New TableCell
            tdtxtAmount7.Controls.Add(txtAmount7)
            tdtxtAmount7.HorizontalAlign = HorizontalAlign.Center
            trx.Cells.Add(tdtxtAmount7)

            'Aug
            Dim txtAmount8 As New TextBox()
            txtAmount8.ID = "txtA08" & myTxtBoxNumber
            txtAmount8.Width = 40
            txtAmount8.Attributes.Add("onchange", "javascript: ShowTotal();")
            Dim tdtxtAmount8 As New TableCell
            tdtxtAmount8.Controls.Add(txtAmount8)
            tdtxtAmount8.HorizontalAlign = HorizontalAlign.Center
            trx.Cells.Add(tdtxtAmount8)

            'Sep
            Dim txtAmount9 As New TextBox()
            txtAmount9.ID = "txtA09" & myTxtBoxNumber
            txtAmount9.Width = 40
            txtAmount9.Attributes.Add("onchange", "javascript: ShowTotal();")
            Dim tdtxtAmount9 As New TableCell
            tdtxtAmount9.Controls.Add(txtAmount9)
            tdtxtAmount9.HorizontalAlign = HorizontalAlign.Center
            trx.Cells.Add(tdtxtAmount9)

            'Oct
            Dim txtAmount10 As New TextBox()
            txtAmount10.ID = "txtA10" & myTxtBoxNumber
            txtAmount10.Width = 40
            txtAmount10.Attributes.Add("onchange", "javascript: ShowTotal();")
            Dim tdtxtAmount10 As New TableCell
            tdtxtAmount10.Controls.Add(txtAmount10)
            tdtxtAmount10.HorizontalAlign = HorizontalAlign.Center
            trx.Cells.Add(tdtxtAmount10)

            'Nov
            Dim txtAmount11 As New TextBox()
            txtAmount11.ID = "txtA11" & myTxtBoxNumber
            txtAmount11.Width = 40
            txtAmount11.Attributes.Add("onchange", "javascript: ShowTotal();")
            Dim tdtxtAmount11 As New TableCell
            tdtxtAmount11.Controls.Add(txtAmount11)
            tdtxtAmount11.HorizontalAlign = HorizontalAlign.Center
            trx.Cells.Add(tdtxtAmount11)

            'Dec
            Dim txtAmount12 As New TextBox()
            txtAmount12.ID = "txtA12" & myTxtBoxNumber
            txtAmount12.Width = 40
            txtAmount12.Attributes.Add("onchange", "javascript: ShowTotal();")
            Dim tdtxtAmount12 As New TableCell
            tdtxtAmount12.Controls.Add(txtAmount12)
            tdtxtAmount12.HorizontalAlign = HorizontalAlign.Center
            trx.Cells.Add(tdtxtAmount12)

            'Jan
            Dim txtAmount1 As New TextBox()
            txtAmount1.ID = "txtA01" & myTxtBoxNumber
            txtAmount1.Width = 40
            txtAmount1.Attributes.Add("onchange", "javascript: ShowTotal();")
            Dim tdtxtAmount1 As New TableCell
            tdtxtAmount1.Controls.Add(txtAmount1)
            tdtxtAmount1.HorizontalAlign = HorizontalAlign.Center
            trx.Cells.Add(tdtxtAmount1)

            'Feb
            Dim txtAmount2 As New TextBox()
            txtAmount2.ID = "txtA02" & myTxtBoxNumber
            txtAmount2.Width = 40
            txtAmount2.Attributes.Add("onchange", "javascript: ShowTotal();")
            Dim tdtxtAmount2 As New TableCell
            tdtxtAmount2.Controls.Add(txtAmount2)
            tdtxtAmount2.HorizontalAlign = HorizontalAlign.Center
            trx.Cells.Add(tdtxtAmount2)

            'Mar
            Dim txtAmount3 As New TextBox()
            txtAmount3.ID = "txtA03" & myTxtBoxNumber
            txtAmount3.Width = 40
            txtAmount3.Attributes.Add("onchange", "javascript: ShowTotal();")
            Dim tdtxtAmount3 As New TableCell
            tdtxtAmount3.Controls.Add(txtAmount3)
            tdtxtAmount3.HorizontalAlign = HorizontalAlign.Center
            trx.Cells.Add(tdtxtAmount3)

            'Total
            Dim txtAmountTotal As New TextBox()
            txtAmountTotal.ID = "txtTotal" & myTxtBoxNumber
            txtAmountTotal.Width = 50
            txtAmountTotal.BackColor = Drawing.Color.Yellow
            txtAmountTotal.Attributes.Add("onchange", "javascript: ShowTotal();")
            Dim tdtxtAmountTotal As New TableCell
            tdtxtAmountTotal.Controls.Add(txtAmountTotal)
            tdtxtAmountTotal.HorizontalAlign = HorizontalAlign.Center
            trx.Cells.Add(tdtxtAmountTotal)

            myTable.Rows.Add(trx)

            myTxtBoxNumber += 1
        End While
        myReader.Close()

        'Retrieve Concession Fee Type Config(Given Additionally During Fee Deposit)

        Dim i As Integer = 0, j As Integer = 0, myCount As Integer = 0
        Dim DefaultFeeTotal As Double = 0, OverallConcessionAmount As Double = 0
        Dim lastFeeTypeID As Integer = 0

        sqlStr = "Select sum(FeeAmount),monthID,FeeTypeID  From vw_FeeConfig Where ASID=" & ASID & " AND FeeGroupID=" & Val(FeeGroupID) & " group by monthID,FeeTypeID order by feetypeID,monthID"

        Dim myreaderConfig As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myreaderConfig.Read
            Dim myFeeAmount As Double = 0
            Dim myFeeTypeIDtmp As Integer = 0
            Dim MonthID As Integer = 0
            myFeeAmount = myreaderConfig(0)
            myFeeTypeIDtmp = myreaderConfig("FeeTypeID")
            MonthID = myreaderConfig("monthID")
            If lastFeeTypeID <> myFeeTypeIDtmp Then
                DefaultFeeTotal = 0
                lastFeeTypeID = myFeeTypeIDtmp
            End If
            For i = 1 To myTable.Rows.Count - 1
                Dim myFeeTypeID As Integer = myTable.Rows(i).Cells(0).Text   'Get FeeTypeID From Table
                If myFeeTypeID = myFeeTypeIDtmp Then
                    For j = 1 To 12
                        If myFeeTypeID = txtConveyanceFeeID.Text Or myFeeTypeID = txtLateFeeID.Text Then
                            CType(myTable.FindControl("txtA" & j.ToString("00") & i), TextBox).Enabled = False
                        Else
                            If MonthID = j Then
                                CType(myTable.FindControl("txtA" & j.ToString("00") & i), TextBox).Text = myFeeAmount
                                DefaultFeeTotal += myFeeAmount
                            End If
                        End If
                    Next
                End If
                If myFeeTypeID = myFeeTypeIDtmp Then
                    CType(myTable.FindControl("txtTotal" & i), TextBox).Text = DefaultFeeTotal
                End If
            Next
        End While
        myreaderConfig.Close()
        btnSave.Visible = True
        btnImportFee.Visible = True
    End Sub
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If cboFeeGroup.SelectedIndex = 0 Then
            lblStatus.Text = "Select a Fee Group..."
            cboFeeGroup.Focus()
            Exit Sub
        End If

        Dim FeeGroupID As Integer = FindMasterID(60, cboFeeGroup.Text)

        Dim sqlStr As String = "Delete from FeeConfig where ASID=" & Request.Cookies("ASID").Value & " and FeeGroupID=" & FeeGroupID
        ExecuteQuery_Update(sqlStr)


        For i = 1 To myTable.Rows.Count - 1
            Dim myFeeTypeID As String = myTable.Rows(i).Cells(0).Text
            For j = 1 To 12
                Dim myFeeAmount As Double = Val(CType(myTable.FindControl("txtA" & j.ToString("00") & i), TextBox).Text)
                If myFeeAmount > 0 Then
                    sqlStr = "Insert into FeeConfig(ASID,FeeGroupID,MonthId,FeeTypeID,FeeAmount) Values(" & _
         Request.Cookies("ASID").Value & "," & _
        FeeGroupID & "," & _
              j & "," & _
        myFeeTypeID & "," & _
        myFeeAmount & ")"
                    ExecuteQuery_Update(sqlStr)
                End If
            Next
        Next

        InitControls()
        lblStatus.Text = "Fees Configuration has been saved"
    End Sub
    Public Shared Function getRow(ByVal FeeGroupId As String, ByVal TermId As String, ByVal FeeTypeId As String, ASID As Integer) As Integer
        Dim Count As Integer = 0

        Dim sqlStr As String = "Select Count(*) From FeeConfig where FeeGroupID='" & FeeGroupId & "' And TermID='" & TermId & "' And FeeTypeID='" & FeeTypeId & "' and ASID=" & ASID

        Count = ExecuteQuery_ExecuteScalar(sqlStr)

        If Count = 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    Protected Sub cboFeeGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboFeeGroup.SelectedIndexChanged

        If cboFeeGroup.Text <> "" Then
            CreateTable(Request.Cookies("ASID").Value)
        Else
            myTable.Rows.Clear()
        End If
    End Sub


    Protected Sub btnImportFee_Click(sender As Object, e As EventArgs) Handles btnImportFee.Click
        Dim FeeGroupID As Integer = FindMasterID(60, cboFeeGroup.Text)
        Dim count As Integer = ExecuteQuery_ExecuteScalar("Select Count(*)  From vw_FeeConfig Where ASID=" & Request.Cookies("ASID").Value & " AND FeeGroupID=" & Val(FeeGroupID) & "")
        If count <= 0 Then
            Dim PreviousASID As Integer = ExecuteQuery_ExecuteScalar("select Top(1) ASID from AcademicSession where ASID<" & Request.Cookies("ASID").Value & " order by ASID DESC ")
            CreateTable(PreviousASID)
        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('You have already configure fees for selected Fee Group...');", True)
            cboFeeGroup.Focus()
            Exit Sub
        End If
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(control As Control)
        ' Verifies that the control is rendered
    End Sub
   Protected Sub ExportToExcel(sender As Object, e As EventArgs)
        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=HTML.xls")
        Response.Charset = ""
        Response.ContentType = "application/vnd.ms-excel"
        Response.Output.Write(Request.Form(hfGridHtml.UniqueID))
        Response.Flush()
        Response.End()
    End Sub

End Class