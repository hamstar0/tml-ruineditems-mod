using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;


namespace RuinedItems {
	public partial class RuinedItemsConfig : ModConfig {
		[DefaultValue( true )]
		public bool RuinedItemsLockedFromUse { get; set; } = true;


		[Range( 0f, 10f )]
		[DefaultValue( 0f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float GeneralRuinRollChance { get; set; } = 0f;


		[DefaultValue( 0f )]
		public float CraftRuinPercentChance { get; set; } = 0f;


		[DefaultValue( 0.35f )]
		public float ReforgeRuinPercentChance { get; set; } = 0.35f;

		[DefaultValue( 4f / 5f )]
		public float ReforgeComboRuinPercentChance { get; set; } = 4f / 5f;


		[DefaultValue( 1f )]
		public float WorldGenChestItemRuinPercentChance { get; set; } = 1f;

		[DefaultValue( 1f )]
		[ReloadRequired]
		public float NPCLootItemRuinPercentChance { get; set; } = 1f;


		[Range( 0f, 1f )]
		[DefaultValue( 1f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float PurchasedItemRuinPercentChance { get; set; } = 1f;


		[Range( 0f, 1f )]
		[DefaultValue( 0.33f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float MagitechScrapRepairChance { get; set; } = 0.33f;
	}
}
