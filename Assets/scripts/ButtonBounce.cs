using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIButtonPop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [Header("Animation")]
    [SerializeField] private float pressedScale = 0.9f;
    [SerializeField] private float releaseScale = 1.05f;
    [SerializeField] private float pressDuration = 0.06f;
    [SerializeField] private float releaseDuration = 0.08f;
    [SerializeField] private float returnDuration = 0.08f;

    [Header("Action")]
    [SerializeField] private UnityEvent onClickAfterAnimation;

    private Vector3 originalScale;
    private bool isPointerInside;
    private bool isAnimating;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isAnimating) return;

        isPointerInside = true;
        StopAllCoroutines();
        StartCoroutine(ScaleTo(originalScale * pressedScale, pressDuration));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isPointerInside || isAnimating) return;

        StopAllCoroutines();
        StartCoroutine(ClickAnimation());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerInside = false;

        if (!isAnimating)
        {
            StopAllCoroutines();
            StartCoroutine(ScaleTo(originalScale, returnDuration));
        }
    }

    private IEnumerator ClickAnimation()
    {
        isAnimating = true;

        yield return ScaleTo(originalScale * pressedScale, pressDuration);
        yield return ScaleTo(originalScale * releaseScale, releaseDuration);
        yield return ScaleTo(originalScale, returnDuration);

        onClickAfterAnimation?.Invoke();

        isAnimating = false;
    }

    private IEnumerator ScaleTo(Vector3 targetScale, float duration)
    {
        Vector3 startScale = transform.localScale;
        float time = 0f;

        while (time < duration)
        {
            time += Time.unscaledDeltaTime;
            float t = time / duration;
            transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            yield return null;
        }

        transform.localScale = targetScale;
    }
}