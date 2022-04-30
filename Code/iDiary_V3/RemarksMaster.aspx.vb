Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class RemarksMaster
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Exam") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("AccessDenied.aspx")
            End If
        Catch ex As Exception
            Response.Redirect("Login.aspx")
        End Try
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
    End Sub

    Private Sub InitControls()
        txtName.Text = ""
        txtID.Text = ""
        ' LoadMasterInfo(4, lstMasters)
        lblStatus.Text = ""
        ' chkDefault.Checked = False
        txtName.Focus()
    End Sub
    Private Sub LoadRemark(ByVal remarkType As Integer)
        ' 1 -> remark
        Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
        myConn.Open()

        Dim sqlstr As String = "Select RemarkName From RemarksMaster where IsRemark='" & remarkType & "' Order By RemarkName"
        Dim myCommand As New SqlCommand(sqlstr, myConn)
        Dim myReader As SqlDataReader = myCommand.ExecuteReader
        lstMasters.Items.Clear()
        While myReader.Read
            lstMasters.Items.Add(myReader(0))
        End While
        myReader.Close()
        myCommand.Dispose()

        myConn.Dispose()
    End Sub
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtName.Text.Length <= 0 Then
            lblStatus.Text = "Wrong Input!"
            txtName.Focus()
            Exit Sub
        End If

        Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
        myConn.Open()
        Dim sqlStr As String = ""
        Dim myCommand As New SqlCommand
        Dim Remarktype As Integer = 0
        If cboType.Text = "Remarks" Then
            Remarktype = 1
        Else
            Remarktype = 0
        End If
        If txtID.Text = "" Then
            'Insert
            sqlStr = "Insert into RemarksMaster Values('" & txtName.Text & "','" & Remarktype & "')"
            myCommand.CommandText = sqlStr
            myCommand.Connection = myConn
            myCommand.ExecuteNonQuery()
            '    insertSyncLog(sqlStr, "I", Request.Cookies("UserID").Value)
        Else
            'Update
            sqlStr = "Update RemarksMaster Set RemarkName='" & txtName.Text & "',IsRemark=" & Remarktype & " Where RemarkID=" & Val(txtID.Text)
            myCommand.CommandText = sqlStr
            myCommand.Connection = myConn
            myCommand.ExecuteNonQuery()
            '   insertSyncLog(sqlStr, "U", Request.Cookies("UserID").Value)
        End If
    


        myCommand.Dispose()
        myConn.Dispose()
        InitControls()
        LoadRemark(Remarktype)
    End Sub

    Protected Sub lstMasters_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstMasters.SelectedIndexChanged
        Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
        myConn.Open()
        Dim Remarktype As Integer = 0
        If cboType.Text = "Remarks" Then
            Remarktype = 1
        Else
            Remarktype = 0
        End If
        Dim sqlStr As String = "Select * From RemarksMaster Where RemarkName='" & lstMasters.Text & "' AND IsRemark=" & Remarktype
        Dim myCommand As New SqlCommand
        myCommand.CommandText = sqlStr
        myCommand.Connection = myConn
        Dim myReader As SqlDataReader = myCommand.ExecuteReader
        While myReader.Read
            txtID.Text = myReader("RemarkID")
            txtName.Text = myReader("RemarkName")
            
        End While
        myReader.Close()
        myCommand.Dispose()
        myConn.Dispose()
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        InitControls()
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
        myConn.Open()

        Dim sqlStr As String = "Delete From RemarksMaster Where RemarkID=" & Val(txtID.Text)
        Dim myCommand As New SqlCommand
        myCommand.CommandText = sqlStr
        myCommand.Connection = myConn
        myCommand.ExecuteNonQuery()
        '   insertSyncLog(sqlStr, "D", Request.Cookies("UserID").Value)
        myCommand.Dispose()
        myConn.Dispose()
        InitControls()
    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboType.SelectedIndexChanged
        If cboType.Text = "Remarks" Then
            LoadRemark(1)
        Else
            LoadRemark(0)
        End If
    End Sub
End Class