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
		

		public override void PostHurt( bool pvp, bool quiet, double damage, int hitDirection, bool crit ) {
			if( quiet ) { return; }

			float dmg = (float)damage * ( crit ? 2f : 1f );

			if( dmg > 1 ) {
				this.RunHurtEffect( (int)dmg );
			}
		}


		public override bool PreKill( double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource ) {
			this.RunDeathEffect();

			return base.PreKill( damage, hitDirection, pvp, ref playSound, ref genGore, ref damageSource );
		}
	}
}
