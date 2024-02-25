Namespace Enumerators

	Public Enum Language
		English = 1
		German
		French
	End Enum

	Public Enum SpellSchool 'used by SpellInfo class and SpellcastingRestrictions class
		None = 0
		Pyromancy = 2
		Cryomancy = 4
		Aeromancy = 8
		Geomancy = 16
		Venomancy = 32
		Vitriomancy = 64
		Druidcraft = 128
		Witchcraft = 256
		Necromancy = 512
		Abjuration = 1024
		Restoration = 2048
		Illusionism = 4096
		Leadership = 8192
		Assassination = 16384
		Hunting = 32768
		Warfare = 65536
		MartialArts = 131072
		KnifeFighting = 262144
		Swordsmanship = 524288
		Fencing = 1048576
		MaceFighting = 2097152
		AxeFighting = 4194304
		PoleFighting = 8388608
		Archery = 16777216
		ShieldFighting = 33554432
	End Enum

	Public Enum SpellType 'used by SpellInfo class
		None
		Ready
		Toggle
		Self
		Target
		Link
		Location
		Skillshot
		Summon
	End Enum

	Public Enum SpellUnicityGroup 'used by SpellInfo class
		None
		Link
		ReadyStrike
		WeaponModifier
		Totem
		Aura
		BattleStance
		DamageProtection
		MagicalGlobe
		Skin
		Invisibility
		Incapacitated
		Snared
	End Enum

	Public Enum EffectGroup 'used by SpellInfo class
		None = 0
		MagicalBuff = 2
		Invisibility = 4
		ElementalCondition = 8
		PhysicalBuff = 16
		MagicalProtection = 32
		Snare = 64
		Disease = 128
		Illusion = 256
		MagicalDebuff = 512
		PhysicalDebuff = 1024
		Poison = 2048
		SpecialEffect = 4096
		Summon = 8192
		Incapacitated = 16384
		ArmorSetBonus = 32768
		PrimalForm = 65536
		Potion = 131072
	End Enum

	Public Enum SpellTooltipActivationType 'used by SpellInfo class
		None
		Targeted
		Sustained
		Ready
		Directional
		Self
	End Enum

	Public Enum SpellTooltipTargetType 'used by SpellInfo class
		None
		Terrain
		Enemy
		FrozenEnemy
		RootedEnemy
		Ally
		CorrodedEnemy
	End Enum

	Public Enum SpellPathfinding 'used by SpellInfo class
		Yes
		No
		Irrelevant
	End Enum

	Public Enum ReadyTrigger 'used by SpellInfo class
		None
		OnDeath
		OnAttack
		OnAttackReceived
	End Enum

	Public Enum FriendlyFire 'used by SpellInfo class
		Yes
		No
		Irrelevant
	End Enum

	Public Enum SpellCrit 'used by SpellInfo class
		Yes
		No
		Irrelevant
	End Enum

	Public Enum SpellDifficulty 'used by SpellInfo class
		Easy
		Medium
		Hard
	End Enum

	Public Enum MonsterCastingStrategy 'used by SpellInfo class
		None
		ReadyOnAttack
		TargetedOffensive
		TargetedBeneficial
		SelfOffensive
		SelfBeneficial
		Teleport
		ReadyOnDeath
	End Enum

	Public Enum CharacterAttribute 'used by SpellInfo class
		None
		Strength
		Dexterity
		Intelligence
		Constitution
		Perception
		Wisdom
	End Enum

	Public Enum SpellActivationType 'used by SpellInfo class
		Deffault 'default term is protected
		Sustained
		Hold
		Ready
		Link
	End Enum

	Public Enum SpellTargetType 'used by SpellInfo class
		Self
		Location
		Entity
		PlaceSpellEntityLocation
		PlaceSpellTotemLocation
	End Enum

	Public Enum SpellWeaponRestriction 'used by SpellcastingRestrictions class
		None
		TwoHanded
		SpellChanneling
		OneHanded
	End Enum

	Public Enum SpellSpecialWeaponRestriction 'used by SpellcastingRestrictions class
		None
		BowRequired
		ShieldRequired
		Unarmed
	End Enum

	Public Enum EquipWeightClass 'used by SpellcastingRestrictions class and FullSetArmorData Class
		None = 0
		Light = 1
		Medium = 2
		Heavy = 4
	End Enum

	Public Enum SpellEntityType 'used by SpellInfo class
		Player = 1
		Enemy = 2
		Ally = 4
		Monster = 8
		Self = 16
	End Enum

	Public Enum StatusEffect 'used by SpellInfo class
		None = 0
		BurningHot = 1
		Cold = 2
		FreezingCold = 3
		Hot = 4
		Hungry = 5
		Tired = 6
		Encumbered = 7
		Starving = 8
		Exhausted = 9
		Stunned = 10
		Bleeding = 11
		Frightened = 12
		HeavilyEncumbered = 13
		DeathMark = 14
		Blinded = 15
		Crippled = 16
		Silenced = 17
		StrikeWounds = 18
		Poisoned = 19
		Enraged = 20
		Slowed = 21
		Inspire = 22
		Absorb = 23
		Unconscious = 24
		Paralyzed = 25
		Snared_Web = 26
		NecroticDisease = 27
		ConcussiveStrike = 28
		WildfireTotemBuff = 29
		WildfireTotemDebuff = 30
		Snared_Net = 31
		VerdantRegrowth = 32
		TotemDecayDebuff = 33
		Hidden = 34
		PavedRoadBuff = 35
		MonsterProtection = 36
		ArmorDebuff = 37
		TamingTrap = 38
		Corrosion = 39
		Resolve = 40
		CallToTenacity = 41
		HomingAI = 42
		EmpowerBuff = 43
		Weakened = 44
		UnholyStr = 45
		DeathBargain = 46
		Knockback = 47
		Chilled = 48
		Warm = 49
		Frozen = 50
		Burning = 51
		Shocked = 52
		Shocked_Paralysis = 53
		LightningRush = 54
		BattleJump = 55
		Dazed = 56
		Confused = 57
		Magnetize = 58
		Explosion = 59
		Petrify = 60
		Unhealable = 61
		SnaredRoot = 62
		Overpower = 63
		RendArmor = 64
		Lunge = 65
		RamThrough = 66
		PierceThrough = 67
		ShadowDash = 68
		Bloodlust = 69
		ExecuteBuff = 70
		ExposeVulnDebuff = 71
		LifeDrain = 72
		ManaDrain = 73
		NimblenessBuff = 74
		ViciousAttacksBuff = 75
		TrueStrike = 76
		ShieldsUp = 77
		ChargeBuff = 78
		FeintBuff = 79
		DebilitatingStrikeDebuff = 80
		CrystalShacklesDebuff = 81
		BondOfAgonyDebuff = 82
		CleaveArmorDebuff = 83
		WizardClothesBuff = 84
		AssassinClothesBuff = 85
		RogueArmorSetBuff = 86
		ClericArmorSetBuff = 87
		SlayerArmorSetBuff = 88
		BattlemageArmorSetBuff = 89
		RangeArmorSetBuff = 90
		KnightArmorSetBuff = 91
		WarlockArmorSetBuff = 92
		HunterArmorSetBuff = 93
		DirtRoadBuff = 94
		PlayerInvulnerability = 95
		AuraOfDevotionBuff = 96
		AuraOfDevotionDebuff = 97
		ChallengeDebuff = 98
		LifeLinkBuff = 99
		InsectSwarmDebuff = 100
		StoneSkinBuff = 101
		Atrophied = 102
		HunterMarkDebuff = 103
		HunterMarkBuff = 104
		ChadraPrimalForm = 105
		ErwydraPrimalForm = 106
		BlessedHumanPrimalForm = 107
		Sated = 108
		Rested = 109
		CrushingAssault = 110
		Bandaging = 111
		BlackOoze = 112
		AuraOfClarity = 113
		AuraOfCourage = 114
		LifeShield = 115
		Resting = 116
		PotionConstitution = 117
		PotionDexterity = 118
		PotionIntelligence = 119
		PotionPerception = 120
		PotionStrength = 121
		PotionWisdom = 122
		PotionAntimagic = 123
		PotionAntivenom = 124
		PotionResFire = 125
		PotionResIce = 126
		PotionResShock = 127
		PotionShielding = 128
		PotionClarity = 129
		PotionCourage = 130
		PotionFormStability = 131
		PotionReflex = 132
		PotionRegeneration = 133
		PotionSharpness = 134
		PotionToughness = 135
		PotionEnergy = 136
		PotionVitality = 137
		PotionHauling = 138
		PotionLuck = 139
		PotionPower = 140
		PotionStealth = 141
		PotionThievery = 142
		PotionIronFist = 143
		PotionSpeed = 144
		PotionRecovery = 145
		PotionInsulation = 146
		PotionResAcid = 147
		Relocate = 148
		ChilledCondition = 149
		WarmCondition = 150
		Hexed = 151
		DrawBack = 176
	End Enum

	Public Enum SpellDataType 'used by SpellDataInfo class
		None
		ManaCost
		ManaCostPerSecond
		Cooldown
		FlatManaCostReduction
		FlatCooldownReduction
		SpellCharges
		ChannelingDuration
		AreaRadius
		ConeWidth
		ConeLength
		LineWidth
		LineLength
		LinkMaxDuration
		LinkMaxDistance
		DamageSlash
		DamagePierce
		DamageCrush
		DamageFire
		DamageIce
		DamageShock
		DamageEnergy
		DamageAcid
		DamagePoison
		DamagePure
		DamageSlashPerSecond
		DamagePiercePerSecond
		DamageCrushPerSecond
		DamageFirePerSecond
		DamageIcePerSecond
		DamageShockPerSecond
		DamageEnergyPerSecond
		DamageAcidPerSecond
		DamagePoisonPerSecond
		DamagePurePerSecond
		DamageAmplificationPercentage
		DamageFalloffPercentage
		DamagePercentage
		DamageIncreasePercentage
		DamageReductionPercentage
		ExtraWeaponDamage
		WeaponDamageIncreasePercentage
		DamageModifierPercentage
		ResistancePhysical
		ResistanceSlash
		ResistancePierce
		ResistanceCrush
		ResistanceMagical
		ResistanceFire
		ResistanceIce
		ResistanceShock
		ResistanceEnergy
		ResistancePoison
		ResistanceAcid
		ResistancesAll
		ResistanceBuff
		ResistanceDebuff
		ResistanceBuffPercentage
		ResistanceDebuffPercentage
		BuffStrength
		BuffDexterity
		BuffIntelligence
		BuffCostitution
		BuffPerception
		BuffCharisma
		DebuffStrength
		DebuffDexterity
		DebuffIntelligence
		DebuffCostitution
		DebuffPerception
		DebuffCharisma
		StacksWarm
		StacksChilled
		StacksShocked
		StacksPoison
		StacksCorrosion
		StacksBleeding
		StacksWarmPerSecond
		StacksChilledPerSecond
		StacksShockedPerSecond
		StacksPoisonPerSecond
		StacksCorrosionPerSecond
		StacksBleedingPerSecond
		DurationHidden
		DurationBlind
		DurationConfusion
		DurationDaze
		DurationFear
		DurationParalysis
		DurationPetrify
		DurationSilence
		DurationSlow
		DurationUnhealable
		DurationWeakened
		DurationAtrophied
		DurationCripple
		DurationRooted
		DurationStun
		DurationTrapped
		DurationWebbed
		DurationEffect
		DurationBuff
		DurationDebuff
		DurationWall
		DurationSummon
		DurationWebs
		DurationTrap
		TrapTriggerDistance
		TrapAreaRadius
		HealthBonus
		RegenerationHealth
		RegenerationMana
		HealthRecovered
		ManaLossPerSecond
		ManaLoss
		LifeStolen
		ManaStolen
		LifeStolenPerSecond
		ManaStolenPerSecond
		MaxHealthPercentage
		MissingHealthPercentage
		CurrentHealthPercentage
		ThresholdHealthPercentage
		ReflectionMagicalPercentage
		ReflectionPhysicalPercentage
		MoveSpeedBonusPercentage
		BonusWillpower
		BonusFortitude
		BonusEvasion
		BonusAccuracy
		EffectsFaloffPercentage
		ThresholdResistanceFlat
		IgnoredResistancePercentage
		ProjectilesCount
		FireballsPerSecond
		ImpactAreaRadius
		HailDropsPerSecond
		ShockwaveAreaRadius
		ShockwaveShockDamage
		ShockwavesCount
		LightningsPerSecond
		StacksMax
		StacksRegenerationTime
		AttacksCount
		BouncesMax
		BounceDistance
		BuffsRemoved
		MaxActivations
		KnockBackDistance
		DistanceDash
		EffectDelay
		MaxDamage
		ThresholdDamageFlat
		ThresholdDamagePercentage
		PureDamageToUndeads
		DamageAmplificationPerCreaturePercentage
		BlockChanceProjectilesPercentage
		DispelChancePercentage
		PlayersHealthThreshold
		PlayersHealthThresholdPercentage
		MonstersHealthThreshold
		MonstersHealthThresholdPercentage
		AttackSpeedBonusPercentage
		MaxExtraDamage
		EffectsAmplificationPercentage
		Replace
		BlockChancePercentage
		HealthRecoveredPercentage
		DamageAmplificationPerStackPercentage
		MaxManaCost
		RegenerationHealthDebuff
		CriticalDamageBonus
		StacksHexed
		StacksHexedPerSecond
		ActivationDelay
	End Enum

	Public Enum SpellCoefficientAttributeType 'used by SpellDataInfo class
		None
		PhysicalPower
		MagicalPower
		HealingPower
	End Enum

	Public Enum SaveModifierType 'used by SpellDataInfo class
		None
		Fortitude
		Willpower
		Evasion
	End Enum

	Public Enum SpellTagType 'used by SpellTagDataInfo class
		None
		MagicalAbility
		PhysicalAbility
		MagicalBuff
		MagicalDebuff
		PhysicalBuff
		PhysicalDebuff
		MagicalProtection
		ElementalCondition
		LinkedEffects
		Channeled
		Melee
		Ranged
		Projectile
		AoE
		Wall
		Mobility
		Healing
		StatusRemoval
		Summon
		Invisibility
		Trap
		Hidden
		Blinded
		Confused
		Dazed
		Frightened
		Paralyzed
		Petrified
		Silenced
		Slowed
		Unhealable
		Weakened
		Burning
		Chilled
		Frozen
		Shocked
		Warm
		Atrophied
		Bleeding
		Corrosion
		Crippled
		Poisoned
		Rooted
		Stunned
		Trapped
		Immortal
		Webbed
		Snared
		Magnetize
		Hexed
	End Enum

	Public Enum Aspects 'Used by ItemType Class
		Mind = 0
		Body = 1
		Soul = 2
		Create = 100
		Destroy = 101
		Negate = 102
		Transform = 103
		Transfer = 104
		Air = 200
		Earth = 201
		Fire = 202
		Water = 203
		Death = 204
		Life = 205
		Chaos = 206
		Order = 207
		Time = 208
		Energy = 209
		Knowledge = 210
	End Enum

	Public Enum ToolCategory 'Used by ItemType Class
		None
		Axe
		Pickaxe
		Basket
		Hammer
		WateringCan
	End Enum

	Public Enum ItemCategory 'Used by ItemType Class
		None
		Hide
		Cloth
		Leather
		Ingot
		WoodPlank
	End Enum

	Public Enum ItemTooltipCategory 'Used by ItemType Class

		None
		Armor
		Weapon
		Shield
		Trinket
		Consumable
		Food
		RawFood
		Reagent
		Gem
		ImbuedGem
		Currency
		Lockpick
		MountToken
		Poison
		CraftingRecipe
		CommonItem
		TamingTool
		Mat_RawHide
		Mat_Skin
		Mat_Hide
		Mat_Leather
		Mat_Fiber
		Mat_Fabric
		Mat_Ingot
		Mat_Board
		HerbalRemedy
		Bandages
		ProficiencyOrb
		AlchemicalOil
		DivineReward
		AspectExtract
		PrimalEnergy
		Potion

	End Enum

	Public Enum MarketPlaceSubFilter 'Used by ItemType Class
		Necklaces = 0
		Rings = 1
		ChainArmor = 2
		HideArmor = 3
		LeatherArmor = 4
		PlateArmor = 5
		RaggedClothes = 6
		ScholarClothes = 7
		Shields = 8
		Bandages = 9
		Foods = 10
		LoreTablets = 11
		OthersConsumables = 12
		Unguents = 13
		AnimalHeads = 14
		Boards = 15
		Cloths = 16
		Fibers = 17
		RawGems = 18
		Ingots = 19
		Leathers = 20
		OthersMaterialsLight = 21
		ProcessedHides = 22
		RawHides = 23
		FoodSacks = 24
		Ores = 25
		Stones = 26
		WoodLogs = 27
		Axes = 28
		Clubs = 29
		Daggers = 30
		Hammers = 31
		Halberds = 32
		Maces = 33
		Quarterstaffs = 34
		Spears = 35
		Swords = 36
		Bows = 37
		MageStaffs = 38
		Horses = 39
		Reagent_Mind = 40
		Reagent_Body = 41
		Reagent_Soul = 42
		Reagent_Create = 43
		Reagent_Destroy = 44
		Reagent_Negate = 45
		Reagent_Transform = 46
		Reagent_Transfer = 47
		Reagent_Air = 48
		Reagent_Earth = 49
		Reagent_Fire = 50
		Reagent_Water = 51
		Reagent_Death = 52
		Reagent_Life = 53
		Reagent_Chaos = 54
		Reagent_Order = 55
		Reagent_Time = 56
		Reagent_Energy = 57
		Reagent_Knowledge = 58
		Other = 59
		Planks = 60
		Ammunitions = 61
		ImbuedGem = 62
		Lockpick = 63
		CommonClothes = 64
		AssassinClothes = 65
		UnlockableRecipe = 66
		BattleMageArmor = 67
		ClericArmor = 68
		HunterArmor = 69
		KnightArmor = 70
		RangerArmor = 71
		RogueArmor = 72
		SlayerArmor = 73
		WarlockArmor = 74
		ProficiencyOrb = 75
		AlchemicalOil = 76
		DivineReward = 77
		None = -1

	End Enum

	Public Enum ReagentRarity 'Used by ItemType Class

		Weak
		Normal
		Strong
		Legendary
	End Enum

	Public Enum MonsterRace 'Used by MonsterClass Class

		None
		Beast
		Giant
		Human
		Humanoid
		Primordial
		Undead
		Reptilian
		Amphibian
		Arachnid
		Dragon
		Insect
		Reptile
		Tutorial
		Spirit

	End Enum

	Public Enum EquipItemEditorType 'Used by EquiType Class
		Deffault 'default term is protected
		Armor
	End Enum


	Public Enum ArmorPieceType 'Used by EquiType Class
		Main
		Secondary
	End Enum

	Public Enum EquipAbilityPowerType 'Used by EquiType Class
		None
		Physical
		Magical
		Healing
		All
	End Enum

	Public Enum EquipType 'Used by EquiType Class
		None = -1
		ClothMain
		ClothSecondary
		LightMain
		LightSecondary
		MediumMain
		MediumSecondary
		HeavyMain
		HeavySecondary
		Weapon
		MagicWeapon
		Shield
		MagicOrb
		Cape
		Belt
		Necklace
		Ring
		Ammunition
	End Enum

	Public Enum EquipmentSlot 'Used by EquiType Class

		None
		Chest
		Helmet
		Cape
		Hands
		Feet
		Belt
		Necklace
		Ring
		Ring_1
		MainHand
		MainHand_1
		MainHand_2
		OffHand
		Mount
	End Enum


	Public Enum RecipeGroup 'Used by EnchantmentRecipe Class
		None
		Attribute
		Resistance
		DamageReflection
		DamageConversion
		OnHitEffect
		ProficiencyBonus
	End Enum

	Public Enum EnchantmentAmountType 'Used by EnchantmentRecipe Class
		PureValue
		Percentage
	End Enum

	Public Enum EquipEnchantClass 'Used by EnchantmentRecipe Class
		None
		Armor
		Weapon
		Shield
		Amulet
		Ring
	End Enum

	Public Enum GemFamily 'Used by GemItemType Class

		Amethys
		Diamond
		Emerald
		Ruby
		Sapphire
		Topaz
	End Enum

	Public Enum GemQuality 'Used by GemItemType Class

		Chipped
		Fine
		Flawless
	End Enum

	Public Enum EquipTier ' Used by FullSetArmorData class and CraftingMaterialInfo class

		Tier1
		Tier2
		Tier3
		Tier4
	End Enum

	Public Enum MaterialGroups ' Used by CraftingMaterialInfo class

		Hide
		Leather
		Cloth
		Metal
		Wood
	End Enum

	Public Enum EquipCraftingType ' Used by CraftingMaterialInfo class

		None = 0
		Armors = 1
		Shields = 2
		Weapons = 4

	End Enum

	Public Enum DamageType 'Used by DamageInformation Class

		None
		Slash
		Pierce
		Crush
		Fire
		Ice
		Shock
		Energy
		Poison
		Acid
		Pure

	End Enum

	Public Enum WeaponDamageModifier 'Used by DamageInformation Class

		None
		PhysicalPower
		MagicalPower
		HealingPower
	End Enum

	Public Enum WeaponType 'Used by WeaponItemType Class

		Dagger
		Axe
		Club
		Mace
		Spear
		Quarterstaff
		Bow
		Unarmed
		None
		Sword
		Crossbow
		Hammer
		Wand
		Halberd

	End Enum

	Public Enum WeaponClass 'Used by WeaponItemType Class

		Melee = 1
		Ranged = 2
		Mage = 4
		Unarmed = 8
		Shield = 16
	End Enum

	Public Enum WeaponAttackSpeed 'Used by WeaponItemType Class

		Slow
		Medium
		Fast
		VerySlow

	End Enum

	Public Enum WeaponDamageType 'Used by WeaponItemType Class and by WeaponItemType Class

		None
		LineDamage
		LineDamageFirstTarget
		ConeDamage
		ProjectileDamage
	End Enum

	Public Enum ArmorSetBonus 'Used by FullsetArmorData Class

		None
		Scholar
		Assassin
		Rogue
		Cleric
		Slayer
		Battlemage
		Ranger
		Knight
		Warlock
		Hunter

	End Enum

	Public Enum TechTreeEnchantClass 'Used by EquipItemType Class

		None
		Tailoring
		Witchcraft
		Blacksmithing
		Carpentry

	End Enum

	Public Enum LegendDifficulty 'Used by MonsterClass Class

		Resident
		Random
		Summoned
		Champion
	End Enum

	Public Enum POIType 'Used by MonsterClass Class

		MountainTrollCamp
		ForestTrollCamp
		SeaTrollDwelling
		HillGoblinsCamp
		RuinsOfOlissa
		ArenaOfOlissa
		RuinsOfFairfront
		RuinsOfHollowgrove
		TempleOfHeartwood
		AltarOfTyros
		ChurchOfTyros
		SacredHill
		ShadowGoblinsCamp
		RuinsOfEastmere
		BonePit
		WargPit
		BanditsCamp
		BanditsFortress
		BanditsTower
		OgreVillage
		FireElementalLaboratory
		FireElemementalPrison
		IceElementalLaboratory
		IceElemementalPrison
		StormElementalLaboratory
		StormElemementalPrison
		CrystalElementalLaboratory
		CrystalElemementalPrison
		ShipwreckCamp
		BuccaneerRest
		JunkPile
		ImprovisedArena
		MoonBayCamp
		RuinedGardens
		LargeBanditCamp
		WickerTotem
		TradersMaze
		ReclaimedRuins
		PlatformsRamp
		Belvedere
		GrokotonDistrict
		GrokotonHamlet
		TermidianNest
		TermidianOutpost
		TermidianNursery
		GuardedBridge
		InfestedDump
		OgreLivingQuarters
		OgreTrainingGrounds
		CreepingGardens
		JotunnOutpost
		JotunnVillage
		JotunnCitadel
		InfestedGraveyard
		MountainDragonNest
		EmberDragonNest
		ArborealDragonNest
		RimeDragonNest
		OgreFacility
		OgreSuburb
		OgreBarracks

	End Enum

	Public Enum UnlockableIDs 'Used by KnowledgeUnlockableValue Class


		str = 1
		dex = 2
		_int = 3
		con = 4
		per = 5
		cha = 6
		endurance = 7
		mana = 8
		accuracy = 9
		fortitude = 10
		evasion = 11
		willpower = 12
		armorPierce = 13
		armorSlash = 14
		armorCrush = 15
		resistanceFire = 16
		resistanceIce = 17
		resistanceShock = 18
		resistanceMagic = 19
		resistancePoison = 20
		resistanceAcid = 21
		stealth = 22
		detection = 23
		spellDamageIncrease = 24
		cooldownReduction = 25
		healthRegen = 26
		manaRegen = 27
		luck = 28
		critChance = 29
		critDamage = 30
		detectionRange = 31
		specialTrait_0 = 1000
		specialTrait_1 = 1001
		specialTrait_2 = 1002
		specialTrait_3 = 1003
		specialTrait_4 = 1004
		specialTrait_5 = 1005
		specialTrait_6 = 1006
		specialTrait_7 = 1007
		specialTrait_8 = 1008
		specialTrait_9 = 1009
		monsterAttack_0 = 2000
		monsterAttack_1 = 2001
		monsterAttack_2 = 2002
		monsterAttack_3 = 2003
		monsterAttack_4 = 2004
		monsterAttack_5 = 2005
		monsterAttack_6 = 2006
		monsterAttack_7 = 2007
		monsterAttack_8 = 2008
		monsterAttack_9 = 2009
		monsterSpell_0 = 3000
		monsterSpell_1 = 3001
		monsterSpell_2 = 3002
		monsterSpell_3 = 3003
		monsterSpell_4 = 3004
		monsterSpell_5 = 3005
		monsterSpell_6 = 3006
		monsterSpell_7 = 3007
		monsterSpell_8 = 3008
		monsterSpell_9 = 3009
	End Enum

	Public Enum SpecialMonsterImmunity 'Used by MonsterClass Class

		None = 0
		SpiderWeb = 1
		Incorporeal = 2
	End Enum

	Public Enum PlayerAttitude 'Used by MonsterClass Class

		Aggressive
		Neutral
		Fleeing
		WanderingNPC
	End Enum

	Public Enum CombatStance 'Used by MonsterClass Class

		Fighter
		KeepDistance
		Assassin
	End Enum

	Public Enum Category 'Used by BookOfKnowledgeEntry Class

		None
		Monster
		Tree
		Bush
		PickablePlant
		Mineral
	End Enum

	Public Enum MonsterCastingRestrictionType 'Used by MonsterCastingRestriction Class

		CasterHealthPercAbove
		CasterHealthPercBelow
		TargetHealthPercAbove
		TargetHealthPercBelow
		TargetDistanceAbove
		TargetDistanceBelow
		CasterHasStatus
		CasterDoesNotHaveStatus
		TargetHasStatus
		TargetDoesNotHaveStatus
		CasterHasStatusType
		TargetHasStatusType

	End Enum

End Namespace
