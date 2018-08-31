using Terraria;
using Terraria.ModLoader;


namespace Encumbrance {
	partial class EncumbrancePlayer : ModPlayer {
		public bool CanDropItem() {
			return this.ItemDropCooldown == 0 && this.Encumberable;
		}


		////////////////

		public void RunItemUseEffect() {
			if( !this.CanDropItem() ) { return; }

			var mymod = (EncumbranceMod)this.mod;
			if( mymod.Config.DropOnItemUse == 0 ) { return; }

			if( mymod.Config.DebugInfoMode ) {
				Main.NewText( " RunItemUseEffect" );
			}

			this.QueueCarriedDrops( mymod.Config.DropOnItemUse );
		}

		public void RunDashEffect() {
			if( !this.CanDropItem() ) { return; }

			var mymod = (EncumbranceMod)this.mod;
			if( mymod.Config.DropOnDash == 0 ) { return; }

			if( mymod.Config.DebugInfoMode ) {
				Main.NewText( " RunDashEffect" );
			}

			this.QueueCarriedDrops( mymod.Config.DropOnDash );
		}

		public void RunSwimEffect() {
			if( !this.CanDropItem() ) { return; }

			var mymod = (EncumbranceMod)this.mod;
			if( mymod.Config.DropOnSwim == 0 ) { return; }

			if( mymod.Config.DebugInfoMode ) {
				Main.NewText( " RunSwimEffect" );
			}

			this.QueueCarriedDrops( mymod.Config.DropOnSwim );
		}
		
		public void RunSwimHoldEffect() {
			if( !this.CanDropItem() ) { return; }

			var mymod = (EncumbranceMod)this.mod;
			if( mymod.Config.DropOnSwimHold == 0 ) { return; }

			if( mymod.Config.DebugInfoMode ) {
				Main.NewText( " RunSwimHoldEffect" );
			}

			this.QueueCarriedDrops( mymod.Config.DropOnSwimHold );
		}

		public void RunJumpEffect() {
			if( !this.CanDropItem() ) { return; }

			var mymod = (EncumbranceMod)this.mod;
			if( mymod.Config.DropOnJump == 0 ) { return; }

			if( mymod.Config.DebugInfoMode ) {
				Main.NewText( " RunJumpEffect" );
			}

			this.QueueCarriedDrops( mymod.Config.DropOnJump );
		}
		
		public void RunJumpHoldEffect() {
			if( !this.CanDropItem() ) { return; }

			var mymod = (EncumbranceMod)this.mod;
			if( mymod.Config.DropOnJumpHold == 0 ) { return; }

			if( mymod.Config.DebugInfoMode ) {
				Main.NewText( " RunJumpHoldEffect" );
			}

			this.QueueCarriedDrops( mymod.Config.DropOnJumpHold );
		}

		public void RunGrappleEffect() {
			if( !this.CanDropItem() ) { return; }

			var mymod = (EncumbranceMod)this.mod;
			if( mymod.Config.DropOnGrapple == 0 ) { return; }

			if( mymod.Config.DebugInfoMode ) {
				Main.NewText( " RunGrappleEffect" );
			}

			this.QueueCarriedDrops( mymod.Config.DropOnGrapple );
		}


		public void RunHurtEffect( int damage ) {
			var mymod = (EncumbranceMod)this.mod;
			if( mymod.Config.DropPerDamageAmount == 0 ) { return; }

			if( mymod.Config.DebugInfoMode ) {
				Main.NewText( " RunHurtEffect" );
			}

			if( damage >= mymod.Config.DropOnDamageMinimum ) {
				this.QueueCarriedDrops( 1 );
			} else {
				this.QueueCarriedDrops( mymod.Config.DropPerDamageAmount / damage );
			}
		}

		public void RunDeathEffect() {
			var mymod = (EncumbranceMod)this.mod;
			if( mymod.Config.DropOnDeath == 0 ) { return; }

			if( mymod.Config.DebugInfoMode ) {
				Main.NewText( " RunDeathEffect" );
			}

			this.QueueCarriedDrops( mymod.Config.DropOnDeath );
		}

		public void RunMountEffect() {
			var mymod = (EncumbranceMod)this.mod;
			if( mymod.Config.DropOnMount == 0 ) { return; }

			if( mymod.Config.DebugInfoMode ) {
				Main.NewText( " RunMountEffect" );
			}

			this.QueueCarriedDrops( mymod.Config.DropOnMount );
		}
	}
}
