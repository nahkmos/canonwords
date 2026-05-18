using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    [SerializeField] private float bobAmplitude = 0.15f;
    [SerializeField] private float bobSpeed = 2f;

    [SerializeField] private float tiltAmount = 5f;
    [SerializeField] private float tiltSpeed = 1.5f;

    private float baseY;

    private void Start()
    {
        baseY = transform.position.y;
    }

    private void LateUpdate()
    {
        float bob = Mathf.Sin(Time.time * bobSpeed + transform.position.x) * bobAmplitude;

        Vector3 position = transform.position;
        position.y = baseY + bob;
        transform.position = position;

        float currentYRotation = transform.rotation.eulerAngles.y;

        float tiltX = Mathf.Sin(Time.time * tiltSpeed + transform.position.z) * tiltAmount;
        float tiltZ = Mathf.Cos(Time.time * tiltSpeed + transform.position.x) * tiltAmount;

        transform.rotation = Quaternion.Euler(tiltX, currentYRotation, tiltZ);
    }
}