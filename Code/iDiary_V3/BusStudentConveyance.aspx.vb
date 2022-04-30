Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary_Student.CLS_iDiary_Student
Imports iDiary_V3.iDiary_Fee.CLS_iDiary_Fee

Public Class BusStudentConveyence
    Inherits System.Web.UI.Page
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Request.Cookies("UType").Value.ToString.Contains("Bus-1") Or Request.Cookies("UType").Value.ToString.Contains("Admin-1") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
            End If
        Catch ex As Exception
            Response.Redirect("~/Login.aspx")
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("ActiveTab") = 11
        Response.Cookies("ActiveTab").Value = 11
        Response.Cookies("ActiveTab").Expires = DateTime.Now.AddHours(1)
        If Not IsPostBack Then
            initControls()

        End If
    End Sub

    Private Sub initControls()
        txtClass.Text = ""
        txtName.Text = ""
        txtRegNo.Text = ""
        txtSection.Text = ""
        'txtsiblingName.Text = ""
        txtSID.Text = ""
        lblMode.Visible = False
        cboModeConvey.Visible = False
        btnSavetransport.Visible = False
        'LoadMasterInfo(2, cboSiblingClass)
        LoadFeeTerms(cboTermNo, 1, "BusFee")
        LoadMasterInfo(38, cboBusLocation)
        LoadMasterInfo(69, cboConvaeyanceHead)
        LoadMasterInfo(40, cboModeConvey)
        LoadMasterInfo(45, cboBus)
        txtRegNo.Focus()
        cboBusService.SelectedIndex = 0
        lblStatus.Text = ""
        lblTerm.Text = ""
        PanelBus.Visible = False

    End Sub

    Protected Sub cboBus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboBusService.SelectedIndexChanged
        If cboBusService.SelectedIndex = 1 Then    '1==NO
            lblMode.Visible = True
            cboModeConvey.Visible = True
            'btnSavetransport.Visible = True
            PanelBus.Visible = False
        ElseIf cboBusService.SelectedIndex = 2 Then    '2==yes
            lblMode.Visible = False
            cboModeConvey.Visible = False
            'btnSavetransport.Visible = False
            PanelBus.Visible = True
        End If
    End Sub
    Protected Sub GridView2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvSearch.SelectedIndexChanged
        gvSearch.Columns.Item(1).Visible = True
        gvSearch.DataBind()

        txtRegNo.Text = gvSearch.SelectedRow.Cells(2).Text
        txtName.Text = gvSearch.SelectedRow.Cells(3).Text
        txtClass.Text = gvSearch.SelectedRow.Cells(4).Text
        txtSection.Text = gvSearch.SelectedRow.Cells(5).Text
        txtSID.Text = gvSearch.SelectedRow.Cells(1).Text
        gvSearch.Visible = False
        ShowDefault()
    End Sub
    Private Sub ShowDefault()
        cboBusService.SelectedIndex = 2
        If cboBusService.SelectedIndex = 1 Then    '1==NO
            lblMode.Visible = True
            cboModeConvey.Visible = True
            'btnSavetransport.Visible = True
            PanelBus.Visible = False
        ElseIf cboBusService.SelectedIndex = 2 Then    '2==yes
            lblMode.Visible = False
            cboModeConvey.Visible = False
            'btnSavetransport.Visible = False
            PanelBus.Visible = True
        End If
        'cboBusRoute.Text = "NA"

        cboTermNo.Focus()
    End Sub
    Private Sub ShowStudent()

    End Sub

    'Protected Sub cboClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSiblingClass.SelectedIndexChanged
    '    LoadClassSection(cboSiblingClass.Text, cboSiblingSection)
    '    cboSiblingSection.Focus()

    'End Sub

    'Protected Sub btnAddSibling_Click(sender As Object, e As EventArgs) Handles btnAddSibling.Click
    '    If Trim(txtsiblingName.Text) = "" Then
    '        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Sibling Name is Required');", True)
    '        txtsiblingName.Focus()
    '        Exit Sub
    '    End If
    '    If Trim(cboSiblingClass.Text) = "" Then
    '        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Sibling Class is Required');", True)
    '        cboSiblingClass.Focus()
    '        Exit Sub
    '    End If
    '    If Trim(cboSiblingSection.Text) = "" Then
    '        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Sibling Section is Required');", True)
    '        cboSiblingSection.Focus()
    '        Exit Sub
    '    End If
    '    AddBusSibling(txtsiblingName.Text, cboSiblingClass.Text, cboSiblingSection.Text)

    '    gvSiblings.DataBind()
    '    txtsiblingName.Focus()
    'End Sub
    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtSID.Text = "" Then
            lblStatus.Text = "Please select Student..."
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please select Student...');", True)
            txtRegNo.Focus()
            Exit Sub
        End If
        If cboTermNo.Text = "" Then
            lblStatus.Text = "Invalid Term..."
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Term...');", True)
            cboTermNo.Focus()
            Exit Sub
        End If
        If cboBusService.Text = "" Then
            lblStatus.Text = "Invalid Bus Servise..."
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Bus Service...');", True)
            cboBusService.Focus()
            Exit Sub
        End If
        If cboBusService.Text = "No" And cboModeConvey.Text = "" Then
            lblStatus.Text = "Invalid Conveyance Mode..."
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Conveyance Mode...');", True)
            cboModeConvey.Focus()
            Exit Sub
        End If
        'If cboBusService.Text = "Yes" And cboBusLocation.Text = "" Then
        '    lblStatus.Text = "Invalid Location..."
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Location...');", True)
        '    cboBusLocation.Focus()
        '    Exit Sub
        'End If
        If cboBusService.Text = "Yes" And cboConvaeyanceHead.Text = "" Then
            lblStatus.Text = "Invalid Conveyance Head..."
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Conveyance Head...');", True)
            cboConvaeyanceHead.Focus()
            Exit Sub
        End If

        'If cboBusService.Text = "Yes" And cboBusRoute.Text = "" Then
        '    lblStatus.Text = "Invalid Route..."
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Route...');", True)
        '    cboBusRoute.Focus()
        '    Exit Sub
        'End If
        'If cboBusService.Text = "Yes" And cboBus.Text = "" Then
        '    lblStatus.Text = "Invalid Bus..."
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Bus...');", True)
        '    cboBus.Focus()
        '    Exit Sub
        'End If






        Dim sqlstr As String = ""


        'Save Bus Details
        Dim BusRequired As Integer = 0
        'Dim busRouteID As Integer = FindMasterID(39, cboConvaeyanceHead.Text)
        Dim locationID As Integer = FindMasterID(38, cboBusLocation.Text)
        Dim busID As Integer = FindMasterID(45, cboBus.Text)
        Dim ConveyanceModeID As Integer = FindMasterID(40, cboModeConvey.Text)
        Dim ConveyanceHeadID As Integer = FindMasterID(69, cboConvaeyanceHead.Text)
        Dim TermID As Integer = cboTermNo.SelectedValue
        'FindMasterID(67, cboTermNo.Text)
        If cboBusService.Text = "Yes" Then
            BusRequired = 1
            ConveyanceModeID = 0
        End If



        'If CheckConveyance() = False Then
        'If cboTermNo.Text = "" Then
        'sqlstr = "Delete from BusStudentMap(SID, BusRequired, ConeyanceMode, busID, locationID,RouteID, TermNo, createdBY) Values('" & txtSID.Text & "','" & BusRequired & "','" & ConveyanceModeID & "','" & busID & "','" & locationID & "','" & RouteID & "','" & cboTermNo.Items(i).Text & "','" & Request.Cookies("UserName").Value & "')"
        '
        '
        'ExecuteQuery_Update(SqlStr)
        For i = cboTermNo.SelectedIndex To cboTermNo.Items.Count - 1

            TermID = FindMasterID(67, cboTermNo.Items(i).Text)
            sqlstr = "Delete from BusStudentMap where SID='" & txtSID.Text & "' and TermNo='" & TermID & "'"

            ExecuteQuery_Update(sqlstr)
            sqlstr = "Insert into BusStudentMap(SID, BusRequired, ConeyanceMode, busID, locationID, RouteID, TermNo, ConveyanceHeadID, Amount, createdBY) Values('" & txtSID.Text & "','" & BusRequired & "','" & ConveyanceModeID & "','" & busID & "','" & locationID & "','" & 0 & "','" & TermID & "','" & ConveyanceHeadID & "','" & GetAmount(ConveyanceHeadID, TermID, Request.Cookies("ASID").Value) & "','" & Request.Cookies("UserID").Value & "')"

            ExecuteQuery_Update(sqlstr)
        Next
        'Else
        '    sqlstr = "Insert into BusStudentMap(SID, BusRequired, ConeyanceMode, busID, locationID, RouteID, TermNo, ConveyanceHeadID, Amount, createdBY) Values('" & txtSID.Text & "','" & BusRequired & "','" & ConveyanceModeID & "','" & busID & "','" & locationID & "','" & RouteID & "','" & cboTermNo.Text & "','" & Request.Cookies("UserName").Value & "')"
        'End If

        'Else

        '    sqlstr = "update BusStudentMap set BusRequired='" & BusRequired & "', ConeyanceMode='" & ConveyanceModeID & "', busID='" & busID & "', locationID='" & locationID & "',RouteID='" & RouteID & "', createdBY='" & Request.Cookies("UserName").Value & "' where SID='" & txtSID.Text & "' and TermNo>=" & cboTermNo.Text
        'End If

        '
        '
        'ExecuteQuery_Update(SqlStr)

        'Update Bus Sibling Information
        'sqlstr = "Delete From BusSiblings Where SID='" & txtSID.Text & "'"
        '
        '
        'ExecuteQuery_Update(SqlStr)

        'Dim TempSQlbus As String = ""
        'Dim lstInsertbus As New ListBox
        'lstInsertbus.Items.Clear()

        'sqlstr = "Select SibName, SibClass, SibSec From TempBusSiblings"
        '
        '
        'Dim BusSibReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        'While BusSibReader.Read
        '    TempSQlbus = "Insert into BusSiblings Values(" & _
        '    "'" & txtSID.Text & "," & _
        '    "'" & BusSibReader(0) & "'," & _
        '    FindMasterID(2, BusSibReader(1)) & "," & _
        '    FindSectionID(BusSibReader(1), BusSibReader(2)) & _
        '    ")"
        '    lstInsertbus.Items.Add(TempSQlbus)
        'End While
        'BusSibReader.Close()

        'sqlstr = "Truncate table TempBusSiblings"
        '
        '
        'ExecuteQuery_Update(SqlStr)

        ''Dim i As Integer = 0
        'For i = 0 To lstInsertbus.Items.Count - 1
        '    myCommand.CommandText = lstInsertbus.Items(i).Text
        '    
        '    ExecuteQuery_Update(SqlStr)
        'Next


        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Conveyance detail has been saved...');", True)
        initControls()
    End Sub

    Protected Sub cboModeConvey_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboModeConvey.SelectedIndexChanged
        If cboModeConvey.SelectedIndex = 1 Then
            btnSavetransport.Visible = True
        End If
    End Sub


    Protected Sub btnSavetransport_Click(sender As Object, e As EventArgs) Handles btnSavetransport.Click
        Dim sqlstr As String = ""
        sqlstr = "Delete From StudentTransport where SID='" & txtSID.Text & "'"
        ExecuteQuery_Update(sqlstr)
        sqlstr = "Insert Into StudentTransport Values('" & txtSID.Text & "','" & cboBusService.SelectedIndex - 1 & "','" & cboModeConvey.Text & "')"
        ExecuteQuery_Update(sqlstr)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Student Conveyence Saved...');", True)
    End Sub
    Private Sub ShowData(ByVal TermNo As String, SID As Integer)




        Dim sqlstr As String = ""


        sqlstr = "Select * From vw_StudentBusMap Where BusTermID='" & TermNo & "' and SID=" & SID


        Dim myreader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)

        While myreader.Read
            Try
                If myreader("BusRequired") = 1 Then
                    cboBusService.Text = "Yes"
                Else
                    cboBusService.Text = "No"
                End If
            Catch ex As Exception
                cboBusService.Text = "No"
            End Try

            If cboBusService.Text = "No" Then    '1==NO
                lblMode.Visible = True
                cboModeConvey.Visible = True
                'btnSavetransport.Visible = True
                PanelBus.Visible = False
                Try
                    cboModeConvey.Text = myreader("ConveyanceName")
                Catch ex As Exception

                End Try

            Else    '2==yes
                lblMode.Visible = False
                cboModeConvey.Visible = False
                'btnSavetransport.Visible = False
                PanelBus.Visible = True
                Try
                    cboBusLocation.Text = myreader("LocationName")
                    cboConvaeyanceHead.Text = myreader("ConveyanceTypeName")
                    'loadBUS(cboConvaeyanceHead.Text)
                    cboBus.Text = myreader("BusName")
                    txtAmount.Text = myreader("Amount")
                Catch ex As Exception

                End Try

            End If

        End While

    End Sub
    Private Function CheckConveyance() As Boolean



        Dim rv As Integer = 0
        Dim sqlstr As String = ""


        sqlstr = "Select Count(*) From BusStudentMap Where SID='" & txtSID.Text & "' and BusTermNo=" & cboTermNo.Text


        Try
            rv = ExecuteQuery_ExecuteScalar(sqlstr)
        Catch ex As Exception

        End Try
        If rv > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Sub GetConveyanceHead(LocationName As String)
        Dim sqlstr As String = ""
        sqlstr = "Select ConveyanceTypeID From locationMaster Where locationName='" & LocationName & "'"
        Dim chi As Integer = 0
        Try
            chi = ExecuteQuery_ExecuteScalar(sqlstr)
            cboConvaeyanceHead.Text = FindMasterName(69, chi)
            GetAmount(chi, cboTermNo.Text, Request.Cookies("ASID").Value)
        Catch ex As Exception

        End Try
    End Sub
    Private Function GetAmount(ConveyanceHeadID As String, TermID As Integer, ASID As Integer) As Double
        Dim sqlstr As String = ""
        sqlstr = "Select Amount From BusHeadTypeConfig Where ConveyanceHeadTypeID=" & ConveyanceHeadID & " and TermNo='" & TermID & "' and ASID=" & ASID
        'sqlstr = "Select ConveyanceTypeID From locationName Where locationName='" & LocationName
        Dim rv As Double = 0
        Try
            rv = ExecuteQuery_ExecuteScalar(sqlstr)
            txtAmount.Text = rv
        Catch ex As Exception

        End Try
        Return rv
    End Function

    Protected Sub cboTermNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTermNo.SelectedIndexChanged
        lblTerm.Text = LoadFeeTermCaption(Val(cboTermNo.Text))
        'LoadDueConfig()
        'cboTermNo.Focus()
        If cboTermNo.Text <> "" Then
            ShowData(cboTermNo.SelectedValue, txtSID.Text)
        End If
    End Sub

    Protected Sub btnNextAdminNo_Click1(sender As Object, e As EventArgs) Handles btnNextAdminNo.Click
        Dim sqlstr As String = ""
        sqlstr = "Select SID,SName,ClassName,SecName From vw_student where regno='" & txtRegNo.Text & "'  and ASID=" & Request.Cookies("ASID").Value

        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)

        While myReader.Read
            txtSID.Text = myReader(0)
            txtName.Text = myReader(1)
            txtClass.Text = myReader(2)
            txtSection.Text = myReader(3)
            ShowDefault()
        End While
        myReader.Close()

        gvSearch.Visible = False
    End Sub

    Protected Sub cboBusLocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboBusLocation.SelectedIndexChanged
        GetConveyanceHead(cboBusLocation.Text)
    End Sub

    Protected Sub cboConvaeyanceHead_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboConvaeyanceHead.SelectedIndexChanged
        GetAmount(FindMasterID(69, cboConvaeyanceHead.Text), cboTermNo.SelectedValue, Request.Cookies("ASID").Value)
    End Sub

    Protected Sub btnNextStudent_Click1(sender As Object, e As EventArgs) Handles btnNextStudent.Click
        gvSearch.Visible = True
        SqlDataSourceSearch.SelectCommand = "SELECT SID,RegNo, SName, ClassName, SecName,FName FROM vw_Student WHERE ASID = " & Request.Cookies("ASID").Value & " AND SName Like '%" & txtName.Text & "%'"

        gvSearch.DataBind()
        'gvSearch.Visible = True
        gvSearch.Columns.Item(1).Visible = False
    End Sub
End Class