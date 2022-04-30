Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Public Class MagazineMaster
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Request.Cookies("UType").Value.ToString.Contains("Library") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
            'Allow
        Else
            Response.Redirect("../AccessDenied.aspx", False)
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
        If Request.Cookies("UType").Value.ToString.Contains("Library-1") = False And Request.Cookies("UType").Value.ToString.Contains("Admin-1") = False Then
            btnSave.Enabled = False
            btnRemove.Enabled = False
        End If
    End Sub


    Private Sub InitControls()
        Dim ObjLib As New iDiary.clsLibrary
        LoadMonths(cboMonth)
        LoadYears(cboYear)
        'lblLastAccNo.Text = ObjLib.LoadLastAccNo()
        txtAccNo.Text = ObjLib.FindNextAccessionNo("M")
        txtDVDAccNo.Text = ""
        txtTitle.Text = ""
        txtBookCodeNo.Text = ""
        chkDVD.Checked = False
        txtDVDTitle.Text = ""
        lblDVDTitle.Visible = False
        txtDVDTitle.Visible = False
        'ObjLib.LoadAuthorsAsDropDown(cboAuthor)
        ObjLib.LoadPublishersAsDropDown(cboPub)
        ObjLib.LoadFrequencyAsDropDown(cboFrequency)
        ObjLib.LoadVendersAsDropDown(cboVender)
        txtPages.Text = ""
        txtPrice.Text = ""
        ObjLib.LoadBookCategoryAsDropDown(cboBookCat)
        ObjLib.LoadRackAsDropDown(cboRack)
        ObjLib.LoadBookStatusAsDropDown(cboStatus)
        'txtEdition.Text = ""
        txtRemark.Text = ""
        cboIssued.SelectedIndex = 0
        txtNoOfBooks.Enabled = True
        txtNoOfBooks.Focus()
        'lstAuthors.Items.Clear()
        txtNoOfBooks.Text = ""
        txtBillNo.Text = ""
        txtDateNow.Text = Now.Date.ToString("dd/MM/yyyy")
        txtVolume.Text = ""
        txtIssue.Text = ""
        txtISSN.Text = ""

        ObjLib = Nothing
    End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("Index.aspx")
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Trim(txtAccNo.Text).Length <= 0 Then
            lblStatus.Text = "Accession No is BLANK..."
            txtAccNo.Focus()
            Exit Sub
        End If

        If Trim(txtTitle.Text).Length <= 0 Then
            lblStatus.Text = "Magazine Title is BLANK..."
            txtTitle.Focus()
            Exit Sub
        End If

        If Trim(txtPages.Text).Length <= 0 Then
            txtPages.Text = "0"
        End If

        If Trim(txtPrice.Text).Length <= 0 Then
            txtPrice.Text = "0"
        End If
        If Trim(txtNoOfBooks.Text).Length <= 0 Then
            lblStatus.Text = "No. Of Magazines is BLANK..."
            txtNoOfBooks.Focus()
        End If
        If chkDVD.Checked = True And chkDVDTitle.Checked = False And txtDVDTitle.Text = "" Then
            lblStatus.Text = "DVD Title is BLANK..."
            txtDVDTitle.Focus()
            Exit Sub
        End If
        Dim AuthorID As Integer = 0
        Dim VenderID As Integer = 0
        Dim PubId As Integer = 0
        Dim BookCatID As Integer = 0
        Dim BookStatusID As Integer = 0
        Dim RackID As Integer = 0
        Dim FrequencyID As Integer = 0

        Dim FinalMessage As String = ""

        Dim ObjLib As New iDiary.clsLibrary
        PubId = ObjLib.GetPubID(cboPub.Text)
        BookCatID = ObjLib.GetBookCatID(cboBookCat.Text)
        BookStatusID = ObjLib.GetBookStatusID(cboStatus.Text)
        RackID = ObjLib.GetRackID(cboRack.Text)
        VenderID = ObjLib.GetVenderID(cboVender.Text)
        FrequencyID = ObjLib.GetFrequencyID(cboFrequency.Text)
        'ObjLib = Nothing

       
       
       

        Dim sqlStr As String = ""
        
        Dim i As Integer = 0
        Dim AccNo As String = txtAccNo.Text
        Dim NoOfBooks As Integer = 1
        If txtNoOfBooks.Text = "" Then
        Else
            NoOfBooks = Convert.ToInt32(txtNoOfBooks.Text)
        End If
        If txtNoOfBooks.Enabled = True Then
            'Try
            For i = 0 To NoOfBooks - 1

                sqlStr = "Insert into BookMaster (AccNo,BookTitle,BookCodeNo,PubId,Pages,Price,BookCatID,RackNo,BookStatusID,"
                sqlStr += "Remark,Issued,Vender,BillNo,BillDate,PhotoPath,Frequency,Volume,Issue,Month,Year,ISSN,DateIn) Values(" & _
                "'" & AccNo & "'," & _
                "'" & txtTitle.Text.Replace("'", "''") & "'," & _
                "'" & txtBookCodeNo.Text & "'," & _
                PubId & "," & _
                Val(txtPages.Text) & "," & _
                CDbl(txtPrice.Text) & "," & _
                BookCatID & "," & _
                RackID & "," & _
                BookStatusID & "," & _
                "'" & txtRemark.Text.Replace("'", "''") & "'," & _
                cboIssued.SelectedIndex & "," & _
                VenderID & "," & _
                "'" & txtBillNo.Text & "'," & _
                "'" & txtBillDate.Text.Substring(6, 4) & "/" & txtBillDate.Text.Substring(3, 2) & "/" & txtBillDate.Text.Substring(0, 2) & "'," & _
                "''," & _
                FrequencyID & "," & _
                "'" & txtVolume.Text & "'," & _
                "'" & txtIssue.Text & "'," & _
                "" & cboMonth.SelectedIndex & "," & _
                "" & cboYear.Text & "," & _
                "'" & txtISSN.Text & "'," & _
                "'" & txtDateNow.Text.Substring(6, 4) & "/" & txtDateNow.Text.Substring(3, 2) & "/" & txtDateNow.Text.Substring(0, 2) & "')"
                'If chkReference.Checked = True Then
                '    sqlStr += 1 & ")"
                'Else
                '    sqlStr += 0 & ")"
                'End If


                
                
                ExecuteQuery_Update(SqlStr)

                If chkDVD.Checked = True Then
                    'sqlStr = "Insert into DVDMaster Values('" & txtDVDTitle.Text & "','" & AccNo & "','" & txtDateNow.Text.Substring(6, 4) & "/" & txtDateNow.Text.Substring(3, 2) & "/" & txtDateNow.Text.Substring(0, 2) & "'," & AuthorID & ")"
                    '
                    '
                    'ExecuteQuery_Update(SqlStr)
                    Dim DVDAccNo = ObjLib.FindNextAccessionNo("D")
                    sqlStr = "Insert into DVDMaster Values('" & txtDVDTitle.Text & "','" & AccNo & "','" & txtDateNow.Text.Substring(6, 4) & "/" & txtDateNow.Text.Substring(3, 2) & "/" & txtDateNow.Text.Substring(0, 2) & "'," & AuthorID & ")"
                    sqlStr = "Insert into BookMaster (AccNo,BookTitle,BookCodeNo,Price,BookCatID,RackNo,BookStatusID,"
                    sqlStr += "Remark,Issued,Vender,BillNo,BillDate,PhotoPath,Frequency,DateIn,BookMagazineAccNo) Values(" & _
                "'" & DVDAccNo & "'," & _
                "'" & txtDVDTitle.Text.Replace("'", "''") & "'," & _
                "'" & txtBookCodeNo.Text & "'," & _
                0 & "," & _
                BookCatID & "," & _
                RackID & "," & _
                BookStatusID & "," & _
                "'" & txtRemark.Text.Replace("'", "''") & "'," & _
                cboIssued.SelectedIndex & "," & _
                VenderID & "," & _
                "'" & txtBillNo.Text & "'," & _
                "'" & txtBillDate.Text.Substring(6, 4) & "/" & txtBillDate.Text.Substring(3, 2) & "/" & txtBillDate.Text.Substring(0, 2) & "'," & _
                "''," & _
                5 & "," & _
                "'" & txtDateNow.Text.Substring(6, 4) & "/" & txtDateNow.Text.Substring(3, 2) & "/" & txtDateNow.Text.Substring(0, 2) & "'," & _
                "'" & AccNo & "')"
                    
                    
                    ExecuteQuery_Update(SqlStr)
                End If
                'Dim k1 As Integer = 0
                'For k1 = 0 To lstAuthors.Items.Count - 1
                '    AuthorID = ObjLib.GetAuthorID(lstAuthors.Items(k1).ToString)
                '    myCommand.CommandText = "Insert into BookAuthors Values('" & AccNo & "'," & AuthorID & ")"
                '    
                '    ExecuteQuery_Update(SqlStr)
                'Next
                'Dim ObjLib As New clsLibrary
                AccNo = ObjLib.FindNextAccessionNo("M")

            Next
            FinalMessage = "Accession No: " & txtAccNo.Text & " successfully added..."
            'Catch ex As Exception
            '    If ex.Message.Contains("duplicate") Then
        Else
            sqlStr = "Update BookMaster Set " & _
            "BookTitle='" & txtTitle.Text.Replace("'", "''") & "'," & _
            "BookCodeNo='" & txtBookCodeNo.Text & "'," & _
            "PubID=" & PubId & "," & _
            "Pages=" & Val(txtPages.Text) & "," & _
            "Price=" & CDbl(txtPrice.Text) & "," & _
            "BookCatID=" & BookCatID & "," & _
            "RackNo=" & RackID & "," & _
            "BookStatusID=" & BookStatusID & "," & _
            "Remark='" & txtRemark.Text.Replace("'", "''") & "'," & _
            "Issued=" & cboIssued.SelectedIndex & "," & _
            "Vender=" & VenderID & "," & _
            "BillNo='" & txtBillNo.Text & "'," & _
            "BillDate='" & txtBillDate.Text.Substring(6, 4) & "/" & txtBillDate.Text.Substring(3, 2) & "/" & txtBillDate.Text.Substring(0, 2) & "'," & _
            "Frequency=" & FrequencyID & "," & _
            "Volume='" & txtVolume.Text & "'," & _
            "Issue='" & txtIssue.Text & "'," & _
            "Month=" & cboMonth.SelectedIndex & "," & _
            "Year=" & cboYear.Text & "," & _
            "ISSN='" & txtISSN.Text & "'" & _
             " Where AccNo='" & txtAccNo.Text & "'"

            
            
            ExecuteQuery_Update(SqlStr)

            sqlStr = "Delete From BookMaster Where BookMagazineAccNo='" & txtAccNo.Text & "'"
            
            
            ExecuteQuery_Update(SqlStr)

            If chkDVD.Checked = True Then
                'myCommand.CommandText = "Insert into DVDMaster Values('" & txtDVDTitle.Text & "','" & txtAccNo.Text & "','" & txtDateNow.Text.Substring(6, 4) & "/" & txtDateNow.Text.Substring(3, 2) & "/" & txtDateNow.Text.Substring(0, 2) & "'," & AuthorID & ")"
                '
                'ExecuteQuery_Update(SqlStr)
                sqlStr = "Insert into BookMaster (AccNo,BookTitle,BookCodeNo,Price,BookCatID,RackNo,BookStatusID,"
                sqlStr += "Remark,Issued,Vender,BillNo,BillDate,PhotoPath,Frequency,DateIn,BookMagazineAccNo) Values(" & _
            "'" & txtDVDAccNo.Text & "'," & _
            "'" & txtDVDTitle.Text.Replace("'", "''") & "'," & _
            "'" & txtBookCodeNo.Text & "'," & _
            0 & "," & _
            BookCatID & "," & _
            RackID & "," & _
            BookStatusID & "," & _
            "'" & txtRemark.Text.Replace("'", "''") & "'," & _
            cboIssued.SelectedIndex & "," & _
            VenderID & "," & _
            "'" & txtBillNo.Text & "'," & _
            "'" & txtBillDate.Text.Substring(6, 4) & "/" & txtBillDate.Text.Substring(3, 2) & "/" & txtBillDate.Text.Substring(0, 2) & "'," & _
            "''," & _
            5 & "," & _
            "'" & txtDateNow.Text.Substring(6, 4) & "/" & txtDateNow.Text.Substring(3, 2) & "/" & txtDateNow.Text.Substring(0, 2) & "'," & _
            "'" & AccNo & "')"
                
                
                ExecuteQuery_Update(SqlStr)
            End If
            Dim k As Integer = 0

            'Dim ACommand As New SqlCommand
            'ACommand.CommandText = "Delete From BookAuthors Where AccNo='" & txtAccNo.Text & "'"
            'ACommand.Connection = myConn
            'ACommand.ExecuteNonQuery()
            ''ACommand.CommandText = "Delete From DVDMaster Where BookMagzineID='" & txtAccNo.Text & "'"
            ''ACommand.ExecuteNonQuery()
            ''ACommand.Dispose()

            'Dim ObjLib1 As New clsLibrary
            'For k = 0 To lstAuthors.Items.Count - 1
            '    AuthorID = ObjLib1.GetAuthorID(lstAuthors.Items(k).ToString)
            '    ACommand.CommandText = "Insert into BookAuthors Values('" & txtAccNo.Text & "'," & AuthorID & ")"
            '    ACommand.Connection = myConn
            '    ACommand.ExecuteNonQuery()
            '    ACommand.Dispose()
            '    System.Threading.Thread.Sleep(500)
            'Next
            'ObjLib1 = Nothing
            FinalMessage = "Accession No: " & txtAccNo.Text & " successfully updated..."
        End If
        'End Try
        




        'Dim Acommand2 As New SqlCommand("Update Params Set LastAccNo='" & txtAccNo.Text & "'", myConn)
        'Acommand2.ExecuteNonQuery()
        'Acommand2.Dispose()

        

        InitControls()
        lblStatus.Text = FinalMessage
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        If Trim(txtAccNo.Text).Length <= 0 Then
            lblStatus.Text = "Accession No is BLANK..."
            txtAccNo.Focus()
            Exit Sub
        End If

       
       
       

        Dim sqlStr As String = ""
        
        Dim FinalMessage As String = ""

        Try
            sqlStr = "Delete From BookAuthors Where AccNo='" & txtAccNo.Text & "'"
            
            
            ExecuteQuery_Update(SqlStr)

            'sqlStr = "Delete From DVDMaster Where BookMagzineID='" & txtAccNo.Text & "'"
            '
            '
            'ExecuteQuery_Update(SqlStr)
            sqlStr = "Delete From BookMaster Where BookMagazineAccNo='" & txtAccNo.Text & "'"
            
            
            ExecuteQuery_Update(SqlStr)

            sqlStr = "Delete From BookMaster Where AccNo='" & txtAccNo.Text & "'"
            
            
            ExecuteQuery_Update(SqlStr)



            FinalMessage = "Accession No: " & txtAccNo.Text & " successfully removed..."
        Catch ex As Exception
            FinalMessage = "Unable to remove Accession No: " & txtAccNo.Text
        End Try
        
        

        InitControls()
        lblStatus.Text = FinalMessage
    End Sub

    Protected Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        If Trim(txtAccNo.Text).Length <= 0 Then
            lblStatus.Text = "Accession No is BLANK..."
            txtAccNo.Focus()
            Exit Sub
        End If

        'lstAuthors.Items.Clear()

       
       
       

        Dim sqlStr As String = ""
        Dim myCount As Integer = 0
        

        sqlStr = "Select * From vw_BookMaster Where AccNo='" & txtAccNo.Text & "'"
        
        
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            txtTitle.Text = myReader("BookTitle")
            txtBookCodeNo.Text = myReader("BookCodeNo")
            cboPub.Text = myReader("PubName")
            txtPages.Text = myReader("Pages")
            txtPrice.Text = myReader("Price")
            cboBookCat.Text = myReader("BookCatName")
            cboRack.Text = myReader("RackName")
            cboStatus.Text = myReader("BookStatusName")
            'txtEdition.Text = myReader("Edition")
            txtRemark.Text = myReader("Remark")
            cboIssued.SelectedIndex = myReader("Issued")
            cboVender.Text = myReader("VenderName")
            cboFrequency.Text = myReader("FrequencyName")
            txtBillNo.Text = myReader("BillNo")
            'lstAuthors.Items.Add(myReader("AuthorName"))
            Dim a As Date = myReader("BillDate").ToString
            txtBillDate.Text = a.ToString("dd/MM/yyyy")
            If IsDBNull(myReader("DVDTitle")) Then
                chkDVD.Checked = False
            Else
                lblDVDTitle.Visible = True
                chkDVDTitle.Visible = True
                txtDVDTitle.Visible = True
                txtDVDTitle.Text = myReader("DVDTitle")
                txtDVDAccNo.Text = myReader("DVDAccNo")
                a = myReader("DateIn").ToString
                txtDateNow.Text = a.ToString("dd/MM/yyyy")
                chkDVD.Checked = True
                If (myReader("DVDTitle") = myReader("BookTitle")) Then
                    chkDVDTitle.Checked = True
                Else
                    chkDVDTitle.Checked = False
                End If
            End If
            txtVolume.Text = myReader("Volume")
            txtIssue.Text = myReader("Issue")
            cboMonth.SelectedIndex = myReader("Month")
            cboYear.Text = myReader("Year")
            txtISSN.Text = myReader("ISSN")


            myCount += 1
        End While
        myReader.Close()
        
        

        If myCount <= 0 Then
            Dim tempAcc As String = txtAccNo.Text
            InitControls()
            txtAccNo.Text = tempAcc
        End If
        txtNoOfBooks.Enabled = False

    End Sub

    'Protected Sub btnAddAuthor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddAuthor.Click
    '    lstAuthors.Items.Add(cboAuthor.Text)
    'End Sub

    'Protected Sub btnRemoveAuthor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemoveAuthor.Click
    '    If lstAuthors.Items.Count = 1 Then
    '        lblStatus.Text = "At least one author is required..."
    '        lstAuthors.Focus()
    '        Exit Sub
    '    End If
    '    lstAuthors.Items.RemoveAt(lstAuthors.SelectedIndex)
    'End Sub

    'Protected Sub btnRefreshAuthors_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRefreshAuthors.Click
    '    Dim ObjLib As New clsLibrary
    '    ObjLib.LoadAuthorsAsDropDown(cboAuthor)
    '    cboAuthor.Focus()
    '    ObjLib = Nothing
    'End Sub

    Protected Sub btnRefreshPublisher_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRefreshPublisher.Click
        Dim ObjLib As New iDiary.clsLibrary
        ObjLib.LoadPublishersAsDropDown(cboPub)
        cboPub.Focus()
        ObjLib = Nothing
    End Sub

    Protected Sub btnRefreshBookCategory_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRefreshBookCategory.Click
        Dim ObjLib As New iDiary.clsLibrary
        ObjLib.LoadBookCategoryAsDropDown(cboBookCat)
        cboBookCat.Focus()
        ObjLib = Nothing
    End Sub

    Protected Sub btnRefreshStatus_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRefreshStatus.Click
        Dim ObjLib As New iDiary.clsLibrary
        ObjLib.LoadBookStatusAsDropDown(cboStatus)
        cboStatus.Focus()
        ObjLib = Nothing
    End Sub

    Protected Sub btnRefreshRack_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRefreshRack.Click
        Dim ObjLib As New iDiary.clsLibrary
        ObjLib.LoadRackAsDropDown(cboRack)
        cboRack.Focus()
        ObjLib = Nothing
    End Sub

    Protected Sub chkDVD_CheckedChanged(sender As Object, e As EventArgs) Handles chkDVD.CheckedChanged
        If chkDVD.Checked = True Then
            lblDVDTitle.Visible = True
            chkDVDTitle.Visible = True
            txtDVDTitle.Visible = True
            chkDVDTitle.Focus()
        Else
            lblDVDTitle.Visible = False
            chkDVDTitle.Visible = False
            txtRemark.Focus()
        End If
    End Sub

    Protected Sub chkDVDTitle_CheckedChanged(sender As Object, e As EventArgs) Handles chkDVDTitle.CheckedChanged
        If chkDVDTitle.Checked = True Then
            txtDVDTitle.Text = txtTitle.Text
            txtRemark.Focus()
        Else
            txtDVDTitle.Visible = True
            txtDVDTitle.Text = ""
            txtDVDTitle.Focus()
        End If
    End Sub
End Class