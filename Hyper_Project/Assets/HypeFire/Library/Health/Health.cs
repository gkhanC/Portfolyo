using System;
using System.Collections.Generic;
using HypeFire.Library.Structures;
using HypeFire.Library.Utilities.Extensions.Object;
using UnityEngine;

namespace HypeFire.Library.Health
{
    public class Health : MonoBehaviour
    {
        public bool isLive => hp.current > 0f;
        [field: SerializeField] public List<BarSlots> hpBars { get; set; } = new List<BarSlots>();

        [field: SerializeField]
        public NaturalAttributeVariable hp { get; private set; } = new NaturalAttributeVariable();

        public float fill => hpBars[0].bar.fillAmount;
        private bool fillChanged = false;

        public void Damage(float damageValue)
        {
            hp.current -= damageValue;
            fillChanged = true;
        }

        public void Heal(float healValue)
        {
            hp.current += healValue;
            fillChanged = true;
        }

        public void IncreaseHp(float increaseValue)
        {
            if (Math.Abs(hp.current - hp.max) < .1f)
            {
                hp.max += increaseValue;
                hp.current = hp.max;
                return;
            }

            hp.max += increaseValue;
        }

        public void ReduceHp(float reduceValue) => hp.max = reduceValue;


        private void Update()
        {
            if (fillChanged)
            {
                foreach (var bar in hpBars)
                {
                    if (bar.IsNotNull() && bar.isUsable)
                    {
                        bar.Animate(hp.fill);
                    }
                }

                fillChanged = hpBars[^1].fillChanged;
            }
        }

        [Serializable]
        public class BarSlots
        {
            public bool isUsable => (bar.IsNotNull() && bar.image.IsNotNull());

            public bool fillChanged = false;

            [field: SerializeField, Range(0f, .25f)]
            public float animationTime { get; set; } = .25f;

            [field: SerializeField] public AnimationCurve damageAnimationCurve { get; set; } = new AnimationCurve();
            [field: SerializeField] public AnimationCurve healAnimationCurve { get; set; } = new AnimationCurve();
            [field: SerializeField] public ProgressionVariable bar { get; set; } = new ProgressionVariable();


            private float _last_fill = 0f;
            private float _timer = 0f;
            private bool _is_lerp_direction_up = false;

            public void Animate(float targetFill)
            {
                if (!isUsable)
                    return;

                if (_last_fill != targetFill)
                {
                    if (_is_lerp_direction_up)
                    {
                        if (targetFill < _last_fill)
                        {
                            _timer = 0f;
                        }
                    }
                    else
                    {
                        if (targetFill > _last_fill)
                        {
                            _timer = 0f;
                        }
                    }

                    _is_lerp_direction_up = _last_fill < targetFill;

                    _last_fill = targetFill;
                }

                if (_last_fill != bar.fillAmount)
                {
                    fillChanged = true;
                    var curve = bar.fillAmount >= _last_fill ? damageAnimationCurve : healAnimationCurve;

                    _timer += Time.deltaTime;
                    bar.fillAmount = Mathf.Lerp(bar.fillAmount, _last_fill,
                        animationTime * curve.Evaluate(_timer));
                    var diff = Mathf.Abs((bar.fillAmount - _last_fill));
                    if (diff < .005f)
                    {
                        bar.fillAmount = _last_fill;
                        fillChanged = false;
                    }
                }
            }
        }
    }
}