using System;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria;
using Terraria.ModLoader;


namespace RuinedItems.Prefixes {
	public partial class RuinedPrefix : ModPrefix {
		private void LoadIL() {
			IL.Terraria.Item.Prefix += RuinedPrefix.HookPostPrefix;
		}


		internal static void HookPostPrefix( ILContext il ) {
			var c = new ILCursor( il );

			// Go to end of method
			c.Index = c.Instrs.Count - 1;

			// Push current Item to stack
			c.Emit( OpCodes.Ldarg_0 );

			// Apply final tweak to item
			c.EmitDelegate<Action<Item>>( ( item ) => {
				item.rare = -1;
			} );
		}
	}
}
