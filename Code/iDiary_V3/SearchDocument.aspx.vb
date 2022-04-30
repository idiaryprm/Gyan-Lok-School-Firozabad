Imports System.IO
Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class SearchDocument
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("e-Docs") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
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

        chkFileNo.Checked = False
        txtFileNo.Text = ""

        chkSubject.Checked = False
        txtSubject.Text = ""

        chkContents.Checked = False
        txtContents.Text = ""

        chkDocDate.Checked = False
        txtDocDate_DD.Text = Now.Day
        txtDocDate_MM.Text = Now.Month
        txtDocDate_YY.Text = Now.Year
        txtDocDate_DDTo.Text = Now.Day
        txtDocDate_MMTo.Text = Now.Month
        txtDocDate_YYTo.Text = Now.Year

        lblStatus.Text = ""
        lblTotalRecords.Text = ""

        If GridView1.Rows.Count > 0 Then
            btnPrint.Visible = True
        Else
            btnPrint.Visible = False
        End If

        GridView1.Visible = False

        chkFileNo.Focus()
    End Sub


    Protected Sub btnExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcel.Click

        Dim sw As New StringWriter()
        Dim hw As New System.Web.UI.HtmlTextWriter(sw)
        Dim frm As HtmlForm = New HtmlForm()

        Dim filename As String = "DocSearch_" + DateTime.Now.ToString() + ".xls"

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

    Protected Sub btnFind_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnFind.Click
        lblStatus.Text = ""

        If chkFileNo.Checked = False And chkSubject.Checked = False And chkContents.Checked = False And chkDocDate.Checked = False Then
            lblStatus.Text = "Please Select atleast one criteria to continue..."
            chkFileNo.Focus()
            Exit Sub
        End If

        If chkFileNo.Checked = True And Trim(txtFileNo.Text).Length <= 0 Then
            lblStatus.Text = "Provide atleast one character as File No to continue..."
            txtFileNo.Focus()
            Exit Sub
        End If

        If chkSubject.Checked = True And Trim(txtSubject.Text).Length <= 0 Then
            lblStatus.Text = "Provide atleast one character as subject to continue..."
            txtSubject.Focus()
            Exit Sub
        End If

        If chkContents.Checked = True And Trim(txtContents.Text).Length <= 0 Then
            lblStatus.Text = "Provide atleast few characters in content to continue..."
            txtContents.Focus()
            Exit Sub
        End If

        If chkDocDate.Checked = True Then
            If IsNumeric(txtDocDate_DD.Text) = False Or IsNumeric(txtDocDate_MM.Text) = False Or IsNumeric(txtDocDate_YY.Text) = False Then
                lblStatus.Text = "Invalid Document From Date..."
                txtDocDate_DD.Focus()
                Exit Sub
            End If
            If IsNumeric(txtDocDate_DDTo.Text) = False Or IsNumeric(txtDocDate_MMTo.Text) = False Or IsNumeric(txtDocDate_YYTo.Text) = False Then
                lblStatus.Text = "Invalid Document To Date..."
                txtDocDate_DDTo.Focus()
                Exit Sub
            End If
        End If

        'Prepare SQL Query
        Dim sqlStr As String = ""
        sqlStr = "Delete From rptDocumentSearch"

        ExecuteQuery_Update(sqlStr)
        sqlStr = "Insert into rptDocumentSearch "
        sqlStr &= "Select FileNo, FileDate, FileSubject, FileContents, FilePath From Documents Where "

        If chkFileNo.Checked = True Then sqlStr &= " FileNo Like '" & txtFileNo.Text & "%' AND "
        If chkSubject.Checked = True Then sqlStr &= " FileSubject Like '%" & txtSubject.Text & "%' AND "
        If chkContents.Checked = True Then sqlStr &= " FileContents Like '%" & txtContents.Text & "%' AND "
        If chkDocDate.Checked = True Then sqlStr &= " FileDate Between '" & txtDocDate_MM.Text & "/" & txtDocDate_DD.Text & "/" & txtDocDate_YY.Text & "' and '" & txtDocDate_MMTo.Text & "/" & txtDocDate_DDTo.Text & "/" & txtDocDate_YYTo.Text & "' AND "

        sqlStr = sqlStr.Substring(0, sqlStr.Length - 4)

        sqlStr &= " Order By FileDate, FileSubject, FileNo"

       
       
        ExecuteQuery_Update(sqlStr)

        

        Dim myHeaderText As String = "Search Parameter: "
        Dim ANDFlag As Boolean = False

        If chkFileNo.Checked = True Then
            If ANDFlag = True Then myHeaderText &= ", "
            myHeaderText &= "File No = " & txtFileNo.Text
            ANDFlag = True
        End If
        If chkSubject.Checked = True Then
            If ANDFlag = True Then myHeaderText &= ", "
            myHeaderText &= "Subject = " & txtSubject.Text
            ANDFlag = True
        End If
        If chkContents.Checked = True Then
            If ANDFlag = True Then myHeaderText &= ", "
            myHeaderText &= "Contents = " & txtContents.Text
            ANDFlag = True
        End If
        If chkDocDate.Checked = True Then
            If ANDFlag = True Then myHeaderText &= ", "
            myHeaderText &= "File Date = " & txtDocDate_DD.Text & "/" & txtDocDate_MM.Text & "/" & txtDocDate_YY.Text & "-" & txtDocDate_DDTo.Text & "/" & txtDocDate_MMTo.Text & "/" & txtDocDate_YYTo.Text
            ANDFlag = True
        End If


        sqlStr = "Select Schoolname From Params"
        
        
        Dim myreader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myreader.Read
            lblSchoolName.Text = myreader(0)
        End While
        myreader.Close()
        
        

        GridView1.DataBind()
        GridView1.Visible = True
        lblTitle.Text = myHeaderText
        lblTotalRecords.Text = "Total " & GridView1.Rows.Count & " records found..."


        If GridView1.Rows.Count > 0 Then
            btnPrint.Visible = True
        Else
            btnPrint.Visible = False
        End If

    End Sub

End Class