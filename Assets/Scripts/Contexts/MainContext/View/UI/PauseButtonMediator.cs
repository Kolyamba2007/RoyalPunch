public class PauseButtonMediator : ViewMediator<PauseButtonView>
{
    [Inject] public StartGameSignal StartGameSignal { get; set; }
    [Inject] public ContinueGameSignal ContinueGameSignal { get; set; }
    [Inject] public EndGameSignal EndGameSignal { get; set; }
    
    [Inject] public PauseGameSignal PauseGameSignal { get; set; }
    [Inject] public SetTimeScaleSignal SetTimeScaleSignal { get; set; }

    public override void OnRegister()
    {
        View.ClickSignal.AddListener(OnViewClick);
        StartGameSignal.AddListener(View.Enable);
        ContinueGameSignal.AddListener(View.Enable);
        EndGameSignal.AddListener(View.Disable);

        View.Disable();
    }

    public override void OnRemove()
    {
        View.ClickSignal.RemoveListener(OnViewClick);
        StartGameSignal.RemoveListener(View.Enable);
        ContinueGameSignal.RemoveListener(View.Enable);
        EndGameSignal.RemoveListener(View.Disable);
    }

    private void OnViewClick()
    {
        PauseGameSignal.Dispatch();
        SetTimeScaleSignal.Dispatch(0);
    }
}
