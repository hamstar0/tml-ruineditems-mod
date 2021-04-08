using System;
using System.ComponentModel;
using Terraria.ID;
using Terraria.ModLoader.Config;


namespace RuinedItems {
	public partial class RuinedItemsConfig : ModConfig {
		[DefaultValue( true )]
		public bool RuinedItemsLockedFromUse { get; set; } = true;

		//

		[Range( 0f, 10f )]
		[DefaultValue( 0f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float GeneralRuinRollChance { get; set; } = 0f;


		[DefaultValue( 0f )]
		public float CraftRuinPercentChance { get; set; } = 0f;


		[DefaultValue( 0.1f )]
		public float ReforgeRuinPercentChance { get; set; } = 0.1f;

		[DefaultValue( 4f / 5f )]
		public float ReforgeComboRuinPercentChance { get; set; } = 4f / 5f;


		[DefaultValue( 4f / 5f )]
		public float WorldGenChestItemRuinPercentChance { get; set; } = 4f / 5f;

		[DefaultValue( 4f / 5f )]
		[ReloadRequired]
		public float NPCLootItemRuinPercentChance { get; set; } = 4f / 5f;


		[Range( 0f, 1f )]
		[DefaultValue( 0f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float PurchasedItemRuinPercentChance { get; set; } = 0f;

		//

		[Range( 0, 999999999 )]
		[DefaultValue( 5000 )]
		public int MagitechScrapPrice { get; set; } = 5000;

		[Range( 0f, 1f )]
		[DefaultValue( 1f / 3f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float MagitechScrapRepairChance { get; set; } = 1f / 3f;

		public NPCDefinition MagitechScrapSoldByWhom { get; set; } = new NPCDefinition( NPCID.GoblinTinkerer );
	}
}
