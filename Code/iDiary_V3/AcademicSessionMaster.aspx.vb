Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class AcademicSessionMaster
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Student") Or Request.Cookies("UType").Value.ToString.Contains("Payroll") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
            End If
        Catch ex As Exception
            Response.Redirect("~/Login.aspx")
        End Try
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
    End Sub

    Private Sub InitControls()
        ' txtName.Text = ""
        txtID.Text = ""
        LoadMasterInfo(1, lstMasters)
        'txtName.Focus()
        'chkDefault.Checked = False
    End Sub

   

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Dim sqlStr As String = ""
        sqlStr = "select * from AcademicSession where asid=(select MAX(ASID) from AcademicSession )"
        Dim tmpDAte As Date = Now.Date
        Dim ASName As String = ""
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        sqlStr = "Insert into AcademicSession "
        While myReader.Read
            ASName = myReader("ASName")
            ASName = getNewASName(ASName)
            tmpDAte = myReader("DateAdmissionStart")
            tmpDAte = tmpDAte.AddYears(1)
            sqlStr &= " Values('" & ASName & "','" & tmpDAte.ToString("yyyy-MM-dd") & "'"
            tmpDAte = myReader("DateAdmissionEnd")
            tmpDAte = tmpDAte.AddYears(1)
            sqlStr &= " ,'" & tmpDAte.ToString("yyyy-MM-dd") & "')"
        End While
        myReader.Close()

        ExecuteQuery_Update(sqlStr)
        InitControls()
    End Sub
    Private Function getNewASName(ByVal asName As String) As String
        Dim rv As Integer = 0

        Try
            rv = asName.Split("-")(1)
            asName = "20" & rv & "-" & (CInt(rv + 1))
        Catch ex As Exception

        End Try
        Return asName
    End Function

    
End Class