Imports System.Net.Security

Public Class SpellDataCreator

    'class that parses a json file of a spell and extracts all the fields in a usable format.

    Private CommonFunctions As CommonFunctions

    Public Sub New(commonFunctions As CommonFunctions)
        Me.CommonFunctions = commonFunctions
    End Sub

    Public Property Output As String 'full list of the properties, to be used for the datamodule on the wiki

    Public Property Description As String

    Public Sub createOutput(data As MirrorClasses.SpellInfo) 'converts the enumerators into clear text and adds the categories

        GetDescription(data.SpellKey)
        If My.Settings.json Then
            Output = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "DefaultOutputs/jsonSpellData.txt"))
        Else
            Output = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "DefaultOutputs/defaultSpellData.txt"))
        End If

        Output = Output.Replace("SpellName", GetTooltipName(data.SpellKey))


        '['Ultimate'] = ultimateValue,
        '['school'] = "schoolValue",
        '['Spell Type'] = "spellTypeValue",
        '['Ability Type'] = "abilityTypeValue",
        '['Pre Cast Time'] = "preCastTimeValue",
        '['Can Crit'] = "canCritValue",
        '['Friend Fire'] = "friendlyFireValue", 
        '['Restricted Armor Weight'] = "armorWeightValue",
        '['Special Weapon Restriction'] = "SpecialWeaponRestictionValue",
        '['Effect Group'] = "effectGroupValue",
        '['description'] = "descriptionValue",
        '['Skill Parameters'] = "skillParametersValue",
        '['Spell Tags'] = "spellTagsValue",
        '['cooldown'] = cooldownValue,
        '['mana_cost'] = manaCostValue,
        '['mana_cost_per_s'] = manaCostPersValue,
        '['max_mana_cost'] = maxManaCostValue,
        '['Player Skill'] = playerSkillValue,
        '['image'] = "imageValue",
        '['Categories'] = "categoriesValue",


        Output = Output.Replace("ultimateValue", IsUltimate(data.difficulty).ToString)
        Output = Output.Replace("schoolValue", [Enum].GetName(GetType(Enumerators.SpellSchool), data.spellSchool))
        Output = Output.Replace("spellTypeValue", GetSpellType(data))
        Output = Output.Replace("abilityTypeValue", GetAbilityTypeSpecial(data))
        Output = Output.Replace("preCastTimeValue", data.preCastTime.ToString)
        Output = Output.Replace("canCritValue", data.canBeCritical.ToString)
        Output = Output.Replace("friendlyFireValue", [Enum].GetName(GetType(Enumerators.FriendlyFire), data.friendlyFire))
        Output = Output.Replace("armorWeightValue", GetArmorRestrictions(data.spellcastingRestrictions.restrictedArmorWeight))
        Output = Output.Replace("SpecialWeaponRestictionValue", GetWeaponRestrictions(data.spellcastingRestrictions.weaponRestriction, data.spellcastingRestrictions.specialWeaponRestriction))
        Output = Output.Replace("effectGroupValue", GetEffectGroup(data.effectGroup, data.irremovable))
        Output = Output.Replace("skillParametersValue", GetSkillParameters(data))
        Output = Output.Replace("spellTagsValue", GetSpellTags(data))
        Output = Output.Replace("cooldownValue", GetCooldown(data))
        If HasSpellData(data, Enumerators.SpellDataType.ManaCost) Then
            Output = Output.Replace("manaCostValue", GetSpellDataValue(data, Enumerators.SpellDataType.ManaCost))
            Output = Output.Replace("manaCostPersValue", "-")
            Output = Output.Replace("maxManaCostValue", "-")
        ElseIf HasSpellData(data, Enumerators.SpellDataType.MaxManaCost) Then
            Output = Output.Replace("maxManaCostValue", GetSpellDataValue(data, Enumerators.SpellDataType.MaxManaCost))
            Output = Output.Replace("manaCostPersValue", "-")
            Output = Output.Replace("manaCostValue", "-")
        ElseIf HasSpellData(data, Enumerators.SpellDataType.ManaCostPerSecond) Then
            Output = Output.Replace("manaCostPersValue", GetSpellDataValue(data, Enumerators.SpellDataType.ManaCostPerSecond))
            Output = Output.Replace("maxManaCostValue", "-")
            Output = Output.Replace("manaCostValue", "-")
        End If
        Output = Output.Replace("playerSkillValue", data.PlayerSpell.ToString)
        Dim icon As String = CommonFunctions.GetIconName(data.Icon.m_PathID)
        Output = Output.Replace("imageValue", icon)



        '['Unicity Group'] = "unicityGroupValue",
        '['Effect Group'] = "effectGroupValue",
        '['Irremovable'] = "irremovableValue",
        '['Pathfinding'] = "spellPathfindingValue",
        '['Ready Trigger'] = "readyTriggerValue",
        '['Secure Hit'] = "secureHitValue",
        '['Remove Stealth'] = "removeStealthValue",
        '['Block Check'] = "blockCheckValue",
        '['ActivationType'] = "activationTypeValue",
        '['Target Type'] = "targetTypeValue",
        '['Targeting Range'] = "targetingRangeValue",
        '['Casting Range'] = "castingRangeValue",
        '['Check Los'] = "checkLosValue",
        '['Status On Target'] = "statusOnTargetValue",
        '['Block In Siege'] = "blockInSiegeValue",

        Output = Output.Replace("unicityGroupValue", [Enum].GetName(GetType(Enumerators.SpellUnicityGroup), data.unicityGroup))
        Output = Output.Replace("effectGroupValue", [Enum].GetName(GetType(Enumerators.EffectGroup), data.effectGroup))
        Output = Output.Replace("irremovableValue", data.irremovable.ToString)
        Output = Output.Replace("spellPathfindingValue", [Enum].GetName(GetType(Enumerators.SpellPathfinding), data.pathfinding))
        Output = Output.Replace("readyTriggerValue", [Enum].GetName(GetType(Enumerators.ReadyTrigger), data.readyTrigger))
        Output = Output.Replace("secureHitValue", data.SecureHit.ToString)
        Output = Output.Replace("removeStealthValue", data.dontRemoveStealth.ToString)
        Output = Output.Replace("blockCheckValue", data.avoidBlockControllerCheck.ToString)
        Output = Output.Replace("activationTypeValue", [Enum].GetName(GetType(Enumerators.SpellActivationType), data.activationType))
        Output = Output.Replace("targetTypeValue", [Enum].GetName(GetType(Enumerators.SpellTargetType), data.targetType))
        Output = Output.Replace("targetingRangeValue", data.targetingRange)
        Output = Output.Replace("castingRangeValue", data.castingRange)
        Output = Output.Replace("checkLosValue", data.checkLos.ToString)
        Output = Output.Replace("blockInSiegeValue", data.blockInSiege.ToString)

        Dim categories As String = ""
        Select Case data.difficulty
            Case 0
                categories &= """Non-Ultimate Skills"","
            Case 1
                categories &= """Non-Ultimate Skills"","
            Case 2
                categories &= """Ultimate Skills"","
        End Select

        Select Case HasTag(data, Enumerators.SpellTagType.MagicalAbility)
            Case True
                categories &= """Magical Skills"","
            Case False
                categories &= """Combat Skills"","
        End Select

        If data.tooltipActivationType = Enumerators.SpellTooltipActivationType.Sustained Then
            categories &= """Sustained Skills"","
        End If

        If data.blockInSiege Then
            categories &= """Skills forbidden in Sieges"","
        End If

        If HasTag(data, Enumerators.SpellTagType.Channeled) Then
            categories &= """Channeled Skills"","
        End If

        If HasTag(data, Enumerators.SpellTagType.LinkedEffects) Then
            categories &= """Linked Skills"","
        End If

        If HasTag(data, Enumerators.SpellTagType.Healing) Then
            categories &= """Healing Skills"","
        End If

        Select Case data.canBeCritical
            Case Enumerators.SpellCrit.Yes
                categories &= """Skills that Can Critically Hit"","
        End Select

        Select Case data.spellcastingRestrictions.restrictedArmorWeight
            Case 0
                categories &= """Skills Usable in Light Armor"","
                categories &= """Skills Usable in Medium Armor"","
                categories &= """Skills Usable in Heavy Armor"","
            Case 1
                categories &= """Skills Usable in Light Armor"","
            Case 2
                categories &= """Skills Usable in Medium Armor"","
            Case 3
                categories &= """Skills Usable in Light Armor"","
                categories &= """Skills Usable in Medium Armor"","
            Case 4
                categories &= """Skills Usable in Heavy Armor"","
            Case 5
                categories &= """Skills Usable in Light Armor"","
                categories &= """Skills Usable in Heavy Armor"","
            Case 6
                categories &= """Skills Usable in Medium Armor"","
                categories &= """Skills Usable in Heavy Armor"","

        End Select

        Select Case data.spellcastingRestrictions.specialWeaponRestriction
            Case Enumerators.SpellSpecialWeaponRestriction.ShieldRequired
                categories &= """Skills Requiring a Shield"","
            Case False
        End Select

        Select Case data.spellcastingRestrictions.specialWeaponRestriction
            Case Enumerators.SpellSpecialWeaponRestriction.BowRequired
                categories &= """Skills that Require Ranged Weapons"","
            Case Enumerators.SpellSpecialWeaponRestriction.Unarmed
                categories &= """Skills that Require Being Unarmed"","
        End Select

        If categories.Length <> 0 Then
            categories = categories.Substring(0, categories.Length - 1)
        End If

        Output = Output.Replace("categoriesValue", categories)
        Output = Output.Replace("Deffault", "Default")
    End Sub


    Private Sub GetDescription(spellKey As String) 'extracts the description of the spell from the I2Language file
        Description = CommonFunctions.terms.getTerm("spells/" & spellKey & "_description", My.Settings.Language)
    End Sub

    Public Function GetTooltipName(spellkey As String)

        Return CommonFunctions.terms.getTerm("spells/" & spellkey & "_name", My.Settings.Language)

    End Function

    Private Function IsUltimate(difficulty As Integer) As Boolean
        If difficulty = 2 Then
            Return True
        Else Return False
        End If
    End Function

    Private Function GetCooldown(spell As MirrorClasses.SpellInfo) As String
        Dim cooldown As String = 0
        Dim cooldownFound As Boolean = False
        For Each data As MirrorClasses.SpellDataInfo In spell.spellData
            If data.dataType = Enumerators.SpellDataType.Cooldown Then
                cooldownFound = True
                If data.level0 = data.level10 Then
                    cooldown = data.level0
                Else
                    cooldown = data.level0.ToString & " to " & data.level10
                End If
            End If
        Next
        If cooldownFound = False Then
            CommonFunctions.AddLogEntry("Cannot find cooldown information for skill " & spell.SpellKey)
        End If
        Return cooldown
    End Function

    Private Function GetAbilityTypeSpecial(spell As MirrorClasses.SpellInfo) As String ' from class UITooltip_Spell
        Dim abilityTypeSpecial As String = ""
        Select Case spell.tooltipActivationType
            Case Enumerators.SpellTooltipActivationType.Targeted
                If spell.tooltipTargetType <> 0 Then
                    Dim abilityType As String = CommonFunctions.terms.getTerm("spells/spellActivationType_" & spell.tooltipTargetType.ToString, My.Settings.Language)
                    abilityTypeSpecial = "Target (" & abilityType & ")"
                Else
                    abilityTypeSpecial = "Target"
                End If
            Case Enumerators.SpellTooltipActivationType.Sustained
                If spell.tooltipTargetType <> 0 Then
                    Dim sustainedType As String = CommonFunctions.terms.getTerm("spells/spellActivationType_" & spell.tooltipTargetType.ToString, My.Settings.Language)
                    abilityTypeSpecial = "Sustained (" & sustainedType & ")"
                Else
                    abilityTypeSpecial = "Sustained"
                End If
            Case Enumerators.SpellTooltipActivationType.Ready
                If spell.tooltipTargetType <> 0 Then
                    Dim sustainedType As String = CommonFunctions.terms.getTerm("spells/spellActivationType_" & spell.tooltipTargetType.ToString, My.Settings.Language)
                    abilityTypeSpecial = "Ready (" & sustainedType & ")"
                Else
                    abilityTypeSpecial = "Ready"
                End If
            Case Else
                abilityTypeSpecial = [Enum].GetName(GetType(Enumerators.SpellTooltipActivationType), spell.tooltipActivationType)
        End Select
        Return abilityTypeSpecial
    End Function

    Private Function GetSpellType(spell As MirrorClasses.SpellInfo) As String ' from class UITooltip_Spell
        Dim spellTypeText As String = ""

        If HasTag(spell, Enumerators.SpellTagType.MagicalAbility) Then
            spellTypeText = CommonFunctions.terms.getTerm("spells/spelltag_" & Enumerators.SpellTagType.MagicalAbility.ToString, My.Settings.Language)
        Else
            spellTypeText = CommonFunctions.terms.getTerm("spells/spelltag_" & Enumerators.SpellTagType.PhysicalAbility.ToString, My.Settings.Language)
        End If
        Return spellTypeText
    End Function

    Private Function HasTag(spell As MirrorClasses.SpellInfo, tag As Enumerators.SpellTagType) As Boolean
        If spell.spellTags.Find(Function(p) p.dataType = tag) IsNot Nothing Then
            Return True
        Else Return False

        End If

    End Function

    Private Function HasSpellData(spell As MirrorClasses.SpellInfo, data As Enumerators.SpellDataType) As Boolean
        If spell.spellData.Find(Function(p) p.dataType = data) IsNot Nothing Then
            Return True
        Else Return False

        End If

    End Function

    Private Function GetSpellDataValue(spell As MirrorClasses.SpellInfo, data As Enumerators.SpellDataType) As Double
        Dim foundData As MirrorClasses.SpellDataInfo = spell.spellData.Find(Function(p) p.dataType = data)
        If foundData IsNot Nothing Then
            Return foundData.level0
        Else Return 0
        End If
    End Function

    Private Function GetArmorRestrictions(restrictions As Enumerators.EquipWeightClass) As String
        Select Case restrictions
            Case Enumerators.EquipWeightClass.Light
                Return CommonFunctions.terms.getTerm("spells/spelldata_Light", My.Settings.Language)
            Case Enumerators.EquipWeightClass.Medium
                Return CommonFunctions.terms.getTerm("spells/spelldata_Light", My.Settings.Language) & " / " & CommonFunctions.terms.getTerm("spells/spelldata_Medium", My.Settings.Language)
            Case Else
                Return "-"
        End Select
    End Function

    Private Function GetWeaponRestrictions(restrictions As Enumerators.SpellWeaponRestriction, specialRestrictions As Enumerators.SpellSpecialWeaponRestriction) As String
        If restrictions = 0 And specialRestrictions = 0 Then
            Return "-"
        Else
            Select Case restrictions
                Case Enumerators.SpellWeaponRestriction.OneHanded
                    Return CommonFunctions.terms.getTerm("spells/spelldata_OneHanded", My.Settings.Language)
                Case Enumerators.SpellWeaponRestriction.TwoHanded
                    Return CommonFunctions.terms.getTerm("spells/spelldata_TwoHanded", My.Settings.Language)
                Case Enumerators.SpellWeaponRestriction.SpellChanneling
                    Return CommonFunctions.terms.getTerm("spells/spelldata_SpellChanneling", My.Settings.Language)
                Case Else
                    Select Case specialRestrictions
                        Case Enumerators.SpellSpecialWeaponRestriction.BowRequired
                            Return CommonFunctions.terms.getTerm("spells/spelldata_BowRequired", My.Settings.Language)
                        Case Enumerators.SpellSpecialWeaponRestriction.ShieldRequired
                            Return CommonFunctions.terms.getTerm("spells/spelldata_ShieldRequired", My.Settings.Language)
                        Case Enumerators.SpellSpecialWeaponRestriction.Unarmed
                            Return CommonFunctions.terms.getTerm("spells/spelldata_Unarmed", My.Settings.Language)
                    End Select
            End Select
        End If
        Return "-"
    End Function

    Private Function GetEffectGroup(effectGroup As Enumerators.EffectGroup, irremovable As Boolean) As String
        If effectGroup <> Enumerators.EffectGroup.None Then
            If irremovable Then
                Return CommonFunctions.terms.getTerm("spells/statusType_" & effectGroup.ToString, My.Settings.Language) & " (" & CommonFunctions.terms.getTerm("spells/spelldata_Irremovable" & effectGroup.ToString, My.Settings.Language) & ")"
            Else
                Return CommonFunctions.terms.getTerm("spells/statusType_" & effectGroup.ToString, My.Settings.Language)
            End If
        Else
            Return "-"
        End If

    End Function

    Private Function GetSpellSchool(spellSchool As Enumerators.SpellSchool) As String
        Return CommonFunctions.terms.getTerm("ui/spellSchool_" & spellSchool.ToString, My.Settings.Language)
    End Function

    Private Function GetSkillParameters(spell As MirrorClasses.SpellInfo) As String

        Dim parameters As String = ""
        For Each data As MirrorClasses.SpellDataInfo In spell.spellData

            If data.dataType = Enumerators.SpellDataType.ManaCost Or data.dataType = Enumerators.SpellDataType.Cooldown Or data.dataType = Enumerators.SpellDataType.ManaCostPerSecond Or data.dataType = Enumerators.SpellDataType.MaxManaCost Then
                Continue For
            End If

            Dim parameterText As String = CommonFunctions.terms.getTerm("spells/spelldata_" & data.dataType.ToString, My.Settings.Language) & ": "

            If data.level0 <> data.level10 Then
                parameterText &= data.level0 & " to " & data.level10
            Else
                parameterText &= data.level0
            End If

            If data.dataType.ToString.Contains("Percentage") Then
                parameterText &= "%"
            End If

            If data.attributeType <> Enumerators.SpellCoefficientAttributeType.None Then
                parameterText &= "( + " & data.attributeType.ToString & ")"
            End If

            If data.modifierType <> Enumerators.SaveModifierType.None Then
                parameterText &= " (" & CommonFunctions.terms.getTerm("spells/spellSaveModifier_" & data.modifierType.ToString, My.Settings.Language) & ")"
            End If

            parameterText &= "</br>"
            parameters &= parameterText
        Next

        If parameters <> "" Then
            parameters = parameters.Substring(0, parameters.Length - 5)
        End If


        Return parameters
    End Function


    Private Function GetSpellTags(spell As MirrorClasses.SpellInfo) As String
        Dim tagText As String = ""
        For Each tag As MirrorClasses.SpellTagDataInfo In spell.spellTags
            If tag.tooltipHidden = False And tag.dataType <> Enumerators.SpellTagType.PhysicalAbility And tag.dataType <> Enumerators.SpellTagType.MagicalAbility Then
                Dim tagLocalization As String = CommonFunctions.terms.getTerm("spells/spelltag_" & tag.dataType.ToString, My.Settings.Language)
                tagText &= tagLocalization & ", "
            End If
        Next
        If tagText.Length <> 0 Then
            tagText = tagText.Substring(0, tagText.Length - 2)
        End If
        Return tagText
    End Function

End Class




