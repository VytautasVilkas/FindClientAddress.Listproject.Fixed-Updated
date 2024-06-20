Imports System.Configuration

Public Class ConfigurationManagerConnectionStringProvider
    Implements IConnectionStringProvider

    Public Function GetConnectionString(name As String) As String Implements IConnectionStringProvider.GetConnectionString
        Try
            Dim connStr As String = ConfigurationManager.ConnectionStrings(name).ConnectionString
            If String.IsNullOrEmpty(connStr) Then
                Throw New ConfigurationErrorsException("Nepavyksta Gauti Prisijungimo is Config failo")
            End If
            Return connStr
        Catch ex As Exception
            Throw New ApplicationException("Nepavyksta Gauti Prisijungimo is Config failo")
        End Try
    End Function
End Class