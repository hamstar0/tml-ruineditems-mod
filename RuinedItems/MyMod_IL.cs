using System;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;


namespace RuinedItems {
	public partial class RuinedItemsMod : Mod {
		internal (Item[] items, int context, int slot) InventoryMouseHoverInfo = default;

		internal Item InventoryMouseHoverItem => this.InventoryMouseHoverInfo.items == null
			? null
			: this.InventoryMouseHoverInfo.items[ this.InventoryMouseHoverInfo.slot ];



		////////////////

		private void LoadIL() {
			On.Terraria.UI.ItemSlot.MouseHover_ItemArray_int_int
				+= ( On.Terraria.UI.ItemSlot.orig_MouseHover_ItemArray_int_int orig, Item[] inv, int context, int slot )
				=> {
					this.ItemSlotMouseHover_Custom( inv, context, slot );
					orig.Invoke( inv, context, slot );
				};
		}


		////

		private void ItemSlotMouseHover_Custom( Item[] inv, int context, int slot ) {
			this.InventoryMouseHoverInfo = (inv, context, slot);
		}
	}
}