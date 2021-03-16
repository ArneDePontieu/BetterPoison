using HarmonyLib;

namespace BetterPoison
{
    internal static class Initializer
    {
        internal static Harmony HarmonyInstance;

        internal static void Initialize()
        {
            EnableHarmonyPatches();
        }

        internal static void Disable()
        {
            DisableHarmonyPatches();
        }

        #region Harmony

        private static void EnableHarmonyPatches()
        {
            HarmonyInstance = new Harmony(BetterPoison.ModGuid);
            HarmonyInstance.PatchAll();
        }

        private static void DisableHarmonyPatches()
        {
            HarmonyInstance.UnpatchSelf();
        }

        #endregion
    }
}