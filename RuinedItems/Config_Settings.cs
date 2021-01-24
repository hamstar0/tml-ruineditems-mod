using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;


namespace RuinedItems {
	public partial class RuinedItemsConfig : ModConfig {
		public bool RuinedItemsLockedFromUse { get; set; } = false;


		[Range( 0f, 10f )]
		[DefaultValue( 0f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float PrefixRollChance { get; set; } = 0f;


		[DefaultValue( 0.35f )]
		public float ReforgeRuinChance { get; set; } = 0.35f;

		[DefaultValue( 4f / 5f )]
		public float ReforgeComboRuinChance { get; set; } = 4f / 5f;


		[DefaultValue( 1f )]
		public float WorldGenChestItemRuinChance { get; set; } = 1f;

		[DefaultValue( 1f )]
		[ReloadRequired]
		public float NPCLootItemRuinChance { get; set; } = 1f;


		[Range( 0f, 1f )]
		[DefaultValue( 1f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float PurchasedItemRuinChance { get; set; } = 1f;
	}
}
