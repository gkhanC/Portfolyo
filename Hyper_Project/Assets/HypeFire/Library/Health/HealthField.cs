using System;
using HypeFire.Library.Structures;
using UnityEngine;

namespace HypeFire.Library.Health
{
    [Serializable]
    public class HealthField
    {
        public bool isLive => hp.current > 0f;

        [field: SerializeField] public ProgressionVariable hpBar { get; set; } = new ProgressionVariable();

        [field: SerializeField] public NaturalAttributeVariable hp { get; set; } = new NaturalAttributeVariable();

        public void Damage(float damageValue)
        {
            hp.current -= damageValue;
        }

        public void Heal(float healValue)
        {
            hp.current += healValue;
        }

        public void IncreaseHp(float addValue)
        {
            hp.max += addValue;
        }

        public void ReduceHp(float deductedValue)
        {
            hp.max -= deductedValue;
        }
    }
}