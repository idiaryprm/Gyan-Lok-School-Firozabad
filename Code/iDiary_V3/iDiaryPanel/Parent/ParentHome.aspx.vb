Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Imports System.IO

Partial Class Parent_ParentHome
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadStudentInfo()
        If IsPostBack = False Then InitControls()
    End Sub
    Private Sub InitControls()
        'imgStudent.ImageUrl = "../../images/StudentDummy.jpg"
    End Sub
    Dim ASID As Int16 = 0
    Public Sub LoadStudentInfo()
        'Basic Details
        Dim FeeGroupID As String = ""
        Dim sqlStr As String = ""

        sqlStr = "Select * From vw_Student Where SID=" & Request.Cookies("SID").Value
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            lblRegNo.Text = myReader("RegNo")
            lblSname.Text = myReader("SName")
            lblClass.Text = myReader("ClassName") & "-" & myReader("SecName")
            lblMob.Text = myReader("MobNo")
            lblAddress.Text = myReader("FatherAddress")
            lblEmail.Text = myReader("EmailFather")
            Dim tmp As Date
            Try
                tmp = myReader("DOB")
                lblDOB.Text = tmp.ToString("dd/MM/yyyy")
            Catch ex As Exception

            End Try
            If myReader("Gender") = 0 Then
                lblFname.Text = "S/O : " & myReader("FName")
            Else
                lblFname.Text = "D/O : " & myReader("FName")
            End If
            FeeGroupID = myReader("FeeGroupID")
            ASID = myReader("ASID")

            imgStudent.ImageUrl = "../../" & myReader("PhotoPath")
        End While
        myReader.Close()

        'Fee
        Dim FeeDepositAmount As String = "0"
        Dim FeeAmount As String = ""
        sqlStr = "Select Sum(FeeDepositAmount) From vw_FeeDeposit Where SID=" & Request.Cookies("SID").Value & " And Year(DepositDate)=" & System.DateTime.Now.Year
        myReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            Try
                FeeDepositAmount = myReader(0)
            Catch ex As Exception

            End Try
        End While
        myReader.Close()
        sqlStr = "Select sum(FeeAmount) From vw_FeeConfig Where FeeGroupID=" & FeeGroupID & " AND ASID=" & ASID
        myReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            Try
                FeeAmount = myReader(0)
            Catch ex As Exception

            End Try
        End While
        myReader.Close()
        lblFee.Text = "Fee Deposited: " & FeeDepositAmount & "/- <br /> Total Fee: " & FeeAmount & "/-"

        'Monthly Attendance
        Dim rv As Integer = 0
        sqlStr = "Select Count(*) From vw_Attendance Where Year(AttDate)=" & System.DateTime.Now.Year & " And Month(AttDate)=" & System.DateTime.Now.Month & " And SID=" & Request.Cookies("SID").Value
        rv = ExecuteQuery_ExecuteScalar(sqlStr)
        sqlStr = "Select Count(*) From vw_Attendance Where Year(AttDate)=" & System.DateTime.Now.Year & " And Month(AttDate)=" & System.DateTime.Now.Month & " And SID=" & Request.Cookies("SID").Value & " And IsPresentM=1"
        Dim isPresent As Integer = ExecuteQuery_ExecuteScalar(sqlStr)
        lblAttendance.Text = "This Month = <b>" & isPresent & "</b> Present <br /> From <b>" & rv & "</b> Days"

        'Load Upcoming Activities
        sqlStr = "Select Count(*) From UpcomingActivities"
        Dim UpcomingActivity As Integer = ExecuteQuery_ExecuteScalar(sqlStr)
        lblActivities.Text = UpcomingActivity & " Upcoming Activities"

        'Load Unread Assignments
        sqlStr = "Select Count(*) From vw_Assignments Where SID=" & Request.Cookies("SID").Value & " AND IsRead=0"
        Dim unReadAssignments As Integer = ExecuteQuery_ExecuteScalar(sqlStr)
        lblUnreadAssignments.Text = unReadAssignments & " new assignments"

        'Load Unread Circulars
        sqlStr = "Select Count(*) From Circulars Where IsRead=0"
        Dim unReadCirculars As Integer = ExecuteQuery_ExecuteScalar(sqlStr)
        lblUnreadCircular.Text = unReadCirculars & " new Circulars"

        'Load Thoughts
        sqlStr = "SELECT TOP 1 Thought FROM vw_Thoughts ORDER BY NEWID()"
        Dim TodayThought As String = ExecuteQuery_ExecuteScalar(sqlStr)
        lblThought.Text = TodayThought

        sqlStr = "Select Top 3 * from AcademicCalender order by ACDate Desc"
        myReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            Dim tmpDate As Date
            Dim tmpDate2 As Date
            tmpDate = myReader("ACDate")
            lblDateFrom.Text &= tmpDate.ToString("dd/MM/yyyy")
            tmpDate2 = myReader("ACDateTo")
            'lblDateFrom.Text &= "  -  <span style=""color:blue"">"
            If tmpDate = tmpDate2 Then
                lblDateFrom.Text &= "  :  <span style=""color:blue"">"
            Else
                lblDateFrom.Text &= "  -  "
                lblDateFrom.Text &= tmpDate2.ToString("dd/MM/yyyy")
                lblDateFrom.Text &= "  :  <span style=""color:blue"">"
            End If
            lblDateFrom.Text &= myReader("AcDetails")
            lblDateFrom.Text &= "</span><br />"
        End While
        myReader.Close()
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Dim fp1 As String = myFile.PostedFile.FileName
        If fp1.ToString() <> "" Then
            Dim fn1 As String = fp1.Substring(fp1.LastIndexOf("\\") + 1)
            Dim sp1 As String = ""
            sp1 = Server.MapPath("~")
            If sp1.EndsWith("\\") = False Then
                sp1 += "Photos\"
            End If

            sp1 &= lblRegNo.Text.Replace("/", "") & "_" & ASID & ".jpg"
            myFile.PostedFile.SaveAs(sp1)
            imgStudent.ImageUrl = "~\" & "Photos\" & lblRegNo.Text.Replace("/", "") & "_" & ASID & ".jpg"
            Dim PhotoPath As String = imgStudent.ImageUrl.Replace("~\", "")
            Dim sqlStr As String = "update student set photopath='" & PhotoPath & "' Where  SID=" & Request.Cookies("SID").Value
            ExecuteQuery_Update(sqlStr)
        End If
    End Sub
End Class