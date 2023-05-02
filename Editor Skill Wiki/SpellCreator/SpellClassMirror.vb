

Public Class SpellClassMirror

    'Class mirroring the contents of the json files of the spells

    Public m_name As String
    Public spellIndex As Integer
    Public SpellKey As String
    Public SpellSchool As Integer
    Public toggleGroup As Integer
    Public effectgroup As Integer
    Public readyTrigger As Integer
    Public friendlyFire As Integer
    Public SecureHit As Boolean
    Public dontRemoveStealth As Boolean
    Public avoidBlockControllerCheck As Boolean
    Public canBeCritical As Boolean
    Public mobilitySpell As Boolean
    Public learnSpellIndex As Integer
    Public gameReady As Boolean
    Public removed As Boolean
    Public difficulty As Integer
    Public spellGroup As Integer
    Public castChance As Double
    Public cooldown As Double
    Public cooldownModAttribute As Integer
    Public cooldownModCoefficient As Double
    Public activationType As Integer
    Public targetType As Integer
    Public TargetingRange As Double
    Public CastingRange As Double
    Public checkLos As Boolean
    Public avoidPathFinding As Boolean
    Public linkSpellDuration As Double
    Public animationType As Integer
    Public targetEntityType As Integer
    Public requireStatusOnTarget As Integer
    Public preCastTime As Double
    Public ResourceCost As Double

    Public spellData As List(Of spellDataInfo)
    Public spellcastingRestrictions As spellcastingRestrictions
    Public customData As customData

    Public Function GetCustomData() 'used to extract the list of customData names and values.
        Dim filteredCustomData As New filteredCustomData
        Dim i As Integer = 0
        For Each p As Reflection.FieldInfo In customData.GetType().GetFields()
            p = p
            If p.GetValue(customData) <> 0 Then
                filteredCustomData.name.Add(p.Name)
                filteredCustomData.value.Add(p.GetValue(customData)?.ToString())
                i += 1
            End If
        Next
        Return filteredCustomData
    End Function


End Class

Public Class spellcastingRestrictions
    Public spellChannelingWeapon As Boolean
    Public restrictedWeaponClass As Integer
    Public restrictedWeaponWeight As Integer
    Public restrictedWeaponDamage As Integer
    Public specialWeaponRestriction As Integer
    Public restrictedArmorWeight As Integer
End Class

Public Class customData 'the json files have a high number of customData fields. These fields are optional in a spell file. This class contains all of them.
    ' I've divided these fields into regions, based on thematic identification. It makes it easier to find them.

#Region "Mana"
    Public consumePerSecond As Double
    Public ManaPerSecond As Double
    Public ManaPerShot As Double
    Public energyConsumeOnHit As Double
#End Region

#Region "Non SE Durations"

    Public spellDuration As Double
    Public ChannelingDuration As Double
    Public channelingDurationPerINT As Double
    Public activationsIntervalPerInt As Double
    Public effectDurationPerINT As Double
    Public durationPerINT As Double
    Public durationPerCON As Double
    Public durationPerCONSTR As Double
    Public durationPerDEX As Double
    Public durationPerDEXPER As Double
    Public durationPerCHA As Double
    Public effectDuration As Double
    Public effectsDuration As Double
    Public debuffDuration As Double
    Public debuffDurationPerDEX As Double
    Public buffDuration As Double
    Public wallLifeSpan As Double
    Public webLifeSpanPerInt As Double
    Public jumpTime As Double
    Public dashDuration As Double
    Public movSpeedBuffPerc As Double
    Public stealthDuration As Double
    Public executeBuffDuration As Double
    Public hiddenDurationModifier As Double
    Public spectralTime As Double
    Public trapLifeSpawnInSec As Double

#End Region

