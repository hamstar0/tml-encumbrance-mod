using HamstarHelpers.Helpers.Players;
using HamstarHelpers.Services.Hooks.LoadHooks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Encumbrance {
	partial class EncumbrancePlayer : ModPlayer {
		public static CustomLoadHookValidator<int> PlayerMovementPromiseValidator;
		private static object PlayerMovementPromiseValidatorKey;

		static EncumbrancePlayer() {
			EncumbrancePlayer.PlayerMovementPromiseValidatorKey = new object();
			EncumbrancePlayer.PlayerMovementPromiseValidator = new CustomLoadHookValidator<int>( EncumbrancePlayer.PlayerMovementPromiseValidatorKey );
		}



		////////////////

		private int QueuedDropAmount = 0;

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
			int invMax = PlayerItemHelpers.VanillaInventoryHotbarSize + PlayerItemHelpers.VanillaInventoryMainSize;
			int maxLoad = invMax - capacity;
			int load = 0;

			if( maxLoad == 0 ) {
				return 0f;
			}

			for( int i = capacity; i < invMax; i++ ) {
				Item item = this.player.inventory[i];

				if( item != null && !item.IsAir ) {
					load++;
				}
			}

			return (float)load / (float)maxLoad;
		}
	}
}
