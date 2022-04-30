Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary_Fee.CLS_iDiary_Fee


Public Class ConveyanceCategoryConfig
    Inherits System.Web.UI.Page


    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
       
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            InitControls()
        Else
            If cboConveyaceTypes.Text <> "" Then
                CreateTable()
            Else
                myTable.Rows.Clear()
            End If
        End If
    End Sub

    Private Sub InitControls()
        'LoadFeeTerms(cboTermNo)
        'LoadMasterInfo(68, cboTermNo)
        'lblTerm.Text = ""
        'txtDD.Text = ""
        'txtMM.Text = ""
        'txtYY.Text = ""
        LoadMasterInfo(69, cboConveyaceTypes)
        'txtAmount.Text = ""
        'txtID.Text = ""
        'optMonthly.Checked = True
        'optDaily.Checked = False
        lblStatus.Text = ""
        myTable.Rows.Clear()
    End Sub

    'Protected Sub cboTermNo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTermNo.SelectedIndexChanged
    '    lblTerm.Text = LoadFeeTermCaption(Val(cboTermNo.Text))
    '    LoadAmount()
    '    cboConveyaceTypes.Focus()
    'End Sub

    Private Sub LoadAmount()
        Dim sqlStr As String = ""
        Dim myCount As Integer = 0

        txtID.Text = ""
        'txtAmount.Text = ""

        'sqlStr = "Select CHCId,Amount From vw_BusHeadTypeConfig Where TermNo=" & Val(cboTermNo.Text) & " and ConveyanceTypeName='" & cboConveyaceTypes.Text & "' and ASID =" & Request.Cookies("ASID").Value
        
        ' Dim tDate As Date
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            myCount += 1
            
            txtID.Text = myReader("CHCId")
            'txtAmount.Text = myReader("Amount")

        End While
        myReader.Close()

        lblStatus.Text = ""
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        'If cboTermNo.SelectedIndex = 0 Then
        '    lblStatus.Text = "Invalid Term..."
        '    cboTermNo.Focus()
        '    Exit Sub
        'End If

        If cboConveyaceTypes.SelectedIndex = 0 Then
            lblStatus.Text = "Select a FeeType..."
            cboConveyaceTypes.Focus()
            Exit Sub
        End If

        'If IsNumeric(txtAmount.Text) = False Then
        '    lblStatus.Text = "Invalid Amount..."
        '    txtAmount.Focus()
        '    Exit Sub
        'End If
        Dim sqlStr As String = ""
        Dim TermID As String = ""
        Dim Amount As Decimal = 0
        Dim ConveyanceHeadTypeID As Integer = FindMasterID(69, cboConveyaceTypes.Text)
        sqlStr = "delete from BusHeadTypeConfig where ASID='" & Request.Cookies("ASID").Value & "', and ConveyanceHeadTypeID=" & ConveyanceHeadTypeID
        ExecuteQuery_Update(sqlStr)

        For i = 1 To myTable.Rows.Count - 1
            TermID = FindMasterID(67, myTable.Rows(i).Cells(0).Text)
            Amount = CType(myTable.FindControl("txtAmount" & i), TextBox).Text
            sqlStr = "insert into BusHeadTypeConfig(ASID,TermNo,ConveyanceHeadTypeID,Amount) Values(" & Request.Cookies("ASID").Value & "," & _
