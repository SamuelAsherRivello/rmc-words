using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace RMC.Words.UI.UIToolkit
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class DialogWindowSystem : CustomVisualElement
    {
        //  Internal Classes ----------------------------------
        public class DialogWindowSystemUnityEvent : UnityEvent<DialogWindowSystem>{}


        /// <summary>
        /// 
        /// </summary>
        public new class UxmlTraits : VisualElement.UxmlTraits {}
    
        /// <summary>
        /// 
        /// </summary>
        public new class UxmlFactory : UxmlFactory<DialogWindowSystem, UxmlTraits>
        {
            public override VisualElement Create(IUxmlAttributes bag, CreationContext cc)
            {
                VisualTreeAsset visualTreeAsset = Resources.Load<VisualTreeAsset>("Uxml/DialogWindowSystemLayout");
                VisualElement rootVisualElement = base.Create(bag, cc);
                visualTreeAsset.CloneTree(rootVisualElement);
                
                DialogWindowSystem dialogWindowSystem = rootVisualElement.Q<DialogWindowSystem>();
                dialogWindowSystem.Initialize();
            
                return rootVisualElement;
            }
        }

        //  Events ----------------------------------------
        public readonly DialogWindowSystemUnityEvent OnShowDialogWindow = new DialogWindowSystemUnityEvent();
        public readonly DialogWindowSystemUnityEvent OnHideDialogWindow = new DialogWindowSystemUnityEvent();

        //  Properties ------------------------------------
        public DialogWindowBase DialogWindowCurrent { get { return _dialogWindowCurrent;}}
        public bool HasDialogWindowCurrent { get { return DialogWindowCurrent != null;}}

        
        //  Fields ----------------------------------------
        private DialogWindowBase _dialogWindowCurrent;

        //  Initialization --------------------------------

        //  Methods ---------------------------------------
        
        public void ShowDialogWindow(DialogWindowBase dialogWindow)
        {
            HideDialogWindow();
            style.visibility = new StyleEnum<Visibility>(Visibility.Visible);
            _dialogWindowCurrent = dialogWindow;
            _dialogWindowCurrent.OnHideRequested.AddListener(OnHideRequested);
            AddFor(dialogWindow ,Content);
            OnShowDialogWindow.Invoke(this);
        }

        private void OnHideRequested(DialogWindowBase arg0)
        {
            Debug.Log("hide");
            HideDialogWindow();
        }

        public void HideDialogWindow()
        {
            style.visibility = new StyleEnum<Visibility>(Visibility.Hidden);
            if (_dialogWindowCurrent != null)
            {
                RemoveFor(_dialogWindowCurrent, Content);
                OnHideDialogWindow.Invoke(this);
                _dialogWindowCurrent = null;
            }
        }


        //  Event Handlers --------------------------------
        protected override void OnAddedInternal (VisualElement child)
        {
            base.OnAddedInternal(child);
            
            //Do something?
            Debug.Log("OnAddedInternal() system " + child);
        }
        
        protected override void OnRemovedInternal (VisualElement child)
        {
            base.OnRemovedInternal(child);
            
            //Do something?
            Debug.Log("OnRemoved() system " + child);
        }
 
    }
}
       
