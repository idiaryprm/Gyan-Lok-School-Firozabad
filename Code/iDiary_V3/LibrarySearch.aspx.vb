Imports System.Data.SqlClient
Imports iDiary_V3.iDiary.CLS_idiary

Public Class LibrarySearch
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Request.Cookies("UType").Value.ToString.Contains("Library") Or Request.Cookies("UType").Value.ToString.Contains("Admin") Then
            'Allow
        Else
            Response.Redirect("AccessDenied.aspx")
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            InitControls()
        Else
            'For Grid View Printing. Must have a blank HTM Page (gview.htm)
            Dim printScript As String = "function PrintGridView() { var gridInsideDiv = document.getElementById('gvDiv');" & _
            " var printWindow = window.open('gview.htm','PrintWindow','letf=0,top=0,width=150,height=300,toolbar=1,scrollbars=1,status=1');" & _
            " printWindow.document.write(gridInsideDiv.innerHTML);printWindow.document.close();printWindow.focus();" & _
            " printWindow.print();printWindow.close();}"
            Me.ClientScript.RegisterStartupScript(Page.[GetType](), "PrintGridView", printScript.ToString(), True)
            btnPrint.Attributes.Add("onclick", "PrintGridView();")
        End If
    End Sub

    Private Sub InitControls()
        Dim ObjLib As New iDiary.clsLibrary
        chkAccNo.Checked = False
        GridView1.Visible = False
        'LoadMasterInfo(2, cboClass)
        'txtRegNo.Text = ""
        txtAccessionNo.Text = ""
        chkTitle.Checked = False
        txtTitle.Text = ""
        chkCodeNo.Checked = False
        txtCodeNo.Text = ""
        chkAuthor.Checked = False
        txtAuthor.Text = ""
        chkPublisher.Checked = False
        txtPublisher.Text = ""
        chkBookCategory.Checked = False
        ObjLib.LoadBookCategoryAsDropDown(cboBookCategory)
        chkIssued.Checked = False
        chkstatus.Checked = False
        ObjLib.LoadBookStatusAsDropDown(cboStatus)
        ObjLib = Nothing
    End Sub

    Protected Sub btnFind_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFind.Click
        lblStatus.Text = ""
        If chkAccNo.Checked = False And chkTitle.Checked = False And chkCodeNo.Checked = False And chkAuthor.Checked = False And chkPublisher.Checked = False And chkBookCategory.Checked = False And chkIssued.Checked = False And chkstatus.Checked = False Then
            lblStatus.Text = "Please Select atleast one Display criterian to continue..."
            Exit Sub
        End If
        If chkAccNo.Checked = True And txtAccessionNo.Text.Length <= 0 Then
            lblStatus.Text = "Provide Accession No."
            txtAccessionNo.Focus()
            Exit Sub
        End If

        If chkTitle.Checked = True And txtTitle.Text.Length <= 0 Then
            lblStatus.Text = "Provide Book Title."
            txtTitle.Focus()
            Exit Sub
        End If

        If chkCodeNo.Checked = True And txtCodeNo.Text.Length <= 0 Then
            lblStatus.Text = "Provide Code No."
            txtCodeNo.Focus()
            Exit Sub
        End If

        If chkAuthor.Checked = True And txtAuthor.Text.Length <= 0 Then
            lblStatus.Text = "Provide Author"
            txtAuthor.Focus()
            Exit Sub
        End If

        If chkPublisher.Checked = True And txtPublisher.Text.Length <= 0 Then
            lblStatus.Text = "Provide Publisher Name"
            txtPublisher.Focus()
            Exit Sub
        End If
        If chkBookCategory.Checked = True And Trim(cboBookCategory.Text) = "" Then
            lblStatus.Text = "Provide Book Category Name"
            cboBookCategory.Focus()
            Exit Sub
        End If
        'If chkBookSubCategory.Checked = True And Trim(cboBookSubCategory.Text) = "" Then
        '    lblStatus.Text = "Provide Book Sub-Category Name"
        '    cboBookSubCategory.Focus()
        '    Exit Sub
        'End If

        Dim sqlStr As String = ""
        'Dim ANDFlag As Boolean = False
        If rbBook.Checked = True Then
            sqlStr = "Select BookAccNo,BookTitle,BookCodeNo,AuthorName, PubName ,issued From vw_BookMaster "
        Else
            sqlStr = "Select AccNo,BookTitle,BookCodeNo,AuthorName, PubName,issued From vw_BookMaster "
        End If
        If rbBook.Checked = True Then
            sqlStr += "Where AccNo like 'B%'"
            'sqlStr += "Where AccNo is not null"
        ElseIf rbMagazine.Checked = True Then
            sqlStr += "Where AccNo like 'M%'"
        Else
            sqlStr += "Where AccNo like 'D%'"
        End If


        Dim myHeaderText As String = "Search Parameter  "
        If rbBook.Checked = True Then
            sqlStr = "Select Distinct AccNo,convert(int,BookAccNo ) as BookAccNo,BookTitle,BookCodeNo, PubName ,issued= case issued when 0 then 'Not issued' else 'Issued' End From vw_BookMaster "
        Else
            sqlStr = "Select Distinct AccNo,AccNo as bookAccno,BookTitle,BookCodeNo, PubName,issued= case issued when 0 then 'Not issued' else 'Issued' End From vw_BookMaster "
        End If
        If rbBook.Checked = True Then
            myHeaderText &= " for Books :"
            sqlStr += "Where AccNo like 'B%'"
        ElseIf rbMagazine.Checked = True Then
            myHeaderText &= " for Magazines :"
            sqlStr += "Where AccNo like 'M%'"
        Else
            myHeaderText &= " for Digital Media :"
            sqlStr += "Where AccNo like 'D%'"
        End If


        If chkAccNo.Checked = True Then
            'If ANDFlag = True Then sqlStr &= ""
            If rbBook.Checked = True Then
                sqlStr &= "  AND BookAccNo='" & txtAccessionNo.Text & "' "
                myHeaderText &= "Acc No=" & txtAccessionNo.Text & ","
            End If

            'ANDFlag = True
        End If

        If chkTitle.Checked = True Then
            'If ANDFlag = True Then sqlStr &= ""
            sqlStr &= "  AND BookTitle LIKE '%" & txtTitle.Text & "%' "
            myHeaderText &= "Book Title=" & txtTitle.Text & ","
            'ANDFlag = True
        End If

        If chkCodeNo.Checked = True Then
            'If ANDFlag = True Then sqlStr &= ""
            sqlStr &= " AND BookCodeNo='" & txtCodeNo.Text & "' "
            myHeaderText &= "Book Code=" & txtCodeNo.Text & ","
            'ANDFlag = True
        End If

        If chkAuthor.Checked = True Then
            'If ANDFlag = True Then sqlStr &= " AND "
            sqlStr &= " AND AuthorName LIKE '%" & txtAuthor.Text & "%' "
            myHeaderText &= "Author Name=" & txtAuthor.Text & ","
            'ANDFlag = True
        End If

        If chkPublisher.Checked = True Then
            'If ANDFlag = True Then sqlStr &= " AND "
            sqlStr &= " AND PubName='" & txtPublisher.Text & "' "
            myHeaderText &= "Publisher=" & txtPublisher.Text & ","
            'ANDFlag = True
        End If

        If chkBookCategory.Checked = True Then
            'If ANDFlag = True Then sqlStr &= " AND "
            sqlStr &= " AND BookCatName='" & cboBookCategory.Text & "' "
            myHeaderText &= "Book Category=" & cboBookCategory.Text & ","
            'ANDFlag = True
        End If

        If chkIssued.Checked = True Then
            'If ANDFlag = True Then sqlStr &= " AND "
            sqlStr &= " AND Issued=" & cboIssued.SelectedIndex & " "
            myHeaderText &= "Issued=" & cboIssued.SelectedIndex & ","
            'ANDFlag = True
        End If

        If chkstatus.Checked = True Then
            'If ANDFlag = True Then sqlStr &= " AND "
            sqlStr &= " AND BookStatusName='" & cboStatus.Text & "' "
            myHeaderText &= "Book Status=" & cboStatus.Text & ","
            'ANDFlag = True
        End If
       
        sqlStr &= " Order By AccNo"

        Dim bookIssue As String = ""
        Dim AccNo As String = "", BookTitle As String = "", BookCodeNo As String = "", AuthorName As String = "", PubName As String = ""

        Dim ds As New DataSet
        ds = ExecuteQuery_DataSet(sqlStr, "tbl")
        ds.Tables(0).Columns.Add("Authors", GetType(String))
        Dim nDS As New DataSet
        'Dim AccNo As String = ""
        Dim AuthorName1 As String = "", AuthorName2 As String = ""

        For Each Row As DataRow In ds.Tables(0).Rows
            AccNo = Row("AccNo")
            Row("Authors") = getAuthors(AccNo)
        Next
        Try
            lblTitle.Text = myHeaderText.Substring(0, myHeaderText.Length - 1)
        Catch ex As Exception

        End Try

        GridView1.DataSource = ds
        GridView1.Visible = True
        GridView1.DataBind()
        lblStatus.Text = "Total " & GridView1.Rows.Count & " Books Found"
    End Sub

    Private Function getAuthors(accNo As String) As String
        Dim rv As String = ""
        Dim sqlStr As String = "SELECT dbo.Authors.AuthorName, dbo.BookAuthors.AccNo FROM dbo.BookAuthors INNER JOIN dbo.Authors ON dbo.BookAuthors.AuthorID = dbo.Authors.AuthorID Where ACCNO='" & accNo & "'"
     
        Dim myReader As SqlDataReader = ExecuteQuery_ExecuteReader(sqlStr)
        While myReader.Read
            rv &= myReader(0) & ","
        End While
        myReader.Close()
      
        Try
            rv = rv.Substring(0, rv.Length - 1)
        Catch ex As Exception

        End Try
        Return rv
    End Function


    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub

    'Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        Dim print As Button = CType(e.Row.FindControl("btnDetails"), Button)
    '        print.Attributes.Add("onclick", "javascript:window.open('Printslip.aspx?ID=" + theID + "','');")
    '    End If
    'End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If (e.CommandName = "details") Then
            ' Retrieve the row index stored in the CommandArgument property.
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)

            ' Retrieve the row that contains the button 
            ' from the Rows collection.
            Dim row As GridViewRow = GridView1.Rows(index)
            Dim url As String = "libraryBookHistory.aspx?AccNo=" & row.Cells(1).Text
            ' Add code here to add the item to the shopping cart.
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "OpenWindow", "window.open('" & url & "','_newtab');", True)
        End If
    End Sub
End Class