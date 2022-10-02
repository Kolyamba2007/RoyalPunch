using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.UI;

namespace Contexts.MainContext
{
    public class PausePanelView : View
    {
        public Signal MenuButtonClickSignal { get; } = new Signal();
        public Signal ContinueButtonClickSignal { get; } = new Signal();
    
        [SerializeField] private Button menuButton;
        [SerializeField] private Button continueButton;
    
        protected override void Start()
        {
            base.Start();

            menuButton.onClick.AddListener(() => MenuButtonClickSignal.Dispatch());
            continueButton.onClick.AddListener(() => ContinueButtonClickSignal.Dispatch());
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
