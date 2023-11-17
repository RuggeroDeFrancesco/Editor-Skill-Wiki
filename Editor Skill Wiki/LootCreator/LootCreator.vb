Imports Newtonsoft.Json
Public Class LootCreator

    'class that parses a json file of a loot list and extracts all the fields in a usable format.

    Dim language As Integer

    Dim deserializedlanguageData As LanguageData

    Public listName As String

    Dim lootData As List(Of LootClassMirror)

    Dim itemData As List(Of ItemClassMirror)

    Dim lootListIndex As String

    Dim itemListIndex As String
    Public Property Output As String 'full list of the properties, to be used for the datamodule on the wiki
    Public Sub New(language As Integer, lootPath As String, itempath As String)

        Me.language = language
        Dim rawlanguagedata As String = GetLanguageData()
        deserializedlanguageData = deserializeLanguageData(rawlanguagedata)
        getLootData(lootPath)
        getItemData(itempath)
        lootListIndex = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "AssetIndices/unifiedIndex.xml"))
        itemListIndex = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "AssetIndices/unifiedIndex.xml"))
    End Sub

    Public Sub parseData(path As String) 'main method of the class, uses the private methods to extract the data from a loot file, deserialize it into a lootClassMirror class and extracts information from lootClassMirror

        Dim data As MonsterData = MonsterDataCreator.parseMonsterData(getRawData(path))
        Output = createOutput(data)
    End Sub

    Private Sub getLootData(path As String)

        Dim files As System.IO.FileInfo() = Nothing

        If path IsNot Nothing Then
            Dim dinfo As New System.IO.DirectoryInfo(path)
            files = dinfo.GetFiles("*.json")
            lootData = New List(Of LootClassMirror)
        Else Exit Sub
        End If

        If files IsNot Nothing AndAlso files.Count <> 0 Then
            For Each file As IO.FileInfo In files
                lootData.Add(deserializeLootData(getRawData(file.FullName)))
            Next
        Else Exit Sub
        End If

    End Sub

    Private Sub getItemData(path As String)

        Dim files As System.IO.FileInfo() = Nothing

        If path IsNot Nothing Then
            Dim dinfo As New System.IO.DirectoryInfo(path)
            files = dinfo.GetFiles("*.json")
            itemData = New List(Of ItemClassMirror)
        Else Exit Sub
        End If

        If files IsNot Nothing AndAlso files.Count <> 0 Then
            For Each file As IO.FileInfo In files
                Try
                    itemData.Add(ItemDataCreator.deserializeItemData(getRawData(file.FullName)))
                Catch ex As Exception

                End Try
            Next
        Else Exit Sub
        End If

    End Sub

    Private Function createOutput(data As MonsterData) As String 'converts the enumerators into clear text and adds the categories
        Dim output As String
        If data.m_name = "class_NPC" Then
            Return ""
        End If
        output = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "LootCreator/defaultLootData.txt"))
        If data.lootList IsNot Nothing Then


            Dim lootListName As String = getLootList(data.lootList.m_PathID)
            If lootListName = "" Then
                Return ""
            End If
            For Each lootList As LootClassMirror In lootData
                If lootList.m_Name = lootListName Then
                    output = output.Replace("monsterNameText", MonsterDataCreator.getMonsterName(data.m_name, deserializedlanguageData, language))

                    output = output.Replace("minimumGoldText", lootList.minGold.ToString)
                    output = output.Replace("maximumGoldText", lootList.maxGold.ToString)

                    output = output.Replace("maximumGemsText", lootList.maxGems.ToString)
                    output = output.Replace("chippedChanceText", lootList.chippedChance.ToString)
                    output = output.Replace("fineChanceText", lootList.fineChance.ToString)
                    output = output.Replace("flawlessChanceText", lootList.flawlessChance.ToString)
                    output = output.Replace("amethystText", lootList.amethyst.ToString)
                    output = output.Replace("diamondText", lootList.diamond.ToString)
                    output = output.Replace("emeraldText", lootList.emerald.ToString)
                    output = output.Replace("rubyText", lootList.ruby.ToString)
                    output = output.Replace("sapphireText", lootList.sapphire.ToString)
                    output = output.Replace("topazText", lootList.topaz.ToString)

                    output = output.Replace("regionText", lootList.loreTabletsLoot.region.ToString)
                    output = output.Replace("minimumCommonText", lootList.loreTabletsLoot.common.minQuantity.ToString)
                    output = output.Replace("maximumCommonText", lootList.loreTabletsLoot.common.maxQuantity.ToString)
                    output = output.Replace("probabilityCommonText", lootList.loreTabletsLoot.common.probability.ToString)
                    output = output.Replace("minimumUncommonText", lootList.loreTabletsLoot.uncommon.minQuantity.ToString)
                    output = output.Replace("maximumUncommonText", lootList.loreTabletsLoot.uncommon.maxQuantity.ToString)
                    output = output.Replace("probabilityUncommonText", lootList.loreTabletsLoot.uncommon.probability.ToString)
                    output = output.Replace("minimumRareText", lootList.loreTabletsLoot.rare.minQuantity.ToString)
                    output = output.Replace("maximumRareText", lootList.loreTabletsLoot.rare.maxQuantity.ToString)
                    output = output.Replace("probabilityRareText", lootList.loreTabletsLoot.rare.probability.ToString)

                    Dim itemLootList As String = extractLootItems(lootList)
                    output = output.Replace("itemDropsSpace", itemLootList)

                End If
            Next
        End If
        Return output
    End Function


    Private Function getRawData(path As String) As String 'reads the file and extracts the content as text
        If System.IO.File.Exists(path) Then
            Dim rawData As String
            rawData = System.IO.File.ReadAllText(path)
            Return rawData
        Else Throw New System.Exception("File does not exist.")
            Return ""
        End If
    End Function

    Private Function deserializeLootData(data As String) As LootClassMirror 'converts the json to fields of the SpellData class

        Dim parsedData As LootClassMirror
        parsedData = JsonConvert.DeserializeObject(Of LootClassMirror)(data)
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

    Private Function getLootList(lootList As String) As String
        If lootList = "0" Then
            Return ""
        End If
        Dim lootListName As String = ""
        Dim index As Integer = lootListIndex.IndexOf(lootList)
        Dim startName As Integer = lootListIndex.LastIndexOf("<Name>", index)
        Dim endName As Integer = lootListIndex.IndexOf("</Name>", startName)
        lootListName = lootListIndex.Substring(startName + 6, endName - startName - 6)
        Return lootListName
    End Function

    Shared Function getItem(item As String, itemListIndex As String) As String 'used also by monster data creator and Item data creator
        If item <> "0" Then
            Dim itemRef As String = ""
            Dim index As Integer = itemListIndex.IndexOf(item)
            Dim startName As Integer = itemListIndex.LastIndexOf("<Name>", index)
            Dim endName As Integer = itemListIndex.IndexOf("</Name>", startName)
            itemRef = itemListIndex.Substring(startName + 6, endName - startName - 6)
            Return itemRef
        Else
            Return ""
        End If



    End Function

    Private Function extractLootItems(lootList As LootClassMirror) As String
        Dim output As String = ""
        lootList.probabilityLoot.AddRange(lootList.alternateLoot) 'I join the 2 lists of loot into one
        For Each item As ItemLoot In lootList.probabilityLoot
            Dim partialOutput As String = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "LootCreator/defaultItemLootData.txt"))
            Dim itemRef As String = getItem(item.item.m_PathID, itemListIndex)
            If itemRef = "" Then
                Return ""
            End If
            Dim itemName As String = ItemDataCreator.GetItemName(itemRef, 0, deserializedlanguageData, language)
            partialOutput = partialOutput.Replace("itemNameText", itemName)
            partialOutput = partialOutput.Replace("minimumQuantityText", item.minQuantity.ToString)
            partialOutput = partialOutput.Replace("maximumQuantityText", item.maxQuantity.ToString)
            partialOutput = partialOutput.Replace("dropProbabilityText", item.probability.ToString)
            output &= partialOutput
            output &= vbCrLf
        Next

        Return output
    End Function

End Class
