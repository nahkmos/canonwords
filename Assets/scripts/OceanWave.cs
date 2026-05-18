using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class OceanWave : MonoBehaviour
{
    [SerializeField] private float waveAmplitude = 0.15f;
    [SerializeField] private float waveFrequency = 1.5f;
    [SerializeField] private float waveSpeed = 1.2f;

    private Mesh mesh;
    private Vector3[] baseVertices;
    private Vector3[] animatedVertices;

 private void Start()
{
    mesh = GetComponent<MeshFilter>().mesh;
    baseVertices = mesh.vertices;
    animatedVertices = new Vector3[baseVertices.Length];
}

    private void Update()
    {
        for (int i = 0; i < baseVertices.Length; i++)
        {
            Vector3 vertex = baseVertices[i];

            float wave =
                Mathf.Sin((vertex.x + Time.time * waveSpeed) * waveFrequency) *
                Mathf.Cos((vertex.z + Time.time * waveSpeed) * waveFrequency) *
                waveAmplitude;

            vertex.y += wave;
            animatedVertices[i] = vertex;
        }

        mesh.vertices = animatedVertices;
        mesh.RecalculateNormals();
    }
}