using UnityEngine;

public class CannonBall : MonoBehaviour
{
    private Transform target;

    [SerializeField] private float travelTime = 0.55f;
    [SerializeField] private float arcHeight = 1.6f;

    private Vector3 startPosition;
    private float elapsedTime;

    public void Initialize(Transform targetTransform)
    {
        target = targetTransform;
        startPosition = transform.position;
        elapsedTime = 0f;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        elapsedTime += Time.deltaTime;

        float progress = elapsedTime / travelTime;
        progress = Mathf.Clamp01(progress);

        Vector3 targetPosition = target.position;

        Vector3 currentPosition = Vector3.Lerp(
            startPosition,
            targetPosition,
            progress
        );

        currentPosition.y += Mathf.Sin(progress * Mathf.PI) * arcHeight;

        transform.position = currentPosition;

        if (progress >= 1f)
        {
            EnemyWord enemy = target.GetComponent<EnemyWord>();

            if (enemy != null)
                enemy.TakeCannonHit();

            CameraShake shake = Camera.main.GetComponent<CameraShake>();

            if (shake != null)
                shake.Shake(0.12f, 0.08f);

            Destroy(gameObject);
        }
    }
}