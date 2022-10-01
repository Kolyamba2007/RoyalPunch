using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] private Transform camera;
    
    void LateUpdate() =>
        transform.LookAt(transform.position + camera.forward);
}
