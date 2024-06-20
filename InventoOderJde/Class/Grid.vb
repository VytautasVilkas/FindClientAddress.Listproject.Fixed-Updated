Imports System.Data.SqlClient
Imports System.Text

Public Class Grid

    Private grd As DataGridView
    Private grdrelated As DataGridView
    Private dataTable As New DataTable()
    Private dataTable2 As New DataTable()
    Private tablename As String
    Private ProcName As String
    Private errorlabel As ErrorLabel
    Private connectionStringProvider As IConnectionStringProvider
    Private connectionString As String = ""
    Private lastSelectedId As Integer = -1
    Private ColumnOrder As String
    Private ColumnWidthList As String
    Private columnMapping As Dictionary(Of String, String) = New Dictionary(Of String, String)()
    Private lastSelectedColumnIndex As Integer = -1
    Private _ExternalContextMenuForHeaders As ContextMenuStrip


    Public Sub New(tablename As String, grd As DataGridView, errorlabel As ErrorLabel, connectionStringProvider As IConnectionStringProvider)
        '
        Me.connectionStringProvider = connectionStringProvider
        connectionString = connectionStringProvider.GetConnectionString("ConnectionString")
        '
        Me.tablename = tablename
        Me.grd = grd
        Me.errorlabel = errorlabel
        AddHandler grd.ColumnHeaderMouseClick, AddressOf CustomDataGridView_ColumnHeaderMouseClick
    End Sub
    Public Sub New(grd As DataGridView, tablename As String, ProcName As String, errorlabel As ErrorLabel, connectionStringProvider As IConnectionStringProvider)
        Me.grd = grd
        ' Me.grdrelated = grdrelated
        '
        Me.connectionStringProvider = connectionStringProvider
        connectionString = connectionStringProvider.GetConnectionString("ConnectionString")
        '
        Me.tablename = tablename
        Me.ProcName = ProcName
        Me.errorlabel = errorlabel
        AddHandler grd.ColumnHeaderMouseClick, AddressOf CustomDataGridView_ColumnHeaderMouseClick
    End Sub
    Public Sub BuildDataTableForGrdSearch(columns As List(Of String))
        Try
            dataTable.Columns.Clear()
            For Each column As String In columns
                dataTable.Columns.Add(column)
            Next
        Catch ex As Exception
        End Try
    End Sub
    Public Sub BindingGrid(Root As Root)
        Try
            For Each item In Root.items
                Dim accessLat As String = String.Join(", ", item.access.Select(Function(a) a.lat.ToString()))
                Dim accessLng As String = String.Join(", ", item.access.Select(Function(a) a.lng.ToString()))
                Dim streetsScore As String = String.Join(", ", item.scoring.fieldScore.streets)
                dataTable.Rows.Add(
                        item.title,
                        item.id,
                        item.resultType,
                        item.houseNumberType,
                        item.address.label,
                        item.address.countryCode,
                        item.address.countryName,
                        item.address.state,
                        item.address.county,
                        item.address.city,
                        item.address.district,
                        item.address.street,
                        item.address.postalCode,
                        item.address.houseNumber,
                        item.position.lat,
                        item.position.lng,
                        accessLat,
                        accessLng,
                        item.mapView.west,
                        item.mapView.south,
                        item.mapView.east,
                        item.mapView.north,
                        item.scoring.queryScore,
                        item.scoring.fieldScore.city,
                        streetsScore,
                        item.scoring.fieldScore.houseNumber
)
            Next
        Catch ex As Exception
        End Try

    End Sub
    Public Property ExternalContextMenuForHeaders() As ContextMenuStrip
        Get
            Return _ExternalContextMenuForHeaders
        End Get
        Set(ByVal value As ContextMenuStrip)
            _ExternalContextMenuForHeaders = value
        End Set
    End Property
    Private Sub CustomDataGridView_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs)
        Dim grid As DataGridView = CType(sender, DataGridView)
        If e.Button = MouseButtons.Right Then
            lastSelectedColumnIndex = e.ColumnIndex
            Dim screenLocation As Point = Cursor.Position
            Try
                ExternalContextMenuForHeaders.Tag = grid
                ExternalContextMenuForHeaders.Show(screenLocation)
            Catch ex As Exception

            End Try
        End If
    End Sub
    Public Sub FillLocationGrid()
        Try
            If Not dataTable Is Nothing Then
                grd.DataSource = dataTable
                If grd.RowCount < 0 Then
                    errorlabel.ShowMessage("Nerasta", False)
                Else
                    Dim count As Integer = grd.RowCount
                    errorlabel.ShowMessage("Rezultatu rasta: " + count.ToString, True)
                End If
            Else
            End If
        Catch ex As Exception
        End Try
    End Sub
    Public Sub datatableClear()
        Try
            If Not dataTable Is Nothing Then
                dataTable.Clear()
            End If
        Catch ex As Exception
        End Try
    End Sub
    Public Sub BindParams()
        Dim query As String = ""
        If String.IsNullOrEmpty(ProcName) Then
            query = "select * from " & tablename
        Else
            query = ProcName
        End If
        Using connection As New SqlConnection(connectionString)
            Using adapter As New SqlDataAdapter(query, connection)
                Try
                    connection.Open()
                    adapter.Fill(dataTable)
                    grd.DataSource = dataTable
                    grd.AllowUserToOrderColumns = True
                    Dim numberOfRecords As String = grd.Rows.Count.ToString
                    errorlabel.ShowMessage(" Irasu skaicius: " + numberOfRecords, True)
                Catch ex As Exception
                    errorlabel.LogErrorToDB("err id - 11: " & ex.Message)
                Finally
                    If connection.State = ConnectionState.Open Then
                        connection.Close()
                    End If
                End Try
            End Using
        End Using
    End Sub
    Public Sub BindParamsFullTable()
        Dim query As String = "select * from " & tablename
        Using connection As New SqlConnection(connectionString)
            Using adapter As New SqlDataAdapter(query, connection)
                Try
                    connection.Open()
                    adapter.Fill(dataTable)
                    grd.DataSource = dataTable
                    grd.AllowUserToOrderColumns = True
                    Dim numberOfRecords As String = grd.Rows.Count.ToString
                    errorlabel.ShowMessage(" Irasu skaicius: " + numberOfRecords, True)
                Catch ex As Exception
                    errorlabel.LogErrorToDB("err id - 12 : " & ex.Message)
                Finally
                    If connection.State = ConnectionState.Open Then
                        connection.Close()
                    End If
                End Try
            End Using
        End Using
    End Sub
    Public Sub RefreshData()
        Dim query As String = ""
        If String.IsNullOrEmpty(ProcName) Then
            query = "SELECT * FROM " & tablename
        Else
            query = ProcName
        End If

        Using connection As New SqlConnection(connectionString)
            Using adapter As New SqlDataAdapter(query, connection)
                Try
                    connection.Open()
                    dataTable.Clear()
                    adapter.Fill(dataTable)
                    grd.DataSource = dataTable
                    grd.AllowUserToOrderColumns = True
                    Dim numberOfRecords As String = grd.Rows.Count.ToString()

                Catch ex As Exception
                    errorlabel.LogErrorToDB("err id-31: " & ex.Message)
                Finally
                    If connection.State = ConnectionState.Open Then
                        connection.Close()
                    End If
                End Try
            End Using
        End Using
    End Sub
    Public Sub RefreshDataFullTable()


        Dim query As String = "SELECT * FROM " & tablename


        Using connection As New SqlConnection(connectionString)
            Using adapter As New SqlDataAdapter(query, connection)
                Try
                    connection.Open()
                    dataTable.Clear()
                    adapter.Fill(dataTable)
                    grd.DataSource = dataTable
                    grd.AllowUserToOrderColumns = True
                    Dim numberOfRecords As String = grd.Rows.Count.ToString()

                Catch ex As Exception
                    errorlabel.LogErrorToDB("err id-31: " & ex.Message)
                Finally
                    If connection.State = ConnectionState.Open Then
                        connection.Close()
                    End If
                End Try
            End Using
        End Using
    End Sub

    Private Sub InsertTitleToLIST_ADDRESS(ID As Int32, name As String, title As String)
        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()

                ' SQL Update Statement
                Dim query As String = "UPDATE ADDRESS_LIST " +
                                      "SET [Title] = @Title " +
                                      "WHERE [ID] = @ID AND [ACCOUNTNUM] = @Name"

                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@ID", ID)
                    command.Parameters.AddWithValue("@Name", name)
                    command.Parameters.AddWithValue("@Title", title)

                    ' Execute the Update
                    Dim rowsAffected As Integer = command.ExecuteNonQuery()

                    ' Optionally, check the number of rows affected
                    If rowsAffected > 0 Then
                        errorlabel.ShowMessage("Atnaujintas adresas", True)
                    Else
                        errorlabel.ShowMessage("Nepavyko atnaujinti", False)
                    End If
                End Using
            End Using
        Catch ex As Exception
            ' Handle exception
            errorlabel.ShowMessage("Err id - 17: " & ex.Message, False)
        End Try
    End Sub


    Public Function InsertAddress(ID As Int32, name As String, bToUpper As Boolean) As Boolean
        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()
                If grd.SelectedRows.Count = 0 Then
                    errorlabel.ShowMessage("Pasirinkite adresą", False)
                    Return False
                End If
                For Each selectedRow As DataGridViewRow In grd.SelectedRows
                    If Not selectedRow.IsNewRow Then
                        ' Check if the ID exists in the database
                        'Dim checkQuery As String = "SELECT COUNT(*) FROM _ListFullAddress WHERE [LIST_ID] = @LIST_ID"
                        'Dim idExists As Boolean

                        'Using checkCommand As New SqlCommand(checkQuery, connection)
                        'checkCommand.Parameters.AddWithValue("@LIST_ID", ID)
                        'idExists = Convert.ToInt32(checkCommand.ExecuteScalar()) > 0
                        'End Using

                        Dim query As String

                        'If idExists Then
                        ' Update query
                        If bToUpper Then
                            query = "UPDATE ADDRESS_LIST SET " &
                                    "[Title] = @Title, [RESULT_ID] = @ID, [Result Type] = @ResultType, [House Number Type] = @HouseNumberType, " &
                                    "[Address Label] = UPPER(@AddressLabel), [Country Code] = UPPER(@CountryCode), [Country Name] = UPPER(@CountryName), " &
                                    "[State] = UPPER(@State), [County] = UPPER(@County), [City] = UPPER(@City), [District] = UPPER(@District), [Street] = UPPER(@Street), " &
                                    "[Postal Code] = @PostalCode, [House Number] = @HouseNumber, [Position Lat] = @PositionLat, " &
                                    "[Position Lng] = @PositionLng, [Access Lat] = @AccessLat, [Access Lng] = @AccessLng, " &
                                    "[MapView West] = @MapViewWest, [MapView South] = @MapViewSouth, [MapView East] = @MapViewEast, " &
                                    "[MapView North] = @MapViewNorth, [Query Score] = @QueryScore, [Field Score City] = @FieldScoreCity, " &
                                    "[Field Score Streets] = @FieldScoreStreets, [Field Score House Number] = @FieldScoreHouseNumber " &
                                    "WHERE [ID] = @LIST_ID"
                        Else
                            query = "UPDATE ADDRESS_LIST SET " &
                                    "[Title] = @Title, [RESULT_ID] = @ID, [Result Type] = @ResultType, [House Number Type] = @HouseNumberType, " &
                                    "[Address Label] = @AddressLabel, [Country Code] = @CountryCode, [Country Name] = @CountryName, " &
                                    "[State] = @State, [County] = @County, [City] = @City, [District] = @District, [Street] = @Street, " &
                                    "[Postal Code] = @PostalCode, [House Number] = @HouseNumber, [Position Lat] = @PositionLat, " &
                                    "[Position Lng] = @PositionLng, [Access Lat] = @AccessLat, [Access Lng] = @AccessLng, " &
                                    "[MapView West] = @MapViewWest, [MapView South] = @MapViewSouth, [MapView East] = @MapViewEast, " &
                                    "[MapView North] = @MapViewNorth, [Query Score] = @QueryScore, [Field Score City] = @FieldScoreCity, " &
                                    "[Field Score Streets] = @FieldScoreStreets, [Field Score House Number] = @FieldScoreHouseNumber " &
                                    "WHERE [ID] = @LIST_ID"

                        End If
                        'Else
                        ' Insert query
                        'query = "INSERT INTO _ListFullAddress (" &
                        '        "[LIST_ID],[LIST_NAME], [Title], [ID], [Result Type], [House Number Type], [Address Label], " &
                        ''       "[Country Code], [Country Name], [State], [County], [City], " &
                        '       "[District], [Street], [Postal Code], [House Number], [Position Lat], " &
                        '       "[Position Lng], [Access Lat], [Access Lng], [MapView West], [MapView South], " &
                        '        "[MapView East], [MapView North], [Query Score], [Field Score City], " &
                        '        "[Field Score Streets], [Field Score House Number]" &
                        '       ") VALUES (" &
                        '        "@LIST_ID,@LIST_NAME , @Title, @ID, @ResultType, @HouseNumberType, @AddressLabel, " &
                        '        "@CountryCode, @CountryName, @State, @County, @City, " &
                        '        "@District, @Street, @PostalCode, @HouseNumber, @PositionLat, " &
                        '        "@PositionLng, @AccessLat, @AccessLng, @MapViewWest, @MapViewSouth, " &
                        '        "@MapViewEast, @MapViewNorth, @QueryScore, @FieldScoreCity, " &
                        '       "@FieldScoreStreets, @FieldScoreHouseNumber)"
                        ' End If
                        Using command As New SqlCommand(query, connection)
                            command.Parameters.AddWithValue("@LIST_ID", ID)
                            command.Parameters.AddWithValue("@LIST_NAME", name)
                            command.Parameters.AddWithValue("@Title", selectedRow.Cells("Title").Value)
                            command.Parameters.AddWithValue("@ID", selectedRow.Cells("ID").Value)
                            command.Parameters.AddWithValue("@ResultType", selectedRow.Cells("Result Type").Value)
                            command.Parameters.AddWithValue("@HouseNumberType", selectedRow.Cells("House Number Type").Value)
                            command.Parameters.AddWithValue("@AddressLabel", selectedRow.Cells("Address Label").Value)
                            command.Parameters.AddWithValue("@CountryCode", selectedRow.Cells("Country Code").Value)
                            command.Parameters.AddWithValue("@CountryName", selectedRow.Cells("Country Name").Value)
                            command.Parameters.AddWithValue("@State", selectedRow.Cells("State").Value)
                            command.Parameters.AddWithValue("@County", selectedRow.Cells("County").Value)
                            command.Parameters.AddWithValue("@City", selectedRow.Cells("City").Value)
                            command.Parameters.AddWithValue("@District", selectedRow.Cells("District").Value)
                            command.Parameters.AddWithValue("@Street", selectedRow.Cells("Street").Value)
                            command.Parameters.AddWithValue("@PostalCode", selectedRow.Cells("Postal Code").Value)
                            command.Parameters.AddWithValue("@HouseNumber", selectedRow.Cells("House Number").Value)
                            command.Parameters.AddWithValue("@PositionLat", selectedRow.Cells("Position Lat").Value)
                            command.Parameters.AddWithValue("@PositionLng", selectedRow.Cells("Position Lng").Value)
                            command.Parameters.AddWithValue("@AccessLat", selectedRow.Cells("Access Lat").Value)
                            command.Parameters.AddWithValue("@AccessLng", selectedRow.Cells("Access Lng").Value)
                            command.Parameters.AddWithValue("@MapViewWest", selectedRow.Cells("MapView West").Value)
                            command.Parameters.AddWithValue("@MapViewSouth", selectedRow.Cells("MapView South").Value)
                            command.Parameters.AddWithValue("@MapViewEast", selectedRow.Cells("MapView East").Value)
                            command.Parameters.AddWithValue("@MapViewNorth", selectedRow.Cells("MapView North").Value)
                            command.Parameters.AddWithValue("@QueryScore", selectedRow.Cells("Query Score").Value)
                            command.Parameters.AddWithValue("@FieldScoreCity", selectedRow.Cells("Field Score City").Value)
                            command.Parameters.AddWithValue("@FieldScoreStreets", selectedRow.Cells("Field Score Streets").Value)
                            command.Parameters.AddWithValue("@FieldScoreHouseNumber", selectedRow.Cells("Field Score House Number").Value)
                            Dim rowsAffected As Integer = command.ExecuteNonQuery()
                            errorlabel.LogErrorToDB("Padarytas atnaujinimas: " + ID.ToString + "," + name.ToString + "," + selectedRow.Cells("Title").Value.ToString)
                            Return True
                        End Using

                    End If
                Next
            End Using

        Catch ex As Exception
            errorlabel.ShowMessage("err id - 19 : " & ex.Message, False)
            errorlabel.LogErrorToDB("err id - 19 : " & ex.Message)
            Return False

        End Try
        Return False
    End Function
    Public Sub SearchAndSelectRowByColumn(columnName As String, searchValue As String)
        Try
            grd.ClearSelection()
            For Each row As DataGridViewRow In grd.Rows
                If Not row.IsNewRow Then
                    If row.Cells(columnName).Value IsNot Nothing AndAlso row.Cells(columnName).Value.ToString().Trim() = searchValue.Trim() Then
                        row.Selected = True
                        grd.FirstDisplayedScrollingRowIndex = row.Index
                        Return
                    End If
                End If
            Next
            errorlabel.ShowMessage("Nerasta", False)
        Catch ex As Exception
            errorlabel.ShowMessage("Err id - 122: " & ex.Message, False)
            errorlabel.LogErrorToDB("Err id - 122: " & ex.Message)
        End Try
    End Sub
    Public Sub SearchAndDisplayResults(columnName As String, searchValue As String)
        Try
            Dim query As String = "SELECT * FROM " & tablename & " WHERE " & columnName & " LIKE @searchValue"

            Using connection As New SqlConnection(connectionString)
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@searchValue", "%" & searchValue & "%")

                    Using adapter As New SqlDataAdapter(command)
                        dataTable.Clear()
                        adapter.Fill(dataTable)
                        grd.DataSource = dataTable
                        If grd.Rows.Count > 0 Then
                            grd.ClearSelection()
                            grd.Rows(0).Selected = True

                        Else
                            errorlabel.ShowMessage("nerasta", False)
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            errorlabel.ShowMessage("Err id - 121 " & ex.Message, False)
            errorlabel.LogErrorToDB("Err id - 121 " & ex.Message)
        End Try
    End Sub
    Public Sub FetchAndDisplayAddressData(listId As Int32)
        Try
            Dim query As String = "SELECT * FROM _ListFullAddress WHERE LIST_ID = @LIST_ID"

            Using connection As New SqlConnection(connectionString)
                Using command As New SqlCommand(query, connection)

                    command.Parameters.AddWithValue("@LIST_ID", listId)

                    Using adapter As New SqlDataAdapter(command)

                        dataTable2.Clear()
                        connection.Open()
                        adapter.Fill(dataTable2)
                        grdrelated.DataSource = dataTable2
                    End Using
                End Using
            End Using
        Catch ex As Exception
            errorlabel.ShowMessage("Err id - 191: " & ex.Message, False)

        End Try
    End Sub


    Public Sub SaveCurrentSelectedRowId()
        ' Save the ID of the currently selected row, if any
        If grd.SelectedRows.Count > 0 Then
            lastSelectedId = Convert.ToInt32(grd.SelectedRows(0).Cells("ID").Value)
        Else
            lastSelectedId = -1
        End If
    End Sub
    Public Sub ReselectSavedRow()
        ' Reselect the row with the stored ID, if it exists
        If lastSelectedId <> -1 Then
            For Each row As DataGridViewRow In grd.Rows
                If Convert.ToInt32(row.Cells("ID").Value) = lastSelectedId Then
                    row.Selected = True
                    ' Optionally scroll to the selected row
                    grd.FirstDisplayedScrollingRowIndex = row.Index
                    Exit For
                End If
            Next
        End If
    End Sub

    Public Sub cleardata()
        dataTable.Clear()
    End Sub
    Public Sub SaveFormSizeToDB(formName As String, width As Integer, height As Integer)

        Using conn As New SqlConnection(connectionString)
            Try
                Dim cmd As SqlCommand
                conn.Open()
                If checkIfExist("FORMSIZE", "SIZ_USER_ID", 1, "SIZ_FORMNAME", formName) Then
                    cmd = New SqlCommand("UPDATE FORMSIZE SET SIZ_WIDTH = @Width, SIZ_HEIGHT = @Height WHERE SIZ_USER_ID = @UserID AND SIZ_FORMNAME = @FormName", conn)
                Else
                    cmd = New SqlCommand("INSERT INTO FORMSIZE (SIZ_USER_ID, SIZ_FORMNAME, SIZ_WIDTH, SIZ_HEIGHT) VALUES (@UserID, @FormName, @Width, @Height)", conn)
                End If
                cmd.Parameters.AddWithValue("@UserID", 1)
                cmd.Parameters.AddWithValue("@FormName", formName)
                cmd.Parameters.AddWithValue("@Width", width)
                cmd.Parameters.AddWithValue("@Height", height)
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                errorlabel.LogErrorToDB("err id-117: " & ex.Message)
                errorlabel.ShowMessage("Ups: (err id-117): " & ex.Message, False)
            Finally
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
            End Try

        End Using
    End Sub
    Public Sub LoadFormSizeFromDB(formName As String, form As Form)

        Using conn As New SqlConnection(connectionString)
            Try
                conn.Open()
                Dim cmd As New SqlCommand("SELECT SIZ_WIDTH, SIZ_HEIGHT FROM FORMSIZE WHERE SIZ_USER_ID = @UserID AND SIZ_FORMNAME = @FormName", conn)
                cmd.Parameters.AddWithValue("@UserID", 1)
                cmd.Parameters.AddWithValue("@FormName", formName)
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        form.Width = reader.GetInt32(reader.GetOrdinal("SIZ_WIDTH"))
                        form.Height = reader.GetInt32(reader.GetOrdinal("SIZ_HEIGHT"))
                    End If
                End Using
            Catch ex As Exception
                ErrorLabel.LogErrorToDB("err id-118: " & ex.Message)
                ErrorLabel.ShowMessage("Ups: (err id-118): " & ex.Message, False)
            Finally
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
            End Try
        End Using
    End Sub
    Private Function checkIfExist(TableToSearch As String, MainParamToSearchBy As String, MainParam As Object, Optional relatedParamToSearchBy As String = "", Optional relatedParam As Object = Nothing) As Boolean

        Using conn As New SqlConnection(connectionString)
            Try
                conn.Open()
                Dim sql As New StringBuilder("SELECT COUNT(*) FROM ")
                sql.Append(TableToSearch)
                sql.AppendFormat(" WHERE {0} = @MainParam", MainParamToSearchBy)

                If Not String.IsNullOrEmpty(relatedParamToSearchBy) AndAlso relatedParam IsNot Nothing Then
                    sql.AppendFormat(" AND {0} = @RelatedParam", relatedParamToSearchBy)
                End If

                Using checkCmd As New SqlCommand(sql.ToString(), conn)
                    checkCmd.Parameters.Add("@MainParam", SqlDbType.VarChar).Value = MainParam
                    If relatedParam IsNot Nothing Then
                        checkCmd.Parameters.Add("@RelatedParam", SqlDbType.VarChar).Value = relatedParam
                    End If
                    Dim exists As Boolean = Convert.ToInt32(checkCmd.ExecuteScalar()) > 0
                    Return exists
                End Using
            Catch ex As Exception
                errorlabel.LogErrorToDB("err id-591: " & ex.Message)
                Return False
            Finally
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
            End Try
        End Using
    End Function
    Public Sub SaveColumnOrder()
        Try
            If saveUserSetting() Then
                Dim whatHappend As String = ""
                If tableName <> "" Then
                    If UserSettingsExist(tablename, 1) Then
                        Dim updateQuery As String = "UPDATE _SETTINGS SET USER_COLUMN_SETTING = @columnOrder, USER_COLUMN_SETTING_WIDTH = @columnWidth  WHERE USER_USER_ID = @userId AND USER_TABLE_NAME = @tableName"
                        Using connection As New SqlConnection(ConnectionString)
                            Using updateCommand As New SqlCommand(updateQuery, connection)
                                updateCommand.Parameters.AddWithValue("@userId", 1)
                                updateCommand.Parameters.AddWithValue("@columnOrder", ColumnOrder)
                                updateCommand.Parameters.AddWithValue("@tableName", tablename)
                                updateCommand.Parameters.AddWithValue("@columnWidth", ColumnWidthList)
                                Try
                                    connection.Open()
                                    updateCommand.ExecuteNonQuery()
                                Catch ex As Exception
                                    errorlabel.LogErrorToDB("err id-54: " & ex.Message)
                                    errorlabel.ShowMessage("Ups: (err id-54): " & ex.Message, False)
                                Finally
                                    If connection.State = ConnectionState.Open Then
                                        connection.Close()
                                    End If
                                End Try
                            End Using
                        End Using
                    Else
                        Dim insertQuery As String = "INSERT INTO _SETTINGS (USER_USER_ID, USER_TABLE_NAME, USER_COLUMN_SETTING,USER_COLUMN_SETTING_WIDTH) VALUES (@userId, @tableName, @columnOrder, @columnWidth)"
                        Using connection As New SqlConnection(ConnectionString)
                            Using insertCommand As New SqlCommand(insertQuery, connection)
                                insertCommand.Parameters.AddWithValue("@userId", 1)
                                insertCommand.Parameters.AddWithValue("@tableName", tablename)
                                insertCommand.Parameters.AddWithValue("@columnOrder", ColumnOrder)
                                insertCommand.Parameters.AddWithValue("@columnWidth", ColumnWidthList)

                                Try
                                    connection.Open()
                                    insertCommand.ExecuteNonQuery()
                                Catch ex As Exception
                                    errorlabel.LogErrorToDB("err id-55: " & ex.Message)
                                    errorlabel.ShowMessage("Ups: (err id-55): " & ex.Message, False)
                                Finally
                                    If connection.State = ConnectionState.Open Then
                                        connection.Close()
                                    End If
                                End Try
                            End Using
                        End Using
                    End If
                Else
                    whatHappend += "nerastas Pagrindines lenteles pavadinimas"
                End If
            End If
        Catch ex As Exception
            ErrorLabel.ShowMessage("err id-16222: " + ex.Message, False)
            ErrorLabel.LogErrorToDB("err id-16222: " + ex.Message)
        End Try



    End Sub
    Private Function saveUserSetting() As Boolean
        Try
            If grd IsNot Nothing Then
                Dim columnLengths As New List(Of String)
                For Each col As DataGridViewColumn In grd.Columns
                    columnLengths.Add(col.Width.ToString())
                Next
                Dim columnLengthsString As String = String.Join(",", columnLengths)
                ColumnWidthList = columnLengthsString
                Dim newOrderDictionary As New Dictionary(Of Integer, String)
                For Each col As DataGridViewColumn In grd.Columns
                    Dim colName As String = col.Name
                    Dim newIndex As Integer = col.DisplayIndex
                    'While newOrderDictionary.ContainsKey(newIndex)
                    '    newIndex += 1
                    'End While
                    newOrderDictionary.Add(newIndex, colName)
                Next
                Dim orderedColumns = newOrderDictionary.OrderBy(Function(pair) pair.Key)
                Dim newOrderList As New List(Of String)
                For Each pair In orderedColumns
                    newOrderList.Add(pair.Value)
                Next
                Dim NewcolumnOrder As String = String.Join(",", newOrderList)
                ColumnOrder = NewcolumnOrder
            End If

        Catch ex As Exception
            ErrorLabel.ShowMessage("err id-3334: " + ex.Message, False)
            ErrorLabel.LogErrorToDB("err id-3334: " + ex.Message)
            Return False
        End Try
        Return True
    End Function
    Public Function UserSettingsExist(table As String, drb_id As Int32) As Boolean

        Dim query As String = "SELECT COUNT(*) FROM _SETTINGS WHERE USER_USER_ID = @userId AND USER_TABLE_NAME = @tableName"
        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@userId", drb_id)
                command.Parameters.AddWithValue("@tableName", table)
                Try
                    connection.Open()
                    Dim count As Integer = CInt(command.ExecuteScalar())
                    Return count > 0
                Catch ex As Exception
                    errorlabel.LogErrorToDB("err id-59: " & ex.Message)
                    errorlabel.ShowMessage("Ups: (err id-59): " & ex.Message, False)
                    Return False
                Finally
                    If connection.State = ConnectionState.Open Then
                        connection.Close()
                    End If
                End Try
            End Using
        End Using
    End Function
    Public Sub AdjustColumnAlignmentByDataType()
        ChangeGridColumnHeader(tablename, grd, columnMapping)
        AdjustColumnAlignmentByDataType(dataTable)
        grd.AllowUserToOrderColumns = True
    End Sub
    Private Sub AdjustColumnAlignmentByDataType(ByVal dataTable As DataTable)
        For columnIndex As Integer = 0 To dataTable.Columns.Count - 1
            Dim isNumericColumn As Boolean = True
            For Each row As DataRow In dataTable.Rows
                Dim value As String = row(columnIndex).ToString()
                Dim numericValue As Double
                If Not Double.TryParse(value, numericValue) Then
                    isNumericColumn = False
                    Exit For
                End If
            Next
            If isNumericColumn Then
                grd.Columns(columnIndex).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End If
        Next
    End Sub
    Public Sub ChangeGridColumnHeader(table As String, selgrid As DataGridView, colMapp As Dictionary(Of String, String))

        Dim query As String = "SELECT COL_COLUMN_HEADERS FROM _COLUMN_NAMES WHERE COL_TABLENAME = @Tablename AND COL_USER_ID = @userId"
        Using connection As New SqlConnection(connectionString)
            Try
                connection.Open()
                Dim columnHeaders As String = ExecuteHeaderQuery(query, table, 1, connection)
                If columnHeaders Is Nothing Then
                    ErrorLabel.LogErrorToDB("err id-46: headeris nerastas, sukuriamas naujas headeris duombazeje")
                    'ErrorLabel.ShowMessage("Ups: (err id-46): headeris nerastas", False)
                    'Exit Sub
                End If
                If String.IsNullOrEmpty(columnHeaders) Then
                    Dim columnsList As New List(Of String)()
                    For Each column As DataGridViewColumn In selgrid.Columns
                        columnsList.Add(column.Name & "," & column.HeaderText)
                    Next
                    columnHeaders = String.Join(",", columnsList)
                    Dim insertQuery As String = "INSERT INTO _COLUMN_NAMES (COL_TABLENAME, COL_USER_ID, COL_COLUMN_HEADERS) VALUES (@Tablename, @userId, @Headers)"
                    Using cmd As New SqlCommand(insertQuery, connection)
                        cmd.Parameters.AddWithValue("@Tablename", table)
                        cmd.Parameters.AddWithValue("@userId", 1)
                        cmd.Parameters.AddWithValue("@Headers", columnHeaders)
                        Try
                            cmd.ExecuteNonQuery()
                            ChangeGridColumnHeader(table, selgrid, colMapp)
                        Catch ex As Exception
                            ErrorLabel.LogErrorToDB("err id-43: " & ex.Message)
                            ErrorLabel.ShowMessage("Ups: (err id-43): " & ex.Message, False)
                            Exit Sub
                        End Try
                    End Using
                    Return
                End If
                Dim headerPairs As String() = columnHeaders.Split(","c)
                For i As Integer = 0 To headerPairs.Length - 1 Step 2
                    Dim columnHeader As String = headerPairs(i)
                    Dim newHeader As String = If(i + 1 < headerPairs.Length, headerPairs(i + 1), String.Empty)
                    If selgrid.Columns.Contains(columnHeader) Then
                        colMapp(newHeader) = columnHeader
                        selgrid.Columns(columnHeader).HeaderText = newHeader
                    End If
                Next
            Catch ex As Exception
                ErrorLabel.LogErrorToDB("err id-44: " & ex.Message)
                ErrorLabel.ShowMessage("Ups: (err id-44): " & ex.Message, False)
            Finally
                If connection.State = ConnectionState.Open Then
                    connection.Close()
                End If
            End Try
        End Using
    End Sub
    Public Function ChangeHeaderName() As Boolean


        Try
            Dim selectedGrid As DataGridView = CType(ExternalContextMenuForHeaders.Tag, DataGridView)
            If selectedGrid IsNot Nothing Then
                If lastSelectedColumnIndex > -1 AndAlso selectedGrid Is grd Then
                    Dim headerText As String = InputBox("Įveskite naują stulpelio antraštę:", "Pakeisti Pavadinimą", grd.Columns(lastSelectedColumnIndex).HeaderText)
                    If Not String.IsNullOrEmpty(headerText) Then
                        grd.Columns(lastSelectedColumnIndex).HeaderText = headerText

                    End If
                    Dim columnInfo As Dictionary(Of String, String) = GetColumnNamesAndHeaders(grd)
                    Dim headerInfoStr As String = ConvertDictionaryToString(columnInfo)
                    SaveHeadersToDB(tableName, headerInfoStr)
                    lastSelectedColumnIndex = -1
                    Return True


                Else
                    Return False
                End If
            Else
                ErrorLabel.ShowMessage("err id-4002: selected Grid tuscias", False)
                ErrorLabel.LogErrorToDB("err id-4002: selected Grid tuscias")
                Return False
            End If
        Catch ex As Exception
            ErrorLabel.ShowMessage("err if 1011" & ex.Message, False)
            ErrorLabel.LogErrorToDB("err if 1011" & ex.Message)
            Return False
        End Try

    End Function
    Public Function GetColumnNamesAndHeaders(grid As DataGridView) As Dictionary(Of String, String)
        Dim columnHeaders As New Dictionary(Of String, String)
        For Each col As DataGridViewColumn In grid.Columns
            columnHeaders.Add(col.Name, col.HeaderText)
        Next
        Return columnHeaders
    End Function
    Public Function ConvertDictionaryToString(dict As Dictionary(Of String, String)) As String
        Dim result As New StringBuilder()
        For Each pair As KeyValuePair(Of String, String) In dict
            result.Append(pair.Key).Append(",").Append(pair.Value).Append(",")
        Next
        ' Remove the trailing comma
        If result.Length > 0 Then
            result.Remove(result.Length - 1, 1)
        End If
        Return result.ToString()
    End Function
    Public Sub SaveHeadersToDB(table As String, columnHeaderInfo As String)

        Using conn As New SqlConnection(connectionString)
            Try
                conn.Open()
                Dim cmd As New SqlCommand("UPDATE _COLUMN_NAMES SET COL_COLUMN_HEADERS = @info WHERE COL_TABLENAME = @Tablename AND COL_USER_ID = @userId", conn)
                cmd.Parameters.AddWithValue("@info", columnHeaderInfo)
                cmd.Parameters.AddWithValue("@Tablename", table)
                cmd.Parameters.AddWithValue("@userId", 1)
                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                If rowsAffected = 0 Then
                    cmd = New SqlCommand("SELECT COUNT(*) FROM _COLUMN_NAMES WHERE COL_TABLENAME = @Tablename AND COL_USER_ID IS NULL", conn)
                    cmd.Parameters.AddWithValue("@Tablename", table)
                    Dim count As Integer = DirectCast(cmd.ExecuteScalar(), Integer)
                    If count > 0 Then
                        cmd = New SqlCommand("INSERT INTO _GRID_COLUMN_NAMES (COL_COLUMN_HEADERS, COL_TABLENAME, COL_USER_ID) VALUES (@info, @Tablename, @userId)", conn)
                        cmd.Parameters.AddWithValue("@info", columnHeaderInfo)
                        cmd.Parameters.AddWithValue("@Tablename", table)
                        cmd.Parameters.AddWithValue("@userId", 1)
                        cmd.ExecuteNonQuery()
                    Else
                        cmd = New SqlCommand("INSERT INTO _GRID_COLUMN_NAMES (COL_COLUMN_HEADERS, COL_TABLENAME, COL_USER_ID) VALUES (@info, @Tablename, @userId)", conn)
                        cmd.Parameters.AddWithValue("@info", columnHeaderInfo)
                        cmd.Parameters.AddWithValue("@Tablename", table)
                        cmd.Parameters.AddWithValue("@userId", 1)
                        cmd.ExecuteNonQuery()
                    End If
                End If
            Catch ex As Exception
                ErrorLabel.LogErrorToDB("err id-102: " & ex.Message)
                ErrorLabel.ShowMessage("Ups: (err id-102): " & ex.Message, False)
            Finally
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
            End Try
        End Using
    End Sub
    Private Function ExecuteHeaderQuery(query As String, table As String, userId As Integer, connection As SqlConnection) As String
        Using command As New SqlCommand(query, connection)
            command.Parameters.AddWithValue("@Tablename", table)
            command.Parameters.AddWithValue("@userId", userId)
            Try
                Return DirectCast(command.ExecuteScalar(), String)
            Catch ex As Exception
                ErrorLabel.LogErrorToDB("err id-45: " & ex.Message)
                ErrorLabel.ShowMessage("Ups: (err id-45): " & ex.Message, False)
                Return Nothing
            End Try
        End Using
    End Function
    Public Sub GetColumnOrder()
        getcolumnorderValue(TableName)
        getColumnWidthList(TableName)
        LoadUserSettings(grd, ColumnOrder, ColumnWidthList)

    End Sub
    Private Sub LoadUserSettings(grd As DataGridView, ColumnOrder As String, ColumnWidthList As String)
        If grd IsNot Nothing Then
            Try
                If Not String.IsNullOrEmpty(ColumnOrder) Then
                    Dim columnOrderArr As String() = ColumnOrder.Split(","c)
                    For i As Integer = 0 To columnOrderArr.Length - 1
                        Dim columnName As String = columnOrderArr(i)
                        Dim col As DataGridViewColumn = grd.Columns(columnName)
                        If col IsNot Nothing Then
                            col.DisplayIndex = i
                        End If
                    Next
                End If
                If Not String.IsNullOrEmpty(ColumnWidthList) Then
                    Dim columnWidths As List(Of String) = ColumnWidthList.Split(","c).ToList()
                    For i As Integer = 0 To grd.Columns.Count - 1
                        Dim col As DataGridViewColumn = grd.Columns(i)
                        Dim colWidth As Integer
                        If i < columnWidths.Count AndAlso Integer.TryParse(columnWidths(i), colWidth) Then
                            col.Width = colWidth
                        End If
                    Next
                End If
            Catch ex As Exception
                ErrorLabel.ShowMessage("err id-3333: " + ex.Message, False)
                ErrorLabel.LogErrorToDB("err id-3333: " + ex.Message)
            End Try
        End If
    End Sub
    Private Sub getcolumnorderValue(tablename As String)
        Dim query As String = "SELECT USER_COLUMN_SETTING  FROM _SETTINGS WHERE USER_USER_ID = @userId and USER_TABLE_NAME = @tableName"
        Using connection As New SqlConnection(ConnectionString)
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@userId", 1)
                command.Parameters.AddWithValue("@tableName", tablename)
                Try
                    connection.Open()
                    Dim result As Object = command.ExecuteScalar()
                    If result IsNot Nothing AndAlso Not DBNull.Value.Equals(result) Then
                        If tablename.Equals(Me.tablename) Then
                            ColumnOrder = CStr(result)
                        End If
                    End If
                Catch ex As Exception
                    errorlabel.LogErrorToDB("err id-60: " & ex.Message & ".. " & Date.Now)
                Finally
                    If connection.State = ConnectionState.Open Then
                        connection.Close()
                    End If
                End Try
            End Using
        End Using
    End Sub
    Private Sub getColumnWidthList(tablename As String)
        Dim queryForWidth As String = "SELECT USER_COLUMN_SETTING_WIDTH  FROM _SETTINGS WHERE USER_USER_ID= @userId and USER_TABLE_NAME = @tableName"
        Using connection As New SqlConnection(ConnectionString)
            Using command As New SqlCommand(queryForWidth, connection)
                command.Parameters.AddWithValue("@userId", 1)
                command.Parameters.AddWithValue("@tableName", tablename)
                Try
                    connection.Open()
                    Dim result As Object = command.ExecuteScalar()
                    If result IsNot Nothing AndAlso Not DBNull.Value.Equals(result) Then
                        If tablename.Equals(Me.tablename) Then
                            ColumnWidthList = CStr(result)

                        End If
                    End If
                Catch ex As Exception
                    errorlabel.LogErrorToDB("err id-61: " & ex.Message)
                Finally
                    If connection.State = ConnectionState.Open Then
                        connection.Close()
                    End If
                End Try
            End Using
        End Using
    End Sub
End Class
