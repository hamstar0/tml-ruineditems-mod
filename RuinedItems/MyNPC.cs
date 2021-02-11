using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using HamstarHelpers.Helpers.Debug;
using RuinedItems.Items;


namespace RuinedItems {
	partial class RuinedItemsNPC : GlobalNPC {
		public override void SetupShop( int type, Chest shop, ref int nextSlot ) {
			var config = RuinedItemsConfig.Instance;
			NPCDefinition npcDef = config.Get<NPCDefinition>( nameof( config.MagitechScrapSoldByWhom ) );

			if( npcDef?.Type == type ) {
				var scrapItem = new Item();
				scrapItem.SetDefaults( ModContent.ItemType<MagitechScrapItem>() );

				shop.item[nextSlot++] = scrapItem;
			}
		}
	}
}
