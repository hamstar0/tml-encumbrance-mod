using HamstarHelpers.Helpers.PlayerHelpers;
using HamstarHelpers.Services.Promises;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Encumbrance {
	class PlayerMovementPromiseArguments : PromiseArguments {
		public int Who;
	}




	partial class EncumbrancePlayer : ModPlayer {
		public static PromiseValidator PlayerMovementPromiseValidator;
		private static object PlayerMovementPromiseValidatorKey;

		static EncumbrancePlayer() {
			EncumbrancePlayer.PlayerMovementPromiseValidatorKey = new object();
			EncumbrancePlayer.PlayerMovementPromiseValidator = new PromiseValidator( EncumbrancePlayer.PlayerMovementPromiseValidatorKey );
		}



		////////////////

		private bool IsJumping = false;
		private bool IsDashing = false;
		private bool IsMounted = false;
		private int ItemUseCooldown = 0;
		private int ItemDropCooldown = 0;

		public bool Encumberable { get; private set; }



		////////////////

		public override void Initialize() {
			this.Encumberable = true;
		}

		public override bool CloneNewInstances { get { return false; } }
		
		public override void clientClone( ModPlayer clone ) {
			base.clientClone( clone );
			var myclone = (EncumbrancePlayer)clone;
			
			myclone.IsDashing = this.IsDashing;
			myclone.IsJumping = this.IsJumping;
			myclone.IsMounted = this.IsMounted;
			myclone.ItemUseCooldown = this.ItemUseCooldown;
			myclone.ItemDropCooldown = this.ItemDropCooldown;
		}

		////////////////

		public void EnableEncumbrance() {
			this.Encumberable = true;
		}

		public void DisableEncumbrance() {
			this.Encumberable = false;
		}


		////////////////

		public int GetCurrentCapacity() {
			var mymod = EncumbranceMod.Instance;

			if( !this.Encumberable ) {
				return PlayerItemHelpers.VanillaInventoryHotbarSize + PlayerItemHelpers.VanillaInventoryMainSize;
			}

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
