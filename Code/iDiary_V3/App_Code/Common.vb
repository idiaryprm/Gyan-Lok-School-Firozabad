Imports System.Collections.Generic
Imports System.Security.Cryptography
Imports System.Web
Imports System.IO
Imports System.Text
Imports System.ComponentModel
Imports System.Globalization
Public Class Common
    '
    ' TODO: Add constructor logic here
    '
    Public Sub New()
    End Sub
    Public Shared Function SortNameValueCollection(nvc As NameValueCollection) As SortedDictionary(Of String, String)
        Dim sortedDict As New SortedDictionary(Of String, String)()
        For Each key As [String] In nvc.AllKeys
            sortedDict.Add(key, nvc(key))
        Next
        Return sortedDict
    End Function

    Public Shared Function GetAppConfig(Key As String) As String
        Return GetAppConfig(Key, "")
    End Function

    Public Shared Function GetAppConfig(Key As String, defaultValue As String) As String
        Dim appConfigValue As String = ""
        Dim AppValue As String = System.Configuration.ConfigurationManager.AppSettings(Key)
        If String.IsNullOrEmpty(AppValue) Then
            If Not String.IsNullOrEmpty(defaultValue) Then
                appConfigValue = defaultValue
            Else
                appConfigValue = ""
            End If
        Else
            appConfigValue = AppValue
        End If
        Return appConfigValue
    End Function

    Public Shared Function GetConfigAlgorithm(key As String) As Crypto.Algorithm
        Return GetConfigAlgorithm(key, "")
    End Function

    ''' <summary>
    ''' Supported Algorithm: SHA1, SHA256, SHA384, MD5 and SHA512
    ''' </summary>
    ''' <param name="key"></param>
    ''' <param name="defaultValue"></param>
    ''' <returns></returns>
    Public Shared Function GetConfigAlgorithm(key As String, defaultValue As String) As Crypto.Algorithm
        Dim ConfigValue As String = GetAppConfig(key, defaultValue)
        Dim algorithm As New Crypto.Algorithm()
        If Not String.IsNullOrEmpty(ConfigValue) Then
            Select Case ConfigValue.ToLower()
                Case "sha1"
                    algorithm = Crypto.Algorithm.SHA1
                    Exit Select
                Case "sha256"
                    algorithm = Crypto.Algorithm.SHA256
                    Exit Select
                Case "sha384"
                    algorithm = Crypto.Algorithm.SHA384
                    Exit Select
                Case "sha512"
                    algorithm = Crypto.Algorithm.SHA512
                    Exit Select
                Case "md5"
                    algorithm = Crypto.Algorithm.MD5
                    Exit Select
                Case Else
                    Throw New ArgumentException("Invalid algorithm configured in configuration", "Algorithm")
            End Select
        Else
            Throw New ArgumentException("Invalid algorithm configured in configuration", "Algorithm")
        End If
        Return algorithm
    End Function
End Class
