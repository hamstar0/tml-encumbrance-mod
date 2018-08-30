using HamstarHelpers.Components.Config;
using HamstarHelpers.Helpers.DebugHelpers;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;


namespace Encumbrance {
	partial class EncumbranceMod : Mod {
		public static EncumbranceMod Instance { get; private set; }



		////////////////

		public JsonConfig<EncumbranceConfigData> ConfigJson { get; private set; }
		public EncumbranceConfigData Config { get { return this.ConfigJson.Data; } }

		public Texture2D ShadowBox = null;


		////////////////

		public EncumbranceMod() {
			this.Properties = new ModProperties() {
				Autoload = true,
				AutoloadGores = true,
				AutoloadSounds = true
			};

			string filename = "Encumbrance Config.json";
			this.ConfigJson = new JsonConfig<EncumbranceConfigData>( filename, ConfigurationDataBase.RelativePath, new EncumbranceConfigData() );
		}

		////////////////

		public override void Load() {
			EncumbranceMod.Instance = this;

			if( !Main.dedServ ) {
				this.ShadowBox = this.GetTexture( "ShadowBox" );
			}

			this.LoadConfig();
		}

		private void LoadConfig() {
			if( !this.ConfigJson.LoadFile() ) {
				this.ConfigJson.SaveFile();
			}

			if( this.Config.UpdateToLatestVersion() ) {
				LogHelpers.Log( "Encumbrance updated to " + this.Version.ToString() );
				this.ConfigJson.SaveFile();
			}
		}

		public override void Unload() {
			EncumbranceMod.Instance = null;
		}


		////////////////

		public override object Call( params object[] args ) {
			if( args.Length == 0 ) { throw new Exception( "Undefined call type." ); }

			string call_type = args[0] as string;
			if( args == null ) { throw new Exception( "Invalid call type." ); }

			var new_args = new object[args.Length - 1];
			Array.Copy( args, 1, new_args, 0, args.Length - 1 );

			return EncumbranceAPI.Call( call_type, new_args );
		}
	}
}
