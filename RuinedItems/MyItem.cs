﻿using System;
using Terraria;
using Terraria.ModLoader;
using RuinedItems.Prefixes;


namespace RuinedItems {
	partial class RuinedItemsItem : GlobalItem {
		public override bool CanUseItem( Item item, Player player ) {
			if( item.prefix == ModContent.PrefixType<RuinedPrefix>() ) {
				return false;
			}

			return base.CanUseItem( item, player );
		}


		public override bool CanRightClick( Item item ) {
			if( item.prefix == ModContent.PrefixType<RuinedPrefix>() ) {
				return false;
			}
			return base.CanRightClick( item );
		}


		public override bool AltFunctionUse( Item item, Player player ) {
			if( item.prefix == ModContent.PrefixType<RuinedPrefix>() ) {
				return false;
			}
			return base.AltFunctionUse( item, player );
		}


		public override bool ConsumeItem( Item item, Player player ) {
			if( item.prefix == ModContent.PrefixType<RuinedPrefix>() ) {
				return false;
			}
			return base.ConsumeItem( item, player );
		}


		/*public override bool ConsumeAmmo( Item item, Player player ) {
			if( item.prefix == ModContent.PrefixType<RuinedPrefix>() ) {
				return false;
			}
			return base.ConsumeAmmo( item, player );
		}*/
	}
}
