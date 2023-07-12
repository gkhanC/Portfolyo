using UnityEngine;
using UnityEngine.Events;

namespace HypeFire.Library.Events
{
    [RequireComponent(typeof(Collider))]
    public class EventObject : MonoBehaviour
    {
        public UnityEvent objectEvent = new UnityEvent();
    }
}