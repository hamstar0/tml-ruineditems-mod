﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using RuinedItems.Prefixes;


namespace RuinedItems {
	partial class RuinedItemsItem : GlobalItem {
		public override Color? GetAlpha( Item item, Color lightColor ) {
			if( item.prefix == ModContent.PrefixType<RuinedPrefix>() ) {
				float pulse = (float)Main.mouseTextColor / 255f;
				pulse = pulse * pulse * pulse;
				pulse *= 0.5f;

				Color newColor = lightColor * (0.25f + pulse);
				newColor.A = lightColor.A;
				return newColor;
			}

			return base.GetAlpha( item, lightColor );
		}


		public override void PostDrawInInventory(
					Item item,
					SpriteBatch sb,
					Vector2 position,
					Rectangle frame,
					Color drawColor,
					Color itemColor,
					Vector2 origin,
					float scale ) {
			if( item == null || item.IsAir ) { return; }
			if( item.prefix != ModContent.PrefixType<RuinedPrefix>() ) { return; }

			var config = RuinedItemsConfig.Instance;
			var mymod = (RuinedItemsMod)this.mod;

			if( !config.Get<bool>(nameof(config.RuinedItemsLockedFromUse)) ) {
				return;
			}

			float posX = position.X;
			posX += ((float)frame.Width / 2f) * scale;
			posX -= ((float)mymod.DisabledItemTex.Width / 2f) * scale;
			float posY = position.Y;
			posY += ((float)frame.Height / 2f) * scale;
			posY -= ((float)mymod.DisabledItemTex.Height / 2f) * scale;
			var pos = new Vector2( posX, posY );

			var rect = new Rectangle( 0, 0, mymod.DisabledItemTex.Width, mymod.DisabledItemTex.Height );
			var color = Color.White * 0.625f;

			sb.Draw( mymod.DisabledItemTex, pos, rect, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f );
		}
	}
}
