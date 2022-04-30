'....................created by vikash supta on 27/06/2016.............................................
Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class SMSConfiguration
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
        If IsPostBack = False Then
            InitControls()
        Else

        End If
    End Sub

    Private Sub InitControls()
        Dim sqlStr As String = ""
        sqlStr = "Select * from SMSConfig"

      
        Dim myreader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
        While myreader.Read
            Try
                txtsmsurl.Text = myreader("SMSURL")
            Catch ex As Exception
                txtsmsurl.Text = ""
            End Try
            Try
                txtsmsport.Text = myreader("SMSPort")
            Catch ex As Exception
                txtsmsport.Text = ""
            End Try
            Try
                txtsmsuser.Text = myreader("SMSUser")
            Catch ex As Exception
                txtsmsuser.Text = ""
            End Try
            Try
                txtsmspassword.Text = myreader("SMSPass")
            Catch ex As Exception
                txtsmspassword.Text = ""
            End Try
        End While
        myreader.Close()

        sqlStr = "select * from SMSSender where IsDefault =1"


        Dim myreader1 As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myreader1.Read
            Try
                txtsmssender.Text = myreader1("SMSSender")
            Catch ex As Exception

            End Try
        End While
        myreader1.Close()

        LoadMasterInfo(71, cboSchool, Request.Cookies("SchoolIDs").Value)
        If cboSchool.Items.Count = 3 Then
            cboSchool.Items.Add("All")

        End If


    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtsmsurl.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('SMS URL is required...');", True)
            txtsmsurl.Focus()
            Exit Sub
        End If
        If txtsmsport.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('SMS Port is required...');", True)
            txtsmsport.Focus()
            Exit Sub
        End If
        If txtsmsuser.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('SMS User is required...');", True)
            txtsmsuser.Focus()
            Exit Sub
        End If
        If txtsmspassword.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('SMS Password is required...');", True)
            txtsmspassword.Focus()
            Exit Sub
        End If
        If txtsmssender.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('SMS Sender is required...');", True)
            txtsmssender.Focus()
            Exit Sub
        End If
        If txtsmssender.Text.Length <> 6 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Only 6 digit is required...');", True)
            txtsmssender.Focus()
            Exit Sub
        End If
        Dim sqlStr As String = ""
        Dim sqlStr1 As String = ""
        Dim mysqlStr As String = ""
        sqlStr = "select Count(*) from SMSconfig"
        Dim noofcolumns As Integer = ExecuteQuery_ExecuteScalar(sqlStr)
        If noofcolumns = 1 Then
            sqlStr = "update SMSConfig set SMSURL='" & txtsmsurl.Text & "', SMSPort='" & txtsmsport.Text & "', SMSUser='" & txtsmsuser.Text & "', SMSPass='" & txtsmspassword.Text & "'"
            ExecuteQuery_Update(sqlStr)
        Else

            sqlStr = "insert into SMSConfig values('" & txtsmsurl.Text & "','" & txtsmsport.Text & "','" & txtsmsurl.Text & "','" & txtsmspassword.Text & "' )"
            ExecuteQuery_Update(sqlStr)
        End If

        Dim SenderName As String = ""
        Try
            SenderName = GridView1.SelectedRow.Cells(2).Text
        Catch ex As Exception
            SenderName = ""
        End Try
        Dim NewSenderName As String = txtsmssender.Text.ToUpper
        sqlStr = "select Count(*) from SMSSender where SMSSender='" & NewSenderName & "'"
        Dim count As Integer = ExecuteQuery_ExecuteScalar(sqlStr)
        If count = 0 Then
        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Sender Name Already Exist...');", True)
            txtsmssender.Focus()
            Exit Sub
        End If
        Dim SchoolID As Integer = FindMasterID(71, cboSchool.SelectedItem.Text)
        If chkisdefault.Checked = True Then
            Dim myquery As String = "update SMSSender set IsDefault=0"
            ExecuteQuery_Update(myquery)
            If SenderName = "" Then
                sqlStr1 = "insert into SMSSender(SMSSender,IsDefault,SchoolID) values('" & NewSenderName & "',1,'" & SchoolID & "')"
                ExecuteQuery_Update(sqlStr1)
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Save Successfully...');", True)
            Else
                sqlStr1 = "update SMSSender set SMSSender='" & NewSenderName & "', IsDefault=1 where SMSSender='" & SenderName & "'"
                ExecuteQuery_Update(sqlStr1)
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Updated Successfully...');", True)
            End If
        Else
            If SenderName = "" Then
                sqlStr1 = "insert into SMSSender(SMSSender,IsDefault,SchoolID) values('" & NewSenderName & "',0,'" & SchoolID & "')"
                ExecuteQuery_Update(sqlStr1)
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Save Successfully...');", True)
            Else
                sqlStr1 = "update SMSSender set SMSSender='" & NewSenderName & "',IsDefault=0 where SMSSender='" & SenderName & "'"
                ExecuteQuery_Update(sqlStr1)
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Updated Successfully...');", True)
            End If
        End If
        
        txtsmssender.Text = ""
        chkisdefault.Checked = False
        GridView1.Visible = True
        SMSSenderDatasource.SelectCommand = "SELECT [SMSSender] FROM [SMSSender]"
        GridView1.DataBind()
    End Sub


    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        txtsmssender.Text = GridView1.SelectedRow.Cells(2).Text
        cboSchool.SelectedItem.Text = GridView1.SelectedRow.Cells(3).Text
        Dim myquery As String = "Select IsDefault from SMSSender where SMSSender='" & txtsmssender.Text & "'"
        Dim status As Boolean = ExecuteQuery_ExecuteScalar(myquery)
        If status = True Then
            chkisdefault.Checked = True
        Else
            chkisdefault.Checked = False
        End If

    End Sub

    Protected Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        txtsmssender.Text = ""
        GridView1.SelectedIndex = -1
    End Sub

    Protected Sub btnremove_Click(sender As Object, e As EventArgs) Handles btnremove.Click
        If txtsmssender.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('SMS Sender is required...');", True)
            txtsmssender.Focus()
            Exit Sub
        End If
        Dim sqlStr As String = "'"
        sqlStr = "select Count(*) from SMSSender"
        Dim totalsmssender As Integer = ExecuteQuery_ExecuteScalar(sqlStr)
        If totalsmssender > 1 Then
            sqlStr = "delete from SMSSender where SMSSender='" & txtsmssender.Text & "'"
            ExecuteQuery_Update(sqlStr)
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Remove Successfully...');", True)
            txtsmssender.Text = ""
        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Atleast One Sender is required for SMS Sending....');", True)
        End If
        
        GridView1.Visible = True
        SMSSenderDatasource.SelectCommand = "SELECT [SMSSender] FROM [SMSSender]"
        GridView1.DataBind()

    End Sub

    








    Protected Sub chkisdefault_CheckedChanged(sender As Object, e As EventArgs) Handles chkisdefault.CheckedChanged
        
    End Sub
End Class