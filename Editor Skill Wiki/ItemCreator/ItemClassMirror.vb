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
    Public ExplicitArmorSetData As FullSetArmorData
    Public armorSetData As PropertyClass
    Public armorPieceType As Integer
    Public enchantClass As Integer
    Public EquipSlot As Integer
    Public EquipType As Integer
    Public EquipWeightClass As Integer
    Public isPrimitive As Boolean
    Public WeaponType As Integer
    Public WeaponClass As Integer
    Public avaibleCastingSchool As Integer
    Public twoHanded As Boolean
    Public poisonable As Boolean
    Public spellChanneling As Boolean
    Public damage As Damage
    Public attackSpeed As Integer
    Public weaponDamageType As Integer
    Public damageWidth As Double
    Public damageAmplitude As Double
    Public damageLength As Double
    Public projectileSpeed As Double
    Public projectileDespawn As Double
    Public materialGroup As Integer
    Public weaponProperties As List(Of WeaponProperty)

    'proprietà Orb
    Public school As Integer

    'proprietà consumabili
    Public hotbarItem As Boolean
    Public removeEffect As Integer
    Public cooldownGroup As Integer
    Public statInfo As StatInfoElement

    'proprietà veleni
    Public poisonTier As String

    'proprietà unlockable recipe
    Public unlockedRecipe As PropertyClass
    Public gameReady As Boolean

End Class

Public Class MagicAspectValue

    Public aspect As Integer
    Public value As Integer

End Class

Public Class Damage

    Public dmgType1 As Integer
    Public dmgType2 As Integer
    Public dmgType3 As Integer
    Public damageModifier As Double
    Public dmgAttr1 As Integer
    Public dmgAttr2 As Integer

End Class

Public Class WeaponProperty
    Public [Property] As PropertyClass
    Public baseAmount As Double
End Class

Public Class WeaponPropertyData
    Public localizationString As String
    Public statusEffect As PropertyClass
    Public stunDuration As String
    Public enchantment As PropertyClass
    Public necroticStatus As PropertyClass
    Public isPercentage As Boolean
    Public m_Name As String
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
    Public heatInsulation As Integer
    Public coldInsulation As Integer
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
    Public weight As Double
    Public weightClass As Integer
End Class

Public Class StatInfoElement
    Public amount As Double
    Public amountPerAttr As Double
    Public regenDuration As Double
    Public amountAttrMod As Integer
    Public statType As Integer
End Class

Public Class EnchantmentRecipe
    Public recipeName As String
End Class


