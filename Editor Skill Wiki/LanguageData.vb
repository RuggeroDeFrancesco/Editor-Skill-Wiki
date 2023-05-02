Public Class LanguageData
    Public Property mSource As MSource

    Public Sub New()
        mSource = New MSource
    End Sub

    Public Function returnTerm(term As String, language As Integer) As String
        Dim output As String = ""
        If mSource.mTerms IsNot Nothing AndAlso mSource.mTerms.Count <> 0 Then
            For i = 0 To mSource.mTerms.Count - 1
                If UCase(mSource.mTerms(i).Term) = UCase(term) Then
                    output = mSource.mTerms(i).Languages(language)
                End If
            Next
        End If
        Return output
    End Function


End Class

Public Class MSource

    Public Sub New()
        mTerms = New List(Of MTerms)
    End Sub

    Public Property mTerms As List(Of MTerms)

End Class

Public Class MTerms

    Public Sub New()
        Languages = New List(Of String)
    End Sub

    Public Property Term As String
    Public Property TermType As String
    Public Property Languages As List(Of String)

End Class