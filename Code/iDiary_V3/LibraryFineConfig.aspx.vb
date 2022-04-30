Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class LibraryFineConfig
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Request.Cookies("UType").Value.ToString.Contains("Library") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
            'Allow
        Else
            Response.Redirect("AccessDenied.aspx")
        End If
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            InitControls()
        End If
    End Sub

    Private Sub InitControls()
        LoadMasterInfo(2, cboClass)
        Dim ObjLib As New iDiary.clsLibrary
        'ObjLib.LoadNewsPaperAsList(lstNewsPapers)
        'ObjLib.LoadFrequencyAsDropDown(cboFrequency)
        txtBookLimit.Text = ""
        txtDayLimit.Text = ""
        txtAmountFine.Text = ""
        txtCategoryID.Text = ""
        'Dim rv As Integer = ObjLib.GetNewRackID()
        'txtRackID.Text = rv
        lblStatus.Text = ""
        txtBookLimit.Focus()
        fillData()
        ObjLib = Nothing
    End Sub

    'Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
    '    Dim ObjLib As New iDiary.clsLibrary
    '    txtNewsPaperName.Text = ""
    '    Dim rv As Integer = ObjLib.GetNewRackID()
    '    txtRackID.Text = rv
    '    objlib = Nothing
    'End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Trim(txtBookLimit.Text) = "" Then
            lblStatus.Text = "No. of Books limit is Empty..."
            txtBookLimit.Focus()
            Exit Sub
        End If
        If Trim(txtDayLimit.Text) = "" Then
            lblStatus.Text = "No. of Days limit is Empty..."
            txtDayLimit.Focus()
            Exit Sub
        End If
        If Trim(txtAmountFine.Text) = "" Then
            lblStatus.Text = "Fine Amount is Empty..."
            txtDayLimit.Focus()
            Exit Sub
        End If
        If Trim(cboClass.Text) = "" And cboCategory.SelectedIndex = 0 Then
            lblStatus.Text = "No Class selected..."
            cboClass.Focus()
            Exit Sub
        End If
        'If Trim(cboFrequency.Text) = "" Then
        '    lblStatus.Text = "News Paper frequency is Empty..."
        '    cboFrequency.Focus()
        '    Exit Sub
        'End If





        Dim sqlstr As String = ""

        Dim FinalMessage As String = ""
        Dim FrequencyID As Integer = 0
        Dim ObjLib As New iDiary.clsLibrary
        'FrequencyID = ObjLib.GetFrequencyID(cboFrequency.Text)
        Dim classID As Integer = 0
        Try
            classID = FindMasterID(2, cboClass.Text)
        Catch ex As Exception

        End Try
        If txtCategoryID.Text = "" Then
            sqlstr = "Insert into LibraryFineConfig Values(" & Val(txtBookLimit.Text) & "," & Val(txtDayLimit.Text) & "," & txtAmountFine.Text & "," & cboCategory.SelectedIndex & "," & classID & ")"


            ExecuteQuery_Update(sqlstr)
            FinalMessage = "Configuration successfully added..."
        Else
            sqlstr = "Update LibraryFineConfig Set limitBook='" & Val(txtBookLimit.Text) & "',limitDay=" & Val(txtDayLimit.Text) & ",AmountFine=" & txtAmountFine.Text & " Where Category=" & Val(txtCategoryID.Text) & " AND classID=" & classID


            ExecuteQuery_Update(sqlstr)
            FinalMessage = "Configuration successfully updated..."
        End If

        'Catch ex As Exception
        '    If ex.Message.Contains("duplicate") Then

        '    End If
        'End Try




        InitControls()
        lblStatus.Text = FinalMessage
    End Sub


    Public Sub fillData()
        Dim ObjLib As New iDiary.clsLibrary
        Dim classID As Integer = 0
        Try
            classID = FindMasterID(2, cboClass.Text)
        Catch ex As Exception

        End Try
        txtBookLimit.Text = ObjLib.GetBookLimit(cboCategory.SelectedIndex, classID)
        txtDayLimit.Text = ObjLib.GetDayLimit(cboCategory.SelectedIndex, classID)
        txtAmountFine.Text = ObjLib.GetAmountFine(cboCategory.SelectedIndex, classID)
        If txtBookLimit.Text <> "0" And txtDayLimit.Text <> "0" And txtAmountFine.Text <> "0" Then
            txtCategoryID.Text = cboCategory.SelectedIndex
        Else
            txtCategoryID.Text = ""
        End If

        'cboFrequency.Text = ObjLib.GetNewsPaperFrequency(lstNewsPapers.Text)
        lblStatus.Text = ""
        ObjLib = Nothing
    End Sub

    Protected Sub cboCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCategory.SelectedIndexChanged
        ' fillData()
    End Sub

    Protected Sub cboClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboClass.SelectedIndexChanged
        fillData()
    End Sub

End Class
