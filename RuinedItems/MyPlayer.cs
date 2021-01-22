using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using RuinedItems.Prefixes;
using RuinedItems.Libraries;


namespace RuinedItems {
	class RuinedItemsPlayer : ModPlayer {
		public override void PreUpdate() {
			if( Main.netMode != NetmodeID.Server ) {
				if( this.player.whoAmI != Main.myPlayer ) { return; }
			}

			this.BlockEquipsIfDisabled();
		}


		////////////////

		private void BlockEquipsIfDisabled() {
			for( int i = 0; i < this.player.armor.Length; i++ ) {
				Item item = this.player.armor[i];
				if( item == null || item.IsAir ) { continue; }

				if( item.prefix == ModContent.PrefixType<RuinedPrefix>() ) {
					PlayerItemHelpers.DropEquippedArmorItem( player, i );
				}
			}

			for( int i = 0; i < this.player.miscEquips.Length; i++ ) {
				Item item = this.player.armor[i];
				if( item == null || item.IsAir ) { continue; }

				if( item.prefix == ModContent.PrefixType<RuinedPrefix>() ) {
					PlayerItemHelpers.DropEquippedMiscItem( player, i );
				}
			}
		}
	}
}
