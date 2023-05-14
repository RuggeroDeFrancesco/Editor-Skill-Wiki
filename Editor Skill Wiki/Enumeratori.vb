Module Enumeratori

#Region "Spell Enumerators"



	Public Enum SpellSchool
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
	Public Enum ToggleGroup
		None
		Aura
		DamageProtection
		Skin
		Stance
		WeaponModifier
		MajorProtection
		Ward
		Invisibility
	End Enum

	Public Enum SpellType
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

	Public Enum ActivationType
		Standard
		Toggle
		Hold
		Ready
		Link
	End Enum

	Public Enum TargetEntityType
		None = 0
		Player = 1
		Enemy = 2
		Ally = 4
		Monster = 8
		Self = 16
	End Enum

	Public Enum TargetType
		Self
		Location
		Entity
		PlaceSpellEntityLocation
		PlaceSpellTotemLocation
	End Enum

	Public Enum tooltipActivationType
		None
		Targeted
		Toggle
		Ready
		Directional
		Self
	End Enum

	Public Enum tooltipTargetType
		None
		Terrain
		Enemy
		FrozenEnemy
		RootedEnemy
		Ally
	End Enum

	Public Enum SpellGroup
		None
		ReadyOnAttack
		TargetedOffensive
		TargetedBeneficial
		SelfOffensive
		SelfBeneficial
		Teleport
		ReadyOnDeath
	End Enum

	Public Enum EffectGroup
		None = 0
		MagicalBuff = 1
		Invisibility = 2
		ElementalConditions = 4
		PhysicalBuff = 8
		MagicalProtection = 16
		Snare = 32
		Disease = 64
		Illusion = 128
		MagicalDebuff = 256
		PhysicalDebuff = 512
		Poison = 1024
		SpecialEffect = 2048
	End Enum

	Public Enum SpellUnicityGroup
		None
		MagicalGlobe
		DamageProtection
		Totem
		WeaponModifier
		Link
		BattleStance
		Skin
		ReadyStrike
		Aura
	End Enum

	Public Enum ReadyTrigger
		None
		OnDeath
		OnAttack
		OnAttackReceived
	End Enum

	Public Enum FriendlyFire
		No
		Yes
		Irrelevant
	End Enum

	Public Enum SecureHit
		No
		Yes
	End Enum
	Public Enum dontRemoveStealth
		No
		Yes
	End Enum
	Public Enum avoidBlockControllerCheck
		No
		Yes
	End Enum

	Public Enum canBeCritical
		No
		Yes
		Irrelevant
	End Enum

	Public Enum irremovable
		No
		Yes
	End Enum

	Public Enum checkLos
		No
		Yes
	End Enum

	Public Enum pathfinding
		No
		Yes
		Irrelevant
	End Enum

	Public Enum Difficulty
		Easy
		Medium
		Hard
	End Enum

	Public Enum SpellTag
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
	End Enum

	Public Enum RequireStatusOnTarget
		None
		BurningHot
		Cold
		FreezingCold
		Hot
		Hungry
		Tired
		Encumbered
		Starving
		Exhausted
		Stunned
		Bleeding
		Frightened
		HeavilyEncumbered
		DeathMark
		Blinded
		Crippled
		Silenced
		StrikeWounds
		Poisoned
		Enraged
		Slowed
		Inspire
		Absorb
		Unconscious
		Paralyzed
		Snared_Web
		NecroticDisease
		ConcussiveStrike
		WildfireTotemBuff
		WildfireTotemDebuff
		Snared_Net
		VerdantRegrowth
		TotemDecayDebuff
		Hidden
		PavedRoadBuff
		MonsterProtection
		ArmorDebuff
		TamingTrap
		Corrosion
		Resolve
		CallToTenacity
		HomingAI
		EmpowerBuff
		Weakened
		UnholyStr
		DeathBargain
		Knockback
		Chilled
		Warm
		Frozen
		Burning
		Shocked
		Shocked_Paralysis
		LightningRush
		BattleJump
		Dazed
		Confused
		Magnetize
		Explosion
		Petrify
		Unhealable
		SnaredRoot
		Overpower
		RendArmor
		Lunge
		RamThrough
		PierceThrough
		ShadowDash
		Bloodlust
		ExecuteBuff
		ExposeVulnDebuff
		LifeDrain
		ManaDrain
		NimblenessBuff
		ViciousAttacksBuff
		TrueStrike
		ShieldsUp
		ChargeBuff
		FeintBuff
		DebilitatingStrikeDebuff
		CrystalShacklesDebuff
		BondOfAgonyDebuff
		CleaveArmorDebuff
		WizardClothesBuff
		AssassinClothesBuff
		RogueArmorSetBuff
		ClericArmorSetBuff
		SlayerArmorSetBuff
		BattlemageArmorSetBuff
		RangeArmorSetBuff
		KnightArmorSetBuff
		WarlockArmorSetBuff
		HunterArmorSetBuff
		DirtRoadBuff
		PlayerInvulnerability
		AuraOfDevotionBuff
		AuraOfDevotionDebuff
		ChallengeDebuff
		LifeLinkBuff
		InsectSwarmDebuff
		StoneSkinBuff
		Atrophied
		HunterMarkDebuff
		HunterMarkBuff
		ChadraPrimalForm
		ErwydraPrimalForm
		BlessedHumanPrimalForm
		Sated
		Rested
		CrushingAssault
	End Enum

	Public Enum SaveModifierType
		None
		Fortitude
		Willpower
		Evasion
	End Enum

	Public Enum SpellDataType
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
		TresholdDamageFlat
		BlockChancePercentage
		HealthRecoveredPercentage
	End Enum


	'Restrizione sull'arma

	'Public Enum ChannelingWeapon
	'	No
	'	Yes
	'End Enum

	Public Enum RestrictedWeaponDamage
		Any = 0
		Slash = 1
		Pierce = 2
		'Slash_Pierce = 3
		Crush = 4
		'Crush_Slash = 5
		'Crush_Pierce = 6
	End Enum

	Public Enum RestrictedArmorWeight
		Any = 0
		Light = 1
		Medium = 2
		Heavy = 4
		'Light_Medium = 3
		'Light_Heavy = 5
		'Medium_Heavy = 6
	End Enum

	'Public Enum RestrictedWeaponWeight
	'	Any = 0
	'	Light = 1
	'	Medium = 2
	'	Heavy = 4
	'	'Light_Medium = 3
	'	'Light_Heavy = 5
	'	'Medium_Heavy = 6
	'End Enum

	Public Enum SpellSpecialWeaponRestriction
		None
		BowRequired
		ShieldRequired
		Unarmed
	End Enum


	Public Enum RestrictedWeaponClass
		None
		TwoHanded
		SpellChanneling
		OneHanded
		'Any = 0
		'Melee = 1
		'Ranged = 2
		'Mage = 4
		'Unarmed = 8
		'Shield = 16
	End Enum

	'Public Enum ShieldRequired
	'	No
	'	Yes
	'End Enum

	Public Enum SpellCoefficientAttributeType
		None
		STR
		DEX
		COS
		PER
		INT
		CHA
	End Enum

