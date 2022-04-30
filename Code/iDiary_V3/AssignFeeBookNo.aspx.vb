Imports iDiary_V3.iDiary.CLS_idiary
Imports System.Data.SqlClient
Imports iDiary_V3.iDiary_Student.CLS_iDiary_Student
Imports System.IO

Public Class AssignFeeBookNo
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
        Session("ActiveTab") = 4
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
        Else
            printFeeBookno()
            If ViewState("myTable") = True Then
                CreateTable()
            End If
        End If
    End Sub


    Protected Sub cboClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboClass.SelectedIndexChanged
        LoadClassSection(cboSchoolName.Text, cboClass.Text, cboSection)
        cboClass.Focus()
    End Sub

    Protected Sub btnAsignNo_Click(sender As Object, e As EventArgs) Handles btnAsignNo.Click
        If cboClass.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Provide atleast one class to continue...');", True)
            cboClass.Focus()
            Exit Sub
        End If
        If cboSection.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Provide atleast one Section to continue...');", True)
            cboSection.Focus()
            Exit Sub
        End If


        lblStatus.Text = ""




        Dim sqlStr As String = ""



        Dim myVal As Integer = ValidateNonNumericRecords()
        If myVal > 0 Then
            lblStatus.Text = "Invalid Fee Book No..."
            CType(myTable.FindControl("txtFeeNo" & myVal), TextBox).Focus()
            Exit Sub
        End If
        Dim asid = Request.Cookies("ASID").Value
        For i As Integer = 1 To myTable.Rows.Count - 1

            Dim SID As Integer = GetSID(myTable.Rows(i).Cells(1).Text)

            Dim myMarksToSave As String = CType(myTable.FindControl("txtFeeNo" & i), TextBox).Text

            If CheckFeeBookNoExist(myMarksToSave, asid, SID) = True Then
                lblStatus.Text = "Fee Book No " & myMarksToSave & " already Exists ..."
                CType(myTable.FindControl("txtFeeNo" & i), TextBox).Focus()
                Exit Sub
            End If
            sqlStr = "Update Student Set FeeBookNo='" & myMarksToSave & "' Where SID=" & SID


            ExecuteQuery_Update(sqlStr)

        Next

        System.Threading.Thread.Sleep(500)


        ViewState("myTable") = False
        '   InitControls()
        myTable.Rows.Clear()
        lblStatus.Text = "Fee Book No. Updated Successfully..."
    End Sub


    Private Sub CreateTable()
        myTable.Rows.Clear()

        Dim tr1 As New TableRow

        Dim td10 As New TableCell
        td10.Text = "<B>Sr. No.</B>"
        td10.HorizontalAlign = HorizontalAlign.Center
        tr1.Cells.Add(td10)

        Dim td11 As New TableCell
        td11.Text = "<B>Admission No.</B>"
        td11.HorizontalAlign = HorizontalAlign.Center
        tr1.Cells.Add(td11)

        Dim td12 As New TableCell
        td12.Text = "<B>Roll No.</B>"
        td12.HorizontalAlign = HorizontalAlign.Center
        tr1.Cells.Add(td12)

        Dim td13 As New TableCell
        td13.Text = "<B>&nbsp;&nbsp;&nbsp;Student Name</B>"
        td13.HorizontalAlign = HorizontalAlign.Left
        tr1.Cells.Add(td13)

        Dim td14 As New TableCell
        td14.Text = "<B>Fee Book No</B>"
        td14.HorizontalAlign = HorizontalAlign.Center
        tr1.Cells.Add(td14)

        myTable.Rows.Add(tr1)

        Dim sqlStr As String = ""







        sqlStr = "Select RegNo,ClassRollno, SName,FeeBookNo From vw_Student Where SchoolName='" & cboSchoolName.Text & "' and ClassName='" & cboClass.Text & "' AND SecName='" & cboSection.Text & "' AND ASID=" & Request.Cookies("ASID").Value & " AND StatusName='" & cboStatus.Text & "' Order By SName,Gender desc"


        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        Dim myTxtBoxNumber As Integer = 1
        While myReader.Read
            Dim trx As New TableRow

            Dim tdx0 As New TableCell
            tdx0.Text = myTxtBoxNumber
            tdx0.HorizontalAlign = HorizontalAlign.Center
            trx.Cells.Add(tdx0)

            Dim tdx1 As New TableCell
            tdx1.Text = myReader(0)
            tdx1.HorizontalAlign = HorizontalAlign.Center
            trx.Cells.Add(tdx1)

            Dim tdx2 As New TableCell
            tdx2.Text = "&nbsp;&nbsp;&nbsp;" & myReader(1)
            tdx2.HorizontalAlign = HorizontalAlign.Center
            trx.Cells.Add(tdx2)

            Dim tdx3 As New TableCell
            tdx3.Text = "&nbsp;&nbsp;&nbsp;" & myReader(2)
            tdx3.HorizontalAlign = HorizontalAlign.Left
            trx.Cells.Add(tdx3)

            Dim txtMax As New TextBox()
            txtMax.ID = "txtFeeNo" & myTxtBoxNumber
            txtMax.Width = 70
            txtMax.TabIndex = myTxtBoxNumber
            Try
                txtMax.Text = myReader(3)
            Catch ex As Exception
                txtMax.Text = ""
            End Try

            Dim tdx4 As New TableCell
            tdx4.Controls.Add(txtMax)
            tdx4.HorizontalAlign = HorizontalAlign.Center
            trx.Cells.Add(tdx4)


            myTable.Rows.Add(trx)

            myTxtBoxNumber += 1
        End While
        myReader.Close()




        btnAsignNo.Visible = True
        myTable.EnableViewState = True
        ViewState("myTable") = True
        txtFeeBooknoStart.Visible = True
        lblFeeBookSeries.Visible = True
        btnAsignNoSeries.Visible = True
    End Sub

    Private Function ValidateNonNumericRecords() As Integer  '1-Max Marks    2-Marks obtained
        Dim i As Integer = 0, rv As Integer = 0
        For i = 1 To myTable.Rows.Count - 1

            If IsNumeric(CType(myTable.FindControl("txtFeeNo" & i), TextBox).Text) = False Then
                rv = i
                Exit For
            End If

        Next
        Return rv
    End Function
    Private Function GetSID(ByVal myAdminNo As String) As Integer




        Dim sqlStr As String = "Select Max(SID) From Student Where RegNo='" & myAdminNo & "' AND ASID=" & Request.Cookies("ASID").Value

        Dim rv As Integer = 0
        rv = ExecuteQuery_ExecuteScalar(sqlStr)


        Return rv
    End Function

    Protected Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
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
        btnprint.Visible = True
        btnexporttoexcel.Visible = True
        lblStatus.Text = ""
        CreateTable()
    End Sub


    Protected Sub btnAsignNoSeries_Click(sender As Object, e As EventArgs) Handles btnAsignNoSeries.Click
        If cboSchoolName.Text = "" Then
            lblStatus.Text = "Invalid School..."
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
        If txtFeeBooknoStart.Text = "" Or IsNumeric(Trim(txtFeeBooknoStart.Text)) = False Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Provide Valid Fee Book Start No. to continue...');", True)
            txtFeeBooknoStart.Focus()
            Exit Sub
        End If

        Dim ds As New DataSet
        Dim SID As Integer = 0, i As Integer = 0
        Dim sqlStr As String = "Select SID, SName,FeeBookNo From vw_Student Where SchoolName='" & cboSchoolName.Text & "' and ClassName='" & cboClass.Text & "' AND SecName='" & cboSection.Text & "' AND ASID=" & Request.Cookies("ASID").Value & " AND StatusName='" & cboStatus.Text & "' Order By SName,gender desc"
        ds = ExecuteQuery_DataSet(sqlStr, "tbl")
        Dim StartFrom As Integer = 0

        Try
            StartFrom = CInt(txtFeeBooknoStart.Text)
        Catch ex As Exception
            Exit Sub
        End Try
        Dim alradyAssigned As String = ""
        Dim ASID As Integer = Request.Cookies("ASID").Value
        For Each Row As DataRow In ds.Tables(0).Rows
            Try
                SID = Row("SID")
                If CheckFeeBookNoExist(StartFrom, ASID, SID) = True Then
                    alradyAssigned &= StartFrom & " ,"
                    StartFrom += 1
                    Continue For
                End If
                sqlStr = "Update Student Set FeeBookNo='" & StartFrom & "' Where SID=" & SID
                ExecuteQuery_Update(sqlStr)
                StartFrom += 1
            Catch ex As Exception

            End Try

        Next
        Try
            alradyAssigned = alradyAssigned.Substring(0, alradyAssigned.Length - 1) & " Fee book no. are already assigned."
        Catch ex As Exception

        End Try
        CreateTable()

        lblStatus.Text = "Fee Book No Assignment complete. " & alradyAssigned
    End Sub

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
        frm.Controls.Add(myTable)
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

    Protected Sub cboSchoolName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSchoolName.SelectedIndexChanged
        LoadMasterInfo(2, cboClass, cboSchoolName.Text)
        cboSchoolName.Focus()
    End Sub
End Class