using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace RMC.Words.UI.UIToolkit
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Keyboard : VisualElement
    {
        //  Internal Classes ----------------------------------

        /// <summary>
        /// 
        /// </summary>
        public new class UxmlTraits : VisualElement.UxmlTraits {}
    
        /// <summary>
        /// 
        /// </summary>
        public new class UxmlFactory : UxmlFactory<Keyboard, UxmlTraits>
        {
            public override VisualElement Create(IUxmlAttributes bag, CreationContext cc)
            {
                VisualTreeAsset visualTreeAsset = Resources.Load<VisualTreeAsset>("Uxml/KeyboardLayout");
                VisualElement rootVisualElement = base.Create(bag, cc);
                visualTreeAsset.CloneTree(rootVisualElement);

                Keyboard keyboard = rootVisualElement.Q<Keyboard>();
                keyboard.ResetKeyboard();
                

                
                return rootVisualElement;
            }
        }



        //  Events ----------------------------------------
        public KeyboardKeyUnityEvent OnKeyboardKeyPressed = new KeyboardKeyUnityEvent();


        //  Properties ------------------------------------


        //  Fields ----------------------------------------


        //  Initialization --------------------------------


        //  Methods ---------------------------------------
        private void ResetKeyboard()
        {
            UQueryBuilder<KeyboardKey> uQueryBuilder = this.Query<KeyboardKey>();
            List<KeyboardKey> keyboardKeys = uQueryBuilder.ToList();
            foreach (KeyboardKey keyboardKey in keyboardKeys)
            {
                keyboardKey.ResetKey();
                
                //TODO: Can I bubble up the key event to the game without trapping+passing here?
                keyboardKey.OnKeyboardKeyPressed.AddListener(keyboardKeyVisualElement => 
                {
                    OnKeyboardKeyPressed.Invoke(keyboardKey);
                });
            }
        }

        //  Event Handlers --------------------------------
 
    }
}