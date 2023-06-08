using System.Collections.Generic;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
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
        public void ResetKeyboard()
        {
            UQueryBuilder<KeyboardKey> uQueryBuilder = this.Query<KeyboardKey>();
            List<KeyboardKey> keyboardKeys = uQueryBuilder.ToList();
            foreach (KeyboardKey keyboardKey in keyboardKeys)
            {
                keyboardKey.ResetKey();
                
                //TODO: Can I bubble up the key event to the game without trapping+passing here?
                keyboardKey.OnKeyboardKeyPressed.RemoveAllListeners();
                keyboardKey.OnKeyboardKeyPressed.AddListener(keyboardKeyVisualElement => 
                {
                    OnKeyboardKeyPressed.Invoke(keyboardKey);
                });
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
                keyboardKeys[20].PopulateKey(new KeyboardKeyData(KeyCode.KeypadEnter, "Ent"));
                keyboardKeys[21].PopulateKey(new KeyboardKeyData(KeyCode.Z)); 
                keyboardKeys[22].PopulateKey(new KeyboardKeyData(KeyCode.X)); 
                keyboardKeys[23].PopulateKey(new KeyboardKeyData(KeyCode.C)); 
                keyboardKeys[24].PopulateKey(new KeyboardKeyData(KeyCode.V)); 
                keyboardKeys[25].PopulateKey(new KeyboardKeyData(KeyCode.B)); 
                keyboardKeys[26].PopulateKey(new KeyboardKeyData(KeyCode.N)); 
                keyboardKeys[27].PopulateKey(new KeyboardKeyData(KeyCode.M));
                keyboardKeys[28].PopulateKey(new KeyboardKeyData(KeyCode.Delete, "Del"));
                keyboardKeys[29].PopulateKey(new KeyboardKeyData(KeyCode.None)); 
            }

        }

        //  Event Handlers --------------------------------
 
    }
}