Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports iDiary_V3.iDiary.CLS_idiary

Public Class StudentEnquiryConfirm
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("ActiveTab") = 1
        If IsPostBack = False Then
            Initcontrols()
        Else

        End If
    End Sub
    Public Sub Initcontrols()
        LoadMasterInfo(71, cboSchoolName, Request.Cookies("SchoolIDs").Value)
        LoadMasterInfo(2, cboClass, cboSchoolName.Text)
        btnSave.Visible = False
        lblSection.Visible = False
        lblSubSection.Visible = False
        cboTmpSec.Visible = False
        cboTmpSubSec.Visible = False
        'btnSave.Visible = False
        cboClass.Focus()
    End Sub

    Protected Sub cboSec_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim cboSec As DropDownList = TryCast(sender, DropDownList)
        Dim gvRowIndex As Integer = Convert.ToInt32(cboSec.Attributes("RowIndex"))
        CheckGvMyTable(gvRowIndex, cboSec.Text)
    End Sub
    Private Sub CheckGvMyTable(gvRowIndex As Integer, SecName As String)
        Dim cboSubSec As DropDownList = DirectCast(GridView1.Rows(gvRowIndex).FindControl("cboSubSec"), DropDownList)
        LoadClassSubSection("", cboClass.Text, SecName, cboSubSec)
        cboSubSec.Focus()
    End Sub
    Protected Sub btnNext_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNext.Click
        If cboClass.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Select Class...');", True)
            cboClass.Focus()
            Exit Sub
        End If
        SqlDataSource1.SelectCommand = "SELECT * FROM [vw_StudentEnquiry] Where ASID='" & Request.Cookies("ASID").Value & "' and statusName='Selected' and ClassName='" & cboClass.Text & "' and IsAdminFeeDeposit=1 and FeeBookNo <> '' and (IsAdmitted is null or IsAdmitted=0)"
        GridView1.DataBind()
        If GridView1.Rows.Count > 0 Then
            GridView1.Visible = True
            btnSave.Visible = True
            lblSection.Visible = True
            lblSubSection.Visible = True
            cboTmpSec.Visible = True
            cboTmpSubSec.Visible = True
            'Stop Individual Sec/Sub Section
            'For Each gvr As GridViewRow In GridView1.Rows
            '    Dim cboSec As DropDownList = DirectCast(gvr.FindControl("cboSec"), DropDownList)
            '    cboSec.Items.Clear()
            '    For i = 0 To cboTmpSec.Items.Count - 1
            '        cboSec.Items.Add(cboTmpSec.Items(i).Text)
            '    Next
            '    cboSec.Text = cboTmpSec.Text
            '    If cboSec.Text <> "" Then
            '        Dim cboSubSec As DropDownList = DirectCast(gvr.FindControl("cboSubSec"), DropDownList)
            '        cboSubSec.Items.Clear()
            '        For i = 0 To cboTmpSubSec.Items.Count - 1
            '            cboSubSec.Items.Add(cboTmpSubSec.Items(i).Text)
            '        Next
            '        cboSubSec.Text = cboTmpSubSec.Text
            '    End If
            'Next
        Else
            GridView1.Visible = False
            btnSave.Visible = False
            lblSection.Visible = False
            lblSubSection.Visible = False
            cboTmpSec.Visible = False
            cboTmpSubSec.Visible = False
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('No Record Found...');", True)
        End If
    End Sub

    Protected Sub cboClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboClass.SelectedIndexChanged
        LoadClassSection(cboSchoolName.Text, cboClass.Text, cboTmpSec)
    End Sub

    Protected Sub cboTmpSec_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTmpSec.SelectedIndexChanged
        LoadClassSubSection(cboSchoolName.Text, cboClass.Text, cboTmpSec.Text, cboTmpSubSec)
    End Sub

    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.RowType = DataControlRowType.DataRow Then
                Try
                    Dim txtRegNo As TextBox = e.Row.FindControl("txtRegNo")
                    Dim EnquiryID As Integer = Convert.ToInt32(GridView1.DataKeys(e.Row.RowIndex).Values(0))
                    Dim RegNo As String = Convert.ToString(GridView1.DataKeys(e.Row.RowIndex).Values(1))
                    txtRegNo.Text = RegNo
                Catch ex As Exception

                End Try
            End If
        End If
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim RegNo As String = "'"
        Dim COUNTReg As Integer = 0
        For i = 0 To GridView1.Rows.Count - 1
            Dim chk As CheckBox = DirectCast(GridView1.Rows(i).FindControl("chkSelect"), CheckBox)
            If chk.Checked = True And GridView1.Rows(i).Visible = True Then
                Try
                    Dim txtRegNo As TextBox = DirectCast(GridView1.Rows(i).FindControl("txtRegNo"), TextBox)
                    If txtRegNo.Text <> "" Then
                        RegNo += txtRegNo.Text & "','"
                    Else
                        GridView1.Rows(i).BackColor = Drawing.Color.OrangeRed
                        COUNTReg = 1
                    End If
                Catch ex As Exception

                End Try
            End If
        Next
        If RegNo = "'" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please Select atleast one Student!!!');", True)
            Exit Sub
        End If
        If COUNTReg = 1 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Please enter Reg No!!!   Please Check Reg No of Colored rows...');", True)
            Exit Sub
        End If
        If RegNo <> "'" Then
            RegNo = RegNo.Substring(0, RegNo.Length - 2)
        End If
        Dim sqlstr As String = ""
        If RegNo <> "'" Then
            sqlstr = "select RegNo from Student where ASID='" & Request.Cookies("ASID").Value & "'"
            If RegNo <> "'" Then
                sqlstr += " and RegNo in (" & RegNo & ")"
            End If
            Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
            RegNo = ""
            While myReader.Read
                RegNo += myReader("RegNo") & ","
            End While
            myReader.Close()
            Dim Count As Integer = 0
            If RegNo <> "" Then
                RegNo = RegNo.Substring(0, RegNo.Length - 1)
                Dim tmpRegNo() As String = RegNo.Split(",")
                For j = 0 To tmpRegNo.Count - 1
                    For i = 0 To GridView1.Rows.Count - 1
                        Dim txtRegNo As TextBox = DirectCast(GridView1.Rows(i).FindControl("txtRegNo"), TextBox)
                        If txtRegNo.Text = tmpRegNo(j) And txtRegNo.Text <> "" Then
                            GridView1.Rows(i).BackColor = Drawing.Color.OrangeRed
                            Count = 1
                        End If
                    Next
                Next
            End If
            If Count = 1 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Reg No!!!   Please Check Reg No of Colored rows...');", True)
                Exit Sub
            End If

        End If
        Dim CSSID As Integer = FindCSSID(cboSchoolName.Text, cboClass.Text, cboTmpSec.Text, cboTmpSubSec.Text)
        If CSSID = 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Class Sec SubSection Configuration!!!');", True)
            Exit Sub
        End If

        For i = 0 To GridView1.Rows.Count - 1
            GridView1.Rows(i).BackColor = Drawing.Color.White
        Next
        Dim onlychild As Integer = 0
        Dim RelID As Integer = FindMasterID(5, FindDefault(5))
        Dim CasteID As Integer = FindMasterID(6, FindDefault(6))
        Dim HouseID As Integer = FindMasterID(4, FindDefault(4))
        Dim StateID As Integer = FindMasterID(35, FindDefault(35))
        Dim NationalityID As Integer = FindMasterID(28, FindDefault(28))
        Dim BGID As Integer = FindMasterID(16, FindDefault(16))
        Dim StatusID As Integer = FindMasterID(10, FindDefault(10))
        Dim PhotoPath As String = ""
        Dim FOccID As Integer = FindMasterID(7, FindDefault(7))
        Dim FDeptID As Integer = FindMasterID(8, FindDefault(8))
        Dim FDesgID As Integer = FindMasterID(9, FindDefault(9))
        Dim MOccID As Integer = FindMasterID(7, FindDefault(7))
        Dim MDeptID As Integer = FindMasterID(8, FindDefault(8))
        Dim MDesgID As Integer = FindMasterID(9, FindDefault(9))
        Dim CategoryID As Integer = FindMasterID(34, FindDefault(34))
        Dim BoardID As Integer = FindMasterID(37, FindDefault(37))
        Dim MotherTongueID As Integer = FindMasterID(41, FindDefault(41))
        Dim SchoolID As Integer = FindMasterID(71, cboSchoolName.Text)
        Dim Counter As Integer = 0

        For i = 0 To GridView1.Rows.Count - 1
            Dim chk As CheckBox = DirectCast(GridView1.Rows(i).FindControl("chkSelect"), CheckBox)
            If chk.Checked = True And GridView1.Rows(i).Visible = True Then
                'Dim cboSec As DropDownList = DirectCast(GridView1.Rows(i).FindControl("cboSec"), DropDownList)
                'Dim cboSubSec As DropDownList = DirectCast(GridView1.Rows(i).FindControl("cboSubSec"), DropDownList)
                Dim txtRegNo As TextBox = DirectCast(GridView1.Rows(i).FindControl("txtRegNo"), TextBox)
                Dim EnquiryID As Integer = Convert.ToInt32(GridView1.DataKeys(i).Values(0))
                'Dim SecName As String = cboSec.Text
                'Dim SubSecName As String = cboSubSec.Text
                RegNo = txtRegNo.Text
                Dim FeeBookNo As String = GridView1.Rows(i).Cells(6).Text
                Dim FormNo As String = GridView1.Rows(i).Cells(2).Text
                Dim ASID As Integer = Request.Cookies("ASID").Value
                Dim Address As String = Convert.ToString(GridView1.DataKeys(i).Values(2))
                Dim SName As String = GridView1.Rows(i).Cells(3).Text
                Dim AdmissionDate As Date = Now.Date
                Dim adminDate As String = GridView1.Rows(i).Cells(8).Text
                Try
                    AdmissionDate = CDate(adminDate.Substring(6, 4) & "/" & adminDate.Substring(3, 2) & "/" & adminDate.Substring(0, 2))
                Catch ex As Exception
                End Try
                Dim Gender As Integer = 0
                Try
                    Gender = Convert.ToInt32(GridView1.DataKeys(i).Values(4))
                Catch ex As Exception

                End Try
                Dim DOB As Date = Now.Date
                Dim dobtmp As String = Convert.ToString(GridView1.DataKeys(i).Values(5))
                Try
                    DOB = CDate(dobtmp.Substring(6, 4) & "/" & dobtmp.Substring(3, 2) & "/" & dobtmp.Substring(0, 2))
                Catch ex As Exception
                End Try
                Dim FName As String = GridView1.Rows(i).Cells(3).Text
                Dim MName As String = Convert.ToString(GridView1.DataKeys(i).Values(3))
                Dim MobNo As String = GridView1.Rows(i).Cells(7).Text
                Dim Email As String = Convert.ToString(GridView1.DataKeys(i).Values(6))
                'Dim CSSID As Integer = FindCSSID(cboClass.Text, SecName, SubSecName)
                'If CSSID = 0 Then
                '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Invalid Class Sec SubSection Configuration!!!   Please Check for Colored rows...');", True)
                '    GridView1.Rows(i).BackColor = Drawing.Color.OrangeRed
                '    Continue For
                'End If
                'Save_Log("STUDENT INSERT")
                'Insert Records into Student Table
                sqlstr = "Insert into StudentBasicInfo " & _
                "(ReceiptNo,FormNo,SName,BloodGroupID,MotherTougueID,AdmissionDate,Gender,RelID,CasteID,DOB,FName,MName,GuardianName,GuardianRelation,GuardianAddress,"
                sqlstr += "GuardianMobile,FatherAddress,District,StateID,PINCODE,PhoneResd,PhoneOfficeFather,PhoneOfficeMother,MobNo,EmailFather,EmailMother,CategoryID,onlychild,"
                sqlstr += "StatusID,LastSchoolName,LastSchoolAddress,LastSchoolBoardID,LastSchoolPercentage,FOccID,FDeptID,FDesgID,FIncome,FEmpNo,MOccID,MDeptID,MDesgID,MIncome,MEmpNo,"
                sqlstr += "NationalityID,AadharNo,UserID,IsDeleted,EnquiryID) Values(" & _
                "''," & _
                "'" & SQLFixup(FormNo) & "'," & _
                "'" & SQLFixup(SName) & "'," & _
                BGID & "," & _
                MotherTongueID & "," & _
                "'" & AdmissionDate.ToString("yyyy/MM/dd") & "'," & _
                Gender & "," & _
                RelID & "," & _
                CasteID & "," & _
                "'" & DOB.ToString("yyyy/MM/dd") & "'," & _
                "'" & SQLFixup(FName) & "'," & _
                "'" & SQLFixup(MName) & "'," & _
                "''," & _
                "''," & _
                "''," & _
                "''," & _
                "''," & _
                "''," & _
                "'" & StateID & "'," & _
                "''," & _
                "''," & _
                "''," & _
                "''," & _
                "'" & SQLFixup(MobNo) & "'," & _
                "'" & SQLFixup(Email) & "'," & _
                "''," & _
                "'" & CategoryID & "'," & _
                "'" & onlychild & "'," & _
                StatusID & "," & _
                "''," & _
                "''," & _
                "'" & BoardID & "'," & _
                "''," & _
                FOccID & "," & _
                FDeptID & "," & _
                FDesgID & "," & _
                0 & "," & _
                "''," & _
                MOccID & "," & _
                MDeptID & "," & _
                MDesgID & "," & _
               0 & "," & _
                "''," & _
                NationalityID & "," & _
                "''," & _
                Request.Cookies("UserID").Value & ",0," & EnquiryID & ")"
                ExecuteQuery_Update(sqlstr)

                Dim TempStudentID As Integer = 0
                sqlstr = "Select Max(StudentID) from StudentBasicInfo where SName='" & SName & "' and MobNo='" & MobNo & "' and FName='" & FName & "'"
                TempStudentID = ExecuteQuery_ExecuteScalar(sqlstr)

                sqlstr = "Insert into Student" & _
               "(SchoolID,StudentID,RegNo,ClassRollno,FeeBookNo,CSSID,HouseID,ASID,Promoted,PhotoPath,FeeConfigType,UserID) Values(" & _
 "'" & SchoolID & "'," & _
 "'" & TempStudentID & "'," & _
                "'" & SQLFixup(RegNo) & "'," & _
                "'0'," & _
                "'" & SQLFixup(FeeBookNo) & "'," & _
                CSSID & "," & _
                HouseID & "," & _
                ASID & "," & _
                "0," & _
                "'" & PhotoPath & "'," & _
                0 & "," & _
                Request.Cookies("UserID").Value & ")"
                ExecuteQuery_Update(sqlstr)
                sqlstr = "update studentenquiry set IsAdmitted=1 where enquiryid=" & EnquiryID
                ExecuteQuery_Update(sqlstr)
                Counter += 1
                GridView1.Rows(i).Visible = False
                chk.Checked = False
            End If

        Next
        'GridView1.DataBind()
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('" & Counter & " Students has been Admitted succesfully...');", True)

        'For i = 0 To GridView1.Rows.Count - 1
        '    GridView1.Rows(i).BackColor = Drawing.Color.White
        'Next
    End Sub

    Protected Sub cboSchoolName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSchoolName.SelectedIndexChanged
        LoadMasterInfo(2, cboClass, cboSchoolName.Text)
        cboSchoolName.Focus()
    End Sub
End Class