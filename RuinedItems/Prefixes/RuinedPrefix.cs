using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsGeneral.Libraries.Players;


namespace RuinedItems.Prefixes {
	public partial class RuinedPrefix : ModPrefix {
		public static bool IsItemRuinable( Item item ) {
			if( item?.active != true || item.maxStack != 1 ) {
				return false;
			}
			if( !item.accessory && !item.melee && !item.ranged && !item.magic && !item.summon ) {
				return false;
			}
			if( item.rare == 0 || item.rare == -1 /*|| item.rare == 1*/ ) {
				return false;
			}

			var config = RuinedItemsConfig.Instance;
			string uid = ItemID.GetUniqueKey( item.type );
			var blacklistedItemUids = config.Get<HashSet<string>>( nameof(config.CannotRuinItems) );

			if( blacklistedItemUids.Contains(uid) ) {
				return false;
			}

			/*if( asWildRoll ) {
				var config = RuinedItemsConfig.Instance;
				return config.Get<float>( nameof(config.GeneralRuinRollChance) ) > 0f;
			}*/
			
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
		
		public override float RollChance( Item item ) {
			var config = RuinedItemsConfig.Instance;
			return config.Get<float>( nameof(config.GeneralRuinRollChance) );
		}


		public override bool CanRoll( Item item ) {
			return RuinedPrefix.IsItemRuinable( item );
		}


		////////////////

		internal void UpdateRuinedAccessoriesForPlayer( Player player ) {
			for( int i = PlayerItemLibraries.VanillaAccessorySlotFirst; PlayerItemLibraries.IsAccessorySlot(player, i); i++ ) {
				Item accItem = player.armor[i];
				if( accItem?.active != true || accItem.prefix != ModContent.PrefixType<RuinedPrefix>() ) {
					continue;
				}

				this.ConveyRuinedAccessoryStatsToPlayer( player );
			}
		}
	}
}
