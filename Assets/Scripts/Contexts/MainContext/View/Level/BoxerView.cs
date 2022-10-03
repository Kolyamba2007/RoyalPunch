using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Contexts.MainContext
{
    public class BoxerView : UnitView
    {
        [Space, SerializeField] private Rigidbody rb;
        [SerializeField] private Rigidbody boxerHeadRb;
        [SerializeField] private Transform hipsTransform;
        [Space, SerializeField] private LayerMask bossLayer;
        [SerializeField] private Transform bossTransform;
        [Space, SerializeField] private Animator animator;
        [SerializeField] private string attackAnimBoolName;
        [SerializeField] private string winAnimTriggerName;
        [SerializeField] private string startAnimTriggerName;
        [SerializeField] private string verticalVelocityName;
        [SerializeField] private string horizontalVelocityName;

        public BoxerData BoxerData { get; private set; }

        private Controls _controls;
        private Coroutine _move;
        private Coroutine _attack;
        private List<Vector3> _lastPositions = new List<Vector3>();
        private List<Quaternion> _lastRotations = new List<Quaternion>();

        protected override void Awake()
        {
            base.Awake();

            foreach (var rb in ragdollList)
            {
                _lastPositions.Add(rb.transform.localPosition);
                _lastRotations.Add(rb.transform.localRotation);
            }
        }

        public void SetData(BoxerData boxerData, Controls controls)
        {
            BoxerData = boxerData;
            _controls = controls;
        }

        public void SetWinViewState()
        {
            rb.isKinematic = true;
            Hit = 0;

            if (animator.GetBool(attackAnimBoolName))
                animator.SetBool(attackAnimBoolName, false);
            
            animator.SetTrigger(winAnimTriggerName);
        }

        public void StartMove()
        {
            _move = StartCoroutine(Move());
        }

        public void StartBossDetect()
        {
            StartCoroutine(UnitDetect(BoxerData.DefaultAttackRange, bossLayer));
        }

        public void StartAttack()
        {
            _attack = StartCoroutine(Attack());
        }

        public void SetStartAnimTrigger()
        {
            animator.SetTrigger(startAnimTriggerName);
        }

        public IEnumerator KnockoutWithUp(Vector3 direction, int force, bool withUp)
        {
            _controls.Character.Disable();
            animator.enabled = false;
            animator.WriteDefaultValues();
            
            StopCoroutine(_move);
            StopCoroutine(_attack);

            if (animator.GetBool(attackAnimBoolName))
                animator.SetBool(attackAnimBoolName, false);
            
            SetRagdollActive(true);
            boxerHeadRb.AddForce(Vector3.Normalize(direction) * force, ForceMode.Impulse);
            
            float knockoutDuration = BoxerData.KnockoutDuration;
            while (knockoutDuration > 0)
            {
                knockoutDuration -= Time.deltaTime;
                
                var currentPos = hipsTransform.position;
                
                transform.position = new Vector3(currentPos.x, transform.position.y, currentPos.z);
                transform.LookAt(bossTransform);
                
                hipsTransform.position = currentPos;

                yield return null;
            }

            if (!withUp) yield break;

            List<Vector3> startPositions = new List<Vector3>();
            List<Quaternion> startRotations = new List<Quaternion>();

            foreach (var rigidbody in ragdollList)
            {
                startPositions.Add(rigidbody.transform.localPosition);
                startRotations.Add(rigidbody.transform.localRotation);
            }
            
            SetRagdollActive(false);
            
            float time = 0;
            while (time < 1)
            {
                time += Time.deltaTime / BoxerData.UpTime;

                for (int i = 0; i < ragdollList.Count; i++)
                {
                    ragdollList[i].transform.localPosition = Vector3.Lerp(startPositions[i], _lastPositions[i], time);
                    ragdollList[i].transform.localRotation = Quaternion.Lerp(startRotations[i], _lastRotations[i], time);
                }

                yield return null;
            }

            animator.enabled = true;
            _controls.Character.Enable();

            StartMove();
            StartAttack();
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

        private IEnumerator Move()
        {
            while (true)
            {
                Vector2 movementVector = _controls.Character.Movement.ReadValue<Vector2>();

                animator.SetFloat(verticalVelocityName, movementVector.y);
                animator.SetFloat(horizontalVelocityName, movementVector.x);

                rb.velocity = new Vector3(0, rb.velocity.y, 0) +
                                   (transform.forward * movementVector.y + transform.right * movementVector.x) *
                                   BoxerData.MovementSpeed;


                transform.LookAt(bossTransform);

                yield return null;
            }
        }

        private void OnHitBoss()
        {
            if (Hit != 0)
                HitUnitSignal.Dispatch(ResultHit[0].collider);
        }
    }
}
