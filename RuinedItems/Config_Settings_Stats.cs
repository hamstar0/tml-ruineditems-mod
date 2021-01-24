using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;


namespace RuinedItems {
	public partial class RuinedItemsConfig : ModConfig {
		[Range( 0f, 10f )]
		[DefaultValue( 2f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float PrefixValueScale { get; set; } = 2f;


		[Range( 0f, 10f )]
		[DefaultValue( 2f /3f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float RuinedDamageScale { get; set; } = 2f / 3f;

		[Range( 0f, 10f )]
		[DefaultValue( 2f / 3f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float RuinedKnockbackScale { get; set; } = 2f / 3f;
		[Range( 0f, 10f )]
		[DefaultValue( 3f / 2f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float RuinedUseTimeScale { get; set; } = 3f / 2f;

		[Range( 0f, 10f )]
		[DefaultValue( 2f / 3f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float RuinedSizeScale { get; set; } = 2f / 3f;

		[Range( 0f, 10f )]
		[DefaultValue( 2f / 3f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float RuinedProjectileVelocityScale { get; set; } = 2f / 3f;

		[Range( 0f, 10f )]
		[DefaultValue( 3f / 2f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float RuinedManaUseScale { get; set; } = 3f / 2f;

		[Range( -100, 100 )]
		[DefaultValue( -5 )]
		public int RuinedCritAdd { get; set; } = -5;
		
		[Range( 0f, 10f )]
		[DefaultValue( 4f / 5f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float RuinedAccessoryMoveSpeedScale { get; set; } = 4f / 5f;

		[Range( -1f, 1f )]
		[DefaultValue( -0.2f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float RuinedAccessoryMeleeSpeedAdd { get; set; } = -0.2f;

		[Range( -100, 100 )]
		[DefaultValue( -5 )]
		public int RuinedAccessoryDefenseAdd { get; set; } = -5;
	}
}
