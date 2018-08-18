using HamstarHelpers.Helpers.PlayerHelpers;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Encumbrance {
	partial class EncumbrancePlayer : ModPlayer {
		public bool CanDrop() {
			if( this.player.mount.Active ) {
				switch( this.player.mount.Type ) {
				case MountID.MineCart:
				case MountID.MineCartWood:
				case MountID.MineCartMech:
					return false;
				}
			}

			return this.DropCooldown == 0;
		}


		////////////////

		public void RunItemUseEffect() {
			if( !this.CanDrop() ) { return; }

			var mymod = (EncumbranceMod)this.mod;
			this.DropCarriedItems( mymod.Config.DropOnItemUse );
		}

		public void RunDashEffect() {
			if( !this.CanDrop() ) { return; }

			var mymod = (EncumbranceMod)this.mod;
			this.DropCarriedItems( mymod.Config.DropOnDash );
		}

		public void RunSwimEffect() {
			if( !this.CanDrop() ) { return; }

			var mymod = (EncumbranceMod)this.mod;
			this.DropCarriedItems( mymod.Config.DropOnSwim );
		}
		
		public void RunSwimHoldEffect() {
			if( !this.CanDrop() ) { return; }

			var mymod = (EncumbranceMod)this.mod;
			this.DropCarriedItems( mymod.Config.DropOnSwimHold );
		}

		public void RunJumpEffect() {
			if( !this.CanDrop() ) { return; }

			var mymod = (EncumbranceMod)this.mod;
			this.DropCarriedItems( mymod.Config.DropOnJump );
		}
		
		public void RunJumpHoldEffect() {
			if( !this.CanDrop() ) { return; }

			var mymod = (EncumbranceMod)this.mod;
			this.DropCarriedItems( mymod.Config.DropOnJumpHold );
		}

		public void RunGrappleEffect() {
			if( !this.CanDrop() ) { return; }

			var mymod = (EncumbranceMod)this.mod;
			this.DropCarriedItems( mymod.Config.DropOnGrapple );
		}


		public void RunHurtEffect( int damage ) {
			var mymod = (EncumbranceMod)this.mod;
			if( damage >= mymod.Config.DropOnDamageMinimum ) {
				this.DropCarriedItem();
			} else {
				this.DropCarriedItems( mymod.Config.DropPerDamageAmount / damage );
			}
		}

		public void RunDeathEffect() {
			var mymod = (EncumbranceMod)this.mod;
			this.DropCarriedItems( mymod.Config.DropOnDeath );
		}

		public void RunMountEffect() {
			var mymod = (EncumbranceMod)this.mod;
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
				this.DropCooldown = mymod.Config.DropCooldown;

				PlayerItemHelpers.DropInventoryItem( this.player, slot );
				return true;
			}
			return false;
		}
		
		public void DropAllCarriedItems() {
			this.DropCooldown = mymod.Config.DropCooldown;

			for( int i = PlayerItemHelpers.VanillaInventoryHotbarSize; i < PlayerItemHelpers.VanillaInventoryMainSize; i++ ) {
				Item item = this.player.inventory[ i ];

				if( item != null && !item.IsAir ) {
					PlayerItemHelpers.DropInventoryItem( this.player, i );
				}
			}
		}
	}
}
