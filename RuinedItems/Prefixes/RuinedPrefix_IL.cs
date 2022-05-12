using System;
using Terraria;
using Terraria.ModLoader;


namespace RuinedItems.Prefixes {
	public partial class RuinedPrefix : ModPrefix {
		private void LoadOn() {
			// Post process ruined items
			On.Terraria.Item.Prefix += ( On.Terraria.Item.orig_Prefix orig, Item item, int pre ) => {
				bool ret = orig.Invoke( item, pre );

				if( item.prefix == ModContent.PrefixType<RuinedPrefix>() ) {
					this.PostApply( item );
				}

				return ret;
			};

			// Ensure crafted items inherit ruined status from ingredients
			On.Terraria.Recipe.Create += ( On.Terraria.Recipe.orig_Create orig, Recipe recipe ) => {
				bool isResultRuined = this.IsRecipeIngredientRuined( recipe );

				orig.Invoke( recipe );

				if( isResultRuined ) {
					Main.mouseItem.Prefix( ModContent.PrefixType<RuinedPrefix>() );
				}
			};
		}
	}
}
