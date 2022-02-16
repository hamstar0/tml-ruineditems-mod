using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Classes.Loadable;
using ModLibsUtilityContent.Items;


namespace RuinedItems.Items {
	public partial class MagitechScrapItem_Mods : ILoadable {
		internal static void UpdateRepairInteractionsIf( Player player ) {
			if( !MagitechScrapItem_Mods.PickerActive ) {
				return;
			}

			if( !Main.playerInventory ) {
				MagitechScrapItem_Mods.PickerActive = false;

				return;
			}

			if( Main.mouseItem?.IsAir == false ) {
				MagitechScrapItem_Mods.PickerActive = false;

				if( MagitechScrapItem_Mods.ApplyRepairIf( player, Main.mouseItem ) ) {
					//Main.mouseItem = new Item();
				}
			}
		}


		////////////////

		public static void DrawPickerMode() {
			if( !MagitechScrapItem_Mods.PickerActive ) {
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
