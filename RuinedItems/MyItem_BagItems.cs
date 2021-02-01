using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.Network.Scraper;
using RuinedItems.Prefixes;


namespace RuinedItems {
	partial class RuinedItemsItem : GlobalItem {
		private void LoadBagProcessors() {
			On.Terraria.Player.openCrate += this.OpenCrateHook;
			On.Terraria.Player.openGoodieBag += this.OpenGoodieBagHook;
			On.Terraria.Player.openLockBox += this.OpenLockBoxHook;
			On.Terraria.Player.openPresent += this.OpenPresentHook;
			On.Terraria.Player.OpenBossBag += this.OpenBossBagHook;
		}


		////////////////

		private void OpenCrateHook( On.Terraria.Player.orig_openCrate orig, Player plr, int itemType ) {
			if( Main.netMode == NetmodeID.SinglePlayer ) {
				this.OpenBagHook_SP( (p) => orig.Invoke(p, itemType), plr );
			} else if( Main.netMode == NetmodeID.MultiplayerClient ) {
				this.OpenBagHook_CL( (p) => orig.Invoke(p, itemType), plr );
			}
		}
		
		private void OpenGoodieBagHook( On.Terraria.Player.orig_openGoodieBag orig, Player plr ) {
			if( Main.netMode == NetmodeID.SinglePlayer ) {
				this.OpenBagHook_SP( (p) => orig.Invoke(p), plr );
			} else if( Main.netMode == NetmodeID.MultiplayerClient ) {
				this.OpenBagHook_CL( (p) => orig.Invoke(p), plr );
			}
		}

		private void OpenLockBoxHook( On.Terraria.Player.orig_openLockBox orig, Player plr ) {
			if( Main.netMode == NetmodeID.SinglePlayer ) {
				this.OpenBagHook_SP( (p) => orig.Invoke(p), plr );
			} else if( Main.netMode == NetmodeID.MultiplayerClient ) {
				this.OpenBagHook_CL( (p) => orig.Invoke(p), plr );
			}
		}

		private void OpenPresentHook( On.Terraria.Player.orig_openPresent orig, Player plr ) {
			if( Main.netMode == NetmodeID.SinglePlayer ) {
				this.OpenBagHook_SP( (p) => orig.Invoke(p), plr );
			} else if( Main.netMode == NetmodeID.MultiplayerClient ) {
				this.OpenBagHook_CL( (p) => orig.Invoke(p), plr );
			}
		}

		private void OpenBossBagHook( On.Terraria.Player.orig_OpenBossBag orig, Player plr, int itemType ) {
			if( Main.netMode == NetmodeID.SinglePlayer ) {
				this.OpenBagHook_SP( (p) => orig.Invoke(p, itemType), plr );
			} else if( Main.netMode == NetmodeID.MultiplayerClient ) {
				this.OpenBagHook_CL( (p) => orig.Invoke(p, itemType), plr );
			}
		}


		////////////////

		private void OpenBagHook_SP( Action<Player> callback, Player plr ) {
			Item[] wldItemsSnapshot = Main.item.ToArray();
			Item[] plrInvSnapshot = plr.inventory.ToArray();

			callback.Invoke( plr );

			for( int i=0; i<wldItemsSnapshot.Length; i++ ) {
				if( Main.item[i]?.active == true && wldItemsSnapshot[i] != Main.item[i] ) {
					this.ProcessBagItem( plr, Main.item[i] );
				}
			}

			for( int i=0; i<plrInvSnapshot.Length; i++ ) {
				if( plr.inventory[i]?.active == true && plrInvSnapshot[i] != plr.inventory[i] ) {
					this.ProcessBagItem( plr, plr.inventory[i] );
				}
			}
		}

		private void OpenBagHook_CL( Action<Player> callback, Player plr ) {
			Action<ScrapedSentData> listener = ( d )=> {
				int itemWho = d.Number;
				Item item = Main.item[ itemWho ];
				if( item?.active != true ) {
					return;
				}

				this.ProcessBagItem( plr, item );
			};

			//

			Scraper.IsScrapingSentData = true;
			Scraper.AddSendDataListener( listener );

			callback.Invoke( plr );

			Scraper.IsScrapingSentData = false;
			Scraper.RemoveSendDataListener( listener );
		}


		////////////////

		private void ProcessBagItem( Player plr, Item item ) {
			if( RuinedPrefix.IsItemRuinable(item, false) ) {
				var config = RuinedItemsConfig.Instance;
				float ruinChance = config.Get<float>( nameof(config.NPCLootItemRuinPercentChance) );

				if( Main.rand.NextFloat() < ruinChance ) {
					item.Prefix( ModContent.PrefixType<RuinedPrefix>() );
				}
			}
		}
	}
}
