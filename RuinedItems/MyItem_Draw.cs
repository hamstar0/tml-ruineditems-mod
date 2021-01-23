using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using RuinedItems.Prefixes;


namespace RuinedItems {
	partial class RuinedItemsItem : GlobalItem {
		public override void ModifyTooltips( Item item, List<TooltipLine> tooltips ) {
			if( item.prefix != ModContent.PrefixType<RuinedPrefix>() ) {
				return;
			}
			if( item.accessory ) {
				this.AddRuinedAccessoryTooltips( item, tooltips );
			}

			var tip = new TooltipLine( this.mod, "RuinedPrefixDesc", "Item is ruined and will need to be reforged" );
			tip.overrideColor = Color.Red;
			tooltips.Add( tip );
		}

		////

		private void AddRuinedAccessoryTooltips( Item item, List<TooltipLine> tooltips ) {
			int statNum = 0;

			//

			void addTip( string stat, bool isBad ) {
				var tip = new TooltipLine( this.mod, "RuinedPrefixAccStats_"+statNum, stat );

				if( isBad ) {
					tip.overrideColor = new Color( 190, 120, 120 );
				} else {
					tip.overrideColor = new Color( 120, 190, 120 ) ;
				}

				tooltips.Add( tip );

				statNum++;
			}

			//

			var config = RuinedItemsConfig.Instance;
			//int critAdd = config.Get<int>( nameof(config.RuinedCritAdd) );
			float moveSpeedMul = config.Get<float>( nameof(config.RuinedAccessoryMoveSpeedScale) );
			float meleeSpeedAdd = config.Get<float>( nameof(config.RuinedAccessoryMeleeSpeedAdd) );
			int defAdd = config.Get<int>( nameof(config.RuinedAccessoryDefenseAdd) );

			int moveSpeedPerc = (int)((moveSpeedMul - 1f) * 100f);
			int meleeSpeedPerc = (int)(meleeSpeedAdd * 100f);

			//addTip( critAdd+"% crit chance" );
			addTip( moveSpeedPerc+"% movement speed", moveSpeedPerc < 0 );
			addTip( meleeSpeedPerc+"% melee speed", meleeSpeedPerc < 0 );
			addTip( defAdd+" defense", defAdd < 0 );
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
