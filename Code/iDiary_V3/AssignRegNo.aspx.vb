Imports iDiary_V3.iDiary.CLS_idiary
Imports System.Data.SqlClient
Imports iDiary_V3.iDiary_Student.CLS_iDiary_Student
Imports System.IO

Public Class AssignRegNo
    Inherits System.Web.UI.Page
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Admin") Or Request.Cookies("UType").Value.ToString.Contains("Student") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
            End If
        Catch ex As Exception
            Response.Redirect("~/Login.aspx")
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnprint.Visible = False
        btnexporttoexcel.Visible = False
        Session("ActiveTab") = 2
        If (Request.Cookies("UType").Value.ToString.Contains("Admin-1") = False And Request.Cookies("UType").Value.ToString.Contains("Student-1") = False) Then
            btnAsignNo.Enabled = False
        End If
        If IsPostBack = False Then
            LoadMasterInfo(71, cboSchoolName, Request.Cookies("SchoolIDs").Value)
            LoadMasterInfo(2, cboClass, cboSchoolName.Text)
            LoadMasterInfo(10, cboStatus)
            cboSection.Text = ""
            btnAsignNo.Visible = False
            '  GridView1.Visible = False
            'Else
            '    printFeeBookno()
            '    If ViewState("myTable") = True Then
            '        CreateTable()
            '    End If
        End If
    End Sub


    Protected Sub cboClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboClass.SelectedIndexChanged
        LoadClassSection(cboSchoolName.Text, cboClass.Text, cboSection)
        cboClass.Focus()
    End Sub

    Protected Sub btnAsignNo_Click(sender As Object, e As EventArgs) Handles btnAsignNo.Click
        'If cboClass.Text = "" Then
        '    lblStatus.Text = "Invalid Class..."
        '    cboClass.Focus()
        '    Exit Sub
        'End If

        'If cboSection.Text = "" Then
        '    lblStatus.Text = "Invalid Section..."
        '    cboSection.Focus()
        '    Exit Sub
        'End If

        'If cboStatus.Text = "" Then
        '    cboStatus.Text = "Invalid Status..."
        '    cboStatus.Focus()
        '    Exit Sub
        'End If
        Dim RegNo As String = ""
        Dim SID As Integer = "0"
        Dim COUNTReg As Integer = 0
        Dim rv As Integer = 0
        For i = 0 To GridView1.Rows.Count - 1
            'Dim chk As CheckBox = DirectCast(GridView1.Rows(i).FindControl("chkSelect"), CheckBox)
            'If chk.Checked = True And GridView1.Rows(i).Visible = True Then
            Try
                Dim txtRegNo As TextBox = DirectCast(GridView1.Rows(i).FindControl("txtRegNo"), TextBox)
                If txtRegNo.Text <> "" Then
                    'RegNo += txtRegNo.Text & "','"
                Else
                    GridView1.Rows(i).BackColor = Drawing.Color.OrangeRed
                    COUNTReg = 1
                End If
            Catch ex As Exception

            End Try
            'End If
        Next
        'If RegNo = "'" Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Enter Reg No for selected Student!!!');", True)
        '    Exit Sub
        'End If
        If COUNTReg = 1 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please enter Reg No!!!   Please Check Reg No of Colored rows...');", True)
            Exit Sub
        End If
       
        COUNTReg = 0
        For i = 0 To GridView1.Rows.Count - 1
            GridView1.Rows(i).BackColor = Drawing.Color.White
        Next

        Dim values As List(Of String) = New List(Of String)
        For i = 0 To GridView1.Rows.Count - 1
            Dim txtRegNo As TextBox = DirectCast(GridView1.Rows(i).FindControl("txtRegNo"), TextBox)
            values.Add(txtRegNo.Text)
        Next
        values.Sort()

        'Dim result As List(Of String) = values.Sort().ToList
        'result = values
        Dim PrvValues As String = ""
        For Each element As String In values
            If PrvValues = element Then
                PrvValues = element
                For i = 0 To GridView1.Rows.Count - 1
                    If GridView1.Rows(i).BackColor <> Drawing.Color.OrangeRed Then
                        Dim txtRegNo As TextBox = DirectCast(GridView1.Rows(i).FindControl("txtRegNo"), TextBox)
                        If txtRegNo.Text = PrvValues Then
                            GridView1.Rows(i).BackColor = Drawing.Color.OrangeRed
                            COUNTReg = 1
                        End If
                    End If
                Next
            Else
                PrvValues = element
                Continue For
            End If
        Next

        If COUNTReg = 1 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Duplicate Reg Nos!!!   Please Check Reg No of Colored rows...');", True)
            Exit Sub
        End If

        COUNTReg = 0
        For i = 0 To GridView1.Rows.Count - 1
            GridView1.Rows(i).BackColor = Drawing.Color.White
        Next

        Dim sqlstr As String = ""
        For i = 0 To GridView1.Rows.Count - 1
            SID = Convert.ToInt32(GridView1.DataKeys(i).Values(0))
            Dim txtRegNo As TextBox = DirectCast(GridView1.Rows(i).FindControl("txtRegNo"), TextBox)
            sqlstr = "select Count(*) from Student where ASID='" & Request.Cookies("ASID").Value & "' and RegNo ='" & txtRegNo.Text & "' and SID <>" & SID
            Try
                rv = ExecuteQuery_ExecuteScalar(sqlstr)
            Catch ex As Exception

            End Try
            If rv > 0 Then
                GridView1.Rows(i).BackColor = Drawing.Color.OrangeRed
                COUNTReg = 1
            End If
        Next

        If COUNTReg = 1 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Reg No allready exist!!!   Please Check Reg No of Colored rows...');", True)
            Exit Sub
        End If

        For i = 0 To GridView1.Rows.Count - 1
            GridView1.Rows(i).BackColor = Drawing.Color.White
        Next

        For i = 0 To GridView1.Rows.Count - 1
            Dim txtRegNo As TextBox = DirectCast(GridView1.Rows(i).FindControl("txtRegNo"), TextBox)
            SID = Convert.ToInt32(GridView1.DataKeys(i).Values(0))
            sqlstr = "update student set RegNo='" & txtRegNo.Text & "' where SID=" & SID
            ExecuteQuery_Update(sqlstr)
        Next
        'GridView1.DataBind()
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Students RegNo has been updated succesfully...');", True)

        'For i = 0 To GridView1.Rows.Count - 1
        '    GridView1.Rows(i).BackColor = Drawing.Color.White
        'Next
        'lblStatus.Text = "Fee Book No Assignment complete. " & alradyAssigned
    End Sub


   
    Protected Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        If cboSchoolName.Text = "" Then
            lblStatus.Text = "Invalid School Name..."
            cboSchoolName.Focus()
            Exit Sub
        End If
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

        If cboStatus.Text = "" Then
            cboStatus.Text = "Invalid Status..."
            cboStatus.Focus()
            Exit Sub
        End If
        btnprint.Visible = False
        btnexporttoexcel.Visible = False
        lblStatus.Text = ""

        SqlDataSource1.SelectCommand = "SELECT * FROM [vw_Student] Where SchoolName='" & cboSchoolName.Text & "' and ASID='" & Request.Cookies("ASID").Value & "' and statusName='" & cboStatus.Text & "' and ClassName='" & cboClass.Text & "' and SecName='" & cboSection.Text & "'"
        GridView1.DataBind()
        If GridView1.Rows.Count > 0 Then
            GridView1.Visible = True
            btnAsignNo.Visible = True
            btnprint.Visible = True
            btnexporttoexcel.Visible = True
        Else
            GridView1.Visible = False
            btnAsignNo.Visible = False
            btnprint.Visible = False
            btnexporttoexcel.Visible = False
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('No Record Found...');", True)
        End If


    End Sub


    'Protected Sub btnAsignNoSeries_Click(sender As Object, e As EventArgs) Handles btnAsignNoSeries.Click
    '    'If cboClass.Text = "" Then
    '    '    lblStatus.Text = "Invalid Class..."
    '    '    cboClass.Focus()
    '    '    Exit Sub
    '    'End If

    '    'If cboSection.Text = "" Then
    '    '    lblStatus.Text = "Invalid Section..."
    '    '    cboSection.Focus()
    '    '    Exit Sub
    '    'End If

    '    'If cboStatus.Text = "" Then
    '    '    cboStatus.Text = "Invalid Status..."
    '    '    cboStatus.Focus()
    '    '    Exit Sub
    '    'End If
    '    Dim RegNo As String = ""
    '    Dim SID As Integer = ""
    '    Dim COUNTReg As Integer = 0
    '    Dim rv As Integer = 0
    '    For i = 0 To GridView1.Rows.Count - 1
    '        'Dim chk As CheckBox = DirectCast(GridView1.Rows(i).FindControl("chkSelect"), CheckBox)
    '        'If chk.Checked = True And GridView1.Rows(i).Visible = True Then
    '        Try
    '            SID = GridView1.DataKeyNames(i).ToString & ","
    '            Dim txtRegNo As TextBox = DirectCast(GridView1.Rows(i).FindControl("txtRegNo"), TextBox)
    '            If txtRegNo.Text <> "" Then
    '                'RegNo += txtRegNo.Text & "','"
    '            Else
    '                GridView1.Rows(i).BackColor = Drawing.Color.OrangeRed
    '                COUNTReg = 1
    '            End If
    '        Catch ex As Exception

    '        End Try
    '        'End If
    '    Next
    '    'If RegNo = "'" Then
    '    '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Enter Reg No for selected Student!!!');", True)
    '    '    Exit Sub
    '    'End If
    '    If COUNTReg = 1 Then
    '        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please enter Reg No!!!   Please Check Reg No of Colored rows...');", True)
    '        Exit Sub
    '    End If
    '    'If RegNo <> "'" Then
    '    '    RegNo = RegNo.Substring(0, RegNo.Length - 2)
    '    'End If
    '    'If SIDList <> "" Then
    '    '    SIDList = SIDList.Substring(0, RegNo.Length - 1)
    '    'End If
    '    COUNTReg = 0
    '    For i = 0 To GridView1.Rows.Count - 1
    '        GridView1.Rows(i).BackColor = Drawing.Color.White
    '    Next

    '    Dim sqlstr As String = ""
    '    For i = 0 To GridView1.Rows.Count - 1
    '        Dim txtRegNo As TextBox = DirectCast(GridView1.Rows(i).FindControl("txtRegNo"), TextBox)
    '        sqlstr = "select Count(*) from Student where ASID='" & Request.Cookies("ASID").Value & "' and RegNo ='" & txtRegNo.Text & "' and SID <>" & SID
    '        Try
    '            rv = ExecuteQuery_ExecuteScalar(sqlstr)
    '        Catch ex As Exception

    '        End Try
    '        If rv > 0 Then
    '            GridView1.Rows(i).BackColor = Drawing.Color.OrangeRed
    '            COUNTReg = 1
    '        End If
    '    Next

    '    If COUNTReg = 1 Then
    '        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Reg No allready exist!!!   Please Check Reg No of Colored rows...');", True)
    '        Exit Sub
    '    End If

    '    For i = 0 To GridView1.Rows.Count - 1
    '        GridView1.Rows(i).BackColor = Drawing.Color.White
    '    Next

    '    For i = 0 To GridView1.Rows.Count - 1
    '        SID = GridView1.DataKeyNames(i).ToString & ","
    '        Dim txtRegNo As TextBox = DirectCast(GridView1.Rows(i).FindControl("txtRegNo"), TextBox)

    '        sqlstr = "update student set RegNo='" & txtRegNo.Text & "' where SID=" & SID
    '            ExecuteQuery_Update(sqlstr)
    '    Next
    '    'GridView1.DataBind()
    '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Students RegNo has been updated succesfully...');", True)

    '    'For i = 0 To GridView1.Rows.Count - 1
    '    '    GridView1.Rows(i).BackColor = Drawing.Color.White
    '    'Next
    '    'lblStatus.Text = "Fee Book No Assignment complete. " & alradyAssigned
    'End Sub

    Protected Sub btnexporttoexcel_Click(sender As Object, e As EventArgs) Handles btnexporttoexcel.Click
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
    Private Sub printFeeBookno()
        Dim printScript As String = "function PrintGridView() { var gridInsideDiv = document.getElementById('gvDiv');" & _
         " var printWindow = window.open('gview.htm','PrintWindow','letf=0,top=0,width=150,height=300,toolbar=1,scrollbars=1,status=1');" & _
         " printWindow.document.write(gridInsideDiv.innerHTML);printWindow.document.close();printWindow.focus();" & _
         " printWindow.print();printWindow.close();}"
        Me.ClientScript.RegisterStartupScript(Page.[GetType](), "PrintGridView", printScript.ToString(), True)
        btnprint.Attributes.Add("onclick", "PrintGridView();")
    End Sub
    Protected Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click

    End Sub

    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.RowType = DataControlRowType.DataRow Then
                Try
                    Dim txtRegNo As TextBox = e.Row.FindControl("txtRegNo")
                    Dim SID As Integer = Convert.ToInt32(GridView1.DataKeys(e.Row.RowIndex).Values(0))
                    Dim RegNo As String = Convert.ToString(GridView1.DataKeys(e.Row.RowIndex).Values(1))
                    txtRegNo.Text = RegNo
                Catch ex As Exception

                End Try
            End If
        End If
    End Sub

    Protected Sub cboSchoolName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSchoolName.SelectedIndexChanged
        LoadMasterInfo(2, cboClass, cboSchoolName.Text)
        cboSchoolName.Focus()
    End Sub
End Class