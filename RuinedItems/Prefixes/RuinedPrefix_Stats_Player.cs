﻿using System;
using Terraria;
using Terraria.ModLoader;


namespace RuinedItems.Prefixes {
	public partial class RuinedPrefix : ModPrefix {
		public void ConveyRuinedAccessoryStatsToPlayer( Player player ) {
			var config = RuinedItemsConfig.Instance;

			float moveMul = config.Get<float>( nameof( config.RuinedAccessoryMoveSpeedScale ) );
			float meleeAdd = config.Get<float>( nameof( config.RuinedAccessoryMeleeSpeedAdd ) );
			//int critAdd = config.Get<int>( nameof(config.RuinedCritAdd) );

			if( moveMul != 1f ) {
				player.moveSpeed *= moveMul;
			}
			if( meleeAdd != 0f ) {
				player.meleeSpeed += meleeAdd;
			}
			//player.meleeCrit += critAdd;
			//player.rangedCrit += critAdd;
			//player.magicCrit += critAdd;
			//player.thrownCrit += critAdd;
		}
	}
}
