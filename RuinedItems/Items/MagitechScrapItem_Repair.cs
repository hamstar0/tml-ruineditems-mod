using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsGeneral.Libraries.Players;
using RuinedItems.Prefixes;


namespace RuinedItems.Items {
	public partial class MagitechScrapItem : ModItem {
		public static bool ApplyRepairIf( Player player, Item item ) {
			if( item?.active != true || item.stack == 0 ) {
				return false;
			}

			int scrapItemType = ModContent.ItemType<MagitechScrapItem>();
			Item scrapItem = PlayerItemFinderLibraries.FindFirstOfPossessedItemFor(
				player,
				new HashSet<int> { scrapItemType },
				false
			);

			var myScrapItem = scrapItem?.modItem as MagitechScrapItem;
			if( myScrapItem == null ) {
				Main.NewText( "No repair scrap items in player's possession.", Color.Yellow );
				return false;
			}

			int ruinedPrefixType = ModContent.PrefixType<RuinedPrefix>();
			if( item.prefix != ruinedPrefixType ) {
				return false;
			}

			var config = RuinedItemsConfig.Instance;
			var myitem = item.GetGlobalItem<RuinedItemsItem>();
			bool onlyOnce = config.Get<bool>( nameof(config.MagitechScrapAttemptsRepairOnlyOncePerItem) );

			if( onlyOnce && myitem.IsScrapUsedUpon ) {
				Main.NewText( "Cannot repair this with scrap more than once. Use reforging instead.", Color.Yellow );
				return false;
			}

			if( PlayerItemLibraries.RemoveInventoryItemQuantity( Main.LocalPlayer, scrapItemType, 1 ) <= 0 ) {
				Main.NewText( "Could not use player's scrap items for repairing.", Color.Yellow );
				return false;
			}

			if( Main.rand.NextFloat() < config.Get<float>( nameof(config.MagitechScrapRepairChance) ) ) {
				float rollChance = config.Get<float>( nameof(config.GeneralRuinRollChance) );

				config.SetOverride( nameof(config.GeneralRuinRollChance), 0f );
				item.Prefix( -1 );
				config.SetOverride( nameof(config.GeneralRuinRollChance), rollChance );

				CombatText.NewText( Main.LocalPlayer.getRect(), Color.Lime, "Repair success!", true );
				Main.NewText( "Repair success!", Color.Lime );
			} else {
				CombatText.NewText( Main.LocalPlayer.getRect(), Color.DimGray, "Repair failed!", true );

				if( config.Get<bool>( nameof(config.MagitechScrapAttemptsRepairOnlyOncePerItem) ) ) {
					Main.NewText( "Repair attempt failed! Item can now only be repaired via. reforging.", Color.OrangeRed );
				} else {
					Main.NewText( "Repair attempt failed!", Color.OrangeRed );
				}
			}

			myitem.IsScrapUsedUpon = true;

			Main.PlaySound( SoundID.Item37, Main.LocalPlayer.Center );
			Main.PlaySound( SoundID.Grab, Main.LocalPlayer.Center );

			return true;
		}
	}
}
