Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class ExamConfig
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            sdsExamTerm.SelectCommand = "SELECT [ExamTermID], [ExamTermName] FROM [ExamTermMaster] "
            cblExamTerms.DataBind()
            fillPermission()
            chkAll.Checked = False
            chkAllTermGrp5.Checked = False
            cblTermsGrp5.Items.Add("1")
            cblTermsGrp5.Items.Add("2")
            fillPermissionForGrp5()
        End If
        
    End Sub

    Private Sub fillPermission()
        For i = 0 To cblExamTerms.Items.Count - 1
            If IsPermissionAllowed(cblExamTerms.Items(i).Value, "ExamConfiguration") Then
                cblExamTerms.Items(i).Selected = True
            Else
                cblExamTerms.Items(i).Selected = False
            End If
        Next
    End Sub

    Private Function IsPermissionAllowed(ByVal termID As Integer, ByVal TableName As String) As Boolean
       
        Dim rv As Integer = 0

        Dim sqlStr As String = "Select top(1) isLocked from " & TableName & " Where ExamTermID=" & termID & " AND ASID=" & Request.Cookies("ASID").Value
       
        Try
            rv = ExecuteQuery_ExecuteScalar(sqlStr)
        Catch ex As Exception
            rv = 0
        End Try
       
        If rv = 1 Then
            Return True
        Else
            Return False
        End If
    End Function

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
       
        Dim rv As String = -1

        Dim sqlStr As String = "", log0 As String = "", val1 As Integer = 0
        For i = 0 To cblExamTerms.Items.Count - 1
            log0 = "Exam Configuration Updated for Term " & cblExamTerms.Items(i).Text
            sqlStr = "Select isLocked from ExamConfiguration Where ASID=" & Request.Cookies("ASID").Value & " AND ExamTermID=" & cblExamTerms.Items(i).Value
            
            Try
                rv = ExecuteQuery_ExecuteScalar(sqlStr)
            Catch ex As Exception
                rv = -1
            End Try
            If IsNothing(rv) Then rv = -1
            If rv = -1 Then
                sqlStr = "Insert Into ExamConfiguration(ASID,ExamTermID,isLocked,createdBy) Values (" & Request.Cookies("ASID").Value & "," & cblExamTerms.Items(i).Value & ","
                If cblExamTerms.Items(i).Selected = True Then
                    sqlStr &= "1,"
                Else
                    sqlStr &= "0,"
                End If
                sqlStr &= "'" & Request.Cookies("UID").Value & "')"
            Else
                sqlStr = "Update ExamConfiguration Set "
                If cblExamTerms.Items(i).Selected = True Then
                    sqlStr &= "isLocked=1"
                    val1 = 1
                Else
                    sqlStr &= "isLocked=0"
                    val1 = 0
                End If
                sqlStr &= " Where ASID=" & Request.Cookies("ASID").Value & " AND ExamTermID=" & cblExamTerms.Items(i).Value

            End If
            If rv = val1 Then
            Else
                If rv = -1 Then
                    log0 &= " changed from None to "
                ElseIf rv = 0 Then
                    log0 &= " changed from Not Locked to "
                Else
                    log0 &= " changed from Locked to "
                End If
                If val1 = 0 Then
                    log0 &= " Not Locked. "
                Else
                    log0 &= " Locked. "
                End If
                log0 &= " For the Session " & Request.Cookies("ASName").Value
                Save_Log("EXAM PERMISSIONS", log0)
            End If
            ExecuteQuery_Update(sqlStr)

        Next
      
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Configuration have been saved successfully...');", True)
    End Sub
    Private Sub Save_Log(ByVal type As String, log0 As String)
        
        Dim log1 As String = log0
        Dim sqlStr As String = ""
        
        sqlStr = "Insert Into Event_log(logTime,EventType,Details,UserID,Visible) Values('" & System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "','" & type & "','" & log1 & "','" & Request.Cookies("UserID").Value & "','1')"
        ExecuteQuery_Update(sqlStr)

    End Sub

    Protected Sub chkAll_CheckedChanged(sender As Object, e As EventArgs) Handles chkAll.CheckedChanged
        For k = 0 To cblExamTerms.Items.Count - 1
            If chkAll.Checked = True Then
                cblExamTerms.Items(k).Selected = True
            Else
                cblExamTerms.Items(k).Selected = False
            End If
        Next
    End Sub

    Protected Sub btnProceed_Click(sender As Object, e As EventArgs) Handles btnProceed.Click
        
        Dim rv As String = -1

        Dim sqlStr As String = "", log0 As String = "", val1 As Integer = 0
        For i = 0 To cblTermsGrp5.Items.Count - 1
            log0 = "Exam Configuration Updated for Term " & cblTermsGrp5.Items(i).Text
            sqlStr = "Select isLocked from ExamConfiguration_Grp5 Where ASID=" & Request.Cookies("ASID").Value & " AND ExamTermID=" & cblTermsGrp5.Items(i).Value

            Try
                rv = ExecuteQuery_ExecuteScalar(sqlStr)
            Catch ex As Exception
                rv = -1
            End Try
            If IsNothing(rv) Then rv = -1
            If rv = -1 Then
                sqlStr = "Insert Into ExamConfiguration_Grp5 (ASID,ExamTermID,isLocked,createdBy) Values (" & Request.Cookies("ASID").Value & "," & cblTermsGrp5.Items(i).Value & ","
                If cblTermsGrp5.Items(i).Selected = True Then
                    sqlStr &= "1,"
                Else
                    sqlStr &= "0,"
                End If
                sqlStr &= "'" & Request.Cookies("EmpID").Value & "')"
            Else
                sqlStr = "Update ExamConfiguration_Grp5 Set "
                If cblTermsGrp5.Items(i).Selected = True Then
                    sqlStr &= "isLocked=1"
                    val1 = 1
                Else
                    sqlStr &= "isLocked=0"
                    val1 = 0
                End If
                sqlStr &= " Where ASID=" & Request.Cookies("ASID").Value & " AND ExamTermID=" & cblTermsGrp5.Items(i).Value

            End If
            If rv = val1 Then
            Else
                If rv = -1 Then
                    log0 &= " changed from None to "
                ElseIf rv = 0 Then
                    log0 &= " changed from Not Locked to "
                Else
                    log0 &= " changed from Locked to "
                End If
                If val1 = 0 Then
                    log0 &= " Not Locked. "
                Else
                    log0 &= " Locked. "
                End If
                log0 &= " For the Session " & Session("ASName")
                Save_Log("EXAM PERMISSIONS", log0)
            End If
            ExecuteQuery_Update(sqlStr)

        Next
        
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Configuration have been saved successfully...');", True)
    End Sub

    Protected Sub chkAllTermGrp5_CheckedChanged(sender As Object, e As EventArgs) Handles chkAllTermGrp5.CheckedChanged
        For k = 0 To cblTermsGrp5.Items.Count - 1
            If chkAllTermGrp5.Checked = True Then
                cblTermsGrp5.Items(k).Selected = True
            Else
                cblTermsGrp5.Items(k).Selected = False
            End If
        Next
    End Sub
    Private Sub fillPermissionForGrp5()
        For i = 0 To cblTermsGrp5.Items.Count - 1
            If IsPermissionAllowed(cblTermsGrp5.Items(i).Value, "ExamConfiguration_Grp5") Then
                cblTermsGrp5.Items(i).Selected = True
            Else
                cblTermsGrp5.Items(i).Selected = False
            End If
        Next
    End Sub
End Class