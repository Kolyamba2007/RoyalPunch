using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Contexts.MainContext
{
    public class EndGamePanelView : View
    {
        public Signal ClickSignal { get; } = new Signal();
    
        [SerializeField] private Button button;
        [SerializeField] private TMP_Text resultText;
    
        private void OnValidate()
        {
            button = GetComponentInChildren<Button>();
            resultText = GetComponentInChildren<TMP_Text>();
        }
    
        protected override void Start()
        {
            base.Start();

            button.onClick.AddListener(() => ClickSignal.Dispatch());
        }

        public void Init(string text)
        {
            resultText.text = text;
        }
    
        public void Enable()
        {
            gameObject.SetActive(true);
        }
    
        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}
