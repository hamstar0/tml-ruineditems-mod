﻿using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Classes.Loadable;
using ModLibsCore.Services.Timers;
using ModLibsUtilityContent.Items;


namespace RuinedItems.Items {
	public partial class MyMagitechScrapItem : ILoadable {
		public static bool PickerActive { get; internal set; } = false;



		////////////////

		void ILoadable.OnModsLoad() { }

		void ILoadable.OnPostModsLoad() { }

		void ILoadable.OnModsUnload() {
			var myitem = ModContent.GetInstance<MagitechScrapItem>();

			myitem.SetStaticDefaults_Hook = this.SetStaticDefaults;
			myitem.ModifyTooltips_Hook = this.ModifyTooltips;
			myitem.CanRightClick_Hook = this.CanRightClick;
		}

		////////////////

		private void SetStaticDefaults( MagitechScrapItem self ) {
			self.Tooltip.SetDefault( "Assorted machine parts with assorted enchantments"
				+"\nRight-click to begin item picking, left-click on an item to repair it"
				+"\nMay only repair ruined items"
			);
		}


		////

		private void ModifyTooltips( MagitechScrapItem self, List<TooltipLine> tooltips ) {
			var config = RuinedItemsConfig.Instance;
			float repairPerc = config.Get<float>( nameof(config.MagitechScrapRepairChance) );

			if( repairPerc < 1f ) {
				string repairPercStr = ((int)(repairPerc * 100f)).ToString();
				tooltips.Add( new TooltipLine(self.mod, "RuinedItemsMagitechScrap1", "Has only a "+repairPercStr+"% chance of success") );
				tooltips.Add( new TooltipLine(self.mod, "RuinedItemsMagitechScrap2", "Only one repair attempt allowed per item") );
			}
		}


		////

		private bool CanRightClick( MagitechScrapItem self ) {
			Timers.SetTimer( "RuinedItemsScrapPickerMode", 2, true, () => {
				MyMagitechScrapItem.PickerActive = !MyMagitechScrapItem.PickerActive;
				return false;
			} );

			return false;
		}
	}
}
