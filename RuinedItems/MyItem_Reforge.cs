using System;
using Microsoft.Xna.Framework;
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
				var myitem = item.GetGlobalItem<RuinedItemsItem>();

				if( this.RuinReforgeIf(item, RuinedItemsItem.IsCurrentPreReforgeItemRuined) ) {
					this.RuinReforge( item );
				}

				myitem.IsScrapUsedUpon = false;
			}
		}

		////

		private bool RuinReforgeIf( Item item, bool wasRuined ) {
			var config = RuinedItemsConfig.Instance;

			if( wasRuined ) {
				float comboChance = config.Get<float>( nameof(config.ReforgeComboRuinPercentChance) );

				if( Main.rand.NextFloat() > comboChance ) {
					if( comboChance > 0f ) {
						Main.NewText( "Ruined items resist reforging. Try again.", Color.Yellow );
					}
					return false;
				}
			} else {
				float ruinChance = config.Get<float>( nameof(config.ReforgeRuinPercentChance) );

				if( Main.rand.NextFloat() > ruinChance ) {
					return false;
				}
			}

			return true;
		}


		private bool RuinReforge( Item item ) {
			item.SetDefaults( item.type );

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
