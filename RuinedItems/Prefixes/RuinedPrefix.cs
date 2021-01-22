using System;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Players;


namespace RuinedItems.Prefixes {
	public class RuinedPrefix : ModPrefix {
		public static bool IsItemRuinable( Item item ) {
			var config = RuinedItemsConfig.Instance;
			return item?.active == true && item.maxStack == 1
				&& ( item.accessory || item.melee || item.ranged || item.magic || item.summon )
				&& config.Get<float>( nameof(config.PrefixRollChance) ) > 0f;
		}



		////////////////

		public override bool Autoload( ref string name ) {
			name = "Ruined";
			return mod.Properties.Autoload;
		}


		////////////////

		public override void ModifyValue( ref float valueMult ) {
			var config = RuinedItemsConfig.Instance;
			valueMult *= config.Get<float>( nameof(config.PrefixValueScale) );
		}


		////////////////
		
		public override float RollChance( Item item ) {
			var config = RuinedItemsConfig.Instance;
			return config.Get<float>( nameof(config.PrefixRollChance) );
			//RuinedItemsConfig.Instance.Get<float>( nameof(RuinedItemsConfig.PrefixRollChance) );
		}


		public override bool CanRoll( Item item ) {
			var config = RuinedItemsConfig.Instance;
			return RuinedPrefix.IsItemRuinable( item )
				&& config.Get<float>( nameof(config.PrefixRollChance) ) > 0f;
			//&& RuinedItemsConfig.Instance.Get<float>( nameof(RuinedItemsConfig.PrefixRollChance) ) > 0f;
		}


		////////////////

		internal void UpdateRuinedAccessoriesForPlayer( Player player ) {
			for( int i = PlayerItemHelpers.VanillaAccessorySlotFirst; PlayerItemHelpers.IsAccessorySlot( player, i ); i++ ) {
				Item accItem = player.armor[i];
				if( accItem?.active != true || accItem.prefix != this.Type ) {
					continue;
				}

				this.ApplyAccessoryForPlayer( player );
			}
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
			if( item.owner < 0 || item.owner >= 255 ) {
				return;
			}

			var config = RuinedItemsConfig.Instance;

			if( !item.accessory ) {
				double useTimeMul = config.Get<float>( nameof(config.RuinedUseTimeScale) );
				double dmgMul = config.Get<float>( nameof(config.RuinedDamageScale) );
				double kbMul = config.Get<float>( nameof(config.RuinedKnockbackScale) );
				double sizeMul = config.Get<float>( nameof(config.RuinedSizeScale) );
				double projSpeedMul = config.Get<float>( nameof(config.RuinedProjectileVelocityScale) );
				double manaUseMul = config.Get<float>( nameof(config.RuinedManaUseScale) );
				int critAdd = config.Get<int>( nameof(config.RuinedCritAdd) );

				item.damage = (int)Math.Round( (double)item.damage * dmgMul );
				item.mana = (int)Math.Round( (double)item.mana * manaUseMul );
				item.knockBack *= (float)kbMul;
				item.scale *= (float)sizeMul;
				item.shootSpeed *= (float)projSpeedMul;
				item.useAnimation = (int)Math.Round( (double)item.useAnimation * useTimeMul );
				item.useTime = (int)Math.Round( (double)item.useTime * useTimeMul );
				item.reuseDelay = (int)Math.Round( (double)item.reuseDelay * useTimeMul );
				item.crit += critAdd;
			}
		}

		public void ApplyAccessoryForPlayer( Player player ) {
			var config = RuinedItemsConfig.Instance;
			
			float moveMul = config.Get<float>( nameof(config.RuinedMoveSpeedScale) );
			float meleeAdd = config.Get<float>( nameof(config.RuinedMeleeSpeedAdd) );
			int critAdd = config.Get<int>( nameof(config.RuinedCritAdd) );

			player.moveSpeed *= moveMul;
			player.meleeSpeed += meleeAdd;
			player.meleeCrit += critAdd;
			player.rangedCrit += critAdd;
			player.magicCrit += critAdd;
			player.thrownCrit += critAdd;
		}
	}
}
