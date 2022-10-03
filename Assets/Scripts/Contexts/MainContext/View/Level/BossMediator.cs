using UnityEngine;

namespace Contexts.MainContext
{
    public class BossMediator : UnitMediator<BossView>
    {
        [Inject] public SetMagneticEffectActiveSignal SetMagneticEffectActiveSignal { get; set; }
        [Inject] public EnableAreaAttackSignal EnableAreaAttackSignal { get; set; }
        [Inject] public EndAreaAttackSignal EndAreaAttackSignal { get; set; }

        public override void OnRegister()
        {
            base.OnRegister();

            StartGameSignal.AddListener(View.StartRotateToTarget);
            StartGameSignal.AddListener(View.StartBoxerDetect);
            StartGameSignal.AddListener(View.StartUseSuperAttack);
            StartGameSignal.AddListener(View.StartAttack);
            LoseSignal.AddListener(View.SetWinViewState);
            EndGameSignal.AddListener(View.StopAllCoroutines);
            
            View.HitUnitSignal.AddListener(OnBoxerHit);
            View.SuperAttackSignal.AddListener(OnBoxerSuperHit);
            View.MagneticAttackSignal.AddListener(SetMagneticEffectActiveSignal.Dispatch);
            View.StartAreaAttackSignal.AddListener(EnableAreaAttackSignal.Dispatch);
            View.EndAreaAttackSignal.AddListener(EndAreaAttackSignal.Dispatch);

            View.SetData(GameConfig.GetBossConfig.BossData);
            View.Init(GameConfig.GetBossConfig.BossData);

            UnitService.AddUnit(View, View.BossData);
        }

        public override void OnRemove()
        {
            base.OnRemove();

            StartGameSignal.RemoveListener(View.StartRotateToTarget);
            StartGameSignal.RemoveListener(View.StartBoxerDetect);
            StartGameSignal.RemoveListener(View.StartUseSuperAttack);
            StartGameSignal.RemoveListener(View.StartAttack);
            LoseSignal.RemoveListener(View.SetWinViewState);
            EndGameSignal.RemoveListener(View.StopAllCoroutines);

            View.HitUnitSignal.RemoveListener(OnBoxerHit);
            View.SuperAttackSignal.RemoveListener(OnBoxerSuperHit);
            View.MagneticAttackSignal.RemoveListener(SetMagneticEffectActiveSignal.Dispatch);
            View.StartAreaAttackSignal.RemoveListener(EnableAreaAttackSignal.Dispatch);
            View.EndAreaAttackSignal.RemoveListener(EndAreaAttackSignal.Dispatch);

            UnitService.Remove(View);
        }

        private void OnBoxerHit(Collider collider)
        {
            if (collider.TryGetComponent(out UnitView unitView))
            {
                UnitService.SetDamage(unitView, View.BossData.DefaultAttackDamage, out int remainingHealth);

                if (remainingHealth != 0)
                    unitView.UpdateHealthBar(remainingHealth);
                else
                {
                    LoseSignal.Dispatch();
                    EndGameSignal.Dispatch();
                    unitView.StartCoroutine((unitView as BoxerView)?.KnockoutWithUp(transform.forward, View.BossData.Force, false));
                }
            }
        }

        private void OnBoxerSuperHit(Collider collider, Vector3 direction)
        {
            if (collider.TryGetComponent(out UnitView unitView))
            {
                UnitService.SetDamage(unitView, View.BossData.SuperAttackDamage, out int remainingHealth);

                if (remainingHealth != 0)
                {
                    unitView.UpdateHealthBar(remainingHealth);
                    unitView.StartCoroutine((unitView as BoxerView)?.KnockoutWithUp(direction, View.BossData.Force, true));
                    View.StartRest();
                }
                else
                {
                    LoseSignal.Dispatch();
                    EndGameSignal.Dispatch();
                    unitView.StartCoroutine((unitView as BoxerView)?.KnockoutWithUp(direction, View.BossData.Force, false));
                }
            }
        }
    }
}
