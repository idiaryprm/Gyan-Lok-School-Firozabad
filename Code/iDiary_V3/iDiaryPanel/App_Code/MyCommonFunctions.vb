Imports Microsoft.VisualBasic
Imports System.Net.Mail

Public Class MyCommonFunctions

    Public Function SendSMS(ByVal FMobileNo As String, ByVal MyMessage As String) As String
        Dim obj As New Class1

        'If your message includes special characters, then please use URLEncode method before passing the value of the message parameter.
        'For Ex. HttpUtility.UrlEncode(message)     	

        Dim strPostResponse As String = obj.SendSMS("alfresco", "be0a-rebel", FMobileNo, MyMessage)
        Return "SMS Server Response " & strPostResponse & ","
    End Function

    Public Function SendEmail(ByVal FatherMail As String, ByVal MyMessage As String) As String

        Try
            Dim mm As New MailMessage()
            mm.To.Add(FatherMail)
            mm.From = New MailAddress("jayash.sharma@gmail.com")
            mm.Subject = "iDiary Message"
            mm.Body = MyMessage

            Dim ss As New SmtpClient("smtp.gmail.com")
            ss.Credentials = New System.Net.NetworkCredential("jayash.sharma@gmail.com", "URY9719=")
            ss.EnableSsl = True
            ss.Send(mm)
            Return "Email sent"
        Catch ex As Exception
            Return ex.ToString
        End Try

    End Function

End Class
