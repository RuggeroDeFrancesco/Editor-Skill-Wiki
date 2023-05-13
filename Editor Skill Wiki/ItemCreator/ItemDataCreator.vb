Imports Newtonsoft.Json
Public Class ItemDataCreator
    Dim language As Integer

    Dim deserializedlanguageData As LanguageData
    Public Property Output As String

    Public ItemName As String

    Public Sub New(language As Integer)
        language = Me.language
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

    Private Function deserializeSpellData(data As String) As ItemClassMirror 'converts the json to fields of the ItemClassMirror

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

    Public Sub getAspectsData(path As String) 'uses the private methods to extract the data from an item file, deserialize it into an itemClassMirror class and extracts the aspects information

        Dim data As ItemClassMirror = deserializeSpellData(cutData(getRawData(path)))
        ItemName = data.Name
        If data.PowerAspects.Count = 0 Then
            Output = ""
            Exit Sub
        End If
        Dim rawlanguagedata As String = GetLanguageData()
        deserializedlanguageData = deserializeLanguageData(rawlanguagedata)
        Output = GetAspectsOutput(data)
        'rawDescription = GetDescription(data)
        'parameters = GetSkillParameters(data)
        'SpellName = data.m_name
        'removed = data.removed
        'gameReady = data.gameReady
        'Output = Output.Replace("SpellName", GetTooltipName(SpellName, deserializedlanguageData, language).Replace("'", "\"))
    End Sub

    Private Function GetAspectsOutput(data As ItemClassMirror) As String
        Dim output As String
        output = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "ItemCreator/defaultReagentData.txt"))
        output = output.Replace("reagentNameValue", GetItemName(data.Name))
        Dim i As Integer = 0
        For Each aspect As MagicAspectValue In data.PowerAspects
            i += 1
            Dim targetName As String = "Aspect" & i
            Dim targetValue As String = "Value" & i
            output = output.Replace(targetName, [Enum].GetName(GetType(Aspects), aspect.aspect))
            output = output.Replace(targetValue, aspect.value)
        Next
        Return output
    End Function

    Private Function GetItemName(name As String) As String
        Dim term As String = "items/" & name & "_name"
        Return deserializedlanguageData.returnTerm(term, language)
    End Function

End Class
