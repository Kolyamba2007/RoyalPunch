using System;
using UnityEngine;

[Serializable]
public struct BoxerData : IUnitData
{
    [SerializeField] private int maxHealth;
    [Space, SerializeField] private int defaultAttackDamage;
    [SerializeField] private float defaultAttackRange;
    [Space, SerializeField] private float movementSpeed;
    [Space, SerializeField] private int force;
    [Space, SerializeField] private float knockoutDuration;
    [Space, SerializeField] private float upTime;
    
    public int MaxHealth => maxHealth;
    public int DefaultAttackDamage => defaultAttackDamage;
    public float DefaultAttackRange => defaultAttackRange;
    public float MovementSpeed => movementSpeed;
    public int Force => force;
    public float KnockoutDuration => knockoutDuration;
    public float UpTime => upTime;
}
