using System;
using Terraria;
using Terraria.ModLoader;


namespace RuinedItems.Prefixes {
	public class RuinedPrefix : ModPrefix {
		public override bool Autoload( ref string name ) {
			name = "Ruined";
			return mod.Properties.Autoload;
		}


		////////////////

		public override void ModifyValue( ref float valueMult ) {
			valueMult *= RuinedItemsConfig.Instance.PrefixValueScale;
		}


		////////////////

		public override float RollChance( Item item ) =>
			RuinedItemsConfig.Instance.PrefixRollChance;
			//RuinedItemsConfig.Instance.Get<float>( nameof(RuinedItemsConfig.PrefixRollChance) );


		public override bool CanRoll( Item item ) => item.maxStack == 1
			&& ( item.accessory || item.melee || item.ranged || item.magic || item.summon )
			&& RuinedItemsConfig.Instance.PrefixRollChance > 0f;
			//&& RuinedItemsConfig.Instance.Get<float>( nameof(RuinedItemsConfig.PrefixRollChance) ) > 0f;
	}
}
