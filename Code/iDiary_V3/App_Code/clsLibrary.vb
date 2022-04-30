Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports iDiary_V3.iDiary.CLS_idiary
Namespace iDiary
    Public Class clsLibrary

        Public Function LoadAuthorListAsList(ByRef myLst As ListBox) As Integer
            Dim sqlstr As String = "Select AuthorName From Authors Order By AuthorName"
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            myLst.Items.Clear()
            While myReader.Read
                myLst.Items.Add(myReader(0))
            End While
            myReader.Close()
            Return 0
        End Function

        Public Function GetNewAuthorID() As Integer
            Dim sqlstr As String = "Select Max(AuthorID) From Authors"
            Dim rv As Integer = 0
            Try
                rv = ExecuteQuery_ExecuteScalar(sqlstr)
            Catch ex As Exception
                rv = 0
            End Try
            Return rv + 1
        End Function

        Public Function GetAuthorID(ByVal myName As String) As Integer
            Dim sqlstr As String = "Select AuthorID From Authors Where AuthorName='" & myName & "'"
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
            Dim rv As Integer = 0
            While myReader.Read
                rv = myReader(0)
            End While
            myReader.Close()
            Return rv
        End Function

        Public Function LoadPublisherAsList(ByRef MyLst As ListBox) As Integer

            Dim sqlstr As String = "Select PubName From Publishers Order By PubName"

            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            MyLst.Items.Clear()
            While myReader.Read
                MyLst.Items.Add(myReader(0))
            End While
            myReader.Close()
            Return 0

        End Function

        Public Function LoadVenderAsList(ByRef MyLst As ListBox) As Integer
            Dim sqlstr As String = "Select VenderName From VenderMaster Order By VenderName"

            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            MyLst.Items.Clear()
            While myReader.Read
                MyLst.Items.Add(myReader(0))
            End While
            myReader.Close()

            Return 0

        End Function

        Public Function GetNewPublisherID() As Integer

            Dim sqlstr As String = "Select Max(PubID) From Publishers"
            Dim rv As Integer = 0
            Try
                rv = ExecuteQuery_ExecuteScalar(sqlstr)
            Catch ex As Exception
                rv = 0
            End Try
            Return rv + 1

        End Function

        Public Function GetPubID(ByVal myName As String) As Integer






            Dim sqlstr As String = "Select PubID From Publishers Where PubName='" & myName & "'"

            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
            Dim rv As Integer = 0
            While myReader.Read
                rv = myReader(0)
            End While
            myReader.Close()
            Return rv

        End Function

        Public Function LoadRackAsList(ByRef MyLst As ListBox) As Integer





            Dim sqlstr As String = "Select RackName From Racks Order By RackName"
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
            MyLst.Items.Clear()
            While myReader.Read
                MyLst.Items.Add(myReader(0))
            End While
            myReader.Close()


            Return 0

        End Function
        Public Function LoadNewsPaperAsList(ByRef MyLst As ListBox) As Integer





            Dim sqlstr As String = "Select NewsPaperName From NewsPaperMaster Order By NewsPaperName"
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
            MyLst.Items.Clear()
            While myReader.Read
                MyLst.Items.Add(myReader(0))
            End While
            myReader.Close()


            Return 0

        End Function
        Public Function LoadBooksAsList(ByRef MyLst As ListBox, SID As String) As Integer





            Dim sqlstr As String = "Select BookTitle,IssueDate From BookMaster inner join BookTransact on BookMaster.AccNo=BookTransact.AccNo where UniqueID= '" & SID & "' and ActualReturnDate=''  Order By IssueDate"
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
            MyLst.Items.Clear()
            While myReader.Read
                Dim a As Date = myReader("IssueDate").ToString
                MyLst.Items.Add(myReader(0) & " Issued On: " & a.ToString("dd/MM/yyyy"))
            End While
            myReader.Close()


            Return 0

        End Function

        Public Function GetNewRackID() As Integer





            Dim sqlstr As String = "Select Max(RackID) From Racks"
            Dim rv As Integer = 0
            Try
                rv = ExecuteQuery_ExecuteScalar(sqlstr)
            Catch ex As Exception
                rv = 0
            End Try

            Return rv + 1

        End Function

        Public Function GetNewsPaperID(ByVal myName As String) As Integer
            Dim sqlstr As String = "Select NewsPaperID From NewsPaperMaster Where NewsPaperName='" & myName & "'"
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
            Dim rv As Integer = 0
            While myReader.Read
                rv = myReader(0)
            End While
            myReader.Close()
            Return rv
        End Function

        Public Function GetBookLimit(ByVal myName As Integer, ByVal classID As Integer) As Integer
            Dim sqlstr As String = "Select limitBook From LibraryFineConfig Where Category=" & myName & " AND classID =" & classID
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
            Dim rv As Integer = 0
            While myReader.Read
                rv = myReader(0)
            End While
            myReader.Close()
            Return rv
        End Function

        Public Function GetDayLimit(ByVal myName As Integer, ByVal classID As Integer) As Integer

            Dim sqlstr As String = "Select limitDay From LibraryFineConfig Where Category=" & myName & " AND classID =" & classID


            Dim myReader As SqlDataReader = ExecuteQuery_Executereader(sqlstr)
            Dim rv As Integer = 0
            While myReader.Read
                rv = myReader(0)
            End While
            myReader.Close()



            Return rv
        End Function
        Public Function GetAmountFine(ByVal myName As Integer, ByVal classID As Integer) As Double






            Dim sqlstr As String = "Select AmountFine From LibraryFineConfig Where Category=" & myName & " AND classID =" & classID


            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
            Dim rv As Double = 0
            While myReader.Read
                rv = myReader(0)
            End While
            myReader.Close()
            Return rv
        End Function
        Public Function GetNewsPaperFrequency(ByVal myName As String) As String






            Dim sqlstr As String = "Select FrequencyName From NewsPaperMaster inner join FrequencyMaster on NewsPaperMaster.Frequency= FrequencyMaster.FrequencyId Where NewsPaperName='" & myName & "'"


            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
            Dim rv As String = ""
            While myReader.Read
                rv = myReader(0)
            End While
            myReader.Close()



            Return rv
        End Function
        Public Function GetRackID(ByVal myName As String) As Integer






            Dim sqlstr As String = "Select RackID From Racks Where RackName='" & myName & "'"


            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
            Dim rv As Integer = 0
            While myReader.Read
                rv = myReader(0)
            End While
            myReader.Close()



            Return rv
        End Function
        Public Function GetVenderID(ByVal myName As String) As Integer






            Dim sqlstr As String = "Select VenderID From VenderMaster Where VenderName='" & myName & "'"


            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
            Dim rv As Integer = 0
            While myReader.Read
                rv = myReader(0)
            End While
            myReader.Close()



            Return rv
        End Function
        Public Function GetFrequencyID(ByVal myName As String) As Integer






            Dim sqlstr As String = "Select FrequencyID From FrequencyMaster Where FrequencyName='" & myName & "'"


            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
            Dim rv As Integer = 0
            While myReader.Read
                rv = myReader(0)
            End While
            myReader.Close()



            Return rv
        End Function

        Public Function LoadBookCatAsList(ByVal myLst As ListBox) As Integer





            Dim sqlstr As String = "Select BookCatName From BookCategory Order By BookCatName"

            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            myLst.Items.Clear()
            While myReader.Read
                myLst.Items.Add(myReader(0))
            End While
            myReader.Close()



            Return 0

        End Function
        Public Function LoadBookLanguageAsList(ByVal myLst As ListBox) As Integer





            Dim sqlstr As String = "Select BooklanguageName From BooklanguageMaster Order By BooklanguageName"

            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            myLst.Items.Clear()
            While myReader.Read
                myLst.Items.Add(myReader(0))
            End While
            myReader.Close()



            Return 0

        End Function

        Public Function GetNewBookCatID() As Integer





            Dim sqlstr As String = "Select Max(BookCatID) From BookCategory"
            Dim rv As Integer = 0
            Try
                rv = ExecuteQuery_ExecuteScalar(sqlstr)
            Catch ex As Exception
                rv = 0
            End Try



            Return rv + 1

        End Function

        Public Function GetBookCatID(ByVal myName As String) As Integer






            Dim sqlstr As String = "Select BookCatID From BookCategory Where BookCatName='" & myName & "'"

            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
            Dim rv As Integer = 0
            While myReader.Read
                rv = myReader(0)
            End While
            myReader.Close()



            Return rv

        End Function

        Public Function GetBookLanguageID(ByVal myName As String) As Integer






            Dim sqlstr As String = "Select BookLanguageID From BookLanguageMaster Where BookLanguageName='" & myName & "'"

            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
            Dim rv As Integer = 0
            While myReader.Read
                rv = myReader(0)
            End While
            myReader.Close()



            Return rv

        End Function

        Public Function GetBookSubCatID(ByVal bookCatID As Integer, ByVal myName As String) As Integer






            Dim sqlstr As String = "Select BookSubCatID From BookSubCategory Where BookSubCatName='" & myName & "' AND BookCatID='" & bookCatID & "'"

            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
            Dim rv As Integer = 0
            While myReader.Read
                rv = myReader(0)
            End While
            myReader.Close()



            Return rv

        End Function

        Public Function LoadBookStatusAsList(ByVal myLst As ListBox) As Integer
            Dim sqlstr As String = "Select BookStatusName From BookStatus Order By BookStatusName"
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
            myLst.Items.Clear()
            While myReader.Read
                myLst.Items.Add(myReader(0))
            End While
            myReader.Close()
            Return 0

        End Function

        Public Function GetNewBookStatusID() As Integer





            Dim sqlstr As String = "Select Max(BookStatusID) From BookStatus"
            Dim rv As Integer = 0
            Try
                rv = ExecuteQuery_ExecuteScalar(sqlstr)
            Catch ex As Exception
                rv = 0
            End Try



            Return rv + 1

        End Function

        Public Function GetNewBookLanguageID() As Integer





            Dim sqlstr As String = "Select Max(BookLanguageID) From BookLanguageMaster"
            Dim rv As Integer = 0
            Try
                rv = ExecuteQuery_ExecuteScalar(sqlstr)
            Catch ex As Exception
                rv = 0
            End Try



            Return rv + 1

        End Function
        Public Function GetBookStatusID(ByVal myName As String) As Integer






            Dim sqlstr As String = "Select BookStatusID From BookStatus Where BookStatusName='" & myName & "'"

            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
            Dim rv As Integer = 0
            While myReader.Read
                rv = myReader(0)
            End While
            myReader.Close()



            Return rv

        End Function

        Public Function LoadLastAccNo() As String

           
           
           

            Dim sqlStr As String = ""
            

            sqlStr = "Select Max(LastAccNo) From Params"
            
            Dim rv As String = ""
            Try
                rv = ExecuteQuery_ExecuteScalar(SqlStr)
            Catch ex As Exception
                rv = ""
            End Try
            
            

            Return rv
        End Function

        Public Function LoadRackAsDropDown(ByRef myCbo As DropDownList) As Integer

           
           
           

            Dim sqlstr As String = "Select RackName From Racks Order By RackName"
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            myCbo.Items.Clear()
            While myReader.Read
                myCbo.Items.Add(myReader(0))
            End While
            myReader.Close()
            
            

            Return 0

        End Function

        Public Function LoadBookLanguageAsDropDown(ByRef myCbo As DropDownList) As Integer

           
           
           

            Dim sqlstr As String = "Select BookLanguageName From BookLanguageMaster Order By BookLanguageName"
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            myCbo.Items.Clear()
            While myReader.Read
                myCbo.Items.Add(myReader(0))
            End While
            myReader.Close()
            
            

            Return 0

        End Function

        Public Function LoadAuthorsAsDropDown(ByRef myCbo As DropDownList) As Integer

           
           
           

            Dim sqlstr As String = "Select AuthorName From Authors Order By AuthorName"
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            myCbo.Items.Clear()
            While myReader.Read
                myCbo.Items.Add(myReader(0))
            End While
            myReader.Close()
            
            

            Return 0

        End Function

        Public Function LoadPublishersAsDropDown(ByRef myCbo As DropDownList) As Integer

           
           
           

            Dim sqlstr As String = "Select PubName From Publishers Order By PubName"
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            myCbo.Items.Clear()
            While myReader.Read
                myCbo.Items.Add(myReader(0))
            End While
            myReader.Close()
            
            

            Return 0

        End Function
        Public Function LoadFrequencyAsDropDown(ByRef myCbo As DropDownList) As Integer

           
           
           

            Dim sqlstr As String = "Select FrequencyName From FrequencyMaster Order By FrequencyName"
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            myCbo.Items.Clear()
            While myReader.Read
                myCbo.Items.Add(myReader(0))
            End While
            myReader.Close()
            
            

            Return 0

        End Function

        Public Function LoadVendersAsDropDown(ByRef myCbo As DropDownList) As Integer

           
           
           

            Dim sqlstr As String = "Select VenderName From VenderMaster Order By VenderName"
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            myCbo.Items.Clear()
            While myReader.Read
                myCbo.Items.Add(myReader(0))
            End While
            myReader.Close()
            
            

            Return 0

        End Function
        Public Function FindNextAccessionNo(ByVal type As String) As String
            Dim sqlStr As String = ""
            sqlStr = "SELECT MAX(CONVERT(INT, SUBSTRING(AccNo, 2,LEN(AccNo)-1)) ) + 1 FROM BookMaster WHERE AccNo like '" & type & "%'"

            Dim myID As Integer = 0
            Try
                myID = ExecuteQuery_ExecuteScalar(sqlStr)
            Catch ex As Exception
                myID = 1
            End Try
            Dim NextAccNo = type + myID.ToString("0000")
            
            
            Return NextAccNo

        End Function

        Public Function LoadBookStatusAsDropDown(ByRef myCbo As DropDownList) As Integer

            Dim sqlstr As String = "Select BookStatusName From BookStatus Order By BookStatusName"
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            myCbo.Items.Clear()
            While myReader.Read
                myCbo.Items.Add(myReader(0))
            End While
            myReader.Close()
            
            

            Return 0

        End Function
        Public Function LoadSubCategoryAsDropDown(ByVal CategoryName As String, ByRef myLst As DropDownList) As Integer

            Dim sqlstr As String = "Select bookSubCatName From vw_Book_Sub_Category Where bookCatName='" & CategoryName & "' Order By bookSubCatName"
            
            Dim myReader As System.Data.SqlClient.SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            myLst.Items.Clear()
            While myReader.Read
                myLst.Items.Add(myReader(0))
            End While
            myReader.Close()

            Return 0

        End Function

        Public Function LoadBookCategoryAsDropDown(ByRef myCbo As DropDownList) As Integer

           
           
           

            Dim sqlstr = "Select BookCatName From BookCategory Order By BookCatName"
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            myCbo.Items.Clear()
            myCbo.Items.Add("")
            While myReader.Read
                myCbo.Items.Add(myReader(0))
            End While
            myReader.Close()
            
            

            Return 0

        End Function
        Public Shared Function checkLibraryMembership(ByVal codeID As Integer, memType As String) As Integer
            'memtype S--student,  E--Employee
            ' rv 0--not a member,  1--allowed,  2-not allowed

            Dim rv  As Integer = 0
           
           
           

            Dim sqlstr As String = ""
            If memType = "S" Then
                sqlstr = "Select top(1)  LibraryMemID from Student Where SID=" & codeID
            Else
                sqlstr = "Select top(1)  LibraryMemID from employeemaster Where EmpID=" & codeID
            End If
            Try
                rv = ExecuteQuery_ExecuteScalar(sqlstr)
            Catch ex As Exception

            End Try

            
            

            Return rv

        End Function

    End Class
End Namespace