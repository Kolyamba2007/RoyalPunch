namespace Contexts.MainContext
{
    public class CameraMediator : ViewMediator<CameraView>
    {
        [Inject] public StartCameraTransitionSignal StartCameraTransitionSignal { get; set; }
        [Inject] public StartGameSignal StartGameSignal { get; set; }
        [Inject] public WinSignal WinSignal { get; set; }

        public override void OnRegister()
        {
            StartCameraTransitionSignal.AddListener(View.StartMove);
            WinSignal.AddListener(View.StartMove);
            View.ReachedFinalPositionSignal.AddListener(OnReachFinalPosition);
        }

        public override void OnRemove()
        {
            StartCameraTransitionSignal.RemoveListener(View.StartMove);
            WinSignal.RemoveListener(View.StartMove);
            View.ReachedFinalPositionSignal.RemoveListener(OnReachFinalPosition);
        
            View.StopAllCoroutines();
        }

        private void OnReachFinalPosition()
        {
            StartGameSignal.Dispatch();
        }
    }
}
