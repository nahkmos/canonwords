using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f;

    private Vector3[] path;
    private int currentPathIndex = 0;

    public void SetPath(Vector3[] newPath)
    {
        path = newPath;
        currentPathIndex = 0;
    }

    private void Update()
{
    if (path == null || path.Length == 0)
        return;

    Vector3 targetPosition = path[currentPathIndex];

    Vector3 direction = targetPosition - transform.position;
    direction.y = 0f;

    if (direction.magnitude < 0.15f)
    {
        currentPathIndex++;

        if (currentPathIndex >= path.Length)
            return;

        return;
    }

    direction.Normalize();

    // Déplacement
    transform.position += direction * speed * Time.deltaTime;

    // Rotation vers la direction du mouvement
    Quaternion targetRotation =
    Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180, 0);

    transform.rotation = Quaternion.Slerp(
        transform.rotation,
        targetRotation,
        5f * Time.deltaTime
    );
}
}