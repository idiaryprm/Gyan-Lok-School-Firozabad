Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class FeeTypeConfig
    Inherits System.Web.UI.Page


    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        Try

            If Request.Cookies("UType").Value.ToString.Contains("Fee") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
                'Allow
            Else
                Response.Redirect("/./AccessDenied.aspx", False)
            End If

        Catch ex As Exception

            If ex.Message.Contains("Object reference not set to an instance of an object") Then
                Response.Redirect("~/Logout.aspx")
            End If

        End Try

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If cboAddmissionFee.Text = "" Then
            lblStatus.Text = "Invalid Addmission Fee Type"
            cboAddmissionFee.Focus()
            Exit Sub
        End If
        If cboLateFee.Text = "" Then
            lblStatus.Text = "Invalid Late Fee Type"
            cboLateFee.Focus()
            Exit Sub
        End If
        If cboTutionFee.Text = "" Then
            lblStatus.Text = "Invalid Tution Fee Type"
            cboTutionFee.Focus()
            Exit Sub
        End If
        
        'If cboConveyanceFee.Text = "" Then
        '    lblStatus.Text = "Invalid Conveyance Fee Type"
        '    cboConveyanceFee.Focus()
        '    Exit Sub
        'End If

        Dim AddmissionFeeID As Integer = FindMasterID(11, cboAddmissionFee.Text)
        Dim LateFeeID As Integer = FindMasterID(11, cboLateFee.Text)
        Dim ConveyanceFeeID As Integer = FindMasterID(11, cboConveyanceFee.Text)
        Dim TutionFeeID As Integer = FindMasterID(11, cboTutionFee.Text)

        Dim ArrearFeeID As Integer = FindMasterID(11, cboArrear.Text)
        Dim ExcessFeeID As Integer = FindMasterID(11, cboExcessFeeID.Text)
        Dim sqlStr As String = ""

        sqlStr = "Update Params Set AddmissionFeeID=" & AddmissionFeeID & ", LateFeeID=" & LateFeeID & ",ConveyanceFeeID=" & ConveyanceFeeID & ",TutionFeeID=" & TutionFeeID & ",ArrearFeeID=" & ArrearFeeID & ",ExcessFeeID=" & ExcessFeeID
        ExecuteQuery_Update(sqlStr)
        lblStatus.Text = "Information Updated Successfully..."
        InitControls()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            InitControls()
        End If
        If Request.Cookies("UType").Value.ToString.Contains("Admin-1") = False And Request.Cookies("UType").Value.ToString.Contains("Fee-1") = False Then
            btnSave.Enabled = False
        End If
    End Sub

    Private Sub InitControls()

        LoadMasterInfo(11, cboAddmissionFee)
        LoadMasterInfo(11, cboLateFee)
        LoadMasterInfo(11, cboTutionFee)
        LoadMasterInfo(11, cboConveyanceFee)
        LoadMasterInfo(11, cboArrear)
        LoadMasterInfo(11, cboExcessFeeID)

        Dim sqlStr As String = ""
        sqlStr = "Select AddmissionFeeID,LateFeeID,ConveyanceFeeID,TutionFeeID,ArrearFeeID,ExcessFeeID From Params"
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            Try
                cboAddmissionFee.Text = FindMasterName(11, myReader(0))
                cboLateFee.Text = FindMasterName(11, myReader(1))
                cboConveyanceFee.Text = FindMasterName(11, myReader(2))
                cboTutionFee.Text = FindMasterName(11, myReader(3))
            Catch ex As Exception

            End Try
            Try
                cboArrear.Text = FindMasterName(11, myReader(4))
                cboExcessFeeID.Text = FindMasterName(11, myReader(5))
            Catch ex As Exception

            End Try
        End While
        myReader.Close()
        cboAddmissionFee.Focus()
    End Sub

End Class