using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f;
    private Transform target;

    private void Start()
    {
        GameObject cannon = GameObject.FindGameObjectWithTag("Player");

        if (cannon != null)
            target = cannon.transform;
    }

    private void Update()
    {
        if (target == null) return;

        Vector3 direction = target.position - transform.position;
        direction.y = 0f;
        direction.Normalize();

        transform.position += direction * speed * Time.deltaTime;

        // On ne tourne plus tout l'objet pour l'instant
        // Ça évite de casser le texte au-dessus.
    }
}