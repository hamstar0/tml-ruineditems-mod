using System;
using Terraria;
using Terraria.ModLoader;


namespace RuinedItems.Prefixes {
	public partial class RuinedPrefix : ModPrefix {
		private void LoadOn() {
			On.Terraria.Item.Prefix += ( On.Terraria.Item.orig_Prefix orig, Item item, int pre ) => {
				bool ret = orig( item, pre );

				if( item.prefix == this.Type ) {
					// Apply final tweak to item
					item.rare = -1;
				}

				return ret;
			};
		}
	}
}
