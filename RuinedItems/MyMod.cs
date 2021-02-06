using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Services.Hooks.ExtendedHooks;
using RuinedItems.Prefixes;


namespace RuinedItems {
	public class RuinedItemsMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-ruineditems-mod";


		////////////////

		public static RuinedItemsMod Instance { get; private set; }



		////////////////

		public Texture2D DisabledItemTex { get; private set; }



		////////////////

		public RuinedItemsMod() {
			RuinedItemsMod.Instance = this;
		}

		public override void Load() {
			if( Main.netMode != NetmodeID.Server ) {
				this.DisabledItemTex = ModContent.GetTexture( "Terraria/MapDeath" );
			}
		}

		public override void PostSetupContent() {
			var config = RuinedItemsConfig.Instance;
			
			if( config.Get<float>( nameof(config.NPCLootItemRuinPercentChance) ) > 0f ) {
				ExtendedItemHooks.AddNPCLootHook( this.RuinLootDropsIf );
			}
		}

		public override void Unload() {
			RuinedItemsMod.Instance = null;
		}


		////////////////

		private void RuinLootDropsIf( NPC npc, IList<int> itemWhos ) {
			var config = RuinedItemsConfig.Instance;
			float npcLootRuinChance = config.Get<float>( nameof(config.NPCLootItemRuinPercentChance) );

			byte ruinedPrefix = ModContent.PrefixType<RuinedPrefix>();

			foreach( int itemWho in itemWhos ) {
				Item item = Main.item[itemWho];
				if( item?.active != true || !RuinedPrefix.IsItemRuinable(item) ) {
					continue;
				}

				if( Main.rand.NextFloat() <= npcLootRuinChance ) {
					item.Prefix( ruinedPrefix );

					if( Main.netMode == NetmodeID.Server ) {
						NetMessage.SendData( MessageID.SyncItem, -1, -1, null, itemWho );
					}
				}
			}
		}
	}
}