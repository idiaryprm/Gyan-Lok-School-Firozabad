Imports System.IO
Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Partial Class EmployeeSearch
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Request.Cookies("UType").Value.ToString.Contains("Admin") Or Request.Cookies("UType").Value.ToString.Contains("Payroll") Then
            'Allow
        Else
            Response.Redirect("AccessDenied.aspx")
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("ActiveTab") = 4
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
        LoadMasterInfo(25, cboDepartment)
        LoadMasterInfo(26, cboDesignation)
        LoadMasterInfo(27, cboQualification)
        LoadMasterInfo(5, cboReligion)
        LoadMasterInfo(6, cboCaste)
        LoadMasterInfo(64, cboEmpType)
        '  LoadMasterInfo(28, cboNationality)
        LoadMasterInfo(20, cboPayScale)
        LoadMasterInfo(29, cboStatus)
        LoadMasterInfo(21, cboGradePay)
        '   LoadMasterInfo(22, cboStatus)
        LoadMasterInfo(30, cboCategory)

        txtByName.Text = ""
        chkByName.Focus()
    End Sub
    Protected Sub btnExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcel.Click

        Dim sw As New StringWriter()
        Dim hw As New System.Web.UI.HtmlTextWriter(sw)
        Dim frm As HtmlForm = New HtmlForm()

        Dim filename As String = "GridViewExport_" + DateTime.Now.ToString() + ".xls"

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

   
  
    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click

        lblStatus.Text = ""

        If chkbyDept.Checked = False And chkByDesg.Checked = False And chkByName.Checked = False And chkCaste.Checked = False And chkCategory.Checked = False And chkDOJ.Checked = False And chkGender.Checked = False And chkGradePay.Checked = False And chkPayScale.Checked = False And chkQualification.Checked = False And chkReligion.Checked = False And chkStatus.Checked = False Then
            lblStatus.Text = "Please Select atleast one criteria to continue..."
            GridView1.Visible = False
            chkByName.Focus()
            Exit Sub
        End If

        If chkbyDept.Checked = True And Trim(cboDepartment.Text) = "" Then
            lblStatus.Text = "Please Select Department..."
            GridView1.Visible = False
            cboDepartment.Focus()
            Exit Sub
        End If

        If chkByDesg.Checked = True And Trim(cboDesignation.Text) = "" Then
            lblStatus.Text = "Please Select Designation..."
            GridView1.Visible = False
            cboDesignation.Focus()
            Exit Sub
        End If

        If chkByName.Checked = True And Trim(txtByName.Text).Length <= 0 Then
            lblStatus.Text = "Provide atleast one character as Employee Name to continue..."
            GridView1.Visible = False
            txtByName.Focus()
            Exit Sub
        End If

        If chkCaste.Checked = True And Trim(cboCaste.Text) = "" Then
            lblStatus.Text = "Please Select Caste..."
            GridView1.Visible = False
            cboCaste.Focus()
            Exit Sub
        End If

        If chkCategory.Checked = True And Trim(cboCategory.Text) = "" Then
            lblStatus.Text = "Please Select Category..."
            GridView1.Visible = False
            cboCategory.Focus()
            Exit Sub
        End If
        Dim DOJFrom As Date = Now.Date
        Dim DOJTo As Date = Now.Date
        If chkDOJ.Checked = True Then
            If Trim(txtDOJFrom.Text) = "" Then
                lblStatus.Text = "Please Enter Date of joinning From..."
                GridView1.Visible = False
                txtDOJFrom.Focus()
                Exit Sub
            End If
            Try
                If txtDOJFrom.Text.Contains("/") Then
                    DOJFrom = CDate(txtDOJFrom.Text.Split("/")(2) & "/" & txtDOJFrom.Text.Split("/")(1) & "/" & txtDOJFrom.Text.Split("/")(0))
                ElseIf txtDOJFrom.Text.Contains(".") Then
                    DOJFrom = CDate(txtDOJFrom.Text.Split(".")(2) & "/" & txtDOJFrom.Text.Split(".")(1) & "/" & txtDOJFrom.Text.Split(".")(0))
                Else
                    DOJFrom = CDate(txtDOJFrom.Text.Split("-")(2) & "/" & txtDOJFrom.Text.Split("-")(1) & "/" & txtDOJFrom.Text.Split("-")(0))
                End If

            Catch ex As Exception
                lblStatus.Text = "Invalid Date Of Joinning From..."
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Date Of Joinning From...');", True)
                GridView1.Visible = False
                txtDOJFrom.Focus()
                Exit Sub
            End Try

            If Trim(txtDOJTO.Text) = "" Then
                lblStatus.Text = "Please Enter Date of joinning To..."
                GridView1.Visible = False
                txtDOJTO.Focus()
                Exit Sub
            End If
            Try
                If txtDOJTO.Text.Contains("/") Then
                    DOJTo = CDate(txtDOJTO.Text.Split("/")(2) & "/" & txtDOJTO.Text.Split("/")(1) & "/" & txtDOJTO.Text.Split("/")(0))
                ElseIf txtDOJTO.Text.Contains(".") Then
                    DOJTo = CDate(txtDOJTO.Text.Split(".")(2) & "/" & txtDOJTO.Text.Split(".")(1) & "/" & txtDOJTO.Text.Split(".")(0))
                Else
                    DOJTo = CDate(txtDOJTO.Text.Split("-")(2) & "/" & txtDOJTO.Text.Split("-")(1) & "/" & txtDOJTO.Text.Split("-")(0))
                End If
            Catch ex As Exception
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Date Of Joinning To...');", True)
                lblStatus.Text = "Invalid Date Of Joinning To..."
                txtDOJTO.Focus()
                Exit Sub
            End Try

            If DOJFrom > DOJTo Then
                lblStatus.Text = "Date of Joinning From Date Can't br greater then Date of Joinning To Date..."
                GridView1.Visible = False
                txtDOJTO.Focus()
                Exit Sub
            End If

        End If

        If chkGender.Checked = True And Trim(cboGender.Text) = "" Then
            lblStatus.Text = "Provide Select Gender to continue..."
            GridView1.Visible = False
            cboGender.Focus()
            Exit Sub
        End If
        If chkGradePay.Checked = True And Trim(cboGradePay.Text) = "" Then
            lblStatus.Text = "Provide Select Grade Pay to continue..."
            GridView1.Visible = False
            cboGradePay.Focus()
            Exit Sub
        End If
        If chkPayScale.Checked = True And Trim(cboPayScale.Text) = "" Then
            lblStatus.Text = "Provide Select Pay Scale to continue..."
            GridView1.Visible = False
            cboPayScale.Focus()
            Exit Sub
        End If
        If chkReligion.Checked = True And cboReligion.Text = "" Then
            lblStatus.Text = "Provide atleast one Religion to continue..."
            GridView1.Visible = False
            cboReligion.Focus()
            Exit Sub
        End If
        If chkQualification.Checked = True And Trim(cboQualification.Text = "") Then
            lblStatus.Text = "Provide atleast one Qualification to continue..."
            GridView1.Visible = False
            cboQualification.Focus()
            Exit Sub
        End If
        If chkStatus.Checked = True And cboStatus.Text = "" Then
            lblStatus.Text = "Provide atleast one Status to continue..."
            GridView1.Visible = False
            cboStatus.Focus()
            Exit Sub
        End If
        If chkType.Checked = True And cboEmpType.Text = "" Then
            lblStatus.Text = "Provide atleast one Type to continue..."
            GridView1.Visible = False
            cboEmpType.Focus()
            Exit Sub
        End If
        'Prepare SQL Query
        Dim sqlStr As String = ""
        'sqlStr = "Insert into RptSearch "
        lblStatus.Text = ""
        sqlStr = "Select EmpCode,EmpName,DOB,DOJ, DeptName, DesgName,QualName, CASE WHEN Gender = 0 THEN 'Male' ELSE 'Female' END AS Gender, RelName, CasteName,AccNo, PAN, PerAdd, Mob,Email,SchoolName From vw_Employees Where EmpID>" & 0

        If chkByName.Checked = True Then sqlStr &= " AND EmpName Like '%" & txtByName.Text & "%' "
        If chkbyDept.Checked = True Then sqlStr &= " AND DeptName='" & cboDepartment.Text & "' "
        If chkByDesg.Checked = True Then sqlStr &= " AND DesgName='" & cboDesignation.Text & "' "
        If chkQualification.Checked = True Then sqlStr &= " AND QualName ='" & cboQualification.Text & "' "
        If chkGender.Checked = True Then sqlStr &= " AND Gender=" & cboGender.SelectedIndex
        If chkReligion.Checked = True Then sqlStr &= " AND RelName='" & cboReligion.Text & "' "
        If chkCaste.Checked = True Then sqlStr &= " AND CasteName='" & cboCaste.Text & "' "
        If chkStatus.Checked = True Then sqlStr &= " AND StatusName='" & cboStatus.Text & "' "
        If chkDOJ.Checked = True Then sqlStr &= " AND DOJ Between '" & DOJFrom.ToString("yyyy/MM/dd") & "' and '" & DOJTo.ToString("yyyy/MM/dd") & "'"
        If chkCategory.Checked = True Then sqlStr &= " AND EmpCatName='" & cboCategory.Text & "' "
        If chkGradePay.Checked = True Then sqlStr &= " AND AGPName='" & cboGradePay.Text & "' "
        If chkPayScale.Checked = True Then sqlStr &= " AND PayScaleName='" & cboPayScale.Text & "' "
        If chkType.Checked = True Then sqlStr &= " AND EmpTypeName='" & cboEmpType.Text & "' "
        If chkDOJ.Checked = True Then
            sqlStr &= " Order By DOJ, EmpName,Gender"
        Else
            sqlStr &= " Order By EmpName,Gender"
        End If


        'Dim myHeaderText As String = "Search Parameter: "
        'Dim ANDFlag As Boolean = False

        'If chkByName.Checked = True Then
        '    If ANDFlag = True Then myHeaderText &= ", "
        '    myHeaderText &= "Student Name=" & txtByName.Text
        '    ANDFlag = True
        'End If
        'If chkbyDept.Checked = True Then
        '    If ANDFlag = True Then myHeaderText &= ", "
        '    myHeaderText &= "Fee Book No=" & txtByCode.Text
        '    ANDFlag = True
        'End If
        'If chkByDesg.Checked = True Then
        '    If ANDFlag = True Then myHeaderText &= ", "
        '    myHeaderText &= "Reg No.=" & txtByReg.Text
        '    ANDFlag = True
        'End If
        'If chkQualification.Checked = True Then
        '    If ANDFlag = True Then myHeaderText &= ", "
        '    myHeaderText &= "Father Name=" & txtFName.Text
        '    ANDFlag = True
        'End If
        'If chkGender.Checked = True Then
        '    If ANDFlag = True Then myHeaderText &= ", "
        '    myHeaderText &= "Class=" & cboClass.Text
        '    ANDFlag = True
        'End If
        'If chkSection.Checked = True Then
        '    If ANDFlag = True Then myHeaderText &= ", "
        '    myHeaderText &= "Section=" & cboSection.Text
        '    ANDFlag = True
        'End If
        'If chkHouse.Checked = True Then
        '    If ANDFlag = True Then myHeaderText &= ", "
        '    myHeaderText &= "House=" & cboHouse.Text
        '    ANDFlag = True
        'End If
        'If chkReligion.Checked = True Then
        '    If ANDFlag = True Then myHeaderText &= ", "
        '    myHeaderText &= "Religion=" & cboReligion.Text
        '    ANDFlag = True
        'End If
        'If chkCaste.Checked = True Then
        '    If ANDFlag = True Then myHeaderText &= ", "
        '    myHeaderText &= "Caste=" & cboCaste.Text
        '    ANDFlag = True
        'End If
        'If chkStatus.Checked = True Then
        '    If ANDFlag = True Then myHeaderText &= ", "
        '    myHeaderText &= "Status=" & cboStatus.Text
        '    ANDFlag = True
        'End If
        'If chkDOJ.Checked = True Then
        '    If ANDFlag = True Then myHeaderText &= ", "
        '    myHeaderText &= "Admission Date=" & txtAdminDate_DD.Text & "/" & txtAdminDate_MM.Text & "/" & txtAdminDate_YY.Text
        '    ANDFlag = True
        'End If
        'If chkFDept.Checked = True Then
        '    If ANDFlag = True Then myHeaderText &= ", "
        '    myHeaderText &= "Father's Department=" & cboFDept.Text
        '    ANDFlag = True
        'End If
        'If chkGender.Checked = True Then
        '    If ANDFlag = True Then myHeaderText &= ", "
        '    myHeaderText &= "Gender=" & cboGender.Text
        '    ANDFlag = True
        'End If

        lblSchoolName.Text = FindSchoolDetails(1)
        SqlDataSource1.SelectCommand = sqlStr
        'GridView1.DataSource = ExecuteQuery_DataSet(sqlStr, "tbl").Tables(0)
        'If Request.QueryString("type") = 0 Then
        GridView1.DataBind()

        For i = 0 To GridView1.Rows.Count - 1
            GridView1.Rows(i).Cells(0).Text = i + 1
        Next

        GridView1.Visible = True
        'GridView2.Visible = False
        'lblTitle.Text = myHeaderText
        lblTotalRecords.Text = "Total Records: " & GridView1.Rows.Count
        'ElseIf Request.QueryString("type") = 1 Then
        '    GridView2.DataBind()

        '    For i = 0 To GridView2.Rows.Count - 1
        '        GridView2.Rows(i).Cells(0).Text = i + 1
        '    Next

        '    GridView2.Visible = True
        '    GridView1.Visible = False
        '    lblTitle.Text = "Class List of " & cboClass.Text & "-" & cboSection.Text
        '    lblTotalRecords.Text = "Total " & GridView2.Rows.Count & " records found..."
        'End If

        'Add Sr. No.

        If GridView1.Rows.Count > 0 Then
            btnPrint.Visible = True
        Else
            btnPrint.Visible = False
        End If
    End Sub
End Class
