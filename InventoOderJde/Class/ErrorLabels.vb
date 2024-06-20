Imports System.Data.SqlClient

Public Class ErrorLabel
    Private _label As Label
    Private _timer As Timer
    Private _originalTop As Integer
    Private _floatSpeed As Integer = 2
    Private _color As Color
    Private _fadeDuration As Integer = 2000
    Private _ticksRequired As Integer
    Private _currentTick As Integer = 0
    Private _startColor As Color
    Private connectionStringProvider As IConnectionStringProvider
    Private connectionString As String = ""

    Public Sub New(ByRef label As Label, color As Color, connectionStringProvider As IConnectionStringProvider)
        '
        Me.connectionStringProvider = connectionStringProvider
        connectionString = connectionStringProvider.GetConnectionString("ConnectionString")
        '
        _color = color
        _label = label
        _originalTop = _label.Top
        _label.Visible = False
        _timer = New Timer()
        _timer.Interval = 100
        _label.AutoSize = True

        '_label.TextAlign = ContentAlignment.TopLeft
        _label.BackColor = color.Transparent
        _ticksRequired = _fadeDuration / _timer.Interval
        AddHandler _timer.Tick, AddressOf TimerElapsed_Tick
    End Sub

    Public Sub ShowMessage(message As String, ok As Boolean)
        If _label.Visible Then
            _timer.Stop()
            _label.Visible = False
        End If
        _currentTick = 0
        _label.Text = message
        _label.BackColor = Color.Transparent
        If ok Then
            _startColor = Color.Blue
        Else
            _startColor = Color.Red
        End If
        _label.ForeColor = _startColor
        _label.Font = New Font(_label.Font.FontFamily, 10)
        _label.Top = _originalTop
        _label.Visible = True
        _timer.Start()
    End Sub

    Private Sub TimerElapsed_Tick(sender As Object, e As EventArgs)
        _label.Top -= _floatSpeed
        _currentTick += 1
        Dim r As Integer = Math.Max(0, Math.Min(255, Transition(_startColor.R, _color.R, _currentTick, _ticksRequired)))
        Dim g As Integer = Math.Max(0, Math.Min(255, Transition(_startColor.G, _color.G, _currentTick, _ticksRequired)))
        Dim b As Integer = Math.Max(0, Math.Min(255, Transition(_startColor.B, _color.B, _currentTick, _ticksRequired)))

        _label.ForeColor = Color.FromArgb(r, g, b)
        If _currentTick >= _ticksRequired Then
            _timer.Stop()
            _label.Visible = False
            _label.Top = _originalTop
        End If
    End Sub
    Public Sub LogErrorToDB(errorMessage As String)
        Using connection As New SqlConnection(connectionString)
            connection.Open()
            Dim command As New SqlCommand("INSERT INTO TestLogs (LOG_WHATHAPPEND, LOG_DATE) VALUES (@message, @date)", connection)
            command.Parameters.AddWithValue("@message", errorMessage)
            command.Parameters.AddWithValue("@date", DateTime.Now)
            Try
                command.ExecuteNonQuery()
            Catch ex As Exception
                ShowMessage(ex.Message, False)
            End Try
        End Using
    End Sub
    Private Function Transition(startValue As Integer, endValue As Integer, currentTick As Integer, totalTicks As Integer) As Integer
        Dim difference As Integer = endValue - startValue
        Dim changePerTick As Double = difference / totalTicks
        Return startValue + CInt(changePerTick * currentTick)
    End Function
End Class
