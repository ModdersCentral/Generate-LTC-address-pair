Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Text.RegularExpressions
'//add your own error checks 
'//im not your personal coder. or am i?
Public Class Form1
    Public Function litecoin(ByVal URL As String, ByVal contnt As String)
        Dim gdf As Byte() = Encoding.UTF8.GetBytes(contnt)
        Dim Req As HttpWebRequest
        Dim SourceStream As System.IO.Stream
        Dim Response As HttpWebResponse

        Try

            'create a web request to the URL  
            Req = HttpWebRequest.Create("http://192.168.0.11:9432")
            Req.Credentials = New NetworkCredential("litecoinerzunite", "youwillneverguessthispass")

            Req.ContentType = "application/json-rpc"
            Req.Method = "POST"
            Dim dataStream As Stream = Req.GetRequestStream()
            dataStream.Write(gdf, 0, gdf.Length)
            dataStream.Close()
            Dim webResponlse As WebResponse = Req.GetResponse()
            Dim loResponseStream As StreamReader = New StreamReader(webResponlse.GetResponseStream())
            Return loResponseStream.ReadToEnd()
        Catch ex As Exception
            MsgBox(ex.Message)
            Return "0"

        End Try
    End Function
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '''''''''''''''''''''''''
        MsgBox(getpair)
    End Sub
    Function getpair()
        Dim address As String = String.Empty
        Dim privkey As String = String.Empty
        Dim getaddress As String = litecoin("", "{""method"":""getnewaddress"",""params"":[""""],""id"":1}")
        Dim theaddress As Match = Regex.Match(getaddress, "{""result"":""([A-Za-z0-9\-]+)"",""error"":null,""id"":1}")
        If (theaddress.Success) Then
            address = (theaddress.Groups(1).Value)
        End If
        '''''''''''''''''''''''''
        Dim privatekey As String = litecoin("", "{""method"":""dumpprivkey"",""params"":[""" & address & """],""id"":1}")
        Dim theakey As Match = Regex.Match(privatekey, "{""result"":""([A-Za-z0-9\-]+)"",""error"":null,""id"":1}")
        If (theakey.Success) Then
            privkey = (theakey.Groups(1).Value)
        End If
        Return "address: " & address & vbNewLine & "privkey: " & privkey
    End Function
End Class
