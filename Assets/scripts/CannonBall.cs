using UnityEngine;

public class CannonBall : MonoBehaviour
{
    private Transform target;
    private float speed = 18f;

    public void Initialize(Transform targetTransform)
    {
        target = targetTransform;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, target.position) < 0.4f)
        {
            EnemyWord enemy = target.GetComponent<EnemyWord>();

            if (enemy != null)
            {
                enemy.TakeCannonHit();
            }
            CameraShake shake = Camera.main.GetComponent<CameraShake>();

            if (shake != null)
                {
                    shake.Shake(0.12f, 0.08f);
                }

            Destroy(gameObject);
        }
    }
}