using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using HamstarHelpers.Helpers.Debug;
using RuinedItems.Items;
using RuinedItems.Prefixes;


namespace RuinedItems {
	partial class RuinedItemsItem : GlobalItem {
		public bool IsScrapUsedUpon { get; internal set; } = false;
		
		public bool WasRuinedSinceLastCheck { get; internal set; } = false;


		////////////////

		public override bool CloneNewInstances => false;

		public override bool InstancePerEntity => true;



		////////////////

		public override GlobalItem Clone( Item item, Item itemClone ) {
			var clone = base.Clone( item, itemClone ) as RuinedItemsItem;
			clone.IsScrapUsedUpon = this.IsScrapUsedUpon;
			return clone;
		}


		////////////////

		public override bool Autoload( ref string name ) {
			this.LoadBagProcessors();

			return base.Autoload( ref name );
		}


		////////////////

		public override void Load( Item item, TagCompound tag ) {
			if( tag.ContainsKey("is_scraped_upon") ) {
				this.IsScrapUsedUpon = tag.GetBool( "is_scraped_upon" );
			}
		}

		public override TagCompound Save( Item item ) {
			return new TagCompound { { "is_scraped_upon", this.IsScrapUsedUpon } };
		}

		public override bool NeedsSaving( Item item ) {
			return true;
		}

		////

		public override void NetReceive( Item item, BinaryReader reader ) {
			this.IsScrapUsedUpon = reader.ReadBoolean();
		}

		public override void NetSend( Item item, BinaryWriter writer ) {
			writer.Write( this.IsScrapUsedUpon );
		}


		////////////////
		
		public override void ModifyTooltips( Item item, List<TooltipLine> tooltips ) {
			MagitechScrapItem.HoverItem = Main.LocalPlayer.inventory.FirstOrDefault(
				i => i.IsTheSameAs( item ) && !i.IsNotTheSameAs( item )
			);

			if( item.prefix == ModContent.PrefixType<RuinedPrefix>() ) {
				this.ApplyRuinedTooltips( item, tooltips );
			}
		}


		////////////////

		public override void UpdateInventory( Item item, Player player ) {
			if( item.prefix == ModContent.PrefixType<RuinedPrefix>() ) {
				this.WasRuinedSinceLastCheck = true;
			} else {
				if( this.WasRuinedSinceLastCheck ) {
					this.WasRuinedSinceLastCheck = false;

					RuinedPrefix.RemoveRuinedStats( item );
				}
			}
		}
	}
}
