
Partial Class Parent_ParentLogout
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If HttpContext.Current IsNot Nothing Then
            Dim cookieCount As Integer = HttpContext.Current.Request.Cookies.Count
            For i As Integer = 0 To cookieCount - 1
                Dim cookie = HttpContext.Current.Request.Cookies(i)
                If cookie IsNot Nothing Then
                    Dim cookieName = cookie.Name
                    Dim expiredCookie = New HttpCookie(cookieName) With {
                         .Expires = DateTime.Now.AddDays(-1)
                    }
                    ' overwrite it
                    HttpContext.Current.Response.Cookies.Add(expiredCookie)
                End If
            Next

            ' clear cookies server side
            HttpContext.Current.Request.Cookies.Clear()
        End If
        Response.Redirect("ParentLogin.aspx")
    End Sub
End Class
