Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class FeeTermMaster
    Inherits System.Web.UI.Page


    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Fee") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
            End If
        Catch ex As Exception
            response.redirect("~/Login.aspx")
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
    End Sub

    Private Sub InitControls()
        txtTermName.Text = ""
        txtId.Text = ""
        LoadMasterInfo(60, cboGroup)
        LoadMonths(chkMonth)
        LoadTermsList(lstTerm, 0)
        'LoadTermsList(lstTerm)
        'cboTermNo.Text = ""
        'txtDispOrder.Text = ""
        cboTermNo.SelectedIndex = lstTerm.Items.Count
        txtDispOrder.Text = lstTerm.Items.Count + 1
        cboGroup.Focus()
    End Sub
    Private Sub LoadTermsList(ByRef myLst As ListBox, ClassGroupID As Integer)
        Dim sqlStr As String = ""
        sqlStr = "Select TermName From TermMaster where FeeGroupID=" & ClassGroupID & " order by DisplayOrder"
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        myLst.Items.Clear()
        While myReader.Read
            myLst.Items.Add(myReader(0))
        End While
        myReader.Close()
    End Sub
    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If cboGroup.Text = "" Then
            lblStatus.Text = "Please select Group"
            cboGroup.Focus()
            Exit Sub
        End If
        If cboTermNo.Text = "" Then
            lblStatus.Text = "Please Enter Term No"
            cboTermNo.Focus()
            Exit Sub
        End If
        'If txtTermName.Text = "" Then
        '    lblStatus.Text = "Please Enter Term Name"
        '    txtTermName.Focus()
        '    Exit Sub
        'End If
        If txtDispOrder.Text = "" Then
            lblStatus.Text = "Please Enter Display Order"
            txtDispOrder.Focus()
            Exit Sub
        End If
        Dim MonthListName As String = ""
        Dim MonthList As String = ""
        For k = 0 To chkMonth.Items.Count - 1
            If chkMonth.Items(k).Selected = True Then
                MonthListName += chkMonth.Items(k).Text & "-"
                If k > 8 Then
                    MonthList += k - 8 & ","
                Else
                    MonthList += k + 4 & ","
                End If
            End If
        Next
        If MonthList = "" Then
            lblStatus.Text = "Please Select atleast one Month"
            'txtDispOrder.Focus()
            Exit Sub
        End If

        MonthList = MonthList.Substring(0, MonthList.Length - 1)
        MonthListName = MonthListName.Substring(0, MonthListName.Length - 1)

        Dim tmpMonthListName() As String = MonthListName.Split("-")
        MonthListName = tmpMonthListName(0)
        If tmpMonthListName.Count > 1 Then
            Try
                MonthListName += "-" & tmpMonthListName(tmpMonthListName.Count - 1)
            Catch ex As Exception

            End Try
        End If
        
        txtTermName.Text = MonthListName
        Dim sqlStr As String = ""

        Dim FeeGroupId As Integer = FindMasterID(60, cboGroup.Text)

        If txtId.Text = "" Then             'Insert
            If CheckTermNo(cboTermNo.Text, FeeGroupId) = True Then
                lblStatus.Text = "Term No Allready Exist"
                cboTermNo.Focus()
                Exit Sub
            End If
            sqlStr = "Insert into TermMaster Values('" & txtTermName.Text & "', '" & cboTermNo.Text & "' ,'" & txtDispOrder.Text & "'," & FeeGroupId & ",'" & MonthList & "')"
        Else                                'Update
            sqlStr = "Update TermMaster Set TermName='" & txtTermName.Text & "',TermNo='" & cboTermNo.Text & "', DisplayOrder='" & txtDispOrder.Text & "', FeeGroupID='" & FeeGroupId & "',MonthID='" & MonthList & "' Where TermId=" & Val(txtId.Text)
        End If
        ExecuteQuery_Update(sqlStr)

        LoadTermsList(lstTerm, FeeGroupId)
        Dim tmpClassGroupID As String = cboGroup.Text
        InitControls()
        cboGroup.Text = tmpClassGroupID
        LoadTermsList(lstTerm, FeeGroupId)
        cboTermNo.SelectedIndex = lstTerm.Items.Count
        txtDispOrder.Text = lstTerm.Items.Count + 1
    End Sub
    Protected Sub lstTerm_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstTerm.SelectedIndexChanged

        Dim FeeGroupId As Integer = FindMasterID(60, cboGroup.Text)
        Dim MonthList As String = ""
        Dim sqlStr As String = "Select * From TermMaster Where TermName='" & lstTerm.Text & "' and FeeGroupID=" & FeeGroupId & " order by DisplayOrder"
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            txtId.Text = myReader("TermId")
            txtTermName.Text = myReader("TermName")
            cboTermNo.Text = myReader("TermNo")
            txtDispOrder.Text = myReader("DisplayOrder")
            MonthList = myReader("MonthID")
        End While

        Dim MonthListtmp() As String
        MonthListtmp = MonthList.Split(",")

        For k = 0 To chkMonth.Items.Count - 1
            chkMonth.Items(k).Selected = False
        Next
        For k = 0 To chkMonth.Items.Count - 1
            For j = 0 To MonthListtmp.Count - 1
                If k > 8 Then
                    If Val(MonthListtmp(j)) = k - 8 Then
                        chkMonth.Items(k).Selected = True
                    End If
                Else
                    If Val(MonthListtmp(j)) = k + 4 Then
                        chkMonth.Items(k).Selected = True
                    End If
                End If
            Next
        Next
        myReader.Close()
    End Sub
    Private Function CheckTermNo(TermNo As String, FeeGroupID As Integer) As Boolean
        Dim rv As Integer = 0
        Dim sqlStr As String = "Select Count(*) From TermMaster Where TermNo='" & cboTermNo.Text & "' and FeeGroupID= " & FeeGroupID & ""
        rv = ExecuteQuery_ExecuteScalar(sqlStr)
        If rv > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    'Private Function CheckMonthNo(TermNo As String, FeeGroupID As Integer) As Boolean
    '    Dim rv As Integer = 0
    '    Dim sqlStr As String = "Select Count(*) From TermMaster Where TermNo='" & cboTermNo.Text & "' and FeeGroupID= " & FeeGroupID & ""
    '    rv = ExecuteQuery_ExecuteScalar(sqlStr)
    '    If rv > 0 Then
    '        Return True
    '    Else
    '        Return False
    '    End If
    'End Function
    Protected Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        InitControls()
    End Sub

    Protected Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        Dim FinalMessage As String = ""
        Dim sqlStr As String = "Delete From TermMaster Where TermId=" & Val(txtId.Text)
        Try
            ExecuteQuery_Update(sqlStr)
        Catch ex As Exception
            FinalMessage = "Record can not be deleted...Record is being used somewhere else..."
        End Try

        InitControls()
        lblStatus.Text = FinalMessage
    End Sub

    Protected Sub cboGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboGroup.SelectedIndexChanged
        Dim ClassGroupId As Integer = FindMasterID(60, cboGroup.Text)
        LoadTermsList(lstTerm, ClassGroupId)
        cboTermNo.SelectedIndex = lstTerm.Items.Count + 1
        txtDispOrder.Text = lstTerm.Items.Count + 1
    End Sub
End Class