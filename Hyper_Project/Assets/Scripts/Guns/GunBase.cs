using Guns.Abstract;
using UnityEditor.Animations;
using UnityEngine;

namespace Guns
{
    public class GunBase : MonoBehaviour, IGun
    {
        [field: SerializeField] public GunType GunType { get; private set; }
        [field: SerializeField] public GameObject GunPrefab { get; private set; }
        [field: SerializeField] public AnimatorController animatorController { get; private set; }

        public void GunActive()
        {
            throw new System.NotImplementedException();
        }
    }

    public enum GunType
    {
        Pistol,
        Uzi
    }
}