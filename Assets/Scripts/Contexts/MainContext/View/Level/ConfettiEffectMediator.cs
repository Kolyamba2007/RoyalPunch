public class ConfettiEffectMediator : ViewMediator<ConfettiEffectView>
{
    [Inject] public WinSignal WinSignal { get; set; }
    
    public override void OnRegister()
    {
        WinSignal.AddListener(View.Enable);
    }

    public override void OnRemove()
    {
        WinSignal.RemoveListener(View.Enable);
    }
}
