using HamstarHelpers.Components.Errors;
using Terraria;

namespace Encumbrance {
	public static partial class EncumbranceAPI {
		public static EncumbranceConfigData GetModSettings() {
			return EncumbranceMod.Instance.Config;
		}

		public static void SaveModSettingsChanges() {
			if( Main.netMode != 0 ) {
				throw new HamstarException( "Mod settings may only be saved in single player." );
			}

			EncumbranceMod.Instance.ConfigJson.SaveFile();
		}


		////////////////

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
