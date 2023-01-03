using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using HypeFire.Library.Utilities.Extensions.Object;
using UnityEngine;
using Random = UnityEngine.Random;

public class ScreenShakeManager : MonoBehaviour
{
    public static ScreenShakeManager GloballAccess { get; set; } = null;

    [field: SerializeField] public CinemachineVirtualCamera virtualCamera { get; private set; } = null;

    private bool _isOnShake = false;

    [Tooltip("Shake offset range.")]
    [field: SerializeField]
    private float _range { get; set; } = .5f;

    private float _timer;

    CinemachineBasicMultiChannelPerlin _noise;

    public ScreenShakeManager()
    {
    }

    private void Awake()
    {
        GloballAccess = this;
    }

    private void Start()
    {
        if (virtualCamera.IsNotNull())
        {
            _noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
    }

    private void FixedUpdate()
    {
        if (_isOnShake)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0f)
            {
                NoiseReset();
                _isOnShake = false;
            }
        }
    }

    public Vector3 GetRandomOffset() => (Vector3.one * .5f) + new Vector3(Random.Range(_range * -1, _range),
        Random.Range(_range * -1, _range), Random.Range(_range * -1, _range));

    public void Shake(float amplitudeGain = 10f, float frequencyGain = 10f, float time = .1f)
    {
        if (_noise.IsNull() || _isOnShake) return;

        _noise.m_PivotOffset = GetRandomOffset();
        _noise.m_AmplitudeGain = amplitudeGain;
        _noise.m_FrequencyGain = frequencyGain;
        _timer = time;
        _isOnShake = true;
    }

    private void NoiseReset()
    {
        _noise.m_AmplitudeGain = 0f;
        _noise.m_FrequencyGain = 0f;
        _timer = 0f;
    }
}