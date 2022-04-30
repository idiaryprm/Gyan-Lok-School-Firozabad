Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class Index
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If SMSFaciltyOpted() = 0 Then
            'btnSend.Visible = False
        Else
            'btnSend.Visible = True
        End If
        'SqlDataSource1.SelectCommand = "Select RegNo, SName, ClassName, SecName, DOB, MobNo From vw_Student Where ASID=" & Request.Cookies("ASID").Value & " AND Day(DOB)=" & Today.Day & " AND Month(DOB)=" & Today.Month
        'GridView1.DataBind()
        'If Request.Cookies("UType").Value.ToString.Contains("Admin") Then   'Only Permissible for Admin
        '    Try
        '        If GridView1.Rows.Count > 0 Then
        '            ' AccordianMain.Visible = True
        '            If SMSFaciltyOpted() = True Then
        '                btnSend.Visible = True
        '            Else
        '                btnSend.Visible = False
        '            End If

        '        Else
        '            ' AccordianMain.Visible = False
        '        End If
        '    Catch ex As Exception

        '    End Try

        'End If

        'lblSchoolName.Text = FindSchoolDetails(1)
        lblSchoolName.Text = "GYAN LOK INTER COLLEGE"
        LoadManufacturerDetails()
        'loadDashData()
        'If Request.Cookies("UType").Value.ToString.Contains("Admin") Or Request.Cookies("UType").Value.ToString.Contains("Student") Then
        '    'lblBirthDay.Visible = True
        '    'GridView1.Visible = True
        'Else
        '    'lblBirthDay.Visible = False
        '    GridView1.Visible = False
        'End If
        'If Request.Cookies("UType").Value.ToString.Contains("Admin-1") Then
        '    If SMSFaciltyOpted() = True Then
        '        btnSend.Visible = True
        '    Else
        '        btnSend.Visible = False
        '    End If
        'End If
    End Sub

    Private Sub LoadManufacturerDetails()

        Dim sqlStr As String = ""

        sqlStr = "Select * From Manufacturer"


        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read

            Try
                lblManfName.Text = myReader("ManfName")
            Catch ex As Exception
                lblManfName.Text = ""
            End Try

            Try
                lblManfAddress1.Text = myReader("ManfAddress1")
            Catch ex As Exception
                lblManfAddress1.Text = ""
            End Try

            Try
                lblManfAddress2.Text = myReader("ManfAddress2")
            Catch ex As Exception
                lblManfAddress2.Text = ""
            End Try

            Try
                lblManfCity.Text = myReader("ManfCity")
            Catch ex As Exception
                lblManfCity.Text = ""
            End Try

            Try
                lblManfEmail.Text = myReader("ManfEmail")
            Catch ex As Exception
                lblManfEmail.Text = ""
            End Try

            Try
                lblManfPhone.Text = myReader("ManfPhone")
            Catch ex As Exception
                lblManfPhone.Text = ""
            End Try

            Try
                lblManfURL.Text = myReader("ManfURL")
            Catch ex As Exception
                lblManfURL.Text = ""
            End Try

        End While

        myReader.Close()

    End Sub

    'Protected Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click

    '    Dim sqlStr As String = ""
    '    Dim MobNo As String = "", SName As String = "", myMessage As String = ""
    '    Dim SMSResponse As String = "", SenderID As String = ""

    '    sqlStr = "Select SMSSender From SMSSender"


    '    Dim SMSReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
    '    While SMSReader.Read
    '        SenderID = SMSReader(0)
    '    End While
    '    SMSReader.Close()

    '    sqlStr = "SELECT SName, MobNo FROM vw_Student Where ASID=" & Request.Cookies("ASID").Value & " AND  (DAY(DOB) = " & Now.Day & ") AND (MONTH(DOB) = " & Now.Month & ")"


    '    Dim MobReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
    '    While MobReader.Read
    '        Try
    '            SName = MobReader(0)
    '            MobNo = MobReader(1)
    '            myMessage = StudentBirthdaySMSMessage
    '            myMessage = myMessage.Replace("<*>", SName) 'Change Variable by Student Name
    '            SMSResponse = SendMySMS(SenderID, MobNo, myMessage)
    '        Catch ex As Exception
    '            Continue While
    '        End Try
    '    End While
    '    MobReader.Close()
    'End Sub
End Class