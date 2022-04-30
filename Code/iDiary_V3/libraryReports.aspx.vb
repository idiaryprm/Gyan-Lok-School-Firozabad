Imports iDiary_V3.iDiary.CLS_idiary

Public Class libraryReports
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If rblreport.SelectedIndex = 0 Then
            GridView1.Visible = True
            GridView2.Visible = False
            lblName.Text = "Student Admn No "
            cboClass.Visible = False
            txtName.Visible = True

        ElseIf rblreport.SelectedIndex = 1 Then
            GridView1.Visible = True
            GridView2.Visible = False
            lblName.Text = "Employee Code"
            cboClass.Visible = False
            txtName.Visible = True

        ElseIf rblreport.SelectedIndex = 2 Then
            GridView1.Visible = False
            GridView2.Visible = True
            lblName.Text = "Class"
            txtName.Visible = False
            cboClass.Visible = True

        End If
        If Not IsPostBack Then
            InitControls()

        End If
    End Sub

    Public Sub InitControls()
        LoadMasterInfo(2, cboClass)
    End Sub
    Protected Sub rblreport_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblreport.SelectedIndexChanged
        lblSection.Visible = False
        cboSection.Visible = False

        If rblreport.SelectedIndex = 0 Then
            GridView1.Visible = True
            GridView2.Visible = False
            lblName.Text = "Student Admn No "
            cboClass.Visible = False
            txtName.Visible = True

        ElseIf rblreport.SelectedIndex = 1 Then
            GridView1.Visible = True
            GridView2.Visible = False
            lblName.Text = "Employee Code"
            cboClass.Visible = False
            txtName.Visible = True

        ElseIf rblreport.SelectedIndex = 2 Then
            GridView1.Visible = False
            GridView2.Visible = True
            lblName.Text = "Class"
            txtName.Visible = False
            cboClass.Visible = True
            cboClass.Text = ""
        End If
    End Sub

    Protected Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Dim sqlStr As String = ""
        If rblreport.SelectedIndex = 0 Then
            GridView1.Visible = True
            GridView2.Visible = False
            cboClass.Visible = False
            txtName.Visible = True
            lblName.Text = "Student Admn No"
            If cboBookStatus.SelectedIndex = 0 Then
                sqlStr = "SELECT [SName], [ClassName], [SecName], [AccNo], [BookTitle], [IssueDate], [ActualReturnDate],[Fine] FROM [vw_BookTransactStudent] WHERE ([RegNo] = '" & Trim(txtName.Text) & "') AND ActualReturnDate is NULL ORDER BY SName"
            Else
                sqlStr = "SELECT [SName], [ClassName], [SecName], [AccNo], [BookTitle], [IssueDate], [ActualReturnDate],[Fine] FROM [vw_BookTransactStudent] WHERE ([RegNo] = '" & Trim(txtName.Text) & "') AND ActualReturnDate is NOT NULL ORDER BY SName"
            End If
            SqlDataSource1.SelectCommand = sqlStr
            GridView1.DataSource = SqlDataSource1
            GridView1.DataBind()
        ElseIf rblreport.SelectedIndex = 1 Then
            GridView1.Visible = False
            GridView2.Visible = True
            cboClass.Visible = False
            txtName.Visible = True
            lblName.Text = "Employee Code"
            If cboBookStatus.SelectedIndex = 0 Then
                sqlStr = "SELECT [EmpName], [DeptName], [DesgName], [AccNo], [BookTitle], [IssueDate],[Fine] FROM [vw_BookTransactEmployee] WHERE ([EmpCode] = '" & Trim(txtName.Text) & "') AND ActualReturnDate is NULL ORDER BY EmpName"
            Else
                sqlStr = "SELECT [EmpName], [DeptName], [DesgName], [AccNo], [BookTitle], [IssueDate],[Fine] FROM [vw_BookTransactEmployee] WHERE ([EmpCode] = '" & Trim(txtName.Text) & "')  AND ActualReturnDate is NOT NULL ORDER BY EmpName"
            End If
            SqlDataSourceTeacher.SelectCommand = sqlStr
            GridView2.DataSource = SqlDataSourceTeacher
            GridView2.DataBind()
        ElseIf rblreport.SelectedIndex = 2 Then
            GridView1.Visible = True
            GridView2.Visible = False
            lblName.Text = "Class"
            txtName.Visible = False
            cboClass.Visible = True
            If cboBookStatus.SelectedIndex = 0 Then
                sqlStr = "SELECT [SName], [ClassName], [SecName], [AccNo], [BookTitle], [IssueDate], [ActualReturnDate],[Fine] FROM [vw_BookTransactStudent] WHERE ([ClassName] = '" & Trim(cboClass.Text) & "' AND SecName='" & cboSection.Text & "') AND ActualReturnDate is NULL ORDER BY SName"
            Else
                sqlStr = "SELECT [SName], [ClassName], [SecName], [AccNo], [BookTitle], [IssueDate], [ActualReturnDate],[Fine] FROM [vw_BookTransactStudent] WHERE ([ClassName] = '" & Trim(cboClass.Text) & "' AND SecName='" & cboSection.Text & "')  AND ActualReturnDate is NOT NULL ORDER BY SName"
            End If
            SqlDataSource1.SelectCommand = sqlStr
            GridView1.DataSource = SqlDataSource1
            GridView1.DataBind()
        End If
    End Sub

    Protected Sub cboClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboClass.SelectedIndexChanged
        cboSection.Visible = True
        lblSection.Visible = True
        LoadClassSection("", cboClass.Text, cboSection)
        cboClass.Focus()
    End Sub
End Class