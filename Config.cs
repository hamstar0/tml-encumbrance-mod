using HamstarHelpers.Components.Config;
using System;


namespace Encumbrance {
	public class EncumbranceConfigData : ConfigurationDataBase {
		public readonly static Version ConfigVersion = new Version( 1, 0, 0 );
		public readonly static string ConfigFileName = "Encumbrance Config.json";


		////////////////

		public string VersionSinceUpdate = "";

		public bool DebugInfoMode = false;

		public int CarryCapacityBase = 10;
		public int CarryCapacityOnPulley = 50;
		public int CarryCapacityOnMinecart = 50;

		public int DropCooldown = 90;   //1.5s
		public bool DropAlwaysAtLeastOf = false;

		public int DropOnItemUse = 1;
		public int DropOnSwim = 0;
		public int DropOnSwimHold = 0;
		public int DropOnJump = 1;
		public int DropOnJumpHold = 0;
		public int DropOnGrapple = 5;
		public int DropOnDash = 2;
		public int DropOnHurt = 1;
		public int DropOnMount = -1;
		public int DropOnDeath = -1;

		public int DropOnDamageMinimum = 25;
		public int DropPerDamageAmount = 50;

		public int DroppedItemNoGrabDelay = 60 * 4;	//4s

		public float EncumbrancePassiveDebuffScalePerItemSlot = 1f / 40f;



		////////////////

		public bool UpdateToLatestVersion() {
			var new_config = new EncumbranceConfigData();
			var vers_since = this.VersionSinceUpdate != "" ?
				new Version( this.VersionSinceUpdate ) :
				new Version();

			if( vers_since >= EncumbranceConfigData.ConfigVersion ) {
				return false;
			}

			this.VersionSinceUpdate = EncumbranceConfigData.ConfigVersion.ToString();

			return true;
		}
	}
}
