using UnityEngine;
using UnityEngine.Events;

namespace HypeFire.Library.Events
{
    public class EventBase : MonoBehaviour
    {
        public string checkTag = "Unknow";
        
        [field: SerializeField]
        public UnityEvent enterAction { get; set; } = new UnityEvent();
        
        [field: SerializeField]
        public UnityEvent stayAction { get; set; } = new UnityEvent();
        
        [field: SerializeField]
        public UnityEvent exitAction { get; set; } = new UnityEvent();
    }
}