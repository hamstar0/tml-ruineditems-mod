﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using RuinedItems.Prefixes;


namespace RuinedItems {
	partial class RuinedItemsItem : GlobalItem {
		public override void ModifyTooltips( Item item, List<TooltipLine> tooltips ) {
			if( item.prefix != ModContent.PrefixType<RuinedPrefix>() ) { return; }

			var tip = new TooltipLine( this.mod, "blah1", "blah2" );
			tooltips.Add( tip );
		}

		////////////////

		public override void PostDrawInInventory( Item item, SpriteBatch sb, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale ) {
			if( item == null || item.IsAir ) { return; }
			if( item.prefix != ModContent.PrefixType<RuinedPrefix>() ) { return; }

			var mymod = (RuinedItemsMod)this.mod;

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
