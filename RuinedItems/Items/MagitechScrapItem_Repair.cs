using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Players;
using RuinedItems.Prefixes;


namespace RuinedItems.Items {
	public partial class MagitechScrapItem : ModItem {
		public static void UpdatePickerMode() {
			if( !Main.playerInventory || Main.LocalPlayer.dead ) {
				MagitechScrapItem.PickerActive = false;
			}

			if( MagitechScrapItem.PickerActive ) {
				if( MagitechScrapItem.ApplyRepairIf(MagitechScrapItem.HoverItem, out bool escapeMode) || escapeMode ) {
					MagitechScrapItem.PickerActive = false;
					MagitechScrapItem.HoverItem = null;
				}
			}
		}


		public static void DrawPickerMode() {
			if( !MagitechScrapItem.PickerActive ) {
				return;
			}

			Texture2D tex = Main.itemTexture[ ModContent.ItemType<MagitechScrapItem>() ];

			Main.spriteBatch.Draw(
				texture: tex,
				position: Main.MouseScreen + new Vector2(16, 12),
				color: Color.White
			);
		}



		////////////////

		private static bool ApplyRepairIf( Item item, out bool escapeMode ) {
			if( item == null || item.IsAir ) {
				escapeMode = false;
				return false;
			}

			if( !Main.mouseRight || !Main.mouseRightRelease ) {
				escapeMode = false;
				return false;
			}

			int ruinedPrefixType = ModContent.PrefixType<RuinedPrefix>();
			if( item.prefix != ruinedPrefixType ) {
				escapeMode = false;
				return false;
			}

			var myitem = item.GetGlobalItem<RuinedItemsItem>();
			if( myitem.IsScrapUsedUpon ) {
				escapeMode = true;
				Main.NewText( "Cannot repair with scrap more than once. Use reforging instead.", Color.Yellow );
				return false;
			}

			int magiScrapItemType = ModContent.ItemType<MagitechScrapItem>();
			if( PlayerItemHelpers.RemoveInventoryItemQuantity( Main.LocalPlayer, magiScrapItemType, 1 ) <= 0 ) {
				escapeMode = true;
				return false;
			}

			var config = RuinedItemsConfig.Instance;
			if( Main.rand.NextFloat() < config.Get<float>( nameof(config.MagitechScrapRepairChance) ) ) {
				item.Prefix( -1 );

				CombatText.NewText( Main.LocalPlayer.getRect(), Color.Lime, "Repair success!", true );
			} else {
				CombatText.NewText( Main.LocalPlayer.getRect(), Color.DimGray, "Repair failed!", true );
				Main.NewText( "Repair failed! Item can now only be repaired via. reforging.", Color.Red );
			}

			myitem.IsScrapUsedUpon = true;

			Main.PlaySound( SoundID.Item37, Main.LocalPlayer.Center );
			Main.PlaySound( SoundID.Grab, Main.LocalPlayer.Center );

			escapeMode = true;
			return true;
		}
	}
}
