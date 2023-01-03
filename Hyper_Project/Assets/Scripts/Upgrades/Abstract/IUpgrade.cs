using CharacterControllers;
using HyperProject.Abstract;
using UnityEngine;

namespace Upgrades.Abstract
{
    public interface IUpgrade
    {
        GameObject TakeGameObject();
        void Apply(IPlayer player);
    }
}