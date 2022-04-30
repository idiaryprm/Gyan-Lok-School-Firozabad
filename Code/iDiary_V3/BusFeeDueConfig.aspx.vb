Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary_Fee.CLS_iDiary_Fee


Public Class BusFeeDueConfig
    Inherits System.Web.UI.Page


    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Fee") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
            End If
        Catch ex As Exception
            response.redirect("~/Login.aspx")
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            InitControls()
        End If
    End Sub

    Private Sub InitControls()
        LoadFeeTerms(cboTermNo, 1, "BusFee")
        lblTerm.Text = ""
        txtDepositDate.Text = ""
        'LoadMasterInfo(11, cboFeeTypes)
        txtAmount.Text = ""
        'optMonthly.Checked = True
        'optDaily.Checked = False
        GridView1.DataBind()
        lblStatus.Text = ""
        txtDepositDate.Text = Now.Date.ToString("dd/MM/yyyy")
        cboTermNo.Focus()
    End Sub

    Protected Sub cboTermNo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTermNo.SelectedIndexChanged
        Dim TermID As Integer = cboTermNo.SelectedValue
        'FindMasterID(67, cboTermNo.Text)
        lblTerm.Text = LoadFeeTermCaption(TermID)
        FillGrid(TermID)
        'LoadDueConfig()
        cboTermNo.Focus()
    End Sub

    Private Sub LoadDueConfig(ByVal ID As Integer)
        Dim sqlStr As String = ""
        Dim myCount As Integer = 0

        txtID.Text = ""
        






        sqlStr = "Select * From BusFeeDueConfig Where BusDueConfigID=" & ID


        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            myCount += 1
            txtID.Text = myReader("BusDueConfigID")
            Dim tmpDate As Date = myReader("LastDate")
            txtDepositDate.Text = tmpDate.ToString("dd/MM/yyyy")
            'cboFeeTypes.Text = myReader("FeeTypeName")
            txtAmount.Text = myReader("LateFeeAmount")
            If myReader("ProcessingMethod") = 1 Then
                optMonthly.Checked = True
            ElseIf myReader("ProcessingMethod") = 2 Then
                optDaily.Checked = True
            Else
                optFixed.Checked = True
            End If
        End While
        myReader.Close()



        'fillfeehead()

        lblStatus.Text = ""
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If cboTermNo.Text = "" Then
            lblStatus.Text = "Please Select Term No..."
            cboTermNo.Focus()
            Exit Sub
        End If

        'If cboFeeTypes.SelectedIndex = 0 Then
        '    lblStatus.Text = "Select a FeeType..."
        '    cboFeeTypes.Focus()
        '    Exit Sub
        'End If
        Dim DepositDate As Date = Now.Date
        Try
            DepositDate = CDate(txtDepositDate.Text.Substring(6, 4) & "/" & txtDepositDate.Text.Substring(3, 2) & "/" & txtDepositDate.Text.Substring(0, 2))
        Catch ex As Exception
            lblStatus.Text = "Please Check Date Formate(dd/MM/yyyy)..."
            txtDepositDate.Focus()
            Exit Sub
        End Try
        If IsNumeric(txtAmount.Text) = False Then
            lblStatus.Text = "Please Enter Valid Amount..."
            txtAmount.Focus()
            Exit Sub
        End If
       
        'Dim LateFeeTypeID As Integer = FindMasterID(11, cboFeeTypes.Text)
        Dim ProcessingMethod As Integer = 1
        If optMonthly.Checked Then
            ProcessingMethod = 1
        ElseIf optDaily.Checked Then
            ProcessingMethod = 2
        Else
            ProcessingMethod = 3
        End If
        Dim TermID As Integer = cboTermNo.SelectedValue
        'FindMasterID(67, cboTermNo.Text)
        Dim sqlStr As String = ""

        If txtID.Text = "" Then     'Insert Command
            sqlStr = "Insert into BusFeeDueConfig Values(" & Request.Cookies("ASID").Value & "," & _
            TermID & "," & _
            "'" & DepositDate.ToString("yyyy/MM/dd") & "'," & _
            Val(txtAmount.Text) & "," & _
            ProcessingMethod & ")"

        Else                        'Update Command
            sqlStr = "Update BusFeeDueConfig Set " & _
            "LastDate='" & DepositDate.ToString("yyyy/MM/dd") & "'," & _
"TermNo=" & TermID & "," & _
"LateFeeAmount=" & Val(txtAmount.Text) & "," & _
            "ProcessingMethod=" & ProcessingMethod & _
            " Where BusDueConfigID=" & txtID.Text

        End If








        ExecuteQuery_Update(SqlStr)




        'InitControls()
        FillGrid(TermID)
        txtAmount.Text = ""
        'txtDepositDate.Text = ""
        txtID.Text = ""
        lblStatus.Text = "Bus Fine Configuration saved successfully..."
        btnRemove.Visible = False
        cboTermNo.Focus()
    End Sub
    Private Sub FillGrid(TermID)
        SqlDataSource1.SelectCommand = "SELECT [LastDate], [LateFeeAmount], [BusDueConfigID]FROM [BusFeeDueConfig] WHERE [TermNo] = " & TermID
        GridView1.DataBind()
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        btnRemove.Visible = True
        'txtID.Text = GridView1.DataKeys(GridView1.SelectedRow.RowIndex).ToString()
        txtID.Text = GridView1.DataKeys(GridView1.SelectedIndex).Value
        'txtID.Text = GridView1.SelectedRow.Cells(3).Text()
        'GridView1.SelectedRow.Cells(3).Text()
        LoadDueConfig(txtID.Text)
    End Sub

    Protected Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        InitControls()
    End Sub

    Protected Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        If txtID.Text = "" Then
            lblStatus.Text = "Please Select Term No to Remove..."
            cboTermNo.Focus()
            Exit Sub
        End If
        Dim sqlStr As String = ""






        sqlStr = "Delete From BusFeeDueConfig Where BusDueConfigID=" & txtID.Text


        ExecuteQuery_Update(SqlStr)


        Dim TermID As Integer = cboTermNo.SelectedValue
        'FindMasterID(67, cboTermNo.Text)
        'fillfeehead()
        lblStatus.Text = "Configuration has been Deleted"
        txtID.Text = ""
        txtAmount.Text = ""
        'txtDepositDate.Text = ""
        FillGrid(TermID)
        btnRemove.Visible = False
        'GridView1.DataBind()
    End Sub
End Class