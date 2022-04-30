Imports iDiary_V3.iDiary.CLS_idiary
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO

Public Class AssignRollNo
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
        Session("ActiveTab") = 2
        If (Request.Cookies("UType").Value.ToString.Contains("Admin-1") = False And Request.Cookies("UType").Value.ToString.Contains("Student-1") = False) Then
            btnAsignNo.Enabled = False
        End If
        If IsPostBack = False Then
            LoadMasterInfo(71, cboSchoolName, Request.Cookies("SchoolIDs").Value)
            LoadMasterInfo(2, cboClass, cboSchoolName.Text)
            LoadMasterInfo(10, cboStatus)
            cboSection.Text = ""
            cboClass.Focus()
            '  GridView1.Visible = False
        Else
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
        AssignRollNoMannually()
    End Sub
    Private Sub AssignRollNoMannually()
        If cboSchoolName.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('School Name is Required...');", True)
            cboSchoolName.Focus()
            Exit Sub
        End If
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


        Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        Dim myConn As New SqlConnection(myConnStr)
        myConn.Open()
        Dim sqlStr As String = ""
        Dim myCommand As New SqlCommand
        myCommand.Connection = myConn
        'For i As Integer = 0 To GridView1.Rows.Count - 1
        '    sqlStr = "Update Student Set ClassRollNo='" & i + 1 & "' Where RegNo='" & GridView1.Rows(i).Cells(0).Text & "' And ASID='" & Request.Cookies("ASID").Value & "'"
        '    myCommand.CommandText = sqlStr
        '    myCommand.ExecuteNonQuery()
        'Next
        'SqlDataSourceStudents.SelectCommand = "SELECT [RegNo], [SName], [ClassRollno], [ClassName], [SecName] FROM [vw_Student] WHERE ([ASID] = @ASID) And ClassName='" & cboClass.Text & "' And SecName='" & cboSection.Text & "' And StatusName='" & cboStatus.Text & "' Order by SName"
        'GridView1.DataBind()


        Dim myVal As Integer = ValidateNonNumericRecords()
        If myVal > 0 Then
            lblStatus.Text = "Invalid Roll No..."
            CType(myTable.FindControl("txtRollNo" & myVal), TextBox).Focus()
            Exit Sub
        End If
        For i As Integer = 1 To myTable.Rows.Count - 1

            Dim SID As Integer = GetSID(myTable.Rows(i).Cells(1).Text)

            Dim myMarksToSave As String = CType(myTable.FindControl("txtRollNo" & i), TextBox).Text
            'Else
            '    myMarksToSave = CType(myTable.FindControl("txtMarks" & i), TextBox).Text & "/" & CType(myTable.FindControl("txtMax" & i), TextBox).Text

            sqlStr = "Update Student Set ClassRollNo='" & myMarksToSave & "' Where SID=" & SID
            myCommand.CommandText = sqlStr
            myCommand.Connection = myConn
            myCommand.ExecuteNonQuery()

        Next

        System.Threading.Thread.Sleep(500)
        myConn.Close()
        myConn.Dispose()
        CreateTable()
        'ViewState("myTable") = False
        '   InitControls()
        'myTable.Rows.Clear()
        lblStatus.Text = "Roll No. Updated Successfully..."
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
        td12.Text = "<B>&nbsp;&nbsp;&nbsp;Student Name</B>"
        td12.HorizontalAlign = HorizontalAlign.Left
        tr1.Cells.Add(td12)

        Dim td13 As New TableCell
        td13.Text = "<B>Roll No</B>"
        td13.HorizontalAlign = HorizontalAlign.Center
        tr1.Cells.Add(td13)

        myTable.Rows.Add(tr1)

        Dim sqlStr As String = ""

        Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
        myConn.Open()

        Dim myCommand As New SqlCommand

        sqlStr = "Select RegNo, SName,ClassRollNo From vw_Student Where SchoolName='" & cboSchoolName.Text & "' and ClassName='" & cboClass.Text & "' AND SecName='" & cboSection.Text & "' AND ASID=" & Request.Cookies("ASID").Value & " AND StatusName='" & cboStatus.Text & "' Order By Convert(int,ClassRollNo)"
        myCommand.CommandText = sqlStr
        myCommand.Connection = myConn
        Dim myReader As SqlDataReader = myCommand.ExecuteReader
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
            tdx2.HorizontalAlign = HorizontalAlign.Left
            trx.Cells.Add(tdx2)

            Dim txtMax As New TextBox()
            txtMax.ID = "txtRollNo" & myTxtBoxNumber
            txtMax.Attributes.Add("onkeypress", "return tabE(this,event)")
            txtMax.Width = 70
            txtMax.TabIndex = myTxtBoxNumber
            Try
                txtMax.Text = myReader(2)
            Catch ex As Exception
                txtMax.Text = 0
            End Try

            Dim tdx3 As New TableCell
            tdx3.Controls.Add(txtMax)
            tdx3.HorizontalAlign = HorizontalAlign.Center
            trx.Cells.Add(tdx3)


            myTable.Rows.Add(trx)
            myTxtBoxNumber += 1
        End While
        myReader.Close()


        sqlStr = "select ClassRollNo ,count(ClassRollNo) from vw_student Where SchoolName='" & cboSchoolName.Text & "' and ClassName='" & cboClass.Text & "' AND SecName='" & cboSection.Text & "' AND ASID=" & Request.Cookies("ASID").Value & " AND StatusName='" & cboStatus.Text & "' group by ClassRollNo having count(ClassRollNo) >1"
        myCommand.CommandText = sqlStr
        myCommand.Connection = myConn
        Dim myReadertmp As SqlDataReader = myCommand.ExecuteReader

        While myReadertmp.Read
            For i As Integer = 1 To myTable.Rows.Count - 1
                Dim ClassRollNo As String = CType(myTable.FindControl("txtRollNo" & i), TextBox).Text
                If ClassRollNo = myReadertmp("ClassRollNo") Then
                    myTable.Rows(i).BackColor = ColorTranslator.FromHtml("#FAA6A6")
                End If
            Next
        End While
        myReadertmp.Close()


        myCommand.Dispose()
        myConn.Dispose()
        myTable.EnableViewState = True
        ViewState("myTable") = True

    End Sub

    Private Function ValidateNonNumericRecords() As Integer  '1-Max Marks    2-Marks obtained
        Dim i As Integer = 0, rv As Integer = 0
        For i = 1 To myTable.Rows.Count - 1

            If IsNumeric(CType(myTable.FindControl("txtRollNo" & i), TextBox).Text) = False Then
                rv = i
                Exit For
            End If

        Next
        Return rv
    End Function
    Private Function GetSID(ByVal myAdminNo As String) As Integer
        Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
        myConn.Open()

        Dim sqlStr As String = "Select Max(SID) From Student Where RegNo='" & myAdminNo & "' AND ASID=" & Request.Cookies("ASID").Value
        Dim myCommand As New SqlCommand(sqlStr, myConn)
        Dim rv As Integer = 0
        rv = myCommand.ExecuteScalar
        myCommand.Dispose()
        myConn.Dispose()
        Return rv
    End Function

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
        lblStatus.Text = ""

        CreateTable()
        btnassign.Visible = True
        btnprint.Visible = True
        lblAssignRollNo.Visible = True
        cboassignrollno.Visible = True
        lblClassName.Text = " Roll No List Class: " & cboClass.Text & " - " & cboSection.Text
    End Sub

    Protected Sub btnAsignNoAlphab_Click(sender As Object, e As EventArgs) Handles btnAsignNoAlphab.Click
        AssignRollNoAlphabetically()
    End Sub
    Private Sub AssignRollNoAlphabetically()
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

        Dim ds As New DataSet
        Dim SID As Integer = 0, i As Integer = 0
        Dim sqlStr As String = "Select SID, SName,ClassRollNo From vw_Student Where SchoolName='" & cboSchoolName.Text & "' and ClassName='" & cboClass.Text & "' AND SecName='" & cboSection.Text & "' AND ASID=" & Request.Cookies("ASID").Value & " AND StatusName='" & cboStatus.Text & "' Order By SName"
        ds = ExecuteQuery_DataSet(sqlStr, "tbl")

        For Each Row As DataRow In ds.Tables(0).Rows
            Try
                SID = Row("SID")
                i += 1
            Catch ex As Exception

            End Try
            sqlStr = "Update Student Set ClassRollNo='" & i & "' Where SID=" & SID
            ExecuteQuery_Update(sqlStr)
        Next
        CreateTable()
        lblStatus.Text = "Roll No Assignment complete."
    End Sub

   
    Public Function ExecuteQuery_DataSet(ByVal strQuery As String, ByVal cTableName As String) As DataSet
        Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        Dim con As New System.Data.SqlClient.SqlConnection(myConnStr)
        Dim SqlCmd = New SqlCommand(strQuery, con)

        Dim da As New SqlDataAdapter()
        da.SelectCommand = SqlCmd
        If con.State <> ConnectionState.Open Then
            con.Open()
        End If
        Dim ds As New DataSet()
        Try
            da.Fill(ds, cTableName)
        Catch ex As Exception
            'HttpContext.Current.Response.Write(" Error Web Msql Error ExecuteQuery : " );
            Throw (ex)
        Finally
            SqlCmd.Connection.Close()
            SqlCmd.Dispose()
            con.Close()
        End Try
        Return ds
    End Function

    Private Sub btnAssignRollAlGen_Click(sender As Object, e As EventArgs) Handles btnAssignRollAlGen.Click
        AssignRollNoGenderWise()
    End Sub

    Private Sub AssignRollNoGenderWise()
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

        Dim ds As New DataSet
        Dim SID As Integer = 0, i As Integer = 0
        Dim sqlStr As String = "Select SID, SName,ClassRollNo From vw_Student Where SchoolName='" & cboSchoolName.Text & "' and ClassName='" & cboClass.Text & "' AND SecName='" & cboSection.Text & "' AND ASID=" & Request.Cookies("ASID").Value & " AND StatusName='" & cboStatus.Text & "' Order By gender desc,sname"
        ds = ExecuteQuery_DataSet(sqlStr, "tbl")

        For Each Row As DataRow In ds.Tables(0).Rows
            Try
                SID = Row("SID")
                i += 1
            Catch ex As Exception

            End Try
            sqlStr = "Update Student Set ClassRollNo='" & i & "' Where SID=" & SID
            ExecuteQuery_Update(sqlStr)
        Next
        CreateTable()

        lblStatus.Text = "Roll No Assignment complete."
    End Sub

    Protected Sub btnassign_Click(sender As Object, e As EventArgs) Handles btnassign.Click
        If cboassignrollno.Text = "Gender wise" Then
            AssignRollNoGenderWise()
        ElseIf cboassignrollno.Text = "Alphabetically" Then
            AssignRollNoAlphabetically()
        ElseIf cboassignrollno.Text = "Manually" Then
            AssignRollNoMannually()
        End If
    End Sub

    Protected Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click
        btnprint.Attributes.Add("onClick", "print();")
    End Sub

    Protected Sub cboSchoolName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSchoolName.SelectedIndexChanged
        LoadMasterInfo(2, cboClass, cboSchoolName.Text)
        cboSchoolName.Focus()
    End Sub
End Class