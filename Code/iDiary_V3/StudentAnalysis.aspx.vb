Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports iDiary_V3.iDiary_Fee.CLS_iDiary_Fee

Public Class StudentAnalysis
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then InitControls()
    End Sub
    Private Sub InitControls()
        txtSRNo.Text = ""
        txtName.Text = ""
        txtSRNo.Focus()
        GridView1.Visible = False
    End Sub
    Protected Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        If txtSRNo.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid SR/Admin No.');", True)
            txtSRNo.Focus()
            Exit Sub
        End If
        For Each gvr As GridViewRow In GridView1.Rows
            Dim url As String = ""
            Dim StudentNotesID As Integer = GridView1.DataKeys(gvr.RowIndex).Value.ToString()
            Dim sqlStr As String = "select NoteDocPath from StudentNotes where StudentNotesID='" & StudentNotesID & "'"

            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
            While myReader.Read
                Dim imagebutton2 As ImageButton = DirectCast(gvr.FindControl("imagebutton2"), ImageButton)
                url = myReader(0)
                If url = "" Then
                    imagebutton2.Visible = False
                Else
                    imagebutton2.Visible = True
                End If
            End While
            myReader.Close()
        Next
        Dim TempSRNo As String = txtSRNo.Text
        InitControls()
        txtSRNo.Text = TempSRNo
        ShowStudentRecord(1, txtSRNo.Text)
        StudentAttendence()
    End Sub
    Private Sub ShowStudentRecord(ByVal SearchType As Integer, SearchVal As String)
        Dim FeeGroupID As String = ""
        Dim sqlstr As String = ""
        If SearchType = 1 Then
            sqlstr = "Select * From vw_Student Where RegNo='" & SearchVal & "' AND ASID=" & Request.Cookies("ASID").Value
        ElseIf SearchType = 2 Then
            sqlstr = "Select * From vw_Student Where SName='" & SearchVal & "' AND ASID=" & Request.Cookies("ASID").Value
        End If
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
        While myReader.Read
            txtID.Text = myReader("SID")
            txtSRNo.Text = myReader("RegNo")
            lblSName.Text = myReader("SName")
            lblClassSec.Text = myReader("ClassName") & " - " & myReader("SecName")
            'lblRollNo.Text = myReader("ClassRollNo")
            lblHouse.Text = myReader("HouseName")
            Dim tmpDate As Date = Now.Date
            Try
                tmpDate = myReader("AdmissionDate")
                lblAdmissionDate.Text = tmpDate.ToString("dd/MM/yyyy")
            Catch ex As Exception

            End Try
            'lblFeeGroup.Text = myReader("FeeGroupName")
            lblFeeBookNo.Text = myReader("FeeBookNo")
            lblFatherName.Text = myReader("FName")
            'lblMotherName.Text = myReader("MName")
            'txtReceiptNo.Text = myReader("ReceiptNo")
            tmpDate = myReader("DOB")
            lblDob.Text = tmpDate.ToString("dd/MM/yyyy")
            lblAddress.Text = myReader("FatherAddress")
            lblMob.Text = myReader("MobNo")
            FeeGroupID = myReader("FeeGroupID")
        End While
        myReader.Close()
        StudentFees(Val(FeeGroupID), txtID.Text)
    End Sub

    Protected Sub btnNameSearch_Click(sender As Object, e As EventArgs) Handles btnNameSearch.Click
        SqlDataSource2.SelectCommand = "SELECT RegNo, SName, ClassName, SecName FROM vw_Student WHERE ASID = " & Request.Cookies("ASID").Value & " AND SName Like '%" & txtName.Text & "%'"
        GridView2.DataBind()
        GridView2.Visible = True
    End Sub
    Public Sub StudentAttendence()
        Dim rv As Integer = 0
        Dim sqlstr As String = ""
        sqlstr = "SELECT MONTH(AttDate) myMONTH, COUNT(*) myCOUNT FROM vw_Attendance WHERE Year(AttDate) = " & System.DateTime.Now.Year & " And RegNo = '" & txtSRNo.Text & "' And IsPresentM=1 GROUP BY MONTH(AttDate) order by myMonth"
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
        While myReader.Read
            Try
                CType(MyDiv.FindControl("lblM" & myReader("myMONTH")), Label).Text = myReader("myCount")
            Catch ex As Exception

            End Try
            ' lblApril.Text = myReader("myCount") & "%"
        End While
        myReader.Close()

        sqlstr = "SELECT MONTH(AttDate) myMONTH, COUNT(*) myCOUNT FROM vw_Attendance WHERE Year(AttDate) = " & System.DateTime.Now.Year & " And RegNo = '" & txtSRNo.Text & "' GROUP BY MONTH(AttDate) order by myMonth"
        myReader = ExecuteQuery_ExecuteReader(sqlstr)
        While myReader.Read
            Try
                CType(MyDiv.FindControl("lblTM" & myReader("myMONTH")), Label).Text = myReader("myCount")
            Catch ex As Exception

            End Try
        End While
        myReader.Close()
    End Sub
    Public Sub StudentNotes(ByVal RegNo As String)
        Dim Sid As Integer = GetSID(RegNo, Request.Cookies("ASID").Value)
        Try
            GridView1.Visible = True
            StudentNotepad.SelectCommand = "SELECT StudentNotesID,EntryDate,Comments, NoteDocPath FROM StudentNotes WHERE SID='" & Sid & "' and Isdeleted=0"

            GridView1.DataBind()
        Catch ex As Exception

        End Try
    End Sub
    Public Sub StudentFees(ByVal FeeGroupID As Integer, ByVal SID As String)
        Dim sqlstr As String = ""
        Dim i As Integer
        Dim htmlText As String = ""
        Dim Count As Integer = 0
        Dim TermNoList As New List(Of String)
        Dim lstMonthID As New List(Of String)
        htmlText &= ""
        Dim quote As String
        quote = Chr(34)

        If FeeGroupID > 0 Then
            sqlstr = "Select TermNo,MonthID From TermMaster Where FeeGroupID=" & FeeGroupID & " order by DisplayOrder"
        Else
            sqlstr = "Select Distinct TermNo,MonthID,DisplayOrder From TermMaster order by DisplayOrder"
        End If
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
        While myReader.Read
            TermNoList.Add(myReader("TermNo"))
            lstMonthID.Add(myReader("MonthID"))
            Count += 1
        End While
        myReader.Close()

        Dim j As Integer = 0
        Dim Width As Integer = 4
        For i = 1 To TermNoList.Count
            htmlText &= "<div id=" & quote & "colum" & j & quote & " style=" & quote & "left: " & Width & "%; background-color: red;" & quote & " class=" & quote & "colu" & quote & "></div>"
            j = j + 1
            htmlText &= "<div id=" & quote & "colum" & j & quote & " style=" & quote & "left: " & Width & "%; background-color: yellow;" & quote & " class=" & quote & "colu" & quote & "></div>"
            j = j + 1
            htmlText &= "<div id=" & quote & "colum" & j & quote & " style=" & quote & "left: " & Width & "%; background-color: #37C464;" & quote & " class=" & quote & "colu2" & quote & "></div>"
            j = j + 1
            Width = (Width + 62 / Count)
        Next
        Dim DivWidth As Integer = 8
        For i = 0 To TermNoList.Count - 1
            htmlText &= "<div class=" & quote & "riga1" & quote & " style=" & quote & "top: 100%; left: " & DivWidth & "%" & quote & ">"
            htmlText &= "<div>" & TermNoList.Item(i) & "</div>"
            htmlText &= "</div>"
            DivWidth += 62 / Count
        Next
        htmlText &= "</div>"

        Dim Sum As Integer = 0
        Dim tmp As String = ""
        Dim HTMLTag As String = ""
        Dim DivYaxis As Integer = 0
        HTMLTag &= "<table id=" & quote & "MyGraph1" & quote & " style=" & quote & "margin-top: 30px; margin-left: 30px" & quote & ">"
        For i = 0 To TermNoList.Count - 1
            sqlstr = "Select sum(FeeAmount) From vw_FeeConfig Where ASID=" & Request.Cookies("ASID").Value & " AND FeeGroupID=" & FeeGroupID & " And MonthID in (" & lstMonthID.Item(i) & ")"
            myReader = ExecuteQuery_ExecuteReader(sqlstr)
            While myReader.Read
                Try
                    Sum = myReader(0)
                Catch ex As Exception
                    Sum = 0
                End Try

                HTMLTag &= "<tr><td>Fee Config</td><td>" & Sum & "</td></tr>"
            End While
            myReader.Close()
            Dim Sum1 As String = ""
            sqlstr = "Select sum(FeeDepositAmount),Sum(ConcessionAmount) From vw_FeeDeposit Where ASID=" & Request.Cookies("ASID").Value & " AND TermNo='" & TermNoList.Item(i) & "' And SID='" & SID & "'"
            myReader = ExecuteQuery_ExecuteReader(sqlstr)
            While myReader.Read
                Try
                    Sum1 = myReader(0) + myReader(1)
                Catch ex As Exception
                    Sum1 = ""
                End Try
                Try
                    Dim ab As Integer = myReader(1)
                    HTMLTag &= "<tr><td>Fee Concession</td><td>" & Sum1 & "</td></tr>"
                Catch ex As Exception
                    HTMLTag &= "<tr><td>Fee Concession</td><td>0</td></tr>"
                End Try

                Sum1 = ""
                Sum1 = myReader(0).ToString
                HTMLTag &= "<tr><td>Fee Deposit</td><td>" & Sum1 & "</td></tr>"
            End While
            myReader.Close()
        Next
        HTMLTag &= "</table>"
        tmp &= "<div id=" & quote & "grafico2" & quote & ">"
        Dim valu As Integer = 100
        DivYaxis = 3000
        For j = 0 To 3
            valu = valu - 20
            tmp &= "<div class=" & quote & "riga2" & quote & " style=" & quote & "top: " & valu & "%" & quote & "><div>" & DivYaxis & "</div></div>"
            DivYaxis += 3000
        Next
        htmlText = htmlText.Insert(0, tmp)
        TermNo.InnerHtml = htmlText
        Division.InnerHtml = HTMLTag
    End Sub

    Protected Sub GridView2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView2.SelectedIndexChanged
        txtSRNo.Text = GridView2.SelectedRow.Cells(1).Text
        If txtSRNo.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid SR/Admin No.');", True)
            txtSRNo.Focus()
            Exit Sub
        End If
        Dim TempSRNo As String = txtSRNo.Text
        InitControls()
        txtSRNo.Text = TempSRNo
        ShowStudentRecord(1, txtSRNo.Text)
        StudentAttendence()
        StudentNotes(txtSRNo.Text)
        GridView2.Visible = False
        txtSRNo.Text = ""
    End Sub
End Class