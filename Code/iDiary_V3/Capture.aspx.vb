Imports System.IO
Imports System.Web.Services
Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Partial Class Capture
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            txtAdminNo.Text = ""
            txtSName.Text = ""
            txtFName.Text = ""
            txtMName.Text = ""
            txtAdmissionDate.Text = ""
            txtDOB.Text = ""
            txtRollNo.Text = ""
            txtClass.Text = ""
            txtSec.Text = ""
            txtSID.Text = ""
            txtGender.Text = ""
            If Request.InputStream.Length > 0 Then
                Using reader As New StreamReader(Request.InputStream)
                    Dim hexString As String = Server.UrlEncode(reader.ReadToEnd())
                    Dim imageName As String = DateTime.Now.ToString("dd-MM-yy hh-mm-ss")
                    Dim imagePath As String = String.Format("~/Captures/{0}.png", imageName)
                    File.WriteAllBytes(Server.MapPath(imagePath), ConvertHexToBytes(hexString))
                    Session("CapturedImage") = ResolveUrl(imagePath)
                End Using
            End If
        End If
    End Sub
    Protected Sub btnFind_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFind.Click
        If txtAdminNo.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Registration No is required...');", True)
            txtAdminNo.Focus()
            Exit Sub
        End If
        FillStudentDetail(txtAdminNo.Text)
        txtSName.ReadOnly = True
        txtAdminNo.Focus()
    End Sub
    Private Sub FillStudentDetail(RegNo As String)
        Dim sqlStr As String = ""
        sqlStr = "Select RegNo, SName, FName, MName, AdmissionDate, DOB, ClassName,SecName,Gender,ClassRollNo,SID From vw_Student Where RegNo='" & RegNo & "' AND ASID=" & Request.Cookies("ASID").Value
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)

        While myReader.Read
            txtSName.Text = myReader("SName")
            txtFName.Text = myReader("FName")
            txtMName.Text = myReader("MName")
            Dim tmpDate As Date = myReader("AdmissionDate")
            'txtAdmissionDate.Text = a.Substring(3, 2) & "/" & a.Substring(0, 2) & "/" & a.Substring(6, 4)
            txtAdmissionDate.Text = tmpDate.ToString("dd/MM/yyyy")
            tmpDate = myReader("DOB")
            'txtDOB.Text = a.Substring(3, 2) & "/" & a.Substring(0, 2) & "/" & a.Substring(6, 4)
            txtDOB.Text = tmpDate.ToString("dd/MM/yyyy")
            txtClass.Text = myReader("ClassName")
            txtSec.Text = myReader("SecName")
            '  txtDOBInWords.Text = ConvertDateToWords(tmpDate.Day, tmpDate.Month, tmpDate.Year)
            txtGender.Text = myReader("Gender")
            txtSID.Text = myReader("SID")
            Try
                txtRollNo.Text = myReader("ClassRollNo")
            Catch ex As Exception

            End Try
        End While
        myReader.Close()
    End Sub
    Private Shared Function ConvertHexToBytes(hex As String) As Byte()
        Dim bytes As Byte() = New Byte(hex.Length / 2 - 1) {}
        For i As Integer = 0 To hex.Length - 1 Step 2
            bytes(i / 2) = Convert.ToByte(hex.Substring(i, 2), 16)
        Next
        Return bytes
    End Function

    <WebMethod(EnableSession:=True)> _
    Public Shared Function GetCapturedImage() As String
        Dim url As String = HttpContext.Current.Session("CapturedImage").ToString()
        HttpContext.Current.Session("CapturedImage") = Nothing
        Return url
    End Function

End Class

