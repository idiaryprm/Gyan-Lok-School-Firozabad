Imports iDiary_V3.iDiary.CLS_idiary
Imports System.Data.SqlClient

Public Class GenerateParentLogins
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
            LoadMasterInfo(71, cboSchoolName, Request.Cookies("SchoolIDs").Value)
            'LoadMasterInfo(2, cboClass)
            cboSection.Text = ""
            gvLogin.Visible = False
        End If
        If Request.Cookies("UType").Value.ToString.Contains("Admin-1") = False Then
            btnGenerate.Enabled = False
        End If
    End Sub

    Protected Sub cboClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboClass.SelectedIndexChanged
        LoadClassSection("", cboClass.Text, cboSection)
    End Sub

    Protected Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        If cboClass.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Provide atleast one class to continue...');", True)
            cboClass.Focus()
            Exit Sub
        End If
        If cboSection.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Provide atleast one Section to continue...');", True)
            cboSection.Focus()
            Exit Sub
        End If
        gvLogin.Visible = True
        gvLogin.DataBind()
    End Sub

    Dim lstSID As New List(Of String)
    Private Sub InitControls()
        LoadMasterInfo(71, cboSchoolName, Request.Cookies("SchoolIDs").Value)
        LoadMasterInfo(2, cboClass, cboSchoolName.Text)
        cboClass.Items.Add("ALL")
        cboSection.Items.Clear()
        'LoadMasterInfo(10, cboStatus)
        cboClass.Focus()

    End Sub

    Protected Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
        Dim lstSIDParentLogin As New List(Of String)
        Dim lstMobNo As New List(Of String)
        Dim sqlStr As String = "SELECT SID,MobNo From vw_Student where ASID='" & Request.Cookies("ASID").Value & "' and ClassName='" & cboClass.Text & "' And SecName='" & cboSection.Text & "' And StatusName='Active' order by SID"

        Dim SIDreader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While SIDreader.Read
            lstSID.Add(SIDreader(0))
            lstMobNo.Add(SIDreader(1))
        End While
        SIDreader.Close()
        Dim parentloginName As String = ""
        Dim parentloginpass As String = ""

        Dim lstLOGINSID As New List(Of String)
        sqlStr = "Select SID from vw_parent_login order by SID"
        SIDreader = ExecuteQuery_ExecuteReader(sqlStr)
        While SIDreader.Read
            lstLOGINSID.Add(SIDreader(0))
        End While
        SIDreader.Close()
        For i As Integer = 0 To lstSID.Count - 1
            Dim flag As Integer = 0
            For j As Integer = 0 To lstLOGINSID.Count - 1

                If lstSID.Item(i) = lstLOGINSID.Item(j) Then
                    lstLOGINSID.RemoveAt(j)
                    flag = 1
                    Exit For
                End If
            Next
            If flag = 0 Then
                parentloginName = "P-" & getRegNO(lstSID(i))
                parentloginpass = "Password@123"
                sqlStr = "Insert Into ParentLogins Values('" & lstSID.Item(i) & "','" & parentloginName & "','" & parentloginpass & "', 1)"
                ExecuteQuery_Update(sqlStr)

                For k = 0 To lstSID.Count - 1
                    lstSIDParentLogin.Add(lstMobNo.Item(k))
                    lstSIDParentLogin.Add(parentloginName)
                Next
            End If
            SendSmsToParent(lstSIDParentLogin)
        Next
    End Sub

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
    Private Sub SendSmsToParent(ByVal lstSID As List(Of String))
        Dim sqlstr As String = ""
        Dim SenderName As String = ""
        Dim Message As String = ""
        sqlstr = "select * from SMSSender"
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
        While myReader.Read
            SenderName = myReader("SMSSender")
        End While
        myReader.Close()

        sqlstr = "Select * from SMSTemplates Where TemplateCode='New Parent Login'"
        myReader = ExecuteQuery_ExecuteReader(sqlstr)
        While myReader.Read
            Message = myReader("TemplateMessage")
        End While
        myReader.Close()

        Dim lstMobile As New List(Of String)

        For i = 0 To lstSID.Count - 1
            If i Mod 2 <> 0 Then
                Message = Message.Replace("(*)", lstSID.Item(i))
                Message = Message.Replace("<*>", "Password@123")
            Else
                lstMobile.Add(lstSID.Item(i))
            End If
            If lstMobile.Count <> 0 And Message <> "" Then
                Dim SMSResponse As String = ""
                SMSResponse = SendMySMS(SenderName, lstMobile, Message)
                lstMobile.Clear()
                Message = ""
            Else

            End If
        Next
    End Sub
End Class