using UnityEngine;

public class Spectator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string stateName;

    private void Awake()
    {
        animator.Play(stateName, -1, Random.Range(0.0f, 1.0f));
    }
}
