﻿using System;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
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
					if( item?.active != true ) {
						continue;
					}

					if( WorldGen.genRand.NextFloat() > config.Get<float>( nameof(config.WorldGenChestItemRuinChance) ) ) {
						continue;
					}

					if( RuinedPrefix.IsItemRuinable(item, false) ) {
						item.Prefix( ruinedPrefix );
					}
				}
			}
		}
	}
}