TermID & "," & _
        ConveyanceHeadTypeID & "," & _
        Amount & ")"
            ExecuteQuery_Update(sqlStr)
        Next

        'Dim TermID As String = 1

        'FindMasterID(67, cboTermNo.Text)



        'If txtID.Text = "" Then     'Insert Command
        'sqlStr = "Insert into BusHeadTypeConfig Values(" & Request.Cookies("ASID").Value & "," & _
        'TermID & "," & _
        'ConveyanceHeadTypeID & "," & _
        'Amount & ")"

        'Else                        'Update Command
        'sqlStr = "Update BusHeadTypeConfig Set " & _
        '"Amount=" & Val(txtAmount.Text) & "" & _
        '" Where ASID=" & Request.Cookies("ASID").Value & " AND " & _
        '" TermNo=" & TermID & " ANd ConveyanceHeadTypeID=" & ConveyanceHeadTypeID
        'End If

        InitControls()
        lblStatus.Text = "Conveyance Fee Configuration saved successfully..."
        cboConveyaceTypes.Focus()
    End Sub

    Protected Sub cboConveyaceTypes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboConveyaceTypes.SelectedIndexChanged
        'LoadAmount()
        If cboConveyaceTypes.Text <> "" Then
            CreateTable()
        Else
            myTable.Rows.Clear()
        End If
    End Sub
    Private Sub CreateTable()
        Dim ConveyanceHeadTypeID As Integer = FindMasterID(69, cboConveyaceTypes.Text)
        myTable.Rows.Clear()

        Dim tr1 As New TableRow

        Dim td10 As New TableCell
        td10.Text = "<B>Term No</B>"
        td10.HorizontalAlign = HorizontalAlign.Center
        tr1.Cells.Add(td10)

        Dim td11 As New TableCell
        td11.Text = "<B>Fee Amount</B>"
        td11.HorizontalAlign = HorizontalAlign.Center
        tr1.Cells.Add(td11)

        'Dim td12 As New TableCell
        'td12.Text = "<B>Total</B>"
        'td12.HorizontalAlign = HorizontalAlign.Center
        'tr1.Cells.Add(td12)

        myTable.Rows.Add(tr1)

        Dim sqlStr As String = ""
        Dim myTxtBoxNumber As Integer = 1

        'Process Late Fee Amount and Type
        'Dim lstLateFeeTypeID As New ListBox, lstLateFeeAmount As New ListBox
        Dim t As Integer = 0

        sqlStr = "Select BusTermNo From BusTermMaster order by DispOrder"
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)

        While myReader.Read
            Dim trx As New TableRow

            Dim tdx0 As New TableCell
            tdx0.Text = myReader(0)
            tdx0.HorizontalAlign = HorizontalAlign.Center
            trx.Cells.Add(tdx0)

            Dim txtAmount As New TextBox()
            txtAmount.ID = "txtAmount" & myTxtBoxNumber
            txtAmount.Width = 120
            txtAmount.Attributes.Add("onchange", "javascript: ShowTotal();")
            Dim tdtxtAmount4 As New TableCell
            tdtxtAmount4.Controls.Add(txtAmount)
            tdtxtAmount4.HorizontalAlign = HorizontalAlign.Center
            trx.Cells.Add(tdtxtAmount4)

            ''Total
            'Dim txtAmountTotal As New TextBox()
            'txtAmountTotal.ID = "txtTotal" & myTxtBoxNumber
            'txtAmountTotal.Width = 120
            'txtAmountTotal.Attributes.Add("onchange", "javascript: ShowTotal();")
            'Dim tdtxtAmountTotal As New TableCell
            'tdtxtAmountTotal.Controls.Add(txtAmountTotal)
            'tdtxtAmountTotal.HorizontalAlign = HorizontalAlign.Center
            'trx.Cells.Add(tdtxtAmountTotal)

            myTable.Rows.Add(trx)

            myTxtBoxNumber += 1
        End While
        myReader.Close()

        For i = 1 To myTable.Rows.Count - 1
            Dim TermID As String = FindMasterID(67, myTable.Rows(i).Cells(0).Text)
            sqlStr = "Select * from BusHeadTypeConfig where TermNo='" & TermID & "' and ConveyanceHeadTypeID=" & ConveyanceHeadTypeID
            myReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                CType(myTable.FindControl("txtAmount" & i), TextBox).Text = myReader("Amount")
            End While
        Next
    End Sub
End Class