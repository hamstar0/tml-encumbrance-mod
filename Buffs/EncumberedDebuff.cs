using Encumbrance;
using HamstarHelpers.Helpers.PlayerHelpers;
using HamstarHelpers.Services.Promises;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;


namespace Stamina.Buffs {
	class EncumberedDebuff : ModBuff {
		private static PromiseValidator MyValidator;
		private static object MyValidatorKey;

		private static Texture2D IconTex1;
		private static Texture2D IconTex2;
		private static Texture2D IconTex3;
		private static Texture2D IconTex4;


		static EncumberedDebuff() {
			EncumberedDebuff.MyValidatorKey = new object();
			EncumberedDebuff.MyValidator = new PromiseValidator( EncumberedDebuff.MyValidatorKey );
		}

		
		////////////////

		public override void SetDefaults() {
			this.DisplayName.SetDefault( "Encumbered" );
			this.Description.SetDefault( "You're weighted down" + '\n' + "More items means slower movement" );

			Main.debuff[ this.Type ] = true;

			if( !Main.dedServ && Promises.CountValidatedPromises(EncumberedDebuff.MyValidator) == 0 ) {
				Promises.AddValidatedPromise( EncumberedDebuff.MyValidator, () => {
					EncumberedDebuff.IconTex1 = EncumbranceMod.Instance.GetTexture( "Buffs/EncumberedDebuff_1" );
					EncumberedDebuff.IconTex2 = EncumbranceMod.Instance.GetTexture( "Buffs/EncumberedDebuff_2" );
					EncumberedDebuff.IconTex3 = EncumbranceMod.Instance.GetTexture( "Buffs/EncumberedDebuff_3" );
					EncumberedDebuff.IconTex4 = EncumbranceMod.Instance.GetTexture( "Buffs/EncumberedDebuff_4" );
					return false;
				} );
				Promises.AddModUnloadPromise( () => {
					EncumberedDebuff.IconTex1 = null;
					EncumberedDebuff.IconTex2 = null;
					EncumberedDebuff.IconTex3 = null;
					EncumberedDebuff.IconTex4 = null;
				} );
			}
		}

		////////////////

		public override void Update( Player player, ref int buff_idx ) {
			var myplayer = player.GetModPlayer<EncumbrancePlayer>();

			float gauge = myplayer.GaugeEncumbrance();

			if( gauge == 0 ) {
				player.ClearBuff( this.Type );
			} else if( gauge < 0.25f ) {
				Main.buffTexture[this.Type] = EncumberedDebuff.IconTex1;
			} else if( gauge < 0.5f ) {
				Main.buffTexture[this.Type] = EncumberedDebuff.IconTex2;
			} else if( gauge < 0.75f ) {
				Main.buffTexture[this.Type] = EncumberedDebuff.IconTex3;
			} else {
				Main.buffTexture[this.Type] = EncumberedDebuff.IconTex4;
			}
		}
	}
}
