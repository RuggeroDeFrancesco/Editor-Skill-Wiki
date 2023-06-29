Imports System.ComponentModel
Public Class LootDataWindow

#Region "Property Changed"
    Implements INotifyPropertyChanged

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) _
        Implements INotifyPropertyChanged.PropertyChanged

    Private Sub OnInternalPropertyChanged(ByVal info As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(info))
    End Sub
#End Region



    Dim languageSelected As EnumLanguage = EnumLanguage.English
    Dim lootFolder As String
    Dim itemsFolder As String
    Dim lootCreator As LootCreator
    Private Property _OutputText As String

    Public Property OutputText As String
        Get
            Return Me._OutputText
        End Get
        Set(value As String)
            _OutputText = value
            Me.OnInternalPropertyChanged("OutputText")
        End Set
    End Property

    Public Sub New()

        ' La chiamata è richiesta dalla finestra di progettazione.
        InitializeComponent()

        ' Aggiungere le eventuali istruzioni di inizializzazione dopo la chiamata a InitializeComponent().
        OutputBlock.DataContext = Me
    End Sub

    Private Sub SelectMonsterButton_Click(sender As Object, e As RoutedEventArgs)
        GetLootDataOutput()
    End Sub

    Private Sub GetLootDataOutput()
        Try
            OutputText = ""
            If lootFolder <> "" And itemsFolder <> "" Then
                Dim OpenFileDialog1 As New Microsoft.Win32.OpenFileDialog
                OpenFileDialog1.Title = "Open File..."
                OpenFileDialog1.Multiselect = True
                OpenFileDialog1.Filter = "All Files|*.*"
                OpenFileDialog1.ShowDialog()

                lootCreator = New LootCreator(languageSelected, lootFolder, itemsFolder)

                For Each file As String In OpenFileDialog1.FileNames
                    Dim Path As String = file
                    If System.IO.File.Exists(Path) Then
                        lootCreator.parseData(Path)
                        OutputText &= lootCreator.Output
                        OutputText &= vbCrLf
                        OutputText &= vbCrLf
                    Else Throw New System.Exception("File does not exist.")
                    End If
                Next

            Else
                MsgBox("Please select valid folders for Attacks and Spells.")
            End If
        Catch ex As Exception
            If ex.Message = "The file selected is not a creature." Then
                MsgBox(ex.Message)
            Else
                MsgBox(ex.Message)
                MsgBox(ex.StackTrace)
            End If
        End Try

    End Sub

    Private Sub LanguageCombo_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        If LanguageCombo.SelectedItem IsNot Nothing Then
            languageSelected = LanguageCombo.SelectedItem
        End If
    End Sub

    Private Sub selectLootLists_Click(sender As Object, e As RoutedEventArgs)
        Dim fbd As Ookii.Dialogs.Wpf.VistaFolderBrowserDialog = New Ookii.Dialogs.Wpf.VistaFolderBrowserDialog
        If fbd.ShowDialog() Then
            lootFolder = fbd.SelectedPath
            SelectedLootListsFolder.Text = lootFolder
        End If
    End Sub

    Private Sub selectItems_Click(sender As Object, e As RoutedEventArgs)
        Dim fbd As Ookii.Dialogs.Wpf.VistaFolderBrowserDialog = New Ookii.Dialogs.Wpf.VistaFolderBrowserDialog
        If fbd.ShowDialog() Then
            itemsFolder = fbd.SelectedPath
            SelectedItemsFolder.Text = itemsFolder
        End If
    End Sub

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        If IO.Directory.Exists(IO.Path.Combine(Environment.CurrentDirectory, "Loot")) Then
            lootFolder = IO.Path.Combine(Environment.CurrentDirectory, "Loot")
            SelectedLootListsFolder.Text = IO.Path.Combine(Environment.CurrentDirectory, "Loot")
        End If
        If IO.Directory.Exists(IO.Path.Combine(Environment.CurrentDirectory, "Items")) Then
            itemsFolder = IO.Path.Combine(Environment.CurrentDirectory, "Items")
            SelectedItemsFolder.Text = IO.Path.Combine(Environment.CurrentDirectory, "Items")
        End If
    End Sub

End Class
