using HamstarHelpers.Helpers.TModLoader.Mods;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;


namespace Encumbrance {
	partial class EncumbranceMod : Mod {
		public static EncumbranceMod Instance { get; private set; }



		////////////////

		public EncumbranceConfigData Config => ModContent.GetInstance<EncumbranceConfigData>();

		public Texture2D ShadowBox = null;


		////////////////

		public EncumbranceMod() {
			EncumbranceMod.Instance = this;
		}

		////////////////

		public override void Load() {
			if( !Main.dedServ ) {
				this.ShadowBox = this.GetTexture( "ShadowBox" );
			}
		}

		public override void Unload() {
			EncumbranceMod.Instance = null;
		}


		////////////////

		public override object Call( params object[] args ) {
			return ModBoilerplateHelpers.HandleModCall( typeof( EncumbranceAPI ), args );
		}
	}
}
