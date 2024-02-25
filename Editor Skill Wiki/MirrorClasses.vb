Imports Newtonsoft.Json

Namespace MirrorClasses

	Public Class AssetBase
		Public m_GameObject As Sprite
		Public m_enabled As Boolean
		Public m_Script As Sprite
	End Class




	Public Class SpellInfo
		Inherits AssetBase

		Public spellIndex As Integer

		Public SpellKey As String

		Public spellSchool As Enumerators.SpellSchool

		Public spellType As Enumerators.SpellType

		Public unicityGroup As Enumerators.SpellUnicityGroup

		Public effectGroup As Enumerators.EffectGroup

		Public irremovable As Boolean

		Public tooltipActivationType As Enumerators.SpellTooltipActivationType

		Public tooltipTargetType As Enumerators.SpellTooltipTargetType

		Public pathfinding As Enumerators.SpellPathfinding

		Public readyTrigger As Enumerators.ReadyTrigger

		Public friendlyFire As Enumerators.FriendlyFire

		Public SecureHit As Boolean

		Public dontRemoveStealth As Boolean

		Public avoidBlockControllerCheck As Boolean

		Public blockInSiege As Boolean

		Public canBeCritical As Enumerators.SpellCrit

		Public baseCriticalDamage As Double

		Public IncorporealCheck As Boolean

		Public LearnSpellIndex As Integer

		Public gameReady As Boolean

		Public removed As Boolean

		Public difficulty As Enumerators.SpellDifficulty

		Public spellGroup As Enumerators.MonsterCastingStrategy

		Public castChance As Integer

		Public cooldown As Double

		Public cooldownModAttribute As Enumerators.CharacterAttribute

		Public cooldownModCoefficient As Double

		Public activationType As Enumerators.SpellActivationType

		Public targetType As Enumerators.SpellTargetType

		Public targetingRange As Double

		Public castingRange As Double

		Public checkLos As Boolean

		Public avoidPathFinding As Boolean

		Public linkSpellDuration As Double

		Public spellcastingRestrictions As SpellcastingRestrictions

		Public targetEntityType As Enumerators.SpellEntityType

		Public requireStatusOnTarget As Enumerators.StatusEffect

		Public preCastTime As Double

		Public resourceCost As Double

		Public spellData As New List(Of SpellDataInfo)

		Public spellTags As New List(Of SpellTagDataInfo)

		Public PlayerSpell As Boolean 'Custom property

		Public Icon As Sprite

	End Class

	Public Class SpellcastingRestrictions
		Public Enum DamageRestriction
			Slash = 1
			Pierce = 2
			Crush = 4
		End Enum

		Public weaponRestriction As Enumerators.SpellWeaponRestriction

		Public specialWeaponRestriction As Enumerators.SpellSpecialWeaponRestriction

		Public spellSchool As Enumerators.SpellSchool

		Public restrictedArmorWeight As Enumerators.EquipWeightClass

	End Class

	Public Class SpellDataInfo
		Public dataType As Enumerators.SpellDataType

		Public attributeType As Enumerators.SpellCoefficientAttributeType

		Public modifierType As Enumerators.SaveModifierType

		Public level0 As Double

		Public level10 As Double
	End Class

	Public Class SpellTagDataInfo
		Public dataType As Enumerators.SpellTagType

		Public tooltipHidden As Boolean
	End Class

	Public Class MonsterClass



		Public Class LootListVariant

			Public lootList As LootList

			Public continentID As Integer
		End Class


		Public m_Name As String

		Public challengeRating As Integer

		Public halfKillsRequired As Boolean

		Public monsterRace As Enumerators.MonsterRace

		Public lootList As LootList

		Public lootListVariants As List(Of LootListVariant) = New List(Of LootListVariant)

		Public capturable As Boolean

		Public petItemType As PetItemType = New PetItemType

		Public skinAmount As Integer

		Public champion As Boolean

		Public difficulty As Enumerators.LegendDifficulty

		Public isSpirit As Boolean

		Public poiAmount As Integer

		Public allowedPoi As List(Of Enumerators.POIType) = New List(Of Enumerators.POIType)


		Public spawnsAmount As Integer

		Public championVersionId As Integer

		Public knowledgeEntry As Sprite

		Public excludeFromKnowledgeSystem As Boolean = False

		Public tutorialMonster As Boolean = False

		Public walkSpeed As Integer

		Public baseSpeed As Integer

		Public rotationSpeed As Double

		Public str As KnowledgeUnlockableInt

		Public dex As KnowledgeUnlockableInt

		Public _int As KnowledgeUnlockableInt

		Public cos As KnowledgeUnlockableInt

		Public per As KnowledgeUnlockableInt

		Public wis As KnowledgeUnlockableInt

		Public baseEndurance As KnowledgeUnlockableInt

		Public baseEnduranceRegen As KnowledgeUnlockableInt

		Public baseEnergy As KnowledgeUnlockableInt

		Public baseEnergyRegen As KnowledgeUnlockableInt

		Public evasion As KnowledgeUnlockableInt

		Public accuracy As KnowledgeUnlockableInt

		Public fortitude As KnowledgeUnlockableInt

		Public willpower As KnowledgeUnlockableInt

		Public stealth As KnowledgeUnlockableInt

		Public detection As KnowledgeUnlockableInt

		Public spellDamageIncrease As KnowledgeUnlockableInt

		Public cooldownReduction As KnowledgeUnlockableInt

		Public baseLuck As KnowledgeUnlockableInt

		Public criticalChance As KnowledgeUnlockableInt

		Public criticalDamage As KnowledgeUnlockableInt

		Public detectionRange As KnowledgeUnlockableInt

		Public armorSlash As KnowledgeUnlockableInt

		Public armorPierce As KnowledgeUnlockableInt

		Public armorCrush As KnowledgeUnlockableInt

		Public fireResistance As KnowledgeUnlockableInt

		Public coldResistance As KnowledgeUnlockableInt

		Public shockResistance As KnowledgeUnlockableInt

		Public magicResistance As KnowledgeUnlockableInt

		Public poisonResistance As KnowledgeUnlockableInt

		Public acidResistance As KnowledgeUnlockableInt

		Public magicalDamageReflection As Integer

		Public physicalDamageReflection As Integer

		Public slashDamageIncrease As Integer

		Public pierceDamageIncrease As Integer

		Public crushDamageIncrease As Integer

		Public fireDamageIncrease As Integer

		Public iceDamageIncrease As Integer

		Public shockDamageIncrease As Integer

		Public acidDamageIncrease As Integer

		Public poisonDamageIncrease As Integer

		Public energyDamageIncrease As Integer

		Public fireDamageConversion As Integer

		Public iceDamageConversion As Integer

		Public shockDamageConversion As Integer

		Public acidDamageConversion As Integer

		Public poisonDamageConversion As Integer

		Public energyDamageConversion As Integer

		Public damageAbsorbed As List(Of Enumerators.DamageType) = New List(Of Enumerators.DamageType)

		Public statusImmune As Boolean

		Public immunityEffectGroup As Enumerators.EffectGroup

		Public immunityStatusEffectList As List(Of Sprite) = New List(Of Sprite)

		Public specialImmunities As Enumerators.SpecialMonsterImmunity

		Public specialTraits As List(Of KnowledgeUnlockableTitleAndDesc) = New List(Of KnowledgeUnlockableTitleAndDesc)

		Public monsterPoisonStack As Integer

		Public MonsterAttacks As List(Of KnowledgeUnlockableMonsterAttack) = New List(Of KnowledgeUnlockableMonsterAttack)

		Public MonsterSpellsList As List(Of KnowledgeUnlockableMonsterSpell) = New List(Of KnowledgeUnlockableMonsterSpell)

		Public spellZSpawnOffset As Double

		Public aboveHeadBarOffset As Double = 1

		Public randomFloatsGenerated As Integer = 0

		Public idleSoundFrequency As Double

		Public idleSoundChance As Integer

		Public specialAttackSoundChance As Integer

		Public specialAttackSound As String

		Public idleClip As String

		Public deathClip As String

		Public fallDeathClip As String

		Public footStepClip As String

		Public optionalMonsterClip As String

		Public IsDummy As Boolean = False

		Public playerAttitude As Enumerators.PlayerAttitude = Enumerators.PlayerAttitude.Aggressive

		Public combatStance As Enumerators.CombatStance = Enumerators.CombatStance.Fighter

		Public BroadcastAggression As Boolean = False

		Public BroadcastRadius As Double = 10

		Public MaxRoamingSpawnDistance As Double

		Public MinRoamingTravel As Double

		Public MinCooldown As Double

		Public MaxCooldown As Double

		Public RoamingDetectionRange As Double

		Public MaxHuntingTargetDistance As Double

		Public MaxHuntingSpawnDistance As Double

		Public FleeingDetectionRange As Double

		Public RunningDistance As Double

		Public ChangeSpawnPointDistance As Double
	End Class

	Public Class LootList

		Public minGold As Integer

		Public maxGold As Integer

		Public maxGems As Integer

		Public chippedChance As Double

		Public fineChance As Double

		Public flawlessChance As Double

		Public amethyst As Boolean

		Public diamond As Boolean

		Public emerald As Boolean

		Public ruby As Boolean

		Public sapphire As Boolean

		Public topaz As Boolean

		Public probabilityLoot As List(Of ProbabilityLoot) = New List(Of ProbabilityLoot)

		Public alternateLoot As List(Of AlternateLoot) = New List(Of AlternateLoot)

		Public divineRewardsLoot As DivineRewardsLoot = New DivineRewardsLoot

	End Class

	Public Structure ProbabilityLoot

		Public item As ItemType

		Public minQuantity As Integer

		Public maxQuantity As Integer

		Public probability As Double
	End Structure

	Public Structure AlternateLoot

		Public item As ItemType

		Public minQuantity As Integer

		Public maxQuantity As Integer

		Public lootGroup As Integer

		Public probability As Double
	End Structure

	Public Structure DivineRewardsLoot

		Public amount As Integer

		Public chanceValiant As Double

		Public chanceHeroic As Double

		Public chanceFabled As Double

		Public chanceEpic As Double

		Public chanceLegendary As Double
	End Structure

	Public Class ItemType

		Public Structure MagicAspectValue

			Public aspect As Enumerators.Aspects

			Public value As Integer
		End Structure

		Public ItemTypeID As Integer

		Public Name As String

		Public Description As String

		Public Weight As Double

		Public Stackable As Boolean

		Public toolCategory As Enumerators.ToolCategory = Enumerators.ToolCategory.None

		Public itemCategory As Enumerators.ItemCategory = Enumerators.ItemCategory.None

		Public itemTooltipCategory1 As Enumerators.ItemTooltipCategory

		Public itemTooltipCategory2 As Enumerators.ItemTooltipCategory

		Public marketplaceFilter As Enumerators.MarketPlaceSubFilter = Enumerators.MarketPlaceSubFilter.None

		Public PowerAspects As List(Of MagicAspectValue) = New List(Of MagicAspectValue)

		Public legendary As Boolean

		Public reagentRarity As Enumerators.ReagentRarity

		Public ReadOnly Property IsReagent
			Get
				If PowerAspects.Count > 0 Then
					Return True
				Else Return False
				End If
			End Get
		End Property

	End Class

	Public Class PetItemType
		Inherits EquipItemType

		Public spellIndex As List(Of Integer) = New List(Of Integer)

		Public runSpeed As List(Of Integer) = New List(Of Integer)

		Public walkSpeed As List(Of Integer) = New List(Of Integer)

		Public health As List(Of Integer) = New List(Of Integer)

		Public abilityPower As List(Of Integer) = New List(Of Integer)

	End Class

	Public Class EquipItemType
		Inherits ItemType


		Public equipEditorType As Enumerators.EquipItemEditorType = Enumerators.EquipItemEditorType.Deffault

		Public armorPieceType As Enumerators.ArmorPieceType

		Public EquipSlot As Enumerators.EquipmentSlot

		Public EquipWeightClass As Enumerators.EquipWeightClass

		Public EquipType As Enumerators.EquipType

		Public enchantClass As Enumerators.EquipEnchantClass

		Public isPrimitive As Boolean

		Public abilityPowerBonusType As Enumerators.EquipAbilityPowerType

		Public armorSetData As FullSetArmorData = New FullSetArmorData

		Public requiredEnchantTech As Enumerators.TechTreeEnchantClass = Enumerators.TechTreeEnchantClass.None

		Public nativeEnchantments As List(Of NativeEnchantmentData) = New List(Of NativeEnchantmentData)

		Public weaponProperties As List(Of WeaponPropertyData) = New List(Of WeaponPropertyData)

		Public ReadOnly Property PieceMultiplier As Double
			Get
				Select Case EquipType
					Case Enumerators.EquipType.Shield
						Return 1
					Case Enumerators.EquipType.ClothMain
						Return 0.4
					Case Enumerators.EquipType.LightMain
						Return 0.4
					Case Enumerators.EquipType.MediumMain
						Return 0.4
					Case Enumerators.EquipType.HeavyMain
						Return 0.4
					Case Else
						Return 0.2
				End Select

			End Get
		End Property

		Public ReadOnly Property PieceOnlyPhysicalArmor As Integer
			Get
				Return armorSetData.physicalArmor * PieceMultiplier
			End Get
		End Property

		Public ReadOnly Property PieceOnlyMagicalArmor As Integer
			Get
				Return armorSetData.magicalArmor * PieceMultiplier
			End Get
		End Property

		Public ReadOnly Property PieceOnlySlashArmor As Integer
			Get
				Return armorSetData.armor_Slash * PieceMultiplier
			End Get
		End Property

		Public ReadOnly Property PieceOnlyCrushArmor As Integer
			Get
				Return armorSetData.armor_Crush * PieceMultiplier
			End Get
		End Property

		Public ReadOnly Property HeatInsulation As Integer
			Get
				Return armorSetData.heatInsulation * PieceMultiplier
			End Get
		End Property

		Public ReadOnly Property ColdInsulation As Integer
			Get
				Return armorSetData.coldInsultaion * PieceMultiplier
			End Get
		End Property





		'Public override String GetLocalizedName()
		'{
		'	String locName = LocalizationHelper.GetLocalizedText("equip/" + Name + "_name");
		'	If (locName!= "")
		'	{
		'		Return locName;
		'	}
		'	Return Name;
		'}

		'Public override String GetLocalizedDescription()
		'{
		'	String locDesc = LocalizationHelper.GetLocalizedText("equip/" + Name + "_desc");
		'	If (String.IsNullOrEmpty(locDesc))
		'	{
		'		foreach (CraftingRecipe craftingRecipe in CraftingRecipesList.List)
		'		{
		'			If (craftingRecipe!= null && craftingRecipe.craftedItem!= null && craftingRecipe.craftedItem.ItemTypeID == ItemTypeID)
		'			{
		'				locDesc = craftingRecipe.GetLocalizedDescription();
		'			}
		'		}
		'	}
		'	Return locDesc;
		'}





		'Public float GetItemMaterialProperty(String metadata, uint propertyIndex)
		'{
		'	float value = 0F;
		'	uint materialByte = ItemMetadata.GetMaterialByte(Metadata, this);
		'	If (materialByte!= 0)
		'	{
		'		CraftingMaterialInfo materialInfo = CraftingHelper.GetMaterialFromIndex(materialByte);
		'		If (materialInfo!= null)
		'		{
		'			If (this Is WeaponItemType)
		'			{
		'				If ((this as WeaponItemType).WeaponType == WeaponType.Wand)
		'				{
		'					foreach (WeaponItemType.WeaponPropertyData materialProp in materialInfo.staffEnchantments)
		'					{
		'						GenericEnchantment_WP enchantmentProperty = materialProp.property As GenericEnchantment_WP;
		'						If (enchantmentProperty!= null && (bool)enchantmentProperty.enchantment && enchantmentProperty.enchantment.enchantmentIndex == propertyIndex)
		'						{
		'							value += materialProp.baseAmount;
		'						}
		'					}
		'				}
		'				Else
		'				{
		'					foreach (WeaponItemType.WeaponPropertyData materialProp2 in materialInfo.weaponEnchantments)
		'					{
		'						GenericEnchantment_WP enchantmentProperty2 = materialProp2.property As GenericEnchantment_WP;
		'						If (enchantmentProperty2!= null && (bool)enchantmentProperty2.enchantment && enchantmentProperty2.enchantment.enchantmentIndex == propertyIndex)
		'						{
		'							value += materialProp2.baseAmount;
		'						}
		'					}
		'				}
		'			}
		'			ElseIf (EquipType == EquipType.Necklace || EquipType == EquipType.Ring)
		'			{
		'				foreach (WeaponItemType.WeaponPropertyData materialProp3 in materialInfo.amuletRingEnchantments)
		'				{
		'					GenericEnchantment_WP enchantmentProperty3 = materialProp3.property As GenericEnchantment_WP;
		'					If (enchantmentProperty3!= null && (bool)enchantmentProperty3.enchantment && enchantmentProperty3.enchantment.enchantmentIndex == propertyIndex)
		'					{
		'						value += materialProp3.baseAmount;
		'					}
		'				}
		'			}
		'			ElseIf (EquipType == EquipType.Shield)
		'			{
		'				foreach (WeaponItemType.WeaponPropertyData materialProp4 in materialInfo.shieldEnchantments)
		'				{
		'					GenericEnchantment_WP enchantmentProperty4 = materialProp4.property As GenericEnchantment_WP;
		'					If (enchantmentProperty4!= null && (bool)enchantmentProperty4.enchantment && enchantmentProperty4.enchantment.enchantmentIndex == propertyIndex)
		'					{
		'						value += materialProp4.baseAmount;
		'					}
		'				}
		'			}
		'			Else
		'			{
		'				foreach (WeaponItemType.WeaponPropertyData materialProp5 in materialInfo.armorEnchantments)
		'				{
		'					GenericEnchantment_WP enchantmentProperty5 = materialProp5.property As GenericEnchantment_WP;
		'					If (enchantmentProperty5!= null && (bool)enchantmentProperty5.enchantment && enchantmentProperty5.enchantment.enchantmentIndex == propertyIndex)
		'					{
		'						value += materialProp5.baseAmount;
		'					}
		'				}
		'			}
		'		}
		'	}
		'	Return value;



	End Class

	Public Class NativeEnchantmentData

		Public enchantment As EnchantmentRecipe

		Public tier As Integer

		Public requiredEquipType As Enumerators.EquipType = Enumerators.EquipType.None
	End Class

	Public Class EnchantmentRecipe

		Public enchantmentIndex As Integer

		Public recipeName As String

		Public allowedGemFamilies As Enumerators.GemFamily

		Public allowedEquipItems As List(Of Enumerators.EquipEnchantClass) = New List(Of Enumerators.EquipEnchantClass)

		Public enchantmentGroup As Enumerators.RecipeGroup

		Public aspects As List(Of Enumerators.Aspects) = New List(Of Enumerators.Aspects)

		Public doesntHaveTier As Boolean

		Public valueT1 As Double

		Public valueT2 As Double

		Public valueT3 As Double
	End Class

	Public Class GemItemType
		Inherits ItemType

		Public family As Enumerators.GemFamily

		Public quality As Enumerators.GemQuality

	End Class

	Public Class FullSetArmorData

		Public id As Integer

		Public physicalArmor As Integer

		Public armor_Slash As Integer

		Public armor_Pierce As Integer

		Public armor_Crush As Integer

		Public magicalArmor As Integer

		Public armorEncumbrance As Integer

		Public magicResistance_Fire As Integer

		Public magicResistance_Ice As Integer

		Public magicResistance_Shock As Integer

		Public magicResistance_Energy As Integer

		Public magicResistance_Poison As Integer

		Public magicResistance_Acid As Integer

		Public heatInsulation As Integer

		Public coldInsultaion As Integer

		Public tier As Enumerators.EquipTier

		Public weight As Double

		Public weightClass As Enumerators.EquipWeightClass

		Public durability As Integer

		Public materialGroup As Enumerators.MaterialGroups = Enumerators.MaterialGroups.Cloth

		Public armorSetBonus As Enumerators.ArmorSetBonus

		'Public String LocalizedName => LocalizationHelper.GetLocalizedText("ui/" + base.name);

		'Public String LocalizedDescription => LocalizationHelper.GetLocalizedText("items/" + base.name + "_desc");

		'Public String LocalizedFullSetBonus => LocalizationHelper.GetLocalizedText("items/" + base.name + "_fullSetBonus");

	End Class

	Public Class CraftingMaterialInfo

		Public materialIndex As Integer

		Public materialName As String

		Public materialGroup As Enumerators.MaterialGroups

		Public materialTier As Enumerators.EquipTier

		Public items As List(Of ItemType)

		Public armorEnchantments As List(Of WeaponPropertyData) = New List(Of WeaponPropertyData)

		Public shieldEnchantments As List(Of WeaponPropertyData) = New List(Of WeaponPropertyData)

		Public weaponEnchantments As List(Of WeaponPropertyData) = New List(Of WeaponPropertyData)

		Public staffEnchantments As List(Of WeaponPropertyData) = New List(Of WeaponPropertyData)

		Public amuletRingEnchantments As List(Of WeaponPropertyData) = New List(Of WeaponPropertyData)

		Public equipTypeOnly As Boolean

		Public restrictEquipType As Boolean

		Public allowedEquipTypes As List(Of Enumerators.EquipType) = New List(Of Enumerators.EquipType)

		Public allowedItems As Enumerators.EquipCraftingType
	End Class

	Public Class WeaponItemType
		Inherits EquipItemType


		Public TrapModuleAttackID As Integer

		Public WeaponType As Enumerators.WeaponType

		Public WeaponClass As Enumerators.WeaponClass

		Public avaibleCastingSchool As Enumerators.SpellSchool

		Public tier As Enumerators.EquipTier

		Public twoHanded As Boolean

		Public poisonable As Boolean

		Public spellChanneling As Boolean

		Public materialGroup As Enumerators.MaterialGroups = Enumerators.MaterialGroups.Wood

		Public alternativeAnimationCount As Integer

		Public animationLength As Double

		Public inGameAnimationLength As Double

		Public DamageAnimationPercentage As Double

		Public attackClip As String

		Public hitClip As String

		Public damage As DamageInformation

		Public attackSpeed As Enumerators.WeaponAttackSpeed

		Public Range As Double

		Public baseCriticalDamage As Double

		Public roundIcon As Sprite

		Public WeaponDamageType As Enumerators.WeaponDamageType


		Public damageWidth As Double


		Public damageLength As Double

		Public damageAmplitude As Double

		Public projectileSpeed As Double


		Public projectileDespawn As Double


	End Class

	Public Class MonsterAttack
		Inherits WeaponItemType
		Public attackName As String

		Public AlternateAnimations As Integer

	End Class

	Public Class DamageInformation

		Public dmgType1 As Enumerators.DamageType

		Public dmgType2 As Enumerators.DamageType

		Public dmgType3 As Enumerators.DamageType

		Public baseDamage As Double

		Public weaponDamageModifier As Enumerators.WeaponDamageModifier

	End Class



	Public Class WeaponPropertyData
		<JsonProperty("property")>
		Public WeaponProperty As Sprite

		Public baseAmount As Double

		Private DeserializedProperty As WeaponProperty

		Public Sub DeserializeProperty(commonFunctions As CommonFunctions)
			Dim path As String = commonFunctions.GetAssetName(WeaponProperty.m_PathID, "WP_")
			DeserializedProperty = Newtonsoft.Json.JsonConvert.DeserializeObject(Of MirrorClasses.WeaponProperty)(commonFunctions.GetRawData(path))
		End Sub

		Public Function GetPropertyTooltip(commonFunctions As CommonFunctions) As String
			baseAmount = MathF.Floor(baseAmount * 10.0) / 10.0
			Dim propertyTooltip As String = commonFunctions.terms.getTerm(DeserializedProperty.localizationString, My.Settings.Language)
			If baseAmount > 0 And DeserializedProperty.signedValue Then
				propertyTooltip &= "+"
			End If
			propertyTooltip &= baseAmount
			If DeserializedProperty.isPercentage Then
				propertyTooltip &= "%"
			End If
			Return propertyTooltip
		End Function

	End Class

	Public Class WeaponProperty

		Public localizationString As String

		Public hasNoValue As Boolean

		Public signedValue As Boolean

		Public isPercentage As Boolean

		Public modifierTooltipKey As String

		Public iconUiSpriteKey As String

		Public Function GetLocalizedName(commonFunctions As CommonFunctions) As String
			Return commonFunctions.terms.getTerm("enchantments/" & localizationString & "_name", My.Settings.Language)
		End Function

		Public Function GetLocalizedDescription(commonFunctions As CommonFunctions) As String
			Return commonFunctions.terms.getTerm("enchantments/" & localizationString & "_desc", My.Settings.Language)
		End Function

	End Class

	Public Class KnowledgeUnlockableValue

		Public id As Enumerators.UnlockableIDs

		Public targetToUnlock As Integer = 1

	End Class

	Public Class KnowledgeUnlockableInt
		Inherits KnowledgeUnlockableValue

		Public value As Integer

		Public random As Integer

	End Class

	Public Class KnowledgeUnlockableTitleAndDesc
		Inherits KnowledgeUnlockableValue
		Public title As String

		Public description As String

	End Class

	Public Class KnowledgeUnlockableMonsterAttack
		Inherits KnowledgeUnlockableValue
		Public value As Sprite

	End Class

	Public Class KnowledgeUnlockableMonsterSpell
		Inherits KnowledgeUnlockableValue
		Public value As Sprite

		Public hideInKnowledgeBook As Boolean

		Public cooldownOverride As Integer

		Public globalCooldown As Integer

		Public castingChance As Integer

		Public castingPriority As Integer

		Public targetAlly As Boolean

		Public castingRestrictions As List(Of MonsterCastingRestriction)
	End Class


	Public Class StatusEffectInfo

		Public ID As Integer

		Public effectGroup As Enumerators.EffectGroup

		Public irremovable As Boolean

		Public statusKey As String

		Public Beneficial As Boolean

		Public DontSetCombatFlag As Boolean

		Public KeepWhenMounted As Boolean

		Public effectToDeactivate As List(Of StatusEffectInfo) = New List(Of StatusEffectInfo)

		Public effectGroupToDeactivate As Enumerators.EffectGroup

		Public immunityEffect As List(Of StatusEffectInfo) = New List(Of StatusEffectInfo)

		Public immunityEffectGroup As Enumerators.EffectGroup

		Public icon As Sprite

		'Public virtual String GetName()
		'{
		'	Return LocalizationHelper.GetLocalizedText("status_effect/" + statusKey + "_name");
		'}

		'Public virtual String GetDescription(List<float> seModifiers)
		'{
		'	Return LocalizationHelper.GetLocalizedText("status_effect/" + statusKey + "_desc");
		'}
	End Class

	Public Class BookOfKnowledgeEntry


		Public knowledgeId As Integer

		Public entryName As String

		Public backgroundImage As Sprite

		Public alternativeIds As List(Of Integer) = New List(Of Integer)

		Public categoryIndexing As Enumerators.Category
	End Class

	Public Class BookOfKnowledgeEntry_Bestiary
		Inherits BookOfKnowledgeEntry
		Public flavourText As String
	End Class

	Public Class MonsterCastingRestriction

		Public type As Enumerators.MonsterCastingRestrictionType

		Public value As Double

		Public statusEffec As Enumerators.StatusEffect

		Public statusEffectGroup As Enumerators.EffectGroup

	End Class

	Public Class Sprite
		Public m_FileID As Integer
		Public m_PathID As Double
	End Class

End Namespace


