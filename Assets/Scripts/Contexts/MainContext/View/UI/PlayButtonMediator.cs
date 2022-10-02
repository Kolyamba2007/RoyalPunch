namespace Contexts.MainContext
{
    public class PlayButtonMediator : ViewMediator<PlayButtonView>
    {
        [Inject] public StartCameraTransitionSignal StartCameraTransitionSignal { get; set; }

        public override void OnRegister()
        {
            View.ClickSignal.AddListener(OnViewClick);
        }

        public override void OnRemove()
        {
            View.ClickSignal.RemoveListener(OnViewClick);
        }

        private void OnViewClick()
        {
            StartCameraTransitionSignal.Dispatch();
        }
    }
}
