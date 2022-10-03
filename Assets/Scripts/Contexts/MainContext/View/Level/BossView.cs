using System.Collections;
using strange.extensions.signal.impl;
using UnityEngine;

namespace Contexts.MainContext
{
    public class BossView : UnitView
    {
        public Signal<Collider, Vector3> SuperAttackSignal { get; } = new Signal<Collider, Vector3>();
        public Signal<bool> MagneticAttackSignal { get; } = new Signal<bool>();
        public Signal<float, float> StartAreaAttackSignal { get; } = new Signal<float, float>();
        public Signal EndAreaAttackSignal { get; } = new Signal();
    
        [Space, SerializeField] private Rigidbody bossRb;
        [SerializeField] private Rigidbody bossSpineRb;
        [SerializeField] private GameObject spineGm;
        [Space, SerializeField] private LayerMask boxerLayer;
        [SerializeField] private Transform boxerTransform;
        [Space, SerializeField] private Animator animator;
        [SerializeField] private string attackAnimBoolName;
        [SerializeField] private string prepareSuperAttackAnimBoolName;
        [SerializeField] private string legPunchAnimBoolName;
        [SerializeField] private string fistPunchAnimBoolName;
        [SerializeField] private string restAnimBoolName;
        [SerializeField] private string rotateAnimBoolName;

        public BossData BossData { get; private set; }

        private Coroutine _rotateToTarget;
        private Coroutine _useSuperAttack;
        private Coroutine _attack;

        protected override void Awake()
        {
            base.Awake();

            bossRb.isKinematic = true;
            spineGm.SetActive(false);
            bossSpineRb.isKinematic = false;
        }

        public void SetData(BossData bossData)
        {
            BossData = bossData;
        }

        public void SetWinViewState()
        {
            Hit = 0;
            
            if(animator.GetBool(attackAnimBoolName))
                animator.SetBool(attackAnimBoolName, false);
        }

        public void AddForce(Vector3 direction, int force)
        {
            bossSpineRb.AddForce(direction * force, ForceMode.Impulse);
        }
        
        public void Knockout(Vector3 direction, int force)
        {
            animator.enabled = false;
            
            spineGm.SetActive(true);
            SetRagdollActive(true);
            AddForce(direction, force);
        }
    
        public void StartRotateToTarget()
        {
            _rotateToTarget = StartCoroutine(RotateToTarget(boxerTransform));
        }
    
        public void StartBoxerDetect()
        {
            StartCoroutine(UnitDetect(BossData.DefaultAttackRange, boxerLayer));
        }
    
        public void StartAttack()
        {
            _attack = StartCoroutine(Attack());
        }
    
        public void StartUseSuperAttack()
        {
            _useSuperAttack = StartCoroutine(UseSuperAttack());
        }

        public void StartRest()
        {
            StartCoroutine(Rest());
        }
        
        private IEnumerator RotateToTarget(Transform target)
        {
            while (true)
            {
                Vector3 direction = target.position - transform.position;
                Quaternion rotation = Quaternion.LookRotation(direction);

                if (transform.rotation != rotation)
                {
                    if (!animator.GetBool(rotateAnimBoolName))
                        animator.SetBool(rotateAnimBoolName, true);

                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, BossData.RotateSpeed * Time.deltaTime);
                }
                else
                {
                    if (animator.GetBool(rotateAnimBoolName))
                        animator.SetBool(rotateAnimBoolName, false);
                }

                yield return null;
            }
        }

        private IEnumerator Attack()
        {
            while (true)
            {
                if (Hit != 0)
                {
                    if (!animator.GetBool(attackAnimBoolName))
                        animator.SetBool(attackAnimBoolName, true);
                }
                else
                {
                    if (animator.GetBool(attackAnimBoolName))
                        animator.SetBool(attackAnimBoolName, false);
                }

                yield return new WaitForFixedUpdate();
            }
        }

        private IEnumerator UseSuperAttack()
        {
            yield return new WaitForSeconds(BossData.SuperAttackCooldown);

            int rand = UnityEngine.Random.Range(1, 3);
            switch (rand)
            {
                case 1:
                    StartCoroutine(AreaAttack());
                    break;
                case 2:
                    StartCoroutine(MagneticAttack());
                    break;
            }
        }
    
        private IEnumerator Rest()
        {
            if (!animator.GetBool(restAnimBoolName))
                animator.SetBool(restAnimBoolName, true);
        
            yield return new WaitForSeconds(BossData.RestDuration);

            if (animator.GetBool(restAnimBoolName))
            {
                animator.SetBool(restAnimBoolName, false);

                StartAttack();
                StartRotateToTarget();
                StartUseSuperAttack();
            }
        }
    
