using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[System.Serializable]
public class Point
{
    public Vector3 vertexPosition;
    public Vector3 normal;
    public Point()
    {

    }
    public Point(Vector3 _position, Vector3 _normal)
    {
        this.vertexPosition = _position;
        this.normal = _normal;
    }
}

public class SurfaceGenerator : MonoBehaviour
{
    private List<Point> points = new List<Point>();

    public Camera cam;
    public RenderTexture surfaceTexture;
    public MeshFilter meshFilter;
    public Texture2D tex;
    public float multipler;

    public Color[] pixels;
    public int vertexCount;
    public int pixelCount;
    public int size;

    public float highestPoint;

    // Start is called before the first frame update
    void Start()
    {
        CreateMesh();
        StartCoroutine(waitAndRender());
        IEnumerator waitAndRender()
        {
            yield return new WaitForEndOfFrame();
            tex = RenderTextureTo2DTexture(surfaceTexture);

            List<Vector3> meshVertices = meshFilter.mesh.vertices.ToList();

            vertexCount = meshFilter.mesh.vertexCount;
            pixelCount = tex.GetPixels().Length;
            for (int i = 0; i < vertexCount; i++)
            {
                meshVertices[i] += Vector3.up * pixels[i].grayscale * multipler;
                if (highestPoint < meshVertices[i].y * 15)
                {
                    highestPoint = meshVertices[i].y * 15;
                }
            }


            meshFilter.mesh.vertices = meshVertices.ToArray();
            meshFilter.mesh.RecalculateBounds();
            meshFilter.mesh.RecalculateNormals();
            meshFilter.mesh.RecalculateTangents();

            //TreeManager.instance.treePositions = meshVertices.Where(t => t.y * 15 < highestPoint * .4f && t.y * 15 > 5 * 15 + 3 * 1.5f && Vector3.Angle(Vector3.up, meshFilter.mesh.normals[meshVertices.IndexOf(t)]) < 15).Select(x => new Point() { vertexPosition = x, normal = meshFilter.mesh.normals[Array.IndexOf(meshFilter.mesh.vertices, x)] }).ToList();
            //TreeManager.instance.GenerateTrees();

            gameObject.AddComponent<MeshCollider>();
            GetComponent<MeshCollider>().sharedMesh = meshFilter.mesh;
        }
    }

    private Texture2D RenderTextureTo2DTexture(RenderTexture rt)
    {
        var texture = new Texture2D(rt.width, rt.height, rt.graphicsFormat, 0, UnityEngine.Experimental.Rendering.TextureCreationFlags.None);
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.filterMode = FilterMode.Bilinear;
        RenderTexture.active = rt;
        texture.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        texture.Apply();
        pixels = texture.GetPixels();
        RenderTexture.active = null;

        return texture;
    }

    void CreateMesh()
    {
        Mesh mesh = new Mesh();
        var vertices = new Vector3[size * size];
        var uvs = new Vector2[size * size];
        var triangles = new int[(size - 1) * (size - 1) * 6];
        var colourMap = new Color[size * size];
        // create vertices
        int i = 0;
        for (int z = 0; z < size; z++)
        {
            for (int x = 0; x < size; x++)
            {
                vertices[i] = new Vector3(x, 0, z);
                i++;
            }
        }

        mesh.vertices = vertices;
        mesh.RecalculateBounds();

        // create triangles
        int vert = 0;
        int tris = 0;
        for (int y = 0; y < size - 1; y++)
        {
            for (int x = 0; x < size - 1; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + size;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + size;
                triangles[tris + 5] = vert + size + 1;
                uvs[vert] = new Vector2(x / (float)size, y / (float)size);
                vert++;
                tris += 6;
            }
            vert++;
        }

        
        mesh.triangles = triangles;
        mesh.uv = uvs;
        
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();

        meshFilter.mesh = mesh;
        meshFilter.mesh.RecalculateBounds();
        meshFilter.mesh.RecalculateNormals();
        meshFilter.mesh.RecalculateTangents();

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
