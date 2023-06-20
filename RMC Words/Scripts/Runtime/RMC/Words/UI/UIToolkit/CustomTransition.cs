using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace RMC.Words.UI.UIToolkit
{
    //  Namespace Properties ------------------------------

    //  Class Attributes ----------------------------------
    public class CustomTransitionUnityEvent : UnityEvent<CustomTransition>{}
    /// <summary>
    /// Replace with comments...
    /// </summary>
    public class CustomTransition
    {
        //  Events ----------------------------------------
        public readonly CustomTransitionUnityEvent OnTransitionEndEvent = new CustomTransitionUnityEvent();
        public readonly CustomTransitionUnityEvent OnTransitionStartEvent = new CustomTransitionUnityEvent();
        

        //  Properties ------------------------------------
        public VisualElement TargetVisualElement
        {
            get { return _targetVisualElement; }
        }
        
        public string FromClassName
        {
            get { return _fromClassName; }
        }

        public string ToClassName
        {
            get { return _toClassName; }
        }


        //  Fields ----------------------------------------
        private string _samplePublicText;
        private readonly VisualElement _targetVisualElement;
        private readonly string _fromClassName;
        private readonly string _toClassName;

        //  Initialization --------------------------------
        public CustomTransition(VisualElement visualElement, string fromClassName, string toClassName)
        {
            _targetVisualElement = visualElement;
            _fromClassName = fromClassName;
            _toClassName = toClassName;
            RegisterCallback();

        }
        
        public CustomTransition(VisualElement visualElement, string toClassName)
        {
            _targetVisualElement = visualElement;
            _fromClassName = string.Empty;
            _toClassName = toClassName;
            RegisterCallback();
        }

        private void RegisterCallback()
        {
                        
            _targetVisualElement.RegisterCallback<TransitionStartEvent>((evt) =>
            {
                if (evt.currentTarget == TargetVisualElement)
                {
                    OnTransitionStartEvent.Invoke(this);
                }
            
            });
            _targetVisualElement.RegisterCallback<TransitionEndEvent>((evt) =>
            {
                if (evt.currentTarget == TargetVisualElement)
                {
                    OnTransitionEndEvent.Invoke(this);
                }
            });
        }


        //  Methods ---------------------------------------
        public void Start()
        {
            if (!string.IsNullOrEmpty(FromClassName))
            {
                TargetVisualElement.RemoveFromClassList(FromClassName); 
                
                Debug.Log($"AddToClassList (from {FromClassName}, to {ToClassName}) on {TargetVisualElement}");

            }
            else
            {
                Debug.Log($"AddToClassList (to {ToClassName}) on {TargetVisualElement}");
            }
          
            TargetVisualElement.AddToClassList(ToClassName);
        }
        
        //  Event Handlers --------------------------------
    }
}