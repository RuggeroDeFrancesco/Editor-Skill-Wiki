Public Class MonsterFinalData 'class which stores the monster data after being calculated from the raw data

    Public name As String
    Public description As String
    Public challengeRating As String

    Public str As String
    Public dex As String
    Public int As String
    Public cos As String
    Public per As String
    Public cha As String
    Public health As String
    Public mana As String
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

    Public immunityEffectGroup As String

    Public monsterAttacks As List(Of MonsterAttackFinalData)
    Public monsterSkills As List(Of MonsterSpellFinalData)
    Public immunityStatusEffectList As List(Of String)


    Public Sub parseData(data As MonsterData, attackFolder As String, spellFolder As String)

        Dim attackFolderPath As String
        Dim spellFolderPath As String
        monsterAttacks = New List(Of MonsterAttackFinalData)
        monsterSkills = New List(Of MonsterSpellFinalData)
        immunityStatusEffectList = New List(Of String)

        If System.IO.Directory.Exists(attackFolder) Then
            attackFolderPath = attackFolder
        Else Throw New Exception("Attack folder does not exist.")
        End If

        If System.IO.Directory.Exists(spellFolder) Then
            spellFolderPath = spellFolder
        Else Throw New Exception("Spell folder does not exist.")
        End If

        name = data.m_name
        challengeRating = data.challengeRating.ToString
        str = data.str.value.ToString
        dex = data.dex.value.ToString
        int = data._int.value.ToString
        cos = data.cos.value.ToString
        per = data.per.value.ToString
        cha = data.cha.value.ToString

        health = (data.baseEndurance.value + data.str.value * 50 + data.cos.value * 75).ToString
        mana = (data.baseEnergy.value + data._int.value * 75 + data.cha.value * 50).ToString
        accuracy = (data.accuracy.value + data.per.value * 25 - 150).ToString
        fortitude = (data.fortitude.value + data.cos.value * 25 - 150).ToString
        evasion = (data.evasion.value + data.dex.value * 25 - 150).ToString
        willpower = (data.willpower.value + data._int.value * 25 - 150).ToString
        stealth = (data.Stealth.value + data.dex.value * 25 - 150).ToString
        detection = (data.Detection.value + data.per.value * 25 - 150).ToString
        luck = (data.baseLuck.value + data.cha.value * 25 - 250).ToString
        criticalChance = (data.criticalChance.value + data.per.value * 1 - 10).ToString & "%"



        criticalDamage = data.criticalDamage.value.ToString
        spellDamageIncrease = data.spellDamageIncrease.value.ToString
        cooldownReduction = data.cooldownReduction.value.ToString


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
        immunityEffectGroup = data.immunityEffectGroup

        Dim statusRawData As String = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "MonsterCreator/statusEffects.xml"))

        For Each StatusEffect As StatusEffect In data.immunityStatusEffectList
            If StatusEffect.m_PathID <> 0 Then
                Dim newImmunity As String = getStatusEffect(StatusEffect.m_PathID, statusRawData)
                If newImmunity <> "" Then
                    immunityStatusEffectList.Add(newImmunity)
                End If

            End If
        Next


        Dim attackRawData As String = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "MonsterCreator/attacks.xml"))

        For Each attack As MonsterAttack In data.MonsterAttacks
            monsterAttacks.Add(getAttackData(attack.value.m_pathID, attackRawData, attackFolderPath))
        Next

        Dim spellRawData As String = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "MonsterCreator/SpellIndex.xml"))

        For Each spell As MonsterSpells In data.MonsterSpellsList
            Dim newskill As MonsterSpellFinalData = getSpellData(spell.value.m_pathID, spellRawData, spellFolderPath)
            newskill.acquired = spell.targetToUnlock & "%"
            compileSkillFullText(newskill)
            monsterSkills.Add(newskill)
        Next

    End Sub

    Private Function calculatePerc(number As Integer) As String
        Dim percentValue As Double

        percentValue = Math.Round(100 * number / (number + 500), 0)



        Return percentValue.ToString & "%"
    End Function

    Private Function getAttackData(attackRef As String, rawdata As String, attackFolder As String) As MonsterAttackFinalData
        Dim newAttack As New MonsterAttackFinalData
        Dim parsedData As MonsterAttackData
        Dim index As Integer = rawdata.IndexOf(attackRef)
        If index <> -1 Then
            Dim startName As Integer = rawdata.LastIndexOf("<Name>", index)
            Dim endName As Integer = rawdata.IndexOf("</Name>", startName)
            Dim name As String = rawdata.Substring(startName + 6, endName - startName - 6)
            Dim filePath As String = attackFolder & "\" & name & ".json"
            If System.IO.File.Exists(filePath) Then
                Dim textData As String = System.IO.File.ReadAllText(filePath)
                parsedData = Newtonsoft.Json.JsonConvert.DeserializeObject(Of MonsterAttackData)(textData)
                newAttack.name = parsedData.attackName
                newAttack.primaryDamage = [Enum].GetName(GetType(DamageType), parsedData.damage.dmgType)
                newAttack.secondaryDamage = [Enum].GetName(GetType(DamageType), parsedData.damage.secondDmgType)
                newAttack.tertiaryDamage = [Enum].GetName(GetType(DamageType), parsedData.damage.thirdDamageType)
                newAttack.damageModifier = parsedData.damage.damageModifier.ToString
                newAttack.primaryStat = [Enum].GetName(GetType(CharacterAttribute), parsedData.damage.damageAttribute)
                newAttack.secondaryStat = [Enum].GetName(GetType(CharacterAttribute), parsedData.damage.secondDamageAttribute)
                newAttack.range = parsedData.damageLength.ToString
                newAttack.speed = parsedData.speed.ToString
                calculateAttackDamage(newAttack)
                compileAttackFullText(newAttack)
            Else Throw New Exception(filePath & " Not Found")
            End If
        End If
        Return newAttack
    End Function

    Private Function getStatusEffect(statusRef As String, rawdata As String) As String
        Dim statusEffect As String = ""
        Dim index As Integer = rawdata.IndexOf(statusRef)
        Dim startName As Integer = rawdata.LastIndexOf("<Name>", index)
        Dim endName As Integer = rawdata.IndexOf("</Name>", startName)
        statusEffect = rawdata.Substring(startName + 6, endName - startName - 6)
        Return statusEffect
    End Function

    Private Function getSpellData(spellRef As String, rawData As String, spellFolder As String)
        Dim newSpell As New MonsterSpellFinalData
        Dim parsedData As SpellClassMirror
        Dim index As Integer = rawData.IndexOf(spellRef)
        If index <> -1 Then
            Dim startName As Integer = rawData.LastIndexOf("<Name>", index)
            Dim endName As Integer = rawData.IndexOf("</Name>", startName)
            Dim name As String = rawData.Substring(startName + 6, endName - startName - 6)

            'eccezioni di skill specifiche dei mostri
            Select Case name
                Case "spell_ArborealDragonBreath_ArborealDragon"
                    name = "spell_ArborealDragonBreath"
                Case "spell_SkeletalDragonBreath_SkeletalDragon"
                    name = "spell_SkeletalDragonBreath"
                Case "spell_MountainDragonBreath_MountainDragon"
                    name = "spell_MountainDragonBreath"
                Case "spell_EmberDragonBreath_EmberDragon"
                    name = "spell_EmberDragonBreath"

                Case "spell_MagicReflection_ColossalHuskWorm"
                    name = "spell_MagicReflection"
                Case "spell_BattleJump_Ghoul"
                    name = "spell_BattleJump"

                Case "spell_MagicReflection_ColossalHuskWorm"
                    name = "spell_MagicReflection"
                Case "spell_LegendWOPSilence"
                    name = "spell_WordOfPower_Silence"
                Case "spell_LegendShivers"
                    name = "spell_Shivers"
                Case "spell_LegendMegaEruption"
                    name = "spell_Eruption"
                Case "spell_LegendLargeSlow"
                    name = "spell_Slow"
                Case "spell_LegendIceSpikesRing"
                    name = "spell_IceSpikes"
                Case "spell_LegendGrooveRam"
                    name = "spell_RamThrough"
                Case "spell_LegendGrooveQuake"
                    name = "spell_Earthquake"
                Case "spell_LegendAxfitmithissTouch"
                    name = "spell_ChillingTouch"




            End Select

            Dim filePath As String = spellFolder & "\" & name & ".json"
            If System.IO.File.Exists(filePath) Then
                Dim textData As String = System.IO.File.ReadAllText(filePath)
                parsedData = Newtonsoft.Json.JsonConvert.DeserializeObject(Of SpellClassMirror)(textData)
                newSpell.name = parsedData.m_name
                newSpell.kp = ((parsedData.difficulty + 1) * 1000).ToString

            Else Throw New Exception(filePath & " Not Found")
            End If
        End If
        Return newSpell
    End Function

    Private Sub calculateAttackDamage(ByRef attack As MonsterAttackFinalData)

        Dim statValue As Integer
        Select Case attack.primaryStat
            Case "Strength"
                statValue = str
            Case "Dexterity"
                statValue = dex
            Case "Intelligence"
                statValue = int
            Case "Constitution"
                statValue = cos
            Case "Perception"
                statValue = per
            Case "Charisma"
                statValue = cha
            Case Else
                statValue = 0

        End Select
        Dim baseAttackDamage As Integer = (attack.damageModifier * statValue)
        Dim minimumAttackDamage As Integer = baseAttackDamage * 0.75
        Dim maximumAttackDamage As Integer = baseAttackDamage * 1.25

        attack.damage = minimumAttackDamage.ToString & " - " & maximumAttackDamage.ToString

    End Sub

    Private Sub compileAttackFullText(ByRef attack As MonsterAttackFinalData)
        Dim baseText As String = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "MonsterCreator/defaultMonsterAttack.txt"))
        'the name will be compiled later by the MonsterDataCreator since it needs the language data
        baseText = baseText.Replace("textDamage", attack.damage)
        baseText = baseText.Replace("textRange", attack.range)
        baseText = baseText.Replace("textSpeed", attack.speed)

        attack.fullText = baseText

    End Sub

    Private Sub compileSkillFullText(ByRef spell As MonsterSpellFinalData)
        Dim baseText As String = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "MonsterCreator/defaultMonsterSkill.txt"))
        'the name will be compiled later by the MonsterDataCreator since it needs the language data
        baseText = baseText.Replace("kpText", spell.kp)
        baseText = baseText.Replace("acquiredText", spell.acquired)

        spell.fullText = baseText
    End Sub

End Class



Public Class MonsterAttackFinalData

    Public name As String
    Public damage As String
    Public damageModifier As Double
    Public range As String
    Public speed As String
    Public primaryDamage As String
    Public secondaryDamage As String
    Public tertiaryDamage As String
    Public primaryStat As String
    Public secondaryStat As String
    Public fullText As String

End Class


Public Class MonsterSpellFinalData

    Public name As String
    Public kp As String
    Public acquired As String
    Public fullText As String

End Class