Imports System.Net
Imports System.IO
Imports System.Data.SqlClient

Public Class Services

    Private connectionStringProvider As IConnectionStringProvider
    Private connectionString As String = ""

    Public Sub New(onnectionStringProvider As IConnectionStringProvider)
        '
        Me.connectionStringProvider = connectionStringProvider
        connectionString = connectionStringProvider.GetConnectionString("ConnectionString")
        '
    End Sub

    Public Function GetResponseString(url As String) As String
        Try
            ServicePointManager.SecurityProtocol = CType(3072, SecurityProtocolType)
            Dim request As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
            request.Method = "GET"

            Using response As WebResponse = request.GetResponse()
                Using reader As New StreamReader(response.GetResponseStream())
                    Return reader.ReadToEnd()
                End Using
            End Using
        Catch ex As WebException
            MessageBox.Show("WebException: " & ex.Message)
            If ex.Response IsNot Nothing Then
                Using reader As New StreamReader(ex.Response.GetResponseStream())
                    MessageBox.Show("Response: " & reader.ReadToEnd())
                End Using
            End If
            Return String.Empty
        Catch ex As Exception
            MessageBox.Show("Exception: " & ex.Message)
            Return String.Empty
        End Try
    End Function
    

End Class
