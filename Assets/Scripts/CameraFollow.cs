using Classes;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;
    private Vector3 offset;

    private void Start()
    {
        target = Player.LocalPlayer.Transform;
        offset = transform.position - target.transform.position;
    }

    private void LateUpdate() 
    {
        transform.position = target.transform.position + offset;
    }
}
