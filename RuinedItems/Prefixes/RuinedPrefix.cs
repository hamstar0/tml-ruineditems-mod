using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsGeneral.Libraries.Players;


namespace RuinedItems.Prefixes {
	public enum RuinFailCode {
		None = 0,
		InvalidItem = 1,
		Stackable = 2,
		CannotPrefix = 4,
		UnacceptableRarity = 8,
		Blacklisted = 16
	}




	public partial class RuinedPrefix : ModPrefix {
		public static bool IsItemRuinable( Item item, out RuinFailCode resultCode ) {
			if( !item.active || item.IsAir ) {
				resultCode = RuinFailCode.InvalidItem;
				return false;
			}
			if( item.maxStack != 1 ) {
				resultCode = RuinFailCode.Stackable;
				return false;
			}
			if( !item.accessory && !item.melee && !item.ranged && !item.magic && !item.summon ) {
				resultCode = RuinFailCode.CannotPrefix;
				return false;
			}

			if( item.rare == 0 || item.rare == -1  ) {	//|| item.rare == 1
				resultCode = RuinFailCode.UnacceptableRarity;
				return false;
			}

			var config = RuinedItemsConfig.Instance;
			string uid = ItemID.GetUniqueKey( item.type );
			var blacklistedItemUids = config.Get<HashSet<string>>( nameof(config.CannotRuinItems) );

			if( blacklistedItemUids.Contains(uid) ) {
				resultCode = RuinFailCode.Blacklisted;
				return false;
			}

			/*if( asWildRoll ) {
				var config = RuinedItemsConfig.Instance;
				return config.Get<float>( nameof(config.GeneralRuinRollChance) ) > 0f;
			}*/

			resultCode = 0;
			return true;
		}



		////////////////

		public override bool Autoload( ref string name ) {
			name = "Ruined";
			this.LoadOn();
			return base.Autoload( ref name );
		}


		////////////////

		public override void ModifyValue( ref float valueMult ) {
			var config = RuinedItemsConfig.Instance;
			valueMult *= config.Get<float>( nameof(config.PrefixValueScale) );
		}


		////////////////
		
		/*public override float RollChance( Item item ) {
			var config = RuinedItemsConfig.Instance;
			float chance = config.Get<float>( nameof(config.GeneralRuinRollChance) );
			return chance;
		}*/		// <-Handled more consistently in GlobalItem.ChoosePrefix?


		public override bool CanRoll( Item item ) {
			return RuinedPrefix.IsItemRuinable( item, out _ );
		}


		////////////////

		internal void UpdateRuinedAccessoriesForPlayer( Player player ) {
			for( int i = PlayerItemLibraries.VanillaAccessorySlotFirst; PlayerItemLibraries.IsAccessorySlot(player, i); i++ ) {
				Item accItem = player.armor[i];
				if( accItem?.active != true || accItem.IsAir ) {
					continue;
				}
				if( accItem.prefix != ModContent.PrefixType<RuinedPrefix>() ) {
					continue;
				}

				this.ConveyRuinedAccessoryStatsToPlayer( player );
			}
		}
	}
}
