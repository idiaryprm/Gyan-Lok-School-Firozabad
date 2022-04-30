Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary
Partial Class Parent_ParentMaster
    Inherits System.Web.UI.MasterPage
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If IsNumeric(Request.Cookies("SID").Value.ToString()) = True Then
                'Allow
            Else
                Response.Redirect("../ParentLogin.aspx")
            End If
        Catch ex As Exception
            Response.Redirect("../ParentLogin.aspx")
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim sqlStr As String = ""
        Dim PhotoPath As String = ""
        Dim myClassID As Integer = 0, mySecID As Integer = 0
        sqlStr = "Select * From vw_Student Where SID=" & Request.Cookies("SID").Value
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            Try
                PhotoPath = myReader("PhotoPath")
                imgPhoto.ImageUrl = "../../" & PhotoPath
            Catch ex As Exception
                imgPhoto.ImageUrl = "../../images/StudentDummy.jpg"
            End Try
            'lblParentName.Text = "Panel for: " & myReader("FName")
            'lblStudentName.Text = "Parent of: " & myReader("SName")
            'lblClass.Text = myReader("ClassName") & "--" & myReader("SecName")
            myClassID = myReader("ClassID")
            mySecID = myReader("SecID")
            'ImageStudent.ImageUrl = "../../Photos/" & myReader("PhotoPath")
            'lblAddress.Text = myReader("FatherAddress")
            'lblMobile.Text = myReader("mobNo")
            'lblEmail.Text = myReader("EmailFather")
            Try
                'lblClassTeacher.Text = myReader("EmpName")
            Catch ex As Exception

            End Try
            Try
                'txtEmpID.Text = myReader("EmpID")
            Catch ex As Exception

            End Try
            Try
                'ImageClassTeacher.ImageUrl = "../../EmpPhotos/" & myReader("TeacherPhoto")
            Catch ex As Exception

            End Try
            imgSmall.ImageUrl = "../../" & PhotoPath
            'imgLarge.ImageUrl = "../../" & PhotoPath
        End While
        myReader.Close()

        sqlStr = "Select ParentPanelAS From Params"
        'myReader = ExecuteQuery_ExecuteReader(sqlStr)
        Dim rv As String = ExecuteQuery_ExecuteScalar(sqlStr)
        'While myReader.Read
        '    rv = myReader(0)
        'End While
        'myReader.Close()

        sqlStr = "Select ASName From AcademicSession Where ASID=" & rv
        lblSession.Text = "Academic Session : " & ExecuteQuery_ExecuteScalar(sqlStr)
        
        ''Load School Name, Parent Name, Student Details (All Details Common to all pages)
        'Dim sqlStr As String = ""
        'Dim myConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("iDiaryConnectionString").ToString
        'Dim myConn As New SqlConnection(myConnStr)
        'myConn.Open()

        'Dim myCommand As New SqlCommand

        ''Display School Name
        ''sqlStr = "Select SchoolName From Schools Where SchoolID=" & Session("SchoolID")
        ''myCommand.CommandText = sqlStr
        ''myCommand.Connection = myConn
        ''Dim SchoolReader As SqlDataReader = myCommand.ExecuteReader
        ''While SchoolReader.Read
        ''    lblSchoolName.Text = SchoolReader("SchoolName")
        ''End While
        ''SchoolReader.Close()

        ''Display Student Details
        'Dim myClassID As Integer = 0, mySecID As Integer = 0
        'sqlStr = "Select SName, FName, ClassID,SecID, FatherAddress, mobNo, EmailFather, ClassName,SecName, PhotoPath,Empname,EmpID,TeacherPhoto From vw_Student Where SID=" & Session("SID")
        'myCommand.CommandText = sqlStr
        'myCommand.Connection = myConn
        'Dim StudentReader As SqlDataReader = myCommand.ExecuteReader
        'While StudentReader.Read
        '    lblParentName.Text = "Panel for: " & StudentReader("FName")
        '    lblStudentName.Text = "Parent of: " & StudentReader("SName")
        '    lblClass.Text = StudentReader("ClassName") & "--" & StudentReader("SecName")
        '    myClassID = StudentReader("ClassID")
        '    mySecID = StudentReader("SecID")
        '    ImageStudent.ImageUrl = "../../Photos/" & StudentReader("PhotoPath")
        '    lblAddress.Text = StudentReader("FatherAddress")
        '    lblMobile.Text = StudentReader("mobNo")
        '    lblEmail.Text = StudentReader("EmailFather")
        '    Try
        '        lblClassTeacher.Text = StudentReader("EmpName")
        '    Catch ex As Exception

        '    End Try
        '    Try
        '        txtEmpID.Text = StudentReader("EmpID")
        '    Catch ex As Exception

        '    End Try
        '    Try
        '        ImageClassTeacher.ImageUrl = "../../EmpPhotos/" & StudentReader("TeacherPhoto")
        '    Catch ex As Exception

        '    End Try
        'End While
        'StudentReader.Close()
        'txtSecID.Text = mySecID
        ''display Teachers details
        ''sqlStr = "select Empname, photopath,classTeacher from vwteachers where secid=" & mySecID
        ''myCommand.CommandText = sqlStr
        ''myCommand.Connection = myConn
        ''Dim teacherreader As SqlDataReader = myCommand.ExecuteReader
        ''While teacherreader.Read
        ''    lblClassTeacher.Text = teacherreader("EmpName")
        ''    'imageclassteacher.imageurl = "../teacherphotos/" & teacherreader("photopath")
        ''End While
        ''teacherreader.Close()

        ''Load Updates
        ''sqlStr = "Select UpdateDetails From Updates Where SchoolID=" & Session("SchoolID")
        ''myCommand.CommandText = sqlStr
        ''myCommand.Connection = myConn
        ''lblUpdates.Text = ""
        ''Dim UpdateReader As SqlDataReader = myCommand.ExecuteReader
        ''While UpdateReader.Read
        ''    lblUpdates.Text &= UpdateReader("UpdateDetails") & "     "
        ''End While
        ''UpdateReader.Close()

        'myCommand.Dispose()
        'myConn.Dispose()
        'SqlDataSourceMates.SelectCommand = "SELECT [SName], [Photopath] FROM [vw_Student] WHERE SecID = " & txtSecID.Text & " and SID <>" & Session("SID")
        'ListView2.DataBind()
    End Sub


    'Public ReadOnly Property parentName() As String
    '    Get
    '        Return DirectCast(txtParent.Text, String)
    '    End Get
    'End Property

    'Public Shared Function PhotoPath(ByVal path As String) As String
    '    Dim tmp As String = "../../EmpPhotos/" & path
    '    Return tmp
    'End Function

    'Public Shared Function PhotoPathStudent(ByVal path As String) As String
    '    Dim tmp As String = "../../Photos/" & path
    '    Return tmp
    'End Function

   
End Class

