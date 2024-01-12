Imports System.ComponentModel
Public Class ImbueDataWindow
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
    Dim imbueDataCreator As ImbueDataCreator



    Private Sub GetImbueData()
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

            imbueDataCreator = New ImbueDataCreator(Convert.ToInt32(languageSelected))
            OutputText = ""
            For Each file As IO.FileInfo In files
                imbueDataCreator.getImbueData(file.FullName)
                If imbueDataCreator.Output <> "" Then
                    OutputText &= imbueDataCreator.Output
                    OutputText &= vbCrLf
                    OutputText &= vbCrLf
                End If

            Next

        End If
    End Sub


    Private Sub LanguageCombo_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        If LanguageCombo.SelectedItem IsNot Nothing Then
            languageSelected = LanguageCombo.SelectedItem
        End If
    End Sub
End Class
