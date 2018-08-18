using System;


namespace Encumbrance {
	public static partial class EncumbranceAPI {
		internal static object Call( string call_type, params object[] args ) {
			switch( call_type ) {
			case "GetModSettings":
				return EncumbranceAPI.GetModSettings();
			default:
				throw new Exception( "No such api call " + call_type );
			}
		}
	}
}
