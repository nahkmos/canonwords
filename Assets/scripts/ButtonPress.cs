using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIButtonPressAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [SerializeField] private float pressedScale = 0.92f;
    [SerializeField] private float animationSpeed = 12f;

    [SerializeField] private string sceneToLoad = "GameScene";

    private Vector3 originalScale;
    private Coroutine scaleCoroutine;
    private bool isLoadingScene = false;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        AnimateScale(originalScale * pressedScale);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        AnimateScale(originalScale);

        if (!isLoadingScene)
            StartCoroutine(LoadSceneAfterAnimation());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        AnimateScale(originalScale);
    }

    private void AnimateScale(Vector3 targetScale)
    {
        if (scaleCoroutine != null)
            StopCoroutine(scaleCoroutine);

        scaleCoroutine = StartCoroutine(ScaleTo(targetScale));
    }

    private IEnumerator ScaleTo(Vector3 targetScale)
    {
        while (Vector3.Distance(transform.localScale, targetScale) > 0.01f)
        {
            transform.localScale = Vector3.Lerp(
                transform.localScale,
                targetScale,
                animationSpeed * Time.unscaledDeltaTime
            );

            yield return null;
        }

        transform.localScale = targetScale;
    }

    private IEnumerator LoadSceneAfterAnimation()
    {
        isLoadingScene = true;

        yield return new WaitForSecondsRealtime(0.15f);

        SceneManager.LoadScene(sceneToLoad);
    }
}