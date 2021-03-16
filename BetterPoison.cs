using BepInEx;

namespace BetterPoison
{
    [BepInPlugin(ModGuid, ModName, ModVer)]
    public class BetterPoison : BaseUnityPlugin
    {
        public const string ModGuid = AuthorName + "." + ModName;
        private const string AuthorName = "Hedrymas";
        private const string ModName = "BetterPoison";
        private const string ModVer = "1.0.0";

        internal static BetterPoison Instance;

        private void Awake()
        {
            Instance = this;

            Initializer.Initialize();
            BetterPoisonConfig.Initialize(Config);
        }

        private void OnDestroy()
        {
            Initializer.Disable();
        }
    }
}