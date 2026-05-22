using UnityEngine;

public class CannonBall : MonoBehaviour
{
    private Transform target;
    private Transform hitPoint;

    [SerializeField] private float speed = 18f;
    [SerializeField] private float arcHeight = 1.1f;

    private Vector3 startPosition;
    private float totalDistance;
    private float traveledDistance;

    public void Initialize(Transform targetTransform)
    {
        target = targetTransform;
        hitPoint = target.Find("HitPoint");

        startPosition = transform.position;
        traveledDistance = 0f;

        Vector3 targetPosition = GetTargetPosition();
        totalDistance = Vector3.Distance(startPosition, targetPosition);
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        traveledDistance += speed * Time.deltaTime;

        float progress = traveledDistance / totalDistance;
        progress = Mathf.Clamp01(progress);

        Vector3 targetPosition = GetTargetPosition();

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

    private Vector3 GetTargetPosition()
    {
        if (hitPoint != null)
            return hitPoint.position;

        if (target != null)
            return target.position;

        return transform.position;
    }
}