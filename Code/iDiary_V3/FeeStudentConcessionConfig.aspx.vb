Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary_Fee.CLS_iDiary_Fee

Public Class FeeStudentConcessionConfig
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
            If txtMyTable.Text <> "" Then
                CreateTable()
            Else
                myTable.Rows.Clear()
            End If
        End If
    End Sub

    Private Sub InitControls()
        LoadFeeConcessionTypeMaster(cboConcessionType)
        lblStatus.Text = ""
        myTable.Rows.Clear()
        txtMyTable.Text = ""
        txtSName.Text = ""
        txtFName.Text = ""
        txtMName.Text = ""
        txtRegNo.Text = ""
        txtFeeBookNo.Text = ""
        txtAdmissionDate.Text = ""
        txtDOB.Text = ""
        txtClass.Text = ""
        txtSec.Text = ""
        txtSID.Text = ""
        txtFeeGroupID.Text = ""
        txtConfigType.Text = ""
        btnSave.Visible = False
        btnRemove.Visible = False
        txtRegNo.Focus()
    End Sub
    Private Sub CreateTable()
        Dim FeeGroupID As Integer = txtFeeGroupID.Text
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
        Dim lstLateFeeTypeID As New ListBox, lstLateFeeAmount As New ListBox
        Dim t As Integer = 0

        'Dim ConveyanceFeeID As Integer = GetFeeTypeConfigID(3)

        sqlStr = "Select FeeTypeID, FeeTypeName From FeeTypes where Concession=1 and FeeTypeID<>" & txtLateFeeID.Text
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
            Dim txtAmount4 As New Label()
            txtAmount4.ID = "txtA04" & myTxtBoxNumber
            txtAmount4.Width = 40
            'txtAmount4.ReadOnly = True
            'txtAmount4.Attributes.Add("onchange", "javascript: ShowTotal();")
            Dim tdtxtAmount4 As New TableCell
            tdtxtAmount4.Controls.Add(txtAmount4)
            tdtxtAmount4.Controls.Add(txtAmount4)
            tdtxtAmount4.HorizontalAlign = HorizontalAlign.Center
            'trx.Cells.Add(tdtxtAmount4)

            Dim txtConAmount4 As New TextBox()
            txtConAmount4.ID = "txtConA04" & myTxtBoxNumber
            txtConAmount4.Width = 40
            txtConAmount4.Attributes.Add("onchange", "javascript: ShowTotal();")
            tdtxtAmount4.Controls.Add(txtConAmount4)
            trx.Cells.Add(tdtxtAmount4)


            'May
            Dim txtAmount5 As New Label()
            txtAmount5.ID = "txtA05" & myTxtBoxNumber
            txtAmount5.Width = 40
            'txtAmount5.ReadOnly = True
            txtAmount5.Attributes.Add("onchange", "javascript: ShowTotal();")
            Dim tdtxtAmount5 As New TableCell
            tdtxtAmount5.Controls.Add(txtAmount5)
            tdtxtAmount5.HorizontalAlign = HorizontalAlign.Center
            trx.Cells.Add(tdtxtAmount5)

            Dim txtConAmount5 As New TextBox()
            txtConAmount5.ID = "txtConA05" & myTxtBoxNumber
            txtConAmount5.Width = 40
            txtConAmount5.Attributes.Add("onchange", "javascript: ShowTotal();")
            tdtxtAmount5.Controls.Add(txtConAmount5)
            trx.Cells.Add(tdtxtAmount5)

            'Jun
            Dim txtAmount6 As New Label()
            txtAmount6.ID = "txtA06" & myTxtBoxNumber
            txtAmount6.Width = 40
            txtAmount6.Attributes.Add("onchange", "javascript: ShowTotal();")
            Dim tdtxtAmount6 As New TableCell
            tdtxtAmount6.Controls.Add(txtAmount6)
            tdtxtAmount6.HorizontalAlign = HorizontalAlign.Center
            'trx.Cells.Add(tdtxtAmount6)
            Dim txtConAmount6 As New TextBox()
            txtConAmount6.ID = "txtConA06" & myTxtBoxNumber
            txtConAmount6.Width = 40
            txtConAmount6.Attributes.Add("onchange", "javascript: ShowTotal();")
            tdtxtAmount6.Controls.Add(txtConAmount6)
            trx.Cells.Add(tdtxtAmount6)

            'Jul
            Dim txtAmount7 As New Label()
            txtAmount7.ID = "txtA07" & myTxtBoxNumber
            txtAmount7.Width = 40
            txtAmount7.Attributes.Add("onchange", "javascript: ShowTotal();")
            Dim tdtxtAmount7 As New TableCell
            tdtxtAmount7.Controls.Add(txtAmount7)
            tdtxtAmount7.HorizontalAlign = HorizontalAlign.Center
            'trx.Cells.Add(tdtxtAmount7)
            Dim txtConAmount7 As New TextBox()
            txtConAmount7.ID = "txtConA07" & myTxtBoxNumber
            txtConAmount7.Width = 40
            txtConAmount7.Attributes.Add("onchange", "javascript: ShowTotal();")
            tdtxtAmount7.Controls.Add(txtConAmount7)
            trx.Cells.Add(tdtxtAmount7)

            'Aug
            Dim txtAmount8 As New Label()
            txtAmount8.ID = "txtA08" & myTxtBoxNumber
            txtAmount8.Width = 40
            txtAmount8.Attributes.Add("onchange", "javascript: ShowTotal();")
            Dim tdtxtAmount8 As New TableCell
            tdtxtAmount8.Controls.Add(txtAmount8)
            tdtxtAmount8.HorizontalAlign = HorizontalAlign.Center
            'trx.Cells.Add(tdtxtAmount8)
            Dim txtConAmount8 As New TextBox()
            txtConAmount8.ID = "txtConA08" & myTxtBoxNumber
            txtConAmount8.Width = 40
            txtConAmount8.Attributes.Add("onchange", "javascript: ShowTotal();")
            tdtxtAmount8.Controls.Add(txtConAmount8)
            trx.Cells.Add(tdtxtAmount8)

            'Sep
            Dim txtAmount9 As New Label()
            txtAmount9.ID = "txtA09" & myTxtBoxNumber
            txtAmount9.Width = 40
            txtAmount9.Attributes.Add("onchange", "javascript: ShowTotal();")
            Dim tdtxtAmount9 As New TableCell
            tdtxtAmount9.Controls.Add(txtAmount9)
            tdtxtAmount9.HorizontalAlign = HorizontalAlign.Center
            trx.Cells.Add(tdtxtAmount9)
            Dim txtConAmount9 As New TextBox()
            txtConAmount9.ID = "txtConA09" & myTxtBoxNumber
            txtConAmount9.Width = 40
            txtConAmount9.Attributes.Add("onchange", "javascript: ShowTotal();")
            tdtxtAmount9.Controls.Add(txtConAmount9)
            trx.Cells.Add(tdtxtAmount9)

            'Oct
            Dim txtAmount10 As New Label()
            txtAmount10.ID = "txtA10" & myTxtBoxNumber
            txtAmount10.Width = 40
            txtAmount10.Attributes.Add("onchange", "javascript: ShowTotal();")
            Dim tdtxtAmount10 As New TableCell
            tdtxtAmount10.Controls.Add(txtAmount10)
            tdtxtAmount10.HorizontalAlign = HorizontalAlign.Center
            'trx.Cells.Add(tdtxtAmount10)
            Dim txtConAmount10 As New TextBox()
            txtConAmount10.ID = "txtConA10" & myTxtBoxNumber
            txtConAmount10.Width = 40
            txtConAmount10.Attributes.Add("onchange", "javascript: ShowTotal();")
            tdtxtAmount10.Controls.Add(txtConAmount10)
            trx.Cells.Add(tdtxtAmount10)

            'Nov
            Dim txtAmount11 As New Label()
            txtAmount11.ID = "txtA11" & myTxtBoxNumber
            txtAmount11.Width = 40
            txtAmount11.Attributes.Add("onchange", "javascript: ShowTotal();")
            Dim tdtxtAmount11 As New TableCell
            tdtxtAmount11.Controls.Add(txtAmount11)
            tdtxtAmount11.HorizontalAlign = HorizontalAlign.Center
            'trx.Cells.Add(tdtxtAmount11)
            Dim txtConAmount11 As New TextBox()
            txtConAmount11.ID = "txtConA11" & myTxtBoxNumber
            txtConAmount11.Width = 40
            txtConAmount11.Attributes.Add("onchange", "javascript: ShowTotal();")
            tdtxtAmount11.Controls.Add(txtConAmount11)
            trx.Cells.Add(tdtxtAmount11)

            'Dec
            Dim txtAmount12 As New Label()
            txtAmount12.ID = "txtA12" & myTxtBoxNumber
            txtAmount12.Width = 40
            txtAmount12.Attributes.Add("onchange", "javascript: ShowTotal();")
            Dim tdtxtAmount12 As New TableCell
            tdtxtAmount12.Controls.Add(txtAmount12)
            tdtxtAmount12.HorizontalAlign = HorizontalAlign.Center
            'trx.Cells.Add(tdtxtAmount12)
            Dim txtConAmount12 As New TextBox()
            txtConAmount12.ID = "txtConA12" & myTxtBoxNumber
            txtConAmount12.Width = 40
            txtConAmount12.Attributes.Add("onchange", "javascript: ShowTotal();")
            tdtxtAmount12.Controls.Add(txtConAmount12)
            trx.Cells.Add(tdtxtAmount12)

            'Jan
            Dim txtAmount1 As New Label()
            txtAmount1.ID = "txtA01" & myTxtBoxNumber
            txtAmount1.Width = 40
            txtAmount1.Attributes.Add("onchange", "javascript: ShowTotal();")
            Dim tdtxtAmount1 As New TableCell
            tdtxtAmount1.Controls.Add(txtAmount1)
            tdtxtAmount1.HorizontalAlign = HorizontalAlign.Center
            'trx.Cells.Add(tdtxtAmount1)
            Dim txtConAmount1 As New TextBox()
            txtConAmount1.ID = "txtConA01" & myTxtBoxNumber
            txtConAmount1.Width = 40
            'txtConAmount1.Style.Add(HorizontalAlign.Center, "")
            txtConAmount1.Attributes.Add("onchange", "javascript: ShowTotal();")
            tdtxtAmount1.Controls.Add(txtConAmount1)
            trx.Cells.Add(tdtxtAmount1)

            'Feb
            Dim txtAmount2 As New Label()
            txtAmount2.ID = "txtA02" & myTxtBoxNumber
            txtAmount2.Width = 40
            txtAmount2.Attributes.Add("onchange", "javascript: ShowTotal();")
            Dim tdtxtAmount2 As New TableCell
            tdtxtAmount2.Controls.Add(txtAmount2)
            tdtxtAmount2.HorizontalAlign = HorizontalAlign.Center
            'trx.Cells.Add(tdtxtAmount2)
            Dim txtConAmount2 As New TextBox()
            txtConAmount2.ID = "txtConA02" & myTxtBoxNumber
            txtConAmount2.Width = 40
            txtConAmount2.Attributes.Add("onchange", "javascript: ShowTotal();")
            tdtxtAmount2.Controls.Add(txtConAmount2)
            trx.Cells.Add(tdtxtAmount2)

            'Mar
            Dim txtAmount3 As New Label()
            txtAmount3.ID = "txtA03" & myTxtBoxNumber
            txtAmount3.Width = 40
            txtAmount3.Attributes.Add("onchange", "javascript: ShowTotal();")
            Dim tdtxtAmount3 As New TableCell
            tdtxtAmount3.Controls.Add(txtAmount3)
            tdtxtAmount3.HorizontalAlign = HorizontalAlign.Center
            'trx.Cells.Add(tdtxtAmount3)
            Dim txtConAmount3 As New TextBox()
            txtConAmount3.ID = "txtConA03" & myTxtBoxNumber
            txtConAmount3.Width = 40
            txtConAmount3.Attributes.Add("onchange", "javascript: ShowTotal();")
            tdtxtAmount3.Controls.Add(txtConAmount3)
            trx.Cells.Add(tdtxtAmount3)

            'Total
            Dim txtAmountTotal As New Label()
            txtAmountTotal.ID = "txtTotal" & myTxtBoxNumber
            txtAmountTotal.Width = 40
            txtAmountTotal.Attributes.Add("onchange", "javascript: ShowTotal();")
            Dim tdtxtAmountTotal As New TableCell
            tdtxtAmountTotal.Controls.Add(txtAmountTotal)
            tdtxtAmountTotal.HorizontalAlign = HorizontalAlign.Center
            Dim txtConTotal As New TextBox()
            txtConTotal.ID = "txtConTotal" & myTxtBoxNumber
            txtConTotal.Width = 40
            txtConTotal.Attributes.Add("onchange", "javascript: ShowTotal();")
            tdtxtAmountTotal.Controls.Add(txtConTotal)
            trx.Cells.Add(tdtxtAmountTotal)


            myTable.Rows.Add(trx)

            myTxtBoxNumber += 1
        End While
        myReader.Close()

        'Retrieve Concession Fee Type Config(Given Additionally During Fee Deposit)


        Dim i As Integer = 0, j As Integer = 0, myCount As Integer = 0
        Dim DefaultFeeTotal As Double = 0, OverallConcessionAmount As Double = 0
        Dim lastFeeTypeID As Integer = 0
        Dim DefaultConcessionTotal As Double = 0
        Dim ConcessionEntryCheck As Integer = 0
        ConcessionEntryCheck = GetConcessionEntry(txtSID.Text)
        If ConcessionEntryCheck > 0 Then
            btnRemove.Visible = True
        Else
            btnRemove.Visible = False
        End If
        If (txtConfigType.Text = "1") Then
            sqlStr = "Select sum(FeeAmount),monthID,FeeTypeID  From vw_FeeConfig Where ASID=" & Request.Cookies("ASID").Value & " AND SID=" & Val(txtSID.Text) & " group by monthID,FeeTypeID order by feetypeID,monthID"
        Else
            sqlStr = "Select sum(FeeAmount),monthID,FeeTypeID  From vw_FeeConfig Where ASID=" & Request.Cookies("ASID").Value & " AND FeeGroupID=" & Val(txtFeeGroupID.Text) & " group by monthID,FeeTypeID order by feetypeID,monthID"
        End If


        Dim myreaderConfig As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myreaderConfig.Read
            Dim myFeeAmount As Double = 0
            Dim myConcessionAmount As Double = 0
            Dim myFeeTypeIDtmp As Integer = 0
            Dim MonthID As Integer = 0
            myFeeAmount = myreaderConfig(0)
            myFeeTypeIDtmp = myreaderConfig("FeeTypeID")
            MonthID = myreaderConfig("monthID")
            If lastFeeTypeID <> myFeeTypeIDtmp Then
                DefaultFeeTotal = 0
                DefaultConcessionTotal = 0
                lastFeeTypeID = myFeeTypeIDtmp
            End If
            For i = 1 To myTable.Rows.Count - 1
                Dim myFeeTypeID As Integer = myTable.Rows(i).Cells(0).Text   'Get FeeTypeID From Table
                If myFeeTypeID = myFeeTypeIDtmp Then
                    For j = 1 To 12
                        If myFeeTypeID = txtConveyanceFeeID.Text Or myFeeTypeID = txtLateFeeID.Text Then
                            CType(myTable.FindControl("txtA" & j.ToString("00") & i), Label).Enabled = False
                        Else
                            If MonthID = j Then
                                CType(myTable.FindControl("txtA" & j.ToString("00") & i), Label).Text = myFeeAmount
                                If ConcessionEntryCheck > 0 Then
                                    myConcessionAmount = GetConcessionAmount(txtSID.Text, myFeeTypeID, j)
                                    CType(myTable.FindControl("txtConA" & j.ToString("00") & i), TextBox).Text = myConcessionAmount
                                Else
                                    If cboConcessionType.Text <> "" Then
                                        myConcessionAmount = GetConcessionCheck(cboConcessionType.Text, myFeeTypeID, CType(myTable.FindControl("txtA" & j.ToString("00") & i), Label).Text)
                                        CType(myTable.FindControl("txtConA" & j.ToString("00") & i), TextBox).Text = myConcessionAmount
                                    Else
                                        Try
                                            CType(myTable.FindControl("txtConA" & j.ToString("00") & i), TextBox).Text = "0"
                                            myConcessionAmount = 0
                                        Catch ex As Exception
                                            myConcessionAmount = 0
                                        End Try

                                    End If
                                End If
                                DefaultFeeTotal += myFeeAmount
                                DefaultConcessionTotal += myConcessionAmount
                            End If
                        End If
                    Next
                End If
                If myFeeTypeID = myFeeTypeIDtmp Then
                    CType(myTable.FindControl("txtTotal" & i), Label).Text = DefaultFeeTotal
                    CType(myTable.FindControl("txtConTotal" & i), TextBox).Text = DefaultConcessionTotal
                End If
            Next
        End While
        myreaderConfig.Close()
        btnSave.Visible = True





















        'For i = 1 To myTable.Rows.Count - 1
        '    Dim myFeeTypeID As String = myTable.Rows(i).Cells(0).Text   'Get FeeTypeID From Table
        '    For j = 1 To 12
        '        If myFeeTypeID = txtConveyanceFeeID.Text Or myFeeTypeID = txtLateFeeID.Text Then
        '            CType(myTable.FindControl("txtA" & j.ToString("00") & i), Label).Enabled = False
        '        Else
        '            If txtConfigType.Text = "1" Then
        '                CType(myTable.FindControl("txtA" & j.ToString("00") & i), Label).Text = GetFeeConfigForFeeHead(Request.Cookies("ASID").Value, 0, myFeeTypeID, j, "", Val(txtSID.Text))
        '            Else
        '                CType(myTable.FindControl("txtA" & j.ToString("00") & i), Label).Text = GetFeeConfigForFeeHead(Request.Cookies("ASID").Value, Val(FeeGroupID), myFeeTypeID, j)
        '            End If
        '            If GetConcessionEntry(txtSID.Text) > 0 Then
        '                CType(myTable.FindControl("txtConA" & j.ToString("00") & i), TextBox).Text = GetConcessionAmount(txtSID.Text, myFeeTypeID, j)
        '            Else
        '                CType(myTable.FindControl("txtConA" & j.ToString("00") & i), TextBox).Text = GetConcessionCheck(cboConcessionType.Text, myFeeTypeID, CType(myTable.FindControl("txtA" & j.ToString("00") & i), Label).Text)
        '            End If
        '        End If
        '    Next

        'Next
        'Page.ClientScript.RegisterStartupScript(Me.GetType(), "MyKey", "ShowTotal();", True)

    End Sub
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtMyTable.Text = "" Then
            lblStatus.Text = "Please Select Student fill Concession Fee Details"
            'ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Invalid Fee Book No');", True)
            Exit Sub
        End If

        Dim sqlStr As String = "Delete from FeeStudentConcession where SID=" & txtSID.Text
        ExecuteQuery_Update(sqlStr)

        For i = 1 To myTable.Rows.Count - 1
            Dim myFeeTypeID As String = myTable.Rows(i).Cells(0).Text
            For j = 1 To 12
                Dim myConcessionAmount As Double = Val(CType(myTable.FindControl("txtConA" & j.ToString("00") & i), TextBox).Text)
                If myConcessionAmount > 0 Then
                    sqlStr = "Insert into FeeStudentConcession(SID,FeeTypeID,MonthID,ConcessionAmount,UserID,EntryDate) Values(" & _
         txtSID.Text & "," & _
        myFeeTypeID & "," & _
              j & "," & _
         myConcessionAmount & "," & _
         Request.Cookies("UserID").Value & "," & _
        "'" & System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "')"
                    ExecuteQuery_Update(sqlStr)
                End If
            Next
        Next

        InitControls()
        lblStatus.Text = "Concession has been saved"
    End Sub

    Protected Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        If txtFeeBookNo.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Fee Book No is required...');", True)
            txtFeeBookNo.Focus()
            Exit Sub
        End If
        FillStudentDetail(txtFeeBookNo.Text, 2)
        txtFeeBookNo.Focus()
    End Sub
    Private Sub FillStudentDetail(valNO As String, Type As Integer)
        txtSID.Text = ""
        txtMyTable.Text = ""
        Dim sqlStr As String = ""
        If Type = 1 Then
            sqlStr = "Select * From vw_Student Where RegNo='" & valNO & "' AND ASID=" & Request.Cookies("ASID").Value
        Else
            sqlStr = "Select * From vw_Student Where FeeBookNo='" & valNO & "' AND ASID=" & Request.Cookies("ASID").Value
        End If

        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)

        While myReader.Read
            txtSName.Text = myReader("SName")
            txtFName.Text = myReader("FName")
            txtMName.Text = myReader("MName")
            Try
                txtFeeBookNo.Text = myReader("FeeBookNo")
            Catch ex As Exception

            End Try
            Try
                txtRegNo.Text = myReader("RegNo")
            Catch ex As Exception

            End Try
            Dim tmpDate As Date = myReader("AdmissionDate")
            'txtAdmissionDate.Text = a.Substring(3, 2) & "/" & a.Substring(0, 2) & "/" & a.Substring(6, 4)
            txtAdmissionDate.Text = tmpDate.ToString("dd/MM/yyyy")
            tmpDate = myReader("DOB")
            'txtDOB.Text = a.Substring(3, 2) & "/" & a.Substring(0, 2) & "/" & a.Substring(6, 4)
            txtDOB.Text = tmpDate.ToString("dd/MM/yyyy")
            txtClass.Text = myReader("ClassName")
            txtSec.Text = myReader("SecName")
            txtSID.Text = myReader("SID")
            txtFeeGroupID.Text = myReader("FeeGroupID")
            Try
                txtConfigType.Text = myReader("FeeConfigType")
            Catch ex As Exception
                txtConfigType.Text = "0"
            End Try
        End While
        myReader.Close()
        If txtSID.Text = "" Then
            lblStatus.Text = "Invalid Admin No."
        Else
            lblStatus.Text = ""
        End If
        'CreateTable()
    End Sub

    Protected Sub GridView2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView2.SelectedIndexChanged
        txtFeeBookNo.Text = GridView2.SelectedRow.Cells(1).Text
        FillStudentDetail(txtFeeBookNo.Text, 2)
        GridView2.Visible = False
    End Sub

    Protected Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If txtSID.Text = "" Then
            lblStatus.Text = "Please Select Student"
            'ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Error", "alert('Invalid Fee Book No');", True)
            Exit Sub
        Else
            txtMyTable.Text = "1"
        End If
        lblStatus.Text = ""
        CreateTable()
    End Sub

    Protected Sub btnNameSearch_Click(sender As Object, e As EventArgs) Handles btnNameSearch.Click
        GridView2.Visible = True
        SqlDataSource2.SelectCommand = "SELECT * FROM vw_Student WHERE ASID = " & Request.Cookies("ASID").Value & " and SName Like '%" & txtSName.Text & "%'"
        GridView2.DataBind()
    End Sub

    Protected Sub btnFindRegNo_Click(sender As Object, e As EventArgs) Handles btnFindRegNo.Click
        If txtRegNo.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Reg. No is required...');", True)
            txtRegNo.Focus()
            Exit Sub
        End If
        FillStudentDetail(txtRegNo.Text, 1)
        txtRegNo.Focus()
    End Sub

    Protected Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        If txtSID.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please select Student...');", True)
            txtRegNo.Focus()
            Exit Sub
        End If
        Dim sqlStr As String = "delete from FeeStudentConcession where SID=" & txtSID.Text
        ExecuteQuery_Update(sqlStr)
        InitControls()
        lblStatus.Text = "Concession entry has been Removed..."
    End Sub
End Class