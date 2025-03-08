using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    [SerializeField] private float speedRotation = 5f;
    private float y = 0;
    private float x = 0;

    void Update()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X") * speedRotation * Time.deltaTime, 0);
        var RotY = Input.GetAxis("Mouse Y");
        var RotX = Input.GetAxis("Mouse X") * speedRotation;
        y -= RotY * speedRotation;
        y = Mathf.Clamp(y, -15, 15);
        x += RotX * speedRotation;
        transform.localRotation = Quaternion.Euler(y, x, 0);
    }
}