using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace RMC.Words.UI.UIToolkit
{
    //  Internal Classes ----------------------------------
    
    /// <summary>
    /// 
    /// </summary>
    public class KeyboardKeyData
    {
        public KeyCode KeyCode { get; private set; }
        public string LabelText { get; private set; }
        
        public Color? BackgroundImageTintColor { get; private set; }
        
        public KeyboardKeyData(KeyCode keyCode)
        {
            KeyCode = keyCode;
            LabelText = KeyCode.ToString().ToUpper();
            
            Color white = Color.white;
            BackgroundImageTintColor = white;
        }
        public KeyboardKeyData(KeyCode keyCode, string labelText, Color backgroundImageTintColor)
        {
            KeyCode = keyCode;
            LabelText = labelText;
            BackgroundImageTintColor = backgroundImageTintColor;
        }
    }
    
    

    public class KeyboardKeyUnityEvent : UnityEvent<KeyboardKey>{}
    
    /// <summary>
    /// 
    /// </summary>
    public sealed class KeyboardKey : VisualElement
    {
        /// <summary>
        /// 
        /// </summary>
        public new class UxmlFactory : UxmlFactory<KeyboardKey, UxmlTraits> {}

        /// <summary>
        /// 
        /// </summary>
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            UxmlStringAttributeDescription _label = new UxmlStringAttributeDescription() { name = "Label" };
            UxmlEnumAttributeDescription<KeyCode> _keyCode = new UxmlEnumAttributeDescription<KeyCode>() { name = "KeyCode" };

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                (ve as KeyboardKey).LabelText = _label.GetValueFromBag(bag, cc);
                (ve as KeyboardKey).KeyCode = _keyCode.GetValueFromBag(bag, cc);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public class KeyboardKeyManipulator : Manipulator
        {
            // Colors to use for different mouse events.
            private Color _normalColor = new Color(0.3f, 0.31f, 0.31f);
            private Color _hoverColor = new Color(0.29f, 0.49f, 0.32f);
            private Color _clickColor = new Color(0.17f, 0.4f, 0.18f);
    
            protected override void RegisterCallbacksOnTarget()
            {
                target.RegisterCallback<MouseEnterEvent>(OnMouseEnter);
                target.RegisterCallback<MouseLeaveEvent>(OnMouseLeave);
                target.RegisterCallback<MouseDownEvent>(OnMouseDown);
                target.RegisterCallback<MouseUpEvent>(OnMouseUp);
            }
    
            protected override void UnregisterCallbacksFromTarget()
            {
                target.UnregisterCallback<MouseEnterEvent>(OnMouseEnter);
                target.UnregisterCallback<MouseLeaveEvent>(OnMouseLeave);
                target.UnregisterCallback<MouseDownEvent>(OnMouseDown);
                target.UnregisterCallback<MouseUpEvent>(OnMouseUp);
            }
    
            private void OnMouseEnter(MouseEnterEvent evt)
            {
                ChangeBackgroundColor(_hoverColor);
            }
    
            private void OnMouseLeave(MouseLeaveEvent evt)
            {
                ChangeBackgroundColor(_normalColor);
            }
    
            private void OnMouseDown(MouseDownEvent evt)
            {
                ChangeBackgroundColor(_clickColor);
            }
    
            private void OnMouseUp(MouseUpEvent evt)
            {
                ChangeBackgroundColor(_hoverColor);
            }
    
            private void ChangeBackgroundColor(Color color)
            {
                target.style.backgroundColor = color;
            }
        }
        
        //  Events ----------------------------------------
        public KeyboardKeyUnityEvent OnKeyboardKeyPressed = new KeyboardKeyUnityEvent();

        //  Properties ------------------------------------
        //TODO: Make a template for visual element subclasses like this
        public string LabelText  
        {
            get
            {
                return _labelText;
            }
            set
            {
                _labelText = value;
                if (_label != null)
                {
                    _label.text = _labelText;
                }
            } 
        }
        
        public StyleColor BackgroundImageTintColor  
        {
            get
            {
                return _backgroundImageTintColor;
            }
            set
            {
                _backgroundImageTintColor = value;
                if (_content != null)
                {
                    _content.style.unityBackgroundImageTintColor = _backgroundImageTintColor;
                }
            } 
        }

        //  Fields ----------------------------------------
        private string _labelText = "";
        public KeyCode KeyCode { get; set; }
        private readonly Label _label;
        private readonly VisualElement _content;
        
        private StyleColor _backgroundImageTintColor;
        private StyleColor _backgroundImageTintColorInitial;
        
        //  Initialization --------------------------------
        public KeyboardKey()
        {
            VisualTreeAsset visualTreeAsset = Resources.Load<VisualTreeAsset>("Uxml/KeyboardKeyLayout");
            visualTreeAsset.CloneTree(this);

            this.RegisterCallback<ClickEvent>((evt) =>
            {
                OnKeyboardKeyPressed.Invoke(this);
            });
        
            _label = this.Q<Label>();
            _content = this.Q("Content");
            
            //Store initial value
            StyleColor s = new StyleColor();
            s.value = Color.grey;
            //_backgroundImageTintColorInitial = BackgroundImageTintColor;
            _backgroundImageTintColorInitial = s;
            
            //TODO: Do we like my idea that EVERY UI Component has "Content" as first child? Yes?
            VisualElement content = this.Q<VisualElement>("Content");
            content.AddManipulator(new KeyboardKeyManipulator());
        }
        
        //  Methods ---------------------------------------
        
        //  Event Handlers --------------------------------
        public void PopulateKey(KeyboardKeyData keyboardKeyData)
        {
            KeyCode = keyboardKeyData.KeyCode;
            LabelText = keyboardKeyData.LabelText;
            
            if (keyboardKeyData.KeyCode == KeyCode.None)
            {
                //Sometimes remove key
                LabelText = "";
                _content.style.display = DisplayStyle.None;
            }
            else
            {
                _content.style.display = DisplayStyle.Flex;
            }

            if (keyboardKeyData.BackgroundImageTintColor.HasValue)
            {
                //Use color
                StyleColor styleColor = new StyleColor();
                styleColor.value = keyboardKeyData.BackgroundImageTintColor.Value;
                BackgroundImageTintColor = styleColor;
            }
            else
            {
                //Fallback color
                BackgroundImageTintColor = _backgroundImageTintColorInitial;
            }
        }
        
        public void ResetKey()
        {
            //Don't change label here. That should be set elsewhere
            //DO set color to default bg (not 'right' or 'wrong' color)
        }
    }
}