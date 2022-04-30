Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class PettyCashStatement
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Accounts") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
            End If
        Catch ex As Exception
            response.redirect("~/Login.aspx")
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            InitControls()

        Else
            'For Grid View Printing. Must have a blank HTM Page (gview.htm)
            Dim printScript As String = "function PrintGridView() { var gridInsideDiv = document.getElementById('gvDiv');" & _
            " var printWindow = window.open('gview.htm','PrintWindow','letf=0,top=0,width=150,height=300,toolbar=1,scrollbars=1,status=1');" & _
            " printWindow.document.write(gridInsideDiv.innerHTML);printWindow.document.close();printWindow.focus();" & _
            " printWindow.print();printWindow.close();}"
            Me.ClientScript.RegisterStartupScript(Page.[GetType](), "PrintGridView", printScript.ToString(), True)
            btnPrint.Attributes.Add("onclick", "PrintGridView();")
        End If
    End Sub

    Private Sub InitControls()
        LoadMasterInfo(17, cboTrans)
        cboTrans.Items.Add("Composite")
        txtFrom.Text = Now.Date
        txtTo.Text = Now.Date
        lblStatus.Text = ""

        ClearPettyCashStatementTable()
        GridView1.DataBind()

        lblSchoolName.Visible = False
        lblTitle.Visible = False
        btnPrint.Visible = False
        btnExcel.Visible = False

        cboTrans.Focus()
    End Sub

    Protected Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click

        Dim sqlStr As String = ""

       
       
       

        

        sqlStr = "DROP TABLE rptPettyCashStatements"
        
        
        ExecuteQuery_Update(SqlStr)

        If cboTrans.Text.Contains("Composite") Then
            sqlStr = "CREATE TABLE rptPettyCashStatements(" & _
                "[Date] date," & _
                "[Head Name] nvarchar(500)," & _
                "[Income] float," & _
                "[Expenditure] float" & _
                ");"
        Else
            sqlStr = "CREATE TABLE rptPettyCashStatements(" & _
                "[Date] date," & _
                "[Head Name] nvarchar(500)," & _
                "[Amount] float," & _
                "[Remark] nvarchar(500)" & _
                ");"
        End If

        
        
        ExecuteQuery_Update(SqlStr)

        Dim TempStr As String = ""
        Dim lstSQL As New ListBox
        lstSQL.Items.Clear()

        If cboTrans.Text.Contains("Composite") Then
            'Insert Income Records
            sqlStr = "Insert into rptPettyCashStatements ([Date],[Head Name],[Income]) " & _
                     "Select TransDate, PCHeadName, TransAmount From vwPettyCashTransaction " & _
                     "Where TransDate Between '" & CDate(txtFrom.Text).Month & "/" & CDate(txtFrom.Text).Day & "/" & CDate(txtFrom.Text).Year & "' " & _
                     "and '" & CDate(txtTo.Text).Month & "/" & CDate(txtTo.Text).Day & "/" & CDate(txtTo.Text).Year & "' " & _
                     " AND TransTypeName Like '%Income%'" & _
                     " Order By TransDate"

            
            
            ExecuteQuery_Update(SqlStr)

            'Insert Expenditure Records
            sqlStr = "Insert into rptPettyCashStatements ([Date],[Head Name],[Expenditure]) " & _
                     "Select TransDate, PCHeadName, TransAmount From vwPettyCashTransaction " & _
                     "Where TransDate Between '" & CDate(txtFrom.Text).Month & "/" & CDate(txtFrom.Text).Day & "/" & CDate(txtFrom.Text).Year & "' " & _
                     "and '" & CDate(txtTo.Text).Month & "/" & CDate(txtTo.Text).Day & "/" & CDate(txtTo.Text).Year & "' " & _
                     " AND TransTypeName Like '%Expend%'" & _
                     " Order By TransDate"
            
            
            ExecuteQuery_Update(SqlStr)

        Else
            sqlStr = "Insert into rptPettyCashStatements " & _
                     "Select TransDate, PCHeadName, TransAmount, TransRemark From vwPettyCashTransaction " & _
                     "Where TransDate Between '" & CDate(txtFrom.Text).Month & "/" & CDate(txtFrom.Text).Day & "/" & CDate(txtFrom.Text).Year & "' " & _
                     "and '" & CDate(txtTo.Text).Month & "/" & CDate(txtTo.Text).Day & "/" & CDate(txtTo.Text).Year & "' " & _
                     " AND TransTypeName='" & cboTrans.Text & "'" & _
                     " Order By TransDate"
            
            
            ExecuteQuery_Update(SqlStr)
        End If

        
        
        SqlDataSource1.SelectCommand &= " Order By [Date]"
        GridView1.DataBind()
        ChangeDateCellFormat()
        lblSchoolName.Text = FindSchoolDetails(1)
        lblTitle.Text = "Petty Cash Statement - " & cboTrans.Text & " (" & txtFrom.Text & " - " & txtTo.Text & ")"

        If GridView1.Rows.Count <= 0 Then
            lblSchoolName.Visible = False
            lblTitle.Visible = False
            btnPrint.Visible = False
            btnExcel.Visible = False
        Else
            'Find Total
            Dim k As Integer = 0
            Dim mySum1 As Double = 0, mySum2 As Double = 0

            If cboTrans.Text.Contains("Composite") Then
                For k = 0 To GridView1.Rows.Count - 1
                    mySum1 += Val(GridView1.Rows(k).Cells(2).Text)
                    mySum2 += Val(GridView1.Rows(k).Cells(3).Text)
                Next
                GridView1.FooterRow.Cells(1).Text = "Total"
                GridView1.FooterRow.Cells(2).Text = mySum1
                GridView1.FooterRow.Cells(3).Text = mySum2
                LitBalance.Text = "Balance = " & mySum1 - mySum2
            Else
                For k = 0 To GridView1.Rows.Count - 1
                    mySum1 += Val(GridView1.Rows(k).Cells(2).Text)
                Next
                GridView1.FooterRow.Cells(1).Text = "Total"
                GridView1.FooterRow.Cells(2).Text = mySum1
            End If
            lblSchoolName.Visible = True
            lblTitle.Visible = True
            btnPrint.Visible = True
            btnExcel.Visible = True
        End If
    End Sub

    Private Sub ChangeDateCellFormat()
        Dim i As Integer = 0
        For i = 0 To GridView1.Rows.Count - 1
            GridView1.Rows(i).Cells(0).Text = GridView1.Rows(i).Cells(0).Text.Substring(0, 10)
        Next
    End Sub

    Protected Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        Dim sw As New System.IO.StringWriter()
        Dim hw As New System.Web.UI.HtmlTextWriter(sw)
        Dim frm As HtmlForm = New HtmlForm()

        Dim filename As String = "PettyCashStatement_" + DateTime.Now.ToString() + ".xls"

        Page.Response.AddHeader("content-disposition", "attachment;filename=" + filename)
        Page.Response.ContentType = "application/vnd.ms-excel"
        Page.Response.Charset = ""
        Page.EnableViewState = False
        frm.Attributes("runat") = "server"
        Controls.Add(frm)
        frm.Controls.Add(GridView1)
        frm.RenderControl(hw)
        Response.Write(sw.ToString())
        Response.End()


    End Sub
End Class