using RMC.Core.Exceptions;
using RMC.Core.Interfaces;
using RMC.Words.Extensions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace RMC.Words.UI.UIToolkit
{
    /// <summary>
    /// Experiment to create some reusable functionality for RMC
    /// </summary>
    public class CustomVisualElement : VisualElement, IInitializable
    {
        //  Internal Classes ----------------------------------
        public class CustomVisualElementUnityEvent : UnityEvent<VisualElement>{}

        //  Events ----------------------------------------
        public readonly CustomVisualElementUnityEvent OnAdded = new CustomVisualElementUnityEvent();
        public readonly CustomVisualElementUnityEvent OnRemoved = new CustomVisualElementUnityEvent();

        //  Properties ------------------------------------
        public VisualElement Content { get { return _content;} protected set { _content = value;}}
        public bool IsInitialized { get { return _isInitialized;} protected set { _isInitialized = value;}}
        
        //  Fields ----------------------------------------
        private VisualElement _content;
        private bool _isInitialized;

        //  Initialization --------------------------------
        public virtual void Initialize()
        {
            if (!IsInitialized)
            {
                IsInitialized = true;
                Content = this.Q("Content");
            }

        }

        public void RequireIsInitialized()
        {
            if (!IsInitialized)
            {
                throw new NotInitializedException(this);
            }
        }


        //  Methods ---------------------------------------
        public new void Add (VisualElement child)
        {
            base.Add(child);
            OnAddedInternal(child);
        }
        
        public new void Remove (VisualElement child)
        {
            base.Remove(child);
            OnRemovedInternal(child);
        }
        

        //  Event Handlers --------------------------------
        protected virtual void OnAddedInternal (VisualElement child)
        {
            RequireIsInitialized();
            OnAdded.Invoke(child);
        }
        
        protected virtual void OnRemovedInternal (VisualElement child)
        {
            RequireIsInitialized();
            OnRemoved.Invoke(child);
        }
 
    }
}
       
