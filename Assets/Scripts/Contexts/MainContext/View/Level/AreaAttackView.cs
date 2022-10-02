using System.Collections;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Contexts.MainContext
{
    public class AreaAttackView : View
    {
        [SerializeField] private Transform attackArea;
        [SerializeField] private ParticleSystem animeEffect;
        [SerializeField] private ParticleSystem smokeEffect;

        private Coroutine _attack;

        public void EnableAttack(float attackDuration, float attackRange)
        {
            _attack = StartCoroutine(AreaAttack(attackDuration, attackRange));
        }
    
        public void DisableAttack()
        {
            if(_attack != null)
                StopCoroutine(_attack);
        
            attackArea.localScale = Vector3.forward;
            animeEffect.Stop();
        }

        public void HandleEndAttack()
        {
            attackArea.localScale = Vector3.forward;
            smokeEffect.Play();
        }
    
        private IEnumerator AreaAttack(float attackDuration, float attackRange)
        {
            animeEffect.Play();

            float duration = attackDuration;
            while (duration > 0)
            {
                duration -= Time.deltaTime;
                float areaSize = (1 - duration / attackDuration) * attackRange;
                attackArea.localScale = new Vector3(areaSize, areaSize, 1);

                yield return null;
            }
        
            animeEffect.Stop();
        }
    }
}
