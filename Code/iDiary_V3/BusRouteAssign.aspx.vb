Imports iDiary_V3.iDiary.CLS_idiary
Imports System.Data.SqlClient

Public Class BusRouteAssign
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            InitControls()
        End If


    End Sub

    Private Sub InitControls()
        LoadMasterInfo(45, cboBus)
        LoadMasterInfo(39, cboRoute)
        viewBUSassignment()

    End Sub

    Protected Sub btnAssign_Click(sender As Object, e As EventArgs) Handles btnAssign.Click
        If Trim(cboBus.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Bus Selection is required...');", True)
            cboBus.Focus()
            Exit Sub
        End If
        If Trim(cboRoute.Text) = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Bus Route is required...');", True)
            cboRoute.Focus()
            Exit Sub
        End If

       
       
       
        
        Dim busID As Integer = FindMasterID(45, cboBus.Text)
        Dim RouteID As Integer = FindMasterID(39, cboRoute.Text)

        Dim SqlStr As String = "Insert Into BusRouteMap Values('" & busID & "','" & RouteID & "')"
        
        
        ExecuteQuery_Update(SqlStr)

        

        viewBUSassignment()

    End Sub

    Private Sub viewBUSassignment()
        Dim  connectionString as string= System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        Dim myConnection As SqlConnection

        Dim sqlStr As String = "SELECT dbo.busMaster.busName As [Bus Name], dbo.BusRouteMaster.routeName as [Route Name] FROM dbo.BusRouteMaster INNER JOIN dbo.BusRouteMap ON dbo.BusRouteMaster.routeID = dbo.BusRouteMap.RouteID RIGHT OUTER JOIN dbo.busMaster ON dbo.BusRouteMap.BusID = dbo.busMaster.busID"
        Dim myCommand As SqlCommand
        Dim myDataSet As DataSet
        myConnection = New SqlConnection(connectionString)
        myConnection.Open()
        myCommand = New SqlCommand(sqlStr, myConnection)
        Dim mySQLDataAdapter As SqlDataAdapter
        myDataSet = New DataSet()
        mySQLDataAdapter = New SqlDataAdapter(myCommand)
        mySQLDataAdapter.Fill(myDataSet, "AccountsTable")
        GridView1.DataSource = myDataSet
        GridView1.DataBind()

    End Sub
End Class