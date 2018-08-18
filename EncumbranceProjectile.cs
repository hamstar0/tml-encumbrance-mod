using Terraria;
using Terraria.ModLoader;


namespace Encumbrance {
	class EncumbranceProjectile : GlobalProjectile {
		public override void UseGrapple( Player player, ref int type ) {
			var myplayer = player.GetModPlayer<EncumbrancePlayer>( this.mod );
			myplayer.RunGrappleEffect();
			//Main.NewText("UseGrapple " + StaminaMod.Config.Data.SingularExertionRate);
		}
	}
}