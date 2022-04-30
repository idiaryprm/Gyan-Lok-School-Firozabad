Imports Microsoft.Reporting.WebForms

Public Class ProgressReport
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click

    End Sub

    Private Sub CreatePDF(fileName As String)
        ' Variables
        Dim warnings As Warning()
        Dim streamIds As String()
        Dim mimeType As String = String.Empty
        Dim encoding As String = String.Empty
        Dim extension As String = String.Empty


        ' Setup the report viewer object and get the array of bytes
        Dim viewer As New ReportViewer()
        viewer.ProcessingMode = ProcessingMode.Local
        viewer.LocalReport.ReportPath = "YourReportHere.rdlc"


        Dim bytes As Byte() = viewer.LocalReport.Render("PDF", Nothing, mimeType, encoding, extension, streamIds, warnings)


        ' Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
        Response.Buffer = True
        Response.Clear()
        Response.ContentType = mimeType
        Response.AddHeader("content-disposition", "attachment; filename=" & fileName & "." & extension)
        Response.BinaryWrite(bytes)
        ' create the file
        Response.Flush()
        ' send it to the client to download
    End Sub
End Class