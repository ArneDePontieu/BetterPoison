using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SE_BetterPoison : StatusEffect
{
    public float m_damageInterval = 1f;
    public float m_baseTTL = 2f;
    public float m_TTLPerDamagePlayer = 2f;
    public float m_TTLPerDamage = 2f;
    public float m_TTLPower = 0.5f;


    private class PoisonStack
    {
        public float timeLeft = 0f;
        public float m_timer;
        public float m_damageLeft = 0f;
        public float m_damagePerHit;
    }

    private Dictionary<Character, PoisonStack> poisonDictionary = new Dictionary<Character, PoisonStack>();

    private List<Character> keysToRemove = new List<Character>();

    public override void UpdateStatusEffect(float dt)
    {
        base.UpdateStatusEffect(dt);

        keysToRemove.Clear();

        foreach (var pair in poisonDictionary)
        {
            PoisonStack poisonStack = pair.Value;
            poisonStack.m_timer -= dt;
            if ((double) poisonStack.m_timer > 0.0)
                return;

            poisonStack.m_timer = m_damageInterval;

            HitData hit = new HitData();
            hit.m_point = this.m_character.GetCenterPoint();
            hit.m_damage.m_poison = poisonStack.m_damagePerHit;
            poisonStack.m_damageLeft -= poisonStack.m_damagePerHit;
            poisonStack.timeLeft -= dt;

            if (poisonStack.timeLeft <= 0)
            {
                keysToRemove.Add(pair.Key);
            }

            this.m_character.ApplyDamage(hit, true, false);
        }

        for (int i = 0; i < keysToRemove.Count; i++)
        {
            poisonDictionary.Remove(keysToRemove[i]);
        }
    }

    public void AddDamage(Character damageSource, float damage)
    {
        PoisonStack poisonStack = GetOrCreatePoisonStack(damageSource);
        poisonStack.m_damageLeft += damage;
        poisonStack.timeLeft = m_baseTTL +
                               Mathf.Pow(
                                   poisonStack.m_damageLeft * (this.m_character.IsPlayer()
                                       ? this.m_TTLPerDamagePlayer
                                       : this.m_TTLPerDamage), this.m_TTLPower);

        int num = (int) ((double) poisonStack.timeLeft / (double) this.m_damageInterval);
        poisonStack.m_damagePerHit = poisonStack.m_damageLeft / (float) num;
        m_ttl = GetLongestPoisonDuration();

#if DEBUG
        Debug.Log(
            $"Applying {damage} poison damage from source {damageSource.m_name} over {num} ticks every {m_damageInterval} seconds for a total of {poisonStack.timeLeft}. \n Also refreshed the entire debuff duration to {m_ttl} seconds.");
#endif

        ResetTime();
    }

    private PoisonStack GetOrCreatePoisonStack(Character damageSource)
    {
        if (!poisonDictionary.TryGetValue(damageSource, out _))
        {
            poisonDictionary[damageSource] = new PoisonStack();
        }

        return poisonDictionary[damageSource];
    }

    /// <summary>
    /// Loop through the poison stacks and check which poison has the most time left
    /// </summary>
    /// <returns></returns>
    private float GetLongestPoisonDuration()
    {
        float longestDuration = 0f;

        foreach (var pair in poisonDictionary)
        {
            if (pair.Value.timeLeft > longestDuration)
            {
                longestDuration = pair.Value.timeLeft;
            }
        }

        return longestDuration;
    }
}