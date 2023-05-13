Public Class ItemClassMirror

    Public Description As String
    Public MaxStackCount As Integer
    Public Name As String
    Public Stackable As Boolean
    Public Wieght As Double
    Public legendary As Boolean
    Public ItemTypeID As Integer
    Public toolCategory As Integer

    Public PowerAspects As List(Of MagicAspectValue)

End Class

Public Class MagicAspectValue

    Public aspect As Integer
    Public value As Integer

End Class
