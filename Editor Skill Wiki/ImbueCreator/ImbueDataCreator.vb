Imports System.ComponentModel
Imports Newtonsoft.Json

Public Class ImbueDataCreator

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

    Dim assetList As AssetItemsList

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

    Public Sub New(language As Integer)
        Me.language = language
        Dim rawlanguagedata As String = GetLanguageData()
        deserializedlanguageData = deserializeLanguageData(rawlanguagedata)
        assetList = MainWindow.GetAssets
    End Sub

    Public Sub getImbueData(path As String) 'uses the private methods to extract the data from an item file, deserialize it into an itemClassMirror class and extracts the generic information
        Dim data As ImbueClassMirror = deserializeImbueData(cutData(getRawData(path)))

        Output = GenerateImbueOutput(data)

    End Sub

    Private Function GenerateImbueOutput(data As ImbueClassMirror) As String
        Dim output As String
        If data.aspects.Count = 0 Or data.allowedEquipItems.Count = 0 Or data.allowedGemFamilies.Count = 0 Then
            Return ""
        End If
        output = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "ImbueCreator/defaultImbueData.txt"))
        Dim itemName As String = GetImbueName(data.recipeName, deserializedlanguageData, language)
        output = output.Replace("ImbueName", itemName)
        Dim value As String = data.value.ToString
        If data.amountType = EnchantmentAmountType.Percentage Then
            value = value & "%"
        End If

        output = output.Replace("recipeNameText", data.recipeName)
        output = output.Replace("valueText", value)
        output = output.Replace("groupText", [Enum].GetName(GetType(RecipeGroup), data.enchantmentGroup))
        output = output.Replace("spazioGemme", GetGems(data.allowedGemFamilies))
        output = output.Replace("spazioSlot", GetSlots(data.allowedEquipItems))
        output = output.Replace("spazioAspetti", GetAspects(data.aspects))
        Return output
    End Function

    Private Function deserializeImbueData(data As String) As ImbueClassMirror 'converts the json to fields of the RecipeClassMirror

        Dim parsedData As ImbueClassMirror
        parsedData = JsonConvert.DeserializeObject(Of ImbueClassMirror)(data)
        Return parsedData
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

    Private Function cutData(data As String) As String 'removes the useless parts of the file
        Dim processedData As String
        Dim startingIndex As Integer
        startingIndex = data.IndexOf("m_Name") - 1
        processedData = "{"
        processedData &= data.Substring(startingIndex)
        Return processedData
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

    Private Function GetImbueName(name As String, deserializedLanguageData As LanguageData, language As Integer) As String
        Dim term As String = ""
        Dim output As String = ""


        term = "enchantments/" & name & "_name"
        output = deserializedLanguageData.returnTerm(term, language)


        If output = "" Then
            Return "WARNING!" & " " & name
        Else
            Return output
        End If
    End Function

    Private Function GetImbueDescription(name As String, deserializedLanguageData As LanguageData, language As Integer) As String
        Dim term As String = ""
        Dim output As String = ""


        term = "enchantments/" & name & "_desc"
        output = deserializedLanguageData.returnTerm(term, language)


        If output = "" Then
            Return "WARNING!" & " " & name
        Else
            Return output
        End If

    End Function

    Private Function GetAspects(aspetti As List(Of Integer)) As String
        Dim spazioAspect As String = ""
        For Each aspect As Integer In aspetti
            Dim aspectText As String = [Enum].GetName(GetType(Aspects), aspect)
            spazioAspect &= "                  """
            spazioAspect &= aspectText
            spazioAspect &= """"
            If aspetti.IndexOf(aspect) <> aspetti.Count - 1 Then
                spazioAspect &= ","
                spazioAspect &= vbCrLf
            End If
        Next
        Return spazioAspect
    End Function


    Private Function GetSlots(aspetti As List(Of Integer)) As String
        Dim spazioSlot As String = ""
        For Each slot As Integer In aspetti
            Dim aspectText As String = [Enum].GetName(GetType(EquipEnchantClass), slot)
            spazioSlot &= "                  """
            spazioSlot &= aspectText
            spazioSlot &= """"
            If aspetti.IndexOf(slot) <> aspetti.Count - 1 Then
                spazioSlot &= ","
                spazioSlot &= vbCrLf
            End If
        Next
        Return spazioSlot
    End Function

    Private Function GetGems(aspetti As List(Of Integer)) As String
        Dim spazioGemme As String = ""
        For Each gem As Integer In aspetti
            Dim aspectText As String = [Enum].GetName(GetType(GemFamily), gem)
            spazioGemme &= "                  """
            spazioGemme &= aspectText
            spazioGemme &= """"
            If aspetti.IndexOf(gem) <> aspetti.Count - 1 Then
                spazioGemme &= ","
                spazioGemme &= vbCrLf
            End If
        Next
        Return spazioGemme
    End Function

End Class
