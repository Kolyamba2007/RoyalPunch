public class EndGamePanelMediator : ViewMediator<EndGamePanelView>
{
    [Inject] public ReloadSceneSignal ReloadSceneSignal { get; set; }
    [Inject] public WinSignal WinSignal { get; set; }
    [Inject] public LoseSignal LoseSignal { get; set; }

    public override void OnRegister()
    {
        View.ClickSignal.AddListener(OnViewClick);
        WinSignal.AddListener(OnWin);
        LoseSignal.AddListener(OnLose);
        
        View.Disable();
    }

    public override void OnRemove()
    {
        View.ClickSignal.RemoveListener(OnViewClick);
        WinSignal.RemoveListener(OnWin);
        LoseSignal.RemoveListener(OnLose);
    }

    private void OnViewClick()
    {
        ReloadSceneSignal.Dispatch();
    }
    
    private void OnWin()
    {
        View.Enable();
        View.Init("SUCCESS");
    }
    
    private void OnLose()
    {
        View.Enable();
        View.Init("FAIL");
    }
}
