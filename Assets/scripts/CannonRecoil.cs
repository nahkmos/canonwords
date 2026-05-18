using System.Collections;
using UnityEngine;

public class CannonRecoil : MonoBehaviour
{
    [SerializeField] private Transform cannonModel;
    [SerializeField] private float recoilDistance = 0.25f;
    [SerializeField] private float recoilBackTime = 0.04f;
    [SerializeField] private float recoilReturnTime = 0.12f;

    private Vector3 startLocalPosition;
    private Coroutine recoilRoutine;

    private void Awake()
    {
        startLocalPosition = cannonModel.localPosition;
    }

    public void PlayRecoil()
    {
        if (recoilRoutine != null)
            StopCoroutine(recoilRoutine);

        recoilRoutine = StartCoroutine(RecoilRoutine());
    }

    private IEnumerator RecoilRoutine()
    {
        Vector3 backPosition = startLocalPosition - Vector3.forward * recoilDistance;

        float t = 0f;

        while (t < recoilBackTime)
        {
            t += Time.deltaTime;
            float lerp = t / recoilBackTime;
            cannonModel.localPosition = Vector3.Lerp(startLocalPosition, backPosition, lerp);
            yield return null;
        }

        t = 0f;

        while (t < recoilReturnTime)
        {
            t += Time.deltaTime;
            float lerp = t / recoilReturnTime;
            cannonModel.localPosition = Vector3.Lerp(backPosition, startLocalPosition, lerp);
            yield return null;
        }

        cannonModel.localPosition = startLocalPosition;
    }
}