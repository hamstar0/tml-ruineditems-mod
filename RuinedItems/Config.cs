using System;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using ModLibsCore.Classes.UI.ModConfig;


namespace RuinedItems {
	class MyFloatInputElement : FloatInputElement { }




	public partial class RuinedItemsConfig : ModConfig {
		public static RuinedItemsConfig Instance => ModContent.GetInstance<RuinedItemsConfig>();



		////////////////

		public override ConfigScope Mode => ConfigScope.ServerSide;


		////////////////

		public bool DebugMode { get; set; } = false;
	}
}
