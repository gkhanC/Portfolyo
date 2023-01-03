using System;
using HypeFire.Library.Utilities.Extensions.Object;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DamageVintage : MonoBehaviour
    {
        public static DamageVintage GlobalAccess { get; private set; }

        private float _lifeTime = .1f;
        private float _lerpStep = .1f;
        private float _targetAlpha = 255f;
        private bool _isFlash;

        private float _baseAlpha = 0f;
        private float _tolerance = .1f;
        private bool _isDamping = false;
        private bool _isInvoked = false;

        private float _lifeTimer;
        [field: SerializeField] public Image vintageImage { get; private set; } = null;

        private void Awake()
        {
            GlobalAccess = this;
        }

        private void Start()
        {
            if (vintageImage.IsNull())
            {
                vintageImage = GetComponent<Image>();
            }
        }

        private void FixedUpdate()
        {
            if (_isInvoked)
            {
                var currentColor = vintageImage.color;
                if (!_isDamping)
                {
                    var offset = Mathf.Abs(currentColor.a - _targetAlpha);
                    if (offset > _tolerance)
                    {
                        currentColor.a = Mathf.Lerp(vintageImage.color.a, _targetAlpha, _lerpStep);
                        vintageImage.color = currentColor;
                        return;
                    }

                    currentColor.a = _targetAlpha;
                    vintageImage.color = currentColor;
                    _isDamping = _isFlash;
                    return;
                }

                if (_isFlash)
                {
                    if (_lifeTimer < _lifeTime)
                    {
                        _lifeTimer += Time.deltaTime;
                        return;
                    }

                    var offset = Mathf.Abs(currentColor.a - _baseAlpha);
                    if (offset > _tolerance)
                    {
                        currentColor.a = Mathf.Lerp(vintageImage.color.a, _baseAlpha, _lerpStep);
                        vintageImage.color = currentColor;
                        return;
                    }

                    currentColor.a = _baseAlpha;
                    vintageImage.color = currentColor;
                    _lifeTimer = 0f;
                    _isDamping = false;
                    _isInvoked = false;
                    return;
                }
            }
        }

        public void OnVintage(float lifeTime = .1f, float lerStep = .5f, float targetAlpha = .4f, bool isFlash = true,
            float baseAlpha = 0f, float tolerance = .1f)
        {
            if (vintageImage.IsNull() || _isInvoked)
                return;

            _lifeTime = lifeTime;
            _lerpStep = lerStep;
            _targetAlpha = targetAlpha;
            _baseAlpha = baseAlpha;
            _tolerance = tolerance;
            _isFlash = isFlash;
            _isInvoked = true;
        }
    }
}