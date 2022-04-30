Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class TeacherInbox
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'Try
        '    If Request.Cookies("UType").Value.ToString.Contains("Admin") Or Request.Cookies("UType").Value.ToString.Contains("Teacher") Then
        '        'Allow
        '    Else
        '        Response.Redirect("/./AccessDenied.aspx", False)
        '    End If
        'Catch ex As Exception
        '    Response.Redirect("~/Login.aspx")
        'End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then

            FillInbox()
        End If

    End Sub

    Private Sub FillInbox()

        GridView1.DataBind()
        Dim i As Integer = 0, ExistCount As Integer = 0
        Dim sqlStr As String = ""
       
        ExistCount = GridView1.Rows.Count
        If ExistCount <= 0 Then
            btnDelete.Visible = False
            'btnRead.Visible = False
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('No Messages to Show !');", True)
        End If
        'btnDelete.Visible = True
        'btnRead.Visible = True

        'For i = 0 To GridView1.Rows.Count - 1
        '    sqlStr = "Select Max(isRead) From vw_msgSentFromParent Where " & _
        '       "msgID=" & GridView1.Rows(i).Cells(1).Text & ""


        '    Dim MSGStatus As Integer = 0
        '    Try
        '        MSGStatus = ExecuteQuery_ExecuteScalar(sqlStr)
        '    Catch ex As Exception
        '        MSGStatus = -1
        '    End Try

        '    If ExistCount > 0 And MSGStatus = 0 Then
        '        GridView1.Rows(i).BackColor = Drawing.Color.White
        '        GridView1.Rows(i).Cells(7).ForeColor = Drawing.Color.White

        '    ElseIf ExistCount > 0 And MSGStatus = 1 Then
        '        GridView1.Rows(i).BackColor = Drawing.Color.AntiqueWhite
        '        GridView1.Rows(i).Cells(7).ForeColor = Drawing.Color.AntiqueWhite
        '    End If
        'Next

        '  GridView1.DataBind()
    End Sub

    'Protected Sub btnRead_Click(sender As Object, e As EventArgs) Handles btnRead.Click
    '    Dim i As Integer = 0, ExistCount As Integer = 0
    '    Dim sqlStr As String = ""

    '    ExistCount = GridView1.Rows.Count
    '    If ExistCount <= 0 Then

    '        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('No Messages !');", True)
    '    End If

    '    For i = 0 To GridView1.Rows.Count - 1

    '        Dim chk As CheckBox = DirectCast(GridView1.Rows(i).FindControl("chkSelect"), CheckBox)

    '        If chk.Checked = True Then
    '            sqlStr = "Update msgSentFromParent Set isRead=1 Where msgID='" & GridView1.Rows(i).Cells(6).Text & "'"
    '            ExecuteQuery_Update(sqlStr)
    '        End If

    '    Next

    '    FillInbox()
    'End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim i As Integer = 0, ExistCount As Integer = 0
        Dim sqlStr As String = ""

        sqlStr = "Delete from msgSentFromParent where msgID='" & GridView1.SelectedRow.Cells(1).Text & "'"
        ExecuteQuery_Update(sqlStr)
        'ExistCount = GridView1.Rows.Count
        'If ExistCount <= 0 Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('No Messages !');", True)
        'End If

        'For i = 0 To GridView1.Rows.Count - 1

        '    Dim chk As CheckBox = DirectCast(GridView1.Rows(i).FindControl("chkSelect"), CheckBox)

        '    If chk.Checked = True Then
        '        sqlStr = "Delete from msgSentFromParent Where msgID='" & GridView1.Rows(i).Cells(6).Text & "'"
        '        ExecuteQuery_Update(sqlStr)
        '    End If

        'Next

        FillInbox()
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        Dim msgID As String = GridView1.DataKeys(0).Values(0).ToString()
        Dim sqlStr As String = ""
       
        sqlStr = "Update msgSentFromParent Set isRead=1 Where msgID='" & msgID & "'"
       ExecuteQuery_Update(sqlStr)

        sqlStr = "Select Subject,Body from msgSentFromParent Where msgID='" & msgID & "'"
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            lblSubject.Text = "Subject : " & myReader("Subject")
            txtMessage.Text = myReader("Body")
        End While
        myReader.Close()

        GridView1.SelectedIndex = -1
        btnDelete.Visible = True
    End Sub
End Class