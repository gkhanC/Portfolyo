using UnityEngine;

namespace HypeFire.Library.Events
{
    public class EventTriggerWithTrigger : EventObject
    {
        public string triggerTag = "Unknow";

        public void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.CompareTag(triggerTag))
            {
                objectEvent.Invoke();
            }
        }
    }
}