Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Partial Class Admin_EventLog
    Inherits System.Web.UI.Page

    Dim sqlstr As String = ""
   
   
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

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' FillList()
            LoadEventType()
        End If
    End Sub
    Private Sub LoadEventType()

       
        
       

        

        Dim sqlStr As String = "Select distinct EventType From Event_Log"
        
        
        chkEventType.Items.Clear()

        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            chkEventType.Items.Add(myReader("EventType"))
        End While
        myReader.Close()

        
        

    End Sub
    Private Sub FillList()
       
        
        sqlstr = "Select top(8) logTime,EventType,Details,loginID From Event_Log Where Visible=1 Order By logID desc"
        
        
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        BulletedListLOG.Items.Clear()
        While myReader.Read
            BulletedListLOG.Items.Add(myReader("EventType") & " By : " & myReader("loginID") & " At Time : " & myReader("logTime") & " Details : " & myReader("Details") & vbCrLf & vbCrLf)

        End While
        myReader.Close()
        

    End Sub

    Protected Sub BulletedListLOG_Click(sender As Object, e As BulletedListEventArgs) Handles BulletedListLOG.Click

    End Sub

    Protected Sub chkAll_CheckedChanged(sender As Object, e As EventArgs) Handles chkAll.CheckedChanged
        Dim i As Integer = 0
        For i = 0 To chkEventType.Items.Count - 1
            If chkAll.Checked = True Then
                chkEventType.Items(i).Selected = True
            Else
                chkEventType.Items(i).Selected = False
            End If
        Next
    End Sub

    Private Sub FillEvents()
        Dim EventName As String = ""
        Dim flag As Integer = 0
        For i = 0 To chkEventType.Items.Count - 1
            If chkEventType.Items(i).Selected = True Then
                EventName &= chkEventType.Items(i).Text & "','"
                flag = 1
            End If
        Next
        If flag = 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please select at least one Event Type...');", True)
        Else
            EventName = EventName.Substring(0, EventName.Length - 3)
            sqlstr = "Select [logTime], [EventType], [Details], [UserName] FROM [vw_Event_log] Where Visible=1 and  EventType in ('" & EventName & "') Order By logID desc"
            SqlDataSourcelog.SelectCommand = sqlstr
            GridView1.Visible = True
            GridView1.DataBind()
        End If

    End Sub

    Protected Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        FillEvents()
    End Sub
End Class
