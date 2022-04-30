Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class ParamsMaster
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
        sqlStr = "Select * from Params"


        Dim myreader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myreader.Read
            Try
                txtschoolname.Text = myreader("SchoolName")
            Catch ex As Exception
                txtschoolname.Text = ""
            End Try
            Try
                txtschooldetails.Text = myreader("SchoolDetails")
            Catch ex As Exception
                txtschooldetails.Text = ""
            End Try
            Dim smsfacility As Integer = 0
            Try
                smsfacility = myreader("SMSFacility")
                If smsfacility = 1 Then
                    cbosmsfacility.Text = "Yes"
                Else
                    cbosmsfacility.Text = "No"
                End If
            Catch ex As Exception
                cbosmsfacility.Text = "No"
            End Try
            Dim srno As Integer = 0
            Try
                srno = myreader("SRAndFeeBookSame")
                If srno = 1 Then
                    cbosrno.Text = "Yes"
                Else
                    cbosrno.Text = "No"
                End If
            Catch ex As Exception
                cbosrno.Text = "No"
            End Try
            Try
                txturlkeywords.Text = myreader("URLkeyWords")
            Catch ex As Exception
                txturlkeywords.Text = ""
            End Try
            Dim tmpdate As Date = Now.Date
            Try
                tmpdate = myreader("DOBAgeOnDate")
                txtageon.Text = tmpdate.ToString("dd/MM/yyyy")
            Catch ex As Exception
                txtageon.Text = Now.Date
            End Try
            txtageon.Text = tmpdate.ToString("dd/MM/yyyy")
            Dim onlineentryallowed As Integer = 0
            Try
                onlineentryallowed = myreader("OnlineEntryAllowed")
                If onlineentryallowed = 1 Then
                    cboonlineentryallowed.Text = "Yes"
                Else
                    cboonlineentryallowed.Text = "No"
                End If
            Catch ex As Exception

            End Try
        End While
        myreader.Close()


    End Sub
    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim Ageondate As Date = Now.Date
        Try
            Ageondate = CDate(txtageon.Text.Substring(6, 4) & "/" & txtageon.Text.Substring(3, 2) & "/" & txtageon.Text.Substring(0, 2))
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Date...');", True)
            txtageon.Focus()
            Exit Sub
        End Try
        Dim sqlStr As String = ""
        Dim smsfacility As Integer = 0
        Dim srno As Integer = 0
        If cbosmsfacility.Text = "Yes" Then
            smsfacility = 1
        Else
            smsfacility = 0
        End If
        If cbosrno.Text = "Yes" Then
            srno = 1
        Else
            srno = 0
        End If
        Dim onlineentryallowed As Integer = 0
        If cboonlineentryallowed.Text = "Yes" Then
            onlineentryallowed = 1
        Else
            onlineentryallowed = 0
        End If
        sqlStr = "select Count(*) from Params"
        Dim noofrows As Integer = ExecuteQuery_ExecuteScalar(sqlStr)
        If noofrows >= 1 Then
            sqlStr = "update Params set SchoolName='" & SQLFixup(txtschoolname.Text) & "', SchoolDetails='" & SQLFixup(txtschooldetails.Text) & "', SMSFacility='" & SQLFixup(smsfacility) & "', SRAndFeeBookSame='" & SQLFixup(srno) & "', URLkeyWords='" & SQLFixup(txturlkeywords.Text) & "', DOBAgeOnDate='" & SQLFixup(Ageondate.ToString("yyyy/MM/dd")) & "', OnlineEntryAllowed='" & SQLFixup(onlineentryallowed) & "'"
            ExecuteQuery_Update(sqlStr)
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Record Updated Successfully...');", True)
        Else
            sqlStr = "insert into Params(SchoolName,SchoolDetails,SMSFacility,SRAndFeeBookSame,URLkeyWords,DOBAgeOnDate,OnlineEntryAllowed) values ('" & SQLFixup(txtschoolname.Text) & "','" & SQLFixup(txtschooldetails.Text) & "','" & SQLFixup(smsfacility) & "','" & SQLFixup(srno) & "','" & SQLFixup(txturlkeywords.Text) & "','" & SQLFixup(Ageondate.ToString("yyyy/MM/dd")) & "','" & SQLFixup(onlineentryallowed) & "')"
            ExecuteQuery_Update(sqlStr)
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Record Inserted Successfully...');", True)
        End If
    End Sub
End Class