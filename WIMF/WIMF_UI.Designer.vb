<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class WIMF_UI
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WIMF_UI))
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopierToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopierAdresseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopierNomToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripDropDownButton1 = New System.Windows.Forms.ToolStripDropDownButton()
        Me.CatalogMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RechargerLesCataloguesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.MinimiserDansLeSystrayToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DémarrerDansLeSystrayToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToujoursVisibleToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.TraiterCommeUneUniqueChaineToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TraiterCommeUneUniqueExpressionRégulièreToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.IgnorerLesSéparateursToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.VérifierSiLeFichierExisteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AssociateMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShortcutMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.AProposToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripProgressBar1 = New System.Windows.Forms.ToolStripProgressBar()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.ListView1 = New WIMF.myListView()
        Me.Fichier = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Extension = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Taille = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Répertoire = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.DateDoc = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TimerListView = New System.Windows.Forms.Timer(Me.components)
        Me.TimerBeginSearch = New System.Windows.Forms.Timer(Me.components)
        Me.ContextMenuStrip1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AToolStripMenuItem, Me.CopierToolStripMenuItem, Me.CopierAdresseToolStripMenuItem, Me.CopierNomToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(152, 92)
        '
        'AToolStripMenuItem
        '
        Me.AToolStripMenuItem.Name = "AToolStripMenuItem"
        Me.AToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
        Me.AToolStripMenuItem.Text = "Localiser"
        '
        'CopierToolStripMenuItem
        '
        Me.CopierToolStripMenuItem.Name = "CopierToolStripMenuItem"
        Me.CopierToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
        Me.CopierToolStripMenuItem.Text = "Copier"
        '
        'CopierAdresseToolStripMenuItem
        '
        Me.CopierAdresseToolStripMenuItem.Name = "CopierAdresseToolStripMenuItem"
        Me.CopierAdresseToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
        Me.CopierAdresseToolStripMenuItem.Text = "Copier adresse"
        '
        'CopierNomToolStripMenuItem
        '
        Me.CopierNomToolStripMenuItem.Name = "CopierNomToolStripMenuItem"
        Me.CopierNomToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
        Me.CopierNomToolStripMenuItem.Text = "Copier nom"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripDropDownButton1, Me.ToolStripProgressBar1, Me.ToolStripStatusLabel1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 452)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1286, 22)
        Me.StatusStrip1.TabIndex = 2
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripDropDownButton1
        '
        Me.ToolStripDropDownButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripDropDownButton1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CatalogMenuItem, Me.RechargerLesCataloguesToolStripMenuItem, Me.ToolStripSeparator1, Me.MinimiserDansLeSystrayToolStripMenuItem, Me.DémarrerDansLeSystrayToolStripMenuItem, Me.ToujoursVisibleToolStripMenuItem, Me.ToolStripSeparator5, Me.TraiterCommeUneUniqueChaineToolStripMenuItem, Me.TraiterCommeUneUniqueExpressionRégulièreToolStripMenuItem, Me.IgnorerLesSéparateursToolStripMenuItem, Me.ToolStripSeparator4, Me.VérifierSiLeFichierExisteToolStripMenuItem, Me.AssociateMenuItem, Me.ShortcutMenuItem, Me.ToolStripSeparator3, Me.AProposToolStripMenuItem})
        Me.ToolStripDropDownButton1.Image = CType(resources.GetObject("ToolStripDropDownButton1.Image"), System.Drawing.Image)
        Me.ToolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripDropDownButton1.Name = "ToolStripDropDownButton1"
        Me.ToolStripDropDownButton1.Size = New System.Drawing.Size(67, 20)
        Me.ToolStripDropDownButton1.Text = "Menu"
        '
        'CatalogMenuItem
        '
        Me.CatalogMenuItem.Name = "CatalogMenuItem"
        Me.CatalogMenuItem.Size = New System.Drawing.Size(281, 22)
        Me.CatalogMenuItem.Text = "Gérer les catalogues"
        '
        'RechargerLesCataloguesToolStripMenuItem
        '
        Me.RechargerLesCataloguesToolStripMenuItem.Name = "RechargerLesCataloguesToolStripMenuItem"
        Me.RechargerLesCataloguesToolStripMenuItem.Size = New System.Drawing.Size(281, 22)
        Me.RechargerLesCataloguesToolStripMenuItem.Text = "Recharger les catalogues"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(278, 6)
        '
        'MinimiserDansLeSystrayToolStripMenuItem
        '
        Me.MinimiserDansLeSystrayToolStripMenuItem.CheckOnClick = True
        Me.MinimiserDansLeSystrayToolStripMenuItem.Name = "MinimiserDansLeSystrayToolStripMenuItem"
        Me.MinimiserDansLeSystrayToolStripMenuItem.Size = New System.Drawing.Size(281, 22)
        Me.MinimiserDansLeSystrayToolStripMenuItem.Text = "Minimiser dans le systray"
        '
        'DémarrerDansLeSystrayToolStripMenuItem
        '
        Me.DémarrerDansLeSystrayToolStripMenuItem.CheckOnClick = True
        Me.DémarrerDansLeSystrayToolStripMenuItem.Name = "DémarrerDansLeSystrayToolStripMenuItem"
        Me.DémarrerDansLeSystrayToolStripMenuItem.Size = New System.Drawing.Size(281, 22)
        Me.DémarrerDansLeSystrayToolStripMenuItem.Text = "Démarrer dans le systray"
        '
        'ToujoursVisibleToolStripMenuItem
        '
        Me.ToujoursVisibleToolStripMenuItem.CheckOnClick = True
        Me.ToujoursVisibleToolStripMenuItem.Name = "ToujoursVisibleToolStripMenuItem"
        Me.ToujoursVisibleToolStripMenuItem.Size = New System.Drawing.Size(281, 22)
        Me.ToujoursVisibleToolStripMenuItem.Text = "Toujours visible"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(278, 6)
        '
        'TraiterCommeUneUniqueChaineToolStripMenuItem
        '
        Me.TraiterCommeUneUniqueChaineToolStripMenuItem.CheckOnClick = True
        Me.TraiterCommeUneUniqueChaineToolStripMenuItem.Name = "TraiterCommeUneUniqueChaineToolStripMenuItem"
        Me.TraiterCommeUneUniqueChaineToolStripMenuItem.Size = New System.Drawing.Size(281, 22)
        Me.TraiterCommeUneUniqueChaineToolStripMenuItem.Text = "Traiter comme une unique chaine"
        '
        'TraiterCommeUneUniqueExpressionRégulièreToolStripMenuItem
        '
        Me.TraiterCommeUneUniqueExpressionRégulièreToolStripMenuItem.CheckOnClick = True
        Me.TraiterCommeUneUniqueExpressionRégulièreToolStripMenuItem.Name = "TraiterCommeUneUniqueExpressionRégulièreToolStripMenuItem"
        Me.TraiterCommeUneUniqueExpressionRégulièreToolStripMenuItem.Size = New System.Drawing.Size(281, 22)
        Me.TraiterCommeUneUniqueExpressionRégulièreToolStripMenuItem.Text = "Traiter comme une expression régulière"
        '
        'IgnorerLesSéparateursToolStripMenuItem
        '
        Me.IgnorerLesSéparateursToolStripMenuItem.CheckOnClick = True
        Me.IgnorerLesSéparateursToolStripMenuItem.Name = "IgnorerLesSéparateursToolStripMenuItem"
        Me.IgnorerLesSéparateursToolStripMenuItem.Size = New System.Drawing.Size(281, 22)
        Me.IgnorerLesSéparateursToolStripMenuItem.Text = "Ignorer les séparateurs"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(278, 6)
        '
        'VérifierSiLeFichierExisteToolStripMenuItem
        '
        Me.VérifierSiLeFichierExisteToolStripMenuItem.CheckOnClick = True
        Me.VérifierSiLeFichierExisteToolStripMenuItem.Name = "VérifierSiLeFichierExisteToolStripMenuItem"
        Me.VérifierSiLeFichierExisteToolStripMenuItem.Size = New System.Drawing.Size(281, 22)
        Me.VérifierSiLeFichierExisteToolStripMenuItem.Text = "Griser si le fichier n'existe plus"
        '
        'AssociateMenuItem
        '
        Me.AssociateMenuItem.Name = "AssociateMenuItem"
        Me.AssociateMenuItem.Size = New System.Drawing.Size(281, 22)
        Me.AssociateMenuItem.Text = "Définir une association de programme"
        '
        'ShortcutMenuItem
        '
        Me.ShortcutMenuItem.Name = "ShortcutMenuItem"
        Me.ShortcutMenuItem.Size = New System.Drawing.Size(281, 22)
        Me.ShortcutMenuItem.Text = "Définir un raccourci clavier"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(278, 6)
        '
        'AProposToolStripMenuItem
        '
        Me.AProposToolStripMenuItem.Name = "AProposToolStripMenuItem"
        Me.AProposToolStripMenuItem.Size = New System.Drawing.Size(281, 22)
        Me.AProposToolStripMenuItem.Text = "A propos ..."
        '
        'ToolStripProgressBar1
        '
        Me.ToolStripProgressBar1.MarqueeAnimationSpeed = 10
        Me.ToolStripProgressBar1.Name = "ToolStripProgressBar1"
        Me.ToolStripProgressBar1.Size = New System.Drawing.Size(300, 16)
        Me.ToolStripProgressBar1.Step = 1
        Me.ToolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.ToolStripProgressBar1.Value = 50
        Me.ToolStripProgressBar1.Visible = False
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(28, 17)
        Me.ToolStripStatusLabel1.Text = "Prêt"
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "WIMF"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.AllowDrop = True
        Me.TableLayoutPanel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.AutoSize = True
        Me.TableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.TableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1289, 35)
        Me.TableLayoutPanel1.TabIndex = 6
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel2.AutoSize = True
        Me.TableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.TableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetPartial
        Me.TableLayoutPanel2.ColumnCount = 3
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 203.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.TextBox1, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.TextBox2, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.ComboBox1, 2, 0)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(1289, 35)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'TextBox1
        '
        Me.TextBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!)
        Me.TextBox1.HideSelection = False
        Me.TextBox1.Location = New System.Drawing.Point(947, 3)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(0)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(150, 29)
        Me.TextBox1.TabIndex = 2
        '
        'TextBox2
        '
        Me.TextBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox2.Location = New System.Drawing.Point(3, 3)
        Me.TextBox2.Margin = New System.Windows.Forms.Padding(0)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(941, 29)
        Me.TextBox2.TabIndex = 1
        '
        'ComboBox1
        '
        Me.ComboBox1.DropDownWidth = 180
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.ItemHeight = 13
        Me.ComboBox1.Items.AddRange(New Object() {"Toujours", "Dernière semaine", "Dernier mois", "Dernière année", "Autre ..."})
        Me.ComboBox1.Location = New System.Drawing.Point(1100, 7)
        Me.ComboBox1.Margin = New System.Windows.Forms.Padding(0, 4, 4, 4)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(180, 21)
        Me.ComboBox1.TabIndex = 3
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel3.AutoSize = True
        Me.TableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.TableLayoutPanel3.ColumnCount = 1
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.ListView1, 0, 0)
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(0, 32)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(1286, 420)
        Me.TableLayoutPanel3.TabIndex = 7
        '
        'ListView1
        '
        Me.ListView1.AllowColumnReorder = True
        Me.ListView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListView1.BackColor = System.Drawing.SystemColors.Window
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Fichier, Me.Extension, Me.Taille, Me.Répertoire, Me.DateDoc})
        Me.ListView1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.ListView1.FullRowSelect = True
        Me.ListView1.HideSelection = False
        Me.ListView1.Location = New System.Drawing.Point(0, 0)
        Me.ListView1.Margin = New System.Windows.Forms.Padding(0)
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(1286, 420)
        Me.ListView1.TabIndex = 4
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'Fichier
        '
        Me.Fichier.Tag = "String"
        Me.Fichier.Text = "Fichier"
        Me.Fichier.Width = 300
        '
        'Extension
        '
        Me.Extension.Tag = "String"
        Me.Extension.Text = "Extension"
        '
        'Taille
        '
        Me.Taille.DisplayIndex = 3
        Me.Taille.Tag = "Size"
        Me.Taille.Text = "Taille"
        Me.Taille.Width = 80
        '
        'Répertoire
        '
        Me.Répertoire.DisplayIndex = 4
        Me.Répertoire.Tag = "String"
        Me.Répertoire.Text = "Répertoire"
        Me.Répertoire.Width = 629
        '
        'DateDoc
        '
        Me.DateDoc.DisplayIndex = 2
        Me.DateDoc.Tag = "Date"
        Me.DateDoc.Text = "Date"
        Me.DateDoc.Width = 80
        '
        'TimerListView
        '
        Me.TimerListView.Interval = 50
        '
        'TimerBeginSearch
        '
        Me.TimerBeginSearch.Interval = 200
        '
        'WIMF_UI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1286, 474)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.TableLayoutPanel3)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(300, 100)
        Me.Name = "WIMF_UI"
        Me.Text = "WIMF"
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripProgressBar1 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripDropDownButton1 As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents CatalogMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents AProposToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NotifyIcon1 As System.Windows.Forms.NotifyIcon
    Friend WithEvents DémarrerDansLeSystrayToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CopierToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CopierAdresseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Fichier As System.Windows.Forms.ColumnHeader
    Friend WithEvents Extension As System.Windows.Forms.ColumnHeader
    Friend WithEvents Taille As System.Windows.Forms.ColumnHeader
    Friend WithEvents Répertoire As System.Windows.Forms.ColumnHeader
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents MinimiserDansLeSystrayToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TraiterCommeUneUniqueExpressionRégulièreToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents AssociateMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShortcutMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TraiterCommeUneUniqueChaineToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents VérifierSiLeFichierExisteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    'Friend WithEvents ListView1 As WIMF.MyListView
    Friend WithEvents TimerListView As System.Windows.Forms.Timer
    Friend WithEvents TimerBeginSearch As System.Windows.Forms.Timer
    Friend WithEvents ListView1 As WIMF.myListView
    Friend WithEvents RechargerLesCataloguesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToujoursVisibleToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents IgnorerLesSéparateursToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents DateDoc As ColumnHeader
    Friend WithEvents CopierNomToolStripMenuItem As ToolStripMenuItem
End Class
