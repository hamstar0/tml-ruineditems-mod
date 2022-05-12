using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using RuinedItems.Prefixes;


namespace RuinedItems {
	partial class RuinedItemsItem : GlobalItem {
		public override void OnCraft( Item item, Recipe recipe ) {
			if( !RuinedPrefix.IsItemRuinable(item, out RuinFailCode code) ) {
				return;
			}

			//

			var config = RuinedItemsConfig.Instance;
			float craftRuinChance = config.Get<float>( nameof(config.CraftRuinPercentChance) );

			bool isRuined = Main.rand.NextFloat() <= craftRuinChance;

			if( isRuined ) {
				item.Prefix( ModContent.PrefixType<RuinedPrefix>() );

				if( craftRuinChance < 1f ) {
					Main.NewText( "Crafted item was ruined! Better luck next time.", Color.Yellow );
				}
			}
		}
	}
}
