Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Data.SqlClient

Class NetworkCheck
    'Private errorlabel As ErrorLabel

    'Public Sub New(errorlabel As ErrorLabel)
    'Me.errorlabel = errorlabel
    'End Sub
    Private connectionStringProvider As IConnectionStringProvider
    Private connectionString As String = ""
    Public Sub New(connectionStringProvider As IConnectionStringProvider)
        '
        Me.connectionStringProvider = connectionStringProvider
        connectionString = connectionStringProvider.GetConnectionString("ConnectionString")
        '
    End Sub
    Public Function checknetwork() As Boolean
        If My.Computer.Network.IsAvailable Then
            If IsServerAvalable() Then
                Return True
            Else
                Return False
            End If
            Return False
        Else
            'If errorlabel IsNot Nothing Then
            '    errorlabel.ShowMessage("Kompiuteris neprijungtas prie tinklo", False)
            'Else
            MessageBox.Show("Kompiuteris neprijungtas prie tinklo")
            'End If
            Return False
        End If
    End Function
    Private Function IsServerAvalable() As Boolean
        Try
            Dim connectionString As String = ""
            If String.IsNullOrEmpty(connectionString) Then
                connectionString = connectionString & ";Connect Timeout=5"
            Else
                connectionString = connectionStringProvider.GetConnectionString("ConnectionString") & ";Connect Timeout=5"
            End If
            Dim testQuery As String = "SELECT 1"
            Using connection As New SqlConnection(connectionString)
                connection.Open()
                Using command As New SqlCommand(testQuery, connection)
                    command.ExecuteScalar()
                End Using
                Return True
            End Using
        Catch ex As Exception
            'If errorlabel IsNot Nothing Then
            '    errorlabel.ShowMessage("Nepavyksta prisijungti prie serverio: " + ex.Message, False)
            'Else
            MessageBox.Show("Nepavyksta prisijungti prie serverio: ")
            'End If

            Return False
        End Try

    End Function


End Class
