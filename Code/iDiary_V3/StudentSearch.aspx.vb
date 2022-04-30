Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports System.IO

Public Class StudentSearch
    Inherits System.Web.UI.Page
    Private Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Student") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
            End If
        Catch ex As Exception
            Response.Redirect("~/Login.aspx")
        End Try
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cookies("ActiveTab").Value = 2
        Response.Cookies("ActiveTab").Expires = DateTime.Now.AddHours(1)
        Session("ActiveTab") = 2
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

        chkByName.Checked = False
        txtByName.Text = ""

        chkbyCode.Checked = False
        txtByCode.Text = ""

        chkByReg.Checked = False
        txtByReg.Text = ""

        chkFName.Checked = False
        txtFName.Text = ""
        LoadMasterInfo(71, cboSchoolName, Request.Cookies("SchoolIDs").Value)
        If cboSchoolName.Items.Count = 3 Then
            cboSchoolName.Items.Add("ALL")
        End If
        chkClass.Checked = False
        LoadMasterInfo(2, cboClass, cboSchoolName.Text)
        cboClass.Items.Add("ALL")
        chkSection.Checked = False
        'LoadClassSection(cboClass.Text, cboSection)

        chkHouse.Checked = False
        LoadMasterInfo(4, cboHouse)
        cboHouse.Items.Add("ALL")

        chkReligion.Checked = False
        LoadMasterInfo(5, cboReligion)

        chkCaste.Checked = False
        LoadMasterInfo(6, cboCaste)

        chkCategory.Checked = False
        LoadMasterInfo(34, cboCategory)

        chkGender.Checked = False

        chkStatus.Checked = False
        LoadMasterInfo(10, cboStatus)

        chkFatherOccupation.Checked = False
        LoadMasterInfo(7, cboFatherOccupation)

        chkAdminDate.Checked = False
        'txtAdminDate_DD.Text = Now.Day
        'txtAdminDate_MM.Text = Now.Month
        'txtAdminDate_YY.Text = Now.Year
        'txtAdminDate_DDTo.Text = Now.Day
        'txtAdminDate_MMTo.Text = Now.Month
        'txtAdminDate_YYTo.Text = Now.Year

        txtAdminDateFrom.Text = Now.Date.ToString("dd/MM/yyyy")
        txtAdminDateTo.Text = Now.Date.ToString("dd/MM/yyyy")

        chkFDept.Checked = False
        LoadMasterInfo(8, cboFDept)
        'chkReferenceCategory.Checked = False
        'LoadMasterInfo(106, cboReferenceCategory)
        'chkReference.Checked = False

        lblTotalRecords.Text = ""

        btnPrint.Visible = False
        btnExcel.Visible = False

        If Request.QueryString("type") = 1 Then 'Class List
            panelSearcCrit.Visible = False
            chkByName.Enabled = False
            txtByName.Enabled = False

            chkbyCode.Enabled = False
            txtByCode.Enabled = False

            chkByReg.Enabled = False
            txtByReg.Enabled = False

            chkReligion.Enabled = False
            cboReligion.Enabled = False

            chkCaste.Enabled = False
            cboCaste.Enabled = False

            chkGender.Enabled = False
            cboGender.Enabled = False

            chkFName.Enabled = False
            txtFName.Enabled = False

            chkHouse.Enabled = False
            cboHouse.Enabled = False

            cboFatherOccupation.Enabled = False
            'chkReferenceCategory.Checked = False
            'cboReferenceCategory.Enabled = False
            'chkReference.Checked = False

            chkAdminDate.Enabled = False
            'txtAdminDate_DD.Enabled = False
            'txtAdminDate_MM.Enabled = False
            'txtAdminDate_YY.Enabled = False
            'txtAdminDate_DDTo.Enabled = False
            'txtAdminDate_MMTo.Enabled = False
            'txtAdminDate_YYTo.Enabled = False
            txtAdminDateFrom.Enabled = False
            txtAdminDateTo.Enabled = False

            chkFDept.Enabled = False
            cboFDept.Enabled = False

        ElseIf Request.QueryString("type") = 0 Then 'General Search
            panelSearcCrit.Visible = True
            chkByName.Enabled = True
            txtByName.Enabled = True

            chkbyCode.Enabled = True
            txtByCode.Enabled = True

            chkByReg.Enabled = True
            txtByReg.Enabled = True

            chkReligion.Enabled = True
            cboReligion.Enabled = True

            chkCaste.Enabled = True
            cboCaste.Enabled = True

            chkGender.Enabled = True
            cboGender.Enabled = True

            chkFName.Enabled = True
            txtFName.Enabled = True

            chkHouse.Enabled = True
            cboHouse.Enabled = True
            'chkReference.Enabled = True
            'chkReferenceCategory.Enabled = True
            'cboReferenceCategory.Enabled = True

            chkAdminDate.Enabled = True
            'txtAdminDate_DD.Enabled = True
            'txtAdminDate_MM.Enabled = True
            'txtAdminDate_YY.Enabled = True
            'txtAdminDate_DDTo.Enabled = True
            'txtAdminDate_MMTo.Enabled = True
            'txtAdminDate_YYTo.Enabled = True

            txtAdminDateFrom.Enabled = True
            txtAdminDateTo.Enabled = True

            chkFDept.Enabled = True
            cboFDept.Enabled = True

        End If

        GridView1.Visible = False
        GridView2.Visible = False

        chkByName.Focus()
    End Sub

    Protected Sub cboClass_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboClass.SelectedIndexChanged
        If cboClass.Text = "ALL" Then
            cboSection.Items.Add("ALL")
        Else
            LoadClassSection(cboSchoolName.Text, cboClass.Text, cboSection)
            cboSection.Items.Add("ALL")
        End If
       
    End Sub


    Protected Sub btnExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcel.Click

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
        If Request.QueryString("type") = 0 Then
            frm.Controls.Add(GridView1)
        ElseIf Request.QueryString("type") = 1 Then
            frm.Controls.Add(GridView2)
        End If
        frm.RenderControl(hw)
        Response.Write(sw.ToString())
        Response.End()

    End Sub
    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        For Each tableCell As TableCell In e.Row.Cells
            Dim cell As DataControlFieldCell = CType(tableCell, DataControlFieldCell)
            If cell.ContainingField.HeaderText = "Admn No." And chkAdmnNoC.Checked = False Then
                cell.Visible = False
                Continue For
            End If

            If cell.ContainingField.HeaderText = "Name" And chkSNameC.Checked = False Then
                cell.Visible = False
                Continue For
            End If

            If cell.ContainingField.HeaderText = "Father Name" And chkFNameC.Checked = False Then
                cell.Visible = False
                Continue For
            End If

            If cell.ContainingField.HeaderText = "Mother Name" And chkMNameC.Checked = False Then
                cell.Visible = False
                Continue For
            End If
            If cell.ContainingField.HeaderText = "School Name" And chkSchoolNameC.Checked = False Then
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

            If cell.ContainingField.HeaderText = "Fee Book No." And chkFBnoC.Checked = False Then
                cell.Visible = False
                Continue For
            End If

            If cell.ContainingField.HeaderText = "Date of Birth" And chkDobC.Checked = False Then
                cell.Visible = False
                Continue For
            End If

            If cell.ContainingField.HeaderText = "Address" And chkAddressC.Checked = False Then
                cell.Visible = False
                Continue For
            End If

            If cell.ContainingField.HeaderText = "Phone" And chkPhoneC.Checked = False Then
                cell.Visible = False
                Continue For
            End If

            If cell.ContainingField.HeaderText = "Mobile" And chkMobileC.Checked = False Then
                cell.Visible = False
                Continue For
            End If

            If cell.ContainingField.HeaderText = "Admission Date" And chkAdmissionDateC.Checked = False Then
                cell.Visible = False
                Continue For
            End If

            If cell.ContainingField.HeaderText = "Roll No" And chkRollNo.Checked = False Then
                cell.Visible = False
                Continue For
            End If
            If cell.ContainingField.HeaderText = "Category" And chkCategoryC.Checked = False Then
                cell.Visible = False
                Continue For
            End If

            If cell.ContainingField.HeaderText = "Religion" And chkReligionC.Checked = False Then
                cell.ViewStateMode = False
            End If


        Next

    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If cboSchoolName.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Provide School Name......');", True)
            cboSchoolName.Focus()
            Exit Sub
        End If
        If chkSchoolNameC.Checked = False And chkSubSection.Checked = False And chkFatherOccupation.Checked = False And chkRollNo.Checked = False And chkAdmnNoC.Checked = False And chkSNameC.Checked = False And chkFNameC.Checked = False And chkMNameC.Checked = False And chkClassC.Checked = False And chkSectionC.Checked = False And chkFBnoC.Checked = False And chkDobC.Checked = False And chkAddressC.Checked = False And chkPhoneC.Checked = False And chkMobileC.Checked = False And chkAdmissionDateC.Checked = False And chkCategoryC.Checked = False And chkmobno.Checked = False Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Select atleast one Display criterian to continue...');", True)
            chkAdmnNoC.Focus()
            Exit Sub
        End If


        If chkFDept.Checked = False And chkFatherOccupation.Checked = False And chkByName.Checked = False And chkbyCode.Checked = False And chkByReg.Checked = False And chkFName.Checked = False And chkClass.Checked = False And chkSection.Checked = False And chkHouse.Checked = False And chkReligion.Checked = False And chkCaste.Checked = False And chkGender.Checked = False And chkStatus.Checked = False And chkAdminDate.Checked = False And chkCategory.Checked = False And chkmobno.Checked = False Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Select atleast one criteria to continue...');", True)
            chkByName.Focus()
            Exit Sub
        End If

        If chkmobno.Checked = True And Trim(txtmobno.Text).Length <= 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Provide atleast one digit as Mobile No to continue......');", True)
            txtByCode.Focus()
            Exit Sub
        End If

        'If chkReference.Checked = True And Trim(txtreference.Text).Length <= 0 Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Provide atleast one Character as Reference to continue......');", True)
        '    txtByCode.Focus()
        '    Exit Sub
        'End If

        If chkbyCode.Checked = True And Trim(txtByCode.Text).Length <= 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Provide atleast one character as Code No. to continue...');", True)
            txtByCode.Focus()
            Exit Sub
        End If

        If chkByName.Checked = True And Trim(txtByName.Text).Length <= 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Provide atleast one character as Student Name to continue...');", True)
            txtByName.Focus()
            Exit Sub
        End If

        If chkFName.Checked = True And Trim(txtFName.Text).Length <= 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Provide atleast one character as Father Name to continue...');", True)
            txtFName.Focus()
            Exit Sub
        End If

        If chkByReg.Checked = True And Trim(txtByReg.Text).Length <= 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Provide atleast one character as Registration No. to continue...');", True)
            txtByReg.Focus()
            Exit Sub
        End If
        If chkClass.Checked = True And cboClass.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Provide atleast one class to continue...');", True)
            cboClass.Focus()
            Exit Sub
        End If

        'If chkReferenceCategory.Checked = True And cboReferenceCategory.Text = "" Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Provide atleast one Reference Category to continue...');", True)
        '    cboClass.Focus()
        '    Exit Sub
        'End If
        If chkSection.Checked = True And cboSection.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Provide atleast one Section to continue...');", True)
            cboSection.Focus()
            Exit Sub
        End If
        If chkSubSection.Checked = True And cboSubSection.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Provide atleast one Sub Section to continue...');", True)
            cboSubSection.Focus()
            Exit Sub
        End If
        If chkHouse.Checked = True And cboHouse.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Provide atleast one House to continue...');", True)
            cboHouse.Focus()
            Exit Sub
        End If
        If chkReligion.Checked = True And cboReligion.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Provide atleast one Religion to continue...');", True)
            cboReligion.Focus()
            Exit Sub
        End If
        If chkCaste.Checked = True And cboCaste.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Provide atleast one Caste to continue...');", True)
            cboCaste.Focus()
            Exit Sub
        End If
        If chkGender.Checked = True And cboGender.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Provide gender information...');", True)
            cboGender.Focus()
            Exit Sub
        End If
        If chkStatus.Checked = True And cboStatus.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Provide atleast one Status to continue...');", True)
            cboStatus.Focus()
            Exit Sub
        End If

        If chkAdminDate.Checked = True Then
            'If IsNumeric(txtAdminDate_DD.Text) = False Or IsNumeric(txtAdminDate_MM.Text) = False Or IsNumeric(txtAdminDate_YY.Text) = False Then
            '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Admission From Date...');", True)
            '    txtAdminDate_DD.Focus()
            '    Exit Sub
            'End If
            'If IsNumeric(txtAdminDate_DDTo.Text) = False Or IsNumeric(txtAdminDate_MMTo.Text) = False Or IsNumeric(txtAdminDate_YYTo.Text) = False Then
            '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Admission To Date...');", True)
            '    txtAdminDate_DDTo.Focus()
            '    Exit Sub
            'End If
            If txtAdminDateFrom.Text = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Admission From Date...');", True)
                txtAdminDateFrom.Focus()
                Exit Sub
            End If
            If txtAdminDateTo.Text = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Admission To Date...');", True)
                txtAdminDateTo.Focus()
                Exit Sub
            End If
        End If

        If chkFDept.Checked = True And cboFDept.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Provide father's department to continue...');", True)
            cboFDept.Focus()
            Exit Sub
        End If
        If chkCategory.Checked = True And cboCategory.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Provide atleast one Category to continue...');", True)
            cboCategory.Focus()
            Exit Sub
        End If
        If chkFatherOccupation.Checked = True And cboFatherOccupation.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Provide atleast one Father Occupation to continue...');", True)
            cboReligion.Focus()
            Exit Sub
        End If
        'Prepare SQL Query
        Dim sqlStr As String = ""
        sqlStr = "Select SchoolName,RegNo,ClassRollNo, SName, FName, MName, ClassName, SecName,CASE WHEN Gender = 0 THEN 'Male' ELSE 'Female' END AS Gender, FeeBookNo, FatherAddress, PhoneResd, DOB, MobNo, AdmissionDate,CategoryName,RelName,HouseName,CasteName From vw_Student Where ASID=" & Request.Cookies("ASID").Value
        If cboSchoolName.Text <> "ALL" Then
            sqlStr += " and SchoolName='" & cboSchoolName.Text & "'"
        End If
        If chkHouse.Checked = True Then
            If cboHouse.Text = "ALL" Then
            Else
                sqlStr &= " AND HouseName='" & cboHouse.Text & "' "
            End If
        End If
        If chkClass.Checked = True Then
            If cboClass.Text = "ALL" Then
            Else
                sqlStr &= " AND ClassName='" & cboClass.Text & "' "
            End If
        End If
        If chkByName.Checked = True Then sqlStr &= " AND SName Like '" & txtByName.Text & "%' "
        If chkbyCode.Checked = True Then sqlStr &= " AND FeeBookNo='" & txtByCode.Text & "' "
        If chkByReg.Checked = True Then sqlStr &= " AND RegNo Like '%" & txtByReg.Text & "%' "
        If chkFName.Checked = True Then sqlStr &= " AND FName Like '%" & txtFName.Text & "%' "
        If chkSection.Checked = True Then sqlStr &= " AND SecName='" & cboSection.Text & "' "
        If chkSubSection.Checked = True Then sqlStr &= " AND SubSecName='" & cboSubSection.Text & "' "
        If chkReligion.Checked = True Then sqlStr &= " AND RelName='" & cboReligion.Text & "' "
        If chkCaste.Checked = True Then sqlStr &= " AND CasteName='" & cboCaste.Text & "' "
        If chkGender.Checked = True Then sqlStr &= " AND Gender='" & cboGender.SelectedIndex & "' "
        If chkStatus.Checked = True Then sqlStr &= " AND StatusName='" & cboStatus.Text & "' "
        If chkFDept.Checked = True Then sqlStr &= " AND DeptName='" & cboFDept.Text & "' "
        If chkCategory.Checked = True Then sqlStr &= " AND CategoryName='" & cboCategory.Text & "' "

        If chkAdminDate.Checked = True Then sqlStr &= " AND AdmissionDate Between '" & txtAdminDateFrom.Text.Substring(6, 4) & "/" & txtAdminDateFrom.Text.Substring(3, 2) & "/" & txtAdminDateFrom.Text.Substring(0, 2) & "' and '" & txtAdminDateTo.Text.Substring(6, 4) & "/" & txtAdminDateTo.Text.Substring(3, 2) & "/" & txtAdminDateTo.Text.Substring(0, 2) & "'"
        If chkmobno.Checked = True Then
            sqlStr &= "AND MobNo Like'%" & txtmobno.Text & "%'"
        End If
        If chkFatherOccupation.Checked = True Then
            sqlStr &= " And OccName='" & cboFatherOccupation.Text & "'"
        End If
     
        If chkAdminDate.Checked = True Then
            sqlStr &= " Order By Convert(int,ClassRollNo),AdmissionDate, SName, RegNo"
        ElseIf cboClass.Text = "ALL" Then
            sqlStr &= " Order By ClassName,SecName,SName"
        Else
            sqlStr &= " Order By Convert(int,ClassRollNo),SName, RegNo"
        End If
        ExecuteQuery_Update(sqlStr)

        Dim myHeaderText As String = "Search Parameter: "
        Dim ANDFlag As Boolean = False

        If chkByName.Checked = True Then
            If ANDFlag = True Then myHeaderText &= ", "
            myHeaderText &= "Student Name=" & txtByName.Text
            ANDFlag = True
        End If
        If chkbyCode.Checked = True Then
            If ANDFlag = True Then myHeaderText &= ", "
            myHeaderText &= "Fee Book No=" & txtByCode.Text
            ANDFlag = True
        End If
        If chkByReg.Checked = True Then
            If ANDFlag = True Then myHeaderText &= ", "
            myHeaderText &= "Reg No.=" & txtByReg.Text
            ANDFlag = True
        End If
        If chkFName.Checked = True Then
            If ANDFlag = True Then myHeaderText &= ", "
            myHeaderText &= "Father Name=" & txtFName.Text
            ANDFlag = True
        End If
        If chkClass.Checked = True Then
            If ANDFlag = True Then myHeaderText &= ", "
            myHeaderText &= "Class=" & cboClass.Text
            ANDFlag = True
        End If
        If chkSection.Checked = True Then
            If ANDFlag = True Then myHeaderText &= ", "
            myHeaderText &= "Section=" & cboSection.Text
            ANDFlag = True
        End If
        If chkSubSection.Checked = True Then
            If ANDFlag = True Then myHeaderText &= ", "
            myHeaderText &= "Sub-Section=" & cboSubSection.Text
            ANDFlag = True
        End If
        If chkHouse.Checked = True Then
            If ANDFlag = True Then myHeaderText &= ", "
            myHeaderText &= "House=" & cboHouse.Text
            ANDFlag = True
        End If
        If chkReligion.Checked = True Then
            If ANDFlag = True Then myHeaderText &= ", "
            myHeaderText &= "Religion=" & cboReligion.Text
            ANDFlag = True
        End If
        If chkCaste.Checked = True Then
            If ANDFlag = True Then myHeaderText &= ", "
            myHeaderText &= "Caste=" & cboCaste.Text
            ANDFlag = True
        End If
        If chkGender.Checked = True Then
            If ANDFlag = True Then myHeaderText &= ", "
            myHeaderText &= "Gender=" & cboGender.SelectedIndex
            ANDFlag = True
        End If
        If chkCategory.Checked = True Then
            If ANDFlag = True Then myHeaderText &= ", "
            myHeaderText &= " Category=" & cboCategory.Text
            ANDFlag = True
        End If
        If chkStatus.Checked = True Then
            If ANDFlag = True Then myHeaderText &= ", "
            myHeaderText &= "Status=" & cboStatus.Text
            ANDFlag = True
        End If
        If chkFDept.Checked = True Then
            If ANDFlag = True Then myHeaderText &= ", "
            myHeaderText &= "Father Department=" & cboFDept.Text
            ANDFlag = True
        End If
        If chkAdminDate.Checked = True Then
            If ANDFlag = True Then myHeaderText &= ", "
            myHeaderText &= "Admission Date=" & txtAdminDateFrom.Text & " to " & txtAdminDateTo.Text
            ANDFlag = True
        End If
        If chkmobno.Checked = True Then
            If ANDFlag = True Then myHeaderText &= ", "
            myHeaderText &= "Mobile No=" & txtmobno.Text
            ANDFlag = True
        End If
        If chkFatherOccupation.Checked = True Then
            If ANDFlag = True Then myHeaderText &= ", "
            myHeaderText &= "Father Occupation=" & cboFatherOccupation.Text
            ANDFlag = True
        End If
        'If chkReference.Checked = True Then
        '    If ANDFlag = True Then myHeaderText &= ", "
        '    myHeaderText &= "Reference=" & txtreference.Text
        '    ANDFlag = True
        'End If
        'If chkReferenceCategory.Checked = True Then
        '    If ANDFlag = True Then myHeaderText &= ", "
        '    myHeaderText &= "Reference Category=" & cboReferenceCategory.Text
        '    ANDFlag = True
        'End If
        Dim SchoolID = FindMasterID(71, cboSchoolName.Text)
        lblSchoolName.Text = FindSchoolDetails1(SchoolID, 0)

        If Request.QueryString("type") = 0 Then
            SqlDataSource1.SelectCommand = sqlStr
            GridView1.DataBind()
            GridView1.Visible = True
            GridView2.Visible = False
            lblTitle.Text = myHeaderText
            lblTotalRecords.Text = "Total " & GridView1.Rows.Count & " records found..."
        ElseIf Request.QueryString("type") = 1 Then
            SqlDataSource2.SelectCommand = sqlStr
            GridView2.DataBind()
            GridView2.Visible = True
            GridView1.Visible = False
            If chkSection.Checked = True Then
                lblTitle.Text = "Class List of " & cboClass.Text & "-" & cboSection.Text
            Else
                lblTitle.Text = "Class List of " & cboClass.Text
            End If

            lblTotalRecords.Text = "Total " & GridView2.Rows.Count & " records found..."
        End If


        If GridView1.Rows.Count > 0 Or GridView2.Rows.Count > 0 Then
            btnPrint.Visible = True
            btnExcel.Visible = True
        Else
            btnPrint.Visible = False
            btnExcel.Visible = False
        End If
        For i = 0 To GridView1.Rows.Count - 1
            ' GridView1.Rows(i).Cells(0).Text = i + 1
            ' myTotal += GridView1.Rows(i).Cells(5).Text
        Next
    End Sub

    
    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        Dim Regno As String = GridView1.SelectedRow.Cells(2).Text
        Response.Write("<script> window.open( 'StudentMaster.aspx?RegNo=" & Regno & "' ); </script>")
    End Sub

    Protected Sub cboSection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSection.SelectedIndexChanged
        LoadClassSubSection(cboSchoolName.Text, cboClass.Text, cboSection.Text, cboSubSection)
        cboSection.Focus()
    End Sub

    Protected Sub cboSchoolName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSchoolName.SelectedIndexChanged
        If cboSchoolName.Text <> "ALL" Then
            LoadMasterInfo(2, cboClass, cboSchoolName.Text)
            cboClass.Items.Add("ALL")
        Else
            LoadMasterInfo(74, cboClass)
            cboClass.Items.Add("ALL")
        End If
        cboSchoolName.Focus()
    End Sub
End Class