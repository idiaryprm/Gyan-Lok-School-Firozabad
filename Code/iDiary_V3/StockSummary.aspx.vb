Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary


Public Class StockSummary
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        StockSummary()
    End Sub

    Protected Sub StockSummary()

        Dim sqlStr As String = ""
        'Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        Dim lstItem As New List(Of String)
        sqlStr = "Select distinct itemName From vw_Stock order by itemName"
        
        
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            lstItem.Add(myReader("itemName"))
        End While
        myReader.Close()

        sqlStr = "truncate table tmpstock"
        
        
        ExecuteQuery_Update(SqlStr)

        Dim qtyIN As Integer = 0
        Dim qtyOUT As Integer = 0
        Dim ItemUnit As String = ""
        For i = 0 To lstItem.Count - 1
            sqlStr = "select Sum(quantity) from vw_stock where stockType='IN' and itemName='" & lstItem.Item(i) & "'"
            Dim sqlStr1 As String = "select top(1) unitName from vw_Stock where itemName='" & lstItem.Item(i) & "'"
            
            
            Try
                qtyIN = ExecuteQuery_ExecuteScalar(SqlStr)
            Catch ex As Exception
                qtyIN = 0
            End Try
            Try
                ItemUnit = ExecuteQuery_ExecuteScalar(sqlStr1)
            Catch ex As Exception

            End Try
            sqlStr = "select Sum(quantity) from vw_stock where stockType='OUT' and itemName='" & lstItem.Item(i) & "'"
            
            
            Try
                qtyOUT = ExecuteQuery_ExecuteScalar(SqlStr)
            Catch ex As Exception
                qtyOUT = 0
            End Try

            sqlStr = "insert into tmpStock Values('" & i + 1 & "','" & lstItem.Item(i) & "','" & ItemUnit & "','" & qtyIN - qtyOUT & "')"
            
            
            ExecuteQuery_Update(SqlStr)

        Next
        
        
        GridView2.DataBind()
    End Sub


End Class