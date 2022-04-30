Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class FeeChqDisHonoured
    Inherits System.Web.UI.Page
    Dim sqlstr As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("ActiveTab") = 3
        If Not IsPostBack Then

            InitControls()
        End If
    End Sub

    Private Sub InitControls()
        txtChequeAmount.Text = ""
        txtDishonouredDate.Text = Now.Date.ToString("dd/MM/yyyy")
        txtClass.Text = ""
        txtDepositDate.Text = Now.Date.ToString("dd/MM/yyyy")
        txtFeeBookNo.Text = ""
        txtFName.Text = ""
        txtID.Text = ""
        txtSearchCheque.Text = ""
        txtSection.Text = ""
        txtSName.Text = ""
        cboChequeDishonour.SelectedIndex = 0
        txtSearchCheque.Focus()
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If Trim(txtSearchCheque.Text) = "" Then
            lblStatus.Text = "Invalid Cheque No."
            txtSearchCheque.Focus()
            Exit Sub
        End If
        If Trim(txtID.Text) = "" Then
            lblStatus.Text = "Invalid Cheque No."
            txtSearchCheque.Focus()
            Exit Sub
        End If
        If txtDishonouredDate.Text = "" Then
            lblStatus.Text = "Invalid Cheque Dishonoured Date."
            txtDishonouredDate.Focus()
            Exit Sub
        End If
        Dim DishonouredDate As String = txtDishonouredDate.Text.Split("/")(2) & "/" & txtDishonouredDate.Text.Split("/")(1) & "/" & txtDishonouredDate.Text.Split("/")(0)
        Try
            Dim a As Date = CDate(DishonouredDate)
        Catch ex As Exception
            lblStatus.Text = "Invalid Cheque Dishonoured Date."
            txtDishonouredDate.Focus()
            Exit Sub
        End Try
        Dim Dishounoured As Integer = 0
        If cboChequeDishonour.Text = "Yes" Then
            Dishounoured = 1
        End If
        sqlstr = "Update FeeDeposit Set dishonourDate='" & DishonouredDate & "', isDishonoured='" & Dishounoured & "', isDeposit='" & cboChequeDishonour.SelectedIndex & "' Where FeeDepositID='" & txtID.Text & "'"
        ExecuteQuery_Update(sqlstr)

        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Error", "alert('Cheque Status has been saved...');", True)
        InitControls()
    End Sub
    Protected Sub btnNext_Click1(sender As Object, e As EventArgs) Handles btnNext.Click
        txtID.Text = ""
        If Trim(txtSearchCheque.Text) = "" Then
            lblStatus.Text = "Invalid Cheque No."
            txtSearchCheque.Focus()
            Exit Sub
        End If

        Dim feeAmount As Double = 0
        sqlstr = "select * from vw_FeeDeposit Where ChequeNo='" & Trim(txtSearchCheque.Text) & "'"
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlstr)
        While myReader.Read
            feeAmount += myReader("FeeDepositAmount")
            txtChequeAmount.Text = feeAmount
            txtClass.Text = myReader("Classname")
            txtFeeBookNo.Text = myReader("FeeBookNo")
            txtFName.Text = myReader("FName")
            txtSection.Text = myReader("Secname")
            txtSName.Text = myReader("SName")
            Dim tmpdate As Date = myReader("chequedate")
            txtDepositDate.Text = tmpdate.ToString("dd/MM/yyyy")
            txtID.Text = myReader("FeeDepositID")
            If myReader("isDishonoured") = "1" Then
                cboChequeDishonour.Text = "Yes"
            Else
                cboChequeDishonour.Text = "No"
            End If
            txtChequeBank.Text = myReader("ChequeBank")
        End While
        myReader.Close()
        If Trim(txtID.Text) = "" Then
            lblStatus.Text = "Invalid Cheque No."
            txtSearchCheque.Focus()
            Exit Sub
        End If
    End Sub
End Class