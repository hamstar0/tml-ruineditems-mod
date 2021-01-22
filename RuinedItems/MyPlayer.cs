using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Players;
using RuinedItems.Prefixes;


namespace RuinedItems {
	partial class RuinedItemsPlayer : ModPlayer {
		public override void PreUpdate() {
			if( Main.netMode != NetmodeID.Server ) {
				if( this.player.whoAmI != Main.myPlayer ) {
					return;
				}
			}

			this.BlockEquipsIf();
		}

		////

		public override void UpdateEquips( ref bool wallSpeedBuff, ref bool tileSpeedBuff, ref bool tileRangeBuff ) {
			ModContent.GetInstance<RuinedPrefix>().UpdateRuinedAccessoriesForPlayer( this.player );
		}


		////////////////

		private void BlockEquipsIf() {
			var config = RuinedItemsConfig.Instance;
			if( !config.Get<bool>( nameof(config.RuinedItemsLockedOnly) ) ) {
				return;
			}

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
