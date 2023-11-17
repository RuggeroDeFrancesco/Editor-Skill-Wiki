Public Class MonsterData

    Public m_name As String
    Public challengeRating As Integer
    Public difficulty As Integer
    Public halfKillsRequired As Boolean
    Public monsterRace As Integer
    Public capturable As Boolean
    Public skinAmount As Integer
    Public petItemType As PropertyClass
    Public champion As Boolean
    Public walkSpeed As Integer
    Public baseSpeed As Integer
    Public str As Strenght
    Public dex As Dexterity
    Public _int As Intelligence
    Public cos As Constitution
    Public per As Perception
    Public wis As Wisdom
    Public baseEndurance As BaseEndurance
    Public baseEnduranceRegen As BaseEnduranceRegen
    Public baseEnergy As BaseEnergy
    Public baseEnergyRegen As BaseEnergyRegen
    Public evasion As Evasion
    Public accuracy As Accuracy
    Public fortitude As Fortitude
    Public willpower As Willpower
    Public Stealth As Stealth
    Public Detection As Detection
    Public spellDamageIncrease As SpellDamageIncrease
    Public cooldownReduction As CooldownReduction
    Public baseLuck As BaseLuck
    Public criticalChance As CriticalChance
    Public criticalDamage As CriticalDamage
    Public armorSlash As ArmorSlash
    Public armorPierce As ArmorPierce
    Public armorCrush As ArmorCrush
    Public fireResistance As FireResistance
    Public coldResistance As ColdResistance
    Public shockResistance As ShockResistance
    Public magicResistance As MagicResistance
    Public poisonResistance As PoisonResistance
    Public acidResistance As AcidResistance
    Public magicalDamageReflection As Integer
    Public physicalDamageReflection As Integer
    Public slashDamageIncrease As Integer
    Public pierceDamageIncrease As Integer
    Public crushDamageIncrease As Integer
    Public fireDamageIncrease As Integer
    Public iceDamageIncrease As Integer
    Public shockDamageIncrease As Integer
    Public acidDamageIncrease As Integer
    Public poisonDamageIncrease As Integer
    Public energyDamageIncrease As Integer
    Public fireDamageConversion As Integer
    Public iceDamageConversion As Integer
    Public shockDamageConversion As Integer
    Public acidDamageConversion As Integer
    Public poisonDamageConversion As Integer
    Public energyDamageConversion As Integer
    Public monsterPoisonRank As Integer
    Public immunityEffectGroup As Integer
    Public specialImmunities As Integer
    Public playerAttitude As Integer
    Public combatStance As Integer
    Public monsterPoisonStack As Integer
    Public BroadcastAggression As Boolean

    Public lootList As PropertyClass

    Public MonsterAttacks As List(Of MonsterAttack)

    Public MonsterSpellsList As List(Of MonsterSpells)

    Public immunityStatusEffectList As List(Of PropertyClass)

    Public damageAbsorbed As List(Of Integer)

    Public allowedPoi As List(Of Integer)
End Class

Public Class Strenght
    Public value As Integer
End Class

Public Class Dexterity
    Public value As Integer
End Class

Public Class Intelligence
    Public value As Integer
End Class

Public Class Constitution
    Public value As Integer
End Class

Public Class Perception
    Public value As Integer
End Class

Public Class Wisdom
    Public value As Integer
End Class

Public Class BaseEndurance
    Public value As Integer
End Class

Public Class BaseEnduranceRegen
    Public value As Integer
End Class

Public Class BaseEnergy
    Public value As Integer
End Class
Public Class BaseEnergyRegen
    Public value As Integer
End Class
Public Class Evasion
    Public value As Integer
End Class
Public Class Accuracy
    Public value As Integer
End Class
Public Class Fortitude
    Public value As Integer
End Class
Public Class Willpower
    Public value As Integer
End Class
Public Class Stealth
    Public value As Integer
End Class
Public Class Detection
    Public value As Integer
End Class

Public Class SpellDamageIncrease
    Public value As Integer
End Class

Public Class CooldownReduction
    Public value As Integer
End Class

Public Class BaseLuck
    Public value As Integer
End Class

Public Class CriticalChance
    Public value As Integer
End Class

Public Class CriticalDamage
    Public value As Integer
End Class

Public Class ArmorSlash
    Public value As Integer
End Class

Public Class ArmorPierce
    Public value As Integer
End Class

Public Class ArmorCrush
    Public value As Integer
End Class

Public Class FireResistance
    Public value As Integer
End Class

Public Class ColdResistance
    Public value As Integer
End Class

Public Class ShockResistance
    Public value As Integer
End Class

Public Class MagicResistance
    Public value As Integer
End Class

Public Class PoisonResistance
    Public value As Integer
End Class

Public Class AcidResistance
    Public value As Integer
End Class

Public Class MonsterAttack

    Public targetToUnlock As Integer
    Public value As PropertyClass


End Class


Public Class MonsterSpells

    Public targetToUnlock As String
    Public value As PropertyClass

    Public hideInKnowledgeBook As Boolean

    Public cooldownOverride As Integer
    Public globalCooldown As Integer
    Public castingChance As Integer
    Public castingPriority As Integer
    Public targetAlly As Boolean
    Public castingRestrictions As List(Of MonsterCastingRestriction)

End Class

Public Class MonsterCastingRestriction
    Public type As Integer
    Public value As Double
    Public statusEffect As Integer
    Public statusEffectGroup As Integer
End Class

'Public Class StatusEffect
'    Public Property m_PathID As Integer
'End Class


'Public Class LootList
'    Public m_FileID As String
'    Public m_PathID As String
'End Class

'Public Class PetItemType
'    Public m_FileID As String
'    Public m_PathID As String
'End Class



'Public Class Value

'    Public m_fileID As String
'    Public m_pathID As String

'End Class