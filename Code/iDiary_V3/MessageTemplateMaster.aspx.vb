Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Public Class MessageTemplate
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
            End If
        Catch ex As Exception
            Response.Redirect("~/Login.aspx")
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
        If Request.Cookies("UType").Value.ToString.Contains("Admin-1") = False Then
            btnSave.Enabled = False
        End If
    End Sub

    Private Sub InitControls()
        txtAPIName.Text = ""
        txturl.Text = ""
        txtID.Text = ""
        txtAPIName.Enabled = True
        lblStatus.Text = ""
        chkDefault.Checked = False
        txtAPIName.Focus()
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        InitControls()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtAPIName.Text = "" Then
            lblStatus.Text = "Please Provide Meaasge Code..."
            txtAPIName.Focus()
            Exit Sub
        End If
        If txturl.Text = "" Then
            lblStatus.Text = "Please Provide Meaasge Details..."
            txturl.Focus()
            Exit Sub
        End If
        Dim URL As String = txturl.Text
        If URL.Contains("<*>") = False Then

            lblStatus.Text = "<*> is required..."
            txturl.Focus()
            Exit Sub
        End If
        'If URL.Contains("<**>") = False Then

        '    lblStatus.Text = "<**> is required for Message..."
        '    txturl.Focus()
        '    Exit Sub
        'End If
        If txtID.Text = "" Then   'Only for Insert Case
            If CheckDoubleEntry(107, txtAPIName.Text) > 0 Then
                lblStatus.Text = "Same Code allready Exist..."
                txtAPIName.Focus()
                Exit Sub
            End If
            'If CheckDoubleEntry(106, txturl.Text) > 0 Then
            '    lblStatus.Text = "Same URL allready Exist..."
            '    txturl.Focus()
            '    Exit Sub
            'End If
        End If

        Dim sqlStr As String = ""
        'Dim IsDefault As Integer = 0
        'If chkDefault.Checked = True Then
        '    IsDefault = 1
        '    sqlStr = "Update MessageTemplate Set IsDefault=0"


        '    ExecuteQuery_Update(sqlStr)
        'End If


        If txtID.Text = "" Then
            sqlStr = "Insert into MessageTemplates(MessageSubject,MessageTemplateDesc) Values (" & _
        "'" & SQLFixup(txtAPIName.Text) & "'," & _
        "'" & SQLFixup(txturl.Text) & "'" & _
        ")"
        Else
            sqlStr = "Update MessageTemplates Set MessageTemplateDesc='" & SQLFixup(txturl.Text) & "' where MessageTemplateID=" & txtID.Text & ""

        End If

        ExecuteQuery_Update(sqlStr)


        InitControls()
        lblStatus.Text = "Message successfully Saved..."
        gvSMSAPI.DataBind()
    End Sub

    Protected Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        'Dim sqlStr As String = ""
        'sqlStr = "Delete From SMSAPIMaster Where SMSAPIID=" & gvSMSAPI.SelectedRow.Cells(1).Text
        'ExecuteQuery_Update(sqlStr)
        'gvSMSAPI.DataBind()
        'InitControls()
    End Sub

    Protected Sub gvSMSAPI_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvSMSAPI.SelectedIndexChanged
        txtID.Text = gvSMSAPI.SelectedRow.Cells(1).Text
        txtAPIName.Text = Trim(gvSMSAPI.SelectedRow.Cells(2).Text)
        txturl.Text = RemoveHTMLTags(gvSMSAPI.SelectedRow.Cells(3).Text)
        txtAPIName.Enabled = False
        'Dim sqlStr As String = ""
        'sqlStr = "select IsDefault from SMSAPIMaster where SMSAPIID=" & txtID.Text & ""
        'Dim defaultvalue As Integer = ExecuteQuery_ExecuteScalar(sqlStr)
        'If defaultvalue = 1 Then
        '    chkDefault.Checked = True
        'Else
        '    chkDefault.Checked = False
        'End If
    End Sub
    Public Function RemoveHTMLTags(ByVal HTMLCode As String) As String
        Dim Result1 As String = System.Text.RegularExpressions.Regex.Replace(HTMLCode, "&gt;", ">")
        Dim Result2 As String = System.Text.RegularExpressions.Regex.Replace(Result1, "&lt;", "<")
        Dim Result3 As String = System.Text.RegularExpressions.Regex.Replace(Result2, "&amp;", "&")
        Dim Result4 As String = System.Text.RegularExpressions.Regex.Replace(Result3, "&#39;", "'")

        Return Result4
    End Function
End Class