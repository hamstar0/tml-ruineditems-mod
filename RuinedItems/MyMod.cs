using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


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

		public override void Unload() {
			RuinedItemsMod.Instance = null;
		}
	}
}