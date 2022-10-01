using UnityEngine;

[CreateAssetMenu(fileName = "BossConfig", menuName = "Configurations/BossConfig", order = 2)]
public class BossConfig : ScriptableObject
{
    [SerializeField] private BossData bossData;

    /// <summary>
    /// Return boss data
    /// </summary>
    public BossData BossData => bossData;
}
