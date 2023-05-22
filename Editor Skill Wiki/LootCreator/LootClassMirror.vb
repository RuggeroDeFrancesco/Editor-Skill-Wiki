Public Class LootClassMirror

    Public m_Name As String
    Public minGold As Integer
    Public maxGold As Integer
    Public maxGems As Integer
    Public chippedChance As Double
    Public fineChance As Double
    Public flawlessChance As Double
    Public amethyst As Boolean
    Public diamond As Boolean
    Public emerald As Boolean
    Public ruby As Boolean
    Public sapphire As Boolean
    Public topaz As Boolean

    Public probabilityLoot As List(Of ItemLoot)

    Public alternateLoot As List(Of ItemLoot)

    Public loreTabletsLoot As TabletsLoot
End Class

Public Class ItemLoot

    Public item As ItemRef
    Public minQuantity As Integer
    Public maxQuantity As Integer
    Public probability As Double

End Class

Public Class ItemRef
    Public m_PathID As String
    Public m_FileID As String
End Class

Public Class TabletsLoot

    Public region As Integer
    Public common As TabletChance
    Public uncommon As TabletChance
    Public rare As TabletChance

End Class

Public Class TabletChance

    Public minQuantity As Integer
    Public maxQuantity As Integer
    Public probability As Double

End Class