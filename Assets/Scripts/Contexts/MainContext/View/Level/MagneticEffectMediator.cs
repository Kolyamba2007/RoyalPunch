public class MagneticEffectMediator : ViewMediator<MagneticEffectView>
{
    [Inject] public SetMagneticEffectActiveSignal SetMagneticEffectActiveSignal { get; set; }
    
    public override void OnRegister()
    {
        SetMagneticEffectActiveSignal.AddListener(View.SetActive);
    }

    public override void OnRemove()
    {
        SetMagneticEffectActiveSignal.RemoveListener(View.SetActive);
    }
}
