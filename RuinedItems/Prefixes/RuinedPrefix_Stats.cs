using System;
using Terraria;
using Terraria.ModLoader;


namespace RuinedItems.Prefixes {
	public partial class RuinedPrefix : ModPrefix {
		public static void AddRuinedStats( Item item ) {
			var config = RuinedItemsConfig.Instance;

			int critAdd = config.Get<int>( nameof( config.RuinedCritAdd ) );

			if( !item.accessory ) {
				double useTimeMul = config.Get<float>( nameof( config.RuinedUseTimeScale ) );
				double dmgMul = config.Get<float>( nameof( config.RuinedDamageScale ) );
				double kbMul = config.Get<float>( nameof( config.RuinedKnockbackScale ) );
				double sizeMul = config.Get<float>( nameof( config.RuinedSizeScale ) );
				double projSpeedMul = config.Get<float>( nameof( config.RuinedProjectileVelocityScale ) );
				double manaUseMul = config.Get<float>( nameof( config.RuinedManaUseScale ) );

				item.useAnimation = (int)Math.Round( (double)item.useAnimation * useTimeMul );
				item.useTime = (int)Math.Round( (double)item.useTime * useTimeMul );
				item.reuseDelay = (int)Math.Round( (double)item.reuseDelay * useTimeMul );
				item.damage = (int)Math.Round( (double)item.damage * dmgMul );
				item.knockBack *= (float)kbMul;
				item.scale *= (float)sizeMul;
				item.shootSpeed *= (float)projSpeedMul;
				item.mana = (int)Math.Round( (double)item.mana * manaUseMul );
			} else {
				item.defense += config.Get<int>( nameof( config.RuinedAccessoryDefenseAdd ) );
			}

			item.crit += critAdd;
		}

		public static void RemoveRuinedStats( Item item ) {
			/*var config = RuinedItemsConfig.Instance;

			int critAdd = config.Get<int>( nameof( config.RuinedCritAdd ) );

			if( !item.accessory ) {
				double useTimeMul = config.Get<float>( nameof( config.RuinedUseTimeScale ) );
				double dmgMul = config.Get<float>( nameof( config.RuinedDamageScale ) );
				double kbMul = config.Get<float>( nameof( config.RuinedKnockbackScale ) );
				double sizeMul = config.Get<float>( nameof( config.RuinedSizeScale ) );
				double projSpeedMul = config.Get<float>( nameof( config.RuinedProjectileVelocityScale ) );
				double manaUseMul = config.Get<float>( nameof( config.RuinedManaUseScale ) );

				item.useAnimation = (int)Math.Round( (double)item.useAnimation / useTimeMul );
				item.useTime = (int)Math.Round( (double)item.useTime / useTimeMul );
				item.reuseDelay = (int)Math.Round( (double)item.reuseDelay / useTimeMul );
				item.damage = (int)Math.Round( (double)item.damage / dmgMul );
				item.knockBack /= (float)kbMul;
				item.scale /= (float)sizeMul;
				item.shootSpeed /= (float)projSpeedMul;
				item.mana = (int)Math.Round( (double)item.mana / manaUseMul );
			} else {
				item.defense = 0;
			}

			item.crit -= critAdd;*/

			int latestPrefix = item.prefix;

			item.SetDefaults( item.type );

			int resetRarity = item.rare;

			item.Prefix( latestPrefix );
			item.rare = resetRarity;
		}



		////////////////

		/*public override void SetStats(
					ref float damageMult,
					ref float knockbackMult,
					ref float useTimeMult,
					ref float scaleMult,
					ref float projectileSpeed,
					ref float manaMult,
					ref int critBonus ) {
			var config = RuinedItemsConfig.Instance;
			if( config.Get<bool>( nameof(config.RuinedItemsLockedOnly) ) ) {
				return;
			}

			damageMult *= config.Get<float>( nameof(config.RuinedDamageScale) );
			knockbackMult *= config.Get<float>( nameof(config.RuinedKnockbackScale) );
			useTimeMult *= config.Get<float>( nameof(config.RuinedUseTimeScale) );
			scaleMult *= config.Get<float>( nameof(config.RuinedSizeScale) );
			projectileSpeed *= config.Get<float>( nameof(config.RuinedProjectileVelocityScale) );
			manaMult *= config.Get<float>( nameof(config.RuinedManaUseScale) );
			critBonus = (int)((float)critBonus * config.Get<float>( nameof(config.RuinedCritBonus) ) );
		}*/

		public override void Apply( Item item ) {
			RuinedPrefix.AddRuinedStats( item );
		}


		private void PostApply( Item item ) {
			item.rare = -1;
		}
	}
}
