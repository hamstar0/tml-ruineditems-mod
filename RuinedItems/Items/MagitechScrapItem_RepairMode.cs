using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;


namespace RuinedItems.Items {
	public partial class MagitechScrapItem : ModItem {
		internal static void UpdateRepairInteractionsIf( Player player ) {
			if( !MagitechScrapItem.PickerActive ) {
				return;
			}

			if( !Main.playerInventory ) {
				MagitechScrapItem.PickerActive = false;

				return;
			}

			if( Main.mouseItem?.IsAir == false ) {
				MagitechScrapItem.PickerActive = false;

				if( MagitechScrapItem.ApplyRepairIf( player, Main.mouseItem ) ) {
					//Main.mouseItem = new Item();
				}
			}
		}


		////////////////

		public static void DrawPickerMode() {
			if( !MagitechScrapItem.PickerActive ) {
				return;
			}

			Texture2D tex = Main.itemTexture[ ModContent.ItemType<MagitechScrapItem>() ];

			Main.spriteBatch.Draw(
				texture: tex,
				position: Main.MouseScreen - new Vector2(20, 24),
				color: Color.White
			);
			Utils.DrawBorderStringFourWay(
				sb: Main.spriteBatch,
				font: Main.fontMouseText,
				text: "Select an item to repair",
				x: Main.mouseX + 12,
				y: Main.mouseY + 16,
				textColor: new Color( Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor ),
				borderColor: Color.Black,
				origin: default
			);
		}
	}
}
