Imports System.ComponentModel
Imports Newtonsoft.Json
Public Class ItemDataCreator


#Region "Property Changed"
    Implements INotifyPropertyChanged

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) _
        Implements INotifyPropertyChanged.PropertyChanged

    Private Sub OnInternalPropertyChanged(ByVal info As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(info))
    End Sub
#End Region

    Dim language As Integer

    Dim deserializedlanguageData As LanguageData

    Private Property _Output As String

    Public Property Output As String
        Get
            Return Me._Output
        End Get
        Set(value As String)
            _Output = value
            Me.OnInternalPropertyChanged("Output")
        End Set
    End Property

    Public ItemName As String

    Private MaterialProperties As List(Of MaterialClassMirror)

    'Dim itemListIndex As String

    Dim itemsFolder As String
    Dim materialsFolder As String
    Dim weaponPropertiesFolder As String
    Dim armorSetsFolder As String
    Dim enchantmentsFolder As String
    Dim assetList As AssetItemsList

    Public Sub New(language As Integer, itemsFolder As String, materialsFolder As String, Optional weaponPropertiesFolder As String = "", Optional ArmorSetsFolder As String = "", Optional enchantmentsFolder As String = "")
        Me.language = language
        Dim rawlanguagedata As String = GetLanguageData()
        deserializedlanguageData = deserializeLanguageData(rawlanguagedata)
        MaterialProperties = New List(Of MaterialClassMirror)
        assetList = MainWindow.GetAssets
        'itemListIndex = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "AssetIndices/unifiedIndex.xml"))
        Me.itemsFolder = itemsFolder
        Me.materialsFolder = materialsFolder
        Me.weaponPropertiesFolder = weaponPropertiesFolder
        Me.armorSetsFolder = ArmorSetsFolder
        Me.enchantmentsFolder = enchantmentsFolder
    End Sub

    Private Function getRawData(path As String) As String 'reads the file and extracts the content as text
        If System.IO.File.Exists(path) Then
            Dim rawData As String
            rawData = System.IO.File.ReadAllText(path)
            Return rawData
        Else Throw New System.Exception("File does not exist.")
            Return ""
        End If
    End Function

    Private Function cutData(data As String) As String 'removes the useless parts of the file
        Dim processedData As String
        Dim startingIndex As Integer
        startingIndex = data.IndexOf("m_Name") - 1
        processedData = "{"
        processedData &= data.Substring(startingIndex)
        Return processedData
    End Function

    Shared Function deserializeItemData(data As String) As ItemClassMirror 'converts the json to fields of the ItemClassMirror, used also by the loot creator

        Dim parsedData As ItemClassMirror
        parsedData = JsonConvert.DeserializeObject(Of ItemClassMirror)(data)
        Return parsedData
    End Function

    Private Function deserializeRecipeData(data As String) As CraftingRecipeMirror 'converts the json to fields of the RecipeClassMirror

        Dim parsedData As CraftingRecipeMirror
        parsedData = JsonConvert.DeserializeObject(Of CraftingRecipeMirror)(data)
        Return parsedData
    End Function

    Private Function GetLanguageData() As String
        Dim rawLanguageData As String
        rawLanguageData = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "I2languages.json"))
        Return rawLanguageData
    End Function

    Private Function deserializeLanguageData(data As String) As LanguageData

        Dim parsedData As LanguageData
        Dim correctedData As String = data.Replace("\n", " ")
        parsedData = JsonConvert.DeserializeObject(Of LanguageData)(data)
        Return parsedData
    End Function

    Public Sub getItemData(path As String) 'uses the private methods to extract the data from an item file, deserialize it into an itemClassMirror class and extracts the generic information
        Dim data As ItemClassMirror = deserializeItemData(cutData(getRawData(path)))
        ItemName = data.Name
        If data.itemTooltipCategory = ItemTooltipCategory.None Then
            Output = ""
        ElseIf ItemName = "Admin Sword" Or ItemName = "Uber Admin Sword" Then
            Output = ""
        Else
            Output = GetGenericItemInfoOutput(data)
        End If
    End Sub

    Public Sub getAspectsData(path As String) 'uses the private methods to extract the data from an item file, deserialize it into an itemClassMirror class and extracts the aspects information

        Dim data As ItemClassMirror = deserializeItemData(cutData(getRawData(path)))
        ItemName = data.Name
        If data.PowerAspects.Count = 0 Then
            Output = ""
            Exit Sub
        End If
        Dim rawlanguagedata As String = GetLanguageData()
        deserializedlanguageData = deserializeLanguageData(rawlanguagedata)
        Output = GetAspectsOutput(data)
    End Sub

    Public Sub getMaterialData(path As String) 'extracts the crafting material data from a folder

        Dim files As System.IO.FileInfo() = Nothing
        If path IsNot Nothing Then
            Dim dinfo As New System.IO.DirectoryInfo(path)
            files = dinfo.GetFiles("*.json")
        End If

        If files IsNot Nothing AndAlso files.Count <> 0 Then
            For Each file As IO.FileInfo In files

                Dim parsedData As MaterialClassMirror
                parsedData = JsonConvert.DeserializeObject(Of MaterialClassMirror)(cutData(getRawData(file.FullName)))
                MaterialProperties.Add(parsedData)

            Next
        End If

    End Sub

    Public Sub getRecipeData(path As String)

        Dim data As CraftingRecipeMirror = deserializeRecipeData(cutData(getRawData(path)))


        Output = GetRecipeInfoOutput(data)

    End Sub
    Public Sub getEquipmentData(path As String) 'uses the private methods to extract the data from an item file, deserialize it into an itemClassMirror class and extracts the equipment information

        Dim data As ItemClassMirror = deserializeItemData(cutData(getRawData(path)))

        If data.EquipSlot <> 0 Then
            Output = GetEquipInfoOutput(data)
        Else 'it is not an equipment
            Output = ""
        End If


    End Sub


    Private Function GetGenericItemInfoOutput(data As ItemClassMirror) As String
        Dim output As String
        output = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "ItemCreator/defaultItemData.txt"))
        Dim itemName As String = GetItemName(data.Name, data.itemTooltipCategory, deserializedlanguageData, language, data.school)
        output = output.Replace("itemNameText", itemName)
        output = output.Replace("weightText", data.Weight.ToString)
        output = output.Replace("imageText", GetIcon(data.icon.m_PathID) & ".png")
        output = output.Replace("envImageText", "Env " & itemName & ".png")
        If data.Stackable Then
            output = output.Replace("stackText", data.MaxStackCount.ToString)
        Else
            output = output.Replace("stackText", "1")
        End If
        output = output.Replace("descriptionText", GetItemDescription(data.itemTooltipCategory, deserializedlanguageData, language, data.poisonTier))
        Return output
    End Function

    Private Function GetEquipInfoOutput(data As ItemClassMirror) As String
        Dim output As String = ""
        'equipment has a non standard structure, so I have to build it piece by piece

        If data.EquipSlot = EquipmentSlot.Mount Then ' I remove the mounts, which are equipments but I don't want them in the output
            Return ""
        End If

        Dim itemName As String = GetItemName(data.Name, data.itemTooltipCategory, deserializedlanguageData, language)
        output &= "     [""" & itemName & """] = {"
        output &= vbCrLf
        output &= "         [""Slot""] = " & """" & [Enum].GetName(GetType(EquipmentSlot), data.EquipSlot) & ""","
        output &= vbCrLf
        Dim durability As Integer = 300
        If data.ExplicitArmorSetData IsNot Nothing Then
            durability = data.ExplicitArmorSetData.durability
        ElseIf data.EquipSlot = EquipmentSlot.Necklace Then
            If data.isPrimitive Then
                durability = 200
            Else durability = 400
            End If
        End If
        output &= "         [""Durability""] = " & """" & durability.ToString & ""","
        output &= vbCrLf

        'armor info
        If data.armorSetData IsNot Nothing AndAlso data.armorSetData.m_PathID <> 0 Then

            data.ExplicitArmorSetData = getArmorSetData(getArmorSet(data.armorSetData.m_PathID), armorSetsFolder)

            output &= "         [""Armor Type""] =" & """" & [Enum].GetName(GetType(EquipmentSlot), data.ExplicitArmorSetData.weightClass) & ""","
            output &= vbCrLf

            Dim multiplier As Integer = 1
            If data.EquipType = EquipType.Shield Then
                multiplier = 5
            ElseIf data.armorPieceType = ArmorPieceType.Main Then
                multiplier = 2
            End If

            Dim physicalArmor As Integer = (data.ExplicitArmorSetData.physicalArmor * multiplier) / 5
            Dim crushArmor As Integer = (data.ExplicitArmorSetData.armor_Crush * multiplier) / 5
            Dim pierceArmor As Integer = (data.ExplicitArmorSetData.armor_Pierce * multiplier) / 5
            Dim slashArmor As Integer = (data.ExplicitArmorSetData.armor_Slash * multiplier) / 5
            Dim magicalArmor As Integer = (data.ExplicitArmorSetData.magicalArmor * multiplier) / 5
            Dim fireResistance As Integer = (data.ExplicitArmorSetData.magicalResistance_Fire * multiplier) / 5
            Dim shockResistance As Integer = (data.ExplicitArmorSetData.magicalResistance_Shock * multiplier) / 5
            Dim poisonResistance As Integer = (data.ExplicitArmorSetData.magicalResistance_Poison * multiplier) / 5
            Dim iceResistance As Integer = (data.ExplicitArmorSetData.magicalResistance_Ice * multiplier) / 5
            Dim corrosionResistance As Integer = (data.ExplicitArmorSetData.magicalResistance_Acid * multiplier) / 5
            Dim energyResistance As Integer = (data.ExplicitArmorSetData.magicalResistance_Energy * multiplier) / 5
            Dim heatInsulation As Integer = (data.ExplicitArmorSetData.heatInsulation * multiplier) / 5
            Dim coldInsulation As Integer = (data.ExplicitArmorSetData.coldInsulation * multiplier) / 5
            Dim encumbrance As Integer = (data.ExplicitArmorSetData.armorEncumbrance * multiplier) / 5

            If physicalArmor <> 0 Then
                output &= "         [""Physical Armor""] =" & " """ & physicalArmor.ToString & ""","
                output &= vbCrLf
            End If
            If crushArmor <> 0 Then
                output &= "         [""Crush Armor""] =" & " """ & crushArmor.ToString & ""","
                output &= vbCrLf
            End If
            If pierceArmor <> 0 Then
                output &= "         [""Pierce Armor""] =" & " """ & pierceArmor.ToString & ""","
                output &= vbCrLf
            End If
            If slashArmor <> 0 Then
                output &= "         [""Slash Armor""] =" & " """ & slashArmor.ToString & ""","
                output &= vbCrLf
            End If
            If magicalArmor <> 0 Then
                output &= "         [""Magical Armor""] =" & " """ & magicalArmor.ToString & ""","
                output &= vbCrLf
            End If
            If fireResistance <> 0 Then
                output &= "         [""Fire Resistance""] =" & " """ & fireResistance.ToString & ""","
                output &= vbCrLf
            End If
            If shockResistance <> 0 Then
                output &= "         [""Shock Resistance""] =" & " """ & shockResistance.ToString & ""","
                output &= vbCrLf
            End If
            If iceResistance <> 0 Then
                output &= "         [""Ice Resistance""] =" & " """ & iceResistance.ToString & ""","
            End If
            If corrosionResistance <> 0 Then
                output &= "         [""Acid Resistance""] =" & " """ & corrosionResistance.ToString & ""","
                output &= vbCrLf
            End If
            If energyResistance <> 0 Then
                output &= "         [""Energy Resistance""] =" & " """ & energyResistance.ToString & ""","
                output &= vbCrLf
            End If
            If heatInsulation <> 0 Then
                output &= "         [""Heat Insulation""] =" & " """ & heatInsulation.ToString & ""","
                output &= vbCrLf
            End If
            If coldInsulation <> 0 Then
                output &= "         [""Cold Insulation""] =" & " """ & coldInsulation.ToString & ""","
                output &= vbCrLf
            End If
            If encumbrance <> 0 Then
                output &= "         [""Encumbrance""] =" & " """ & encumbrance.ToString & ""","
                output &= vbCrLf
            End If

        End If

        'weapon info
        If data.enchantClass = 2 Then

            If data.twoHanded And data.WeaponClass = WeaponClass.Melee Then
                output &= "         [""Weapon Type""] =" & " ""Two Handed Melee"","
            ElseIf data.twoHanded And data.WeaponClass = WeaponClass.Ranged Then
                output &= "         [""Weapon Type""] =" & " ""Two Handed Ranged"","
            ElseIf data.twoHanded And data.WeaponClass = WeaponClass.Mage Then
                output &= "         [""Weapon Type""] =" & " ""Two Handed Ranged"","
            ElseIf Not data.twoHanded And data.WeaponClass = WeaponClass.Melee Then
                output &= "         [""Weapon Type""] =" & " ""One Handed Melee"","
            ElseIf Not data.twoHanded And data.WeaponClass = WeaponClass.Ranged Then
                output &= "         [""Weapon Type""] =" & " ""One Handed Ranged"","
            ElseIf Not data.twoHanded And data.WeaponClass = WeaponClass.Mage Then
                output &= "         [""Weapon Type""] =" & " ""One Handed Mage"","
            ElseIf data.WeaponClass = WeaponClass.Shield Then
                output &= "         [""Weapon Type""] =" & " ""Shield"","
            End If
            output &= vbCrLf

            Dim specialProperties As String = ""
            If data.poisonable And data.spellChanneling Then
                specialProperties = "Poisonable, Spell Channeling"
            ElseIf data.poisonable Then
                specialProperties = "Poisonable"
            ElseIf data.spellChanneling Then
                specialProperties = "Spell Channeling"
            End If
            output &= "         [""Special Properties""] = " & """" & specialProperties & ""","
            output &= vbCrLf

            Dim damageType As String = ""
            If data.damage.dmgType2 = 0 Then
                damageType = [Enum].GetName(GetType(DamageType), data.damage.dmgType1)
            ElseIf data.damage.dmgType3 = 0 Then
                damageType = [Enum].GetName(GetType(DamageType), data.damage.dmgType1) & ", " & [Enum].GetName(GetType(DamageType), data.damage.dmgType2)
            Else
                damageType = [Enum].GetName(GetType(DamageType), data.damage.dmgType1) & ", " & [Enum].GetName(GetType(DamageType), data.damage.dmgType2) & ", " & [Enum].GetName(GetType(DamageType), data.damage.dmgType3)
            End If
            output &= "         [""Damage Type""] = " & """" & damageType & ""","
            output &= vbCrLf

            Dim damageAttribute As String = ""
            If data.damage.dmgAttr2 = 0 Then
                damageAttribute = [Enum].GetName(GetType(CharacterAttribute), data.damage.dmgAttr1)
            Else
                damageAttribute = [Enum].GetName(GetType(CharacterAttribute), data.damage.dmgAttr1) & " | " & [Enum].GetName(GetType(CharacterAttribute), data.damage.dmgAttr2)
            End If
            output &= "         [""Damage Attribute""] = " & """" & damageAttribute & ""","
            output &= vbCrLf

            output &= "         [""Damage""] = " & """" & data.damage.damageModifier & ""","
            output &= vbCrLf

            output &= "         [""Attack Speed""] = " & """" & [Enum].GetName(GetType(WeaponAttackSpeed), data.attackSpeed) & ""","
            output &= vbCrLf

            Dim range As String = ""
            If data.weaponDamageType = WeaponDamageType.ConeDamage Then
                range = "Cone " & data.damageAmplitude & "°, " & data.damageLength & ""
            ElseIf data.weaponDamageType = WeaponDamageType.LineDamageFirstTarget Then
                range = "Line, First Target,  " & data.projectileDespawn & "m"
            ElseIf data.weaponDamageType = WeaponDamageType.LineDamage Then
                range = "Line, " & data.damageLength & "m"
            ElseIf data.weaponDamageType = WeaponDamageType.ProjectileDamage Then
                range = "Line, First Target,  " & data.projectileDespawn & "m"
            End If

            output &= "         [""Range""] = " & """" & range & ""","
            output &= vbCrLf

            'casting schools
            output &= "         [""Fighting Schools""] = {"
            output &= vbCrLf
            Dim listOfSchools As List(Of String) = Enumeratori.combinedEnumeratorNameToList(GetType(SpellSchool), data.avaibleCastingSchool)
            For Each school As String In listOfSchools
                output &= "             """ & school & """"
                If listOfSchools.IndexOf(school) <> listOfSchools.Count - 1 Then ' se non è l'ultimo, aggiungo la virgola
                    output &= ","
                End If
                output &= vbCrLf
            Next
            output &= "             },"
            output &= vbCrLf

        End If



        'Dim statusRawData As String = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "AssetIndices/unifiedIndex.xml"))
        'Dim attackRawData As String = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "AssetIndices/unifiedIndex.xml"))
        'Dim wpRawData As String = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "AssetIndices/unifiedIndex.xml"))
        'Dim enchantsRawData As String = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "AssetIndices/unifiedIndex.xml"))

        'inherent properties

        For Each prop As WeaponProperty In data.weaponProperties
            Dim propName As String = MonsterFinalData.getAttackPropertyName(prop.Property.m_PathID, weaponPropertiesFolder, enchantmentsFolder, deserializedlanguageData, EnumLanguage.English, assetList)
            Dim propValue As String = prop.baseAmount.ToString
            If WpIsPercentage(prop.Property.m_PathID, weaponPropertiesFolder, assetList) Or propName = "Poison Chance" Then
                propValue &= "%"
            End If
            output &= "         [""" & propName & """] = " & """" & propValue & ""","
            output &= vbCrLf
        Next

        'material properties

        If Not data.isPrimitive Then

            output &= "         [""Material Stats""] = {"
            output &= vbCrLf

            Dim material As MaterialGroups
            If data.ExplicitArmorSetData IsNot Nothing Then
                material = data.ExplicitArmorSetData.materialGroup
            Else
                material = data.materialGroup
            End If


            If data.EquipType = EquipType.ClothMain Or data.EquipType = EquipType.ClothSecondary Or data.EquipType = EquipType.LightMain Or data.EquipType = EquipType.LightSecondary Or data.EquipType = EquipType.MediumMain Or data.EquipType = EquipType.MediumSecondary Or data.EquipType = EquipType.HeavyMain Or data.EquipType = EquipType.HeavySecondary Then
                For Each materiale As MaterialClassMirror In MaterialProperties
                    If materiale.MaterialGroup = material Then
                        output &= "             {"
                        output &= vbCrLf
                        For Each prop As WeaponProperty In materiale.armorEnchantments
                            Dim propName As String = MonsterFinalData.getAttackPropertyName(prop.Property.m_PathID, weaponPropertiesFolder, enchantmentsFolder, deserializedlanguageData, EnumLanguage.English, assetList)
                            Dim propValue As String = prop.baseAmount.ToString
                            If WpIsPercentage(prop.Property.m_PathID, weaponPropertiesFolder, assetList) Or propName = "Poison Chance" Then
                                propValue &= "%"
                            End If
                            output &= "             [""" & propName & """] = " & """" & propValue & ""","
                            output &= vbCrLf
                        Next
                        output &= "             },"
                        output &= vbCrLf
                    End If
                Next
            ElseIf data.EquipType = EquipType.Weapon Or data.EquipType = EquipType.MagicWeapon Or data.EquipType = EquipType.MagicOrb Then
                For Each materiale As MaterialClassMirror In MaterialProperties
                    If materiale.MaterialGroup = material Then
                        output &= "             {"
                        output &= vbCrLf
                        For Each prop As WeaponProperty In materiale.weaponEnchantments
                            Dim propName As String = MonsterFinalData.getAttackPropertyName(prop.Property.m_PathID, weaponPropertiesFolder, enchantmentsFolder, deserializedlanguageData, EnumLanguage.English, assetList)
                            Dim propValue As String = prop.baseAmount.ToString
                            If WpIsPercentage(prop.Property.m_PathID, weaponPropertiesFolder, assetList) Or propName = "Poison Chance" Then
                                propValue &= "%"
                            End If
                            output &= "             [""" & propName & """] = " & """" & propValue & ""","
                            output &= vbCrLf
                        Next
                        output &= "             },"
                        output &= vbCrLf
                    End If
                Next
            ElseIf data.EquipType = EquipType.Necklace Or data.EquipType = EquipType.Ring Then
                For Each materiale As MaterialClassMirror In MaterialProperties
                    If materiale.MaterialGroup = material Then
                        output &= "             {"
                        output &= vbCrLf
                        For Each prop As WeaponProperty In materiale.amuletRingEnchantments
                            Dim propName As String = MonsterFinalData.getAttackPropertyName(prop.Property.m_PathID, weaponPropertiesFolder, enchantmentsFolder, deserializedlanguageData, EnumLanguage.English, assetList)
                            Dim propValue As String = prop.baseAmount.ToString
                            If WpIsPercentage(prop.Property.m_PathID, weaponPropertiesFolder, assetList) Or propName = "Poison Chance" Then
                                propValue &= "%"
                            End If
                            output &= "             [""" & propName & """] = " & """" & propValue & ""","
                            output &= vbCrLf
                        Next
                        output &= "             },"
                        output &= vbCrLf
                    End If
                Next
            ElseIf data.EquipType = EquipType.Shield Then
                For Each materiale As MaterialClassMirror In MaterialProperties
                    If materiale.MaterialGroup = material Then
                        output &= "             {"
                        output &= vbCrLf
                        For Each prop As WeaponProperty In materiale.shieldEnchantments
                            Dim propName As String = MonsterFinalData.getAttackPropertyName(prop.Property.m_PathID, weaponPropertiesFolder, enchantmentsFolder, deserializedlanguageData, EnumLanguage.English, assetList)
                            Dim propValue As String = prop.baseAmount.ToString
                            If WpIsPercentage(prop.Property.m_PathID, weaponPropertiesFolder, assetList) Or propName = "Poison Chance" Then
                                propValue &= "%"
                            End If
                            output &= "             [""" & propName & """] = " & """" & propValue & ""","
                            output &= vbCrLf
                        Next
                        output &= "             },"
                        output &= vbCrLf
                    End If
                Next
            End If

            output &= "         },"
            output &= vbCrLf



        End If



        output &= "     },"
        Return output
    End Function

    Private Function GetAspectsOutput(data As ItemClassMirror) As String
        Dim output As String
        output = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "ItemCreator/defaultReagentData.txt"))
        output = output.Replace("reagentNameValue", GetItemName(data.Name, 0, deserializedlanguageData, language))
        output = output.Replace("rarityText", GetReagentRarity(data.reagentRarity, deserializedlanguageData, language))
        Dim i As Integer = 0
        For Each aspect As MagicAspectValue In data.PowerAspects
            i += 1
            Dim targetName As String = "aspect" & i
            Dim targetValue As String = "Value" & i
            output = output.Replace(targetName, [Enum].GetName(GetType(Aspects), aspect.aspect))
            output = output.Replace(targetValue, aspect.value)
        Next
        'rimuovo gli aspetti non usati
        output = output.Replace("[""aspect2""] = {""Value2""},", "")
        output = output.Replace("[""aspect3""] = {""Value3""},", "")
        Return output
    End Function

    Private Function GetRecipeInfoOutput(data As CraftingRecipeMirror) As String

        Dim output As String
        output = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "ItemCreator/defaultRecipeData.txt"))
        'I need the item name and item tooltip type of the created item
        Dim itemref As String = ""
        If data.craftedItem.m_PathID <> 0 Then
            itemref = LootCreator.getItem(data.craftedItem.m_PathID, assetList)
        Else
            Return ""
        End If
        Dim item As ItemClassMirror = deserializeItemData(cutData(getRawData(itemsFolder & "/" & itemref & ".json")))
        'non craftabili
        If itemref = "Item_PoisonPasteStrong" Then
            Return ""
        ElseIf itemref = "Item_PoisonPasteWeak" Then
            Return ""
        ElseIf itemref = "Item_PoisonPasteSimple" Then
            Return ""
        ElseIf itemref = "item_DualDagger" Then
            Return ""
        End If
        Dim itemName As String = GetItemName(itemref, item.itemTooltipCategory, deserializedlanguageData, language)

        output = output.Replace("itemNameText", itemName)
        output = output.Replace("quantityText", data.craftedQuantity.ToString)
        output = output.Replace("timeText", data.craftingTime.ToString)
        output = output.Replace("stationText", "") ' TODO

        output = output.Replace("spazioIngredienti", GetIngredients(data))
        Return output
    End Function

    Shared Function GetItemName(name As String, itemType As Integer, deserializedLanguageData As LanguageData, language As Integer, Optional school As Integer = 0) As String 'used also by Loot Creator and monster creator
        Dim term As String = ""
        Dim output As String = ""
        name = name.Replace(" ", "") 'alcuni vecchi oggetti hanno degli spazi all'interno
        name = name.Replace("ChainMailArmor", "chainMail")
        name = name.Replace("PlateArmor", "fullPlate")
        'eccezioni
        If name = "grilledMarrows" Then
            name = "grilledMarrow"
        ElseIf name = "item_Onions" Then
            name = "item_onion"
        ElseIf name = "item_BearMeat" Or name = "bearMeat" Then
            name = "item_rawBearMeat"
        ElseIf name = "item_WolfMeat" Then
            name = "item_rawWolfMeat"
        ElseIf name = "item_MooseMeat" Then
            name = "item_rawMooseMeat"
        ElseIf name = "item_MammothMeat" Then
            name = "item_rawMammothMeat"
        ElseIf name = "item_Venison" Then
            name = "item_rawVenison"
        ElseIf name = "item_JotunnBlood" Then
            name = "item_jotunBlood"
        ElseIf name = "item_HideArmor_AnimalHead_Bear" Then
            name = "item_hideArmor_BearHead"
        ElseIf name = "item_HideArmor_AnimalHead_Stag" Then
            name = "item_hideArmor_StagHead"
        ElseIf name = "item_HideArmor_AnimalHead_Wolf" Then
            name = "item_hideArmor_WolfHead"
        ElseIf name = "item_HideArmor_AnimalHead_Warthog" Then
            name = "item_hideArmor_WarthogHead"
        ElseIf name = "item_ThrowingDagger(UNUSED)" Then
            name = "item_ThrowingDagger"
        ElseIf name = "item_SlayerArmor_Gauntlets" Then
            name = "item_SlayerArmor_Gloves"
        ElseIf name = "item_KnightArmor_Gauntlets" Then
            name = "item_KnightArmor_Gloves"
        End If

        'If name = "item_PrimitiveMace" Then 'correzione bug
        '    itemType = ItemTooltipCategory.Weapon
        'End If


        If itemType = Enumeratori.ItemTooltipCategory.ProficiencyOrb Then
            Dim schoolName As String = GetSpellSchoolName(school, deserializedLanguageData, language)
            Dim orbDescription As String = deserializedLanguageData.returnTerm("ui/item_tooltip_proficiencyOrb", language)
            output = orbDescription.Replace("{0}", schoolName)

        ElseIf itemType = Enumeratori.ItemTooltipCategory.Armor Or
                 itemType = Enumeratori.ItemTooltipCategory.Weapon Or
                 itemType = Enumeratori.ItemTooltipCategory.Shield Or
            itemType = Enumeratori.ItemTooltipCategory.Trinket Then

            term = "equip/" & name & "_name"
            term = term.Replace("item_", "")
            term = term.Replace("Item_", "") 'sometimes the I is capital
            output = deserializedLanguageData.returnTerm(term, language)
        ElseIf itemType = Enumeratori.ItemTooltipCategory.CraftingRecipe Then 'too difficult to get the name for these ones, I will just get it from the file name
            Return name.Replace("item_UnlockableRecipe_", "").Replace(".json", "")
        Else
            term = "items/" & name & "_name"
            term = term.Replace("item_", "")
            term = term.Replace("Item_", "") 'sometimes the I is capital
            output = deserializedLanguageData.returnTerm(term, language)

        End If


        If output = "" Then
            Return "WARNING!" & " " & name
        Else
            Return output
        End If
    End Function

    Private Function GetItemDescription(category As Integer, deserializedLanguageData As LanguageData, language As Integer, Optional poisonTier As String = "0") As String 'used also by Loot Creator and monster creator
        Dim tooltipCategory As String = [Enum].GetName(GetType(ItemTooltipCategory), category)
        Dim term As String = "ui/item_tooltip_ItemDescription_" & tooltipCategory
        Dim output As String = deserializedLanguageData.returnTerm(term, language)
        If output = "" Then
            Return ""
        End If
        output = output.Replace("/n", "")
        output = output.Replace(vbCr, "").Replace(vbLf, "")        ' carriage returns break the module code

        If category = ItemTooltipCategory.Poison Then 'needs a second part to the description
            term = "ui/item_tooltip_poisonTierStack_" & poisonTier
            Dim additionalOutput = deserializedLanguageData.returnTerm(term, language)

            additionalOutput = additionalOutput.Replace("/n", "")
            additionalOutput = additionalOutput.Replace(vbCr, "").Replace(vbLf, "")        ' carriage returns break the module code
            output &= "</br>" & additionalOutput
        End If

        Return output

    End Function

    Private Function GetReagentRarity(rarity As Integer, deserializedLanguageData As LanguageData, language As Integer) As String
        Dim term As String = "ui/item_tooltip_reagentRarity_" & [Enum].GetName(GetType(ReagentRarity), rarity)
        Return deserializedLanguageData.returnTerm(term, language)
    End Function

    Shared Function GetIcon(iconRef As String) As String 'used also by statusEffectDataCreator
        Dim rawdata As String = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "AssetIndices/icons.xml"))
        Dim iconName As String = ""
        Dim index As Integer
        If iconRef <> "0" Then
            index = rawdata.IndexOf(iconRef)
        Else
            Return "Icon Unknown"
        End If

        If index <> -1 Then
            Dim startName As Integer = rawdata.LastIndexOf("<Name>", index)
            Dim endName As Integer = rawdata.IndexOf("</Name>", startName)
            iconName = rawdata.Substring(startName + 6, endName - startName - 6)
        End If

        Return iconName
    End Function

    Shared Function GetRecipe(recipeRef As String, assets As AssetItemsList) As String
        Dim recipeName As String = ""
        If recipeRef <> "0" Then
        Else
            Return "No Icon"
        End If

        Return MainWindow.GetAssetFromIndex(recipeRef, assets).Name
    End Function
    Shared Function GetRecipeData(recipeName As String, recipeFolder As String) As RecipeClassMirror
        Dim filePath As String = recipeFolder & "\" & recipeName & ".json"
        Dim recipe As New RecipeClassMirror
        If System.IO.File.Exists(filePath) Then
            Dim textData As String = System.IO.File.ReadAllText(filePath)
            recipe = Newtonsoft.Json.JsonConvert.DeserializeObject(Of RecipeClassMirror)(textData)
        Else Throw New Exception(filePath & " Not Found")
            Return Nothing
        End If
        Return recipe
    End Function


    Shared Function GetSpellSchoolName(school As Integer, deserializedLanguageData As LanguageData, language As Integer) As String 'used by getItemName
        Dim term As String = "ui/spellSchool_" & [Enum].GetName(GetType(SpellSchool), school)
        Dim output As String = deserializedLanguageData.returnTerm(term, language)
        Return output
    End Function

    Private Function GetIngredients(data As CraftingRecipeMirror) As String

        Dim ingredientSpace As String = ""
        If data.ingredients.Count <> 0 Then
            Dim complessa As Boolean = False
            Dim materialGroup As Integer = 0
            Dim materialGroupQuantity As Integer = 0
            For Each ingrediente As Ingredient In data.ingredients
                If ingrediente.canUseMaterial Then
                    complessa = True
                    materialGroup = ingrediente.materialGroup
                    materialGroupQuantity = ingrediente.requiredQuantity
                End If
            Next
            'procedura semplice
            If Not complessa Then
                ingredientSpace &= "          {"
                ingredientSpace &= vbCrLf
                For i = 0 To data.ingredients.Count - 1
                    Dim partialOutput As String = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "ItemCreator/defaultIngredientData.txt"))
                    Dim ingrRef As String = LootCreator.getItem(data.ingredients(i).item.m_PathID, assetList)
                    Dim ingr As ItemClassMirror = deserializeItemData(cutData(getRawData(itemsFolder & "/" & ingrRef & ".json")))
                    Dim ingrName As String = GetItemName(ingrRef, ingr.itemTooltipCategory, deserializedlanguageData, language)
                    Dim icona As String = GetIcon(ingr.icon.m_PathID) & ".png"

                    partialOutput = partialOutput.Replace("ingredientNameText", ingrName)
                    partialOutput = partialOutput.Replace("quantityText", data.ingredients(i).requiredQuantity)
                    partialOutput = partialOutput.Replace("imageText", icona)

                    ingredientSpace &= partialOutput
                    If Not i = data.ingredients.Count - 1 Then
                        ingredientSpace &= ","
                    End If
                    ingredientSpace &= vbCrLf
                Next
                ingredientSpace &= "          }"
                ingredientSpace &= vbCrLf
            Else
                'procedura complessa
                Dim materialList As List(Of MaterialClassMirror) = getSubmaterialGroup(materialGroup)
                For i = 0 To materialList.Count - 1
                    Dim partialOutput As String = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "ItemCreator/defaultIngredientData.txt"))
                    Dim ingrRef As String = LootCreator.getItem(materialList(i).items(0).m_PathID, assetList)
                    Dim ingr As ItemClassMirror = deserializeItemData(cutData(getRawData(itemsFolder & "/" & ingrRef & ".json")))
                    Dim ingrName As String = GetItemName(ingrRef, ingr.itemTooltipCategory, deserializedlanguageData, language)
                    Dim icona As String = GetIcon(ingr.icon.m_PathID) & ".png"
                    partialOutput = partialOutput.Replace("ingredientNameText", ingrName)
                    partialOutput = partialOutput.Replace("quantityText", materialGroupQuantity)
                    partialOutput = partialOutput.Replace("imageText", icona)
                    ingredientSpace &= "            {"
                    ingredientSpace &= vbCrLf
                    ingredientSpace &= partialOutput
                    ingredientSpace &= ","
                    ingredientSpace &= vbCrLf
                    For j = 0 To data.ingredients.Count - 1
                        If Not data.ingredients(j).canUseMaterial Then
                            partialOutput = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "ItemCreator/defaultIngredientData.txt"))
                            ingrRef = LootCreator.getItem(data.ingredients(j).item.m_PathID, assetList)
                            ingr = deserializeItemData(cutData(getRawData(itemsFolder & "/" & ingrRef & ".json")))
                            ingrName = GetItemName(ingrRef, ingr.itemTooltipCategory, deserializedlanguageData, language)
                            icona = GetIcon(ingr.icon.m_PathID) & ".png"
                            partialOutput = partialOutput.Replace("ingredientNameText", ingrName)
                            partialOutput = partialOutput.Replace("quantityText", data.ingredients(j).requiredQuantity)
                            partialOutput = partialOutput.Replace("imageText", icona)
                            ingredientSpace &= partialOutput
                            If Not j = data.ingredients.Count - 1 Then
                                ingredientSpace &= ","
                            End If
                            ingredientSpace &= vbCrLf
                        End If
                    Next
                    ingredientSpace &= "            }"
                    If Not i = materialList.Count - 1 Then
                        ingredientSpace &= ","
                    End If
                    ingredientSpace &= vbCrLf
                Next
            End If

        End If
        Return ingredientSpace

    End Function

    Private Function getSubmaterialGroup(materialGroup As Integer) As List(Of MaterialClassMirror)
        Dim sublist As New List(Of MaterialClassMirror)
        For Each materiale As MaterialClassMirror In MaterialProperties
            If materiale.MaterialGroup = materialGroup Then
                sublist.Add(materiale)
            End If
        Next
        Return sublist
    End Function

    Private Function getArmorSet(armorSetRef As Integer)
        Dim data As New FullSetArmorData
        Dim setName As String = ""
        If armorSetRef <> "0" Then
        Else
            Return ""
        End If
        Return MainWindow.GetAssetFromIndex(armorSetRef, assetList).Name
    End Function

    Private Function getArmorSetData(armorSetName As String, armorSetFolder As String)
        Dim filePath As String = armorSetFolder & "\" & armorSetName & ".json"
        Dim armorSet As New FullSetArmorData
        If System.IO.File.Exists(filePath) Then
            Dim textData As String = System.IO.File.ReadAllText(filePath)
            armorSet = Newtonsoft.Json.JsonConvert.DeserializeObject(Of FullSetArmorData)(textData)
        Else Throw New Exception(filePath & " Not Found")
            Return Nothing
        End If
        Return armorSet
    End Function

    Private Function WpIsPercentage(wpref As String, wpfolder As String, assets As AssetItemsList)
        Dim isPercentage As Boolean = False
        Dim attackPropertyData As New WeaponPropertyData
        Dim filePath As String = MainWindow.GetAssetFromIndex(wpref, assets).Name
        If System.IO.File.Exists(filePath) Then
            Dim textData As String = System.IO.File.ReadAllText(filePath)
            attackPropertyData = Newtonsoft.Json.JsonConvert.DeserializeObject(Of WeaponPropertyData)(textData)
            isPercentage = attackPropertyData.isPercentage
        End If
        Return isPercentage
    End Function

End Class