using UnityEngine;

[CreateAssetMenu(fileName = "BoxerConfig", menuName = "Configurations/BoxerConfig", order = 1)]
public class BoxerConfig : ScriptableObject
{
    [SerializeField] private BoxerData boxerData;

    /// <summary>
    /// Return boxer data
    /// </summary>
    public BoxerData BoxerData => boxerData;
}
