using UnityEngine;
using UnityEngine.UIElements;

namespace RMC.Words.Extensions
{
    /// <summary>
    /// Extension methods for UnityEngine.VisualElement.
    /// </summary>
    public static class VisualElementExtensions
    {
        public static void RegisterCallbackOneShot<TEventType>(this VisualElement visualElement, 
            EventCallback<TEventType> eventCallback) where TEventType : EventBase<TEventType>, new()
        {
            
            EventCallback<TEventType> eventCallbackParent = null;
            eventCallbackParent = (evt) =>
            {
                eventCallback.Invoke(evt);
                visualElement.UnregisterCallback(eventCallbackParent);
                Debug.Log("Unregistered!");
            };
            visualElement.RegisterCallback(eventCallbackParent);
        }
    }
}