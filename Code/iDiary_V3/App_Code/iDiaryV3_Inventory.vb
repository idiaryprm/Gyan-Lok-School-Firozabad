Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Public Class iDiaryV3_Inventory
    Public Shared Function GetVendorID(VendorName As String) As Integer
        Dim rv As Integer = 0
        Dim SqlStr As String = "select max(VendorID) from VendorInventory where VendorName='" & VendorName & "'"
        Try
            rv = ExecuteQuery_ExecuteScalar(SqlStr)
        Catch ex As Exception
            rv = 0
        End Try
        Return rv
    End Function
    Public Shared Function GetItemUnitID(ItemUnitName As String) As Integer
        Dim rv As Integer = 0
        Dim SqlStr As String = "select Max(iuID) from itemUnitMaster where unitName='" & ItemUnitName & "'"
        Try
            rv = ExecuteQuery_ExecuteScalar(SqlStr)
        Catch ex As Exception
            rv = 0
        End Try
        Return rv
    End Function
    Public Shared Function GetItemID(ItemName As String) As Integer
        Dim rv As Integer = 0
        Dim SqlStr As String = "Select Max(itemID) From itemMaster where itemName='" & ItemName & "'"
        Try
            rv = ExecuteQuery_ExecuteScalar(SqlStr)
        Catch ex As Exception
            rv = 0
        End Try
        Return rv
    End Function
    Public Shared Function GetLedgerID(LedgerName As String) As Integer
        Dim rv As Integer = 0
        Dim SqlStr As String = "select max(LedgerID) from InventoryLedger where LedgerName='" & LedgerName & "'"
        Try
            rv = ExecuteQuery_ExecuteScalar(SqlStr)
        Catch ex As Exception
            rv = 0
        End Try
        Return rv
    End Function
    Public Shared Function GetItemUnitName(ItemUnitID As Integer) As String
        Dim rv As String = 0
        Dim SqlStr As String = "select unitName from itemUnitMaster where iuID='" & ItemUnitID & "'"
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(SqlStr)
        While myReader.Read
            rv = myReader(0)
        End While
        myReader.Close()

        Return rv
    End Function
    Public Shared Function LoadInventoryInfo(ByVal MasterType As Integer, ByRef myCbo As DropDownList) As Integer
        Dim sqlStr As String = ""

        Select Case MasterType
            Case 1 : sqlStr = "Select unitName From itemUnitMaster"
            Case 2 : sqlStr = "select LedgerName from InventoryLedger"
            Case 3 : sqlStr = "Select VendorName From VendorInventory"
            Case 4 : sqlStr = "Select itemName From itemMaster"
        End Select

        Dim myReader As System.Data.SqlClient.SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        myCbo.Items.Clear()
        myCbo.Items.Add("")
        While myReader.Read
            myCbo.Items.Add(myReader(0))
        End While
        myReader.Close()

        Return 0
    End Function
    Public Shared Function LoadInventoryInfo(ByVal MasterType As Integer, ByRef myLst As ListBox) As Integer

        Dim sqlStr As String = ""

        Select Case MasterType
            Case 1 : sqlStr = "Select itemName From itemMaster"
            Case 2 : sqlStr = "Select unitName From itemUnitMaster"
            Case 3 : sqlStr = "Select VendorName From VendorInventory"
            Case 4 : sqlStr = "select LedgerName from InventoryLedger"
        End Select

        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        myLst.Items.Clear()

        While myReader.Read
            myLst.Items.Add(myReader(0))
        End While
        myReader.Close()

        Return 0

    End Function
    Public Shared Function CheckInventoryDuplicateEntry(ByVal MasterType As Integer, ByRef MasterName As String) As Integer
        Dim sqlStr As String = ""

        Select Case MasterType
            Case 1 : sqlStr = "select Count(itemName) from itemMaster where itemName='" & MasterName & "'"
            Case 2 : sqlStr = "select Count(*) from itemUnitMaster where unitName='" & MasterName & "'"
            Case 3 : sqlStr = "select Count(*) from VendorInventory where VendorName='" & MasterName & "'"
            Case 4 : sqlStr = "select Count(*) from InventoryLedger where LedgerName='" & MasterName & "'"
        End Select
        Dim myID As Integer = 0
        Try
            myID = ExecuteQuery_ExecuteScalar(sqlStr)
        Catch ex As Exception
            myID = 0
        End Try

        Return myID
    End Function
End Class
