Public Class CraftingRecipeMirror

    Public m_Name As String
    Public recipeName As String
    Public useRecipeName As Boolean
    Public craftedItem As PropertyClass
    Public craftedQuantity As Integer
    Public craftingTime As Double
    Public ingredients As List(Of Ingredient)


End Class

Public Class Ingredient
    Public item As PropertyClass
    Public requiredQuantity As Integer
    Public canUseMaterial As Boolean
    Public materialGroup As Integer
End Class