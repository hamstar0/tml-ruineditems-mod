﻿using System;
using Terraria;
using Terraria.ModLoader;
using RuinedItems.Prefixes;


namespace RuinedItems {
	partial class RuinedItemsItem : GlobalItem {
		private static bool IsCurrentPreReforgeItemRuined = false;



		////////////////

		public override bool NewPreReforge( Item item ) {
			RuinedItemsItem.IsCurrentPreReforgeItemRuined = item.prefix == ModContent.PrefixType<RuinedPrefix>();

			return base.NewPreReforge( item );
		}


		public override void PostReforge( Item item ) {
			if( RuinedPrefix.IsItemRuinable(item) ) {
				this.RuinReforgeIf( item );
				this.IsScrapUsedUpon = false;
			}
		}

		private bool RuinReforgeIf( Item item ) {
			var config = RuinedItemsConfig.Instance;

			if( RuinedItemsItem.IsCurrentPreReforgeItemRuined ) {
				if( Main.rand.NextFloat() > config.Get<float>( nameof(config.ReforgeComboRuinPercentChance) ) ) {
					return false;
				}
			} else if( Main.rand.NextFloat() > config.Get<float>( nameof(config.ReforgeRuinPercentChance) ) ) {
				return false;
			}

			if( item.accessory ) {
				var resetItem = new Item();
				resetItem.SetDefaults( item.type, true );

				item.rare = resetItem.rare;
				item.defense = 0;
				item.crit = 0;
			}

			item.Prefix( ModContent.PrefixType<RuinedPrefix>() );

			/*if( Main.netMode == NetmodeID.MultiplayerClient ) {
				int itemWho = Array.FindIndex( Main.item, i => i == item );
				if( itemWho != -1 ) {
					NetMessage.SendData( MessageID.SyncItem, -1, -1, null, itemWho );
				}
			}*/
			return true;
		}
	}
}
