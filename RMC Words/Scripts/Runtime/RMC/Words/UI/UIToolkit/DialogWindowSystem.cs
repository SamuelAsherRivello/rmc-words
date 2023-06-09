using UnityEngine;
using UnityEngine.UIElements;

namespace RMC.Words.UI.UIToolkit
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class DialogWindowSystem : VisualElement
    {
        //  Internal Classes ----------------------------------

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


        //  Properties ------------------------------------
        public DialogWindowBase DialogWindowCurrent { get { return _dialogWindowCurrent;}}
        public bool HasDialogWindowCurrent { get { return DialogWindowCurrent != null;}}
        private VisualElement _content;
        
        //  Fields ----------------------------------------
        private DialogWindowBase _dialogWindowCurrent;

        //  Initialization --------------------------------
        public void Initialize()
        {
            this.
            _content = this.Q("Content");
            Debug.Log("_content1: " + _content);
        }


        //  Methods ---------------------------------------
        
        public void ShowDialogWindow(DialogWindowBase dialogWindow)
        {
            HideDialogWindow();
            _dialogWindowCurrent = dialogWindow;
            Debug.Log("_content2: " + _content);
            _content.Add(new DialogWindowBase());
        }
        
        public void HideDialogWindow()
        {
            if (_dialogWindowCurrent != null)
            {
                _content.Remove(_dialogWindowCurrent);
                _dialogWindowCurrent = null;
            }
        }


        //  Event Handlers --------------------------------
 
    }
}
       
