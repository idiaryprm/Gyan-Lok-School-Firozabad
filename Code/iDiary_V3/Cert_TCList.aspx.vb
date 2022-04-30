Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class Cert_TCList
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
    End Sub
    Private Sub InitControls()

        'txtFrom.Text = Now.Date.ToString("dd/MM/yyyy")
        'txtFrom_CalendarExtender.EnableViewState = True
        'txtTo.Text = Now.Date.ToString("dd/MM/yyyy")
        'txtTo_CalendarExtender.EnableViewState = True

        'txtFrom.Focus()
        LoadMasterInfo(71, cboSchoolName, Request.Cookies("SchoolIDs").Value)
        txtDateFrom.Text = Now.Date.ToString("dd/MM/yyyy")
        txtDateTo.Text = Now.Date.ToString("dd/MM/yyyy")
        LoadMasterInfo(2, cboClass, cboSchoolName.Text)
        cboClass.Text = ""
        cboSection.Text = ""
    End Sub
    ' Protected Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click
    'Dim FromDate1 As String = txtFrom.Text.Substring(6, 4) & "/" & txtFrom.Text.Substring(3, 2) & "/" & txtFrom.Text.Substring(0, 2)
    'Dim ToDate1 As String = txtTo.Text.Substring(6, 4) & "/" & txtTo.Text.Substring(3, 2) & "/" & txtTo.Text.Substring(0, 2)

    'SqlDataSourceTC.SelectCommand = "SELECT [TCNo], [studentCategory], [RegNo], [SName], [FName], [ClassName], [SecName], [dateOfIssue] FROM [vw_StudentTC] WHERE convert(date,dateOfIssue) as dateOfIssue between '" & FromDate1 & "' AND '" & ToDate1 & "'"
    'End Sub

    Private Function ChangeDate(ByVal txtDate As String)
        'Dim issuedDate As String
        If txtDate.Contains("/") Then
            txtDate = txtDate.Split("/")(2) & "/" & txtDate.Split("/")(1) & "/" & txtDate.Split("/")(0)
        Else
            txtDate = txtDate.Split("-")(2) & "-" & txtDate.Split("-")(1) & "-" & txtDate.Split("-")(0)
        End If

        Return txtDate
    End Function

   
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
       
       
       

        Dim sqlstr As String = ""
        

        sqlstr = "Select * From TC"

        
        
        Dim lstDate As String
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            Dim tmp As String = ChangeDate(myReader("DateOfIssue"))
            lstDate = tmp
            sqlstr = "Update TC set dateofissue ='" & lstDate & "' where TCNo='" & myReader("TCNo") & "'"
            ExecuteQuery_Update(sqlstr)
        End While

        myReader.Close()
        
        
    End Sub

    Protected Sub cboClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboClass.SelectedIndexChanged
        LoadClassSection(cboSchoolName.Text, cboClass.Text, cboSection)
    End Sub

    Protected Sub btnAdnmSearch_Click(sender As Object, e As EventArgs) Handles btnAdnmSearch.Click
        SqlDataSourceTC.SelectCommand = "SELECT [TCNo], [RegNo], [SName], [FName], [ClassName], [SecName], [dateOfIssue] FROM [vw_StudentTC] WHERE RegNo='" & txtRegNo.Text & "'"
        GridView1.DataBind()
    End Sub

    Protected Sub btnNameSearch_Click(sender As Object, e As EventArgs) Handles btnNameSearch.Click
        SqlDataSourceTC.SelectCommand = "SELECT [TCNo], [RegNo], [SName], [FName], [ClassName], [SecName], [dateOfIssue] FROM [vw_StudentTC] WHERE SName Like '%" & txtName.Text & "%'"
        GridView1.DataBind()
    End Sub

    Protected Sub btnDateSearch_Click(sender As Object, e As EventArgs) Handles btnDateSearch.Click
        Dim dateFrom As String = ChangeDate(txtDateFrom.Text)
        Dim dateTo As String = ChangeDate(txtDateTo.Text)
        SqlDataSourceTC.SelectCommand = "SELECT [TCNo], [RegNo], [SName], [FName], [ClassName], [SecName], [dateOfIssue] FROM [vw_StudentTC] WHERE dateofissue BETWEEN '" & dateFrom & "'" & _
             " AND '" & dateTo & "'"
        GridView1.DataBind()
    End Sub

    Protected Sub btnClassSearch_Click(sender As Object, e As EventArgs) Handles btnClassSearch.Click
        If Trim(cboClass.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Class is required...');", True)
            cboClass.Focus()
            Exit Sub
        End If
        If Trim(cboSection.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Section is required...');", True)
            cboSection.Focus()
            Exit Sub
        End If
        SqlDataSourceTC.SelectCommand = "SELECT [TCNo], [RegNo], [SName], [FName], [ClassName], [SecName], [dateOfIssue] FROM [vw_StudentTC] WHERE ClassName='" & cboClass.Text & "' AND SecName='" & cboSection.Text & "'"
        GridView1.DataBind()
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        Dim TCNo As String = GridView1.SelectedRow.Cells(1).Text
        Response.Write("<script> window.open( 'Cert_TC.aspx?TCNo=" & TCNo & "' ); </script>")
    End Sub

    Protected Sub cboSchoolName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSchoolName.SelectedIndexChanged
        LoadMasterInfo(2, cboClass, cboSchoolName.Text)
        cboClass.Items.Add("ALL")
        cboSchoolName.Focus()
    End Sub
End Class