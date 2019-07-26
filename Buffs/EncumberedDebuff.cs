using HamstarHelpers.Services.Hooks.LoadHooks;
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

			int maxJump = (int)( Player.jumpHeight * scale );
			if( player.jump > maxJump ) { player.jump = maxJump; }
		}



		////////////////

		public override void SetDefaults() {
			int mytype = this.Type;

			this.DisplayName.SetDefault( "Encumbered" );
			this.Description.SetDefault( "You're weighted down" + '\n' + "More items = more burden" );

			Main.debuff[mytype] = true;

			if( !Main.dedServ && EncumberedDebuff.IconTex1 == null ) {
				EncumberedDebuff.IconTex1 = EncumbranceMod.Instance.GetTexture( "Buffs/EncumberedDebuff_1" );
				EncumberedDebuff.IconTex2 = EncumbranceMod.Instance.GetTexture( "Buffs/EncumberedDebuff_2" );
				EncumberedDebuff.IconTex3 = EncumbranceMod.Instance.GetTexture( "Buffs/EncumberedDebuff_3" );
				EncumberedDebuff.IconTex4 = EncumbranceMod.Instance.GetTexture( "Buffs/EncumberedDebuff_4" );
				
				if( !EncumberedDebuff.HasSetPromise ) {
					EncumberedDebuff.HasSetPromise = true;

					CustomLoadHooks.AddHook( EncumbrancePlayer.PlayerMovementPromiseValidator, ( whoAmI ) => {
						Player plr = Main.player[ whoAmI ];

						if( plr != null && plr.active && plr.HasBuff(mytype) ) {
							EncumberedDebuff.ApplyMovementHinderance( plr );
						}
						return true;
					} );
				}

				LoadHooks.AddModUnloadHook( () => {
					EncumberedDebuff.IconTex1 = null;
					EncumberedDebuff.IconTex2 = null;
					EncumberedDebuff.IconTex3 = null;
					EncumberedDebuff.IconTex4 = null;
				} );
			}
		}

		////////////////

		public override void Update( Player player, ref int buffIdx ) {
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
