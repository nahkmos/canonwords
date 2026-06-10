using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private EnemyWord currentTarget;

    [SerializeField] private GameObject cannonBallPrefab;
    [SerializeField] private Transform playerShip;

    [Header("Cannon Rotation")]
    [SerializeField] private Transform cannonModel;
    [SerializeField] private float cannonRotationSpeed = 6f;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private ParticleSystem smokeRing;
    [SerializeField] private CannonRecoil cannonRecoil;

    [Header("Win Condition")]
    [SerializeField] private int shipsToDestroyToWin = 20;
    [SerializeField] private string levelSelectSceneName = "LevelSelect";

    private int destroyedShips = 0;
    private bool levelFinished = false;
    private EnemySpawner enemySpawner;    

    private void Start()
    {
        enemySpawner = GetComponent<EnemySpawner>();

        int selectedLevel = PlayerPrefs.GetInt("SelectedLevel", 1);
        ApplyLevelSettings(selectedLevel);
    }

    private void ApplyLevelSettings(int level)
    {
        int minimumEnemies;
        float spawnInterval;

        switch (level)
        {
            case 1:
                minimumEnemies = level;
                spawnInterval = 4.5f;
                shipsToDestroyToWin = level * 10;
                break;

            case 2:
                minimumEnemies = level;
                spawnInterval = 4f;
                shipsToDestroyToWin = level * 10;
                break;

            case 3:
                minimumEnemies = level;
                spawnInterval = 3.5f;
                shipsToDestroyToWin = level * 10;
                break;

            case 4:
                minimumEnemies = level;
                spawnInterval = 3f;
                shipsToDestroyToWin = level * 10;
                break;

            case 5:
                minimumEnemies = level;
                spawnInterval = 2f;
                shipsToDestroyToWin = level * 10;
                break;

            default:
                minimumEnemies = 1;
                spawnInterval = 5f;
                shipsToDestroyToWin = 5;
                break;
        }

        if (enemySpawner != null)
            enemySpawner.SetRules(minimumEnemies, spawnInterval);
    }

    private void Update()
    {
        if (levelFinished)
            return;

        HandleKeyboardInput();
        RotateCannonTowardTarget();
    }

    public void RegisterEnemyDestroyed()
    {
        if (levelFinished)
            return;

        destroyedShips++;

        Debug.Log($"Ships destroyed: {destroyedShips}/{shipsToDestroyToWin}");

        if (destroyedShips >= shipsToDestroyToWin)
            WinLevel();
    }

    private void WinLevel()
    {
        levelFinished = true;
        SceneManager.LoadScene(levelSelectSceneName);
    }

    private void HandleKeyboardInput()
    {
        if (Keyboard.current == null)
            return;

        foreach (var key in Keyboard.current.allKeys)
        {
            if (!key.wasPressedThisFrame)
                continue;

            string keyName = key.displayName;

            if (string.IsNullOrEmpty(keyName) || keyName.Length != 1)
                continue;

            char typedChar = char.ToLowerInvariant(keyName[0]);
            HandleTypedLetter(typedChar);
        }
    }

    private void HandleTypedLetter(char typedChar)
    {
        if (currentTarget == null)
            currentTarget = FindTargetStartingWith(typedChar);

        if (currentTarget == null)
            return;

        bool success = currentTarget.TryType(typedChar);

        if (!success)
            return;

        if (currentTarget.IsComplete)
        {
            ShootAtEnemy(currentTarget);
            currentTarget = null;
        }
    }

    private void RotateCannonTowardTarget()
    {
        if (currentTarget == null || cannonModel == null)
            return;

        Vector3 direction = currentTarget.transform.position - cannonModel.position;
        direction.y = 0f;

        if (direction == Vector3.zero)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(direction) * Quaternion.Euler(-90f, -90f, 0f);

        cannonModel.rotation = Quaternion.Slerp(
            cannonModel.rotation,
            targetRotation,
            cannonRotationSpeed * Time.deltaTime
        );
    }

    private void ShootAtEnemy(EnemyWord enemy)
    {
        GameObject cannonBall = Instantiate(cannonBallPrefab, playerShip.position, Quaternion.identity);

        CannonBall cannonBallScript = cannonBall.GetComponent<CannonBall>();

        if (cannonBallScript == null)
        {
            Debug.LogError("CannonBall prefab has no CannonBall script!");
            return;
        }

        if (muzzleFlash != null)
            muzzleFlash.Play();

        if (smokeRing != null)
            smokeRing.Play();

        if (cannonRecoil != null)
            cannonRecoil.PlayRecoil();

        cannonBallScript.Initialize(enemy.transform);
    }

    private EnemyWord FindTargetStartingWith(char typedChar)
    {
        EnemyWord[] enemies = FindObjectsByType<EnemyWord>(
            FindObjectsInactive.Exclude,
            FindObjectsSortMode.None
        );

        EnemyWord closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (EnemyWord enemy in enemies)
        {
            if (enemy.CurrentLetter != typedChar)
                continue;

            float distance = Vector3.Distance(playerShip.position, enemy.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }
}