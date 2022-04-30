Imports iDiary_V3.iDiary.CLS_idiary

Public Class ExamAbbreviation
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then Initcontrols()
    End Sub

    Private Sub Initcontrols()
        txtAbbName.Text = ""
        cboAbbType.SelectedIndex = 0
        GridView1.DataBind()
        txtID.Text = ""
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtAbbName.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Enter Abbreviation Name..');", True)
            txtAbbName.Focus()
            Exit Sub
        End If
        Dim sqlstr As String = ""
        If txtID.Text = "" Then
            If CheckDuplicateEntry(txtAbbName.Text, "ExamAbbreviation", "AbbName") = 0 Then
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Abbreviation Already Exists..');", True)
                Exit Sub
            End If
            sqlstr = "Insert Into ExamAbbreviation (AbbName, AbbType) Values('" & txtAbbName.Text & "', '" & cboAbbType.SelectedIndex & "')"
        Else
            sqlstr = "Update ExamAbbreviation Set AbbName='" & txtAbbName.Text & "', AbbType='" & cboAbbType.SelectedIndex & "' Where AbbID=" & txtID.Text & ""
        End If
        ExecuteQuery_Update(sqlstr)
        Initcontrols()
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Abbreviation Added Successfully..');", True)
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        txtID.Text = GridView1.SelectedDataKey(0)
        txtAbbName.Text = GridView1.SelectedRow.Cells(1).Text
        cboAbbType.Text = GridView1.SelectedRow.Cells(2).Text
    End Sub

    Protected Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Initcontrols()
    End Sub
End Class