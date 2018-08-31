using HamstarHelpers.Helpers.PlayerHelpers;
using Terraria;
using Terraria.ModLoader;


namespace Encumbrance {
	partial class EncumbrancePlayer : ModPlayer {
		public void QueueCarriedDrops( int amt ) {
			if( amt == -1 ) {
				this.QueuedDropAmount = -1;
			} else {
				if( this.QueuedDropAmount != -1 ) {
					this.QueuedDropAmount += amt;
				}
			}
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

		////////////////

		public bool DropCarriedItem() {
			if( Main.netMode == 2 ) { return false; }

			var mymod = (EncumbranceMod)this.mod;

			int inv_max = PlayerItemHelpers.VanillaInventoryHotbarSize + PlayerItemHelpers.VanillaInventoryMainSize;
			int encumb_start_slot = this.GetCurrentCapacity();
			int encumb_span = inv_max - encumb_start_slot;
			int slot = (int)(Main.rand.NextFloat() * (float)encumb_span) + encumb_start_slot;

			Item item = this.player.inventory[ slot ];

			if( item != null && !item.IsAir ) {
				int _;
				PlayerItemHelpers.DropInventoryItem( this.player, slot, mymod.Config.DroppedItemNoGrabDelay, out _ );

				this.ItemDropCooldown = mymod.Config.DropCooldown;

				if( mymod.Config.DebugInfoMode ) {
					Main.NewText( " Dropped " + item.Name + "("+slot+")" );
				}

				return true;
			}
			return false;
		}
		
		public void DropAllCarriedItems() {
			if( Main.netMode == 2 ) { return; }

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
