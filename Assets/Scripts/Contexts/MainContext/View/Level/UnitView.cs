using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitView : View
{
    [SerializeField] protected List<Rigidbody> ragdollList;
    
    [Space, SerializeField] private Canvas healthBar;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text healthText;
    
    protected readonly RaycastHit[] ResultHit = new RaycastHit[1];
    protected int Hit;
    
    public ushort ID { get; private set; }

    public Signal<Collider> HitUnitSignal { get; } = new Signal<Collider>();

    protected override void Awake()
    {
        base.Awake();

        SetRagdollActive(false);
    }

    public void SetID(ushort id)
    {
        ID = id;
    }

    public void Init(IUnitData unitData)
    {
        var maxHealth = unitData.MaxHealth;
        
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
        healthText.text = maxHealth.ToString();

        DisableHealthBar();
    }

    public void EnableHealthBar()
    {
        healthBar.gameObject.SetActive(true);
    }
    
    public void DisableHealthBar()
    {
        healthBar.gameObject.SetActive(false);
    }

    public void UpdateHealthBar(int health)
    {
        healthSlider.value = health;
        healthText.text = health.ToString();
    }
    
    protected IEnumerator UnitDetect(float maxDistance, LayerMask layerMask)
    {
        while (true)
        {
            Hit = Physics.RaycastNonAlloc(transform.position + Vector3.up, transform.forward, ResultHit,
                maxDistance, layerMask.value);

            yield return new WaitForFixedUpdate();
        }
    }

    public void SetRagdollActive(bool active)
    {
        foreach (var rb in ragdollList)
            rb.isKinematic = !active;
    }
}
