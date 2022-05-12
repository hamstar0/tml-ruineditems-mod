using System;
using System.IO;
using Terraria;
using Terraria.Utilities;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using ModLibsCore.Libraries.Debug;
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
				var myitem = item.GetGlobalItem<RuinedItemsItem>();

				myitem.IsScrapUsedUpon = tag.GetBool( "is_scraped_upon" );
			}
		}

		public override TagCompound Save( Item item ) {
			var myitem = item.GetGlobalItem<RuinedItemsItem>();

			return new TagCompound {
				{ "is_scraped_upon", myitem.IsScrapUsedUpon }
			};
		}

		public override bool NeedsSaving( Item item ) {
			return RuinedPrefix.IsItemRuinable( item, out _ );
		}

		////

		public override void NetReceive( Item item, BinaryReader reader ) {
			var myitem = item.GetGlobalItem<RuinedItemsItem>();

			myitem.IsScrapUsedUpon = reader.ReadBoolean();
		}

		public override void NetSend( Item item, BinaryWriter writer ) {
			var myitem = item.GetGlobalItem<RuinedItemsItem>();

			writer.Write( myitem.IsScrapUsedUpon );
		}


		////////////////

		public override int ChoosePrefix( Item item, UnifiedRandom rand ) {
			var config = RuinedItemsConfig.Instance;
			float chance = config.Get<float>( nameof(config.GeneralRuinRollChance) );

			int prefix = rand.NextFloat() < chance
				? ModContent.PrefixType<RuinedPrefix>()
				: -1;

			return prefix;
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
