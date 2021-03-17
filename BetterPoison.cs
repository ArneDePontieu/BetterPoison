using BepInEx;
using BetterPoison.StatusEffects.BetterPoison;
using BetterPoison.Utility;

namespace BetterPoison
{
    [BepInDependency(ValheimLib.ValheimLib.ModGuid, BepInDependency.DependencyFlags.HardDependency)]
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

            CustomAssetHelper.Init();

            ItemData.Init();
        }

        private void OnDestroy()
        {
            Initializer.Disable();
        }
    }
}