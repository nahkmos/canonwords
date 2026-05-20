using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private GameObject enemyPrefab;

    [Header("Spawn Area")]
    [SerializeField] private float minX = -4f;
    [SerializeField] private float maxX = 4f;
    [SerializeField] private float spawnZ = 9f;
    [SerializeField] private float spawnY = -0.10f;

    [Header("Portals")]

    [SerializeField] private Transform leftPortalEntry;
    [SerializeField] private Transform leftPortalExit;

    [SerializeField] private Transform leftPortalEntry1;
    [SerializeField] private Transform leftPortalExit1;

    [SerializeField] private Transform rightPortalEntry;
    [SerializeField] private Transform rightPortalExit;

    [SerializeField] private Transform rightPortalEntry1;
    [SerializeField] private Transform rightPortalExit1;

    [Header("Target")]
    [SerializeField] private Transform cannonTarget;

    [Header("Path Randomness")]
    [SerializeField] private float middlePointXRandom = 3f;
    [SerializeField] private float middlePointZRandom = 2f;
    [SerializeField] private float finalTargetRandomRadius = 1.2f;

    [Header("Rules")]
    [SerializeField] private int minimumEnemiesOnScreen = 1;

    [Tooltip("Temps entre chaque spawn additionnel")]
    [SerializeField] private float additionalSpawnInterval = 5f;

    private float spawnTimer;

    private void Start()
    {
        if (cannonTarget == null)
        {
            GameObject cannon = GameObject.FindGameObjectWithTag("Player");

            if (cannon != null)
                cannonTarget = cannon.transform;
        }

        EnsureMinimumEnemies();
    }

    private void Update()
    {
        EnsureMinimumEnemies();

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= additionalSpawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemy();
        }
    }

    private void EnsureMinimumEnemies()
    {
        int currentEnemyCount = FindObjectsByType<EnemyWord>(
            FindObjectsInactive.Exclude,
            FindObjectsSortMode.None
        ).Length;

        while (currentEnemyCount < minimumEnemiesOnScreen)
        {
            SpawnEnemy();
            currentEnemyCount++;
        }
    }

    private void SpawnEnemy()
    {
        Transform[] portalEntries =
        {
            leftPortalEntry,
            leftPortalEntry1,
            rightPortalEntry,
            rightPortalEntry1
        };

        Transform[] portalExits =
        {
            leftPortalExit,
            leftPortalExit1,
            rightPortalExit,
            rightPortalExit1
        };

        int randomPortalIndex = Random.Range(0, portalEntries.Length);

        Transform portalEntry = portalEntries[randomPortalIndex];
        Transform portalExit = portalExits[randomPortalIndex];

        if (portalEntry == null || portalExit == null || cannonTarget == null)
        {
            Debug.LogError("EnemySpawner: Portal Entry / Exit ou CannonTarget manquant !");
            return;
        }

        Vector3 spawnPosition = new Vector3(
            Random.Range(minX, maxX),
            spawnY,
            spawnZ
        );

        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        EnemyWord enemyWord = enemy.GetComponent<EnemyWord>();

        if (enemyWord == null)
        {
            Debug.LogError("Enemy prefab has no EnemyWord component!");
            return;
        }

        enemyWord.SetWord(WordDatabase.GetRandomWord());

        EnemyMovement movement = enemy.GetComponent<EnemyMovement>();

        if (movement == null)
        {
            Debug.LogError("Enemy prefab has no EnemyMovement component!");
            return;
        }

        Vector3 middlePoint = Vector3.Lerp(
            portalExit.position,
            cannonTarget.position,
            0.55f
        );

        middlePoint += new Vector3(
            Random.Range(-middlePointXRandom, middlePointXRandom),
            0f,
            Random.Range(-middlePointZRandom, middlePointZRandom)
        );

        Vector3 finalTarget = cannonTarget.position + new Vector3(
            Random.Range(-finalTargetRandomRadius, finalTargetRandomRadius),
            0f,
            Random.Range(-finalTargetRandomRadius, finalTargetRandomRadius)
        );

        Vector3[] path =
        {
            portalEntry.position,
            portalExit.position,
            middlePoint,
            finalTarget
        };

        movement.SetPath(path);
    }
}