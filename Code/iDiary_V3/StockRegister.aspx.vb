Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiaryV3_Inventory
Imports System.IO


Public Class StockRegister
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("ActiveTab") = 12
        If Not IsPostBack Then InitControls()
    End Sub
    Private Sub InitControls()

        txtDateFrom.Text = Now.Date.ToString("dd/MM/yyyy")
        txtDateTo.Text = Now.Date.ToString("dd/MM/yyyy")
       
        If Request.QueryString("type").ToString = "OUT" Then
            lblLedger.Text = "Ledger"
            LoadInventoryInfo(2, cboLedger)
            GridView2.Columns(5).Visible = False
            GridView2.Columns(6).Visible = True
        Else
            lblLedger.Text = "Vendor"
            LoadInventoryInfo(3, cboLedger)
            GridView2.Columns(5).Visible = True
            GridView2.Columns(6).Visible = False
        End If
        LoadInventoryInfo(4, cboItem)
        txtDateFrom.Focus()
        '   ObjLib = Nothing
        cboItem.Items.Add("ALL")
        cboLedger.Items.Add("ALL")
    End Sub

    Protected Sub StockReport(DF As String, DT As String)

        Dim sqlStr As String = ""
      
        sqlStr = "Select * From vw_Stock where stockType = '" & Request.QueryString("type").ToString & "' and StockDate between '" & DF & "' and '" & DT & "'"
        If cboItem.Text <> "" Then
            If cboItem.SelectedItem.Text = "ALL" Then
            Else
                sqlStr += " and itemname='" & cboItem.Text & "'"
            End If

        End If
        If cboLedger.Text <> "" Then
            If cboLedger.Text = "ALL" Then
            Else
                If Request.QueryString("type").ToString = "OUT" Then
                    sqlStr += " and ledgername='" & cboLedger.Text & "'"
                Else
                    sqlStr += " and Vendorname='" & cboLedger.Text & "'"
                End If
            End If
           
        End If
        sqlStr += " order by StockDate"
        SqlDataSource1.SelectCommand = sqlStr
        GridView2.DataBind()
        If GridView2.Rows.Count > 0 Then
            btnExcel.Visible = True
        Else
            btnExcel.Visible = False
        End If
    End Sub

  

    Protected Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click

        Dim sw As New StringWriter()
        Dim hw As New System.Web.UI.HtmlTextWriter(sw)
        Dim frm As HtmlForm = New HtmlForm()

        Dim filename As String = "SearchResult_" + DateTime.Now.ToString() + ".xls"

        Page.Response.AddHeader("content-disposition", "attachment;filename=" + filename)
        Page.Response.ContentType = "application/vnd.ms-excel"
        Page.Response.Charset = ""
        Page.EnableViewState = False
        frm.Attributes("runat") = "server"
        Controls.Add(frm)
        frm.Controls.Add(GridView2)
        frm.RenderControl(hw)
        Response.Write(sw.ToString())
        Response.End()
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If txtDateFrom.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Enter Date From ..');", True)
            txtDateFrom.Focus()
            Return
        End If
        If txtDateTo.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Enter Date To ..');", True)
            txtDateTo.Focus()
            Return
        End If
        Dim StartDate As Date = New Date(txtDateFrom.Text.Substring(6, 4), txtDateFrom.Text.Substring(3, 2), txtDateFrom.Text.Substring(0, 2))
        Dim EndDate As Date = New Date(txtDateTo.Text.Substring(6, 4), txtDateTo.Text.Substring(3, 2), txtDateTo.Text.Substring(0, 2))
        If StartDate > EndDate Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('To Date can not less than From Date ..');", True)

            txtDateTo.Focus()
            Return
        End If
        StockReport(StartDate.ToString("yyyy/MM/dd"), EndDate.ToString("yyyy/MM/dd"))
    End Sub

    Protected Sub GridView2_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView2.RowCommand
        Dim index As Integer = Convert.ToInt32(e.CommandArgument.ToString())
        Dim StockID As String = GridView2.DataKeys(index).Values(0).ToString()
        ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('Inventory.aspx?StockID=" & StockID & "');", True)
    End Sub
End Class