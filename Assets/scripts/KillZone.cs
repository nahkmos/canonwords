using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        EnemyWord enemy = other.GetComponent<EnemyWord>();

        if (enemy == null)
            return;

        Debug.Log("GAME OVER");

        Destroy(other.gameObject);
    }
}