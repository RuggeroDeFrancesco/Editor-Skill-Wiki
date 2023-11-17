Imports System.ComponentModel

Public Class ItemDataWindow
#Region "Property Changed"
    Implements INotifyPropertyChanged

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) _
        Implements INotifyPropertyChanged.PropertyChanged

    Private Sub OnInternalPropertyChanged(ByVal info As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(info))
    End Sub
#End Region

    Public Sub New()

        ' La chiamata è richiesta dalla finestra di progettazione.
        InitializeComponent()

        ' Aggiungere le eventuali istruzioni di inizializzazione dopo la chiamata a InitializeComponent().
        OutputBlock.DataContext = Me
        LanguageCombo.ItemsSource = [Enum].GetValues(GetType(EnumLanguage))

    End Sub

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

    Dim languageSelected As EnumLanguage = EnumLanguage.English
    Dim itemDataCreator As ItemDataCreator
    Dim itemsFolder As String
    Dim materialsFolder As String
    Dim weaponPropertiesFolder As String
    Dim armorSetsFolder As String
    Dim enchantmentsFolder As String

    Private Sub GetItemsData()
        Dim fbd As Ookii.Dialogs.Wpf.VistaFolderBrowserDialog = New Ookii.Dialogs.Wpf.VistaFolderBrowserDialog
        Dim path As String = Nothing
        Dim files As System.IO.FileInfo() = Nothing
        If fbd.ShowDialog() Then
            path = fbd.SelectedPath

        End If

        If path IsNot Nothing Then
            Dim dinfo As New System.IO.DirectoryInfo(path)
            files = dinfo.GetFiles("*.json")
        End If

        If files IsNot Nothing AndAlso files.Count <> 0 Then
            Dim itemsInserted As New List(Of String)
            itemDataCreator = New ItemDataCreator(Convert.ToInt32(languageSelected), itemsFolder, materialsFolder)
            For Each file As IO.FileInfo In files

                itemDataCreator.getItemData(file.FullName)
                If itemDataCreator.Output <> "" Then
                    OutputText &= itemDataCreator.Output
                    OutputText &= vbCrLf
                    OutputText &= vbCrLf
                End If
                itemsInserted.Add(itemDataCreator.ItemName)
            Next
        End If
        Dim stringola As String = OutputBlock.Text
    End Sub

    Private Sub GetReagentsData()
        Dim fbd As Ookii.Dialogs.Wpf.VistaFolderBrowserDialog = New Ookii.Dialogs.Wpf.VistaFolderBrowserDialog
        Dim path As String = Nothing
        Dim files As System.IO.FileInfo() = Nothing
        If fbd.ShowDialog() Then
            path = fbd.SelectedPath

        End If

        If path IsNot Nothing Then
            Dim dinfo As New System.IO.DirectoryInfo(path)
            files = dinfo.GetFiles("*.json")
        End If

        If files IsNot Nothing AndAlso files.Count <> 0 Then
            Dim itemsInserted As New List(Of String)
            itemDataCreator = New ItemDataCreator(Convert.ToInt32(languageSelected), itemsFolder, materialsFolder)
            For Each file As IO.FileInfo In files

                itemDataCreator.getAspectsData(file.FullName)
                If itemDataCreator.Output <> "" Then
                    OutputText &= itemDataCreator.Output
                    OutputText &= vbCrLf
                    OutputText &= vbCrLf
                End If
                itemsInserted.Add(itemDataCreator.itemName)
            Next
        End If
        Dim stringola As String = OutputBlock.Text
    End Sub

    Private Sub GetRecipesData()
        Try
            If itemsFolder <> "" And materialsFolder <> "" Then
                Dim OpenFileDialog1 As New Microsoft.Win32.OpenFileDialog
                OpenFileDialog1.Title = "Open File..."
                OpenFileDialog1.Multiselect = True
                OpenFileDialog1.Filter = "All Files|*.*"
                OpenFileDialog1.ShowDialog()

                'extract language data to get the correct names
                Dim outputText As String = ""

                itemDataCreator = New ItemDataCreator(Convert.ToInt32(languageSelected), itemsFolder, materialsFolder)

                itemDataCreator.getMaterialData(materialsFolder) 'collects the material data

                For Each file As String In OpenFileDialog1.FileNames
                    If System.IO.File.Exists(file) Then
                        itemDataCreator.getRecipeData(file)
                        If itemDataCreator.Output <> "" Then
                            OutputText &= itemDataCreator.Output
                            OutputText &= vbCrLf
                            OutputText &= vbCrLf
                        End If
                    Else Throw New System.Exception("File does not exist.")
                    End If
                Next

                OutputBlock.Text = outputText

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

    Private Sub GetEquipmentData()
        Try
            If itemsFolder <> "" And materialsFolder <> "" Then
                Dim OpenFileDialog1 As New Microsoft.Win32.OpenFileDialog
                OpenFileDialog1.Title = "Open File..."
                OpenFileDialog1.Multiselect = True
                OpenFileDialog1.Filter = "All Files|*.*"
                OpenFileDialog1.ShowDialog()

                'extract language data to get the correct names
                Dim outputText As String = ""

                itemDataCreator = New ItemDataCreator(Convert.ToInt32(languageSelected), itemsFolder, materialsFolder, weaponPropertiesFolder, armorSetsFolder, enchantmentsFolder)

                itemDataCreator.getMaterialData(materialsFolder) 'collects the material data

                For Each file As String In OpenFileDialog1.FileNames
                    If System.IO.File.Exists(file) Then
                        itemDataCreator.getEquipmentData(file)
                        If itemDataCreator.Output <> "" Then
                            outputText &= itemDataCreator.Output
                            outputText &= vbCrLf
                            outputText &= vbCrLf
                        End If
                    Else Throw New System.Exception("File does not exist.")
                    End If
                Next

                OutputBlock.Text = outputText

            Else
                MsgBox("Please select valid folders.")
            End If
        Catch ex As Exception
            If ex.Message = "The file selected is not an item." Then
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

    Private Sub selectMaterialsLists_Click(sender As Object, e As RoutedEventArgs)
        Dim fbd As Ookii.Dialogs.Wpf.VistaFolderBrowserDialog = New Ookii.Dialogs.Wpf.VistaFolderBrowserDialog
        If fbd.ShowDialog() Then
            materialsFolder = fbd.SelectedPath
            SelectedMaterialListsFolder.Text = materialsFolder
        End If
    End Sub

    Private Sub selectItems_Click(sender As Object, e As RoutedEventArgs)
        Dim fbd As Ookii.Dialogs.Wpf.VistaFolderBrowserDialog = New Ookii.Dialogs.Wpf.VistaFolderBrowserDialog
        If fbd.ShowDialog() Then
            itemsFolder = fbd.SelectedPath
            SelectedItemsFolder.Text = itemsFolder
        End If
    End Sub

    Private Sub selectProperties_Click(sender As Object, e As RoutedEventArgs)
        Dim fbd As Ookii.Dialogs.Wpf.VistaFolderBrowserDialog = New Ookii.Dialogs.Wpf.VistaFolderBrowserDialog
        If fbd.ShowDialog() Then
            weaponPropertiesFolder = fbd.SelectedPath
            SelectedWpFolder.Text = weaponPropertiesFolder
        End If
    End Sub

    Private Sub selectArmorSets_Click(sender As Object, e As RoutedEventArgs)
        Dim fbd As Ookii.Dialogs.Wpf.VistaFolderBrowserDialog = New Ookii.Dialogs.Wpf.VistaFolderBrowserDialog
        If fbd.ShowDialog() Then
            armorSetsFolder = fbd.SelectedPath
            SelectedArmorSetsFolder.Text = armorSetsFolder
        End If
    End Sub

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        If IO.Directory.Exists(IO.Path.Combine(Environment.CurrentDirectory, "Materials")) Then
            materialsFolder = IO.Path.Combine(Environment.CurrentDirectory, "Materials")
            SelectedMaterialListsFolder.Text = IO.Path.Combine(Environment.CurrentDirectory, "Materials")
        End If
        If IO.Directory.Exists(IO.Path.Combine(Environment.CurrentDirectory, "Items")) Then
            itemsFolder = IO.Path.Combine(Environment.CurrentDirectory, "Items")
            SelectedItemsFolder.Text = IO.Path.Combine(Environment.CurrentDirectory, "Items")
        End If

        If IO.Directory.Exists(IO.Path.Combine(Environment.CurrentDirectory, "WP")) Then
            weaponPropertiesFolder = IO.Path.Combine(Environment.CurrentDirectory, "WP")
            SelectedWpFolder.Text = IO.Path.Combine(Environment.CurrentDirectory, "WP")
        End If

        If IO.Directory.Exists(IO.Path.Combine(Environment.CurrentDirectory, "ArmorSets")) Then
            armorSetsFolder = IO.Path.Combine(Environment.CurrentDirectory, "ArmorSets")
            SelectedArmorSetsFolder.Text = IO.Path.Combine(Environment.CurrentDirectory, "ArmorSets")
        End If

        If IO.Directory.Exists(IO.Path.Combine(Environment.CurrentDirectory, "Enchantment")) Then
            enchantmentsFolder = IO.Path.Combine(Environment.CurrentDirectory, "Enchantment")
            SelectedEnchantsFolder.Text = IO.Path.Combine(Environment.CurrentDirectory, "Enchantment")
        End If

    End Sub

    Private Sub selectEnchants_Click(sender As Object, e As RoutedEventArgs)

    End Sub
End Class
