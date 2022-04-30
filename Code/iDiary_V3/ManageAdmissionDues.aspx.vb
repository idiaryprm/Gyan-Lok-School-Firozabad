Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class ManageAdmissionDues
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        Try

            If Request.Cookies("UType").Value.ToString.Contains("Fee") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("AccessDenied.aspx")
            End If

        Catch ex As Exception

            If ex.Message.Contains("Object reference not set to an instance of an object") Then
                Response.Redirect("Logout.aspx")
            End If

        End Try

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
    End Sub

    Private Sub InitControls()
        LoadClasses()
        cboSection.Items.Clear()
        LoadStatus()
        'LoadMasterInfo(1, cboASession)
        'GridView1.DataBind()
    End Sub
    Private Sub LoadClasses()
        Dim sqlStr As String = "Select ClassName From Classes order by DisplayOrder"

        Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
        myConn.Open()

        Dim myCommand As New SqlCommand
        myCommand.CommandText = sqlStr
        myCommand.Connection = myConn
        Dim myReader As SqlDataReader = myCommand.ExecuteReader
        cboClass.Items.Clear()
        cboClass.Items.Add("")
        While myReader.Read
            cboClass.Items.Add(myReader(0))
        End While
        myReader.Close()
        myCommand.Dispose()
        myConn.Dispose()
    End Sub
    Private Sub LoadSections()
        Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
        myConn.Open()

        Dim sqlStr As String = "Select SecName From vw_Class_Section Where ClassName='" & cboClass.Text & "'"
        Dim myCommand As New SqlCommand
        myCommand.CommandText = sqlStr
        myCommand.Connection = myConn
        Dim myReader As SqlDataReader = myCommand.ExecuteReader
        cboSection.Items.Clear()
        cboSection.Items.Add("")
        While myReader.Read
            Try
                cboSection.Items.Add(myReader(0))
            Catch ex As Exception

            End Try
        End While
        myReader.Close()
        myCommand.Dispose()

        myConn.Dispose()
    End Sub
    Private Sub LoadStatus()
        Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
        myConn.Open()

        Dim sqlstr As String = "Select StatusName From StatusMaster"
        Dim myCommand As New SqlCommand(sqlstr, myConn)
        Dim myReader As SqlDataReader = myCommand.ExecuteReader
        cboStatus.Items.Clear()
        While myReader.Read
            cboStatus.Items.Add(myReader(0))
        End While
        myReader.Close()
        myCommand.Dispose()

        myConn.Dispose()
    End Sub
    Protected Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        If cboClass.Text = "" Then
            lblStatus.Text = "Please Select Class..."
            cboClass.Focus()
            Exit Sub
        End If
        If cboSection.Text = "" Then
            lblStatus.Text = "Please Select Section..."
            cboSection.Focus()
            Exit Sub
        End If
        If cboStatus.Text = "" Then
            lblStatus.Text = "Please Select Status..."
            cboStatus.Focus()
            Exit Sub
        End If
        lblStatus.Text = ""
        SqlDataSource1.SelectCommand = "SELECT SID, RegNo, SName, FName, FeeBookNo FROM vw_Student WHERE  ClassName='" & cboClass.Text & "' AND SecName='" & cboSection.Text & "' AND ASID=" & Request.Cookies("ASID").Value & " AND StatusName='" & cboStatus.Text & "'  Order By Gender DESC,SName ASC"
        GridView1.DataSource = SqlDataSource1
        GridView1.DataBind()

        Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
        myConn.Open()

        Dim sqlStr As String = ""
        Dim myCommand As New SqlCommand

        Dim i As Integer = 0

        For i = 0 To GridView1.Rows.Count - 1

            sqlStr = "Select Count(*) From vw_Student Where RegNo='" & GridView1.Rows(i).Cells(2).Text & "' AND ASID <> " & Request.Cookies("ASID").Value
            myCommand.CommandText = sqlStr
            myCommand.Connection = myConn
            Dim RecordExist As Integer = 0
            RecordExist = myCommand.ExecuteScalar

            If RecordExist > 0 Then
                GridView1.Rows(i).Visible = False
                Continue For
            End If

            Dim chk As CheckBox = DirectCast(GridView1.Rows(i).FindControl("chkSelect"), CheckBox)

            sqlStr = "Select Count(*) From FeeDuesAdmission Where ASID=" & Request.Cookies("ASID").Value & " AND SID=" & GetSID(GridView1.Rows(i).Cells(2).Text, Request.Cookies("ASID").Value)
            myCommand.CommandText = sqlStr
            myCommand.Connection = myConn

            If myCommand.ExecuteScalar <= 0 Then
                chk.Checked = False
            Else
                chk.Checked = True
            End If
        Next


    End Sub

    Protected Sub chkCheckAll_CheckedChanged(sender As Object, e As EventArgs) Handles chkCheckAll.CheckedChanged
        Dim myVal As Boolean = False
        Dim i As Integer = 0, myCount As Integer = 0

        If chkCheckAll.Checked = True Then myVal = True

        For i = 0 To GridView1.Rows.Count - 1

            DirectCast(GridView1.Rows(i).FindControl("chkSelect"), CheckBox).Checked = myVal

            myCount += 1
        Next
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click

        'Add this procedure in Student Master too.

        Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
        myConn.Open()

        Dim sqlStr As String = ""
        Dim myCommand As New System.Data.SqlClient.SqlCommand
        sqlStr = "Delete from FeeDuesAdmission where SID in (select SID from vw_student where ClassName='" & cboClass.Text & "' and SecName='" & cboSection.Text & "' and ASID=" & Request.Cookies("ASID").Value & ")"
        myCommand.CommandText = sqlStr
        myCommand.Connection = myConn
        myCommand.ExecuteNonQuery()
        Dim i As Integer = 0
        For i = 0 To GridView1.Rows.Count - 1
            Dim SID As Integer = GetSID(GridView1.Rows(i).Cells(2).Text, Request.Cookies("ASID").Value)
           
            Dim chk As CheckBox = DirectCast(GridView1.Rows(i).FindControl("chkSelect"), CheckBox)
            If chk.Checked = True And GridView1.Rows(i).Visible = True Then
                sqlStr = "Insert into FeeDuesAdmission Values(" & Request.Cookies("ASID").Value & "," & GetSID(GridView1.Rows(i).Cells(2).Text, Request.Cookies("ASID").Value) & ")"
                myCommand.CommandText = sqlStr
                myCommand.Connection = myConn
                myCommand.ExecuteNonQuery()
            End If
            'Then Continue For
            'sqlStr = "Select Count(*) From FeeDuesAdmission Where ASID=" & Request.Cookies("ASID").Value & " AND SID=" & Val(GridView1.Rows(i).Cells(1).Text)
            'myCommand.CommandText = sqlStr
            'myCommand.Connection = myConn

            'If myCommand.ExecuteScalar > 0 Then Continue For
        Next

        System.Threading.Thread.Sleep(500)

        myCommand.Dispose()
        myConn.Close()

        InitControls()
        lblStatus.Text = "New Addmissions has been Saved..."
    End Sub

    Protected Sub cboClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboClass.SelectedIndexChanged
        'LoadSections()
        LoadClassSection("", cboClass.Text, cboSection)
    End Sub
End Class