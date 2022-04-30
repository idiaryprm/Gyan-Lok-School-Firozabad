Imports System.Collections.Generic
Imports System.Security.Cryptography
Imports System.Web
Imports System.IO
Imports System.Text
Imports System.ComponentModel
Imports System.Globalization
Public Class Crypto
    'Algorithm types
    Public Enum Algorithm As Integer
        SHA1 = 0
        SHA256 = 1
        SHA384 = 2
        MD5 = 9
        SHA512 = 12
    End Enum

    'Encoding Types
    Public Enum EncodingType As Integer
        HEX = 0
        BASE_64 = 1
    End Enum

    'Error messages
    Private Const ERR_NO_CONTENT As String = "No content was provided"
    Private Const ERR_INVALID_PROVIDER As String = "An invalid cryptographic provider was specified for this method"

    ''' <summary>
    ''' Crypto constructor
    ''' </summary>
    Public Sub New()
    End Sub

    ''' <summary>
    ''' Generates hash string for given value and algorith with encoding type
    ''' Supported Algorithm: SHA1, SHA256, SHA384, MD5 and SHA512
    ''' Supported Encoding Types: HEX and BASE_64
    ''' </summary>
    ''' <param name="Content">Content for hash</param>
    ''' <param name="algorithm">Supported Algorithm: SHA1, SHA256, SHA384, MD5 and SHA512</param>
    ''' <param name="et">Supported Encoding Types: HEX and BASE_64</param>
    ''' <returns>Generated hash</returns>
    Public Shared Function GenerateHashString(Content As String, algorithm__1 As Algorithm, et As EncodingType) As String
        Dim _content As String
        If Content Is Nothing OrElse Content.Equals(String.Empty) Then
            Throw New CryptographicException(ERR_NO_CONTENT)
        End If

        Dim hashAlgorithm As HashAlgorithm = Nothing

        Select Case algorithm__1
            Case Algorithm.SHA1
                hashAlgorithm = New SHA1CryptoServiceProvider()
                Exit Select
            Case Algorithm.SHA256
                hashAlgorithm = New SHA256Managed()
                Exit Select
            Case Algorithm.SHA384
                hashAlgorithm = New SHA384Managed()
                Exit Select
            Case Algorithm.SHA512
                hashAlgorithm = New SHA512Managed()
                Exit Select
            Case Algorithm.MD5
                hashAlgorithm = New MD5CryptoServiceProvider()
                Exit Select
            Case Else
                Throw New CryptographicException(ERR_INVALID_PROVIDER)
        End Select

        Try
            Dim hash As Byte() = ComputeHash(hashAlgorithm, Content)
            If et = EncodingType.HEX Then
                _content = BytesToHex(hash)
            Else
                _content = System.Convert.ToBase64String(hash)
            End If
            hashAlgorithm.Clear()
            Return _content
        Catch cge As CryptographicException
            Throw cge
        Catch ex As Exception
            Throw ex
        Finally
            hashAlgorithm.Clear()
        End Try
    End Function

#Region "Utility Functions"
    ''' <summary>
    ''' Compute Hash for given algorithm and content
    ''' </summary>
    ''' <param name="Provider">Hash algorithm</param>
    ''' <param name="plainText">Content</param>
    ''' <returns>Computed hash</returns>
    Private Shared Function ComputeHash(Provider As HashAlgorithm, plainText As String) As Byte()
        Dim hash As Byte() = Provider.ComputeHash(System.Text.Encoding.UTF8.GetBytes(plainText))
        Provider.Clear()
        Return hash
    End Function

    ''' <summary>
    ''' Converts a byte array to a hex-encoded string
    ''' </summary>
    ''' <param name="bytes">Array of bytes</param>
    ''' <returns>Converted hex-encoded string</returns>
    Private Shared Function BytesToHex(bytes As Byte()) As String
        Dim hex As New StringBuilder()
        For n As Integer = 0 To bytes.Length - 1
            hex.AppendFormat("{0:X2}", bytes(n))
        Next
        Return hex.ToString()
    End Function

    ''' <summary>
    ''' Converts a hex-encoded string to bytes array
    ''' </summary>
    ''' <param name="hexString">Hex-encoded string</param>
    ''' <returns>Array of bytes</returns>
    Private Shared Function ConvertHexStringToByteArray(hexString As String) As Byte()
        If hexString.Length Mod 2 <> 0 Then
            Throw New ArgumentException([String].Format(CultureInfo.InvariantCulture, "The binary key cannot have an odd number of digits: {0}", hexString))
        End If

        Dim HexAsBytes As Byte() = New Byte(hexString.Length \ 2 - 1) {}
        For index As Integer = 0 To HexAsBytes.Length - 1
            Dim byteValue As String = hexString.Substring(index * 2, 2)
            HexAsBytes(index) = Byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture)
        Next

        Return HexAsBytes
    End Function
#End Region
End Class
