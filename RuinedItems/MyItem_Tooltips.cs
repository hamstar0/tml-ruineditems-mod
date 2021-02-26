using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using RuinedItems.Items;
using RuinedItems.Prefixes;


namespace RuinedItems {
	partial class RuinedItemsItem : GlobalItem {
		public override void ModifyTooltips( Item item, List<TooltipLine> tooltips ) {
			MagitechScrapItem.HoverItem = Main.LocalPlayer.inventory.FirstOrDefault(
				i => i.IsTheSameAs( item ) && !i.IsNotTheSameAs( item )
			);

			if( item.prefix == ModContent.PrefixType<RuinedPrefix>() ) {
				this.ApplyRuinedTooltips( item, tooltips );

				if( this.IsScrapUsedUpon ) {
					var tip = new TooltipLine( this.mod, "ScrapUsed", "Magitech scrap repair attempted and failed; cannot retry" );
					tip.overrideColor = Color.Red;
				}
			}
		}


		////////////////

		private void ApplyRuinedTooltips( Item item, List<TooltipLine> tooltips ) {
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
	}
}
