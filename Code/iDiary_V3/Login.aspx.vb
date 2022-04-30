Imports System.Data.SqlClient
Imports iDiary_V3.iDiary_Security.CLS_iDiary_Security

Public Class Login1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
    End Sub

    Private Sub InitControls()
        txtUserName.Text = ""
        txtPassword.Text = ""
        txtUserName.Focus()
    End Sub

    Protected Sub btnSignIn_Click(sender As Object, e As EventArgs) Handles btnSignIn.Click
        'If Request.Url.AbsoluteUri.Contains("apsdc.idiary") And getEntryStatus() = 0 Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Online Access is Disabled.Please Contact School.');", True)
        '    Exit Sub
        'End If
        'If ccLogin.IsValid = False Then
        '    ccLogin.Validate()
        '    txtPassword.Focus()
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Enter Valid Captcha.');", True)
        '    Exit Sub
        'End If
        Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
        myConn.Open()

        'Check Credentials
        Dim sqlStr As String = "Select LoginID, LoginPass,UserName,UserID, GroupName,W,EmpID,ASID,EmpASID,EmpASName,SchoolIDs From vwUserGroups Where LoginID=@loginID AND LoginPass=@loginPass collate SQL_Latin1_General_Cp1_CS_AS"
        Dim myCommand As New System.Data.SqlClient.SqlCommand
        myCommand.CommandText = sqlStr
        myCommand.Connection = myConn
        myCommand.Parameters.Add("@loginID", SqlDbType.NVarChar)
        myCommand.Parameters("@loginID").Value = txtUserName.Text

        myCommand.Parameters.Add("@loginPass", SqlDbType.NVarChar)
        myCommand.Parameters("@loginPass").Value = txtPassword.Text
        Dim myReader As System.Data.SqlClient.SqlDataReader = myCommand.ExecuteReader
        Dim myCount As Integer = 0
        Dim UType As String = ""
        Dim cookieUserID As New HttpCookie("UserID")
        'Dim cookieUserName As New HttpCookie("UserName")
        Dim cookieUID As New HttpCookie("UID")
        Dim cookieUType As New HttpCookie("UType")
        Dim cookieASID As New HttpCookie("ASID")
        Dim cookieEmpASID As New HttpCookie("EmpASID")
        Dim cookieEmpASName As New HttpCookie("EmpASName")
        Dim cookieSchoolIDs As New HttpCookie("SchoolIDs")

        While myReader.Read
            myCount += 1
            UType &= myReader("GroupName") & "-" & myReader("W")
            cookieUID.Value = myReader("LoginID")
            cookieUserID.Value = myReader("UserID")
            cookieASID.Value = myReader("ASID")
            Try
                cookieEmpASID.Value = myReader("EmpASID")
            Catch ex As Exception

            End Try
            Try
                cookieEmpASName.Value = myReader("EmpASName")
            Catch ex As Exception

            End Try
            Try
                cookieSchoolIDs.Value = myReader("SchoolIDs")
            Catch ex As Exception

            End Try
            cookieUID.Expires = Now.AddHours(2)
            Response.Cookies.Add(cookieUID)
            cookieUserID.Expires = Now.AddHours(2)
            Response.Cookies.Add(cookieUserID)
            cookieASID.Expires = Now.AddHours(2)
            Response.Cookies.Add(cookieASID)
        
            cookieEmpASID.Expires = Now.AddHours(2)
            Response.Cookies.Add(cookieEmpASID)
            cookieEmpASName.Expires = Now.AddHours(2)
            Response.Cookies.Add(cookieEmpASName)
            cookieSchoolIDs.Expires = Now.AddHours(2)
            Response.Cookies.Add(cookieSchoolIDs)
        End While
        'Session("UType") = UType
        cookieUType.Value = UType
        cookieUType.Expires = Now.AddHours(2)
        Response.Cookies.Add(cookieUType)
        myReader.Close()

        If myCount <= 0 Then    'If No then Show Message
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Login Failed...');", True)
            myCommand.Dispose()
            myConn.Dispose()
            txtPassword.Focus()
        Else    'Check ASID Details
            sqlStr = "Select urlkeyWords From Params"
            myCommand.CommandText = sqlStr
            myCommand.Connection = myConn
            Dim ASIDreader As SqlDataReader = myCommand.ExecuteReader
            ASIDreader.Read()
           
            Dim urlKeyWords As String = ASIDreader("urlkeyWords")

            ASIDreader.Close()

            sqlStr = "Select ASName From AcademicSession where ASID=" & Request.Cookies("ASID").Value.ToString()
            myCommand.CommandText = sqlStr
            myCommand.Connection = myConn
            Dim cookieASName As New HttpCookie("ASName")
            cookieASName.Value = myCommand.ExecuteScalar
            cookieASName.Expires = Now.AddHours(2)
            Response.Cookies.Add(cookieASName)

            Dim strArr() As String
            strArr = urlKeyWords.Split(";")

            Dim cookieKeyword1 As New HttpCookie("keyword1")
            cookieKeyword1.Value = strArr(0)
            cookieKeyword1.Expires = Now.AddHours(2)
            Response.Cookies.Add(cookieKeyword1)
            Dim cookieKeyword2 As New HttpCookie("keyword2")
            cookieKeyword2.Value = strArr(1)
            cookieKeyword2.Expires = Now.AddHours(2)
            Response.Cookies.Add(cookieKeyword2)
            
            If cookieASID.Value Is Nothing Or IsDBNull(cookieASID.Value) Then
                myCommand.Dispose()
                myConn.Dispose()
                Response.Redirect("~/AcademicSession.aspx")
            Else
                myCommand.Dispose()
                myConn.Dispose()
                If Request.Cookies("UType").Value.ToString.Contains("Executive") Then
                    Response.Redirect("~/Dashboard.aspx")
                Else
                    Response.Redirect("~/Index.aspx")
                End If
            End If

        End If

    End Sub
    Private Function getEntryStatus() As Integer
        Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
        myConn.Open()
        Dim sqlStr As String = ""
        Dim myCommand As New SqlCommand
        Dim status As Integer = 0
        '0---------not allowed
        '1---------Allowed

        sqlStr = "select OnlineEntryAllowed from PARAMS"
        Try
            myCommand.CommandText = sqlStr
            myCommand.Connection = myConn
            status = myCommand.ExecuteScalar
        Catch ex As Exception

        End Try
        myCommand.Dispose()
        myConn.Dispose()
        Return status
    End Function
 
End Class