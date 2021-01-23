using System;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Players;


namespace RuinedItems.Prefixes {
	public partial class RuinedPrefix : ModPrefix {
		public static bool IsItemRuinable( Item item, bool asWildRoll ) {
			if( item?.active != true || item.maxStack != 1 ) {
				return false;
			}
			if( !item.accessory && !item.melee && !item.ranged && !item.magic && !item.summon ) {
				return false;
			}

			if( asWildRoll ) {
				var config = RuinedItemsConfig.Instance;
				return config.Get<float>( nameof(config.PrefixRollChance) ) > 0f;
			}
			
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
			return config.Get<float>( nameof(config.PrefixRollChance) );
			//RuinedItemsConfig.Instance.Get<float>( nameof(RuinedItemsConfig.PrefixRollChance) );
		}


		public override bool CanRoll( Item item ) {
			var config = RuinedItemsConfig.Instance;
			return RuinedPrefix.IsItemRuinable( item, true )
				&& config.Get<float>( nameof(config.PrefixRollChance) ) > 0f;
			//&& RuinedItemsConfig.Instance.Get<float>( nameof(RuinedItemsConfig.PrefixRollChance) ) > 0f;
		}


		////////////////

		internal void UpdateRuinedAccessoriesForPlayer( Player player ) {
			for( int i = PlayerItemHelpers.VanillaAccessorySlotFirst; PlayerItemHelpers.IsAccessorySlot( player, i ); i++ ) {
				Item accItem = player.armor[i];
				if( accItem?.active != true || accItem.prefix != this.Type ) {
					continue;
				}

				this.ApplyAccessoryForPlayer( player );
			}
		}
	}
}
