Imports System.Data.SqlClient

Partial Class Parent_ParentLogin
    Inherits System.Web.UI.Page

    Protected Sub btnSignIn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSignIn.Click

        'Parent Logins
        'Check existance
        'Find School and Children Details
        'GoTo Parent Panel Page

        Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        Dim myConn As New SqlConnection(myConnStr)
        myConn.Open()

        Dim myCommand As New SqlCommand
        Dim sqlStr As String = ""

        sqlStr = "Select ParentLoginName,ParentPassword,SID,Fname,ASID From vw_Parent_Login Where ParentLoginName=@loginID AND ParentPassword=@loginPass collate SQL_Latin1_General_Cp1_CS_AS"
        myCommand.CommandText = sqlStr
        myCommand.Connection = myConn
        myCommand.Parameters.Add("@loginID", SqlDbType.NVarChar)
        myCommand.Parameters("@loginID").Value = txtUserName.Text

        myCommand.Parameters.Add("@loginPass", SqlDbType.NVarChar)
        myCommand.Parameters("@loginPass").Value = txtPassword.Text

        Dim SID As Integer = -1, SchoolId As Integer = -1
        Dim cookieSID As New HttpCookie("SID")
        Dim cookieFName As New HttpCookie("FName")
        Dim myReader As SqlDataReader = myCommand.ExecuteReader
        While myReader.Read
            cookieSID.Value = myReader("SID")
            SID = myReader("SID")
            cookieSID.Expires = Now.AddHours(2)
            Response.Cookies.Add(cookieSID)
            cookieFName.Value = myReader("FName")
            cookieFName.Expires = Now.AddHours(2)
            Response.Cookies.Add(cookieFName)
        End While

        myReader.Close()
        myCommand.Dispose()
        myConn.Dispose()

        If SID = -1 Then
            'lblStatus.Text = "Login Failed..."
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Login Failed...');", True)
        Else
            Request.Cookies("SID").Value = SID
            Response.Redirect("Parent/ParentHome.aspx")
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
    End Sub

    Private Sub InitControls()
        txtUserName.Text = ""
        txtPassword.Text = ""
        'lblStatus.Text = ""
    End Sub
End Class
