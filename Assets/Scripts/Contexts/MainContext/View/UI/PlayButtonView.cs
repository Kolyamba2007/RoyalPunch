using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.UI;

namespace Contexts.MainContext
{
    public class PlayButtonView : View
    {
        public Signal ClickSignal { get; } = new Signal();
    
        [SerializeField] private Button button;
    
        private void OnValidate()
        {
            button = GetComponent<Button>();
        }
    
        protected override void Start()
        {
            base.Start();

            button.onClick.AddListener(() =>
            {
                ClickSignal.Dispatch();
                gameObject.SetActive(false);
            });
        }
    }
}
