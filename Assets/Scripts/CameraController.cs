using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    [SerializeField] private float speedRotation = 5f;
    private float y;
    private float x;

    void FixedUpdate()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        var RotY = Input.GetAxis("Mouse Y");
        var RotX = Input.GetAxis("Mouse X");
        y -= RotY * speedRotation;
        y = Mathf.Clamp(y, -15, 15);
        x += RotX * speedRotation;
        transform.localRotation = Quaternion.Euler(y, x, 0);
    }
}