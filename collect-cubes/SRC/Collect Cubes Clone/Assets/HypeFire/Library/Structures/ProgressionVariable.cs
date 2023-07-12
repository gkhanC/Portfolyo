using System;
using HypeFire.Library.Utilities.Extensions.Object;
using UnityEngine;
using UnityEngine.UI;

namespace HypeFire.Library.Structures
{
    [Serializable]
    public class ProgressionVariable
    {
        [field: SerializeField] public Image image { get; set; } = null;

        public float fillAmount
        {
            get => image.IsNotNull() ? image.fillAmount : 0f;
            set
            {
                if (image.IsNotNull())
                {
                    image.fillAmount = value;
                }

                return;
            }
        }
    }
}