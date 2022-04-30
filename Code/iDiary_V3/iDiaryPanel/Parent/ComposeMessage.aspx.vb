Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Partial Class Parent_ComposeMessage
    Inherits System.Web.UI.Page

    Protected Sub btnSend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSend.Click
        lblStatus.Text = "Message sent successfully to: " & cboTo.Text
        Dim sqlStr As String
        Dim ParentNameFromMaster As String = ""
        'Master.parentName
        'Dim TeacherID As Integer=
        Dim ffname As String = Request.Cookies("FName").Value
        
        sqlStr = "Select CSSID from vw_Student Where SID=" & Request.Cookies("SID").Value
        Dim CSSID As Integer = 0
        Dim empID As Integer = 0
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            CSSID = myReader("CSSID")
        End While
        myReader.Close()

        sqlStr = "Select Distinct EmpID From vw_UserSubjectPermission where CSSID=" & CSSID & " AND IsClassTeacher=1"
        myReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            Try
                empID = myReader("EmpID")
            Catch ex As Exception
            End Try
        End While
        myReader.Close()

        sqlStr = "INSERT INTO msgSentFromParent(sentDate,sentTime,Subject,Body,empID,SentFromSID,isRead) Values(" & _
           "'" & Date.Now.ToString("yyyy/MM/dd") & "'," & _
           "'" & System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "'," & _
           "'" & txtSubject.Text & "'," & _
           "'" & txtMessage.Text & "'," & _
           "'" & empID & "'," & _
           "'" & Request.Cookies("SID").Value & "'," & _
           "'0')"

        ExecuteQuery_Update(sqlStr)

        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Message Sent Successfully ..');", True)
        InitControls()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            InitControls()
        End If
    End Sub
    Private Sub InitControls()
        txtSubject.Text = ""
        txtMessage.Text = ""
        cboTo.Focus()
    End Sub
End Class