#Region "SE Durations"
    Public rootDurationPerINT As Double
    Public snareDurationPerINT As Double
    Public snareDurationPerPER As Double
    Public stunDurationPerCHA As Double
    Public crippleDuration As Double
    Public crippledDuration As Double
    Public stunDuration As Double
    Public stunDurationPerQuake As Double
    Public snareDuration As Double
    Public slowDuration As Double
    Public slowDurationPerINT As Double
    Public silenceDuration As Double
    Public silencedDuration As Double
    Public blindDuration As Double
    Public dazeDuration As Double
    Public unhealSEDuration As Double
    Public petrifyDurationIntMultiplier As Double
    Public frightenedDuration As Double
    Public confusionDuration As Double
#End Region

#Region "Delays"
    Public delayActivationTime As Double
    Public ActivationDelayTime As Double
    Public DamageDelayTime As Double
    Public lineDelayDamage As Double
    Public explosionDelay As Double
    Public bounceDelaySeconds As Double
    Public chargeActivationDelay As Double
#End Region

#Region "Areas"
    Public minSpellRadius As Double
    Public maxSpellRadius As Double
    Public fireballRadius As Double
    Public iceballRadius As Double
    Public damageRadius As Double
    Public radius As Double
    Public spellRadius As Double
    Public spellRadiusPerSTR As Double
    Public areaEffectRadius As Double
    Public areaRadius As Double
    Public effectRadius As Double
    Public bounceRadius As Double
    Public boxLenght As Double
    Public boxAmplitue As Double
    Public coneWidth As Double
    Public coneLength As Double
    Public coneLenght As Double
    Public coneAmplitude As Double
    Public damageDistanceTolerance As Double
    Public knockBackDistance As Double
    Public dashDistance As Double
    Public eruptionRadius As Double
    Public eruptionKnockback As Double
#End Region

#Region "Non INT Damage"
    Public damage As Double
    Public minDamage As Double
    Public maxDamage As Double
    Public extraDamageDEXModifier As Double
    Public extraDamagePerSTR As Double
    Public extraDamagePerDEXPER As Double
    Public extraDamagePerDEX As Double
    Public extraDamagePerPER As Double
    Public dmgPerSTR As Double
    Public damagePerDEX As Double
    Public damagePerPER As Double
    Public magicDamagePerPER As Double
    Public damagePerSTR As Double
    Public damagePerCHA As Double
    Public crushDamage As Double
    Public amplifiedDamagePerTarget As Double
    Public damageFractionPercMul As Double
    Public damageReductionPerTarget As Double
    Public poisonedDamageMul As Double
    Public spikeDamage As Double
    Public eruptionDamage As Double
    Public dmgMulPercentagePerPER As Double
#End Region

#Region "INT Damage"
    Public minDmgPerINT As Double
    Public maxDmgPerINT As Double
    Public dmgPerINT As Double
    Public intDamageMultiplier As Double
    Public extraDamagePerINT As Double
    Public fireDamagePerINT As Double
    Public targetDamagePerINT As Double
    Public aoeDamagePerINT As Double
    Public dmgPerIntPerFireball As Double
    Public undeadDamagePerINT As Double
    Public rootDpsPerINT As Double
    Public dmgPerIntPerTick As Double
    Public damagePerIntPerTick As Double
    Public minDamagePerINT As Double
    Public maxDamagePerINT As Double
    Public fullDamagePerINT As Double
    Public reducedDamagePerINT As Double
    Public damagePerINT As Double
    Public dmgPerIntPerThunder As Double
#End Region

#Region "Heal"
    Public healPerINT As Double
    Public healPerCHA As Double
    Public healthPerINT As Double
    Public healthPerCHA As Double
    Public healAmount As Double
#End Region

