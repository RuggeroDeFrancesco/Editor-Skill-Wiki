Imports System.ComponentModel
Public Class SeDataWindow
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
    Dim statusEffectsDataCreator As StatusEffectsDataCreator
    Dim skillsFolder As String


    Private Sub GetStatusEffectData()
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

            statusEffectsDataCreator = New StatusEffectsDataCreator(Convert.ToInt32(languageSelected), skillsFolder)
            OutputText = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "SpecialEffectsCreator/SeTableStructureStart.txt"))
            For Each file As IO.FileInfo In files

                statusEffectsDataCreator.GetStatusEffectTable(file.FullName)
                If statusEffectsDataCreator.Output <> "" Then
                    OutputText &= statusEffectsDataCreator.Output
                    OutputText &= vbCrLf
                    OutputText &= vbCrLf
                End If

            Next
            OutputText &= System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "SpecialEffectsCreator/SeTableStructureEnd.txt"))
        End If
    End Sub


    Private Sub LanguageCombo_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        If LanguageCombo.SelectedItem IsNot Nothing Then
            languageSelected = LanguageCombo.SelectedItem
        End If
    End Sub

    Private Sub selectSkills_Click(sender As Object, e As RoutedEventArgs)
        Dim fbd As Ookii.Dialogs.Wpf.VistaFolderBrowserDialog = New Ookii.Dialogs.Wpf.VistaFolderBrowserDialog
        If fbd.ShowDialog() Then
            skillsFolder = fbd.SelectedPath
            SelectedSkillsFolder.Text = skillsFolder
        End If
    End Sub


    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        If IO.Directory.Exists(IO.Path.Combine(Environment.CurrentDirectory, "Materials")) Then
            skillsFolder = IO.Path.Combine(Environment.CurrentDirectory, "Spells")
            SelectedSkillsFolder.Text = IO.Path.Combine(Environment.CurrentDirectory, "Spells")
        End If

    End Sub

End Class
