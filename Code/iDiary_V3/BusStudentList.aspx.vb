Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary_Fee.CLS_iDiary_Fee

Public Class BusStudentList
    Inherits System.Web.UI.Page


    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        Try

            If Request.Cookies("UType").Value.ToString.Contains("Fee") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
            End If

        Catch ex As Exception

            If ex.Message.Contains("Object reference not set to an instance of an object") Then
                Response.Redirect("~/Login.aspx")
            End If

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

        LoadMasterInfo(2, cboClass)
        cboClass.Items.Add("ALL")
        cboSection.Items.Clear()
        LoadMasterInfo(10, cboStatus)
        LoadFeeTerms(cboTerm, 1, "BusFee")
        LoadMasterInfo(45, cboBus)
        GridView1.Visible = True

        btnPrint.Enabled = False
        btnExcel.Enabled = False


        cboClass.Focus()

    End Sub

    Protected Sub cboClass_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboClass.SelectedIndexChanged
        LoadClassSection(cboClass.Text, cboSection)
        cboSection.Items.Add("ALL")
        cboClass.Focus()
    End Sub

    Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        lblSchoolName.Visible = True
    End Sub

    Protected Sub btnExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcel.Click

        Dim sw As New System.IO.StringWriter()
        Dim hw As New System.Web.UI.HtmlTextWriter(sw)
        Dim frm As HtmlForm = New HtmlForm()

        Dim filename As String = "BusFeeDueList_" + DateTime.Now.ToString() + ".xls"

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

    Protected Sub btnViewSummaryList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnViewSummaryList.Click
        If cboClass.Text = "" Then
            lblStatus.Text = "Invalid Class..."
            cboClass.Focus()
            Exit Sub
        End If
        If cboSection.Text = "" Then
            lblStatus.Text = "Invalid Section..."
            cboSection.Focus()
            Exit Sub
        End If
        'ALL-XXX (Not Allowed...)
        If cboClass.Text = "ALL" And cboSection.Text <> "ALL" Then
            lblStatus.Text = "Invalid Section (Please Select --ALL--)"
            cboSection.Focus()
            Exit Sub
        End If
        'XXX-ALL (Not Allowed...)
        If cboClass.Text <> "ALL" And cboSection.Text = "ALL" Then
            lblStatus.Text = "Invalid Class (Please Select --ALL--)"
            cboClass.Focus()
            Exit Sub
        End If
        If cboStatus.Text = "" Then
            lblStatus.Text = "Invalid Status..."
            cboStatus.Focus()
            Exit Sub
        End If
        If cboTerm.Text = "" Then
            lblStatus.Text = "Invalid Term..."
            cboTerm.Focus()
            Exit Sub
        End If
      
     

        lblStatus.Text = ""

        Dim sqlStr As String = "", CollegeNote As String = ""
        Dim i As Integer = 0
        Dim lstClass As New ListBox, lstSection As New ListBox

        lstClass.Items.Clear()
        lstSection.Items.Clear()

       
       
       

        



        sqlStr = "Select SID, FeeBookNo, SName,FName,CASE WHEN Gender = 0 THEN 'Male' ELSE 'Female' END AS Gender,ClassName,SecName,DOB,TempAddress,MobNo,BusName From vw_StudentBusList where StatusName='" & cboStatus.Text & "' and (TermNo='" & cboTerm.Text & "' or TermNo is null) and ASID=" & Request.Cookies("ASID").Value

        If cboClass.Text <> "ALL" And cboSection.Text <> "ALL" Then     ''XXX-XXX(Allowed)
            sqlStr += " and ClassName='" & cboClass.Text & "' and SecName='" & cboSection.Text & "'"
        End If
        If cboBus.Text <> "" Then
            sqlStr += " and BusName='" & cboBus.Text & "'"
        End If
        sqlStr += " Order By DisplayOrder, ClassName, secName"

        SqlDataSource1.SelectCommand = sqlStr

        'Header Contents
        lblSchoolName.Text = FindSchoolDetails(1)
        lblTitle.Text = "Bus Student List"
        ' for As On " & Now.Date.Day & "/" & Now.Date.Month & "/" & Now.Date.Year

        'Bind DataGrid
        GridView1.DataBind()

        'If No Content, Hide Print Button
        If GridView1.Rows.Count > 0 Then
            lblSchoolName.Visible = True
            lblTitle.Visible = True
            btnPrint.Enabled = True
            btnExcel.Enabled = True
            GridView1.Visible = True
            lblTotalRecords.Text = "Total " & GridView1.Rows.Count & " records found..."
        Else
            lblSchoolName.Visible = False
            lblTitle.Visible = False

            btnPrint.Enabled = False
            btnExcel.Enabled = False
            GridView1.Visible = False
            lblTotalRecords.Text = ""
        End If


    End Sub

    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        For Each tableCell As TableCell In e.Row.Cells
            Dim cell As DataControlFieldCell = CType(tableCell, DataControlFieldCell)
            If cell.ContainingField.HeaderText = "Fee Book No" And chkFBnoC.Checked = False Then
                cell.Visible = False
                Continue For
            End If

            If cell.ContainingField.HeaderText = "Student Name" And chkSNameC.Checked = False Then
                cell.Visible = False
                Continue For
            End If

            If cell.ContainingField.HeaderText = "Father's Name" And chkFNameC.Checked = False Then
                cell.Visible = False
                Continue For
            End If

            If cell.ContainingField.HeaderText = "Gender" And chkGenderC.Checked = False Then
                cell.Visible = False
                Continue For
            End If

            If cell.ContainingField.HeaderText = "Class" And chkClassC.Checked = False Then
                cell.Visible = False
                Continue For
            End If

            If cell.ContainingField.HeaderText = "Section" And chkSectionC.Checked = False Then
                cell.Visible = False
                Continue For
            End If

            

            If cell.ContainingField.HeaderText = "DOB" And chkDobC.Checked = False Then
                cell.Visible = False
                Continue For
            End If

            If cell.ContainingField.HeaderText = "Address" And chkAddressC.Checked = False Then
                cell.Visible = False
                Continue For
            End If

            If cell.ContainingField.HeaderText = "Mobile" And chkMobileC.Checked = False Then
                cell.Visible = False
                Continue For
            End If

            If cell.ContainingField.HeaderText = "Bus" And chkBusC.Checked = False Then
                cell.Visible = False
                Continue For
            End If
        Next
    End Sub
End Class

