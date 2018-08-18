using HamstarHelpers.Helpers.PlayerHelpers;
using Terraria;
using Terraria.ModLoader;


namespace Encumbrance {
	partial class EncumbrancePlayer : ModPlayer {
		public void RunItemUseEffect() {
			var mymod = (EncumbranceMod)this.mod;
			this.DropCarriedItems( mymod.Config.DropOnItemUse );
		}

		public void RunDashEffect() {
			var mymod = (EncumbranceMod)this.mod;
			this.DropCarriedItems( mymod.Config.DropOnDash );
		}

		public void RunSwimEffect() {
			var mymod = (EncumbranceMod)this.mod;
			this.DropCarriedItems( mymod.Config.DropOnSwim );
		}
		
		public void RunSwimHoldEffect() {
			var mymod = (EncumbranceMod)this.mod;
			this.DropCarriedItems( mymod.Config.DropOnSwimHold );
		}

		public void RunJumpEffect() {
			var mymod = (EncumbranceMod)this.mod;
			this.DropCarriedItems( mymod.Config.DropOnJump );
		}
		
		public void RunJumpHoldEffect() {
			var mymod = (EncumbranceMod)this.mod;
			this.DropCarriedItems( mymod.Config.DropOnJumpHold );
		}

		public void RunGrappleEffect() {
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
				PlayerItemHelpers.DropInventoryItem( this.player, slot );
				return true;
			}
			return false;
		}
		
		public void DropAllCarriedItems() {
			for( int i = PlayerItemHelpers.VanillaInventoryHotbarSize; i < PlayerItemHelpers.VanillaInventoryMainSize; i++ ) {
				Item item = this.player.inventory[ i ];

				if( item != null && !item.IsAir ) {
					PlayerItemHelpers.DropInventoryItem( this.player, i );
				}
			}
		}
	}
}
