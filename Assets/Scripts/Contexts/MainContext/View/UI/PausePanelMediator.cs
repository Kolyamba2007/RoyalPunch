public class PausePanelMediator : ViewMediator<PausePanelView>
{
    [Inject] public ReloadSceneSignal ReloadSceneSignal { get; set; }
    [Inject] public ContinueGameSignal ContinueGameSignal { get; set; }
    [Inject] public PauseGameSignal PauseGameSignal { get; set; }
    
    [Inject] public SetTimeScaleSignal SetTimeScaleSignal { get; set; }

    public override void OnRegister()
    {
        View.MenuButtonClickSignal.AddListener(OnMenuButtonClick);
        View.ContinueButtonClickSignal.AddListener(OnContinueButtonClick);
        PauseGameSignal.AddListener(View.Enable);
        
        View.Disable();
    }

    public override void OnRemove()
    {
        View.MenuButtonClickSignal.RemoveListener(OnMenuButtonClick);
        View.ContinueButtonClickSignal.RemoveListener(OnContinueButtonClick);
        PauseGameSignal.RemoveListener(View.Enable);
    }
    
    private void OnMenuButtonClick()
    {
        ReloadSceneSignal.Dispatch();
        SetTimeScaleSignal.Dispatch(1);
    }
    
    private void OnContinueButtonClick()
    {
        ContinueGameSignal.Dispatch();
        SetTimeScaleSignal.Dispatch(1);
        View.Disable();
    }
}
