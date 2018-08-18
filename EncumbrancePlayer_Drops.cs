using HamstarHelpers.Helpers.PlayerHelpers;
using Terraria;
using Terraria.ModLoader;


namespace Encumbrance {
	partial class EncumbrancePlayer : ModPlayer {
		public bool CanDropItem() {
			return this.ItemDropCooldown == 0;
		}


		////////////////

		public void RunItemUseEffect() {
			if( !this.CanDropItem() ) { return; }

			var mymod = (EncumbranceMod)this.mod;
			if( mymod.Config.DropOnItemUse == 0 ) { return; }

			if( mymod.Config.DebugInfoMode ) {
				Main.NewText( " RunItemUseEffect" );
			}

			this.DropCarriedItems( mymod.Config.DropOnItemUse );
		}

		public void RunDashEffect() {
			if( !this.CanDropItem() ) { return; }

			var mymod = (EncumbranceMod)this.mod;
			if( mymod.Config.DropOnDash == 0 ) { return; }

			if( mymod.Config.DebugInfoMode ) {
				Main.NewText( " RunDashEffect" );
			}

			this.DropCarriedItems( mymod.Config.DropOnDash );
		}

		public void RunSwimEffect() {
			if( !this.CanDropItem() ) { return; }

			var mymod = (EncumbranceMod)this.mod;
			if( mymod.Config.DropOnSwim == 0 ) { return; }

			if( mymod.Config.DebugInfoMode ) {
				Main.NewText( " RunSwimEffect" );
			}

			this.DropCarriedItems( mymod.Config.DropOnSwim );
		}
		
		public void RunSwimHoldEffect() {
			if( !this.CanDropItem() ) { return; }

			var mymod = (EncumbranceMod)this.mod;
			if( mymod.Config.DropOnSwimHold == 0 ) { return; }

			if( mymod.Config.DebugInfoMode ) {
				Main.NewText( " RunSwimHoldEffect" );
			}

			this.DropCarriedItems( mymod.Config.DropOnSwimHold );
		}

		public void RunJumpEffect() {
			if( !this.CanDropItem() ) { return; }

			var mymod = (EncumbranceMod)this.mod;
			if( mymod.Config.DropOnJump == 0 ) { return; }

			if( mymod.Config.DebugInfoMode ) {
				Main.NewText( " RunJumpEffect" );
			}

			this.DropCarriedItems( mymod.Config.DropOnJump );
		}
		
		public void RunJumpHoldEffect() {
			if( !this.CanDropItem() ) { return; }

			var mymod = (EncumbranceMod)this.mod;
			if( mymod.Config.DropOnJumpHold == 0 ) { return; }

			if( mymod.Config.DebugInfoMode ) {
				Main.NewText( " RunJumpHoldEffect" );
			}

			this.DropCarriedItems( mymod.Config.DropOnJumpHold );
		}

		public void RunGrappleEffect() {
			if( !this.CanDropItem() ) { return; }

			var mymod = (EncumbranceMod)this.mod;
			if( mymod.Config.DropOnGrapple == 0 ) { return; }

			if( mymod.Config.DebugInfoMode ) {
				Main.NewText( " RunGrappleEffect" );
			}

			this.DropCarriedItems( mymod.Config.DropOnGrapple );
		}


		public void RunHurtEffect( int damage ) {
			var mymod = (EncumbranceMod)this.mod;
			if( mymod.Config.DropPerDamageAmount == 0 ) { return; }

			if( mymod.Config.DebugInfoMode ) {
				Main.NewText( " RunHurtEffect" );
			}

			if( damage >= mymod.Config.DropOnDamageMinimum ) {
				this.DropCarriedItem();
			} else {
				this.DropCarriedItems( mymod.Config.DropPerDamageAmount / damage );
			}
		}

		public void RunDeathEffect() {
			var mymod = (EncumbranceMod)this.mod;
			if( mymod.Config.DropOnDeath == 0 ) { return; }

			if( mymod.Config.DebugInfoMode ) {
				Main.NewText( " RunDeathEffect" );
			}

			this.DropCarriedItems( mymod.Config.DropOnDeath );
		}

		public void RunMountEffect() {
			var mymod = (EncumbranceMod)this.mod;
			if( mymod.Config.DropOnMount == 0 ) { return; }

			if( mymod.Config.DebugInfoMode ) {
				Main.NewText( " RunMountEffect" );
			}

			this.DropCarriedItems( mymod.Config.DropOnMount );
		}


		////////////////

		public void DropCarriedItems( int amt ) {
			if( amt == -1 ) {
				this.DropAllCarriedItems();
			} else {
				for( int i = 0; i < amt; i++ ) {
					this.DropCarriedItem();
				}
			}
		}


		public bool DropCarriedItem() {
			var mymod = (EncumbranceMod)this.mod;
			
			int blocked = (PlayerItemHelpers.VanillaInventoryHotbarSize + PlayerItemHelpers.VanillaInventoryMainSize) - mymod.Config.CarryCapacityBase;
			int slot = (int)(Main.rand.NextFloat() * blocked) + PlayerItemHelpers.VanillaInventoryHotbarSize;

			Item item = this.player.inventory[ slot ];

			if( item != null && !item.IsAir ) {
				this.ItemDropCooldown = mymod.Config.DropCooldown;

				if( mymod.Config.DebugInfoMode ) {
					Main.NewText( " Dropped "+this.player.inventory[slot].Name );
				}

				int _;
				PlayerItemHelpers.DropInventoryItem( this.player, slot, mymod.Config.DroppedItemNoGrabDelay, out _ );

				return true;
			}
			return false;
		}
		
		public void DropAllCarriedItems() {
			var mymod = (EncumbranceMod)this.mod;

			this.ItemDropCooldown = mymod.Config.DropCooldown;

			for( int i = PlayerItemHelpers.VanillaInventoryHotbarSize; i < PlayerItemHelpers.VanillaInventoryMainSize; i++ ) {
				Item item = this.player.inventory[ i ];

				if( item != null && !item.IsAir ) {
					if( mymod.Config.DebugInfoMode ) {
						Main.NewText( " Dropped " + item.Name );
					}

					int _;
					PlayerItemHelpers.DropInventoryItem( this.player, i, mymod.Config.DroppedItemNoGrabDelay, out _ );
				}
			}
		}
	}
}
