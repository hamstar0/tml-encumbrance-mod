using Terraria;


namespace Encumbrance {
	public static partial class EncumbranceAPI {
		public static void EnableEncumbrance() {
			var myplayer = Main.LocalPlayer.GetModPlayer<EncumbrancePlayer>();

			myplayer.EnableEncumbrance();
		}

		public static void DisableEncumbrance() {
			var myplayer = Main.LocalPlayer.GetModPlayer<EncumbrancePlayer>();
			
			myplayer.DisableEncumbrance();
		}
	}
}
