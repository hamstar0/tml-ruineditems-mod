using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Classes.Loadable;
using ModLibsCore.Services.Timers;
using ModLibsUtilityContent.Items;


namespace RuinedItems.Items {
	public partial class MagitechScrapItem_Mods : ILoadable {
		public static bool PickerActive { get; internal set; } = false;



		////////////////

		void ILoadable.OnModsLoad() {
		}

		void ILoadable.OnPostModsLoad() {
			var myitem = ModContent.GetInstance<MagitechScrapItem>();

			myitem.ModifyTooltips_Hook = this.ModifyTooltips;
			myitem.CanRightClick_Hook = this.CanRightClick;
		}

		void ILoadable.OnModsUnload() {
		}


		////////////////

		private void ModifyTooltips( MagitechScrapItem self, List<TooltipLine> tooltips ) {
			var config = RuinedItemsConfig.Instance;
			float repairPerc = config.Get<float>( nameof(config.MagitechScrapRepairChance) );

			//

			tooltips.Add( new TooltipLine( self.mod, "RuinedItemsMagitechScrap1",
				"Assorted machine parts with assorted enchantments"
			) );

			tooltips.Add( new TooltipLine( self.mod, "RuinedItemsMagitechScrap2",
				"Right-click scrap to begin picker, left-click on an item to repair it"
			) );

			tooltips.Add( new TooltipLine( self.mod, "RuinedItemsMagitechScrap3",
				"May only repair ruined items"
			) );

			//

			if( repairPerc < 1f ) {
				string repairPercStr = ((int)(repairPerc * 100f)).ToString();

				tooltips.Add( new TooltipLine( self.mod, "RuinedItemsMagitechScrap4",
					"Has only a "+repairPercStr+"% chance of success"
				) );

				tooltips.Add( new TooltipLine( self.mod, "RuinedItemsMagitechScrap5",
					"Only one repair attempt allowed per item"
				) );
			}
		}


		////

		private bool CanRightClick( MagitechScrapItem self ) {
			Timers.SetTimer( "RuinedItemsScrapPickerMode", 2, true, () => {
				MagitechScrapItem_Mods.PickerActive = !MagitechScrapItem_Mods.PickerActive;
				return false;
			} );

			return false;
		}
	}
}
