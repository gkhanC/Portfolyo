using UnityEngine;
using UnityEngine.Events;

namespace HyperProject.Shoother
{
    public class MuzzleController : MonoBehaviour
    {
        public UnityEvent muzzle_launch = new UnityEvent();
    }
}