Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Data
Imports System.Collections.Generic
Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.IO

Namespace AdmiDiary

    Public Class CLS_idiary



        Public Shared Function ExecuteQuery_UpdateOnline(strQuery As String) As Integer
            Dim myConnStr As String = ConfigurationManager.ConnectionStrings("iDiaryConnectionStringOnline").ToString
            Dim myConn As New SqlConnection(myConnStr)
            myConn.Open()
            Dim myCommand As New SqlCommand
            myCommand.Connection = myConn
            myCommand.CommandText = strQuery
            Try
                myCommand.ExecuteNonQuery()
            Catch ex As Exception
            Finally
                myConn.Close()
                myCommand.Dispose()
                myConn.Dispose()
            End Try
            Return 0
        End Function
        Public Shared Function ExecuteQuery_ExecuteScalarOnline(strQuery As String) As String
            Dim myConnStr As String = ConfigurationManager.ConnectionStrings("iDiaryConnectionStringOnline").ToString
            Dim myConn As New SqlConnection(myConnStr)
            myConn.Open()
            Dim myCommand As New SqlCommand
            myCommand.Connection = myConn
            myCommand.CommandText = strQuery
            Dim rv As String = ""
            Try
                rv = myCommand.ExecuteScalar()
            Catch ex As Exception
            Finally
                myConn.Close()
                myCommand.Dispose()
                myConn.Dispose()
            End Try

            Return rv
        End Function
        Public Shared Function ExecuteQuery_ExecuteReaderOnline(strQuery As String) As SqlDataReader
            Dim myConnStr As String = ConfigurationManager.ConnectionStrings("iDiaryConnectionStringOnline").ToString
            Dim myConn As New SqlConnection(myConnStr)
            myConn.Open()
            Dim myCommand As New SqlCommand
            myCommand.Connection = myConn
            myCommand.CommandText = strQuery
            Dim rv As SqlDataReader
            Try
                rv = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Catch ex As Exception
            Finally
                myCommand.Dispose()
            End Try
            Return rv
        End Function
        Public Shared Function ExecuteQuery_DataSetOnline(ByVal strQuery As String, ByVal cTableName As String) As DataSet
            Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionStringOnline").ToString
            Dim con As New System.Data.SqlClient.SqlConnection(myConnStr)
            Dim SqlCmd = New SqlCommand(strQuery, con)
            SqlCmd.CommandTimeout = 3000000

            Dim da As New SqlDataAdapter()
            da.SelectCommand = SqlCmd
            If con.State <> ConnectionState.Open Then
                con.Open()
            End If
            Dim ds As New DataSet()
            Try
                da.Fill(ds, cTableName)
            Catch ex As Exception
                'HttpContext.Current.Response.Write(" Error Web Msql Error ExecuteQuery : " );
                Throw (ex)
            Finally
                SqlCmd.Connection.Close()
                SqlCmd.Dispose()
                con.Close()
            End Try
            Return ds
        End Function


    End Class
End Namespace