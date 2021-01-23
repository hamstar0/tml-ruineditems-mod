using System;
using Terraria;
using Terraria.ModLoader;
using RuinedItems.Prefixes;


namespace RuinedItems {
	partial class RuinedItemsItem : GlobalItem {
		public override void PostReforge( Item item ) {
			if( !RuinedPrefix.IsItemRuinable(item, false) ) {
				return;
			}

			var config = RuinedItemsConfig.Instance;
			if( Main.rand.NextFloat() > config.Get<float>( nameof(config.ReforgeRuinChance) ) ) {
				return;
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
		}
	}
}
