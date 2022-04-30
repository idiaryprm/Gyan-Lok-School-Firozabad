Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary.CLS_iDiary_Exam
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports Microsoft.Reporting.WebForms

Public Class ExamDateSheet
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
        Session("ActiveTab") = 7
        If (Request.Cookies("UType").Value.ToString.Contains("Admin-1") = False And Request.Cookies("UType").Value.ToString.Contains("Student-1") = False) Then
            btnAsignNo.Enabled = False
        End If
        If IsPostBack = False Then
           LoadMasterInfo(71, cboSchoolName, Request.Cookies("SchoolIDs").Value)

        LoadMasterInfo(2, cboClass, cboSchoolName.Text)
        cboClass.Text = ""
        cboSection.Items.Clear()
            gvExamDateSheet.Visible = False
            btnSave.Visible = False
            cboSection.Text = ""
            cboClass.Focus()
            '  GridView1.Visible = False
        Else
        End If
    End Sub


    Protected Sub cboClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboClass.SelectedIndexChanged
        LoadClassSection(cboClass.Text, cboSection)
        cboSection.Items.Add("ALL")
        Dim schoolid = FindMasterID(71, cboSchoolName.Text)
        lblExamGrpID.Text = FindMasterID(114, cboClass.SelectedItem.Text, schoolid)
        ' LoadSubjectGroupExamTermsMajor(cboExamType, 1, lblExamGrpID.Text)
        LoadExamTerms(cboExamType, lblExamGrpID.Text, 0)

        'LoadExamTerms(cboTerm, lblGrpID.Text, 0)
    End Sub
    'Changes By Abhinav
    Public Sub LoadExamTerms(ByRef cblTerm As DropDownList, grpID As Integer, type As Integer)
        'type 0 = major, type 1 = minor, type 2 = both
        cblTerm.Items.Clear()
        Dim sqlStr As String = "Select ExamTermID,ExamTermName From ExamTermMaster Where IsMinor=1"
        sqlStr &= " order by displayorder"
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            cblTerm.Items.Add(New ListItem(myReader(1), myReader(0)))
        End While
        myReader.Close()
    End Sub

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

        If cboExamType.Text = "" Then
            cboExamType.Text = "Invalid Exam Group..."
            cboExamType.Focus()
            Exit Sub
        End If
        Dim schoolid = FindMasterID(71, cboSchoolName.Text)
        lblStatus.Text = ""
        gvExamDateSheet.Visible = True
        'Rajat
        If cboSection.SelectedItem.Text = "ALL" Then
            GVCreateMarksEntry.SelectCommand = "Select Distinct SubjectName,SubjectID,priority,SchoolID From vw_ExamSubjectMapping Where  ClassName='" & cboClass.SelectedItem.Text & "' AND SecName='A' and partofcalculation=1 and SchoolID='" & schoolid & "' AND ASID=" & Request.Cookies("ASID").Value
        Else
            GVCreateMarksEntry.SelectCommand = "Select Distinct SubjectName,SubjectID,priority,SchoolID From vw_ExamSubjectMapping Where  ClassName='" & cboClass.SelectedItem.Text & "' AND SecName='" & cboSection.SelectedItem.Text & "' and partofcalculation=1 and SchoolID='" & schoolid & "' AND ASID=" & Request.Cookies("ASID").Value
        End If

        ' GVCreateMarksEntry.SelectCommand = "Select distinct SubjectName From vw_ExamSubjectMapping Where ClassName='" & cboClass.SelectedItem.Text & "' AND SecName='" & cboSection.SelectedItem.Text & "' and partofcalculation=1 "
        gvExamDateSheet.DataBind()
        btnSave.Visible = True
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim SchoolID As Integer = FindMasterID(71, cboSchoolName.Text)
        Dim ClassID As Integer = FindMasterID(2, cboClass.Text)
        Dim SecID As Integer = 0
        If cboSection.SelectedItem.Text = "ALL" Then
            SecID = 1
        Else
            SecID = FindMasterID(3, cboSection.Text)
        End If
        Dim sqlStr As String = ""
        'Rajat
        If cboSection.SelectedItem.Text = "ALL" Then
            sqlStr = "delete from ExamDateSheet where ClassID=" & ClassID & " and SecID in (1,2,3,4,5,6) and ExamTermID=" & lblExamGrpID.Text & ""

        Else
            sqlStr = "delete from ExamDateSheet where ClassID=" & ClassID & " and SecID=" & SecID & " and ExamTermID=" & lblExamGrpID.Text & ""

        End If
        ExecuteQuery_Update(sqlStr)

        For Each gvr As GridViewRow In gvExamDateSheet.Rows
            Dim Date2 As String = "", Day As String = "", timefrom As String = Now.Date.ToString("hh:mm tt"), timeto As String = Now.Date.ToString("hh:mm tt"), SubjectName As String = ""
            Dim Date1 As Date = Now.Date
            Dim today = Now.Date
            Dim today2 = Now.Date
            Try
                Dim tmpStr As String = DirectCast(gvr.FindControl("txtDate"), TextBox).Text
                Dim newtmpstr = CDate(tmpStr)
                tmpStr = newtmpstr.ToString("dd/MM/yyyy")

                Try
                    Date1 = CDate(tmpStr.Substring(6, 4) & "/" & tmpStr.Substring(3, 2) & "/" & tmpStr.Substring(0, 2))
                    'Date1 = DirectCast(gvr.FindControl("txtDate"), TextBox).Text
                Catch ex As Exception
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Enter Valid Date., " & DirectCast(gvr.FindControl("txtDate"), TextBox).Text & "');", True)
                    Exit Sub
                End Try
                'Try
                '    Day = DirectCast(gvr.FindControl("txtDay"), TextBox).Text
                'Catch ex As Exception
                '    Day = ssss
                'End Try
                Try
                    today = Convert.ToDateTime(DirectCast(gvr.FindControl("txtTime"), TextBox).Text)

                Catch ex As Exception
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Enter Valid Time From.');", True)
                    Exit Sub
                End Try
                Try
                    today2 = Convert.ToDateTime(DirectCast(gvr.FindControl("txtTimeTo"), TextBox).Text)

                Catch ex As Exception
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Enter Valid Time To.');", True)
                    Exit Sub
                End Try
                Try
                    SubjectName = gvExamDateSheet.DataKeys(gvr.RowIndex).Value.ToString()
                Catch ex As Exception
                    SubjectName = ""
                End Try
                If cboSection.SelectedItem.Text = "ALL" Then
                    sqlStr = "insert into ExamDateSheet(ClassID,SecID,Date1,TimeFrom,TimeTo,SubjectName,ExamTermID,SchoolID) values(" & ClassID & "," & 1 & ",'" & Date1.ToString("yyyy/MM/dd") & "','" & today.ToString("hh:mm tt") & "','" & today2.ToString("hh:mm tt") & "','" & SubjectName & "'," & lblExamGrpID.Text & "," & SchoolID & ")"
                    sqlStr += "insert into ExamDateSheet(ClassID,SecID,Date1,TimeFrom,TimeTo,SubjectName,ExamTermID,SchoolID) values(" & ClassID & "," & 2 & ",'" & Date1.ToString("yyyy/MM/dd") & "','" & today.ToString("hh:mm tt") & "','" & today2.ToString("hh:mm tt") & "','" & SubjectName & "'," & lblExamGrpID.Text & "," & SchoolID & ")"
                    sqlStr += "insert into ExamDateSheet(ClassID,SecID,Date1,TimeFrom,TimeTo,SubjectName,ExamTermID,SchoolID) values(" & ClassID & "," & 3 & ",'" & Date1.ToString("yyyy/MM/dd") & "','" & today.ToString("hh:mm tt") & "','" & today2.ToString("hh:mm tt") & "','" & SubjectName & "'," & lblExamGrpID.Text & "," & SchoolID & ")"
                    sqlStr += "insert into ExamDateSheet(ClassID,SecID,Date1,TimeFrom,TimeTo,SubjectName,ExamTermID,SchoolID) values(" & ClassID & "," & 4 & ",'" & Date1.ToString("yyyy/MM/dd") & "','" & today.ToString("hh:mm tt") & "','" & today2.ToString("hh:mm tt") & "','" & SubjectName & "'," & lblExamGrpID.Text & "," & SchoolID & ")"
                    sqlStr += "insert into ExamDateSheet(ClassID,SecID,Date1,TimeFrom,TimeTo,SubjectName,ExamTermID,SchoolID) values(" & ClassID & "," & 5 & ",'" & Date1.ToString("yyyy/MM/dd") & "','" & today.ToString("hh:mm tt") & "','" & today2.ToString("hh:mm tt") & "','" & SubjectName & "'," & lblExamGrpID.Text & "," & SchoolID & ")"
                    sqlStr += "insert into ExamDateSheet(ClassID,SecID,Date1,TimeFrom,TimeTo,SubjectName,ExamTermID,SchoolID) values(" & ClassID & "," & 6 & ",'" & Date1.ToString("yyyy/MM/dd") & "','" & today.ToString("hh:mm tt") & "','" & today2.ToString("hh:mm tt") & "','" & SubjectName & "'," & lblExamGrpID.Text & "," & SchoolID & ")"

                Else
                    sqlStr = "insert into ExamDateSheet(ClassID,SecID,Date1,TimeFrom,TimeTo,SubjectName,ExamTermID,SchoolID) values(" & ClassID & "," & SecID & ",'" & Date1.ToString("yyyy/MM/dd") & "','" & today.ToString("hh:mm tt") & "','" & today2.ToString("hh:mm tt") & "','" & SubjectName & "'," & lblExamGrpID.Text & "," & SchoolID & ")"
                End If
                ExecuteQuery_Update(sqlStr)
            Catch ex As Exception

            End Try
          
        Next
        sqlStr = "select distinct * from ExamDateSheet where ClassID=" & ClassID & " and SecID=" & SecID & " and ExamTermID=" & lblExamGrpID.Text & " and SchoolID='" & SchoolID & "'"
        PrepareReport(sqlStr, "rptExamDateSheet.rdlc", "Exam Date Sheet For Class :" & cboClass.SelectedItem.Text & "-" & cboSection.SelectedItem.Text)
    End Sub

    Private Sub PrepareReport(ByVal sqlString As String, ByVal reportName As String, MyHeader As String, Optional rptParam As String = "")
        Dim SchoolID As Integer = FindMasterID(71, cboSchoolName.Text)
        Dim sqlStr As String = ""
        '................................................vikash..........17/06/2016.........................................
        sqlStr = "Select * from Params"
        Dim SchoolName As String = ""
        Dim SchoolAddress As String = ""
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)

        While myReader.Read
            SchoolName = myReader("SchoolName")
            SchoolAddress = myReader("SchoolDetails")
        End While
        myReader.Close()
        SchoolName = FindSchoolDetails1(SchoolID, 0)
        SchoolAddress = FindSchoolDetails1(SchoolID, 1)
        '.........................................................................................................................
        Dim termname As String = cboExamType.SelectedItem.Text
        If termname = "TERM 1" Then
            termname = "MID TERM EXAM"
        Else
            termname = "FINAL EXAM"
        End If

        Dim ClassHeader As String = cboClass.Text & " - " & cboSection.Text & " For Term " & termname
        Dim ds As New DataSet
        ds = ExecuteQuery_DataSet(sqlString, "tbl")

        Dim rds As ReportDataSource = New ReportDataSource()
        rds.Name = "DataSet1" ' Change to what you will be using when creating an objectdatasource
        rds.Value = ds.Tables(0)

        Dim rptReport As String = "Exam_Reports/"
        With ReportViewer1   ' Name of the report control on the form
            .Reset()
            .ProcessingMode = ProcessingMode.Local
            .LocalReport.DataSources.Clear()
            .Visible = True
            .LocalReport.ReportPath = rptReport & reportName
            .LocalReport.DataSources.Add(rds)
            .LocalReport.EnableExternalImages = True
        End With
        Dim params(2) As Microsoft.Reporting.WebForms.ReportParameter
        params(0) = New Microsoft.Reporting.WebForms.ReportParameter("SchoolName", SchoolName, True)
        params(1) = New Microsoft.Reporting.WebForms.ReportParameter("SchoolAddress", SchoolAddress, True)
        params(2) = New Microsoft.Reporting.WebForms.ReportParameter("ClassHeader", ClassHeader, True)
        'params(3) = New Microsoft.Reporting.WebForms.ReportParameter("MyHeader", MyHeader, True)
        'params(4) = New Microsoft.Reporting.WebForms.ReportParameter("ImagePath", Server.MapPath("~/images/SchoolLogo.png"), Visible)
        'params(5) = New Microsoft.Reporting.WebForms.ReportParameter("ImageSign", Server.MapPath("~/images/signature.png"), Visible)
        'params(6) = New Microsoft.Reporting.WebForms.ReportParameter("UserName", Request.Cookies("UID").Value, Visible)
        'params(7) = New Microsoft.Reporting.WebForms.ReportParameter("rptParam", rptParam, Visible)
        Me.ReportViewer1.LocalReport.SetParameters(params)

        ReportViewer1.Visible = True
        ReportViewer1.LocalReport.Refresh()


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

    Protected Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click
        btnprint.Attributes.Add("onClick", "print();")
    End Sub

    Protected Sub cboSchool_SelectedIndexChanged(sender As Object, e As EventArgs)
           LoadMasterInfo(2, cboClass, cboSchoolName.Text)
        cboClass.Text = ""
        cboSection.Items.Clear()
      
        cboSchoolName.Focus()
    
      
    End Sub
End Class