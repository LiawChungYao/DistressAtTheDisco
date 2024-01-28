// COMP30019 - Graphics and Interaction
// (c) University of Melbourne, 2022

using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class GenerateCube : MonoBehaviour
{
    [SerializeField] private Shader shader;
    [SerializeField] private bool perVertexNormals = false;
    [SerializeField] private PointLight pointLight;
    [SerializeField] private Texture texture;

    private MeshRenderer _renderer;

    private void Start()
    {
        // Generate the mesh and assign to the mesh filter.
        GetComponent<MeshFilter>().mesh = CreateMesh();

        // Store renderer reference
        this._renderer = gameObject.GetComponent<MeshRenderer>();

        // Assign custom shader
        this._renderer.material.shader = this.shader;
        
        // Pass texture to shader (task 6)
        this._renderer.material.mainTexture = this.texture;
    }

    private void Update()
    {
        // Pass updated light colour to shader
        this._renderer.material.SetColor("_PointLightColor",
            this.pointLight.GetColor());

        // Pass updated light position to shader
        this._renderer.material.SetVector("_PointLightPosition",
            this.pointLight.GetWorldPosition());
    }

    private Mesh CreateMesh()
    {
        // Our beloved cube is back...
        var mesh = new Mesh
        {
            name = "Cube"
        };

        // Define the vertex positions (same as workshop 2).
        mesh.SetVertices(new[]
        {
            // Top face
            new Vector3(-1.0f, 1.0f, -1.0f),
            new Vector3(-1.0f, 1.0f, 1.0f),
            new Vector3(1.0f, 1.0f, 1.0f),

            new Vector3(-1.0f, 1.0f, -1.0f),
            new Vector3(1.0f, 1.0f, 1.0f),
            new Vector3(1.0f, 1.0f, -1.0f),

            // Bottom face
            new Vector3(-1.0f, -1.0f, -1.0f),
            new Vector3(1.0f, -1.0f, 1.0f),
            new Vector3(-1.0f, -1.0f, 1.0f),

            new Vector3(-1.0f, -1.0f, -1.0f),
            new Vector3(1.0f, -1.0f, -1.0f),
            new Vector3(1.0f, -1.0f, 1.0f),

            // Left face
            new Vector3(-1.0f, -1.0f, -1.0f),
            new Vector3(-1.0f, -1.0f, 1.0f),
            new Vector3(-1.0f, 1.0f, 1.0f),

            new Vector3(-1.0f, -1.0f, -1.0f),
            new Vector3(-1.0f, 1.0f, 1.0f),
            new Vector3(-1.0f, 1.0f, -1.0f),

            // Right face
            new Vector3(1.0f, -1.0f, -1.0f),
            new Vector3(1.0f, 1.0f, 1.0f),
            new Vector3(1.0f, -1.0f, 1.0f),

            new Vector3(1.0f, -1.0f, -1.0f),
            new Vector3(1.0f, 1.0f, -1.0f),
            new Vector3(1.0f, 1.0f, 1.0f),

            // Front face
            new Vector3(-1.0f, 1.0f, 1.0f),
            new Vector3(1.0f, -1.0f, 1.0f),
            new Vector3(1.0f, 1.0f, 1.0f),

            new Vector3(-1.0f, 1.0f, 1.0f),
            new Vector3(-1.0f, -1.0f, 1.0f),
            new Vector3(1.0f, -1.0f, 1.0f),

            // Back face
            new Vector3(-1.0f, 1.0f, -1.0f),
            new Vector3(1.0f, 1.0f, -1.0f),
            new Vector3(1.0f, -1.0f, -1.0f),

            new Vector3(-1.0f, -1.0f, -1.0f),
            new Vector3(-1.0f, 1.0f, -1.0f),
            new Vector3(1.0f, -1.0f, -1.0f)
        });

        // Define the vertex colours (same as workshop 2).
        mesh.SetColors(new[]
        {
            // Top face
            Color.red,
            Color.red,
            Color.red,

            Color.red,
            Color.red,
            Color.red,

            // Bottom face
            Color.red,
            Color.red,
            Color.red,

            Color.red,
            Color.red,
            Color.red,

            // Left face
            Color.yellow,
            Color.yellow,
            Color.yellow,

            Color.yellow,
            Color.yellow,
            Color.yellow,

            // Right face
            Color.yellow,
            Color.yellow,
            Color.yellow,

            Color.yellow,
            Color.yellow,
            Color.yellow,

            // Front face
            Color.blue,
            Color.blue,
            Color.blue,

            Color.blue,
            Color.blue,
            Color.blue,

            // Back face
            Color.blue,
            Color.blue,
            Color.blue,

            Color.blue,
            Color.blue,
            Color.blue
        });

        if(!perVertexNormals){
            // Task 1: Define the correct normals (as unit vectors; currently they're all "zero")
            var topNormal = new Vector3(0.0f, 1.0f, 0.0f).normalized;
            var bottomNormal = new Vector3(0.0f, -1.0f, 0.0f).normalized;
            var leftNormal = new Vector3(-1.0f, 0.0f, 0.0f).normalized;
            var rightNormal = new Vector3(1.0f, 0.0f, 0.0f).normalized;
            var frontNormal = new Vector3(0.0f, 0.0f, 1.0f).normalized;
            var backNormal = new Vector3(0.0f, 0.0f, -1.0f).normalized;

            mesh.SetNormals(new[]
            {
                topNormal, // Top
                topNormal,
                topNormal,
                topNormal,
                topNormal,
                topNormal,

                bottomNormal, // Bottom
                bottomNormal,
                bottomNormal,
                bottomNormal,
                bottomNormal,
                bottomNormal,

                leftNormal, // Left
                leftNormal,
                leftNormal,
                leftNormal,
                leftNormal,
                leftNormal,

                rightNormal, // Right
                rightNormal,
                rightNormal,
                rightNormal,
                rightNormal,
                rightNormal,

                frontNormal, // Front
                frontNormal,
                frontNormal,
                frontNormal,
                frontNormal,
                frontNormal,

                backNormal, // Back
                backNormal,
                backNormal,
                backNormal,
                backNormal,
                backNormal
            });
        }
        else{
                

            // Smoothed per-vertex-position normals (task 5)
            var frontBottomLeftNormal = new Vector3(-1.0f, -1.0f, 1.0f).normalized;
            var frontTopLeftNormal = new Vector3(-1.0f, 1.0f, 1.0f).normalized;
            var frontTopRightNormal = new Vector3(1.0f, 1.0f, 1.0f).normalized;
            var frontBottomRightNormal = new Vector3(1.0f, -1.0f, 1.0f).normalized;
            var backBottomLeftNormal = new Vector3(-1.0f, -1.0f, -1.0f).normalized;
            var backBottomRightNormal = new Vector3(1.0f, -1.0f, -1.0f).normalized;
            var backTopLeftNormal = new Vector3(-1.0f, 1.0f, -1.0f).normalized;
            var backTopRightNormal = new Vector3(1.0f, 1.0f, -1.0f).normalized;

            mesh.SetNormals(new[]
            {
                backTopLeftNormal, // Top
                frontTopLeftNormal,
                frontTopRightNormal,
                backTopLeftNormal,
                frontTopRightNormal,
                backTopRightNormal,

                backBottomLeftNormal, // Bottom
                frontBottomRightNormal,
                frontBottomLeftNormal,
                backBottomLeftNormal,
                backBottomRightNormal,
                frontBottomRightNormal,

                backBottomLeftNormal, // Left
                frontBottomLeftNormal,
                frontTopLeftNormal,
                backBottomLeftNormal,
                frontTopLeftNormal,
                backTopLeftNormal,

                backBottomRightNormal, // Right
                frontTopRightNormal,
                frontBottomRightNormal,
                backBottomRightNormal,
                backTopRightNormal,
                frontTopRightNormal,

                frontTopLeftNormal, // Front
                frontBottomRightNormal,
                frontTopRightNormal,
                frontTopLeftNormal,
                frontBottomLeftNormal,
                frontBottomRightNormal,

                backTopLeftNormal, // Back
                backTopRightNormal,
                backBottomRightNormal,
                backBottomLeftNormal,
                backTopLeftNormal,
                backBottomRightNormal
            });
        }

        // Task 6 (add back in UVs)
        mesh.SetUVs(0, new[]
        {
            new Vector2(0.0f, 0.666f), // Top
            new Vector2(0.0f, 1.0f),
            new Vector2(0.333f, 1.0f),
            new Vector2(0.0f, 0.666f),
            new Vector2(0.333f, 1.0f),
            new Vector2(0.333f, 0.666f),

            new Vector2(0.333f, 0.333f), // Bottom
            new Vector2(0.666f, 0.0f),
            new Vector2(0.333f, 0.0f),
            new Vector2(0.333f, 0.333f),
            new Vector2(0.666f, 0.333f),
            new Vector2(0.666f, 0.0f),

            new Vector2(0.666f, 0.666f), // Left
            new Vector2(0.333f, 0.666f),
            new Vector2(0.333f, 1.0f),
            new Vector2(0.666f, 0.666f),
            new Vector2(0.333f, 1.0f),
            new Vector2(0.666f, 1.0f),

            new Vector2(0.0f, 0.333f), // Right
            new Vector2(0.333f, 0.666f),
            new Vector2(0.333f, 0.333f),
            new Vector2(0.0f, 0.333f),
            new Vector2(0.0f, 0.666f),
            new Vector2(0.333f, 0.666f),

            new Vector2(0.666f, 0.666f), // Front
            new Vector2(0.333f, 0.333f),
            new Vector2(0.333f, 0.666f),
            new Vector2(0.666f, 0.666f),
            new Vector2(0.666f, 0.333f),
            new Vector2(0.333f, 0.333f),

            new Vector2(0.0f, 0.333f), // Back
            new Vector2(0.333f, 0.333f),
            new Vector2(0.333f, 0.0f),
            new Vector2(0.0f, 0.0f),
            new Vector2(0.0f, 0.333f),
            new Vector2(0.333f, 0.0f)
        });

        // Define the indices (same as workshop 2).
        var indices = Enumerable.Range(0, mesh.vertices.Length).ToArray();
        mesh.SetIndices(indices, MeshTopology.Triangles, 0);

        return mesh;
    }
}
