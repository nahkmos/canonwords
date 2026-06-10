using UnityEngine;

public class LevelSelectBoat : MonoBehaviour
{
    [Header("Path")]
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;

    [Header("Movement")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float arriveDistance = 0.15f;

    [Header("Model Orientation")]
    [SerializeField] private Vector3 rotationOffset = new Vector3(0f, 180f, 0f);

    private Vector3[] path;
    private int currentPathIndex = 0;

    private void Start()
    {
        if (startPoint == null || endPoint == null)
            return;

        transform.position = startPoint.position;

        path = new Vector3[]
        {
            endPoint.position
        };

        currentPathIndex = 0;
    }

    private void Update()
    {
        if (path == null || path.Length == 0)
            return;

        Vector3 targetPosition = path[currentPathIndex];

        Vector3 direction = targetPosition - transform.position;
        direction.y = 0f;

        if (direction.magnitude < arriveDistance)
        {
            transform.position = startPoint.position;
            currentPathIndex = 0;
            return;
        }

        direction.Normalize();

        transform.position += direction * speed * Time.deltaTime;

        Quaternion targetRotation =
            Quaternion.LookRotation(direction) * Quaternion.Euler(rotationOffset);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }
}