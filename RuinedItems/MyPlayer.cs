using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
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
		}

		private void UpdateClient() {
			//this.BlockEquipsIf();
		}

		private void UpdateServer() {
			//this.BlockEquipsIf();
		}

		////

		public override void UpdateEquips( ref bool wallSpeedBuff, ref bool tileSpeedBuff, ref bool tileRangeBuff ) {
			ModContent.GetInstance<RuinedPrefix>()
				.UpdateRuinedAccessoriesForPlayer( this.player );
		}

		////

		public override void PostUpdate() {
			if( this.player.whoAmI != Main.myPlayer ) { return; }

			MagitechScrapItem_Mods.UpdateRepairInteractionsIf( this.player );
		}

		public override void UpdateAutopause() {
			if( !Main.gamePaused ) { return; }
			if( this.player.whoAmI != Main.myPlayer ) { return; }

			MagitechScrapItem_Mods.UpdateRepairInteractionsIf( this.player );
		}
	}
}
