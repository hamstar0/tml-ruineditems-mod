using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Services.Timers;


namespace RuinedItems.Items {
	public partial class MagitechScrapItem : ModItem {
		public static bool PickerActive { get; internal set; } = false;

		public static Item HoverItem { get; internal set; } = null;



		////////////////

		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Magitech Scrap" );
			this.Tooltip.SetDefault( "Assorted machine parts bearing enchanted properties"
				+"\nRight click to begin item picking, right click again on an item to repair it"
				+"\nMay only repair ruined items"
			);
		}

		public override void SetDefaults() {
			this.item.width = 12;
			this.item.height = 12;
			this.item.maxStack = 1;
			this.item.consumable = true;
			this.item.value = Item.buyPrice( 0, 1, 0, 0 );
			//this.item.UseSound = SoundID.Item108;
			this.item.rare = ItemRarityID.Orange;
		}


		////

		public override void ModifyTooltips( List<TooltipLine> tooltips ) {
			var config = RuinedItemsConfig.Instance;
			float repairPerc = config.Get<float>( nameof(config.MagitechScrapRepairChance) );

			if( repairPerc < 1f ) {
				string repairPercStr = ((int)(repairPerc * 100f)).ToString();
				tooltips.Add( new TooltipLine(this.mod, "RuinedItemsMagitechScrap1", "Has only a "+repairPercStr+"% of success") );
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
