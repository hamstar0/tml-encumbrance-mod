using HamstarHelpers.Helpers.TModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;


namespace Encumbrance {
	partial class EncumbranceMod : Mod {
		public override void ModifyInterfaceLayers( List<GameInterfaceLayer> layers ) {
			int layerIdx = layers.FindIndex( layer => layer.Name.Equals( "Vanilla: Inventory" ) );
			if( layerIdx == -1 ) { return; }

			GameInterfaceDrawMethod invOver = delegate {
				if( !Main.playerInventory ) {
					return true;
				}

				var mymod = EncumbranceMod.Instance;
				if( mymod.Config.BurdenedItemSlotOverlayOpacity == 0f ) {
					return true;
				}

				var myplayer = (EncumbrancePlayer)TmlHelpers.SafelyGetModPlayer( Main.LocalPlayer, this, "EncumbrancePlayer" );
				int capacity = myplayer.GetCurrentCapacity();

				float invScale = 0.85f;
				//if( (plr.chest != -1 || Main.npcShop > 0) && !Main.recBigList ) {
				//	inv_scale = 0.755f;
				//}

				for( int i = 0; i < 10; i++ ) {
					for( int j = 0; j < 5; j++ ) {
						int idx = i + j * 10;

						if( idx < capacity ) {
							continue;
						}

						Texture2D tex = this.ShadowBox;
						int posX = (int)( 20f + ((float)i * 56f) * invScale );
						int posY = (int)( 20f + ((float)j * 56f) * invScale );
						var pos = new Vector2( posX, posY );
						Color color = Color.White * mymod.Config.BurdenedItemSlotOverlayOpacity;
						
						Main.spriteBatch.Draw( tex, pos, null, color, 0f, default( Vector2 ), invScale, SpriteEffects.None, 1f );
					}
				}

				return true;
			};

			var invOverLayer = new LegacyGameInterfaceLayer( "Encumbrance: Inventory Overlay", invOver, InterfaceScaleType.UI );

			layers.Insert( layerIdx + 1, invOverLayer );
		}
	}
}
