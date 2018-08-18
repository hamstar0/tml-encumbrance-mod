using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;


namespace Encumbrance {
	partial class EncumbrancePlayer : ModPlayer {
		public override bool PreItemCheck() {
			bool prechecked = base.PreItemCheck();
			Item item = this.player.inventory[ this.player.selectedItem ];

			if( item != null && !item.IsAir && !this.player.noItems ) {
				if( this.player.controlUseItem && this.player.itemTime <= 1 ) {
					if( this.ItemUseCooldown == 0 ) {
						this.RunItemUseEffect();
						this.ItemUseCooldown = 36;
					}
				}
			}

			return prechecked;
		}
		

		public override void PostHurt( bool pvp, bool quiet, double damage, int hit_direction, bool crit ) {
			if( quiet ) { return; }

			float dmg = (float)damage * ( crit ? 2f : 1f );

			if( dmg > 1 ) {
				this.RunHurtEffect( (int)dmg );
			}
		}


		public override bool PreKill( double damage, int hit_direction, bool pvp, ref bool play_sound, ref bool gen_gore, ref PlayerDeathReason damage_source ) {
			this.RunDeathEffect();

			return base.PreKill( damage, hit_direction, pvp, ref play_sound, ref gen_gore, ref damage_source );
		}
	}
}
