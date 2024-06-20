Imports Newtonsoft.Json
Imports System.Net
Imports System.IO
Imports System.ComponentModel

Public Class frmFindOderJde
    Private grdSearch As Grid
    Private grdForDalyviai As Grid
    Private checknetwork As NetworkCheck
    Private errorlabel As ErrorLabel
    Private selectedcolor As Color
    Private service As Services
    Private DalyvioId As Int32
    Private DalyvioName As String = ""
    Private fullTable As Boolean = False
    Private Address As String = ""
    Private previousRowIndex As Integer = -1
    Private WithEvents bgWorker As New BackgroundWorker
    Private killProg As Boolean = False
    Private WasLoaded As Boolean = False
    Private provider As IConnectionStringProvider

    Public Sub New()
        InitializeComponent()
        provider = New ConfigurationManagerConnectionStringProvider()
        ' Configure BackgroundWorker
        bgWorker.WorkerReportsProgress = True
        bgWorker.WorkerSupportsCancellation = True
        AddHandler grdLocation.DataBindingComplete, AddressOf grdLocation_DataBindingComplete
    End Sub
    Private Sub frmFindOderJde_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        errorlabel = New ErrorLabel(ErrorMessage, selectedcolor, provider)
        checknetwork = New NetworkCheck(provider)

        If checknetwork.checknetwork Then
            errorlabel.ShowMessage("sekmingai prisijungta ", True)
        Else
            errorlabel.ShowMessage("Nepavyko Prisijungti ", False)
            errorlabel.LogErrorToDB("Nepavyko Prisijungti ")
            killProg = True
            Me.Close()
        End If

        service = New Services(provider)
        LoadGrdDalyviai()
        LoadGrdSearch()
        ProgressBar.Visible = False
        loadcontextmenu()
    End Sub
    
    Private Sub frmFindOderJde_Close(sender As System.Object, e As System.EventArgs) Handles MyBase.FormClosing
        If Not killProg Then
            grdForDalyviai.SaveFormSizeToDB(Me.Name, Me.Width, Me.Height)
            If WasLoaded Then
                grdSearch.SaveColumnOrder()
            End If
            grdForDalyviai.SaveColumnOrder()
        End If
    End Sub
    Private Sub LoadGrdSearch()
        grdSearch = New Grid("laikinaLentele", grdLocation, errorlabel, provider)

        Dim CustomListOfColumns As List(Of String) = MakeCustomGrid()
        grdSearch.BuildDataTableForGrdSearch(CustomListOfColumns)
    End Sub
    Private Sub LoadGrdDalyviai()

        grdForDalyviai = New Grid(grdDalyviai, "ADDRESS_LIST", "SELECT * from ADDRESS_LIST where ADDRESS <> 'NULL'", errorlabel, provider)
        grdForDalyviai.LoadFormSizeFromDB(Me.Name, Me)
        grdForDalyviai.BindParams()
        loadGridSettings()

    End Sub
    Private Sub loadGridSettings()
        grdForDalyviai.AdjustColumnAlignmentByDataType()
        
        grdForDalyviai.GetColumnOrder()
        

    End Sub
    Private Sub loadcontextmenu()
        grdForDalyviai.ExternalContextMenuForHeaders = ContextMenuStrip1
        grdSearch.ExternalContextMenuForHeaders = ContextMenuStrip1
    End Sub
    Private Sub grdLocation_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles grdLocation.DataBindingComplete
        If grdLocation.Rows.Count > 0 Then
            grdLocation.ClearSelection()
            grdLocation.Rows(0).Selected = True
            'grdLocation.CurrentCell = grdLocation.Rows(0).Cells(0)

        End If
    End Sub
    Private Sub BtnSendOrder_Click(sender As System.Object, e As System.EventArgs) Handles BtnSendOrder.Click
        makeSearchRequest()
    End Sub
    Private Sub makeSearchRequest()
        If Not String.IsNullOrEmpty(txtSentData.Text) Then
            Try
                Dim urlString As String = service.GetResponseString("https://geocode.search.hereapi.com/v1/geocode?apikey=Xv5Vmevz6kMPEDM7oxopmzOB6Db1l2_1b-f3aDzXedA&q=" & txtSentData.Text)
                Dim root As Root = JsonConvert.DeserializeObject(Of Root)(urlString)

                Me.Invoke(Sub()

                              grdSearch.datatableClear()
                              grdSearch.BindingGrid(root)
                              grdSearch.FillLocationGrid()
                              If Not WasLoaded Then
                                  grdSearch.AdjustColumnAlignmentByDataType()
                                  grdSearch.GetColumnOrder()
                                  WasLoaded = True
                              End If

                              ' Selection logic moved to DataBindingComplete event
                          End Sub)
            Catch ex As Exception
                Me.Invoke(Sub()
                              errorlabel.ShowMessage("err id - 5 " & ex.Message, False)
                              errorlabel.LogErrorToDB("err id - 5 " & ex.Message)
                          End Sub)
            End Try
        Else
            Me.Invoke(Sub()
                          errorlabel.ShowMessage("Tuscias paieskos Laukas", False)
                      End Sub)
        End If
    End Sub


    'Private Sub tabControl_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MainTab.SelectedIndexChanged
    '    If MainTab.SelectedTab Is PanelSearch Then
    '        makeSearchRequest()
    '    End If
    'End Sub

    Private Function MakeCustomGrid() As List(Of String)
        Dim columns As New List(Of String) From {
            "Title",
            "ID",
            "Result Type",
            "House Number Type",
            "Address Label",
            "Country Code",
            "Country Name",
            "State",
            "County",
            "City",
            "District",
            "Street",
            "Postal Code",
            "House Number",
            "Position Lat",
            "Position Lng",
            "Access Lat",
            "Access Lng",
            "MapView West",
            "MapView South",
            "MapView East",
            "MapView North",
            "Query Score",
            "Field Score City",
            "Field Score Streets",
            "Field Score House Number"
        }

        Return columns

    End Function
    Public Sub PopulateTextBoxes(txtDalyvioId As TextBox, txtDalyvioName As TextBox, selectedRow As DataGridViewRow)
        Try
            ' Make text boxes editable
            txtDalyvioId.ReadOnly = False
            txtDalyvioName.ReadOnly = False

            ' Ensure the necessary cells are not null before accessing them
            If selectedRow.Cells("ID").Value IsNot Nothing AndAlso Not IsDBNull(selectedRow.Cells("ID").Value) AndAlso
               selectedRow.Cells("ACCOUNTNUM").Value IsNot Nothing AndAlso Not IsDBNull(selectedRow.Cells("ACCOUNTNUM").Value) AndAlso
               selectedRow.Cells("ADDRESS").Value IsNot Nothing AndAlso Not IsDBNull(selectedRow.Cells("ADDRESS").Value) Then

                ' Retrieve the values from the selected row
                DalyvioId = Convert.ToInt32(selectedRow.Cells("ID").Value)
                DalyvioName = selectedRow.Cells("ACCOUNTNUM").Value.ToString().Trim()
                Address = selectedRow.Cells("ADDRESS").Value.ToString().Trim()

                ' Place the values into the text boxes
                txtDalyvioId.Text = DalyvioId.ToString()
                txtDalyvioName.Text = DalyvioName
                txtSentData.Text = Address

                ' Make text boxes read-only again
                txtDalyvioId.ReadOnly = True
                txtDalyvioName.ReadOnly = True
            Else
                errorlabel.ShowMessage("Pasirinkta eilute neturi duomenu", False)
            End If
        Catch ex As Exception
            errorlabel.ShowMessage("err id- 1: " & ex.Message, False)
            errorlabel.LogErrorToDB("err id- 1: " & ex.Message)
        End Try
    End Sub

    Private Sub BtnUpdate_Click(sender As System.Object, e As System.EventArgs) Handles BtnUpdate.Click
        MakeUpdate()
        refreshDalyviai()
        grdSearch.cleardata()
    End Sub
    Private Sub EnsureFirstRowSelected()
        If grdDalyviai.Rows.Count > 0 AndAlso grdDalyviai.SelectedRows.Count = 0 Then
            grdDalyviai.Rows(0).Selected = True
        End If
        If grdLocation.Rows.Count > 0 AndAlso grdLocation.SelectedRows.Count = 0 Then
            grdLocation.Rows(0).Selected = True
        End If
    End Sub

    Private Function MakeUpdate() As Boolean
        Try
            If grdDalyviai.InvokeRequired Then
                ' If we are not on the UI thread, use Invoke to call this function on the UI thread
                Return CType(grdDalyviai.Invoke(New Func(Of Boolean)(AddressOf MakeUpdate)), Boolean)
            Else
                ' We are on the UI thread

                If grdDalyviai.SelectedRows.Count > 0 Then
                    Dim selectedRow As DataGridViewRow = grdDalyviai.SelectedRows(0)
                    Dim DalyvioId As Integer = Convert.ToInt32(selectedRow.Cells("ID").Value)
                    Dim DalyvioName As String = selectedRow.Cells("ACCOUNTNUM").Value.ToString().Trim()

                    ' Call the InsertAddress method
                    If Me.grdSearch.InsertAddress(DalyvioId, DalyvioName, CBool(Me.chkToUpper.Checked)) Then
                        SearchBox.Text = ""
                        errorlabel.ShowMessage("Sekmingai atnaujinta: " & DalyvioName, True)
                        Return True
                    Else
                        errorlabel.ShowMessage("Nepavyko atnaujinti: " & DalyvioName, False)
                        Return False
                    End If
                Else
                    errorlabel.ShowMessage("Nepasirinkta eilute", False)
                    Return False
                End If
            End If
        Catch ex As Exception
            errorlabel.ShowMessage("err id-12: " + ex.Message, False)
            errorlabel.LogErrorToDB("err id-12: " + ex.Message)
            Return False
        End Try
    End Function

    Private Sub MakeUpdateMultipleUpdate(worker As BackgroundWorker, e As DoWorkEventArgs)
        Try

            

            Dim totalSelectedRows As Integer = grdDalyviai.SelectedRows.Count
            Dim processedRows As Integer = 0

            For Each selectedRow As DataGridViewRow In grdDalyviai.SelectedRows
                If worker.CancellationPending Then
                    e.Cancel = True
                    Exit For
                End If

                If selectedRow.Cells("ADDRESS").Value IsNot Nothing AndAlso
                   Not IsDBNull(selectedRow.Cells("ADDRESS").Value) AndAlso
                   Not String.IsNullOrWhiteSpace(selectedRow.Cells("ADDRESS").Value.ToString()) AndAlso
                   Not selectedRow.Cells("ADDRESS").Value.ToString().Trim().ToUpper() = "NULL" Then

                    Dim DalyvioId As Integer = Convert.ToInt32(selectedRow.Cells("ID").Value)
                    Dim DalyvioName As String = selectedRow.Cells("ACCOUNTNUM").Value.ToString().Trim()

                    Me.Invoke(Sub()
                                  grdForDalyviai.FetchAndDisplayAddressData(DalyvioId)
                                  PopulateTextBoxes(txtDalyvioId, txtDalyvioName, selectedRow)
                                  txtSentData.Text = Address
                                  makeSearchRequest()
                                  If Me.grdSearch.InsertAddress(DalyvioId, DalyvioName, CBool(Me.chkToUpper.Checked)) Then
                                      SearchBox.Text = ""
                                      errorlabel.ShowMessage("Sekmingai atnaujintas adresas: " & DalyvioName, True)
                                  Else
                                      errorlabel.ShowMessage("Nepavyko atnaujinti adreso: " & DalyvioName, False)
                                      errorlabel.LogErrorToDB("Nepavyko atnaujinti adreso: " & DalyvioName)
                                  End If

                              End Sub)
                    processedRows += 1
                    Dim progress As Integer = CInt((processedRows / totalSelectedRows) * 100)
                    worker.ReportProgress(progress, "Processing row " + processedRows.ToString + " of " + totalSelectedRows.ToString)
                End If
            Next


        Catch ex As Exception
            Me.Invoke(Sub()
                          errorlabel.ShowMessage("err id-122: " & ex.Message, False)
                          errorlabel.LogErrorToDB("err id-122: " & ex.Message)
                      End Sub)
        End Try
    End Sub



    Public Sub IterateAndPerformActions(worker As BackgroundWorker, e As DoWorkEventArgs)
        Try
            ' Iterate through all rows in grdDalyviai
            Dim totalRows As Integer = grdDalyviai.Rows.Count
            Dim recordsFound As Boolean = False

            For i As Integer = 0 To totalRows - 1
                If worker.CancellationPending Then
                    e.Cancel = True
                    Exit For
                End If

                Dim row As DataGridViewRow = grdDalyviai.Rows(i)
                ' Check if the ADDRESS cell is not empty, not null, and not the string "NULL"
                If row.Cells("ADDRESS").Value IsNot Nothing AndAlso Not IsDBNull(row.Cells("ADDRESS").Value) AndAlso
                   Not String.IsNullOrWhiteSpace(row.Cells("ADDRESS").Value.ToString()) AndAlso
                   Not row.Cells("ADDRESS").Value.ToString().Trim().ToUpper() = "NULL" Then

                    ' Call makeSearchRequest()
                    makeSearchRequest()

                    ' If any records were found, check if the first row in grdLocation is selected and call MakeUpdate
                    'If grdLocation.Rows.Count > 0 AndAlso grdLocation.Rows(0).Selected = True Then
                    Me.Invoke(Sub()
                                  MakeUpdate()
                              End Sub)
                    'End If
                End If

                ' Report progress

                Dim progress As Integer = CInt((i / totalRows) * 100)
                worker.ReportProgress(progress, "Processing row {i + 1} of {totalRows}")
            Next

        Catch ex As Exception
            Me.Invoke(Sub()
                          errorlabel.ShowMessage("err id-122: " & ex.Message, False)
                          errorlabel.LogErrorToDB("err id-122: " & ex.Message)
                      End Sub)
        End Try
    End Sub



    Private Sub BtnRefresh_Click(sender As System.Object, e As System.EventArgs) Handles BtnRefresh.Click

        refreshDalyviai()
    End Sub

    Private Sub refreshDalyviai()
        grdForDalyviai.SaveCurrentSelectedRowId()
        If fullTable Then
            grdForDalyviai.RefreshDataFullTable()
        Else
            grdForDalyviai.RefreshData()
        End If
        grdForDalyviai.ReselectSavedRow()
    End Sub

    Private Sub BtnFullTable_Click(sender As System.Object, e As System.EventArgs) Handles BtnFullTable.Click
        If Not fullTable Then
            BtnFullTable.Text = "Įrašai be adreso"
            fullTable = True
            refreshDalyviai()
        Else
            BtnFullTable.Text = "Pilna lentele"
            fullTable = False
            refreshDalyviai()
        End If
    End Sub
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim searchValue As String = SearchBox.Text.Trim()

        If String.IsNullOrEmpty(searchValue) Then
            refreshDalyviai()
        Else
            Dim columnName As String = "ACCOUNTNUM" ' 
            grdForDalyviai.SearchAndDisplayResults(columnName, searchValue)
        End If
    End Sub
    Private Sub grdDalyviai_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdDalyviai.CellClick
        Try
            If e.RowIndex >= 0 AndAlso e.RowIndex < grdDalyviai.Rows.Count Then
                If e.RowIndex <> previousRowIndex Then
                    previousRowIndex = e.RowIndex
                    Dim selectedRow As DataGridViewRow = grdDalyviai.Rows(e.RowIndex)
                    If selectedRow.Cells("ID").Value IsNot Nothing AndAlso Not IsDBNull(selectedRow.Cells("ID").Value) Then
                        Dim nowSelected As Int32 = Convert.ToInt32(selectedRow.Cells("ID").Value)
                        'grdForDalyviai.FetchAndDisplayAddressData(nowSelected)
                        PopulateTextBoxes(txtDalyvioId, txtDalyvioName, selectedRow)
                        txtSentData.Text = Address
                        makeSearchRequest()
                    Else
                        errorlabel.ShowMessage("Nerastas ID", False)
                    End If
                End If
            End If
        Catch ex As Exception
            errorlabel.ShowMessage("err id-21 " & ex.Message, False)
            errorlabel.LogErrorToDB("err id-21 " & ex.Message)
        End Try
    End Sub


    Private Sub ToolStripButton1_Click(sender As System.Object, e As System.EventArgs) Handles btUpdateSelected.Click
        If Not bgWorker.IsBusy Then
            ProgressBar.Visible = True
            bgWorker.RunWorkerAsync()
        End If
    End Sub
    Private Sub bgWorker_DoWork(sender As Object, e As DoWorkEventArgs) Handles bgWorker.DoWork
        Dim worker As BackgroundWorker = CType(sender, BackgroundWorker)
        EnsureFirstRowSelected()
        MakeUpdateMultipleUpdate(bgWorker, e)

    End Sub
    Private Sub bgWorker_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles bgWorker.ProgressChanged
        ' Ensure the progress value is within the valid range
        Dim progressValue As Integer = Math.Max(ProgressBar.Minimum, Math.Min(ProgressBar.Maximum, e.ProgressPercentage))

        ' Update progress UI
        ProgressBar.Value = progressValue

        ' Optionally display the progress message
        ' errorlabel.ShowMessage(e.UserState.ToString(), False)
    End Sub


    Private Sub bgWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bgWorker.RunWorkerCompleted
        If e.Error IsNot Nothing Then
            errorlabel.ShowMessage("err id-11 " & e.Error.Message, False)
            errorlabel.LogErrorToDB("err id-11 " & e.Error.Message)
            ProgressBar.Visible = False

        ElseIf e.Cancelled Then
            errorlabel.ShowMessage("operacija buvo nutraukta", False)
            ProgressBar.Visible = False
        Else
            errorlabel.ShowMessage("Opracija Atlikta", True)
            ProgressBar.Visible = False
        End If
        refreshDalyviai()
        grdSearch.cleardata()
    End Sub


    'Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles btUpdateSelected.Click
    '    MakeUpdateMultipleUpdate(bgWorker, e)
    '    refreshDalyviai()
    '    grdSearch.cleardata()
    'End Sub
    Private Sub PakeistiPavadinimaToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles PakeistiPavadinimaToolStripMenuItem.Click


        If grdForDalyviai.ChangeHeaderName() Then
        Else
            grdSearch.ChangeHeaderName()
        End If

    End Sub


End Class
