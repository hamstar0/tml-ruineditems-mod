using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsGeneral.Libraries.Items;
using RuinedItems;
using RuinedItems.Prefixes;
using RuinedItems.Items;


namespace ModLibsCore.Commands {
	/// @private
	public class CreateRandomRuinedItemCommand : ModCommand {
		/// @private
		public override CommandType Type => CommandType.Chat;
		/// @private
		public override string Command => "ri-test-item";
		/// @private
		public override string Usage => "/" + this.Command;
		/// @private
		public override string Description => "Creates a test ruined item and some magitech scrap.";



		////////////////

		/// @private
		public override void Action( CommandCaller caller, string input, string[] args ) {
			if( !RuinedItemsConfig.Instance.DebugMode ) {
				caller.Reply( "Debug mode must be enabled in mod settings.", Color.Yellow );
				return;
			}

			Vector2 pos = caller.Player.position;
			ItemLibraries.CreateItem( pos, ItemID.HermesBoots, 1, 16, 16, ModContent.PrefixType<RuinedPrefix>() );
			ItemLibraries.CreateItem( pos, ModContent.ItemType<MagitechScrapItem>(), 1, 16, 16 );
		}
	}
}
