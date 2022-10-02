using System;
using UnityEngine;

namespace Contexts.MainContext
{
    [Serializable]
    public struct BossData : IUnitData
    {
        [SerializeField] private int maxHealth;
        [SerializeField] private int force;
        [SerializeField] private float rotateSpeed;
        [Space, SerializeField] private int defaultAttackDamage;
        [SerializeField] private float defaultAttackRange;
        [Space, SerializeField] private int superAttackDamage;
        [SerializeField] private float superAttackCooldown;
        [Space, SerializeField] private float areaAttackRange;
        [SerializeField] private float areaAttackDuration;
        [Space, SerializeField] private float magneticAttackDuration;
        [SerializeField] private float magneticScale;
        [Space, SerializeField] private float restDuration;

        public int MaxHealth => maxHealth;
        public float RotateSpeed => rotateSpeed;
        public int DefaultAttackDamage => defaultAttackDamage;
        public int Force => force;
        public float DefaultAttackRange => defaultAttackRange;
        public int SuperAttackDamage => superAttackDamage;
        public float SuperAttackCooldown => superAttackCooldown;
        public float AreaAttackRange => areaAttackRange;
        public float AreaAttackDuration => areaAttackDuration;
        public float MagneticAttackDuration => magneticAttackDuration;
        public float MagneticScale => magneticScale;
        public float RestDuration => restDuration;
    }
}
