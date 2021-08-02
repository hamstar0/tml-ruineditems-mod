using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace RuinedItems.Prefixes {
	public partial class RuinedPrefix : ModPrefix {
		private bool IsRecipeIngredientRuined( Recipe recipe )  {
			Player plr = Main.LocalPlayer;

			for( int i = 0; i < Recipe.maxRequirements; i++ ) {
				Item reqItem = recipe.requiredItem[i];
				if( reqItem.type == ItemID.None ) {
					break;
				}

				if( reqItem.maxStack != 1 && reqItem.stack != 1 ) {
					continue;
				}

				Item[] inv = plr.inventory;
				if( this.IsItemPoolItemRuined(recipe, reqItem, inv, inv.Length - 1) ) {
					return true;
				}

				if( plr.chest == -1 ) {
					continue;
				} else if( plr.chest > -1 ) {
					inv = Main.chest[ plr.chest ].item;
				} else if( plr.chest == -2 ) {
					inv = plr.bank.item;
				} else if( plr.chest == -3 ) {
					inv = plr.bank2.item;
				} else if( plr.chest == -4 ) {
					inv = plr.bank3.item;
				}

				if( this.IsItemPoolItemRuined(recipe, reqItem, inv, inv.Length) ) {
					return true;
				}
			}

			return false;
		}


		private bool IsItemPoolItemRuined( Recipe recipe, Item reqItem, Item[] pool, int maxPool ) {
			for( int j = 0; j < maxPool; j++ ) {
				Item ingItem = pool[j];
				bool isIngMatch = ingItem.IsTheSameAs( reqItem )
					|| recipe.useWood( ingItem.type, reqItem.type )
					|| recipe.useSand( ingItem.type, reqItem.type )
					|| recipe.useFragment( ingItem.type, reqItem.type )
					|| recipe.useIronBar( ingItem.type, reqItem.type )
					|| recipe.usePressurePlate( ingItem.type, reqItem.type )
					|| recipe.AcceptedByItemGroups( ingItem.type, reqItem.type );

				if( isIngMatch && ingItem.prefix == ModContent.PrefixType<RuinedPrefix>() ) {
					return true;
				}
			}

			return false;
		}
	}
}
