Imports Newtonsoft.Json
Imports System.Collections.Generic

Public Class Address
    Public Property label As String
    Public Property countryCode As String
    Public Property countryName As String
    Public Property state As String
    Public Property county As String
    Public Property city As String
    Public Property district As String
    Public Property street As String
    Public Property postalCode As String
    Public Property houseNumber As String
End Class

Public Class Position
    Public Property lat As Double
    Public Property lng As Double
End Class

Public Class Access
    Public Property lat As Double
    Public Property lng As Double
End Class

Public Class MapView
    Public Property west As Double
    Public Property south As Double
    Public Property east As Double
    Public Property north As Double
End Class

Public Class FieldScore
    Public Property city As Double
    Public Property streets As List(Of Double)
    Public Property houseNumber As Double
End Class

Public Class Scoring
    Public Property queryScore As Double
    Public Property fieldScore As FieldScore
End Class

Public Class Item
    Public Property title As String
    Public Property id As String
    Public Property resultType As String
    Public Property houseNumberType As String
    Public Property address As Address
    Public Property position As Position
    Public Property access As List(Of Access)
    Public Property mapView As MapView
    Public Property scoring As Scoring
End Class

Public Class Root
    Public Property items As List(Of Item)
End Class

