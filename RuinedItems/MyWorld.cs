using System;
using Terraria;
using Terraria.ModLoader;
using RuinedItems.Prefixes;


namespace RuinedItems {
	class RuinedItemsWorld : ModWorld {
		public override void PostWorldGen() {
			byte ruinedPrefix = ModContent.PrefixType<RuinedPrefix>();

			for( int i=0; i<Main.chest.Length; i++ ) {
				Item[] items = Main.chest[i]?.item;
				if( items == null ) { return; }

				for( int j=0; j<items.Length; j++ ) {
					if( RuinedPrefix.IsItemRuinable(items[j]) ) {
						items[j].prefix = ruinedPrefix;
					}
				}
			}
		}
	}
}
