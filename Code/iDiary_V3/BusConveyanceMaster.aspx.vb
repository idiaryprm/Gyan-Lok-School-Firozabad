Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary_Student.CLS_iDiary_Student
Imports iDiary_V3.iDiary_Fee.CLS_iDiary_Fee

Public Class StudentConveyence
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            initControls()

        End If
    End Sub

    Private Sub initControls()
        txtClass.Text = ""
        txtName.Text = ""
        txtRegNo.Text = ""
        txtSection.Text = ""
        txtsiblingName.Text = ""
        txtSID.Text = ""
        lblMode.Visible = False
        cboModeConvey.Visible = False
        btnSavetransport.Visible = False
        LoadMasterInfo(2, cboSiblingClass)
        LoadFeeTerms(cboTermNo, 1, "BusFee")
        LoadMasterInfo(38, cboBusLocation)
        LoadMasterInfo(39, cboBusRoute)
        LoadMasterInfo(40, cboModeConvey)
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

    Protected Sub btnNext0_Click(sender As Object, e As ImageClickEventArgs) Handles btnNext0.Click
        gvSearch.Visible = True
        SqlDataSourceSearch.SelectCommand = "SELECT SID,RegNo, SName, ClassName, SecName,FName FROM vw_Student WHERE ASID = " & Request.Cookies("ASID").Value & " AND SName Like '%" & txtName.Text & "%'"

        gvSearch.DataBind()
        'gvSearch.Visible = True
        gvSearch.Columns.Item(1).Visible = False

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

    Protected Sub btnNext_Click(sender As Object, e As ImageClickEventArgs) Handles btnNext.Click
        Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
        myConn.Open()

        Dim sqlstr As String = ""
        Dim myCommand As New SqlCommand

        sqlstr = "Select SID,SName,ClassName,SecName From vw_student where regno='" & txtRegNo.Text & "'"
        myCommand.CommandText = sqlstr
        myCommand.Connection = myConn

        Dim myReader As SqlDataReader = myCommand.ExecuteReader

        While myReader.Read
            txtSID.Text = myReader(0)
            txtName.Text = myReader(1)
            txtClass.Text = myReader(2)
            txtSection.Text = myReader(3)
            ShowDefault()
        End While
        myReader.Close()
        myConn.Close()
        gvSearch.Visible = False

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

        cboBusLocation.Focus()
    End Sub
    Private Sub ShowStudent()

    End Sub

    Protected Sub cboClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSiblingClass.SelectedIndexChanged
        LoadClassSection(cboSiblingClass.Text, cboSiblingSection)
        cboSiblingSection.Focus()

    End Sub

    Protected Sub btnAddSibling_Click(sender As Object, e As EventArgs) Handles btnAddSibling.Click
        If Trim(txtsiblingName.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Sibling Name is Required');", True)
            txtsiblingName.Focus()
            Exit Sub
        End If
        If Trim(cboSiblingClass.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Sibling Class is Required');", True)
            cboSiblingClass.Focus()
            Exit Sub
        End If
        If Trim(cboSiblingSection.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Sibling Section is Required');", True)
            cboSiblingSection.Focus()
            Exit Sub
        End If
        AddBusSibling(txtsiblingName.Text, cboSiblingClass.Text, cboSiblingSection.Text)

        gvSiblings.DataBind()
        txtsiblingName.Focus()
    End Sub
    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
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
        If cboBusService.Text = "Yes" And cboBusLocation.Text = "" Then
            lblStatus.Text = "Invalid Location..."
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Location...');", True)
            cboBusLocation.Focus()
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
        
        
        Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
        myConn.Open()

        Dim sqlstr As String = ""
        Dim myCommand As New SqlCommand

        'Save Bus Details
        Dim BusRequired As Integer = 0
        
        Dim locationID As Integer = FindMasterID(38, cboBusLocation.Text)
        Dim busRouteID As Integer = FindMasterID(39, cboBusRoute.Text)
        Dim busID As Integer = FindMasterID(45, cboBus.Text)
        Dim ConveyanceModeID As Integer = FindMasterID(40, cboModeConvey.Text)
        Dim RouteID As Integer = FindMasterID(39, cboBusRoute.Text)
        If cboBusService.Text = "Yes" Then
            BusRequired = 1
            ConveyanceModeID = 0
        End If
        If CheckConveyance() = False Then
            If cboTermNo.Text = "All" Then
                'sqlstr = "Delete from BusStudentMap(SID, BusRequired, ConeyanceMode, busID, locationID,RouteID, TermNo, createdBY) Values('" & txtSID.Text & "','" & BusRequired & "','" & ConveyanceModeID & "','" & busID & "','" & locationID & "','" & RouteID & "','" & cboTermNo.Items(i).Text & "','" & Request.Cookies("UserName").Value & "')"
                'myCommand.CommandText = sqlstr
                'myCommand.Connection = myConn
                'myCommand.ExecuteNonQuery()
                For i = 1 To cboTermNo.Items.Count - 2
                    sqlstr = "Insert into BusStudentMap(SID, BusRequired, ConeyanceMode, busID, locationID,RouteID, TermNo, createdBY) Values('" & txtSID.Text & "','" & BusRequired & "','" & ConveyanceModeID & "','" & busID & "','" & locationID & "','" & RouteID & "','" & cboTermNo.Items(i).Text & "','" & Request.Cookies("UserName").Value & "')"
                    myCommand.CommandText = sqlstr
                    myCommand.Connection = myConn
                    myCommand.ExecuteNonQuery()
                Next
            Else
                sqlstr = "Insert into BusStudentMap(SID, BusRequired, ConeyanceMode, busID, locationID,RouteID, TermNo, createdBY) Values('" & txtSID.Text & "','" & BusRequired & "','" & ConveyanceModeID & "','" & busID & "','" & locationID & "','" & RouteID & "','" & cboTermNo.Text & "','" & Request.Cookies("UserName").Value & "')"
            End If

        Else

            sqlstr = "update BusStudentMap set BusRequired='" & BusRequired & "', ConeyanceMode='" & ConveyanceModeID & "', busID='" & busID & "', locationID='" & locationID & "',RouteID='" & RouteID & "', createdBY='" & Request.Cookies("UserName").Value & "' where SID='" & txtSID.Text & "' and TermNo>=" & cboTermNo.Text
        End If

        myCommand.CommandText = sqlstr
        myCommand.Connection = myConn
        myCommand.ExecuteNonQuery()

        'Update Bus Sibling Information
        'sqlstr = "Delete From BusSiblings Where SID='" & txtSID.Text & "'"
        'myCommand.CommandText = sqlstr
        'myCommand.Connection = myConn
        'myCommand.ExecuteNonQuery()

        'Dim TempSQlbus As String = ""
        'Dim lstInsertbus As New ListBox
        'lstInsertbus.Items.Clear()

        'sqlstr = "Select SibName, SibClass, SibSec From TempBusSiblings"
        'myCommand.CommandText = sqlstr
        'myCommand.Connection = myConn
        'Dim BusSibReader As SqlDataReader = myCommand.ExecuteReader
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
        'myCommand.CommandText = sqlstr
        'myCommand.Connection = myConn
        'myCommand.ExecuteNonQuery()

        ''Dim i As Integer = 0
        'For i = 0 To lstInsertbus.Items.Count - 1
        '    myCommand.CommandText = lstInsertbus.Items(i).Text
        '    myCommand.Connection = myConn
        '    myCommand.ExecuteNonQuery()
        'Next
        myCommand.Dispose()
        myConn.Close()
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Conveyance detail has been saved...');", True)
        initControls()
    End Sub

    Protected Sub cboModeConvey_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboModeConvey.SelectedIndexChanged
        If cboModeConvey.SelectedIndex = 1 Then
            btnSavetransport.Visible = True

        End If
    End Sub

   
    Protected Sub btnSavetransport_Click(sender As Object, e As EventArgs) Handles btnSavetransport.Click

        Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
        myConn.Open()

        Dim sqlstr As String = ""
        Dim myCommand As New SqlCommand

        sqlstr = "Delete From StudentTransport where SID='" & txtSID.Text & "'"
        myCommand.Connection = myConn
        myCommand.CommandText = sqlstr
        myCommand.ExecuteNonQuery()

        sqlstr = "Insert Into StudentTransport Values('" & txtSID.Text & "','" & cboBusService.SelectedIndex - 1 & "','" & cboModeConvey.Text & "')"
        myCommand.Connection = myConn
        myCommand.CommandText = sqlstr
        myCommand.ExecuteNonQuery()

        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Student Conveyence Saved...');", True)
    End Sub

    Protected Sub cboBusRoute_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboBusRoute.SelectedIndexChanged
        loadBUS(cboBusRoute.Text)

    End Sub

    Private Sub loadBUS(ByVal route As String)
        Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
        myConn.Open()

        Dim sqlstr As String = ""
        Dim myCommand As New SqlCommand

        sqlstr = "Select BusName From vw_Bus_Route Where RouteName='" & route & "'"
        myCommand.Connection = myConn
        myCommand.CommandText = sqlstr
        Dim myreader As SqlDataReader = myCommand.ExecuteReader
        cboBus.Items.Clear()

        While myreader.Read
            cboBus.Items.Add(myreader(0))
        End While

    End Sub
    Private Sub ShowData(ByVal TermNo As Integer, SID As Integer)
        Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
        myConn.Open()

        Dim sqlstr As String = ""
        Dim myCommand As New SqlCommand

        sqlstr = "Select * From vw_StudentBusMap Where TermNo='" & TermNo & "' and SID=" & SID
        myCommand.Connection = myConn
        myCommand.CommandText = sqlstr
        Dim myreader As SqlDataReader = myCommand.ExecuteReader

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
                    cboBusRoute.Text = myreader("RouteName")
                    loadBUS(cboBusRoute.Text)
                    cboBus.Text = myreader("BusName")
                Catch ex As Exception

                End Try
              
            End If

        End While

    End Sub
    Private Function CheckConveyance() As Boolean
        Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        Dim myConn As New System.Data.SqlClient.SqlConnection(myConnStr)
        myConn.Open()
        Dim rv As Integer = 0
        Dim sqlstr As String = ""
        Dim myCommand As New SqlCommand

        sqlstr = "Select Count(*) From BusStudentMap Where SID='" & txtSID.Text & "' and TermNo=" & cboTermNo.Text
        myCommand.Connection = myConn
        myCommand.CommandText = sqlstr
        Try
            rv = myCommand.ExecuteScalar
        Catch ex As Exception

        End Try
        If rv > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Protected Sub cboTermNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTermNo.SelectedIndexChanged
        lblTerm.Text = LoadFeeTermCaption(Val(cboTermNo.Text))
        'LoadDueConfig()
        'cboTermNo.Focus()
        If cboTermNo.Text <> "All" Then
            ShowData(cboTermNo.Text, txtSID.Text)
        End If
    End Sub
End Class