#End Region

	Public Enum EnumLanguage
		English = 0
		German = 1
		French = 2
	End Enum

	Public Enum DamageType
		None = 0
		Slash = 1
		Pierce = 2
		Crush = 3
		Fire = 4
		Ice = 5
		Shock = 6
		Energy = 7
		Poison = 8
		Acid = 9
		Pure = 10
	End Enum

	Public Enum CharacterAttribute
		None = 0
		Strength = 1
		Dexterity = 2
		Intelligence = 3
		Constitution = 4
		Perception = 5
		Charisma = 6
	End Enum


	Public Enum ItemCategory
		None
		Hide
		Cloth
		Leather
		Ingot
		WoodPlank
	End Enum

	Public Enum ToolCategory
		None
		Axe
		Pickaxe
		Basket
		Hammer
		WateringCan
	End Enum

	Public Enum Aspects
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

	Public Function combinedEnumeratorName(enumType As Type, value As Integer) As String
		Dim listOfEnumerators As New List(Of Integer)
		Dim combinedName As String = ""
		Dim highestPower As Integer = 1

		If value = 0 Then
			Return [Enum].GetName(enumType, 0)
		End If

		While highestPower * 2 <= value
			highestPower = highestPower * 2
		End While
		Dim i As Integer = highestPower
		While True

			If value >= i Then
				listOfEnumerators.Add(i)
				value = value - i
			End If
			If i = 1 Then
				Exit While
			Else
				i = i / 2
			End If

		End While

		For Each enumeratore As Integer In listOfEnumerators
			combinedName &= [Enum].GetName(enumType, enumeratore) & ", "
		Next

		If combinedName <> "" Then
			combinedName = combinedName.Substring(0, combinedName.Count() - 2) 'tolgo il vbCrlf di troppo
		End If

		Return combinedName
	End Function

End Module
