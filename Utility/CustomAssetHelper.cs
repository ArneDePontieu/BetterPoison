using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using ValheimLib;
using ValheimLib.ODB;
using UnityObject = UnityEngine.Object;

namespace BetterPoison.Utility
{
    public class CustomAssetHelper
    {
        private const string AssetBundleName = "betterpoison";
        private static AssetBundle BetterPoisonAssetBundle;

        public const string BetterPoisonIconPath = "Assets/Mock/BetterPoison_Icon.png";
        public static SE_BetterPoison BetterPoisonScriptableObject;

        public static void Init()
        {
            BetterPoisonAssetBundle = GetAssetBundleFromResources(AssetBundleName);

            Sprite newSprite = BetterPoisonAssetBundle.LoadAsset<Sprite>(BetterPoisonIconPath);

            BetterPoisonScriptableObject = ScriptableObject.CreateInstance<SE_BetterPoison>();
            BetterPoisonScriptableObject.m_damageInterval = 1f;
            BetterPoisonScriptableObject.m_baseTTL = 1f;
            BetterPoisonScriptableObject.m_TTLPerDamage = 1f;
            BetterPoisonScriptableObject.m_TTLPerDamagePlayer = 5f;
            BetterPoisonScriptableObject.m_TTLPower = 0.5f;
            BetterPoisonScriptableObject.m_icon = newSprite;
            BetterPoisonScriptableObject.name = "BetterPoison";
        }

        private static AssetBundle GetAssetBundleFromResources(string fileName)
        {
            var execAssembly = Assembly.GetExecutingAssembly();

            var resourceName = execAssembly.GetManifestResourceNames()
                .Single(str => str.EndsWith(fileName));

            using var stream = execAssembly.GetManifestResourceStream(resourceName);

            return AssetBundle.LoadFromStream(stream);
        }
    }
}