#Region "Stacks"
    Public burningStackPerInt As Double
    Public warmStacksPerTick As Integer
    Public warmStackPerTick As Integer
    Public shockedStacksPerTick As Integer
    Public shockedStackPerTick As Integer
    Public chilledStackPerTick As Integer
    Public chilledStacksPerTick As Integer
    Public chilledStacksPerSpike As Integer
    Public corrosionStacksPerTick As Integer
    Public corrosionStacks As Integer
    Public warmStacks As Integer
    Public minShockedStacks As Integer
    Public maxShockedStacks As Integer
    Public shockedStack As Integer
    Public shockedStacks As Integer
    Public shockStack As Integer
    Public chilledStack As Integer
    Public chilledStacks As Integer
    Public bleedStackPerInt As Double
    Public bleedingStackPerInt As Double
    Public bleedStackPerSTR As Double
    Public poisonStackPerInt As Double
    Public minPoisonStack As Integer
    Public maxPoisonStack As Integer
    Public poisonStack As Integer
    Public aoeChilledStacks As Integer
    Public chilledStackPerSpike As Integer
    Public stackPerMeter As Double
#End Region

#Region "Miscellanous"
    Public magicReflectionPerINT As Double
    Public magicResistancePerINT As Double
    Public bonusFireResPerInt As Double
    Public bonusShockResPerInt As Double
    Public bonusIceResPerInt As Double
    Public bonusAcidResPerInt As Double
    Public bonusPoisonResPerInt As Double
    Public resBonusPerINT As Double
    Public effectsPowerPerSTR As Double
    Public bonusFortPerCHA As Double
    Public resistancePerCHA As Double
    Public bonusPercentagePerCON As Double
    Public bouncesPerINT As Double
    Public moveSpeedIncreasePerc As Double
    Public timeBeforeDie As Double
    Public healthPercentagePerInt As Double
    Public chancePerINT As Double
    Public spawnOffset As Double
    Public currentHealthPercThreshold As Double
    Public debuffPerINT As Double
    Public immunityThreshold As Double
    Public ignoreArmorPerc As Double
    Public bonusValuePerCHA As Double
    Public forwardOffset As Double
    Public pullCasterOffset As Double
    Public activationsCount As Double
    Public damageReductionPerINT As Double
    Public projectilesPerINT As Double
    Public shockResDebuffPerINT As Double
    Public manaLossPerINT As Double
    Public baseRestLost As Double
    Public bonusStealthPerDex As Double
    Public missingHealthPerc As Double
    Public DEX_PER_CapMultiplier As Double
    Public thundersPerINTPerTick As Double
    Public bonusStrengthPerCHA As Double
    Public regenBonusPerINT As Double
    Public projectileAmount As Double
    Public maxPlayersHealthPerc As Double
    Public maxMonsterHealth As Double
    Public absorbedDamagePercPerINT As Double
#End Region

#Region "Step and Charges"
    Public charges As Integer
    Public stackRecoverTime As Double
    Public maxCharges As Integer
    Public maxChargePerINT As Double
    Public _2RemovalsPER As Integer
    Public _3RemovalsPER As Integer
    Public _5SpikesINT As Integer
    Public _7SpikesINT As Integer
    Public _9SpikesINT As Integer
    Public _2chargesDEX As Double
    Public _3chargesDEX As Double
    Public _4chargesDEX As Double
    Public _2chargesINT As Integer
    Public _3chargesINT As Integer
    Public _4chargesINT As Integer
    Public _2BouncesPER As Integer
    Public _3BouncesPER As Integer
    Public _4BouncesPER As Integer
    Public _2StrikePER As Integer
    Public _3StrikePER As Integer
    Public _4StrikePER As Integer
#End Region



End Class

Public Class spellDataInfo
    Public dataType As Integer
    Public attributeType As Integer
    Public modifierType As Integer
    Public level0 As Double
    Public level10 As Double
End Class

Public Class filteredCustomData
    Public Property name As ObjectModel.ObservableCollection(Of String)
    Public Property value As ObjectModel.ObservableCollection(Of String)

    Public Sub New()
        name = New ObjectModel.ObservableCollection(Of String)
        value = New ObjectModel.ObservableCollection(Of String)
    End Sub

End Class
