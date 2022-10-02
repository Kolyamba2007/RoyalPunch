using UnityEngine.InputSystem;

namespace Contexts.MainContext
{
    public class JoystickMediator : ViewMediator<JoystickView>
    {
        [Inject] public StartGameSignal StartGameSignal { get; set; }
        [Inject] public PauseGameSignal PauseGameSignal { get; set; }
        [Inject] public ContinueGameSignal ContinueGameSignal { get; set; }
        [Inject] public EndGameSignal EndGameSignal { get; set; }
    
        [Inject] public Controls Controls { get; set; }

        public override void OnRegister()
        {
            StartGameSignal.AddListener(Controls.Enable);
            PauseGameSignal.AddListener(Controls.Disable);
            ContinueGameSignal.AddListener(Controls.Enable);
            EndGameSignal.AddListener(Controls.Disable);
        
            Controls.User.TouchContact.started += (_) => View.SetActive(true);
            Controls.User.TouchContact.canceled += (_) => View.SetActive(false);
            Controls.User.TouchPosition.performed += (_) => View.MoveToPosition();
        
            View.SetData(Controls);
        }

        public override void OnRemove()
        {
            StartGameSignal.RemoveListener(Controls.Enable);
            PauseGameSignal.RemoveListener(Controls.Disable);
            ContinueGameSignal.RemoveListener(Controls.Enable);
            EndGameSignal.RemoveListener(Controls.Disable);
        
            Controls.RemoveAllBindingOverrides();
        }
    }
}
