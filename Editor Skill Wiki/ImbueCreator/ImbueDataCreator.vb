Imports System.ComponentModel
Imports Newtonsoft.Json

Public Class ImbueDataCreator

#Region "Property Changed"
    Implements INotifyPropertyChanged

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) _
        Implements INotifyPropertyChanged.PropertyChanged

    Private Sub OnInternalPropertyChanged(ByVal info As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(info))
    End Sub
#End Region

    Dim language As Integer

    Dim deserializedlanguageData As LanguageData

    Private Property _Output As String

    Public Property Output As String
        Get
            Return Me._Output
        End Get
        Set(value As String)
            _Output = value
            Me.OnInternalPropertyChanged("Output")
        End Set
    End Property

End Class
