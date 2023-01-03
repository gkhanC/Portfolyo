using HyperProject.Shoother;
using UnityEngine;

namespace HyperProject.Abstract
{
    public interface IPlayer
    {
        public GameObject controllerObject { get; }
        public ShotController shotController { get; }
        public CharacterAnimator characterAnimator { get; }

        public void SetTarget(GameObject target);

        public void Damage(float damageValue);
        public void AddScore(int scoreValue);
        public void SetAttackSpeed(float attackSpeedValue);
        public void SetCharacterSpeed(float characterSpeedValue);
        public void SetEnergy(float energyValue);
        public void SetHp(float hpValue);
        public void ShotInvoke();
        public void OnShot();
        public void ShotCompleted();
    }
}