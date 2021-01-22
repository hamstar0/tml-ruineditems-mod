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
	}
}
