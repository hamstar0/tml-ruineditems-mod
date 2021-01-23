using System;
using Terraria;
using Terraria.ModLoader;


namespace RuinedItems.Prefixes {
	public partial class RuinedPrefix : ModPrefix {
		internal static bool HookPostPrefix( On.Terraria.Item.orig_Prefix orig, Item item, int pre ) {
			bool ret = orig( item, pre );

			if( item.prefix == ModContent.PrefixType<RuinedPrefix>() ) {
				// Apply final tweak to item
				item.rare = -1;
			}

			return ret;
		}



		////////////////

		private void LoadOn() {
			On.Terraria.Item.Prefix += RuinedPrefix.HookPostPrefix;
		}
	}
}
