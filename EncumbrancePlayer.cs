using HamstarHelpers.Helpers.PlayerHelpers;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Encumbrance {
	partial class EncumbrancePlayer : ModPlayer {
		private bool IsJumping = false;
		private bool IsDashing = false;
		private bool IsMounted = false;
		private int ItemUseCooldown = 0;
		private int DropCooldown = 0;



		////////////////

		public override bool CloneNewInstances { get { return false; } }


		////////////////

		public override void PreUpdate() {
			if( this.player.dead ) {
				return;
			}

			if( this.DropCooldown > 0 ) {
				this.DropCooldown--;
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


		////////////////
		
		public int GetCurrentCapacity() {
			var mymod = EncumbranceMod.Instance;

			if( this.player.pulley ) {
				return mymod.Config.CarryCapacityOnPulley;
			}
			if( this.player.mount.Active ) {
				switch( this.player.mount.Type ) {
				case MountID.MineCart:
				case MountID.MineCartWood:
				case MountID.MineCartMech:
					return mymod.Config.CarryCapacityOnMinecart;
				}
			}
			return mymod.Config.CarryCapacityBase;
		}


		public float GaugeEncumbrance() {
			var mymod = EncumbranceMod.Instance;

			int capacity = this.GetCurrentCapacity();
			int inv_max = PlayerItemHelpers.VanillaInventoryHotbarSize + PlayerItemHelpers.VanillaInventoryMainSize;
			int max_load = inv_max - capacity;
			int load = 0;

			if( max_load == 0 ) {
				return 0f;
			}

			for( int i = capacity; i < inv_max; i++ ) {
				Item item = this.player.inventory[i];

				if( item != null && !item.IsAir ) {
					load++;
				}
			}

			return (float)load / (float)max_load;
		}
	}
}
