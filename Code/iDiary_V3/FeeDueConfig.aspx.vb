Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary_Fee.CLS_iDiary_Fee

Public Class FeeDueConfig
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
        Response.Cookies("ActiveTab").Value = 3
        If IsPostBack = False Then
            InitControls()
        End If
    End Sub

    Private Sub InitControls()
        LoadMasterInfo(60, cboFeeGroup)
        cboTermNo.Items.Clear()
        lblTerm.Text = ""
        txtDepositDate.Text = ""
        
        txtAmount.Text = ""
        optFixed.Checked = True
        btnRemove.Visible = False
        lblStatus.Text = ""
        cboFeeGroup.Focus()
        GridView1.DataBind()
    End Sub

    Protected Sub cboTermNo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTermNo.SelectedIndexChanged
        Dim FeeGroupID As Integer = FindMasterID(60, cboFeeGroup.Text)
        lblTerm.Text = LoadFeeTermCaption(LoadTermID(cboTermNo.Text, FeeGroupID))
        txtID.Text = ""
        txtDepositDate.Text = ""
        'cboFeeTypes.SelectedIndex = 0
        txtAmount.Text = ""
        lblStatus.Text = ""
        FillGrid(Request.Cookies("ASID").Value)
       
    End Sub

    Private Sub LoadDueConfig(ByVal ID As Integer)
        Dim sqlStr As String = ""
        Dim myCount As Integer = 0

        txtID.Text = ""
        txtDepositDate.Text = ""

        txtAmount.Text = ""

        sqlStr = "Select * From vwFeeDueConfig Where DueConfigID=" & ID
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            myCount += 1
            txtID.Text = myReader("DueConfigID")
            Dim tmpDate As Date = myReader("LastDate")
            txtDepositDate.Text = tmpDate.ToString("dd/MM/yyyy")
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
        lblStatus.Text = ""
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If cboFeeGroup.SelectedIndex = 0 Then
            lblStatus.Text = "Select Fee Group..."
            cboFeeGroup.Focus()
            Exit Sub
        End If
        If cboTermNo.SelectedIndex = 0 Then
            lblStatus.Text = "Invalid Term..."
            cboTermNo.Focus()
            Exit Sub
        End If
        Dim DepositDate As Date = Now.Date
        Try
            DepositDate = txtDepositDate.Text.Substring(6, 4) & "/" & txtDepositDate.Text.Substring(3, 2) & "/" & txtDepositDate.Text.Substring(0, 2)
        Catch ex As Exception
            lblStatus.Text = "Invalid Date..."
            txtDepositDate.Focus()
            Exit Sub
        End Try
        
        If IsNumeric(txtAmount.Text) = False Then
            lblStatus.Text = "Invalid Amount..."
            txtAmount.Focus()
            Exit Sub
        End If

        'Dim LateFeeTypeID As Integer = FindMasterID(11, cboFeeTypes.Text)
        Dim ProcessingMethod As Integer = 1
        If optMonthly.Checked Then    ' 1 means Monthy
            ProcessingMethod = 1
        ElseIf optDaily.Checked Then
            ProcessingMethod = 2      ' 2 means Daily
        ElseIf optFixed.Checked Then
            ProcessingMethod = 3      ' 3 Means Fixed
        End If
        lblStatus.Text = ""
        Dim FeeGroupID As Integer = FindMasterID(60, cboFeeGroup.Text)
        '0
        Dim TermId As String = LoadTermID(cboTermNo.Text, FeeGroupID)

        Dim sqlStr As String = ""

        If txtID.Text = "" Then     'Insert Command Val(txtDepositDate.Text)
            sqlStr = "Insert into FeeDueConfig(ASID,TermId,LastDate,LateFeeAmount,ProcessingMethod,FeeGroupID) Values(" & Request.Cookies("ASID").Value & "," & _
            TermId & "," & _
            "'" & DepositDate.ToString("yyyy/MM/dd") & "'," & _
                        Val(txtAmount.Text) & "," & _
            ProcessingMethod & "," & _
                     FeeGroupID & ")"

        Else                        'Update Command
            sqlStr = "Update FeeDueConfig Set " & _
            "LastDate='" & DepositDate.ToString("yyyy/MM/dd") & "'," & _
                        "LateFeeAmount=" & Val(txtAmount.Text) & "," & _
            "ProcessingMethod=" & ProcessingMethod & "," & _
            "FeeGroupID=" & FeeGroupID & " " & _
                       " Where DueConfigID=" & txtID.Text
            'ASID=" & Request.Cookies("ASID").Value & " AND " & _

        End If
        ExecuteQuery_Update(sqlStr)

        txtDepositDate.Text = ""
        txtAmount.Text = ""
        txtID.Text = ""
        btnRemove.Visible = False
        lblStatus.Text = "Due Configuration saved successfully..."
        FillGrid(Request.Cookies("ASID").Value)
        cboTermNo.Focus()
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        btnRemove.Visible = True
        txtID.Text = GridView1.DataKeys(GridView1.SelectedRow.RowIndex).Value.ToString()
        'txtID.Text = GridView1.SelectedRow.Cells(3).Text()
        LoadDueConfig(txtID.Text)
    End Sub

    Protected Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        InitControls()
        txtID.Text = ""
    End Sub

    Protected Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        Dim sqlstr As String = ""
        sqlstr = "Delete from FeeDueConfig Where DueConfigID='" & txtID.Text & "'"
ExecuteQuery_Update(sqlstr)

       
        txtDepositDate.Text = ""
        txtAmount.Text = ""
        txtID.Text = ""
        FillGrid(Request.Cookies("ASID").Value)
    End Sub
    Private Sub FillGrid(ByVal ASID As Integer)
        SqlDataSource1.SelectCommand = "SELECT [LastDate],  [LateFeeAmount], [DueConfigID]FROM [vwFeeDueConfig] WHERE TermNo = '" & cboTermNo.Text & "' and FeeGroupName = '" & cboFeeGroup.Text & "'  AND ASID=" & ASID
        GridView1.DataBind()
    End Sub
    Protected Sub cboSection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboFeeGroup.SelectedIndexChanged
        Dim FeeGroupID As Integer = FindMasterID(60, cboFeeGroup.Text)
        LoadFeeTerms(cboTermNo, FeeGroupID)
        FillGrid(Request.Cookies("ASID").Value)
        lblStatus.Text = ""
        cboTermNo.Focus()
    End Sub

    Protected Sub btnimportPrevDue_Click(sender As Object, e As EventArgs) Handles btnimportPrevDue.Click
        Dim count As Integer = ExecuteQuery_ExecuteScalar("Select Count(*)  From vwFeeDueConfig Where TermNo = '" & cboTermNo.Text & "' and FeeGroupName = '" & cboFeeGroup.Text & "'  AND ASID=" & Request.Cookies("ASID").Value & "")
        If count <= 0 Then
            Dim PreviousASID As Integer = ExecuteQuery_ExecuteScalar("select Top(1) ASID from AcademicSession where ASID<" & Request.Cookies("ASID").Value & " order by ASID DESC ")
            FillGrid(PreviousASID)
        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('You have already configure Dues for selected Fee Group...');", True)
            cboFeeGroup.Focus()
            Exit Sub
        End If
    End Sub
End Class