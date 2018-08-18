using HamstarHelpers.Helpers.PlayerHelpers;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Encumbrance {
	partial class EncumbrancePlayer : ModPlayer {
		private bool IsJumping = false;
		private bool IsDashing = false;
		private bool IsMounted = false;
		private int ItemUseDrainDuration = 0;



		////////////////

		public override bool CloneNewInstances { get { return false; } }


		////////////////

		public override void PreUpdate() {
			if( this.player.dead ) {
				return;
			}

			if( this.ItemUseDrainDuration > 0 ) {
				this.ItemUseDrainDuration--;

				Item curr_item = this.player.inventory[ this.player.selectedItem ];
				if( curr_item != null && !curr_item.IsAir ) {
					this.RunItemUseEffect();
				}
			}

			switch( this.player.mount.Type ) {
			case MountID.MineCart:
			case MountID.MineCartWood:
			case MountID.MineCartMech:
				break;
			default:
				if( this.player.mount.Active ) {
					if( !this.IsMounted ) {
						this.IsMounted = true;
						this.RunMountEffect();
					}
				} else {
					this.IsMounted = false;
				}
				break;
			}
		}


		public override void PostUpdateRunSpeeds() {
			if( !this.player.dead ) { return; }

			var mymod = (EncumbranceMod)this.mod;
			
			// Is sprinting?
			/*if( !player.mount.Active && player.velocity.Y == 0f && player.dashDelay >= 0 ) {
				float runMin = PlayerMovementHelpers.MinimumRunSpeed( player );
				float acc = player.accRunSpeed + 0.1f;
				float velX = player.velocity.X;

				if( ( player.controlRight && velX > runMin && velX < acc ) ||
					( player.controlLeft && velX < -runMin && velX > -acc ) ) {
					//Main.NewText("runMin:"+ runMin+ ",acc:"+ acc+ ",velX:"+ velX+",maxRunSpeed:"+ this.Player.maxRunSpeed);
					this.DrainStaminaViaSprint( mymod, player );
				}
			}*/

			// Is dashing?
			if( !this.IsDashing ) {
				if( player.dash != 0 && player.dashDelay == -1 ) {
					this.RunDashEffect();
					this.IsDashing = true;
				}
			} else if( player.dashDelay != -1 ) {
				this.IsDashing = false;
			}

			// Is (attempting) jump?
			if( player.controlJump ) {
				if( !this.IsJumping && !PlayerMovementHelpers.IsFlying( player ) ) {
					if( player.swimTime > 0 ) {
						this.RunSwimEffect();
					} else {
						if( player.velocity.Y == 0 || player.sliding ||
								player.jumpAgainBlizzard || player.jumpAgainCloud || player.jumpAgainFart || player.jumpAgainSandstorm ) {
							this.RunJumpEffect();
						}
					}
					this.IsJumping = true;
				}

				if( player.jump > 0 || PlayerMovementHelpers.IsFlying( player ) ) {
					if( player.swimTime > 0 ) {
						this.RunSwimHoldEffect();
					} else {
						this.RunJumpHoldEffect();
					}
				}
			} else if( this.IsJumping ) {
				this.IsJumping = false;
			}
		}
	}
}
