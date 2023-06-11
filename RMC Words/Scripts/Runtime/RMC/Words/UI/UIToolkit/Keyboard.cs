using System.Collections.Generic;
using RMC_Words.Scripts.Runtime.RMC.Utilities;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
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
        public void ResetKeyboard()
        {
            UQueryBuilder<KeyboardKey> uQueryBuilder = this.Query<KeyboardKey>();
            List<KeyboardKey> keyboardKeys = uQueryBuilder.ToList();
            foreach (KeyboardKey keyboardKey in keyboardKeys)
            {
                keyboardKey.ResetKey();
                
                //Mouse
                //TODO: Can I bubble up the key event to the game without trapping+passing here?
                keyboardKey.OnKeyboardKeyPressed.RemoveListener(KeyboardKey_KeyboardKeyPressed);
                keyboardKey.OnKeyboardKeyPressed.AddListener(KeyboardKey_KeyboardKeyPressed);
                
                //Typing
                if (UnityEngine.InputSystem.Keyboard.current != null)
                {
                    UnityEngine.InputSystem.Keyboard.current.onTextInput -= InputSystem_OnTextInput;
                    UnityEngine.InputSystem.Keyboard.current.onTextInput += InputSystem_OnTextInput;
                }
  
            }

            //TODO: What's a better way to support multi-region layouts or game-specific layouts (pass in custom class?)
            //HERE IS US Layout
           if (keyboardKeys.Count == 30)
            {
                //Row 1
                keyboardKeys[0].PopulateKey(new KeyboardKeyData(KeyCode.Q)); 
                keyboardKeys[1].PopulateKey(new KeyboardKeyData(KeyCode.W)); 
                keyboardKeys[2].PopulateKey(new KeyboardKeyData(KeyCode.E)); 
                keyboardKeys[3].PopulateKey(new KeyboardKeyData(KeyCode.R)); 
                keyboardKeys[4].PopulateKey(new KeyboardKeyData(KeyCode.T)); 
                keyboardKeys[5].PopulateKey(new KeyboardKeyData(KeyCode.Y)); 
                keyboardKeys[6].PopulateKey(new KeyboardKeyData(KeyCode.U)); 
                keyboardKeys[7].PopulateKey(new KeyboardKeyData(KeyCode.I)); 
                keyboardKeys[8].PopulateKey(new KeyboardKeyData(KeyCode.O)); 
                keyboardKeys[9].PopulateKey(new KeyboardKeyData(KeyCode.P));

                //Row 2
                keyboardKeys[10].PopulateKey(new KeyboardKeyData(KeyCode.A)); 
                keyboardKeys[11].PopulateKey(new KeyboardKeyData(KeyCode.S)); 
                keyboardKeys[12].PopulateKey(new KeyboardKeyData(KeyCode.D)); 
                keyboardKeys[13].PopulateKey(new KeyboardKeyData(KeyCode.F)); 
                keyboardKeys[14].PopulateKey(new KeyboardKeyData(KeyCode.G)); 
                keyboardKeys[15].PopulateKey(new KeyboardKeyData(KeyCode.H)); 
                keyboardKeys[16].PopulateKey(new KeyboardKeyData(KeyCode.J)); 
                keyboardKeys[17].PopulateKey(new KeyboardKeyData(KeyCode.K)); 
                keyboardKeys[18].PopulateKey(new KeyboardKeyData(KeyCode.L));
                keyboardKeys[19].PopulateKey(new KeyboardKeyData(KeyCode.None)); 

                //Row 3
                
                Color red = Color.red;
                red.a = 0.5f;
                Color green = Color.green;
                green.a = 0.5f;
                keyboardKeys[20].PopulateKey(new KeyboardKeyData(KeyCode.Backspace, "Delete", red ));
                keyboardKeys[21].PopulateKey(new KeyboardKeyData(KeyCode.Z)); 
                keyboardKeys[22].PopulateKey(new KeyboardKeyData(KeyCode.X)); 
                keyboardKeys[23].PopulateKey(new KeyboardKeyData(KeyCode.C)); 
                keyboardKeys[24].PopulateKey(new KeyboardKeyData(KeyCode.V)); 
                keyboardKeys[25].PopulateKey(new KeyboardKeyData(KeyCode.B)); 
                keyboardKeys[26].PopulateKey(new KeyboardKeyData(KeyCode.N)); 
                keyboardKeys[27].PopulateKey(new KeyboardKeyData(KeyCode.M));
                keyboardKeys[28].PopulateKey(new KeyboardKeyData(KeyCode.Return, "Enter", green));
                keyboardKeys[29].PopulateKey(new KeyboardKeyData(KeyCode.None));


            }

        }

        private void InputSystem_OnTextInput(char character)
        {
            if (!UnityEngine.InputSystem.Keyboard.current.wasUpdatedThisFrame)
            {
                //Needed to slow down repeated keys or not?
                return;
            }
            
            KeyCode typedKeyCode = KeyCode.None;
            
            //Is it a single letter (NOT punctuation?
            if (WordsHelper.IsASingleLetter(character.ToString()))
            {
                typedKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), character.ToString(), true);
            }
            else
            {
         
                //Is it a special key that we care about?
                if (UnityEngine.InputSystem.Keyboard.current.enterKey.wasPressedThisFrame)
                {
                    typedKeyCode = KeyCode.Return;
                }
                
                //Is it a special key that we care about?
                if (UnityEngine.InputSystem.Keyboard.current.backspaceKey.wasPressedThisFrame)
                {
                    typedKeyCode = KeyCode.Backspace;
                }
            }


            if (typedKeyCode != KeyCode.None)
            {
                // Lookup which, if any, onscreen keys are a match
                UQueryBuilder<KeyboardKey> uQueryBuilder = this.Query<KeyboardKey>();
                List<KeyboardKey> keyboardKeys = uQueryBuilder.ToList();
            
                foreach (KeyboardKey keyboardKey in keyboardKeys)
                {
                    if (keyboardKey.KeyCode == typedKeyCode)
                    {
                        //TODO: Maybe make this a separate event?
                        //TODO: Maybe make typing events optional with a bool
                        OnKeyboardKeyPressed.Invoke(keyboardKey);
                        return;
                    }
                }   
            }
        }


        //  Event Handlers --------------------------------
        private void KeyboardKey_KeyboardKeyPressed(KeyboardKey keyboardKey)
        {
            OnKeyboardKeyPressed.Invoke(keyboardKey);
        }
    }
}