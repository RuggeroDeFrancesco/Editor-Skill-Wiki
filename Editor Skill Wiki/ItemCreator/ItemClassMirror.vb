Public Class ItemClassMirror
    'proprietà base
    Public Description As String
    Public MaxStackCount As Integer
    Public Name As String
    Public Stackable As Boolean
    Public Weight As Double
    Public legendary As Boolean
    Public ItemTypeID As Integer
    Public toolCategory As Integer
    Public itemCategory As Integer
    Public itemTooltipCategory As Integer
    Public marketPlaceFilter As Integer
    Public PowerAspects As List(Of MagicAspectValue)
    Public reagentRarity As Integer

    Public icon As PropertyClass

    'proprietà equip
    Public armorSetData As FullSetArmorData
    Public armorPieceType As Integer
    Public enchantClass As Integer
    Public EquipSlot As Integer
    Public EquipType As Integer
    Public EquipWeightClass As Integer
    Public isPrimitive As Boolean
    Public weaponProperties As List(Of WeaponProperty)

    'proprietà Orb
    Public school As Integer

    'proprietà consumabili
    Public hotbarItem As Boolean
    Public removeEffect As Integer
    Public cooldownGroup As Integer
    Public statInfo As StatInfoElement

    'proprietà unlockable recipe
    Public unlockedRecipe As PropertyClass
    Public gameReady As Boolean

End Class

Public Class MagicAspectValue

    Public aspect As Integer
    Public value As Integer

End Class

Public Class Damage

    Public dmgType As Integer
    Public secondDmgType As Integer
    Public thirdDamageType As Integer
    Public damageModifier As Double
    Public damageAttribute As Integer
    Public secondDamageAttribute As Integer

End Class

Public Class WeaponProperty
    Public [Property] As PropertyClass
    Public baseAmount As Double
End Class

Public Class PropertyClass
    Public m_fileID As String
    Public m_PathID As String
End Class

Public Class FullSetArmorData
    Public armor_Crush As Integer
    Public armor_Pierce As Integer
    Public armor_Slash As Integer
    Public armorEncumbrance As Integer
    Public armorSetBonus As Integer
    Public coldInsultaion As Integer
    Public durability As Integer
    Public magicalArmor As Integer
    Public magicalResistance_Acid As Integer
    Public magicalResistance_Energy As Integer
    Public magicalResistance_Fire As Integer
    Public magicalResistance_Ice As Integer
    Public magicalResistance_Poison As Integer
    Public magicalResistance_Shock As Integer
    Public materialGroup As Integer
    Public physicalArmor As Integer
    Public tier As Integer
    Public weight As Integer
    Public weightClass As Integer
End Class

Public Class StatInfoElement
    Public amount As Double
    Public amountPerAttr As Double
    Public regenDuration As Double
    Public amountAttrMod As Integer
    Public statType As Integer
End Class