        private IEnumerator AreaAttack()
        {
            StopCoroutine(_attack);
            StopCoroutine(_useSuperAttack);
            StopCoroutine(_rotateToTarget);
        
            if(animator.GetBool(attackAnimBoolName))
                animator.SetBool(attackAnimBoolName, false);
            if(animator.GetBool(rotateAnimBoolName))
                animator.SetBool(rotateAnimBoolName, false);
        
            if (!animator.GetBool(prepareSuperAttackAnimBoolName))
                animator.SetBool(prepareSuperAttackAnimBoolName, true);
        
            StartAreaAttackSignal.Dispatch(BossData.AreaAttackDuration, BossData.AreaAttackRange);

            yield return new WaitForSeconds(BossData.AreaAttackDuration);
        
            if (!animator.GetBool(legPunchAnimBoolName))
                animator.SetBool(legPunchAnimBoolName, true);
        }
    
        private IEnumerator MagneticAttack()
        {
            StopCoroutine(_attack);
            StopCoroutine(_useSuperAttack);
        
            if(animator.GetBool(attackAnimBoolName))
                animator.SetBool(attackAnimBoolName, false);
        
            if (!animator.GetBool(prepareSuperAttackAnimBoolName))
                animator.SetBool(prepareSuperAttackAnimBoolName, true);
        
            MagneticAttackSignal.Dispatch(true);

            if (boxerTransform.TryGetComponent<Rigidbody>(out Rigidbody boxerRB))
            {
                float duration = BossData.MagneticAttackDuration;
                while (duration > 0)
                {
                    duration -= Time.deltaTime;

                    float сharacterDist = Vector3.Distance(transform.position, boxerTransform.position);
                    if (сharacterDist <= BossData.DefaultAttackRange)
                    {
                        if (!animator.GetBool(fistPunchAnimBoolName))
                            animator.SetBool(fistPunchAnimBoolName, true);

                        yield break;
                    }

                    boxerRB.velocity += boxerTransform.forward * BossData.MagneticScale;

                    yield return null;
                }
            }
        
            MagneticAttackSignal.Dispatch(false);

            if (animator.GetBool(prepareSuperAttackAnimBoolName))
                animator.SetBool(prepareSuperAttackAnimBoolName, false);
            if (animator.GetBool(fistPunchAnimBoolName))
                animator.SetBool(fistPunchAnimBoolName, false);
        
            StopCoroutine(_rotateToTarget);
            StartRest();
        
            if (animator.GetBool(rotateAnimBoolName))
                animator.SetBool(rotateAnimBoolName, false);
        }

        private void OnHitBoxer()
        {
            if (Hit != 0)
                HitUnitSignal.Dispatch(ResultHit[0].collider);
        }

        private void OnMagneticAttack()
        {
            StopCoroutine(_rotateToTarget);

            if (animator.GetBool(rotateAnimBoolName))
                animator.SetBool(rotateAnimBoolName, false);
            if (animator.GetBool(prepareSuperAttackAnimBoolName))
                animator.SetBool(prepareSuperAttackAnimBoolName, false);
            if (animator.GetBool(fistPunchAnimBoolName))
                animator.SetBool(fistPunchAnimBoolName, false);
            if (animator.GetBool(attackAnimBoolName))
                animator.SetBool(attackAnimBoolName, false);

            MagneticAttackSignal.Dispatch(false);

            if (Hit != 0)
                SuperAttackSignal.Dispatch(ResultHit[0].collider, transform.forward + transform.up );
            else
                StartCoroutine(Rest());
        }

        private void OnAreaAttack()
        {
            if (animator.GetBool(prepareSuperAttackAnimBoolName))
                animator.SetBool(prepareSuperAttackAnimBoolName, false);
            if (animator.GetBool(legPunchAnimBoolName))
                animator.SetBool(legPunchAnimBoolName, false);
            if (animator.GetBool(fistPunchAnimBoolName))
                animator.SetBool(fistPunchAnimBoolName, false);

            EndAreaAttackSignal.Dispatch();

            if (Vector3.Distance(transform.position, boxerTransform.position) <= BossData.AreaAttackRange * 2.5f)
            {
                if (boxerTransform.TryGetComponent<Collider>(out Collider collider))
                    SuperAttackSignal.Dispatch(collider, -boxerTransform.forward);
            }
            else
                StartCoroutine(Rest());
        }
    }
}
