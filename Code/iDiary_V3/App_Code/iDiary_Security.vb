Imports System.Data.SqlClient
Imports System.Data
Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI.Control
Imports iDiary_V3.iDiary.CLS_idiary

Namespace iDiary_Security

    Public Class CLS_iDiary_Security

        '---Load System Created Groups---
        Public Shared Function LoadGroups(ByRef myChk As CheckBoxList) As Integer

           
           
           

            Dim sqlStr As String = ""
            

            sqlStr = "Select GroupName From Groups Where GroupCreatedBy=0"
            
            
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            myChk.Items.Clear()
            While myReader.Read
                myChk.Items.Add(myReader(0))
            End While
            myReader.Close()
            
            

            Return 0

        End Function

        '---Check Login Exists or Not---
        Public Shared Function CheckLoginAvailability(ByVal LoginID As String) As Integer

           
           
           

            Dim sqlStr As String = ""
            
            sqlStr = "Select Count(LoginID) From Users where LoginID='" & LoginID & "'"
            
            
            Dim myCount As Integer = 0
            myCount = ExecuteQuery_ExecuteScalar(SqlStr)
            
            

            Return myCount

        End Function

        Public Shared Function LoadUsers(ByRef myCbo As DropDownList) As Integer

           
           
           
            Dim sqlStr As String = ""
            sqlStr = "Select LoginID From Users"

            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            myCbo.Items.Clear()
            myCbo.Items.Add("")
            While myReader.Read
                myCbo.Items.Add(myReader(0))
            End While
            myReader.Close()
            
            

            Return 0

        End Function

    End Class

End Namespace

