using HamstarHelpers.Components.Config;
using HamstarHelpers.Helpers.DebugHelpers;
using System;
using System.IO;
using Terraria;
using Terraria.ModLoader;


namespace Encumbrance {
	partial class EncumbranceMod : Mod {
		public static string GithubUserName { get { return "hamstar0"; } }
		public static string GithubProjectName { get { return "tml-encumbrance-mod"; } }

		public static string ConfigFileRelativePath {
			get { return ConfigurationDataBase.RelativePath + Path.DirectorySeparatorChar + EncumbranceConfigData.ConfigFileName; }
		}
		public static void ReloadConfigFromFile() {
			if( Main.netMode != 0 ) {
				throw new Exception( "Cannot reload configs outside of single player." );
			}
			if( EncumbranceMod.Instance != null ) {
				if( !EncumbranceMod.Instance.ConfigJson.LoadFile() ) {
					EncumbranceMod.Instance.ConfigJson.SaveFile();
				}
			}
		}

		public static void ResetConfigFromDefaults() {
			if( Main.netMode != 0 ) {
				throw new Exception( "Cannot reset to default configs outside of single player." );
			}

			var new_config = new EncumbranceConfigData();
			//new_config.SetDefaults();

			EncumbranceMod.Instance.ConfigJson.SetData( new_config );
			EncumbranceMod.Instance.ConfigJson.SaveFile();
		}
	}
}
