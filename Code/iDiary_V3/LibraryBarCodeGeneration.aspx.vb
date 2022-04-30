Imports System.Data.SqlClient
Imports Microsoft.Reporting.WebForms
Imports iDiary_V3.iDiary.CLS_idiary

Public Class LibraryBarCodeGeneration
    Inherits System.Web.UI.Page


    Protected Sub btnGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        If optRange.Checked = True Then
            If IsNumeric(txtStartNo.Text) = False Then
                lblStatus.Text = "Invalid Start Number..."
                txtStartNo.Focus()
                Exit Sub
            End If

            If IsNumeric(txtLastNo.Text) = False Then
                lblStatus.Text = "Invalid Last Number..."
                txtLastNo.Focus()
                Exit Sub
            End If

            If Val(txtLastNo.Text) < Val(txtStartNo.Text) Then
                lblStatus.Text = "Last Number should be greater than Start Number..."
                txtLastNo.Focus()
                Exit Sub
            End If
        End If

        Dim sqlStr As String = ""
        Dim i As Integer = 0
        
        sqlStr = "Delete From rptLibraryBarCodes"

        ExecuteQuery_Update(sqlStr)

        Dim lstCodeList As New ListBox
        Dim AccNo As String = ""
        If rbBook.Checked = True Then
            'AccNo = "B"
            AccNo = ""
        ElseIf rbMagazine.Checked = True Then
            AccNo = "M"
        Else
            AccNo = "D"
        End If

        If optRange.Checked = True Then
            lstCodeList.Items.Clear()
            For i = Val(txtStartNo.Text) To Val(txtLastNo.Text)
                'AccNo = AccNo & i.ToString("0000")
                If rbBook.Checked = True Then
                    'AccNo = "B"
                    AccNo = AccNo & i
                    '.ToString("0000")
                    'ElseIf rbMagazine.Checked = True Then
                Else
                    AccNo = AccNo & i.ToString("0000")
                    '    AccNo = "D"
                End If
                lstCodeList.Items.Add(AccNo)
                If rbBook.Checked = True Then
                    'AccNo = "B"
                    AccNo = ""
                ElseIf rbMagazine.Checked = True Then
                    AccNo = "M"
                Else
                    AccNo = "D"
                End If
            Next

        ElseIf optInd.Checked = True Then

            lstCodeList.Items.Clear()
            Dim myCodes() As String = txtBarCodes.Text.Split(",")
            For i = 0 To myCodes.Count - 1
                '
                If rbBook.Checked = True Then
                    'AccNo = "B"
                    'ElseIf rbMagazine.Checked = True Then
                    '    AccNo = "M"
                    AccNo = i
                    lstCodeList.Items.Add(myCodes(AccNo))
                Else
                    'AccNo = "D"
                    AccNo = AccNo & Convert.ToInt32(myCodes(i)).ToString("0000")
                End If
                If rbBook.Checked = True Then
                    'AccNo = "B"
                    AccNo = ""
                ElseIf rbMagazine.Checked = True Then
                    AccNo = "M"
                Else
                    AccNo = "D"
                End If
            Next

        End If

        For i = 0 To lstCodeList.Items.Count - 1
            Dim BookName As String = "", BookCodeNo As String = "", BarCode As String = ""
            Dim myCount As Integer = 0
            If rbBook.Checked = True Then
                sqlStr = "Select BookCodeNo, BookTitle From BookMaster Where BookAccNo='" & lstCodeList.Items(i).Text & "'"
            Else
                sqlStr = "Select BookCodeNo, BookTitle From BookMaster Where AccNo='" & lstCodeList.Items(i).Text & "'"
            End If
            
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                BarCode = lstCodeList.Items(i).Text
                BookCodeNo = myReader("BookCodeNo")
                BookName = myReader("BookTitle")
                myCount += 1
            End While
            myReader.Close()

            If myCount > 0 Then
                sqlStr = "Insert into rptLibraryBarCodes Values('*" & BarCode & "*','" & BookCodeNo & "','" & BookName.Replace("'", "''") & "')"
                ExecuteQuery_Update(sqlStr)
            Else
                Continue For
            End If
        Next
        System.Threading.Thread.Sleep(500)
        PrepareReport()
    End Sub
    Private Sub PrepareReport()
        Dim sqlStr As String = ""
        sqlStr = "select * from rptLibraryBarCodes"
        Dim ds As New DataSet
        ds = ExecuteQuery_DataSet(sqlStr, "tbl")
        Dim rds As ReportDataSource = New ReportDataSource()
        rds.Name = "DataSet1" ' Change to what you will be using when creating an objectdatasource
        rds.Value = ds.Tables(0)
        With ReportViewer1   ' Name of the report control on the form
            .Reset()
            .ProcessingMode = ProcessingMode.Local
            .LocalReport.DataSources.Clear()
            .Visible = True
            .LocalReport.ReportPath = "Report\rptLibraryBarCode.rdlc"
            .LocalReport.DataSources.Add(rds)
        End With
        ReportViewer1.Visible = True
        ReportViewer1.LocalReport.Refresh()

    End Sub
  
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Request.Cookies("UType").Value.ToString.Contains("Library") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
            'Allow
        Else
            Response.Redirect("AccessDenied.aspx")
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
    End Sub

    Private Sub InitControls()
        'ReportViewer1.Visible = False
        txtStartNo.Text = ""
        txtLastNo.Text = ""
        optRange.Checked = True
        optInd.Checked = False
        txtStartNo.Focus()
    End Sub

End Class