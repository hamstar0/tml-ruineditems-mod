using System;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using RuinedItems.Prefixes;


namespace RuinedItems {
	class RuinedItemsWorld : ModWorld {
		public override void PostWorldGen() {
			var config = RuinedItemsConfig.Instance;
			byte ruinedPrefix = ModContent.PrefixType<RuinedPrefix>();

			for( int i=0; i<Main.chest.Length; i++ ) {
				Item[] items = Main.chest[i]?.item;
				if( items == null ) {
					continue;
				}

				for( int j=0; j<items.Length; j++ ) {
					Item item = items[j];
					if( item?.active != true || item.IsAir ) {
						continue;
					}

					if( WorldGen.genRand.NextFloat() > config.Get<float>( nameof(config.WorldGenChestItemRuinPercentChance) ) ) {
						continue;
					}

					if( !RuinedPrefix.IsItemRuinable(item, out _) ) {
						continue;
					}

					item.Prefix( ruinedPrefix );
				}
			}
		}
	}
}
