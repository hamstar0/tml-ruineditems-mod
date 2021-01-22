using System;
using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using HamstarHelpers.Classes.UI.ModConfig;


namespace RuinedItems {
	class MyFloatInputElement : FloatInputElement { }




	public partial class RuinedItemsConfig : ModConfig {
		public static RuinedItemsConfig Instance => ModContent.GetInstance<RuinedItemsConfig>();



		////////////////

		public override ConfigScope Mode => ConfigScope.ServerSide;



		////////////////

		[Range( 0f, 10f )]
		[DefaultValue( 0f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float PrefixRollChance { get; set; } = 0f;

		[Range( 0f, 10f )]
		[DefaultValue( 2f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float PrefixValueScale { get; set; } = 2f;


		[DefaultValue( 0.75f )]
		public float ReforgeRuinChance { get; set; } = 0.75f;

		[DefaultValue( 1f )]
		public float WorldGenChestItemRuinChance { get; set; } = 1f;

		[DefaultValue( 1f )]
		[ReloadRequired]
		public float NPCLootItemRuinChance { get; set; } = 1f;


		public bool RuinedItemsLockedOnly { get; set; } = false;


		[Range( 0f, 10f )]
		[DefaultValue( 2f /3f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float RuinedDamageScale { get; set; } = 2f / 3f;

		[Range( 0f, 10f )]
		[DefaultValue( 2f / 3f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float RuinedKnockbackScale { get; set; } = 2f / 3f;
		[Range( 0f, 10f )]
		[DefaultValue( 3f / 2f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float RuinedUseTimeScale { get; set; } = 3f / 2f;

		[Range( 0f, 10f )]
		[DefaultValue( 2f / 3f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float RuinedSizeScale { get; set; } = 2f / 3f;

		[Range( 0f, 10f )]
		[DefaultValue( 2f / 3f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float RuinedProjectileVelocityScale { get; set; } = 2f / 3f;

		[Range( 0f, 10f )]
		[DefaultValue( 3f / 2f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float RuinedManaUseScale { get; set; } = 3f / 2f;

		[Range( -100, 100 )]
		[DefaultValue( -10 )]
		public int RuinedCritAdd { get; set; } = -10;

		[Range( 0f, 10f )]
		[DefaultValue( 4f / 5f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float RuinedMoveSpeedScale { get; set; } = 4f / 5f;

		[Range( -1f, 1f )]
		[DefaultValue( -0.2f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float RuinedMeleeSpeedAdd { get; set; } = -0.2f;
	}
}
