Imports System.Collections.ObjectModel
Imports System.ComponentModel
Public Class Log

#Region "Property Changed"
    Implements INotifyPropertyChanged

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) _
        Implements INotifyPropertyChanged.PropertyChanged

    Private Sub OnInternalPropertyChanged(ByVal info As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(info))
    End Sub
#End Region

    Dim commonFunctions As CommonFunctions

    Public Sub New(commonFunctions As CommonFunctions)

        ' La chiamata è richiesta dalla finestra di progettazione.
        InitializeComponent()

        ' Aggiungere le eventuali istruzioni di inizializzazione dopo la chiamata a InitializeComponent().
        OutputBlock.DataContext = Me
        OutputText = New ObservableCollection(Of String)
        Me.commonFunctions = commonFunctions
        Me.log = Me.commonFunctions.CurrentLog
    End Sub

    Private Sub UpdateLogText(entry As String) Handles log.LogUpdated
        OutputText.Add(entry)
    End Sub

    Dim WithEvents log As CommonFunctions.Log

    Private Property _OutputText As ObservableCollection(Of String)

    Public Property OutputText As ObservableCollection(Of String)
        Get
            Return Me._OutputText
        End Get
        Set(value As ObservableCollection(Of String))
            _OutputText = value
            Me.OnInternalPropertyChanged("OutputText")
        End Set
    End Property


End Class
