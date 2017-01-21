using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBehaviour : MonoBehaviour {

    public bool AutoUpdateMesh = true;
    public bool Dirty = true;
    public int Sections {
        get { return m_Sections; }
        set { m_Sections = value;  Dirty = true; }
    }
    public float SizeX
    {
        get { return m_SizeX; }
        set { m_SizeX = value; Dirty = true; }
    }
    public float SizeZ
    {
        get { return m_SizeZ; }
        set { m_SizeZ = value; Dirty = true; }
    }
    
    private int m_Sections = 10;
    private float m_SizeX = 200;
    private float m_SizeZ = 200;

    /**
     * Base vertex locations of the plane. Not to be modified unless updating the geometry. Used as reference point for runtime calculations.
     */
    protected Vector3[] m_BaseVertices;
    /**
     * Base normals of the plane. Not to be modified unless updating the geometry. Used as reference point for runtime calculations.
     * Tho frankly might be unneeded since the base normal is always (0|1|0).
     */
    protected Vector3[] m_BaseNormals;
    protected int[] m_Triangles;
    protected WaveObject[] m_Waves;


	// Use this for initialization
	void Start () {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        GenerateMeshBase();
	}
	
	// Update is called once per frame
	void Update () {
		if( AutoUpdateMesh && Dirty )
        {
            GenerateMeshBase();
            Dirty = false;
        }
	}

    public void GenerateMeshBase()
    {
        List<Vector3> Vertices = new List<Vector3>();
        List<int> Indices = new List<int>();

        float HalfX = m_SizeX / 2;
        float HalfZ = m_SizeZ / 2;
        float distX = m_SizeX / m_Sections;
        float distZ = m_SizeZ / m_Sections;

        for( float x = -HalfX; x < HalfX; x += distX )
        {
            for( float z = -HalfZ; z < HalfZ - distZ; z += distZ )
            {
                int Index0, Index1, Index2, Index3;

                // Add the vertices and track their indices
                Vertices.Add(new Vector3(x, 0, z));
                Index0 = Vertices.Count - 1;
                Vertices.Add(new Vector3(x + distX, 0, z));
                Index1 = Vertices.Count - 1;
                Vertices.Add(new Vector3(x, 0, z + distZ));
                Index2 = Vertices.Count - 1;
                Vertices.Add(new Vector3(x + distX, 0, z + distZ));
                Index3 = Vertices.Count - 1;

                // Add tri 1
                Indices.Add(Index2);
                Indices.Add(Index1);
                Indices.Add(Index0);

                // Add tri 2
                Indices.Add(Index2);
                Indices.Add(Index3);
                Indices.Add(Index1);
            }
        }

        m_BaseVertices = Vertices.ToArray();
        m_Triangles = Indices.ToArray();

        // Add normals to all vertices
        m_BaseNormals = new Vector3[Vertices.Count];
        Vector3 Normal = new Vector3(0, 1, 0);
        for( int i = 0; i < Vertices.Count; ++i)
        {
            m_BaseNormals[i] = Normal;
        }

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.vertices  = m_BaseVertices;
        mesh.normals   = m_BaseNormals;
        mesh.triangles = m_Triangles;
    }

}
