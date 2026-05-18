using System.Collections;
using TMPro;
using UnityEngine;

public class EnemyWord : MonoBehaviour
{
    [SerializeField] private TMP_Text wordText;
    [SerializeField] private string word = "pirate";

    [Header("Death Effect")]
    [SerializeField] private GameObject deathEffectPrefab;
    [SerializeField] private float destroyDelay = 0.8f;
    [SerializeField] private float sinkSpeed = 0.4f;
    [SerializeField] private float tiltAmount = 25f;
    [SerializeField] private Transform visualRoot;

    private int currentIndex = 0;
    private bool isDying = false;

    public bool IsComplete
    {
        get { return currentIndex >= word.Length; }
    }

    public char CurrentLetter
    {
        get
        {
            if (IsComplete || isDying)
                return '\0';

            return char.ToLowerInvariant(word[currentIndex]);
        }
    }

    private void Start()
    {
        RefreshDisplay();
    }

    public bool TryType(char letter)
    {
        if (IsComplete || isDying)
            return false;

        letter = char.ToLowerInvariant(letter);

        if (letter != CurrentLetter)
            return false;

        currentIndex++;
        RefreshDisplay();

        return true;
    }

    public void SetWord(string newWord)
    {
        word = newWord;
        currentIndex = 0;
        RefreshDisplay();
    }

    public void TakeCannonHit()
    {
        if (isDying)
            return;

        isDying = true;

        if (deathEffectPrefab != null)
        {
            Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        }

        EnemyMovement movement = GetComponent<EnemyMovement>();
        if (movement != null)
            movement.enabled = false;

        Collider col = GetComponent<Collider>();
        if (col != null)
            col.enabled = false;

        if (wordText != null)
            wordText.gameObject.SetActive(false);

        StartCoroutine(DeathRoutine());
    }

    private IEnumerator DeathRoutine()
{
    float timer = 0f;

    Transform sinkTarget = visualRoot != null ? visualRoot : transform;

    Quaternion startRotation = sinkTarget.localRotation;
Quaternion targetRotation = startRotation * Quaternion.Euler(tiltAmount, 0f, 0f);

    while (timer < destroyDelay)
    {
        timer += Time.deltaTime;
        float t = timer / destroyDelay;

        sinkTarget.localPosition += Vector3.down * sinkSpeed * Time.deltaTime;
        sinkTarget.localRotation = Quaternion.Lerp(startRotation, targetRotation, t);

        yield return null;
    }

    Destroy(gameObject);
}

    private void RefreshDisplay()
    {
        if (wordText == null)
            return;

        string typed = word.Substring(0, currentIndex);
        string remaining = word.Substring(currentIndex);

        wordText.text = $"<color=#55ff55>{typed}</color>{remaining}";
    }
}