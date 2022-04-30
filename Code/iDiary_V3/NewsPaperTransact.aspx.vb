Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Public Class NewPaperTransact
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Request.Cookies("UType").Value.ToString.Contains("Library") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
            'Allow
        Else
            Response.Redirect("AccessDenied.aspx")
        End If
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            InitControls()
        End If
    End Sub

    Private Sub InitControls()
        'Dim ObjLib As New iDiary.clsLibrary
        'ObjLib.LoadNewsPaperAsList(lstNewsPapers)
        'ObjLib.LoadFrequencyAsDropDown(cboFrequency)
        txtDate.Text = Now.Date.ToString("dd/MM/yyyy")
        LoadNewPapers()
        'Dim rv As Integer = ObjLib.GetNewRackID()
        'txtRackID.Text = rv
        lblStatus.Text = ""
        txtDate.Focus()
        'ObjLib = Nothing
    End Sub
    Private Sub LoadNewPapers()

       
        
       

        

        Dim sqlStr As String = "Select NewspaperID, NewsPaperName From NewsPaperMaster"
        
        
        cboNewsPapers.Items.Clear()
        ListPaperID.Items.Clear()

        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            ListPaperID.Items.Add(myReader("NewspaperID"))
            cboNewsPapers.Items.Add(myReader("NewsPaperName"))
        End While
        myReader.Close()

        
        

    End Sub
    'Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
    '    Dim ObjLib As New iDiary.clsLibrary
    '    txtNewsPaperName.Text = ""
    '    Dim rv As Integer = ObjLib.GetNewRackID()
    '    txtRackID.Text = rv
    '    objlib = Nothing
    'End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Trim(txtDate.Text) = "" Then
            lblStatus.Text = "Date is Empty..."
            txtDate.Focus()
            Exit Sub
        End If
        'If Trim(cboFrequency.Text) = "" Then
        '    lblStatus.Text = "News Paper frequency is Empty..."
        '    cboFrequency.Focus()
        '    Exit Sub
        'End If


       
        
       

        
        Dim i As Integer = 0

        For i = 0 To cboNewsPapers.Items.Count - 1
            Dim sqlStr As String = ""
            Dim paperID As Integer = Val(ListPaperID.Items(i).Text)
            sqlStr = "Delete from NewsPaperEntry Where NewsPaperID='" & paperID & "' AND DateIn='" & txtDate.Text.Substring(6, 4) & "/" & txtDate.Text.Substring(3, 2) & "/" & txtDate.Text.Substring(0, 2) & "'"
            
            
            ExecuteQuery_Update(SqlStr)
            sqlStr = "Insert into NewsPaperEntry (NewsPaperID, DateIn,Present) Values (" & _
            paperID & "," & _
            "'" & txtDate.Text.Substring(6, 4) & "/" & txtDate.Text.Substring(3, 2) & "/" & txtDate.Text.Substring(0, 2) & "',"
            If cboNewsPapers.Items(i).Selected = True Then
                sqlStr &= "1)"
            Else
                sqlStr &= "0)"
            End If

            
            
            ExecuteQuery_Update(SqlStr)

           
        Next

        InitControls()
        lblStatus.Text = "Data is Saved"
    End Sub

    'Protected Sub lstPub_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstNewsPapers.SelectedIndexChanged
    '    Dim ObjLib As New iDiary.clsLibrary
    '    txtDate.Text = lstNewsPapers.Text
    '    txtNewsPaperID.Text = ObjLib.GetNewsPaperID(lstNewsPapers.Text)
    '    cboFrequency.Text = ObjLib.GetNewsPaperFrequency(lstNewsPapers.Text)
    '    lblStatus.Text = ""
    '    ObjLib = Nothing
    'End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        If Trim(txtDate.Text) = "" Then
            lblStatus.Text = "Select a News Paper Name to remove..."
            txtDate.Focus()
            Exit Sub
        End If

       
       
       

        Dim sqlStr As String = ""
        

        sqlStr = "Delete From NewsPaperMaster Where NewsPaperID=" & Val(txtNewsPaperID.Text)
        
        
        ExecuteQuery_Update(SqlStr)

        
        

        Dim TempName As String = txtDate.Text

        InitControls()

        lblStatus.Text = "News Paper: " & TempName & " removed successfully..."
    End Sub

    Protected Sub chkAll_CheckedChanged(sender As Object, e As EventArgs) Handles chkAll.CheckedChanged
        Dim i As Integer = 0
        For i = 0 To cboNewsPapers.Items.Count - 1
            If chkAll.Checked = True Then
                cboNewsPapers.Items(i).Selected = True
            Else
                cboNewsPapers.Items(i).Selected = False
            End If
        Next
    End Sub
End Class