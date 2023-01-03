using HyperProject.Shoother;
using Managers;
using UnityEngine;

namespace UI
{
    public class VTypeProjectileUpgradeButton : MonoBehaviour
    {
        [field: SerializeField] public int addProjectileCount { get; set; } = 1;
        private VTypeShotModule _projectile_module = new VTypeShotModule();

        public void Invoke()
        {
            var shotController = GameManager.GetInstance().player.shotController;

            _projectile_module = new VTypeShotModule(shotController);
            _projectile_module.UpdateProjectileCount(shotController.shotModule.projectileCount + addProjectileCount);
            shotController.UpdateShotModule(_projectile_module);
        }
    }
}