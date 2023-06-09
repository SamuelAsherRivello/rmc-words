using UnityEngine;
using UnityEngine.UIElements;

namespace RMC.Words.UI.UIToolkit
{
    /// <summary>
    /// 
    /// </summary>
    public class DialogWindowBase : VisualElement
    {
        //  Internal Classes ----------------------------------

        /// <summary>
        /// 
        /// </summary>
        public new class UxmlTraits : VisualElement.UxmlTraits {}
    
        /// <summary>
        /// 
        /// </summary>
        public new class UxmlFactory : UxmlFactory<DialogWindowBase, UxmlTraits>
        {
            public override VisualElement Create(IUxmlAttributes bag, CreationContext cc)
            {
                VisualTreeAsset visualTreeAsset = Resources.Load<VisualTreeAsset>("Uxml/DialogWindowBaseLayout");
                VisualElement rootVisualElement = base.Create(bag, cc);
                visualTreeAsset.CloneTree(rootVisualElement);

                DialogWindowBase dialogWindowBase = rootVisualElement.Q<DialogWindowBase>();
                dialogWindowBase.Show();
                
                return rootVisualElement;
            }
        }



        //  Events ----------------------------------------


        //  Properties ------------------------------------


        //  Fields ----------------------------------------


        //  Initialization --------------------------------
        public DialogWindowBase()
        {
            VisualTreeAsset visualTreeAsset = Resources.Load<VisualTreeAsset>("Uxml/DialogWindowBaseLayout");
            visualTreeAsset.CloneTree(this);
            Show();
        }

        //  Methods ---------------------------------------
        
        public void Show()
        {
            DialogWindowBase dialogWindowBase = this.Q<DialogWindowBase>();
            dialogWindowBase.style.flexGrow = new StyleFloat(1);
            dialogWindowBase.style.width = new StyleLength(new Length(100, LengthUnit.Percent));
            
            dialogWindowBase.RegisterCallback<ClickEvent>(OnClickEvent);
        }

        public void Hide()
        {
            this.parent.Remove(this);
        }


        //  Event Handlers --------------------------------
        private void OnClickEvent(ClickEvent evt)
        {
            Hide();
        }
 
    }
}