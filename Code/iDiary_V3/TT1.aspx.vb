Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports System.IO
Imports iDiary_V3.iDiary.iDiary_TT
Imports iDiary_V3.iDiary.CLS_iDiary_Exam

Public Class TT1
    Inherits System.Web.UI.Page


    Dim lstPeriod As New List(Of Integer)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("ActiveTab") = 5
        If IsPostBack = False Then
            InitControls()
        Else
            Dim printScript As String = "function PrintGridView() { var gridInsideDiv = document.getElementById('GridView1');" & _
                    " var printWindow = window.open('gview.htm','PrintWindow','letf=0,top=0,width=500,height=300,toolbar=1,scrollbars=1,status=1');" & _
                    " printWindow.document.write(gridInsideDiv.innerHTML);printWindow.document.close();printWindow.focus();" & _
                    " printWindow.print();printWindow.close();}"
            Me.ClientScript.RegisterStartupScript(Page.[GetType](), "PrintGridView", printScript.ToString(), True)
            btnPrint.Attributes.Add("onclick", "PrintGridView();")
        End If

        Try
            lblType.Text = Request.QueryString("type")
            If lblType.Text = "" Then lblType.Text = 0
        Catch ex As Exception
            lblType.Text = 0
        End Try
        If lblType.Text = "" Or lblType.Text = 0 Then
            PanelEmployee.Visible = False
            PanelClass.Visible = True
        ElseIf lblType.Text = 1 Then
            PanelEmployee.Visible = True
            PanelClass.Visible = False
        Else
            PanelEmployee.Visible = False
            PanelClass.Visible = False
        End If
    End Sub
    Private Sub InitControls()
        LoadMasterInfo(2, cboClass)
        cboSection.Items.Clear()

        'cboLeaveType.Items.Add("--------")
        'cboLeaveType.Items.Add("Present")
        'cboLeaveType.Items.Add("Absent")
        'cboLeaveType.Items.Add("Half Day")
        '  cboLeaveType.Items.Add("Late")

        'lblStatus.Text = ""
    End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand

        If e.CommandName.ToString() = "ColumnClick" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim dayID As String = GridView1.DataKeys(index).Value.ToString()

            Dim selectedRowIndex As Integer = Convert.ToInt32(e.CommandArgument.ToString())
            Dim selectedColumnIndex As Integer = Convert.ToInt32(Request.Form("__EVENTARGUMENT").ToString())
            Dim sb As New StringBuilder
            sb.Append("<script type='text/javascript'>")
            sb.Append("$('#currentdetail').modal('show');")
            sb.Append("</script>")
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(),
            "ModalScript", sb.ToString(), False)

            Dim SelectedVal As String = GridView1.Rows(selectedRowIndex).Cells(selectedColumnIndex).Text

            lblDayID.Text = GridView1.DataKeys(selectedRowIndex).Value.ToString()
            lblPeriodID.Text = selectedColumnIndex - 2
            lblDay.Text = GridView1.Rows(selectedRowIndex).Cells(2).Text
            lblSubject.Text = SelectedVal.Substring(0, SelectedVal.IndexOf("<br"))
            lblTeacher.Text = SelectedVal.Substring(SelectedVal.IndexOf("<br />") + 6, SelectedVal.Length - SelectedVal.IndexOf("<br />") - 6)
            lblPeriod.Text = GridView1.Columns(selectedColumnIndex).HeaderText
            lblRow.Text = selectedRowIndex.ToString()
            lblColumn.Text = selectedColumnIndex.ToString()

        End If
    End Sub

    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim _singleClickButton As LinkButton = DirectCast(e.Row.Cells(0).Controls(0), LinkButton)
                Dim _jsSingle As String = ClientScript.GetPostBackClientHyperlink(_singleClickButton, "")
                ' Add events to each editable cell
                Dim rowIndex As Integer = 0
                For columnIndex As Integer = 3 To e.Row.Cells.Count - 1
                    rowIndex = e.Row.RowIndex
                    Dim EmpID As String = GridView1.DataKeys(e.Row.RowIndex).Values(0).ToString()
                    Try
                        If lblType.Text = 0 Then
                            e.Row.Cells(columnIndex).Text = getTTSubject(lblCssID.Text, rowIndex + 1, columnIndex - 2) & "<br />" & getTTSubjectTeacher(lblCssID.Text, rowIndex + 1, columnIndex - 2)
                        Else
                            e.Row.Cells(columnIndex).Text = getTTTeacherSubject(lblEmpID.Text, rowIndex + 1, columnIndex - 2) & "<br />" & getTTTeacherClass(lblEmpID.Text, rowIndex + 1, columnIndex - 2)
                        End If

                        If Trim(e.Row.Cells(columnIndex).Text) = "<br />" Then
                            e.Row.Cells(columnIndex).BackColor = Drawing.ColorTranslator.FromHtml("#DAD7D7")
                        Else
                            e.Row.Cells(columnIndex).BackColor = Drawing.ColorTranslator.FromHtml("#F5CACA")
                        End If

                    Catch ex As Exception

                    End Try
                    ' Add the column index as the event argument parameter
                    Dim js As String = _jsSingle.Insert(_jsSingle.Length - 2, columnIndex.ToString())
                    ' Add this javascript to the onclick Attribute of the cell
                    e.Row.Cells(columnIndex).Attributes("onclick") = js
                    ' Add a cursor style to the cells
                    e.Row.Cells(columnIndex).Attributes("style") += "cursor:pointer;cursor:hand;"


                Next
            End If
        End If


    End Sub

    Protected Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click
        generateTT() 'tpye=0
    End Sub

    Private Sub generateTT()
        lblCssID.Text = FindCSSID("", cboClass.Text, cboSection.Text, "")
        Dim clmNo As Integer = GridView1.Columns.Count
        For j = 3 To clmNo - 1
            Try
                GridView1.Columns.RemoveAt(3)
            Catch ex As Exception

            End Try

        Next
        GridView1.DataSource = Nothing
        GridView1.DataBind()

        Dim bfield As New BoundField()

        Dim sqlStr As String = ""
        sqlStr = "SELECT [ttDayID],[ttDayNAme] FROM TTDayMaster"
        'sqlStr = "SELECT [EmpCode], [EmpName], [AttDate], [Att], [LeaveName] FROM [vw_employee_Attendance]"
        ' Where EmpCatName='" & cboEmpCat.Text & "' and StatusName ='" & cboStatus.Text & "'   AND AttDate between '" & dateFrom & "' AND '" & dateTo & "'"
        Dim ds As DataSet = ExecuteQuery_DataSet(sqlStr, "att")
        GridView1.DataSource = ds.Tables(0)
        GridView1.DataBind()
        For i = 1 To 8
            Dim tfield As New TemplateField()
            tfield.HeaderText = "Period " & i
            GridView1.Columns.Add(tfield)
            'GridView1.Rows.Item(i)..Cells(2).Text =
            ''ddlDate.Items.Add(dateList.Item(i).ToString("yyyy/MM/dd"), i)
        Next
        'ddlDate.DataSource = dateList
        'ddlDate.DataBind()
        GridView1.DataBind()
    End Sub
    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

    End Sub

    Protected Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click

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

        frm.Controls.Add(GridView1)
        frm.RenderControl(hw)
        Response.Write(sw.ToString())
        Response.End()
    End Sub

    Private Function EntryAlreadyExists(ByVal str As String) As Boolean
        Dim rv As Boolean = False
       
       
       
        
        Dim cnt As Integer = 0

        Try
            cnt = ExecuteQuery_ExecuteScalar(str)
        Catch ex As Exception
            cnt = 0
        End Try

        
        
        If cnt = 0 Then
            rv = False
        Else
            rv = True
        End If
        Return rv
    End Function

    Protected Sub cboClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboClass.SelectedIndexChanged
        LoadClassSection("", cboClass.Text, cboSection)
    End Sub

    Protected Sub btnRelease_Click(sender As Object, e As EventArgs) Handles btnRelease.Click
        Dim sqlStr As String = ""
        sqlStr = "Delete from TTgenerate Where CSSID=" & lblCssID.Text & " And dayID=" & lblDayID.Text & " AND periodID=" & lblPeriodID.Text
        ExecuteQuery_Update(sqlStr)
        generateTT()
    End Sub

    Protected Sub btnFill_Click(sender As Object, e As EventArgs) Handles btnFill.Click
        Dim SubID As Integer = FindSubjectID(cboFreeSubject.SelectedItem.Text)
        Dim EmpID As Integer = getEmpIDfromSubject(lblCssID.Text, SubID)
        Dim sqlStr As String = ""
        If EmpID > 0 And (checkPreviousPeriodConstrain(lblCssID.Text, SubID, lblPeriodID.Text, lblDayID.Text, getSubjectContinuousCount(lblCssID.Text, SubID)) = 1) Then

            If (checkEmpAvailability(lblPeriodID.Text, lblDayID.Text, EmpID) = 1) Then
                sqlStr = "Insert into TTGenerate(CSSID,dayID,periodID,SubjectID) Values(" & lblCssID.Text & "," & lblDayID.Text & "," & lblPeriodID.Text & "," & SubID & ")"
                ExecuteQuery_Update(sqlStr)
                lblStatus.Text = "Subject/Teacher Mapped."
                generateTT()
            Else
                lblStatus.Text = "Subject/Teacher already occupied."
            End If

        Else
            lblStatus.Text = "Subject/Teacher already occupied."
        End If

    End Sub

    Protected Sub btnGet_Click(sender As Object, e As EventArgs) Handles btnGet.Click
        lblStatus.Text = ""
        LoadSubjectClassWise(cboFreeSubject, cboClass.Text, cboSection.Text, Request.Cookies("ASID").Value)
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles btnEmp.Click
        Dim sqlStr As String = ""
        sqlStr = "Select EmpID,EmpName from EmployeeMaster Where EmpCode='" & txtEmpCode.Text & "'"
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            lblEmpID.Text = myReader("EmpID")
            txtEmpName.Text = myReader("EmpName")
        End While
        myReader.Close()
        If txtEmpName.Text = "" Then
            lblStatus.Text = "enter valid employee code."
            Exit Sub
        End If
        generateTT() 'type=1
        lblEmpLoad.Text = "Weekly Work Load=" & getTTTeacherWeekLoad(lblEmpID.Text)
    End Sub
End Class
