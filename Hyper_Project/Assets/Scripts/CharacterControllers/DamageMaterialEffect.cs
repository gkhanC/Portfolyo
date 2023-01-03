using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageMaterialEffect : MonoBehaviour
{
    public static DamageMaterialEffect GlobalAccess { get; private set; } = null;
    private float _effectTime;
    private float _timer;
    private bool _isEffectOn;
    [field: SerializeField] public Color effectColor { get; private set; } = Color.red;
    [field: SerializeField] public List<Material> materials { get; private set; } = new List<Material>();
    [field: SerializeField] public List<Color> baseColors { get; private set; } = new List<Color>();

    public void DamageEffect(float effectTime = .1f)
    {
        if (_isEffectOn)
            return;

        _effectTime = effectTime;
        EffectOn();
    }

    public void SetEffectColor(Color c)
    {
        effectColor = c;
    }

    public void SetMaterial(params Material[] mats)
    {
        materials.AddRange(mats);
    }

    private void EffectOn()
    {
        if (materials.Count <= 0)
            return;

        baseColors = new List<Color>();
        for (int i = 0; i < materials.Count; i++)
        {
            var mat = materials[i];
            baseColors.Add(mat.color);
            mat.color = effectColor;
        }

        _timer = 0;
        _isEffectOn = true;
    }

    private void EffectOff()
    {
        if (materials.Count <= 0)
            return;

        if (baseColors.Count <= 0)
            return;

        for (int i = 0; i < materials.Count; i++)
        {
            var mat = materials[i];
            mat.color = baseColors[i];
        }

        _timer = 0;
        _isEffectOn = false;
    }

    private void Awake()
    {
        GlobalAccess = this;
    }

    private void FixedUpdate()
    {
        if (_isEffectOn)
        {
            _timer += Time.deltaTime;
            if (_timer >= _effectTime)
            {
                EffectOff();
            }
        }
    }

    private void OnDisable()
    {
        EffectOff();
    }

    private void OnDestroy()
    {
        EffectOff();
    }
}