using System.Collections.Generic;
using BetterPoison.Utility;
using UnityEngine;
using ValheimLib;
using ValheimLib.ODB;

namespace BetterPoison.StatusEffects.BetterPoison
{
    public static class ItemData
    {
        public static CustomStatusEffect CustomStatusEffect;
        /*     
           public const string TokenName = "$better_poison";
           public const string TokenValue = "Lead";
   
           public const string TokenDescriptionName = "$custom_item_lead_description";
           public const string TokenDescriptionValue = "Leads are used to leash and lead passive mobs";
   
           public const string CraftingStationPrefabName = "piece_workbench";*/

        internal static void Init()
        {
            AddCustomStatusEffect();

            // Language.AddToken(TokenName, TokenValue);
            // Language.AddToken(TokenDescriptionName, TokenDescriptionValue);
        }

        private static void AddCustomStatusEffect()
        {
            CustomStatusEffect = new CustomStatusEffect(CustomAssetHelper.BetterPoisonScriptableObject, false);
            Debug.LogError(CustomStatusEffect);
            ObjectDBHelper.Add(CustomStatusEffect);
        }
    }
}