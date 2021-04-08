using System;
using Terraria;
using Terraria.ModLoader;
using RuinedItems.Prefixes;


namespace RuinedItems {
	partial class RuinedItemsItem : GlobalItem {
		public override void OnCraft( Item item, Recipe recipe ) {
			if( !RuinedPrefix.IsItemRuinable(item) ) {
				return;
			}

			var config = RuinedItemsConfig.Instance;
			float craftRuinChance = config.Get<float>( nameof(config.CraftRuinPercentChance) );

			if( Main.rand.NextFloat() <= craftRuinChance ) {
				item.Prefix( ModContent.PrefixType<RuinedPrefix>() );
			}
		}
	}
}
