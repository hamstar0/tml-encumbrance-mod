using HamstarHelpers.Helpers.DebugHelpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;


namespace Encumbrance {
	partial class EncumbranceMod : Mod {
		public override void ModifyInterfaceLayers( List<GameInterfaceLayer> layers ) {
			int layer_idx = layers.FindIndex( layer => layer.Name.Equals( "Vanilla: Inventory" ) );
			if( layer_idx == -1 ) { return; }

			GameInterfaceDrawMethod inv_over = delegate {
				if( !Main.playerInventory ) { return true; }

				var mymod = EncumbranceMod.Instance;
				Player plr = Main.LocalPlayer;

				float inv_scale = 0.85f;
				if( (plr.chest != -1 || Main.npcShop > 0) && !Main.recBigList ) {
					inv_scale = 0.755f;
				}

				for( int i = 0; i < 10; i++ ) {
					for( int j = 0; j < 5; j++ ) {
						int idx = i + j * 10;

						if( idx < mymod.Config.CarryCapacityBase ) {
							continue;
						}

						Texture2D tex = Main.inventoryBackTexture;
						int pos_x = (int)( 20f + ((float)i * 56f) * inv_scale );
						int pos_y = (int)( 20f + ((float)j * 56f) * inv_scale );
						var pos = new Vector2( pos_x, pos_y );
						
						Main.spriteBatch.Draw( tex, pos, null, Color.White * 0.5f, 0f, default( Vector2 ), inv_scale, SpriteEffects.None, 1f );
					}
				}

				return true;
			};

			var inv_over_layer = new LegacyGameInterfaceLayer( "Encumbrance: Inventory Overlay", inv_over, InterfaceScaleType.UI );

			layers.Insert( layer_idx + 1, inv_over_layer );
		}
	}
}
