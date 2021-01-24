using System;
using Terraria;
using Terraria.ModLoader;
using RuinedItems.Prefixes;


namespace RuinedItems {
	partial class RuinedItemsItem : GlobalItem {
		public override void OnCraft( Item item, Recipe recipe ) {
			if( !RuinedPrefix.IsItemRuinable(item, false) ) {
				return;
			}

			var config = RuinedItemsConfig.Instance;

			if( Main.rand.NextFloat() > config.Get<float>(nameof(config.CraftRuinPercentChance)) ) {
				return;
			}

			item.Prefix( ModContent.PrefixType<RuinedPrefix>() );
		}
	}
}
