using System;
using Terraria;
using Terraria.ModLoader;


namespace RuinedItems.Prefixes {
	public class RuinedPrefix : ModPrefix {
		public static bool IsItemRuinable( Item item ) {
			var config = RuinedItemsConfig.Instance;
			return item?.active == true && item.maxStack == 1
				&& ( item.accessory || item.melee || item.ranged || item.magic || item.summon )
				&& config.Get<float>( nameof(config.PrefixRollChance) ) > 0f;
		}



		////////////////

		public override bool Autoload( ref string name ) {
			name = "Ruined";
			return mod.Properties.Autoload;
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
			return RuinedPrefix.IsItemRuinable( item )
				&& config.Get<float>( nameof(config.PrefixRollChance) ) > 0f;
			//&& RuinedItemsConfig.Instance.Get<float>( nameof(RuinedItemsConfig.PrefixRollChance) ) > 0f;
		}
	}
}
