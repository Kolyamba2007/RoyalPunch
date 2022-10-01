using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxerView : UnitView
{
    [Space, SerializeField] private Rigidbody boxerRb;
    
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

    private Coroutine _move, _attack;

    public void SetData(BoxerData boxerData, Controls controls)
    {
        BoxerData = boxerData;
        _controls = controls;
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

            if (movementVector.y < 0)
                animator.SetFloat(horizontalVelocityName, -movementVector.x);
            else
                animator.SetFloat(horizontalVelocityName, movementVector.x);

            boxerRb.velocity = new Vector3(0, boxerRb.velocity.y, 0) +
                          (transform.forward * movementVector.y + transform.right * movementVector.x) *
                          BoxerData.MovementSpeed;


            transform.LookAt(bossTransform);

            yield return null;
        }
    }

    public void StartMoveCoroutine()
    {
        _move = StartCoroutine(Move());
    }
    
    public void StartBossDetectCoroutine()
    {
        StartCoroutine(UnitDetect(BoxerData.DefaultAttackRange, bossLayer));
    }
    
    public void StartAttackCoroutine()
    {
        _attack = StartCoroutine(Attack());
    }

    public void SetStartAnimTrigger()
    {
        animator.SetTrigger(startAnimTriggerName);
    }
    
    private void OnHitBoss()
    {
        if (Hit != 0)
            HitUnitSignal.Dispatch(ResultHit[0].collider);
    }

    public IEnumerator KnockoutWithUp(Vector3 direction, int force)
    {
        StopCoroutine(_move);
        StopCoroutine(_attack);
        
        if(animator.GetBool(attackAnimBoolName))
            animator.SetBool(attackAnimBoolName, false);
        
        List<Vector3> lastPositions = new List<Vector3>();
        List<Quaternion> lastRotations = new List<Quaternion>();

        foreach (var rb in ragdollList)
        {
            lastPositions.Add(rb.transform.localPosition);
            lastRotations.Add(rb.transform.localRotation);
        }

        animator.enabled = false;
        SetRagdollActive(true);
        _controls.Character.Disable();
        
        boxerRb.AddForce(direction * force, ForceMode.Impulse);

        float knockoutDuration = BoxerData.KnockoutDuration;
        while (knockoutDuration > 0)
        {
            knockoutDuration -= Time.deltaTime;
            transform.LookAt(bossTransform);

            yield return null;
        }

        SetRagdollActive(false);
        
        List<Vector3> startPositions = new List<Vector3>();
        List<Quaternion> startRotations = new List<Quaternion>();

        foreach (var rigidbody in ragdollList)
        {
            startPositions.Add(rigidbody.transform.localPosition);
            startRotations.Add(rigidbody.transform.localRotation);
        }
        
        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime / BoxerData.UpTime;

            int count = 0;
            foreach (var rigidbody in ragdollList)
            {
                rigidbody.transform.localPosition = Vector3.Lerp(startPositions[count], lastPositions[count], time);
                rigidbody.transform.localRotation = Quaternion.Slerp(startRotations[count], lastRotations[count], time);

                count++;
            }

            yield return null;
        }
        
        animator.enabled = true;
        _controls.Character.Enable();
        
        StartMoveCoroutine();
        StartAttackCoroutine();
    }
    
    public void Knockout(Vector3 direction, int force)
    {
        animator.enabled = false;
        SetRagdollActive(true);
        
        boxerRb.AddForce(direction * force, ForceMode.Impulse);
    }
    
    public void SetWinViewState()
    {
        Hit = 0;
        
        if(animator.GetBool(attackAnimBoolName))
            animator.SetBool(attackAnimBoolName, false);
        animator.SetTrigger(winAnimTriggerName);
    }
}
