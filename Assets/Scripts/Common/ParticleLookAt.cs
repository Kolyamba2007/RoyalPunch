using UnityEngine;

public class ParticleLookAt : MonoBehaviour
{
    [SerializeField] private Transform target;

    void Update() =>
        transform.LookAt(target);
}
