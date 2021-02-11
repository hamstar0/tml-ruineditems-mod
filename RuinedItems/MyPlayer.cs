using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Players;
using RuinedItems.Prefixes;
using RuinedItems.Items;


namespace RuinedItems {
	partial class RuinedItemsPlayer : ModPlayer {
		public override void PostBuyItem( NPC vendor, Item[] shopInventory, Item item ) {
			if( !RuinedPrefix.IsItemRuinable(item) ) {
				return;
			}

			var config = RuinedItemsConfig.Instance;
			float purchaseRuinChance = config.Get<float>( nameof(config.PurchasedItemRuinPercentChance) );

			if( purchaseRuinChance <= 0f ) {
				return;
			}

			if( Main.rand.NextFloat() < purchaseRuinChance ) {
				item.Prefix( ModContent.PrefixType<RuinedPrefix>() );
			}
		}


		////////////////

		public override void PreUpdate() {
			if( Main.netMode != NetmodeID.Server ) {
				if( this.player.whoAmI == Main.myPlayer ) {
					this.UpdateLocal();
				} else {
					this.UpdateClient();
				}
			} else {
				this.UpdateServer();
			}
		}

		private void UpdateLocal() {
			this.BlockEquipsIf();

			MagitechScrapItem.UpdatePickerMode();
		}

		private void UpdateClient() {
			//this.BlockEquipsIf();
		}

		private void UpdateServer() {
			//this.BlockEquipsIf();
		}

		////

		public override void UpdateEquips( ref bool wallSpeedBuff, ref bool tileSpeedBuff, ref bool tileRangeBuff ) {
			ModContent.GetInstance<RuinedPrefix>().UpdateRuinedAccessoriesForPlayer( this.player );
		}


		////////////////

		private void BlockEquipsIf() {
			var config = RuinedItemsConfig.Instance;
			if( !config.Get<bool>( nameof(config.RuinedItemsLockedFromUse) ) ) {
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
