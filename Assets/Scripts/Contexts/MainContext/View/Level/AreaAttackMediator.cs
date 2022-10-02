namespace Contexts.MainContext
{
    public class AreaAttackMediator : ViewMediator<AreaAttackView>
    {
        [Inject] public EnableAreaAttackSignal EnableAreaAttackSignal { get; set; }
        [Inject] public EndAreaAttackSignal EndAreaAttackSignal { get; set; }
        [Inject] public EndGameSignal EndGameSignal { get; set; }
    
        public override void OnRegister()
        {
            EnableAreaAttackSignal.AddListener(View.EnableAttack);
            EndAreaAttackSignal.AddListener(View.HandleEndAttack);
            EndGameSignal.AddListener(View.DisableAttack);
        }

        public override void OnRemove()
        {
            EnableAreaAttackSignal.RemoveListener(View.EnableAttack);
            EndAreaAttackSignal.RemoveListener(View.HandleEndAttack);
            EndGameSignal.RemoveListener(View.DisableAttack);
        }
    }
}
