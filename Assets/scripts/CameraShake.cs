using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Vector3 originalPosition;

    private void Awake()
    {
        originalPosition = transform.localPosition;
    }

    public void Shake(float duration, float strength)
    {
        StopAllCoroutines();
        StartCoroutine(ShakeRoutine(duration, strength));
    }

    private IEnumerator ShakeRoutine(float duration, float strength)
    {
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            Vector3 offset = new Vector3(
                Random.Range(-1f, 1f) * strength,
                0f,
                Random.Range(-1f, 1f) * strength
            );

            transform.localPosition = originalPosition + offset;

            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}