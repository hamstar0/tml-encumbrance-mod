using System;
using Terraria;


namespace Encumbrance {
	public static partial class EncumbranceAPI {
		internal static object Call( string call_type, params object[] args ) {
			Player player;

			switch( call_type ) {
			case "GetModSettings":
				return EncumbranceAPI.GetModSettings();
			case "SaveModSettingsChanges":
				EncumbranceAPI.SaveModSettingsChanges();
				return null;
			case "EnableEncumbrance":
				EncumbranceAPI.EnableEncumbrance();
				return null;
			case "DisableEncumbrance":
				EncumbranceAPI.DisableEncumbrance();
				return null;
			default:
				throw new Exception( "No such api call " + call_type );
			}
		}
	}
}
