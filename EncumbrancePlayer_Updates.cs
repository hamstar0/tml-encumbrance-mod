using Encumbrance.Buffs;
using HamstarHelpers.Helpers.DebugHelpers;
using HamstarHelpers.Helpers.PlayerHelpers;
using HamstarHelpers.Services.Promises;
using Terraria.ID;
using Terraria.ModLoader;


namespace Encumbrance {
	partial class EncumbrancePlayer : ModPlayer {
		public override void PreUpdate() {
			if( this.player.dead ) {
				return;
			}

			var mymod = (EncumbranceMod)this.mod;
			float encumbrance = this.GaugeEncumbrance();

			if( mymod.Config.DebugInfoMode ) {
				DebugHelpers.Print( "Encumbrance", "Capacity: " + this.GetCurrentCapacity() + ", Encumbrance: " + encumbrance, 20 );
			}

			if( encumbrance > 0f ) {
				this.player.AddBuff( this.mod.BuffType<EncumberedDebuff>(), 3 );
			}

			if( this.ItemDropCooldown > 0 ) {
				this.ItemDropCooldown--;
			}
			if( this.ItemUseCooldown > 0 ) {
				this.ItemUseCooldown--;
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
			if( this.player.dead ) { return; }

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
				if( this.player.dash != 0 && this.player.dashDelay == -1 ) {
					this.RunDashEffect();
					this.IsDashing = true;
				}
			} else if( this.player.dashDelay != -1 ) {
				this.IsDashing = false;
			}

			// Is (attempting) jump?
			if( this.player.controlJump ) {
				if( !this.IsJumping && !PlayerMovementHelpers.IsFlying( this.player ) ) {
					if( this.player.swimTime > 0 ) {
						this.RunSwimEffect();
					} else {
						if( this.player.velocity.Y == 0 || this.player.sliding ||
								this.player.jumpAgainBlizzard || this.player.jumpAgainCloud ||
								this.player.jumpAgainFart || this.player.jumpAgainSandstorm ) {
							this.RunJumpEffect();
						}
					}
					this.IsJumping = true;
				}

				if( this.player.jump > 0 || PlayerMovementHelpers.IsFlying( this.player ) ) {
					if( this.player.swimTime > 0 ) {
						this.RunSwimHoldEffect();
					} else {
						this.RunJumpHoldEffect();
					}
				}
			} else if( this.IsJumping ) {
				this.IsJumping = false;
			}
			
			Promises.TriggerValidatedPromise( EncumbrancePlayer.PlayerMovementPromiseValidator,
				EncumbrancePlayer.PlayerMovementPromiseValidatorKey,
				new PlayerMovementPromiseArguments { Who = this.player.whoAmI }
			);
		}
	}
}
