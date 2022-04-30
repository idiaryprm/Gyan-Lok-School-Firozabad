Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary_Fee.CLS_iDiary_Fee

Public Class StudentFatherPromotion
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Fee") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
            End If
        Catch ex As Exception
            Response.Redirect("~/Login.aspx")
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
    End Sub

    Private Sub InitControls()

        'LoadMasterInfo(60, cboFeeGroup)
        'LoadMasterInfo(61, cboCategoryArmy)
        'cboSection.Items.Clear()
        txtDOP.Text = Now.Date.ToString("dd/MM/yyyy")
        LoadMasterInfo(61, cboOldArmyCat)
        LoadMasterInfo(61, cboNewArmyCat)
        txtRegno.Text = ""
        txtName.Text = ""

        'txtd.Text = ""
        txtID.Text = ""
        lblStatus.Text = ""
        'GridView1.DataBind()

        txtRegno.Focus()
    End Sub
    'Protected Sub cboSection_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSection.SelectedIndexChanged
    '    'LoadFeeTerms(cboTermNo, GetFeeGroupID(cboClass.Text))
    '    GridView1.DataBind()
    '    cboTermNo.Focus()
    'End Sub


    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtRegno.Text = "" Or CheckRegNo(txtRegno.Text) = False Then
            lblStatus.Text = "Invalid Reg No...."
            txtRegno.Focus()
            Exit Sub
        End If
        'If cboFeeGroup.SelectedIndex = 0 Then
        '    lblStatus.Text = "Select a Fee Group..."
        '    cboFeeGroup.Focus()
        '    Exit Sub
        'End If
        'If cboCategoryArmy.SelectedIndex = 0 Then
        '    lblStatus.Text = "Select a Category..."
        '    cboCategoryArmy.Focus()
        '    Exit Sub
        'End If
        If cboOldArmyCat.SelectedIndex = 0 Then
            lblStatus.Text = "Select a Old Rank..."
            cboOldArmyCat.Focus()
            Exit Sub
        End If
        If cboNewArmyCat.SelectedIndex = 0 Then
            lblStatus.Text = "Select a Promoted Rank..."
            cboNewArmyCat.Focus()
            Exit Sub
        End If
        Dim PromotionDate As String = ""
        Try
            PromotionDate = txtDOP.Text.Split("/")(2) & "/" & txtDOP.Text.Split("/")(1) & "/" & txtDOP.Text.Split("/")(0)
        Catch ex As Exception
            lblStatus.Text = "Invalid Promotion Date..."
            txtDOP.Focus()
            Exit Sub
        End Try
        If cboTermNo.SelectedIndex = 0 Then
            lblStatus.Text = "Select a Term..."
            cboTermNo.Focus()
            Exit Sub
        End If
        Dim OldArmyCategoryID As Integer = FindMasterID(61, cboOldArmyCat.Text)
        Dim NewArmyCategoryID As Integer = FindMasterID(61, cboNewArmyCat.Text)
        'Dim FeeGroupID As Integer = FindMasterID(60, cboFeeGroup.Text)
        'Dim CatArmyID As Integer = FindMasterID(61, cboCategoryArmy.Text)
        'Dim TermId As String = LoadTermID(cboTermNo.Text, txtFeeGroupID.Text)
        'Dim CSSID As Integer = FindCSSID(cboClass.Text, cboSection.Text, cboSubSection.Text)
        Dim TermId As String = LoadTermID(cboTermNo.Text, txtFeeGroupID.Text)
        Dim sqlStr As String = ""

       
       
       
        
        '  FeeGroupID & "," & _
        'CatArmyID & "," & _
        ';If txtID.Text = "" Then

        sqlStr = "Delete from StudentFatherPromotion where SID=" & Val(txtSID.Text)
        
        
        ExecuteQuery_Update(SqlStr)

        sqlStr = "Insert into StudentFatherPromotion(SID, PromotionDate, OldArmyCategory, PromotedArmyCategory,TermID) Values(" & _
    Val(txtSID.Text) & "," & _
        "'" & PromotionDate & "'," & _
    OldArmyCategoryID & "," & _
    NewArmyCategoryID & "," & _
        TermId & ")"

        
        
        ExecuteQuery_Update(SqlStr)

        
        
        'Else
        InitControls()
        lblStatus.Text = "Promotion has been Configured"
        'End If
    End Sub

   
    'Protected Sub loadFeeConfigID()
    '    Dim sqlStr As String = ""

    '   
    '   
    '   
    '    

    '    sqlStr = "Select * from vw_FeeConfig Where ClassName='" & cboClass.Text & "' and SecName='" & _
    '    cboSection.Text & "' and TermNo='" & cboTermNo.Text & "' and FeeTypeName='" & _
    '    cboFeeTypes.Text & "'"

    '    
    '    

    '    Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
    '    While myReader.Read
    '        ' myCbo.Items.Add(myReader("TermNo"))
    '        txtID.Text = myReader("FeeConfigID")
    '    End While
    '    myReader.Close()
    '    
    '    
    'End Sub

    Public Shared Function getRow(ByVal TermId As String, ByVal FeeTypeId As String, SID As Integer) As Integer
        Dim Count As Integer = 0

        Dim sqlStr As String = "Select Count(*) From FeeConfig where SID='" & SID & "' And TermID='" & TermId & "' And FeeTypeID='" & FeeTypeId & "'"

       
        
       

        
        
        
        Count = ExecuteQuery_ExecuteScalar(SqlStr)

        
        

        If Count = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    'Protected Sub cboCategoryArmy_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCategoryArmy.SelectedIndexChanged
    '    GridView1.DataBind()
    'End Sub
    Private Sub GetFeeGroup(RegNo As String)
        Try
            Dim sqlStr As String = "Select FeeGroupID,FeeGroupName,categoryArmyID,SID,SName,FName,ClassName,SecName,CategoryArmyName From vw_Student where  Regno='" & RegNo & "' and ASID=" & Request.Cookies("ASID").Value & " Order by DisplayOrder"
           
           
           

            
            
            
            Dim FeeGroupId As String = ""
            Dim myReader As System.Data.SqlClient.SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                FeeGroupId = myReader(0).ToString
                'cboGroup.Text = myReader(1).ToString
                txtFeeGroupID.Text = FeeGroupId
                txtCategoryArmyID.Text = myReader(2).ToString
                txtSID.Text = myReader(3).ToString
                lblSName.Text = myReader("SName").ToString
                lblFather.Text = myReader("FName").ToString
                lblClass.Text = myReader("ClassName").ToString & "-" & myReader("SecName").ToString
                cboOldArmyCat.Text = myReader("CategoryArmyName").ToString
                LoadFeeTerms(cboTermNo, FeeGroupId)
            End While
            myReader.Close()

            
            
            'LoadClass(cboClass, FeeGroupId)
            'cboClass.Items.Add("ALL")
            'LoadFeeTerms(cboTermNo, FeeGroupId)
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnNext_Click(sender As Object, e As ImageClickEventArgs) Handles btnNext.Click
        If txtRegno.Text = "" Or CheckRegNo(txtRegno.Text) = False Then
            lblStatus.Text = "Invalid Reg No...."
            txtRegno.Focus()
            Exit Sub
        End If
        GetFeeGroup(txtRegno.Text)
        'GridView1.DataBind()
    End Sub
    Private Function CheckRegNo(Regno As String) As Boolean
       
       
       

        Dim sqlstr As String = ""
        

        sqlstr = "Select Count(*) From vw_student where regno='" & Regno & "'"
        
        
        Dim rv As Integer = 0
        rv = ExecuteQuery_ExecuteScalar(SqlStr)
        
        
        If rv > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Protected Sub btnNameSearch_Click(sender As Object, e As ImageClickEventArgs) Handles btnNameSearch.Click
        SqlDataSource2.SelectCommand = "SELECT RegNo, SName, ClassName, SecName FROM vw_Student WHERE ASID = " & Request.Cookies("ASID").Value & " AND SName Like '%" & txtName.Text & "%'"
        GridView2.DataBind()
        GridView2.Visible = True
    End Sub

    Protected Sub GridView2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView2.SelectedIndexChanged
        txtRegno.Text = GridView2.SelectedRow.Cells(1).Text
        GetFeeGroup(txtRegno.Text)
        GridView2.Visible = False
        'GridView1.DataBind()
    End Sub

    Protected Sub cboTermNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTermNo.SelectedIndexChanged
        lblTerm.Text = LoadFeeTermCaption(LoadTermID(cboTermNo.Text, GetFeeGroupID(cboFeeGroup.Text)))
    End Sub
End Class