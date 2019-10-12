using HamstarHelpers.Classes.UI.ModConfig;
using HamstarHelpers.Helpers.Players;
using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;


namespace Encumbrance {
	class MyFloatInputElement : FloatInputElement { }





	public class EncumbranceConfigData : ModConfig {
		public override ConfigScope Mode => ConfigScope.ServerSide;


		////

		public bool DebugInfoMode = false;


		[Range( 0f, 1f )]
		[DefaultValue( 0.5f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float BurdenedItemSlotOverlayOpacity = 0.5f;


		[Range( 0, PlayerHelpers.InventorySize )]
		[DefaultValue( 10 )]
		public int CarryCapacityBase = 10;

		[Range( 0, PlayerHelpers.InventoryMainSize )]
		[DefaultValue( 50 )]
		public int CarryCapacityOnPulley = 50;

		[Range( 0, PlayerHelpers.InventoryMainSize )]
		[DefaultValue( 50 )]
		public int CarryCapacityOnMinecart = 50;


		[Range( 0, Int32.MaxValue )]
		[DefaultValue( 90 )]
		public int DropCooldown = 90;   //1.5s

		[DefaultValue( false )]
		public bool DropAlwaysAtLeastOf = false;


		[Range( 0, PlayerHelpers.InventoryMainSize )]
		[DefaultValue( 1 )]
		public int DropOnItemUse = 1;

		[Range( 0, PlayerHelpers.InventoryMainSize )]
		[DefaultValue( 0 )]
		public int DropOnSwim = 0;

		[Range( 0, PlayerHelpers.InventoryMainSize )]
		[DefaultValue( 0 )]
		public int DropOnSwimHold = 0;

		[Range( 0, PlayerHelpers.InventoryMainSize )]
		[DefaultValue( 1 )]
		public int DropOnJump = 1;

		[Range( 0, PlayerHelpers.InventoryMainSize )]
		[DefaultValue( 0 )]
		public int DropOnJumpHold = 0;

		[Range( 0, PlayerHelpers.InventoryMainSize )]
		[DefaultValue( 5 )]
		public int DropOnGrapple = 5;

		[Range( 0, PlayerHelpers.InventoryMainSize )]
		[DefaultValue( 2 )]
		public int DropOnDash = 2;

		[Range( 0, PlayerHelpers.InventoryMainSize )]
		[DefaultValue( 1 )]
		public int DropOnHurt = 1;

		[Range( 0, PlayerHelpers.InventoryMainSize )]
		[DefaultValue( -1 )]
		public int DropOnMount = -1;

		[Range( 0, PlayerHelpers.InventoryMainSize )]
		[DefaultValue( -1 )]
		public int DropOnDeath = -1;


		[Range( 0, Int32.MaxValue )]
		[DefaultValue( 25 )]
		public int DropOnDamageMinimum = 25;

		[Range( 0, Int32.MaxValue )]
		[DefaultValue( 50 )]
		public int DropPerDamageAmount = 50;


		[Range( 0, Int32.MaxValue )]
		[DefaultValue( 60 * 4 )]
		public int DroppedItemNoGrabDelay = 60 * 4; //4s


		[Range( 0, 1f )]
		[DefaultValue( 1f / 40f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float EncumbrancePassiveDebuffScalePerItemSlot = 1f / 40f;
	}
}
