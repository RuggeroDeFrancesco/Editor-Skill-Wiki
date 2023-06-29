Imports Newtonsoft.Json
Public Class ItemDataCreator
    Dim language As Integer

    Dim deserializedlanguageData As LanguageData
    Public Property Output As String

    Public ItemName As String

    Public Sub New(language As Integer)
        Me.language = language
        Dim rawlanguagedata As String = GetLanguageData()
        deserializedlanguageData = deserializeLanguageData(rawlanguagedata)
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
        ElseIf ItemName = "Admin Sword" Then
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
        output = output.Replace("descriptionText", GetItemDescription(data.itemTooltipCategory, deserializedlanguageData, language))
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
        Return output
    End Function

    Shared Function GetItemName(name As String, itemType As Integer, deserializedLanguageData As LanguageData, language As Integer, Optional school As Integer = 0) As String 'used also by Loot Creator and monster creator
        Dim term As String = ""
        Dim output As String = ""
        name = name.Replace(" ", "") 'alcuni vecchi oggetti hanno degli spazi all'interno
        'eccezioni
        If name = "grilledMarrows" Then
            name = "grilledMarrow"
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
        End If

        If itemType = Enumeratori.ItemTooltipCategory.ProficiencyOrb Then
            Dim schoolName As String = GetSpellSchoolName(school, deserializedLanguageData, language)
            Dim orbDescription As String = deserializedLanguageData.returnTerm("ui/item_tooltip_proficiencyOrb", language)
            output = orbDescription.Replace("{0}", schoolName)

        ElseIf itemType = Enumeratori.ItemTooltipCategory.Armor Or
                 itemType = Enumeratori.ItemTooltipCategory.Weapon Or
                 itemType = Enumeratori.ItemTooltipCategory.Shield Or
            itemType = Enumeratori.ItemTooltipCategory.Trinket Then

            term = "equip/" & name & "_name"
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

    Private Function GetItemDescription(category As Integer, deserializedLanguageData As LanguageData, language As Integer) As String 'used also by Loot Creator and monster creator
        Dim tooltipCategory As String = [Enum].GetName(GetType(ItemTooltipCategory), category)
        Dim term As String = "ui/item_tooltip_ItemDescription_" & tooltipCategory
        Dim output As String = deserializedLanguageData.returnTerm(term, language)
        If output = "" Then
            Return ""
        End If
        output = output.Replace("/n", "")
        output = output.Replace(vbCr, "").Replace(vbLf, "")        ' carriage returns break the module code
        'If output = "" Then 'potrebbe essere un equip
        '    term = "equip/" & category & "_desc"
        '    output = deserializedLanguageData.returnTerm(term, language)
        'Else
        '    Return output
        'End If

        'If output = "" Then
        '    Return "WARNING!" & " " & category
        'Else
        Return output
        'End If
    End Function

    Private Function GetReagentRarity(rarity As Integer, deserializedLanguageData As LanguageData, language As Integer) As String
        Dim term As String = "ui/item_tooltip_reagentRarity_" & [Enum].GetName(GetType(ReagentRarity), rarity)
        Return deserializedLanguageData.returnTerm(term, language)
    End Function

    Shared Function GetIcon(iconRef As String) As String
        Dim rawdata As String = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "AssetIndices/icons.xml"))
        Dim iconName As String = ""
        Dim index As Integer
        If iconRef <> "0" Then
            index = rawdata.IndexOf(iconRef)
        Else
            Return "No Icon"
        End If

        If index <> -1 Then
            Dim startName As Integer = rawdata.LastIndexOf("<Name>", index)
            Dim endName As Integer = rawdata.IndexOf("</Name>", startName)
            iconName = rawdata.Substring(startName + 6, endName - startName - 6)
        End If
        Return iconName
    End Function

    Shared Function GetRecipe(recipeRef As String) As String
        Dim rawdata As String = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "AssetIndices/recipeIndex.xml"))
        Dim recipeName As String = ""
        Dim index As Integer
        If recipeRef <> "0" Then
            index = rawdata.IndexOf(recipeRef)
        Else
            Return "No Icon"
        End If

        If index <> -1 Then
            Dim startName As Integer = rawdata.LastIndexOf("<Name>", index)
            Dim endName As Integer = rawdata.IndexOf("</Name>", startName)
            recipeName = rawdata.Substring(startName + 6, endName - startName - 6)
        End If
        Return recipeName
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



End Class
