Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Partial Class Admin_Updates
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
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtActivityDetails.Text.Length <= 0 Then
            lblStatus.Text = "Please Provide Details..."
            txtActivityDetails.Focus()
            Exit Sub
        End If
        If txtDate.Text.Length <= 0 Then
            lblStatus.Text = "Please Provide Activity Date..."
            txtDate.Focus()
            Exit Sub
        End If
        If myFile.PostedFile.FileName.ToString.Length <= 0 Then
            lblStatus.Text = "Please select a file..."
            myFile.Focus()
            Exit Sub
        End If
        SaveUpcomingActivitiesToDB()
        InitControls()
        lblStatus.Text = "Upcoming activity Saved..."
        gvActivities.DataBind()
    End Sub
    Private Sub SaveUpcomingActivitiesToDB()

       
        
       

        
        Dim sqlStr As String = ""

        sqlStr = "Insert into UpcomingActivities Values (" & _
        "'" & txtDate.Text.Substring(6, 4) & "/" & txtDate.Text.Substring(3, 2) & "/" & txtDate.Text.Substring(0, 2) & "'," & _
        "'" & txtActivityDetails.Text & "'," & _
        "'" & myFile.PostedFile.FileName & "')"

        
        
        ExecuteQuery_Update(SqlStr)

        
        

        UploadActivityFile()
    End Sub
    Private Sub UploadActivityFile()
        Dim fp1 As String = myFile.PostedFile.FileName
        If fp1.ToString() <> "" Then
            Dim fn1 As String = fp1.Substring(fp1.LastIndexOf("\\") + 1)
            Dim sp1 As String = ""
            sp1 = Server.MapPath("~/Activity")
            If sp1.EndsWith("\\") = False Then
                sp1 += "\"
            End If

            sp1 += fp1
            myFile.PostedFile.SaveAs(sp1)
        End If

    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
    End Sub

    Private Sub InitControls()
        txtDate.Text = Now.Date.ToString("dd/MM/yyyy")
        txtActivityDetails.Text = ""

        'lblStatus.Text = ""
        txtActivityDetails.Focus()
    End Sub
End Class
