Public Class StatusEffectClassMirror

    Public Beneficial As Boolean
    Public DontSetCombatFlag As Boolean
    Public effectGroup As Integer
    Public icon As PropertyClass
    Public irremovable As Boolean
    Public KeepWhenMounted As Boolean
    Public statusKey As String

    'Atrophied
    Public dexHardCap As Double

    'Bleeding

    Public minTargetHeatlhClamp As Double
    Public maxTargetHealthClamp As Double
    Public minLossPerc As Double
    Public maxLossPerc As Double
    Public effectFrequency As Double
    Public stackLossFrequency As Double
    Public stackLossAmount As Double
    Public maxStack As Double
    Public movementEffectFrequency As Double

    'blinded

    Public perceptionHardCap As Double

    'burning

    Public fixedDuration As Double
    Public fireResitDebuff As Double
    Public damagePerTick As Double
    Public tickInterval As Double
    Public warmStackOnExpiration As Double

    'chilled

    Public iceResistDebuffPerStack As Double
    Public moveSpeedDebuffPerStackPerc As Double
    'Public stackLossFrequency As Integer
    'Public stackLossAmount As Integer
    'Public maxStack As Integer

    'confused
    Public debuff As Double
    Public spellFailurePerc As Double

    'corrosion

    Public armorDebuffPerStack As Double
    Public dmgPerStack As Double
    'Public effectFrequency As Integer
    'Public stackLossFrequency As Integer
    'Public stackLossAmount As Integer
    'Public maxStack As Integer

    'cripple

    Public dexterityHardCap As Double

    'Dazed

    Public willpowerDebuff As Double

    'on road

    Public BonusMoveSpeed As Double
    Public BonusMoveSpeedWagon As Double

    'Frightened
    Public saveModifiersDebuff As Double
    Public charismaHardCap As Double
    Public damageDebuffPerc As Double

    'Frozen
    'Public fixedDuration As Integer
    Public iceResitDebuff As Double
    Public armorBuff As Double
    Public chilledStackOnExpiration As Double

    'Hungry
    Public healthRegenDebuff As Double

    'Petrified
    Public slashArmor As Double
    Public crushArmor As Double
    Public pierceArmor As Double
    'Poisoned
    'Public minTargetHeatlhClamp As Double
    'Public maxTargetHealthClamp As Double
    'Public minLossPerc As Double
    'Public maxLossPerc As Double
    'Public effectFrequency As Double
    'Public stackLossFrequency As Double
    'Public stackLossAmount As Double
    'Public maxStack As Double

    'Rested
    Public movSpeedBuff As Double
    'Sated
    Public healthRegenBuff As Double
    'Shocked
    Public shockResistDebuffPerStack As Double
    Public shockedParalysisChancePerStack As Double
    Public shockedParalysisDuration As Double
    'Public stackLossFrequency As Double
    'Public stackLossAmount As Double
    'Public maxStack As Double

    'Silenced
    'Public charismaHardCap As Double

    'Slowed
    Public moveSpeedDebuffPerc As Double
    Public attackSpeedDebuffPerc As Double
    'Starving
    'Public healthRegenDebuff As Double

    'Tired
    Public movSpeedDebuff As Double
    'Warm
    Public fireResistDebuffPerStack As Double
    'Public stackLossFrequency As Double
    'Public stackLossAmount As Double
    'Public maxStack As Double

    'Weakened
    Public strengthHardCap As Double


End Class
