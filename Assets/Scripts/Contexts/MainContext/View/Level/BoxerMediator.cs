using UnityEngine;

namespace Contexts.MainContext
{
    public class BoxerMediator : UnitMediator<BoxerView>
    {
        [Inject] public Controls Controls { get; set; }

        public override void OnRegister()
        {
            base.OnRegister();
        
            StartGameSignal.AddListener(View.SetStartAnimTrigger);
            StartGameSignal.AddListener(View.StartBossDetect);
            StartGameSignal.AddListener(View.StartAttack);
            WinSignal.AddListener(View.SetWinViewState);
            EndGameSignal.AddListener(View.StopAllCoroutines);
        
            View.HitUnitSignal.AddListener(OnBossHit);
        
            View.SetData(GameConfig.GetBoxerConfig.BoxerData, Controls);
            View.Init(GameConfig.GetBoxerConfig.BoxerData);
        
            UnitService.AddUnit(View, View.BoxerData);
        
            View.StartMove();
        }

        public override void OnRemove()
        {
            base.OnRemove();
        
            StartGameSignal.RemoveListener(View.SetStartAnimTrigger);
            StartGameSignal.RemoveListener(View.StartBossDetect);
            StartGameSignal.RemoveListener(View.StartAttack);
            WinSignal.RemoveListener(View.SetWinViewState);
            EndGameSignal.RemoveListener(View.StopAllCoroutines);
        
            View.HitUnitSignal.RemoveListener(OnBossHit);
        
            UnitService.Remove(View);
        }
    
        private void OnBossHit(Collider collider)
        {
            if (collider.TryGetComponent(out UnitView unitView))
            {
                UnitService.SetDamage(unitView, View.BoxerData.DefaultAttackDamage, out int remainingHealth);

                if (remainingHealth != 0)
                {
                    unitView.UpdateHealthBar(remainingHealth);
                    (unitView as BossView)?.AddForce(transform.forward, View.BoxerData.Force);
                }
                else
                {
                    WinSignal.Dispatch();
                    EndGameSignal.Dispatch();
                    (unitView as BossView)?.Knockout(transform.forward, View.BoxerData.Force);
                }
            }
        }
    }
}
