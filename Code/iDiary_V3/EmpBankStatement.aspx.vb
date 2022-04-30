Imports System.IO
Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class BankStatement
    Inherits System.Web.UI.Page

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
        If Request.Cookies("UType").Value.ToString.Contains("Admin-1") = False And Request.Cookies("UType").Value.ToString.Contains("Payroll-1") = False Then
            btnGenerate.Enabled = False
        End If
    End Sub


    Protected Sub btnExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcel.Click

        Dim sw As New StringWriter()
        Dim hw As New System.Web.UI.HtmlTextWriter(sw)
        Dim frm As HtmlForm = New HtmlForm()

        Dim filename As String = "BankStatement_" + DateTime.Now.ToString() + ".xls"

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

    Private Sub InitControls()
        LoadMasterInfo(22, cboBank)
        LoadMonths(cboMonth)
        LoadYears(cboYear)
        cboBank.Focus()
        lblHeader.Visible = False
        btnPrint.Visible = False
        btnExcel.Visible = False
        GridView1.Visible = False
    End Sub

    Protected Sub btnGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        If cboBank.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Select a Bank to continue...');", True)
            cboBank.Focus()
            Exit Sub
        End If
        If cboMonth.SelectedIndex = 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Select a month to continue...');", True)
            cboMonth.Focus()
            Exit Sub
        End If

        If cboYear.SelectedIndex = 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Select a Year to continue...');", True)
            cboYear.Focus()
            Exit Sub
        End If
        PrepareBankStatement()
        GridView1.DataBind()
        lblHeader.Text = "Bank Statement for " & cboMonth.Text & "-" & cboYear.Text
        If GridView1.Rows.Count > 0 Then
            lblHeader.Visible = True
            btnPrint.Visible = True
            GridView1.Visible = True
            btnExcel.Visible = True
        Else
            lblHeader.Visible = False
            btnPrint.Visible = False
            GridView1.Visible = False
            btnExcel.Visible = False
        End If
    End Sub

    Private Sub PrepareBankStatement()

        Dim sqlStr As String = ""
        Dim NetPay As Double = 0

        Dim lstEmpID As New ListBox
        Dim lstEmpName As New ListBox
        Dim lstAcc As New ListBox
        Dim lstInsert As New ListBox

       
       
       

        

        lstEmpID.Items.Clear()
        lstEmpName.Items.Clear()
        lstAcc.Items.Clear()
        lstInsert.Items.Clear()

        sqlStr = "Delete From rptBankStatement"
        
        
        ExecuteQuery_Update(SqlStr)

        sqlStr = "Select EmpID,EmpName, AccNo From vw_Employees Where BankName='" & cboBank.Text & "'"
        
        

        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            lstEmpID.Items.Add(myReader(0))
            lstEmpName.Items.Add(myReader(1))
            lstAcc.Items.Add(myReader(2))
        End While
        myReader.Close()


        Dim i As Integer = 0
        For i = 0 To lstEmpID.Items.Count - 1

            sqlStr = "Select NetPay From BankStatements Where " & _
            "EmpID=" & lstEmpID.Items.Item(i).Text & _
            " AND MonthID=" & cboMonth.SelectedIndex & _
            " AND YearID='" & cboYear.Text & "'"

            
            

            Dim NetPayReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While NetPayReader.Read
                NetPay = NetPayReader(0)
            End While
            NetPayReader.Close()

            sqlStr = "Insert into rptBankStatement Values(" & _
            "'" & SQLFixup(lstAcc.Items.Item(i).Text) & "'," & _
            "'" & SQLFixup(lstEmpName.Items.Item(i).Text) & "'," & _
            NetPay & ")"

            
            
            ExecuteQuery_Update(SqlStr)

        Next
        System.Threading.Thread.Sleep(300)

        
        
    End Sub

End Class