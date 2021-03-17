using HarmonyLib;
using HarmonyLib.Tools;
using UnityEngine;
using ValheimLib;

namespace BetterPoison.GameClasses
{ 

    [HarmonyPatch(typeof(Character), "AddPoisonDamage")]
    public static class CancelOldPoisonDamage
    {
        private static bool Prefix(ref Character __instance, ref float damage)
        {
            return false;
        }
    }

    [HarmonyPatch(typeof(Character), "ApplyDamage")]
    public static class ImprovedPoisonDamage
    {
        public static void Prefix(ref Character __instance, HitData hit)
        {
            float poisonDamage = hit.m_damage.GetTotalDamage();
            AddPoisonDamage(__instance, hit.GetAttacker(), poisonDamage);
        }

        private static void AddPoisonDamage(Character character, Character attacker, float damage)
        {
            if ((double) damage <= 0.0)
            {
                Debug.Log("Return because 0 poison damage");
                return;
            }

            if (!character)
            {
                Debug.Log("Character is null");
                return;
            }
            
            if (!attacker)
            {
                Debug.Log("Attacker is null");
                return;
            }

            SE_BetterPoison sePoison = character.m_seman.GetStatusEffect("BetterPoison") as SE_BetterPoison;

            if (sePoison == null)
            {
                sePoison = character.m_seman.AddStatusEffect("BetterPoison") as SE_BetterPoison;
            }
            
            Debug.Log("Adding poison damage");
            sePoison.AddDamage(attacker, damage);
        }
    }
}