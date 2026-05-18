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

    [Header("Rules")]
    [SerializeField] private int minimumEnemiesOnScreen = 1;

    [Tooltip("Temps entre chaque spawn additionnel")]
    [SerializeField] private float additionalSpawnInterval = 5f;

    private float spawnTimer;

    private void Start()
    {
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
    }
}