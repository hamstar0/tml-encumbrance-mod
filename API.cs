namespace Encumbrance {
	public static partial class EncumbranceAPI {
		public static EncumbranceConfigData GetModSettings() {
			return EncumbranceMod.Instance.Config;
		}
	}
}
