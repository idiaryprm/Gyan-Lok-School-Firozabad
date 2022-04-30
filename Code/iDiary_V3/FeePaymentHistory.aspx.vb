Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class FeePaymentHistory
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
                response.redirect("~/Login.aspx")
            End If

        End Try

    End Sub

    Private Sub ClearReportData()
        Dim sqlStr As String = ""
       
       
       

        

        sqlStr = "Delete from rptFeeHistory"
        
        
        ExecuteQuery_Update(SqlStr)

        
        

    End Sub

    Protected Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        If txtFeeBookNo.Text = "" Then
            lblStatus.Text = "Invalid Fee Book No"
            txtFeeBookNo.Focus()
        Else
            PrepareHistory(1)
        End If
    End Sub

    Private Sub PrepareHistory(type As Integer)

        Dim lstFeeDepositID As New ListBox
        Dim InsertSQl As New ListBox
        Dim OldDepositID As Integer = 0

        lstFeeDepositID.Items.Clear()
        InsertSQl.Items.Clear()

        Dim sqlStr As String = "", TempSQL As String = ""
       
       
       

        

        sqlStr = "Delete from rptFeeHistory"
        
        
        ExecuteQuery_Update(SqlStr)

        'Read Concession Fee Type
        sqlStr = "Select ConcessionFeeType, FeetypeName From vwFeeConcessionConfig"
        
        

        Dim ConcessionConfigReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        Dim ConcessionFeeTypeID As Integer = 0
        Dim ConcessionFeeTypeName As String = ""

        While ConcessionConfigReader.Read
            ConcessionFeeTypeID = ConcessionConfigReader(0)
            ConcessionFeeTypeName = ConcessionConfigReader(1)
        End While
        ConcessionConfigReader.Close()
        If type = 1 Then
            sqlStr = "Select SID, RegNo, FeeBookNo, SName, FName, ClassName, SecName, FeeDepositID, DepositDate, PMName, DepositDetails, TermNo From vw_FeeDeposit Where FeeBookNo='" & txtFeeBookNo.Text & "' AND FeeTypeName<>'" & ConcessionFeeTypeName & "' AND ASID=" & Request.Cookies("ASID").Value
        Else
            sqlStr = "Select SID, RegNo, FeeBookNo, SName, FName, ClassName, SecName, FeeDepositID, DepositDate, PMName, DepositDetails, TermNo From vw_FeeDeposit Where RegNo='" & txtRegNo.Text & "' AND FeeTypeName<>'" & ConcessionFeeTypeName & "' AND ASID=" & Request.Cookies("ASID").Value
        End If
        
        

        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            lblRegNo.Text = myReader("RegNo")
            lblFeeBookNo.Text = myReader("FeeBookNo")
            lblClass.Text = myReader("ClassName") & "-" & myReader("SecName")
            lblSName.Text = myReader("SName")
            lblFatherName.Text = myReader("FName")

            If myReader("FeeDepositID") <> OldDepositID Then
                lstFeeDepositID.Items.Add(myReader("FeeDepositID"))

                TempSQL = "Insert into rptFeeHistory (DepositDate, TermNo, DepositAmount, DepositMode, DepositDetails,FeeDepositID) Values (" & _
                "'" & CDate(myReader("DepositDate")).Month & "/" & CDate(myReader("DepositDate")).Day & "/" & CDate(myReader("DepositDate")).Year & "','" & _
                myReader("TermNo") & "'," & _
                -1 & "," & _
                "'" & myReader("PMName") & "'," & _
                "'" & myReader("DepositDetails") & "'," & _
                "'" & myReader("FeeDepositID") & "')"
                InsertSQl.Items.Add(TempSQL)
            End If

            OldDepositID = myReader("FeeDepositID")
        End While
        myReader.Close()

        Dim i As Integer = 0
        For i = 0 To lstFeeDepositID.Items.Count - 1

            sqlStr = "Select Sum(FeeDepositAmount) From FeeDepositDetails Where FeeDepositID=" & lstFeeDepositID.Items(i).Text & " and FeeTypeID<>" & ConcessionFeeTypeID
            
            
            Dim DepositAmount As Double = 0
            Try
                DepositAmount = ExecuteQuery_ExecuteScalar(SqlStr)
            Catch ex As Exception
                DepositAmount = 0
            End Try

            InsertSQl.Items(i).Text = InsertSQl.Items(i).Text.Replace("-1", DepositAmount)
            myCommand.CommandText = InsertSQl.Items(i).Text
            
            ExecuteQuery_Update(SqlStr)

        Next

        System.Threading.Thread.Sleep(500)

        
        

        lblSchoolName.Text = FindSchoolDetails(1)
        lblTitle.Text = "Payment History"
        btnPrint.Visible = True
        btnExcel.Visible = True

        GridView1.DataBind()

        If GridView1.Rows.Count <= 0 Then
            If type = 1 Then
                lblStatus.Text = "No record Found for FeeBook: " & txtFeeBookNo.Text
            Else
                lblStatus.Text = "No record Found for RegNo: " & txtRegNo.Text
            End If
            lblStatus.Visible = True
        Else
            lblStatus.Visible = False
        End If
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
        txtFeeBookNo.Text = Request.QueryString("FeeBookNo")
        lblStatus.Visible = False
        btnPrint.Visible = False
        btnExcel.Visible = False
        ClearReportData()
        GridView1.DataBind()

        PrepareHistory(1)

        txtRegNo.Focus()
    End Sub

    Protected Sub btnExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Dim sw As New System.IO.StringWriter()
        Dim hw As New System.Web.UI.HtmlTextWriter(sw)
        Dim frm As HtmlForm = New HtmlForm()

        Dim filename As String = "Payment_History" + DateTime.Now.ToString() + ".xls"

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
    Protected Sub btnAdminNoNext_Click(sender As Object, e As EventArgs) Handles btnAdminNoNext.Click
        If txtRegNo.Text = "" Then
            lblStatus.Text = "Invalid RegNo"
            txtRegNo.Focus()
        Else
            PrepareHistory(2)
        End If
    End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        'If e.CommandName.ToString() = "Detail" Then
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim FeeDepositID As Integer = GridView1.DataKeys(index).Value.ToString()
        SqlDataSource2.SelectCommand = "SELECT [FeeTypeName], [FeeDepositAmount] FROM vw_FeeDeposit where FeeDepositAmount>0 and FeeDepositID=" & FeeDepositID
        lblTermNo.Text = GridView1.Rows(index).Cells(1).Text
        GridView2.DataBind()
        'End If
    End Sub
End Class