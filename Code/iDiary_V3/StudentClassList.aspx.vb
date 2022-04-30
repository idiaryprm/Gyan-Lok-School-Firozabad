Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports System.IO

Public Class StudentClassList
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

        chkClass.Checked = False
        LoadMasterInfo(71, cboSchoolName, Request.Cookies("SchoolIDs").Value)
        LoadMasterInfo(2, cboClass, cboSchoolName.Text)

        chkSection.Checked = False
        'LoadClassSection(cboClass.Text, cboSection)

        chkHouse.Checked = False
        LoadMasterInfo(4, cboHouse)


        chkCategory.Checked = False
        LoadMasterInfo(34, cboCategory)

        chkGender.Checked = False

        chkStatus.Checked = False
        LoadMasterInfo(10, cboStatus)

        btnPrint.Visible = False
        btnExcel.Visible = False


        chkGender.Enabled = True
        cboGender.Enabled = True

        chkHouse.Enabled = True
        cboHouse.Enabled = True

        GridView2.Visible = False
        lblCount.Text = ""

    End Sub

    Protected Sub cboClass_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboClass.SelectedIndexChanged
        LoadClassSection(cboSchoolName.Text, cboClass.Text, cboSection)
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
        frm.Controls.Add(GridView2)

        frm.RenderControl(hw)
        Response.Write(sw.ToString())
        Response.End()

    End Sub
  
    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If cboSchoolName.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Provide School Name to continue...');", True)
            cboSchoolName.Focus()
            Exit Sub
        End If
        If chkClass.Checked = True And cboClass.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Provide atleast one class to continue...');", True)
            cboClass.Focus()
            Exit Sub
        End If
        If chkSection.Checked = True And cboSection.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Provide atleast one Section to continue...');", True)
            cboSection.Focus()
            Exit Sub
        End If
        If chkHouse.Checked = True And cboHouse.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Provide atleast one House to continue...');", True)
            cboHouse.Focus()
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

        If chkCategory.Checked = True And cboCategory.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Provide atleast one Category to continue...');", True)
            cboCategory.Focus()
            Exit Sub
        End If
        If chkClass.Checked = False And chkSection.Checked = False Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Class and Section are compulsory for continue...');", True)
            cboClass.Focus()
            Exit Sub
        End If
        If chkClass.Checked = True And chkSection.Checked = False Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Section are compulsory for continue...');", True)
            cboSection.Focus()
            Exit Sub
        End If
        If chkClass.Checked = False And chkSection.Checked = True Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Class are compulsory for continue...');", True)
            cboClass.Focus()
            Exit Sub
        End If
        'Prepare SQL Query
        Dim sqlStr As String = ""
        sqlStr = "Select RegNo,ClassRollNo, SName, FName, MName, ClassName, SecName, FeeBookNo, FatherAddress, PhoneResd, DOB, MobNo, AdmissionDate,CategoryName From vw_Student Where SchoolName='" & cboSchoolName.Text & "' and ASID=" & Request.Cookies("ASID").Value

        If chkClass.Checked = True Then sqlStr &= " AND ClassName='" & cboClass.Text & "' "
        If chkSection.Checked = True Then sqlStr &= " AND SecName='" & cboSection.Text & "' "
        If chkHouse.Checked = True Then sqlStr &= " AND HouseName='" & cboHouse.Text & "' "
        If chkGender.Checked = True Then sqlStr &= " AND Gender='" & cboGender.SelectedIndex & "' "
        If chkStatus.Checked = True Then sqlStr &= " AND StatusName='" & cboStatus.Text & "' "
        If chkCategory.Checked = True Then sqlStr &= " AND CategoryName='" & cboCategory.Text & "' "
        sqlStr &= " Order By Convert(int,ClassRollNo),SName, RegNo"
        
        ExecuteQuery_Update(sqlStr)
        
        Dim myHeaderText As String = "Search Parameter: "
        Dim ANDFlag As Boolean = False

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
        If chkHouse.Checked = True Then
            If ANDFlag = True Then myHeaderText &= ", "
            myHeaderText &= "House=" & cboHouse.Text
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

        lblSchoolName.Text = FindSchoolDetails(1)

            SqlDataSource2.SelectCommand = sqlStr
            GridView2.DataBind()
            GridView2.Visible = True
        If chkSection.Checked = True Then
            lblTitle.Text = "Class List of " & cboClass.Text & "-" & cboSection.Text
        Else
            lblTitle.Text = "Class List of " & cboClass.Text
        End If


        If GridView2.Rows.Count > 0 Then
            btnPrint.Visible = True
            btnExcel.Visible = True
        Else
            btnPrint.Visible = False
            btnExcel.Visible = False
        End If
        lblCount.Text = GridView2.Rows.Count & " records found"
    End Sub

    Protected Sub cboSchoolName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSchoolName.SelectedIndexChanged
        LoadMasterInfo(2, cboClass, cboSchoolName.Text)
        cboSchoolName.Focus()
    End Sub
End Class