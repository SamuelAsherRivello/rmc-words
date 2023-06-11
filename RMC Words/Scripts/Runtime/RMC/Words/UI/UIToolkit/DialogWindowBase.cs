using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace RMC.Words.UI.UIToolkit
{
    /// <summary>
    /// Create the parent class of any WINDOW to be attached at runtime.
    /// Ideally the contents are added separately (child class?, tbd)
    /// </summary>
    public class DialogWindowBase : CustomVisualElement
    {
        //  Internal Classes ----------------------------------
        /// <summary>
        /// 
        /// </summary>
        public class DialogWindowUnityEvent : UnityEvent<DialogWindowBase>{}

        
        /// <summary>
        /// 
        /// </summary>
        public new class UxmlTraits : VisualElement.UxmlTraits {}
    
        /// <summary>
        /// 
        /// </summary>
        public new class UxmlFactory: UxmlFactory<DialogWindowBase, UxmlTraits>
        {
            public override VisualElement Create(IUxmlAttributes bag, CreationContext cc)
            {
                VisualTreeAsset visualTreeAsset = Resources.Load<VisualTreeAsset>("Uxml/DialogWindowBaseLayout");
                VisualElement rootVisualElement = base.Create(bag, cc);
                visualTreeAsset.CloneTree(rootVisualElement);

                DialogWindowBase dialogWindowBase = rootVisualElement.Q<DialogWindowBase>();
                
                //////////////////////////////////////////
                // Initialize - Is this only at edit-time?
                //////////////////////////////////////////
                dialogWindowBase.Initialize();
                
                return rootVisualElement;
            }
        }



        //  Events ----------------------------------------
        public readonly DialogWindowUnityEvent OnHideRequested = new DialogWindowUnityEvent();

        //  Properties ------------------------------------
        public override VisualElement contentContainer => _contentContainer;
        public VisualElement Window { get { return _window;} }

        //  Fields ----------------------------------------
        private VisualElement _contentContainer;
        private VisualElement _window;

        //  Initialization --------------------------------
        public DialogWindowBase()
        {
            VisualTreeAsset visualTreeAsset = Resources.Load<VisualTreeAsset>("Uxml/DialogWindowBaseLayout");
            visualTreeAsset.CloneTree(this);
            
            ////////////////////////////////////////
            // Initialize - Is this only at runtime?
            ////////////////////////////////////////
            Initialize();
        }

        public override void Initialize()
        {
            if (!IsInitialized)
            {
                base.Initialize();
                
                DialogWindowBase dialogWindowBase = this.Q<DialogWindowBase>();
                dialogWindowBase.style.flexGrow = new StyleFloat(1);
                dialogWindowBase.style.width = new StyleLength(new Length(100, LengthUnit.Percent));
                dialogWindowBase.RegisterCallback<ClickEvent>(OnClickEvent);
                
                //
                _window = dialogWindowBase.Q("Window");
                _contentContainer = dialogWindowBase.Q("ContentContainer");
            }
        }

        //  Methods ---------------------------------------


        //  Event Handlers --------------------------------
        private void OnClickEvent(ClickEvent evt)
        {
            Debug.Log("yes");
            OnHideRequested.Invoke(this);
        }
    }
}