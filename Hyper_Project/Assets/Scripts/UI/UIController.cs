using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        public static UIController GlobalAccess { get; private set; }
        public TextMeshProUGUI energyText;
        public TextMeshProUGUI speedText;
        public TextMeshProUGUI hpText;
        public TextMeshProUGUI scoreText;

        private void Awake()
        {
            GlobalAccess = this;
        }

        public void UpdateEnergy(float energyValue)
        {
            energyText.text = energyValue.ToString();
        }

        public void UpdateSpeed(float speedBonus)
        {
            speedText.text = speedBonus.ToString();
        }

        public void UpdateScore(int Value)
        {
            scoreText.text = $"Score <b>{Value}</b>";
        }
    }
}