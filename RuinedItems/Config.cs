using System;
using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;


namespace RuinedItems {
	class RuinedItemsConfig : ModConfig {
		public static RuinedItemsConfig Instance => ModContent.GetInstance<RuinedItemsConfig>();



		////////////////

		public override ConfigScope Mode => ConfigScope.ServerSide;



		////////////////

		[DefaultValue( 0f )]
		public float PrefixRollChance { get; set; } = 0f;

		[DefaultValue( 0.25f )]
		public float PrefixValueScale { get; set; } = 0.25f;
	}
}
