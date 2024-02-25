Imports Editor_Skill_Wiki.MirrorClasses
Imports Newtonsoft.Json

Public Class MonsterFinalData 'class which stores the monster data after being calculated from the raw data

    Dim commonFunctions As CommonFunctions
    Dim attackBaseText As String
    Dim skillBaseText As String

    Public Sub New(commonFunctions As CommonFunctions)
        Me.commonFunctions = commonFunctions
        If My.Settings.json Then
            attackBaseText = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "DefaultOutputs/jsonMonsterAttack.txt"))
            skillBaseText = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "DefaultOutputs/jsonMonsterSkill.txt"))
        Else
            attackBaseText = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "DefaultOutputs/defaultMonsterAttack.txt"))
            skillBaseText = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "DefaultOutputs/defaultMonsterSkill.txt"))
        End If
    End Sub

    Public name As String
    Public description As String

    Public str As String
    Public dex As String
    Public int As String
    Public cos As String
    Public per As String
    Public wis As String
    Public health As String
    Public healthRegen As String
    Public mana As String
    Public manaRegen As String
    Public accuracy As String
    Public fortitude As String
    Public evasion As String
    Public willpower As String

    Public stealth As String
    Public detection As String
    Public spellDamageIncrease As String
    Public cooldownReduction As String
    Public luck As String
    Public criticalChance As String
    Public criticalDamage As String
    Public slashArmor As String
    Public slashArmorPerc As String
    Public pierceArmor As String
    Public pierceArmorPerc As String
    Public crushArmor As String
    Public crushArmorPerc As String

    Public fireResistance As String
    Public fireResistancePerc As String
    Public coldResistance As String
    Public coldResistancePerc As String
    Public shockResistance As String
    Public shockResistancePerc As String
    Public magicResistance As String
    Public magicResistancePerc As String
    Public poisonResistance As String
    Public poisonResistancePerc As String
    Public acidResistance As String
    Public acidResistancePerc As String
    Public magicalDamageRefection As String
    Public physicalDamageReflection As String
    Public ChallengeRating As Integer
    Public Difficulty As String
    Public HalfKillsRequired As Boolean
    Public Capturable As Boolean
    Public MonsterRace As String
    Public SkinAmount As Integer
    Public Champion As Boolean
    Public WalkSpeed As Double
    Public BaseSpeed As Double
    Public BroadCastAggression As Boolean
    Public PlayerAttitude As String
    Public CombatStance As String

    Public StatusImmunities As String
    Public SpecialImmunities As String
    Public EffectGroupImmunities As String
    Public Absorb As String

    Public monsterAttacks As List(Of MonsterAttackFinalData)
    Public monsterSkills As List(Of MonsterSpellFinalData)

    Public Category As String


    Public Sub parseData(data As MirrorClasses.MonsterClass)


        ChallengeRating = data.challengeRating
        Difficulty = data.difficulty.ToString
        HalfKillsRequired = data.halfKillsRequired
        Capturable = data.capturable
        MonsterRace = data.monsterRace.ToString
        SkinAmount = data.skinAmount
        Champion = data.champion
        WalkSpeed = data.walkSpeed
        BaseSpeed = data.baseSpeed
        BroadCastAggression = data.BroadcastAggression
        PlayerAttitude = data.playerAttitude.ToString
        CombatStance = data.combatStance.ToString




        monsterAttacks = New List(Of MonsterAttackFinalData)
        monsterSkills = New List(Of MonsterSpellFinalData)

        GetMonsterNameAndDescription(data, name, description)
        str = data.str.value.ToString
        dex = data.dex.value.ToString
        int = data._int.value.ToString
        cos = data.cos.value.ToString
        per = data.per.value.ToString
        wis = data.wis.value.ToString

        health = (data.baseEndurance.value + data.cos.value * 50).ToString
        healthRegen = (data.baseEnduranceRegen.value + data.str.value * 0.5).ToString
        mana = (data.baseEnergy.value + data._int.value * 100).ToString
        manaRegen = (data.baseEnergyRegen.value + data._int.value * 3).ToString
        accuracy = (data.accuracy.value + data.per.value * 40 - 200).ToString
        fortitude = (data.fortitude.value + data.cos.value * 40 - 200).ToString
        evasion = (data.evasion.value + data.dex.value * 40 - 200).ToString
        willpower = (data.willpower.value + data._int.value * 40 - 200).ToString
        stealth = (data.stealth.value + data.dex.value * 20 - 100).ToString
        detection = (data.detection.value + data.per.value * 20 - 100).ToString
        luck = (data.baseLuck.value).ToString
        criticalChance = (data.criticalChance.value + data.per.value * 0.5 - 2.5).ToString & "%"



        criticalDamage = (data.criticalDamage.value + data.per.value * 3).ToString & "%"
        spellDamageIncrease = data.spellDamageIncrease.value.ToString
        cooldownReduction = (data.cooldownReduction.value + data.dex.value).ToString & "%"


        slashArmor = data.armorSlash.value.ToString
        slashArmorPerc = calculatePerc(data.armorSlash.value)
        pierceArmor = data.armorPierce.value.ToString
        pierceArmorPerc = calculatePerc(data.armorPierce.value)
        crushArmor = data.armorCrush.value.ToString
        crushArmorPerc = calculatePerc(data.armorCrush.value)
        fireResistance = data.fireResistance.value.ToString
        fireResistancePerc = calculatePerc(data.fireResistance.value)
        coldResistance = data.coldResistance.value.ToString
        coldResistancePerc = calculatePerc(data.coldResistance.value)
        shockResistance = data.shockResistance.value.ToString
        shockResistancePerc = calculatePerc(data.shockResistance.value)
        magicResistance = data.magicResistance.value.ToString
        magicResistancePerc = calculatePerc(data.magicResistance.value)
        poisonResistance = data.poisonResistance.value.ToString
        poisonResistancePerc = calculatePerc(data.poisonResistance.value)
        acidResistance = data.acidResistance.value.ToString
        acidResistancePerc = calculatePerc(data.acidResistance.value)

        magicalDamageRefection = data.magicalDamageReflection & "%"
        physicalDamageReflection = data.physicalDamageReflection & "%"


        For Each status As MirrorClasses.Sprite In data.immunityStatusEffectList
            Dim assetpath As String = commonFunctions.GetAssetName(status.m_PathID)
            Dim statusData As MirrorClasses.StatusEffectInfo = JsonConvert.DeserializeObject(Of MirrorClasses.StatusEffectInfo)(commonFunctions.GetRawData(assetpath))
            StatusImmunities &= commonFunctions.terms.getTerm("status_effect/" & statusData.statusKey & "_name", My.Settings.Language)
            If Not data.immunityStatusEffectList.IndexOf(status) = data.immunityStatusEffectList.Count - 1 Then
                StatusImmunities &= ", "
            End If
        Next

        EffectGroupImmunities = data.immunityEffectGroup.ToString
        SpecialImmunities = data.specialImmunities.ToString

        For Each damage As Enumerators.DamageType In data.damageAbsorbed
            Absorb &= commonFunctions.terms.getTerm("ui/item_tooltip_damageType_" & [Enum].GetName(GetType(Enumerators.DamageType), damage), My.Settings.Language)
            If Not data.damageAbsorbed.IndexOf(damage) = data.damageAbsorbed.Count - 1 Then
                Absorb &= ", "
            End If
        Next
        If Absorb = "" Then Absorb = "-"

        For Each attack As MirrorClasses.KnowledgeUnlockableMonsterAttack In data.MonsterAttacks
            Dim assetpath As String = commonFunctions.GetAssetName(attack.value.m_PathID)
            Dim monsterAttackData As MirrorClasses.MonsterAttack = JsonConvert.DeserializeObject(Of MirrorClasses.MonsterAttack)(commonFunctions.GetRawData(assetpath))
            monsterAttacks.Add(getAttackData(monsterAttackData))
        Next

        For Each spell As KnowledgeUnlockableMonsterSpell In data.MonsterSpellsList
            Dim assetPath As String = commonFunctions.GetAssetName(spell.value.m_PathID)
            Dim newskill As MonsterSpellFinalData = getSpellData(assetPath)
            newskill.acquired = spell.targetToUnlock & "%"
            compileSkillFullText(newskill)
            monsterSkills.Add(newskill)
        Next

        CompileCategories(data)

    End Sub

    Private Function calculatePerc(number As Integer) As String
        Dim percentValue As Double

        percentValue = Math.Round(100 * number / (number + 500), 0)

        Return percentValue.ToString & "%"
    End Function

    Private Function getAttackData(data As MirrorClasses.MonsterAttack) As MonsterAttackFinalData

        Dim newAttack As New MonsterAttackFinalData

        newAttack.name = commonFunctions.terms.getTerm(data.attackName.Replace("key:", ""), My.Settings.Language)
        newAttack.primaryDamage = commonFunctions.terms.getTerm("ui/item_tooltip_damageType_" & data.damage.dmgType1, My.Settings.Language)
        If data.damage.dmgType2 <> Enumerators.DamageType.None Then
            newAttack.secondaryDamage = commonFunctions.terms.getTerm("ui/item_tooltip_damageType_" & [Enum].GetName(GetType(Enumerators.DamageType), data.damage.dmgType2), My.Settings.Language)
        Else
            newAttack.secondaryDamage = "-"
        End If

        If data.damage.dmgType3 <> Enumerators.DamageType.None Then
            newAttack.tertiaryDamage = commonFunctions.terms.getTerm("ui/item_tooltip_damageType_" & [Enum].GetName(GetType(Enumerators.DamageType), data.damage.dmgType3), My.Settings.Language)
        Else
            newAttack.tertiaryDamage = "-"
        End If

        newAttack.range = data.damageLength.ToString
        newAttack.speed = commonFunctions.terms.getTerm("ui/item_tooltip_weaponSpeed_" & data.attackSpeed, My.Settings.Language)
        newAttack.damage = data.damage.baseDamage
        newAttack.projectileSpeed = data.projectileSpeed
        newAttack.projectileDespawn = data.projectileDespawn
        newAttack.damageAmplitude = data.damageAmplitude
        newAttack.damageWidth = data.damageWidth
        newAttack.damageLength = data.damageLength
        newAttack.weaponDamageType = [Enum].GetName(GetType(Enumerators.WeaponDamageType), data.WeaponDamageType)
        newAttack.attackProperties = data.weaponProperties

        compileAttackFullText(newAttack)

        Return newAttack
    End Function



    Private Function getSpellData(filepath As String)
        Dim newSpell As New MonsterSpellFinalData
        Dim parsedData As MirrorClasses.SpellInfo

        Dim textData As String = commonFunctions.GetRawData(filepath)
        parsedData = Newtonsoft.Json.JsonConvert.DeserializeObject(Of MirrorClasses.SpellInfo)(textData)
        newSpell.name = commonFunctions.terms.getTerm("spells/" & parsedData.SpellKey & "_name", My.Settings.Language)

        Return newSpell
    End Function



    Private Sub compileAttackFullText(ByRef attack As MonsterAttackFinalData)
        Dim baseText As String = attackBaseText
        baseText = baseText.Replace("textName", attack.name)
        baseText = baseText.Replace("textDamageValue", attack.damage)
        baseText = baseText.Replace("textWeaponType", attack.weaponDamageType)
        baseText = baseText.Replace("textType1", attack.primaryDamage)
        baseText = baseText.Replace("textType2", attack.secondaryDamage)
        baseText = baseText.Replace("textType3", attack.tertiaryDamage)
        baseText = baseText.Replace("textRange", attack.range)
        baseText = baseText.Replace("textSpeed", attack.speed)
        baseText = baseText.Replace("textDamageWidth", attack.damageWidth)
        baseText = baseText.Replace("textDamageLength", attack.damageLength)
        baseText = baseText.Replace("textDamageAmplitude", attack.damageAmplitude)
        baseText = baseText.Replace("textProjectileSpeed", attack.projectileSpeed)
        baseText = baseText.Replace("textProjectileDespawn", attack.projectileDespawn)


        Dim properties As String = ""
        For Each data As MirrorClasses.WeaponPropertyData In attack.attackProperties
            data.DeserializeProperty(commonFunctions)
            Dim parameterText As String = data.GetPropertyTooltip(commonFunctions)

            parameterText &= "</br>"
            properties &= parameterText
        Next

        If properties <> "" Then
            properties = properties.Substring(0, properties.Length - 5)
        End If

        baseText = baseText.Replace("propertySpace", properties)

        attack.fullText = baseText

    End Sub

    Private Sub compileSkillFullText(ByRef spell As MonsterSpellFinalData)
        Dim baseText As String = skillBaseText
        baseText = baseText.Replace("nameText", spell.name)
        baseText = baseText.Replace("acquiredText", spell.acquired)

        spell.fullText = baseText
    End Sub

    Private Sub GetMonsterNameAndDescription(Data As MirrorClasses.MonsterClass, ByRef name As String, ByRef description As String)
        Dim assetpath As String = commonFunctions.GetAssetName(Data.knowledgeEntry.m_PathID)
        Dim monsterKnowledgeData As MirrorClasses.BookOfKnowledgeEntry_Bestiary = JsonConvert.DeserializeObject(Of MirrorClasses.BookOfKnowledgeEntry_Bestiary)(commonFunctions.GetRawData(assetpath))
        name = commonFunctions.terms.getTerm(monsterKnowledgeData.entryName.Replace("key:", ""), My.Settings.Language)
        description = commonFunctions.terms.getTerm(monsterKnowledgeData.flavourText.Replace("key:", ""), My.Settings.Language)
    End Sub

    Private Sub CompileCategories(data As MirrorClasses.MonsterClass)
        If data.champion Then
            Category &= "Legendary Monsters, "
        End If

        If data.isSpirit Then
            Category &= "Spirit Monsters, "
        End If

        If data.tutorialMonster Then
            Category &= "Tutorial Monsters, "
        End If

        If data.BroadcastAggression Then
            Category &= "Chain Pull Monsters, "
        End If

        Category &= "CR " & data.challengeRating & " Monsters"

    End Sub


End Class



Public Class MonsterAttackFinalData

    Public name As String
    Public damage As String
    Public damageModifier As Double
    Public weaponDamageType As String
    Public range As String
    Public speed As String
    Public primaryDamage As String
    Public secondaryDamage As String
    Public tertiaryDamage As String
    Public primaryStat As String
    Public secondaryStat As String
    Public damageWidth As String
    Public damageLength As String
    Public damageAmplitude As String
    Public projectileSpeed As String
    Public projectileDespawn As String
    Public fullText As String

    Public attackProperties As List(Of WeaponPropertyData) = New List(Of WeaponPropertyData)

End Class

Public Class MonsterSpellFinalData

    Public name As String
    Public kp As String
    Public acquired As String
    Public fullText As String

End Class