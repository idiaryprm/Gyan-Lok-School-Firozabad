Imports iDiary_V3.iDiary.CLS_idiary
Imports System.Data.SqlClient

Public Class generateParentLoginIndi
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
        If Not IsPostBack Then
            txtAdmnNo.Text = ""
            gvLogin.Visible = False
        End If
        If Request.Cookies("UType").Value.ToString.Contains("Admin-1") = False Then
            btnGenerate.Enabled = False
        End If
    End Sub

    Protected Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        If txtAdmnNo.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Provide Admin no to continue...');", True)
            txtAdmnNo.Focus()
            Exit Sub
        End If
       
        gvLogin.Visible = True
        gvLogin.DataBind()

    End Sub

    Dim lstSID As New List(Of String)

    Protected Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
        If gvLogin.Rows.Count <= 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('First Generate Login for This Student...');", True)
            Exit Sub
        End If
        If txtAdmnNo.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Provide Admin no to continue...');", True)
            txtAdmnNo.Focus()
            Exit Sub
        End If
        Dim SId As Integer = 0
        SId = GetSID(txtAdmnNo.Text, Request.Cookies("ASID").Value)
        If SId = 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Provide correct Admin no to continue...');", True)
            txtAdmnNo.Focus()
            Exit Sub
        End If
        Dim sqlStr As String = "Update ParentLogins Set ParentPassword='Password@123' where SID=" & SId
        ExecuteQuery_Update(sqlStr)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Password Reset ...');", True)
        txtAdmnNo.Text = ""
        gvLogin.Visible = False
        Dim MobNo As String = GetPhoneNo(SId)
        SendSmsToParent(MobNo)
    End Sub
    Public Shared Function GetPhoneNo(ByVal SID As String)
        Dim sqlStr As String = "Select MobNo From vw_Student Where SID='" & SID & "'"
        Dim rv As String = ""
        rv = ExecuteQuery_ExecuteScalar(sqlStr)
        Return rv
    End Function
    Private Function getRegNO(ByVal SID As Integer) As String

        Dim sqlStr As String = ""
        Dim regNO As String = ""

        sqlStr = "Select top(1) regno From Student Where SID=" & SID
        Try
            regNO = ExecuteQuery_ExecuteScalar(sqlStr)
        Catch ex As Exception

        End Try
        Return regNO
    End Function
    Private Sub SendSmsToParent(ByVal MobNo As String)
        Dim sqlstr As String = ""
        Dim SenderName As String = ""
        Dim Message As String = ""
        sqlstr = "select * from SMSSender"
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
        While myReader.Read
            SenderName = myReader("SMSSender")
        End While
        myReader.Close()

        sqlstr = "Select * from SMSTemplates Where TemplateCode='Parent Password Reset'"
        myReader = ExecuteQuery_ExecuteReader(sqlstr)
        While myReader.Read
            Message = myReader("TemplateMessage")
        End While
        myReader.Close()

        Dim UserName As String = gvLogin.Rows(0).Cells(4).Text
        Message = Message.Replace("<*>", UserName)
        Message = Message.Replace("(*)", "Password@123")

        Dim lstMobile As New List(Of String)
        lstMobile.Add(MobNo)
        Dim SMSResponse As String = ""
        SMSResponse = SendMySMS(SenderName, lstMobile, Message)
    End Sub
End Class