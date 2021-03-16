using HarmonyLib;
using HarmonyLib.Tools;
using UnityEngine;
using ValheimLib;

namespace BetterPoison.GameClasses
{
    [HarmonyPatch(typeof(SE_Poison), "AddDamage")]
    public static class ImprovedPoisonDamage
    {
        private static void Prefix(ref SE_Poison __instance, ref float damage)
        {
            if (BetterPoisonConfig.AllowStackingPoison.Value)
            {
                damage += __instance.m_damageLeft;
            }
        }
    }
}