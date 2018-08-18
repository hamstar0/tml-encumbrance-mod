using HamstarHelpers.Services.Promises;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;


namespace Encumbrance.Buffs {
	class EncumberedDebuff : ModBuff {
		private static bool HasSetPromise = false;
		private static Texture2D IconTex1;
		private static Texture2D IconTex2;
		private static Texture2D IconTex3;
		private static Texture2D IconTex4;


		////////////////
		
		public static void ApplyMovementHinderance( Player player ) {
			var myplayer = player.GetModPlayer<EncumbrancePlayer>();
			float scale = 1f - myplayer.GaugeEncumbrance();
			
			player.maxRunSpeed *= scale;
			player.accRunSpeed = player.maxRunSpeed;
			player.moveSpeed *= scale;

			int max_jump = (int)( Player.jumpHeight * scale );
			if( player.jump > max_jump ) { player.jump = max_jump; }
		}



		////////////////

		public override void SetDefaults() {
			this.DisplayName.SetDefault( "Encumbered" );
			this.Description.SetDefault( "You're weighted down" + '\n' + "More items = more burden" );

			Main.debuff[ this.Type ] = true;

			if( !Main.dedServ && EncumberedDebuff.IconTex1 == null ) {
				EncumberedDebuff.IconTex1 = EncumbranceMod.Instance.GetTexture( "Buffs/EncumberedDebuff_1" );
				EncumberedDebuff.IconTex2 = EncumbranceMod.Instance.GetTexture( "Buffs/EncumberedDebuff_2" );
				EncumberedDebuff.IconTex3 = EncumbranceMod.Instance.GetTexture( "Buffs/EncumberedDebuff_3" );
				EncumberedDebuff.IconTex4 = EncumbranceMod.Instance.GetTexture( "Buffs/EncumberedDebuff_4" );
				
				if( !EncumberedDebuff.HasSetPromise ) {
					EncumberedDebuff.HasSetPromise = true;

					Promises.AddValidatedPromise<PlayerMovementPromiseArguments>( EncumbrancePlayer.PlayerMovementPromiseValidator, ( args ) => {
						Player plr = Main.player[ args.Who ];

						if( plr != null && plr.active ) {
							EncumberedDebuff.ApplyMovementHinderance( plr );
						}
						return true;
					} );
				}

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
