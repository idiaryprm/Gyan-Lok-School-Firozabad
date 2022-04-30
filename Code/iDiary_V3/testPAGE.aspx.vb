Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WebForms
Imports iDiary_V3.iDiary.CLS_idiary

Public Class testPAGE
    Inherits System.Web.UI.Page

   
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            Dim TheFile As FileInfo = New FileInfo(MapPath("./Photos") & "\" & txtOldFile.Text)
            If TheFile.Exists Then
                File.Move(MapPath("./Photos") & "\" & txtOldFile.Text, MapPath("./Photos") & "\" & txtNewFile.Text)
            Else
                Throw New FileNotFoundException()
            End If

        Catch ex As FileNotFoundException
            lblStatus.Text += ex.Message
        Catch ex As Exception
            lblStatus.Text += ex.Message
        End Try
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
      
        Dim sqlStr As String = ""
        ' 
        Dim marks As String = ""
        Dim defaultMark As String = "T1# $T2# "
        Dim myMarksToSave As String = ""
        Dim cnt As Integer = 0
        sqlStr = "select * FROM A001 where AcaStart='2012'"
        Dim StudentID As Integer = 0, HouseID As Integer = 0, AdmissionNo As String = "", CSSID As Integer = 0
        Dim nDS As DataSet = ExecuteQuery_DataSet(sqlStr, "t1")
        Dim tableName As String = ""
        Dim ar As String = ""
        For Each Row As DataRow In nDS.Tables(0).Rows
            ' For i As Integer = Index To Index + 10
            AdmissionNo = Row("AdmissionNo")
            HouseID = Row("HouseID")

            cnt = ExecuteQuery_ExecuteScalar("Select Count(StudentID) from StudentBasicInfo where FormNo='" & AdmissionNo & "'")
            If cnt <= 0 Then
                sqlStr = "INSERT INTO [dbo].[StudentBasicInfo]([ReceiptNo],[FormNo],[SName],[BloodGroupID],[MotherTougueID],[AdmissionDate],[Gender],[RelID],[CasteID],[DOB], [FName], [MName], [GuardianName], [GuardianRelation], [GuardianAddress], [GuardianMobile], [FatherAddress], [District]" & _
                " ,[PINCODE],[PhoneResd],[PhoneOfficeFather],[PhoneOfficeMother],[MobNo],[EmailFather],[EmailMother],[CategoryID],[OnlyChild],[StatusID],[LastSchoolName],[LastSchoolAddress],[FOccID],[FDeptID],[FDesgID],[FIncome]  ,[MOccID],[MDeptID],[MDesgID],[MIncome],[NationalityID])" & _
                " select Distinct AdmissionNo as rctNO,AdmissionNo,SName,bloodgroupid,MotherTongueID,DateOfAdmission ,sex,ReligionID,CasteID,DateOfBirth,FatherName,MotherName,guardianName,relation,Address,LocalGMob,flatno,MailingcityID ,mailingpin,TelephoneNo,FMobile ,MMobile1,TelephoneNo,FEmail,MEmail1, " & _
                "CategoryID,noofchild,StudentStatus,PSName,psaddress,FOccid,FQual,FDesgID,Fincome,MOccID,MQual1,MDesgID,MIncome,NationalityID From A001 where AcaStart='2012' and AdmissionNo='" & AdmissionNo & "'"
                ExecuteQuery_Update(sqlStr)
            Else
                tableName += AdmissionNo & ","
            End If
            Try
                
            Catch ex As Exception

            End Try
            Try
                StudentID = ExecuteQuery_ExecuteScalar("Select StudentID from StudentBasicInfo Where FormNo='" & AdmissionNo & "'")
                CSSID = FindCSSID(Row("ClassName1"), Row("sectionname1"), "")
            Catch ex As Exception

            End Try
            cnt = ExecuteQuery_ExecuteScalar("Select Count(SID) from Student where asid=3 and regno='" & AdmissionNo & "'")
            If cnt <= 0 Then
                sqlStr = "Insert into Student Values(" & StudentID & ",'" & AdmissionNo & "','" & Row("ClassRollNo") & "','" & Row("FEENO") & "','" & CSSID & "','" & HouseID & "','3','0','NA','1','1')"
                ExecuteQuery_Update(sqlStr)
            Else
                tableName += AdmissionNo & ","
            End If
           



        Next
        Dim val As String = tableName
        'PrepareReport("Select SID,Regno,SName,ClassRollNo,ClassName,SecName,Sub01 From vw_grp0_Marks Where ClassName='NUR' And SecName='A' AND StatusName='Active'")
    End Sub
    Private Sub PrepareReport(ByVal sqlString As String)
        'Dim TermID As Integer = 0
        'TermID = FindTermID()
        'Dim Sql As String = "Select * from Group2_ReportCard_1"

        Dim ds As New DataSet
        ds = ExecuteQuery_DataSet(sqlString, "tbl")
        Dim countGrade As List(Of String) = getGradeCount(ds, txtInput.Text)
        Dim arGrade() As String = txtInput.Text.Split(",")

        Dim groups = countGrade.GroupBy(Function(value) value)
        ' lblMsg.Text = groups
        'For i = 0 To arGrade.Count - 1
        '    lblMsg.Text = arGrade(i) & "-" & countGrade.FindAll(
        'Next
        For Each grp In groups
            ListBox1.Items.Add(grp(0) & " - " & grp.Count)
            lblMsg.Text = grp(0) & " - " & grp.Count & "     " & vbNewLine
        Next
    End Sub

    Private Function getGradeCount(ByVal ds As DataSet, ByVal grade As String) As List(Of String)

        Dim gradeLst As New List(Of String)
        Dim countG As Integer = 0
        Dim MaxRows As Integer = ds.Tables(0).Rows.Count
        Dim nDS As DataSet = ds
        Dim gradeF As String = ""
        For Each Row As DataRow In nDS.Tables(0).Rows
            ' For i As Integer = Index To Index + 10
            Dim ar As String = Row(6)
            '  arGrade.
            Try
                gradeF = MarksToGrade(MarkstoShow(ar))
                gradeLst.Add(gradeF)
                If gradeF = grade Then
                    countG = countG + 1
                End If
                'Row(6) = MarkstoShow(ar)
                'ar = Row("Sub01")
            Catch ex As Exception
                Row(6) = 0
            End Try
            'Row(i) = ar(0) & "/" & ar(2)
            'Next
        Next
        Return gradeLst
    End Function

    Private Function MarkstoShow(ByVal marks As String) As String
        Dim outMarks As String = ""
        Dim termAr() As String = marks.Split("$")
        For i As Integer = 0 To termAr.Count - 1
            If termAr(i).Contains("FA1_1") Then
                outMarks = termAr(i).Split("#")(1)
                Exit For
            End If
            ' outMarks &= marks(i) & "$"
        Next

        Return outMarks
    End Function



    Public Function MarksToGrade(ByVal myMarks As String) As String
        'Perform Marks-Grade Mapping for Group-3 (IX-X) Academic 
        Dim myEnteredMarks As Double = -1
        Try
            Dim ar() As String = myMarks.Split("/")
            myEnteredMarks = (Convert.ToDecimal(ar(0)) / Convert.ToDecimal(ar(1))) * 100
        Catch ex As Exception

        End Try

        Dim myGrade As String = ""
        If myEnteredMarks >= 91 And myEnteredMarks <= 100 Then myGrade = "A1"
        If myEnteredMarks >= 81 And myEnteredMarks <= 90.99 Then myGrade = "A2"
        If myEnteredMarks >= 71 And myEnteredMarks <= 80.99 Then myGrade = "B1"
        If myEnteredMarks >= 61 And myEnteredMarks <= 70.99 Then myGrade = "B2"
        If myEnteredMarks >= 51 And myEnteredMarks <= 60.99 Then myGrade = "C1"
        If myEnteredMarks >= 41 And myEnteredMarks <= 50.99 Then myGrade = "C2"
        If myEnteredMarks >= 33 And myEnteredMarks <= 40.99 Then myGrade = "D"
        If myEnteredMarks >= 21 And myEnteredMarks <= 32.99 Then myGrade = "E1"
        If myEnteredMarks <= 20 Then myGrade = "E2"

        Return myGrade
    End Function

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim FILE_NAME As String = "E:\TB.txt"
        Dim line1 As String = ""

        Dim objWriter As New System.IO.StreamWriter(FILE_NAME, True)
        'objWriter.NewLine = True

        line1 = TextBox1.Text
        Dim ar() As String = line1.Split("#")
        For i As Integer = 0 To ar.Length - 1
            line1 = "  Dim " & ar(i) & " As DbSyncTableDescription = SqlSyncDescriptionBuilder.GetDescriptionForTable(""" & ar(i) & """, serverconn)"
            line1 &= vbNewLine & " scopeDesc.Tables.Add(" & ar(i) & ")" & vbNewLine
            objWriter.WriteLine(line1)
        Next



        'objWriter.Write(TextBox1.Text)

        objWriter.Close()
    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

       
       
       
        Dim sqlStr As String = ""
        
        Dim marks As String = ""
        Dim defaultMark As String = "T1# $T2# "
        Dim myMarksToSave As String = ""

        sqlStr = "Select SID,Attendance from " & TextBox1.Text
        '
        '
        'Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        'While myReader.Read
        '    marks = myReader(0)
        '    myMarksToSave = marks.Split("$")(1).Split("#")(1)
        'End While
        Dim nDS As DataSet = ExecuteQuery_DataSet(sqlStr, "t1")
        Dim SID As Integer = 0
        Dim ar As String = ""
        For Each Row As DataRow In nDS.Tables(0).Rows
            ' For i As Integer = Index To Index + 10


            Try
                SID = Row(0)
                marks = Row(1)
                myMarksToSave = marks.Split("$")(1).Split("#")(1)
            Catch ex As Exception
                marks = ""
            End Try
            If Trim(myMarksToSave) = "" Then
            Else
                sqlStr = "Update  " & TextBox1.Text & " Set Attendance='T1#" & myMarksToSave & "$T2# ' Where SID=" & SID
                
                
                ExecuteQuery_Update(SqlStr)
            End If

        Next

        
        
    End Sub

    Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        PrepareReport()
    End Sub
    Private Sub PrepareReport()
        Dim sqlStr As String = ""
        sqlStr = "select AccNO as barcode ,AccNO as barcode ,AccNO as barcode from Bookmaster"
        Dim ds As New DataSet
        ds = ExecuteQuery_DataSet(sqlStr, "tbl")
        Dim rds As ReportDataSource = New ReportDataSource()
        rds.Name = "DataSet1" ' Change to what you will be using when creating an objectdatasource
        rds.Value = ds.Tables(0)
        With ReportViewer1   ' Name of the report control on the form
            .Reset()
            .ProcessingMode = ProcessingMode.Local
            .LocalReport.DataSources.Clear()
            .Visible = True
            .LocalReport.ReportPath = "rptLibraryAccno.rdlc"
            .LocalReport.DataSources.Add(rds)
        End With
        'Dim FromDate1 As String = Val(txtFromDay.Text) & "/" & Val(txtFromMonth.Text) & "/" & Val(txtFromYear.Text)
        'Dim ToDate1 As String = Val(txtToDay.Text) & "/" & Val(txtToMonth.Text) & "/" & Val(txtToYear.Text)

        'myHeaderText &= txtDateFrom.Text & "-" & txtDateTo.Text
        'Dim params(0) As Microsoft.Reporting.WebForms.ReportParameter
        'params(0) = New Microsoft.Reporting.WebForms.ReportParameter("myHeader", myHeaderText, True)
        'params(1) = New Microsoft.Reporting.WebForms.ReportParameter("LastGrossPay", LastGrossPay, True)
        'params(2) = New Microsoft.Reporting.WebForms.ReportParameter("P2", P2, True)
        'params(3) = New Microsoft.Reporting.WebForms.ReportParameter("P3", P3, True)
        'params(4) = New Microsoft.Reporting.WebForms.ReportParameter("P4", P4, True)
        'params(5) = New Microsoft.Reporting.WebForms.ReportParameter("P5", P5, True)
        'ReportViewer1.LocalReport.SetParameters(params)
        ReportViewer1.Visible = True
        ReportViewer1.LocalReport.Refresh()

    End Sub
 
    Protected Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

       
       
        Dim sqlStr As String = ""
        
        Dim marks As String = ""

        sqlStr = "Select Accno,Pubid from Bookmaster001"
        '
        '
        'Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        'While myReader.Read
        '    marks = myReader(0)
        '    myMarksToSave = marks.Split("$")(1).Split("#")(1)
        'End While
        Dim nDS As DataSet = ExecuteQuery_DataSet(sqlStr, "t1")
        Dim AccNo As String = ""
        Dim pub As String = ""
        Dim pubID As Integer = 0
        Dim ar As String = ""
        For Each Row As DataRow In nDS.Tables(0).Rows
            ' For i As Integer = Index To Index + 10

            Try
                AccNo = Row(0)
                pub = Row(1)
                If IsNumeric(pub) Then
                    Continue For
                End If
                sqlStr = "Select PubID from Publishers Where PubName='" & pub & "'"
                
                
                Try
                    pubID = ExecuteQuery_ExecuteScalar(SqlStr)
                Catch ex As Exception
                    pubID = 0
                End Try
                ' ExecuteQuery_Update(SqlStr)
                '  myMarksToSave = marks.Split("$")(1).Split("#")(1)
            Catch ex As Exception
                'marks = ""
            End Try

            If pubID = 0 Then
                sqlStr = "Select Max(PubID) from Publishers "
                
                
                pubID = ExecuteQuery_ExecuteScalar(SqlStr)
                pubID += 1
                sqlStr = "Insert Into Publishers Values(" & pubID & ",'" & pub & "')"
                
                
                ExecuteQuery_Update(SqlStr)

            End If

            sqlStr = "Update  bookmaster001 Set pubID='" & pubID & "' Where accNo='" & AccNo & "'"
            
            
            ExecuteQuery_Update(SqlStr)

        Next

        
        
    End Sub

    Protected Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("testDB").ToString
       
       
        Dim sqlStr As String = ""
        
        Dim marks As String = ""
        Dim defaultMark As String = "T1# $T2# "
        Dim myMarksToSave As String = ""

        sqlStr = " CREATE PROCEDURE dbo.proc1  AS  Begin set nocount on; "
        Dim startTime As Date = Now
        For i = 1001 To 11001
            sqlStr += "insert into Student Values('" & i & "','" & "DATA" & i & "','" & i & "','" & i & "')   " & vbNewLine
            If i Mod 5 = 0 Then
                sqlStr += "Update Student Set sname='" & i & "' Where SID='" & i & "'" & vbNewLine
            End If
        Next

        sqlStr += " END"
        
        
        ExecuteQuery_Update(SqlStr)


        'myCommand.CommandType = CommandType.StoredProcedure
        'myCommand.CommandText = "proc1"
        ExecuteQuery_Update(SqlStr)

        'Dim cmd As New SqlCommand()

        'cmd.CommandType = CommandType.StoredProcedure
        'cmd.CommandText = "[sp_ItemPackingList]"
        ''cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = dateDate.Text.Trim()
        ''cmd.Parameters.Add("@Thk", SqlDbType.VarChar).Value = txtPendQty.Text.Trim()
        ''cmd.Parameters.Add("@Dia", SqlDbType.VarChar).Value = txtPendQty.Text.Trim()
        'Try
        '    cmd.ExecuteNonQuery()
        '    ' lblMessage.Text = "Record inserted successfully"
        'Catch ex As Exception
        '    Throw ex
        '    ' obj.GetDataTable("[sp_ItemPackingList]", sqlpr1)

        'End Try
        Dim endTime As Date = Now
        Dim rv As String = ""
        rv = " Start Time: " + startTime + vbLf
        rv += " Complete Time: " + endTime + vbLf
        'rv += " time taken : " + endTime - startTime



        lblStatus.Text = rv
        
        
    End Sub

    Protected Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        insertSynLog(txtInput.Text, "I", 1)

    End Sub
    Private Sub insertSynLog(ByVal dbQuery As String, ByVal qrType As String, ByVal userID As Integer)
       
       
       
        Dim sqlStr As String = ""
        


        sqlStr = "Insert into Sync_Log(dbQuery,qType,qrTimeStamp,UserID) Values('" & SQLFixup(dbQuery) & "','" & qrType & "','" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "','" & userID & "')"
        
        
        ExecuteQuery_Update(SqlStr)

        
        
        
    End Sub
    Private Sub StoProc()
       
       
       
        Dim sqlStr As String = ""
        
     
        Try
            '  myCommand.CommandText = "DROP PROCEDURE dbo.Stored_Proc_Log"
            
            ExecuteQuery_Update(SqlStr)
        Catch ex As Exception

        End Try
     
        sqlStr = " CREATE PROCEDURE dbo.Stored_Proc_Log  AS  Begin set nocount on; "
        Dim startTime As Date = Now
        '   myCommand.CommandText = "Select dbQuery from Sync_Log"
        
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            sqlStr += myReader(0) & vbNewLine
        End While
        myReader.Close()
     
        sqlStr += " END"
        
        
        ExecuteQuery_Update(SqlStr)

        
        
        
    End Sub

    Protected Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        StoProc()
    End Sub
End Class