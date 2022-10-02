using strange.extensions.mediation.impl;

namespace Contexts.MainContext
{
    public class UnitMediator<T> : Mediator
    {
        [Inject] public StartGameSignal StartGameSignal { get; set; }
        [Inject] public EndGameSignal EndGameSignal { get; set; }
        [Inject] public WinSignal WinSignal { get; set; }
        [Inject] public LoseSignal LoseSignal { get; set; }
    
        [Inject] public T View { get; set; }
        [Inject] public GameConfig GameConfig { get; set; }
        [Inject] public IUnitService UnitService { get; set; }

        public override void OnRegister()
        {
            StartGameSignal.AddListener((View as UnitView)!.EnableHealthBar);
            EndGameSignal.AddListener((View as UnitView)!.DisableHealthBar);
        }

        public override void OnRemove()
        {
            StartGameSignal.RemoveListener((View as UnitView)!.EnableHealthBar);
            EndGameSignal.RemoveListener((View as UnitView)!.DisableHealthBar);
        }
    }
}
