using System;
using Terraria;
using Terraria.ID;


namespace RuinedItems.Libraries {
	public class PlayerItemHelpers {
		/// <summary>
		/// Drops a given armor item to the ground.
		/// Source: Mod Helpers
		/// </summary>
		/// <param name="player"></param>
		/// <param name="slot"></param>
		/// <param name="noGrabDelay"></param>
		public static void DropEquippedArmorItem( Player player, int slot, int noGrabDelay = 100 ) {
			Item item = player.armor[slot];
			if( item == null || item.IsAir ) { return; }

			player.armor[slot] = new Item();

			int itemIdx = Item.NewItem( player.position, item.width, item.height, item.type, item.stack, false, item.prefix, false, false );

			item.position = Main.item[itemIdx].position;
			item.noGrabDelay = noGrabDelay;
			Main.item[itemIdx] = item;

			if( Main.netMode != NetmodeID.SinglePlayer ) {
				float delay = noGrabDelay > 0
					? 0f
					: 1f;

				NetMessage.SendData( MessageID.SyncItem, -1, -1, null, itemIdx, delay, 0f, 0f, 0, 0, 0 );
				//if( Main.netMode == NetmodeID.MultiplayerClient ) {
				//	ItemNoGrabProtocol.SendToServer( itemIdx, noGrabDelay );
				//}
			}
		}

		/// <summary>
		/// Drops a given misc item to the ground.
		/// Source: Mod Helpers
		/// </summary>
		/// <param name="player"></param>
		/// <param name="slot"></param>
		/// <param name="noGrabDelay"></param>
		public static void DropEquippedMiscItem( Player player, int slot, int noGrabDelay = 100 ) {
			Item item = player.miscEquips[slot];
			if( item == null || item.IsAir ) { return; }

			player.miscEquips[slot] = new Item();

			int itemIdx = Item.NewItem( player.position, item.width, item.height, item.type, item.stack, false, item.prefix, false, false );

			item.position = Main.item[itemIdx].position;
			item.noGrabDelay = noGrabDelay;
			Main.item[itemIdx] = item;

			if( Main.netMode != NetmodeID.SinglePlayer ) {
				float delay = noGrabDelay > 0
					? 0f
					: 1f;

				NetMessage.SendData( MessageID.SyncItem, -1, -1, null, itemIdx, delay, 0f, 0f, 0, 0, 0 );
				//if( Main.netMode == NetmodeID.MultiplayerClient ) {
				//	ItemNoGrabProtocol.SendToServer( itemIdx, noGrabDelay );
				//}
			}
		}
	}
}
