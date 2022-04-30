'  ....................... created by vikash gupta on date 24/06/2016.........................


Imports System.Data.SqlClient
Imports iDiary_V3.iDiary_Date.CLS_iDiary_Date
Imports iDiary_V3.iDiary.CLS_idiary
Imports Microsoft.Reporting.WebForms
Imports System.IO

Public Class StudentNotes
    Inherits System.Web.UI.Page
    Private Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cookies("ActiveTab").Value = 15
        Response.Cookies("ActiveTab").Expires = DateTime.Now.AddHours(1)
        Session("ActiveTab") = 15
        If IsPostBack = False Then
            InitControls()
        Else
            Dim printScript As String = "function PrintGridView() { var gridInsideDiv = document.getElementById('gvDiv');" & _
          " var printWindow = window.open('gview.htm','PrintWindow','letf=0,top=0,width=150,height=300,toolbar=1,scrollbars=1,status=1');" & _
          " printWindow.document.write(gridInsideDiv.innerHTML);printWindow.document.close();printWindow.focus();" & _
          " printWindow.print();printWindow.close();}"
            Me.ClientScript.RegisterStartupScript(Page.[GetType](), "PrintGridView", printScript.ToString(), True)
            btnprint.Attributes.Add("onclick", "PrintGridView();")
        End If
    End Sub

    Private Sub InitControls()
        txtAdminNo.Text = ""
        txtSchoolName.Text = ""
        txtSName.Text = ""
        txtFName.Text = ""
        txtMName.Text = ""
        txtAdmissionDate.Text = ""
        txtDOB.Text = ""
        txtRollNo.Text = ""
        txtClass.Text = ""
        txtSec.Text = ""
        txtSID.Text = ""
        txtGender.Text = ""
        txtdate.Text = Now.Date.ToString("dd/MM/yyyy")
        txtcomments.Text = ""
        GridView2.Visible = False
        'ReportViewer1.Visible = False
        GridView2.Columns(1).Visible = False
        btnremove.Visible = False
        txtAdminNo.Focus()
        btnprint.Visible = False
        btnexporttoexel.Visible = False
        ImageButton1.Visible = False
        imgPhoto.Visible = False

    End Sub

    Protected Sub btnFind_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFind.Click
        imgPhoto.Visible = True
        If txtAdminNo.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Registration No is required...');", True)
            txtAdminNo.Focus()
            Exit Sub
        End If
        FillStudentDetail(txtAdminNo.Text)
        Dim ClassTeacher As Boolean = False
        Dim SecTeacher As Boolean = False
        If Request.Cookies("UType").Value.ToString.Contains("Admin") = False Then
            Dim EmpID As Integer = FindEmpID()
            ClassTeacher = CheckClassTeacher(txtSchoolName.Text, txtClass.Text, EmpID)
            SecTeacher = CheckSecTeacher(txtSchoolName.Text, txtSec.Text, EmpID)
            If ClassTeacher = True And SecTeacher = True Then
                btnsave.Visible = True
            Else
                btnsave.Visible = False
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('You have not permission to Enter Notes for current student...');", True)
            End If
        End If
        If btnsave.Visible = True Then
            Dim Sid As Integer = GetSID(txtAdminNo.Text, Request.Cookies("ASID").Value)
            Try
                GridView2.Visible = True
                StudentNotepad.SelectCommand = "SELECT StudentNotesID,EntryDate,Comments,('StudentNotes/' + NoteDocPath) as NoteDocPath FROM StudentNotes WHERE SID='" & Sid & "' and Isdeleted=0"
                GridView2.DataBind()
            Catch ex As Exception

            End Try
        End If

        'imgPhoto.ImageUrl = "~/images/StudentDummy.jpg"
        'ImageButton1.Visible = False
        'btnprint.Visible = False
        'btnexporttoexel.Visible = False
        'For Each gvr As GridViewRow In GridView2.Rows
        '    Dim url As String = ""
        '    Dim StudentNotesID As Integer = GridView2.DataKeys(gvr.RowIndex).Value.ToString()
        '    Dim sqlStr As String = "select NoteDocPath from StudentNotes where StudentNotesID='" & StudentNotesID & "'"

        '    Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        '    While myReader.Read
        '        Dim imagebutton2 As ImageButton = DirectCast(gvr.FindControl("imagebutton2"), ImageButton)
        '        url = myReader(0)
        '        If url = "" Then
        '            imagebutton2.Visible = False
        '        Else
        '            imagebutton2.Visible = True
        '        End If
        '    End While
        '    myReader.Close()
        'Next
        txtAdminNo.Focus()
    End Sub
    Private Function CheckClassTeacher(SchoolName As String, ClsName As String, EmpID As Integer) As Boolean
        Dim sqlstr As String = "Select count(*) From vw_ClassTeacher Where EmpID='" & EmpID & "' and ClassName='" & ClsName & "' and SchoolName='" & SchoolName & "'"
        Dim rv As Integer = 0
        Try
            rv = ExecuteQuery_ExecuteScalar(sqlstr)
        Catch ex As Exception

        End Try
        If rv > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Function CheckSecTeacher(SchoolName As String, SecName As String, EmpID As Integer) As Boolean
        Dim sqlstr As String = "Select count(*) From vw_ClassTeacher Where EmpID='" & EmpID & "' and SecName='" & SecName & "' and SchoolName='" & SchoolName & "'"
        Dim rv As Integer = 0
        Try
            rv = ExecuteQuery_ExecuteScalar(sqlstr)
        Catch ex As Exception

        End Try
        If rv > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Function FindEmpID() As Integer
        Dim sqlStr As String = ""
        sqlStr = "Select MAX(EmpID) From Users Where UserID='" & Request.Cookies("UserID").Value & "'"
        Dim rv As Integer = 0
        Try
            rv = ExecuteQuery_ExecuteScalar(sqlStr)
        Catch ex As Exception

        End Try
        Return rv
    End Function
    Private Sub FillStudentDetail(RegNo As String)
        Dim sqlStr As String = ""
        sqlStr = "Select RegNo, SName, FName, MName, AdmissionDate, DOB, ClassName,SecName,Gender,ClassRollNo,SID,PhotoPath,SchoolName From vw_Student Where RegNo='" & RegNo & "' AND ASID=" & Request.Cookies("ASID").Value
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)

        While myReader.Read
            txtSchoolName.Text = myReader("SchoolName")
            txtSName.Text = myReader("SName")
            txtFName.Text = myReader("FName")
            txtMName.Text = myReader("MName")
            Dim tmpDate As Date = myReader("AdmissionDate")
            'txtAdmissionDate.Text = a.Substring(3, 2) & "/" & a.Substring(0, 2) & "/" & a.Substring(6, 4)
            txtAdmissionDate.Text = tmpDate.ToString("dd/MM/yyyy")
            tmpDate = myReader("DOB")
            'txtDOB.Text = a.Substring(3, 2) & "/" & a.Substring(0, 2) & "/" & a.Substring(6, 4)
            txtDOB.Text = tmpDate.ToString("dd/MM/yyyy")
            txtClass.Text = myReader("ClassName")
            txtSec.Text = myReader("SecName")
            txtDOBInWords.Text = ConvertDateToWords(tmpDate.Day, tmpDate.Month, tmpDate.Year)
            txtGender.Text = myReader("Gender")
            txtSID.Text = ""
            imgPhoto.ImageUrl = myReader("PhotoPath")
            Try
                txtRollNo.Text = myReader("ClassRollNo")
            Catch ex As Exception

            End Try
        End While
        myReader.Close()
    End Sub


    Protected Sub btnNameSearch_Click(sender As Object, e As EventArgs) Handles btnNameSearch.Click
        GridView1.Visible = True
        SqlDataSource1.SelectCommand = "SELECT RegNo, SName, ClassName, SecName, FName, MName, AdmissionDate, DOB, SchoolName FROM vw_Student WHERE ASID = " & Request.Cookies("ASID").Value & " and SName Like '%" & txtSName.Text & "%'"
        GridView1.DataBind()
        ImageButton1.Visible = False
        btnprint.Visible = False
        btnexporttoexel.Visible = False
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        btnprint.Visible = True
        btnexporttoexel.Visible = True
        imgPhoto.Visible = True
        txtAdminNo.Text = GridView1.SelectedRow.Cells(1).Text
        FillStudentDetail(txtAdminNo.Text)
        Dim ClassTeacher As Boolean = False
        Dim SecTeacher As Boolean = False
        If Request.Cookies("UType").Value.ToString.Contains("Admin") = False Then
            Dim EmpID As Integer = FindEmpID()
            ClassTeacher = CheckClassTeacher(txtSchoolName.Text, txtClass.Text, EmpID)
            SecTeacher = CheckSecTeacher(txtSchoolName.Text, txtSec.Text, EmpID)
            If ClassTeacher = True And SecTeacher = True Then
                btnsave.Visible = True
            Else
                btnsave.Visible = False
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('You have not permission to Enter Notes for current student...');", True)
            End If
        End If
        GridView1.Visible = False
        If btnsave.Visible = True Then
            Dim Sid As Integer = GetSID(txtAdminNo.Text, Request.Cookies("ASID").Value)
            Try
                GridView2.Visible = True
                StudentNotepad.SelectCommand = "SELECT StudentNotesID,EntryDate,Comments, NoteDocPath FROM StudentNotes WHERE SID='" & Sid & "' and Isdeleted=0"

                GridView2.DataBind()
            Catch ex As Exception

            End Try
            ImageButton1.Visible = False
            If GridView2.Rows.Count > 0 Then
                btnprint.Visible = True
                btnexporttoexel.Visible = True
            Else
                btnprint.Visible = False
                btnexporttoexel.Visible = False
            End If
        End If
        
        'For Each gvr As GridViewRow In GridView2.Rows
        '    Dim url As String = ""
        '    Dim StudentNotesID As Integer = GridView2.DataKeys(gvr.RowIndex).Value.ToString()
        '    Dim sqlStr As String = "select NoteDocPath from StudentNotes where StudentNotesID='" & StudentNotesID & "'"

        '    Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        '    While myReader.Read
        '        Dim imagebutton2 As ImageButton = DirectCast(gvr.FindControl("imagebutton2"), ImageButton)
        '        url = myReader(0)
        '        If url = "" Then
        '            imagebutton2.Visible = False
        '        Else
        '            imagebutton2.Visible = True
        '        End If
        '    End While
        '    myReader.Close()
        'Next

        'imgPhoto.ImageUrl = "~/images/StudentDummy.jpg"

    End Sub

    Protected Sub GridView2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView2.SelectedIndexChanged
       
        btnremove.Visible = True
        txtSID.Text = GridView2.DataKeys(GridView2.SelectedIndex).Value.ToString()
        txtdate.Text = GridView2.SelectedRow.Cells(2).Text
        txtcomments.Text = GridView2.SelectedRow.Cells(3).Text
        Dim url As String = ""
        Dim sqlStr As String = "select NoteDocPath from StudentNotes where StudentNotesID='" & txtSID.Text & "'"

        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            url = myReader(0)
        End While
        myReader.Close()
        If url = "" Then
            ImageButton1.Visible = False
        Else
            ImageButton1.Visible = True
        End If

    End Sub
    Protected Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        btnremove.Visible = False
        If Trim(txtAdminNo.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Admission/SR No. is required...');", True)
            txtAdminNo.Focus()
            Exit Sub
        End If
        If Trim(txtcomments.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Comments is required...');", True)
            txtAdminNo.Focus()
            Exit Sub
        End If
        If txtdate.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Date is required...');", True)
            txtdate.Focus()
            Exit Sub
        End If
        Dim CreatedOn As Date = Now.Date
        Try
            CreatedOn = txtdate.Text.Substring(6, 4) & "/" & txtdate.Text.Substring(3, 2) & "/" & txtdate.Text.Substring(0, 2)
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Date...');", True)
            txtdate.Focus()
            Exit Sub
        End Try
        Dim fileName As String = ""
        If myfile.PostedFile.FileName = "" Then
            fileName = ""
        Else
            fileName = myfile.PostedFile.FileName
        End If
        Dim Sid As Integer = GetSID(txtAdminNo.Text, Request.Cookies("ASID").Value)
        If Sid <> 0 Then
            Dim sqlStr As String = ""
            If txtSID.Text = "" Then
                sqlStr = "Insert into StudentNotes(SID,EntryDate,Comments,NoteDocPath,CreatedOn,CreatedBy,Isdeleted) values('" & Sid & "','" & Now.Date.ToString("yyyy/MM/dd") & "','" & SQLFixup(txtcomments.Text) & "','" & fileName & "',"
                sqlStr += "'" & txtdate.Text.Substring(6, 4) & "/" & txtdate.Text.Substring(3, 2) & "/" & txtdate.Text.Substring(0, 2) & "', " & Request.Cookies("UserID").Value & ""
                sqlStr += ",0)"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Note Added Successfully ..');", True)
            Else
                sqlStr = "update StudentNotes set UpdateOn='" & txtdate.Text.Substring(6, 4) & "/" & txtdate.Text.Substring(3, 2) & "/" & txtdate.Text.Substring(0, 2) & "', Comments='" & SQLFixup(txtcomments.Text) & "', UpdateBy=" & Request.Cookies("UserID").Value & " where StudentNotesID='" & txtSID.Text & "'"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Note Updated Successfully ..');", True)
            End If
            ExecuteQuery_Update(sqlStr)
            UploadNotes()
            Try
                GridView2.Visible = True
                StudentNotepad.SelectCommand = "SELECT StudentNotesID,EntryDate,Comments,('StudentNotes/' + NoteDocPath) as NoteDocPath FROM StudentNotes WHERE SID='" & Sid & "' and Isdeleted=0"
                GridView2.DataBind()
            Catch ex As Exception

            End Try
            txtcomments.Text = ""
            ImageButton1.Visible = False
        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Registration No...');", True)
            txtAdminNo.Focus()
            Exit Sub
        End If
       
       
       
    End Sub


    Private Sub UploadNotes()
        Dim fp1 As String = myfile.PostedFile.FileName
        If fp1.ToString() <> "" Then
            Dim fn1 As String = fp1.Substring(fp1.LastIndexOf("\\") + 1)
            Dim sp1 As String = ""
            sp1 = Server.MapPath("~/StudentNotes")
            If sp1.EndsWith("\\") = False Then
                sp1 += "\"
            End If

            sp1 += fp1
            myfile.PostedFile.SaveAs(sp1)
        End If

    End Sub

    Protected Sub btnremove_Click(sender As Object, e As EventArgs) Handles btnremove.Click
        Dim sqlStr As String = ""
        sqlStr = "update StudentNotes set Isdeleted=1, UpdateOn='" & txtdate.Text.Substring(6, 4) & "/" & txtdate.Text.Substring(3, 2) & "/" & txtdate.Text.Substring(0, 2) & "', UpdateBy=" & Request.Cookies("UserID").Value & "  where StudentNotesID='" & txtSID.Text & "'"
        ExecuteQuery_Update(sqlStr)
        Dim Sid As Integer = GetSID(txtAdminNo.Text, Request.Cookies("ASID").Value)
        Try
            GridView2.Visible = True
            If myfile.PostedFile.FileName = "" Then
                StudentNotepad.SelectCommand = "SELECT StudentNotesID,EntryDate,Comments,('StudentNotes/' + NoteDocPath) as NoteDocPath FROM StudentNotes WHERE SID='" & Sid & "' and Isdeleted=0"
            Else
                StudentNotepad.SelectCommand = "SELECT StudentNotesID,EntryDate,Comments,('StudentNotes/' + NoteDocPath) as NoteDocPath FROM StudentNotes WHERE SID='" & Sid & "' and Isdeleted=0"
            End If
            GridView2.DataBind()
        Catch ex As Exception

        End Try
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Notes Remove Successfully...');", True)
        txtcomments.Text = ""
        ImageButton1.Visible = False
    End Sub

    Protected Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click
       
    End Sub

    Protected Sub btnexporttoexel_Click(sender As Object, e As EventArgs) Handles btnexporttoexel.Click
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

    Protected Sub downloadnotes_Click(sender As Object, e As ImageClickEventArgs)
       
        Dim url As String = ""
        Dim sqlStr As String = ""

        If txtSID.Text = "" Then
            Dim rowVal As ImageButton = TryCast(sender, ImageButton)
            Dim gvRowIndex As Integer = 0
            gvRowIndex = Convert.ToInt32(rowVal.Attributes("RowIndex"))
            Dim index As Integer = Convert.ToInt32(gvRowIndex)
            Dim FileAttach As String = GridView2.DataKeys(index).Values(1)
            Dim p As String = FileAttach
            Response.Write("<script> window.open('" & p & "'); </script>")


            'sqlStr = "select NoteDocPath from StudentNotes where StudentNotesID='" & gvRowIndex + 1 & "'"
        Else
            'sqlStr = "select NoteDocPath from StudentNotes where StudentNotesID='" & txtSID.Text & "'"
        End If


        'Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        'While myReader.Read
        '    url = myReader(0)
        'End While
        'If url = "" Then
        '    ImageButton1.Visible = False
        'Else
        '    ImageButton1.Visible = True
        'End If
        'myReader.Close()

        'Response.Clear()
        'Response.ContentType = "APPLICATION/OCTET-STREAM"
        'Dim Header As [String] = "Attachment; Filename=" & url
        'Response.AppendHeader("Content-Disposition", Header)
        'Try
        '    Dim Dfile As New System.IO.FileInfo(Server.MapPath("~/StudentNotes/" + url))
        '    Response.WriteFile(Dfile.FullName)
        'Catch ex As Exception

        'End Try
        'Response.[End]()
    End Sub

    Protected Sub GridView2_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView2.RowCommand
        'Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        'Dim FileAttach As String = GridView2.DataKeys(index).Values(1)
        'Dim p As String = "StudentNotes\\" & FileAttach
        'Response.Write("<script> window.open('" & p & "'); </script>")
    End Sub

    Protected Sub GridView2_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView2.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim imgBtnFile As ImageButton = TryCast(e.Row.FindControl("imagebutton2"), ImageButton)
            If GridView2.DataKeys(e.Row.RowIndex).Values(1) <> "" Then
                imgBtnFile.Visible = True
            Else
                imgBtnFile.Visible = False
            End If
        End If
    End Sub

End Class