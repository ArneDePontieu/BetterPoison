using BepInEx;
using BepInEx.Configuration;

namespace BetterPoison
{
    public static class BetterPoisonConfig
    {
        public static ConfigEntry<bool> AllowStackingPoison;

        public static void Initialize(ConfigFile configFile)
        {
            AllowStackingPoison = configFile.Bind("General", "AllowStackingPoison", true,
                "Makes poison stack so every poison deals its full damage.");
        }
    }
}