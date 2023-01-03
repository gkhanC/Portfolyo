using UnityEditor.Animations;
using UnityEngine;

namespace Guns.Abstract
{
    public interface IGun
    {
        public GunType GunType { get; }
        public GameObject GunPrefab { get; }
        public AnimatorController animatorController { get; }
        public void GunActive();
    }
}