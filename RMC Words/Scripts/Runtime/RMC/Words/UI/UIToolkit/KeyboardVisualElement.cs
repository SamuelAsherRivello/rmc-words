using UnityEngine;
using UnityEngine.UIElements;

namespace RMC.Words.UI.UIToolkit
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class KeyboardVisualElement : VisualElement
    {
        //  Internal Classes ----------------------------------

        /// <summary>
        /// 
        /// </summary>
        public new class UxmlTraits : VisualElement.UxmlTraits {}
    
        /// <summary>
        /// 
        /// </summary>
        public new class UxmlFactory : UxmlFactory<KeyboardVisualElement, UxmlTraits>
        {
            public override VisualElement Create(IUxmlAttributes bag, CreationContext cc)
            {
                VisualTreeAsset visualTreeAsset = Resources.Load<VisualTreeAsset>("Uxml/KeyboardLayout");
                VisualElement rootVisualElement = base.Create(bag, cc);
                visualTreeAsset.CloneTree(rootVisualElement);

                KeyboardVisualElement keyboardVisualElement = rootVisualElement.Q<KeyboardVisualElement>();

                //TODO: can I bubbled up instead of catch/release/catch/etc... here...?
                UQueryBuilder<KeyboardKeyVisualElement> x = keyboardVisualElement.Query<KeyboardKeyVisualElement>();
                x.ForEach((element =>
                {
                    element.OnKeyboardKeyPressed.AddListener(keyboardKeyVisualElement => 
                    {
                        keyboardVisualElement.OnKeyboardKeyPressed.Invoke(keyboardKeyVisualElement);
                    });
                }));
                
                return rootVisualElement;
            }
        }
        
        //  Events ----------------------------------------
        public KeyboardKeyVisualElement.KeyboardKeyVisualElementUnityEvent OnKeyboardKeyPressed = new KeyboardKeyVisualElement.KeyboardKeyVisualElementUnityEvent();


        //  Properties ------------------------------------


        //  Fields ----------------------------------------


        //  Initialization --------------------------------


        //  Methods ---------------------------------------


        //  Event Handlers --------------------------------
 
    }
}