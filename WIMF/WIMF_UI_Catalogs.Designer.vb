<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WIMF_UI_Catalogs
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
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

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WIMF_UI_Catalogs))
        Me.Button_ok = New System.Windows.Forms.Button()
        Me.ListView_Catalogs = New System.Windows.Forms.ListView()
        Me.LName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.LPath = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.LCount = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.LUpdate = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.LExtensions = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Button_ok
        '
        Me.Button_ok.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_ok.Location = New System.Drawing.Point(12, 269)
        Me.Button_ok.Name = "Button_ok"
        Me.Button_ok.Size = New System.Drawing.Size(796, 27)
        Me.Button_ok.TabIndex = 3
        Me.Button_ok.Text = "Fermer"
        Me.Button_ok.UseVisualStyleBackColor = True
        '
        'ListView_Catalogs
        '
        Me.ListView_Catalogs.AllowColumnReorder = True
        Me.ListView_Catalogs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListView_Catalogs.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.LName, Me.LPath, Me.LCount, Me.LUpdate, Me.LExtensions})
        Me.ListView_Catalogs.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListView_Catalogs.FullRowSelect = True
        Me.ListView_Catalogs.GridLines = True
        Me.ListView_Catalogs.HideSelection = False
        Me.ListView_Catalogs.Location = New System.Drawing.Point(12, 12)
        Me.ListView_Catalogs.MultiSelect = False
        Me.ListView_Catalogs.Name = "ListView_Catalogs"
        Me.ListView_Catalogs.Size = New System.Drawing.Size(796, 251)
        Me.ListView_Catalogs.TabIndex = 5
        Me.ListView_Catalogs.UseCompatibleStateImageBehavior = False
        Me.ListView_Catalogs.View = System.Windows.Forms.View.Details
        '
        'LName
        '
        Me.LName.Text = "Nom"
        Me.LName.Width = 100
        '
        'LPath
        '
        Me.LPath.Text = "Chemin"
        Me.LPath.Width = 240
        '
        'LCount
        '
        Me.LCount.Text = "Fichiers"
        '
        'LUpdate
        '
        Me.LUpdate.Text = "Dernière mise à jour"
        Me.LUpdate.Width = 140
        '
        'LExtensions
        '
        Me.LExtensions.Text = "Liste des extensions"
        Me.LExtensions.Width = 250
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Gray
        Me.Label1.Location = New System.Drawing.Point(825, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(92, 39)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Les catalogues" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "localisés sur le" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "réseau sont grisés"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'WIMF_UI_Catalogs
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(929, 303)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ListView_Catalogs)
        Me.Controls.Add(Me.Button_ok)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "WIMF_UI_Catalogs"
        Me.Text = "Catalogues"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button_ok As System.Windows.Forms.Button
    Friend WithEvents ListView_Catalogs As System.Windows.Forms.ListView
    Friend WithEvents LName As System.Windows.Forms.ColumnHeader
    Friend WithEvents LPath As System.Windows.Forms.ColumnHeader
    Friend WithEvents LUpdate As System.Windows.Forms.ColumnHeader
    Friend WithEvents LCount As System.Windows.Forms.ColumnHeader
    Friend WithEvents LExtensions As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
