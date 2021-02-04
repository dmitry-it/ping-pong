using UnityEngine;
using UnityEngine.Events;

namespace Popups
{
    public abstract class Popup : MonoBehaviour
    {
        public readonly UnityEvent OnCloseEvent = new UnityEvent();

        public void Close()
        {
            OnCloseEvent.Invoke();
            Destroy(gameObject);
        }
    }
}