using System;
using Terraria;
using Terraria.ModLoader;
using ModLibsGeneral.Libraries.Players;
using RuinedItems.Prefixes;


namespace RuinedItems {
	partial class RuinedItemsPlayer : ModPlayer {
		private void BlockEquipsIf() {
			var config = RuinedItemsConfig.Instance;
			if( !config.Get<bool>( nameof(config.RuinedItemsLockedFromUse) ) ) {
				return;
			}

			for( int i = 0; i < this.player.armor.Length; i++ ) {
				Item item = this.player.armor[i];
				if( item == null || item.IsAir ) { continue; }

				if( item.prefix == ModContent.PrefixType<RuinedPrefix>() ) {
					PlayerItemLibraries.DropEquippedArmorItem( player, i );
				}
			}

			for( int i = 0; i < this.player.miscEquips.Length; i++ ) {
				Item item = this.player.armor[i];
				if( item == null || item.IsAir ) { continue; }

				if( item.prefix == ModContent.PrefixType<RuinedPrefix>() ) {
					PlayerItemLibraries.DropEquippedMiscItem( player, i );
				}
			}
		}
	}
}
