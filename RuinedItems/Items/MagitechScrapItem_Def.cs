using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Services.Timers;


namespace RuinedItems.Items {
	public partial class MagitechScrapItem : ModItem {
		public static bool PickerActive { get; internal set; } = false;



		////////////////

		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Magitech Scrap" );
			this.Tooltip.SetDefault( "Assorted machine parts with assorted enchantments"
				+"\nRight-click to begin item picking, left-click on an item to repair it"
				+"\nMay only repair ruined items"
			);
		}

		public override void SetDefaults() {
			var config = RuinedItemsConfig.Instance;

			this.item.width = 12;
			this.item.height = 12;
			this.item.maxStack = 1;
			this.item.consumable = true;
			this.item.value = config.Get<int>( nameof(config.MagitechScrapPrice) );	//Item.buyPrice( 0, 3, 0, 0 );
			//this.item.UseSound = SoundID.Item108;
			this.item.rare = ItemRarityID.Orange;
		}


		////

		public override void ModifyTooltips( List<TooltipLine> tooltips ) {
			var config = RuinedItemsConfig.Instance;
			float repairPerc = config.Get<float>( nameof(config.MagitechScrapRepairChance) );

			if( repairPerc < 1f ) {
				string repairPercStr = ((int)(repairPerc * 100f)).ToString();
				tooltips.Add( new TooltipLine(this.mod, "RuinedItemsMagitechScrap1", "Has only a "+repairPercStr+"% chance of success") );
				tooltips.Add( new TooltipLine(this.mod, "RuinedItemsMagitechScrap2", "Only one repair attempt allowed per item") );
			}
		}


		////

		public override bool CanRightClick() {
			Timers.SetTimer( "RuinedItemsScrapPickerMode", 2, true, () => {
				MagitechScrapItem.PickerActive = !MagitechScrapItem.PickerActive;
				return false;
			} );

			return false;
		}
	}
}
