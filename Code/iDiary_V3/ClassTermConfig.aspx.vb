Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary_Fee.CLS_iDiary_Fee
Public Class ClassTermConfig
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            InitControls()
        End If
    End Sub

    Private Sub InitControls()
        LoadMasterInfo(2, cboClass)
        LoadMasterInfo(3, cboSection)
        'LoadMasterInfo(62, cboSubSection)
        'LoadFeeTerms(cboTermNo)
        LoadTerms()
        rdoDeSelectAll.Checked = True
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If cboClass.SelectedIndex = 0 Then
            lblStatus.Text = "Select a Class..."
            cboClass.Focus()
            Exit Sub
        End If
        If cboSection.SelectedIndex = 0 Then
            lblStatus.Text = "Select a Section..."
            cboSection.Focus()
            Exit Sub
        End If
        Dim flag As Boolean = False
        For j = 0 To chkTermNo.Items.Count - 1
            If chkTermNo.Items.Item(j).Selected = True Then
                flag = True
            End If
        Next
        If flag = False Then
            lblStatus.Text = "Please Select Atlease one term..."
            Exit Sub
        End If
        Dim ClassID As Integer = FindMasterID(2, cboClass.Text)
        Dim SecID As Integer = 0
        'FindSectionID(cboClass.Text, cboSection.Text)
        'Dim TermId As String = LoadTermID(cboTermNo.Text)

        Dim sqlStr As String = ""
        Dim i As Integer = 0

       
       
       

        

        sqlStr = "Delete from ClassTermConfig where ClassID='" & ClassID & "' And SecID='" & SecID & "'"
        
        
        ExecuteQuery_Update(SqlStr)
   

        For i = 0 To chkTermNo.Items.Count - 1
            If chkTermNo.Items.Item(i).Selected = True Then

                Dim TermId As String = 0
                'LoadTermID(chkTermNo.Items.Item(i).Text)
                'myConn.Open()

                sqlStr = "Insert into ClassTermConfig Values(" & _
                Request.Cookies("ASID").Value & "," & _
                ClassID & "," & _
                SecID & "," & _
                TermId & ")"

                
                
                ExecuteQuery_Update(SqlStr)
            End If
        Next
        
        
        

        lblStatus.Text = "Configured Successfully..."
        For i = 0 To chkTermNo.Items.Count - 1
            chkTermNo.Items.Item(i).Selected = False
        Next
        cboSection.SelectedIndex = 0
    End Sub

    Protected Sub cboSection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSection.SelectedIndexChanged
        lblStatus.Text = ""
        Dim i As Integer = 0
        For i = 0 To chkTermNo.Items.Count - 1
            chkTermNo.Items.Item(i).Selected = False
        Next
       
       
       

        Dim sqlStr As String = "Select TermNo From vw_ClassTermConfig where ClassName='" & cboClass.Text & "' And SecName='" & cboSection.Text & "' and ASID=" & Request.Cookies("ASID").Value
        
        
        
        Dim myReader As System.Data.SqlClient.SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        'chkTermNo.Items.Clear()
        While myReader.Read
            Dim TermNo As String = myReader(0)
            chkTermNo.Items.FindByText(TermNo).Selected = True
        End While
        myReader.Close()
        
        
    End Sub
    Protected Sub cboClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboClass.SelectedIndexChanged
        'LoadSection(cboClass.Text, cboSection)
        lblStatus.Text = ""
        rdoDeSelectAll.Checked = True
        Dim i As Integer = 0
        For i = 0 To chkTermNo.Items.Count - 1
            chkTermNo.Items.Item(i).Selected = False
        Next
    End Sub
    Protected Sub rdoSelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles rdoSelectAll.CheckedChanged
        Dim i As Integer = 0
        For i = 0 To chkTermNo.Items.Count - 1
            chkTermNo.Items.Item(i).Selected = True
        Next
    End Sub

    Protected Sub rdoDeSelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles rdoDeSelectAll.CheckedChanged
        Dim i As Integer = 0
        For i = 0 To chkTermNo.Items.Count - 1
            chkTermNo.Items.Item(i).Selected = False
        Next
    End Sub
    Private Sub LoadTerms()

       
       
       

        Dim sqlStr As String = "Select TermNo From TermMaster Order By DisplayOrder"

        
        
        
        Dim myReader As System.Data.SqlClient.SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        chkTermNo.Items.Clear()
        While myReader.Read
            chkTermNo.Items.Add(myReader(0))
        End While
        myReader.Close()
        
        
    End Sub
End Class