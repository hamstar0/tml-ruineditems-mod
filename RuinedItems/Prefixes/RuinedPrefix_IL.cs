using System;
using Terraria;
using Terraria.ModLoader;


namespace RuinedItems.Prefixes {
	public partial class RuinedPrefix : ModPrefix {
		private void LoadOn() {
			On.Terraria.Item.Prefix += ( On.Terraria.Item.orig_Prefix orig, Item item, int pre ) => {
				bool ret = orig.Invoke( item, pre );
				if( item.prefix == this.Type ) {
					this.PostApply( item );
				}
				return ret;
			};


			On.Terraria.Recipe.Create += ( On.Terraria.Recipe.orig_Create orig, Recipe recipe ) => {
				bool isResultRuined = this.IsRecipeIngredientRuined( recipe );
				Main.mouseItem.Prefix( this.Type );

				orig.Invoke( recipe );
			};
		}
	}
}
