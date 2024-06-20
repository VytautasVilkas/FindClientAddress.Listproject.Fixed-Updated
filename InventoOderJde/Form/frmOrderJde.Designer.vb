<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFindOderJde
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFindOderJde))
        Me.ErrorMessage = New System.Windows.Forms.Label()
        Me.MessagePanel = New System.Windows.Forms.Panel()
        Me.SearchPanel = New System.Windows.Forms.Panel()
        Me.PanelForSearch = New System.Windows.Forms.Panel()
        Me.grdLocation = New System.Windows.Forms.DataGridView()
        Me.PanelTb = New System.Windows.Forms.Panel()
        Me.grdDalyviai = New System.Windows.Forms.DataGridView()
        Me.BtnSendOrder = New System.Windows.Forms.Button()
        Me.txtSentData = New System.Windows.Forms.TextBox()
        Me.BtnUpdate = New System.Windows.Forms.Button()
        Me.txtDalyvioId = New System.Windows.Forms.TextBox()
        Me.txtDalyvioName = New System.Windows.Forms.TextBox()
        Me.LBDalyvioName = New System.Windows.Forms.Label()
        Me.LBDalyvioId = New System.Windows.Forms.Label()
        Me.ProgressBar = New System.Windows.Forms.ProgressBar()
        Me.TBdalyviai = New System.Windows.Forms.ToolStrip()
        Me.BtnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.BtnFullTable = New System.Windows.Forms.ToolStripButton()
        Me.SearchBox = New System.Windows.Forms.ToolStripTextBox()
        Me.btnSearch = New System.Windows.Forms.ToolStripButton()
        Me.TabPanel = New System.Windows.Forms.Panel()
        Me.chkToUpper = New System.Windows.Forms.CheckBox()
        Me.btUpdateSelected = New System.Windows.Forms.Button()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.PakeistiPavadinimaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MessagePanel.SuspendLayout()
        Me.SearchPanel.SuspendLayout()
        Me.PanelForSearch.SuspendLayout()
        CType(Me.grdLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelTb.SuspendLayout()
        CType(Me.grdDalyviai, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TBdalyviai.SuspendLayout()
        Me.TabPanel.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ErrorMessage
        '
        Me.ErrorMessage.AutoSize = True
        Me.ErrorMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ErrorMessage.Location = New System.Drawing.Point(0, 0)
        Me.ErrorMessage.Name = "ErrorMessage"
        Me.ErrorMessage.Size = New System.Drawing.Size(0, 13)
        Me.ErrorMessage.TabIndex = 1
        '
        'MessagePanel
        '
        Me.MessagePanel.Controls.Add(Me.ErrorMessage)
        Me.MessagePanel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.MessagePanel.Location = New System.Drawing.Point(0, 813)
        Me.MessagePanel.Name = "MessagePanel"
        Me.MessagePanel.Size = New System.Drawing.Size(2024, 19)
        Me.MessagePanel.TabIndex = 3
        '
        'SearchPanel
        '
        Me.SearchPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SearchPanel.Controls.Add(Me.PanelForSearch)
        Me.SearchPanel.Location = New System.Drawing.Point(0, 140)
        Me.SearchPanel.Name = "SearchPanel"
        Me.SearchPanel.Size = New System.Drawing.Size(2021, 667)
        Me.SearchPanel.TabIndex = 1
        '
        'PanelForSearch
        '
        Me.PanelForSearch.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelForSearch.Controls.Add(Me.grdLocation)
        Me.PanelForSearch.Controls.Add(Me.PanelTb)
        Me.PanelForSearch.Location = New System.Drawing.Point(0, 0)
        Me.PanelForSearch.Name = "PanelForSearch"
        Me.PanelForSearch.Size = New System.Drawing.Size(2456, 667)
        Me.PanelForSearch.TabIndex = 0
        '
        'grdLocation
        '
        Me.grdLocation.AllowUserToAddRows = False
        Me.grdLocation.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(186, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdLocation.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdLocation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(186, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdLocation.DefaultCellStyle = DataGridViewCellStyle2
        Me.grdLocation.Location = New System.Drawing.Point(0, 488)
        Me.grdLocation.Name = "grdLocation"
        Me.grdLocation.ReadOnly = True
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(186, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdLocation.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.grdLocation.Size = New System.Drawing.Size(1451, 179)
        Me.grdLocation.TabIndex = 2
        '
        'PanelTb
        '
        Me.PanelTb.AutoScroll = True
        Me.PanelTb.Controls.Add(Me.grdDalyviai)
        Me.PanelTb.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelTb.Location = New System.Drawing.Point(0, 0)
        Me.PanelTb.Name = "PanelTb"
        Me.PanelTb.Size = New System.Drawing.Size(2456, 667)
        Me.PanelTb.TabIndex = 1
        '
        'grdDalyviai
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(186, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdDalyviai.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.grdDalyviai.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(186, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdDalyviai.DefaultCellStyle = DataGridViewCellStyle5
        Me.grdDalyviai.Location = New System.Drawing.Point(0, 0)
        Me.grdDalyviai.Name = "grdDalyviai"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(186, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdDalyviai.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.grdDalyviai.Size = New System.Drawing.Size(1451, 489)
        Me.grdDalyviai.TabIndex = 0
        '
        'BtnSendOrder
        '
        Me.BtnSendOrder.Location = New System.Drawing.Point(3, 28)
        Me.BtnSendOrder.Name = "BtnSendOrder"
        Me.BtnSendOrder.Size = New System.Drawing.Size(138, 24)
        Me.BtnSendOrder.TabIndex = 7
        Me.BtnSendOrder.Text = "Ieškoti"
        Me.BtnSendOrder.UseVisualStyleBackColor = True
        '
        'txtSentData
        '
        Me.txtSentData.Location = New System.Drawing.Point(147, 31)
        Me.txtSentData.Multiline = True
        Me.txtSentData.Name = "txtSentData"
        Me.txtSentData.Size = New System.Drawing.Size(169, 38)
        Me.txtSentData.TabIndex = 9
        '
        'BtnUpdate
        '
        Me.BtnUpdate.Location = New System.Drawing.Point(3, 53)
        Me.BtnUpdate.Name = "BtnUpdate"
        Me.BtnUpdate.Size = New System.Drawing.Size(138, 23)
        Me.BtnUpdate.TabIndex = 10
        Me.BtnUpdate.Text = "Prisegti"
        Me.BtnUpdate.UseVisualStyleBackColor = True
        '
        'txtDalyvioId
        '
        Me.txtDalyvioId.Location = New System.Drawing.Point(434, 31)
        Me.txtDalyvioId.Name = "txtDalyvioId"
        Me.txtDalyvioId.Size = New System.Drawing.Size(66, 20)
        Me.txtDalyvioId.TabIndex = 11
        '
        'txtDalyvioName
        '
        Me.txtDalyvioName.Location = New System.Drawing.Point(434, 53)
        Me.txtDalyvioName.Name = "txtDalyvioName"
        Me.txtDalyvioName.Size = New System.Drawing.Size(134, 20)
        Me.txtDalyvioName.TabIndex = 12
        '
        'LBDalyvioName
        '
        Me.LBDalyvioName.AutoSize = True
        Me.LBDalyvioName.Location = New System.Drawing.Point(321, 56)
        Me.LBDalyvioName.Name = "LBDalyvioName"
        Me.LBDalyvioName.Size = New System.Drawing.Size(107, 13)
        Me.LBDalyvioName.TabIndex = 13
        Me.LBDalyvioName.Text = "Dalyvio pavadinimas:"
        '
        'LBDalyvioId
        '
        Me.LBDalyvioId.AutoSize = True
        Me.LBDalyvioId.Location = New System.Drawing.Point(369, 34)
        Me.LBDalyvioId.Name = "LBDalyvioId"
        Me.LBDalyvioId.Size = New System.Drawing.Size(59, 13)
        Me.LBDalyvioId.TabIndex = 14
        Me.LBDalyvioId.Text = "Dalyvio ID:"
        '
        'ProgressBar
        '
        Me.ProgressBar.Location = New System.Drawing.Point(1012, 45)
        Me.ProgressBar.Name = "ProgressBar"
        Me.ProgressBar.Size = New System.Drawing.Size(357, 30)
        Me.ProgressBar.TabIndex = 8
        '
        'TBdalyviai
        '
        Me.TBdalyviai.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BtnRefresh, Me.BtnFullTable, Me.SearchBox, Me.btnSearch})
        Me.TBdalyviai.Location = New System.Drawing.Point(0, 0)
        Me.TBdalyviai.Name = "TBdalyviai"
        Me.TBdalyviai.Size = New System.Drawing.Size(2024, 25)
        Me.TBdalyviai.TabIndex = 15
        Me.TBdalyviai.Text = "ToolStrip1"
        '
        'BtnRefresh
        '
        Me.BtnRefresh.Image = CType(resources.GetObject("BtnRefresh.Image"), System.Drawing.Image)
        Me.BtnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnRefresh.Name = "BtnRefresh"
        Me.BtnRefresh.Size = New System.Drawing.Size(79, 22)
        Me.BtnRefresh.Text = "Atnaujinti"
        '
        'BtnFullTable
        '
        Me.BtnFullTable.Image = CType(resources.GetObject("BtnFullTable.Image"), System.Drawing.Image)
        Me.BtnFullTable.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnFullTable.Name = "BtnFullTable"
        Me.BtnFullTable.Size = New System.Drawing.Size(94, 22)
        Me.BtnFullTable.Text = "Pilna Lentele"
        '
        'SearchBox
        '
        Me.SearchBox.Name = "SearchBox"
        Me.SearchBox.Size = New System.Drawing.Size(100, 25)
        '
        'btnSearch
        '
        Me.btnSearch.Image = CType(resources.GetObject("btnSearch.Image"), System.Drawing.Image)
        Me.btnSearch.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(61, 22)
        Me.btnSearch.Text = "Ieškoti"
        '
        'TabPanel
        '
        Me.TabPanel.Controls.Add(Me.chkToUpper)
        Me.TabPanel.Controls.Add(Me.btUpdateSelected)
        Me.TabPanel.Controls.Add(Me.TBdalyviai)
        Me.TabPanel.Controls.Add(Me.ProgressBar)
        Me.TabPanel.Controls.Add(Me.LBDalyvioId)
        Me.TabPanel.Controls.Add(Me.LBDalyvioName)
        Me.TabPanel.Controls.Add(Me.txtDalyvioName)
        Me.TabPanel.Controls.Add(Me.txtDalyvioId)
        Me.TabPanel.Controls.Add(Me.BtnUpdate)
        Me.TabPanel.Controls.Add(Me.txtSentData)
        Me.TabPanel.Controls.Add(Me.BtnSendOrder)
        Me.TabPanel.Controls.Add(Me.SearchPanel)
        Me.TabPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabPanel.Location = New System.Drawing.Point(0, 0)
        Me.TabPanel.Name = "TabPanel"
        Me.TabPanel.Size = New System.Drawing.Size(2024, 832)
        Me.TabPanel.TabIndex = 2
        '
        'chkToUpper
        '
        Me.chkToUpper.AutoSize = True
        Me.chkToUpper.Location = New System.Drawing.Point(842, 59)
        Me.chkToUpper.Name = "chkToUpper"
        Me.chkToUpper.Size = New System.Drawing.Size(164, 17)
        Me.chkToUpper.TabIndex = 17
        Me.chkToUpper.Text = "Konvertuoti į didžiasias raides"
        Me.chkToUpper.UseVisualStyleBackColor = True
        '
        'btUpdateSelected
        '
        Me.btUpdateSelected.Location = New System.Drawing.Point(601, 37)
        Me.btUpdateSelected.Name = "btUpdateSelected"
        Me.btUpdateSelected.Size = New System.Drawing.Size(154, 38)
        Me.btUpdateSelected.TabIndex = 16
        Me.btUpdateSelected.Text = "Išsaugoti pažymėtus"
        Me.btUpdateSelected.UseVisualStyleBackColor = True
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PakeistiPavadinimaToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(180, 26)
        '
        'PakeistiPavadinimaToolStripMenuItem
        '
        Me.PakeistiPavadinimaToolStripMenuItem.Name = "PakeistiPavadinimaToolStripMenuItem"
        Me.PakeistiPavadinimaToolStripMenuItem.Size = New System.Drawing.Size(179, 22)
        Me.PakeistiPavadinimaToolStripMenuItem.Text = "Pakeisti pavadinima"
        '
        'frmFindOderJde
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(2024, 832)
        Me.Controls.Add(Me.MessagePanel)
        Me.Controls.Add(Me.TabPanel)
        Me.Name = "frmFindOderJde"
        Me.Text = "FormToJde"
        Me.MessagePanel.ResumeLayout(False)
        Me.MessagePanel.PerformLayout()
        Me.SearchPanel.ResumeLayout(False)
        Me.PanelForSearch.ResumeLayout(False)
        CType(Me.grdLocation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelTb.ResumeLayout(False)
        CType(Me.grdDalyviai, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TBdalyviai.ResumeLayout(False)
        Me.TBdalyviai.PerformLayout()
        Me.TabPanel.ResumeLayout(False)
        Me.TabPanel.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ErrorMessage As System.Windows.Forms.Label
    Friend WithEvents MessagePanel As System.Windows.Forms.Panel
    Friend WithEvents SearchPanel As System.Windows.Forms.Panel
    Friend WithEvents PanelForSearch As System.Windows.Forms.Panel
    Friend WithEvents grdLocation As System.Windows.Forms.DataGridView
    Friend WithEvents PanelTb As System.Windows.Forms.Panel
    Friend WithEvents grdDalyviai As System.Windows.Forms.DataGridView
    Friend WithEvents BtnSendOrder As System.Windows.Forms.Button
    Friend WithEvents txtSentData As System.Windows.Forms.TextBox
    Friend WithEvents BtnUpdate As System.Windows.Forms.Button
    Friend WithEvents txtDalyvioId As System.Windows.Forms.TextBox
    Friend WithEvents txtDalyvioName As System.Windows.Forms.TextBox
    Friend WithEvents LBDalyvioName As System.Windows.Forms.Label
    Friend WithEvents LBDalyvioId As System.Windows.Forms.Label
    Friend WithEvents ProgressBar As System.Windows.Forms.ProgressBar
    Friend WithEvents TBdalyviai As System.Windows.Forms.ToolStrip
    Friend WithEvents BtnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnFullTable As System.Windows.Forms.ToolStripButton
    Friend WithEvents SearchBox As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents btnSearch As System.Windows.Forms.ToolStripButton
    Friend WithEvents TabPanel As System.Windows.Forms.Panel
    Friend WithEvents btUpdateSelected As System.Windows.Forms.Button
    Friend WithEvents chkToUpper As System.Windows.Forms.CheckBox
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents PakeistiPavadinimaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
