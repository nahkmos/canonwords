using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class OceanMeshGenerator : MonoBehaviour
{
    [SerializeField] private int xSegments = 80;
    [SerializeField] private int zSegments = 140;
    [SerializeField] private float width = 24f;
    [SerializeField] private float length = 42f;

    private Mesh mesh;

    private void Awake()
    {
        GenerateMesh();
    }

    private void GenerateMesh()
    {
        mesh = new Mesh();
        mesh.name = "Generated Ocean Mesh";

        Vector3[] vertices = new Vector3[(xSegments + 1) * (zSegments + 1)];
        Vector2[] uvs = new Vector2[vertices.Length];
        int[] triangles = new int[xSegments * zSegments * 6];

        int vertexIndex = 0;

        for (int z = 0; z <= zSegments; z++)
        {
            for (int x = 0; x <= xSegments; x++)
            {
                float xPos = ((float)x / xSegments - 0.5f) * width;
                float zPos = ((float)z / zSegments - 0.5f) * length;

                vertices[vertexIndex] = new Vector3(xPos, 0f, zPos);
                uvs[vertexIndex] = new Vector2((float)x / xSegments, (float)z / zSegments);

                vertexIndex++;
            }
        }

        int triIndex = 0;

        for (int z = 0; z < zSegments; z++)
        {
            for (int x = 0; x < xSegments; x++)
            {
                int i = z * (xSegments + 1) + x;

                triangles[triIndex++] = i;
                triangles[triIndex++] = i + xSegments + 1;
                triangles[triIndex++] = i + 1;

                triangles[triIndex++] = i + 1;
                triangles[triIndex++] = i + xSegments + 1;
                triangles[triIndex++] = i + xSegments + 2;
            }
        }

        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        GetComponent<MeshFilter>().mesh = mesh;
    }
}