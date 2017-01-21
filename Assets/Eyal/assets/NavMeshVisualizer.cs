using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshVisualizer : MonoBehaviour {
    public Color m_Color;
	// Use this for initialization
	void Start () {
		
	}

    

    private void OnDrawGizmos()
    {

        var nav = GetComponent<NavMeshAgent>();
        if (nav == null || nav.path == null)
            return;

        var path = nav.path;
        for (int i = 1; i < path.corners.Length; i++)
        {
            Debug.DrawLine(path.corners[i-1],path.corners[i],m_Color);
        }
        return;

        var line = this.GetComponent<LineRenderer>();
        if (line == null)
        {
            line = this.gameObject.AddComponent<LineRenderer>();
            line.material = new Material(Shader.Find("Sprites/Default")) { color = Color.red };
            //line.SetWidth(0.5f, 0.5f);
            line.SetColors(Color.red, Color.red);
        }

        

        line.SetVertexCount(path.corners.Length);

        for (int i = 0; i < path.corners.Length; i++)
        {
            line.SetPosition(i, path.corners[i]);
        }

    }

    void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.SetColors(color, color);
        lr.SetWidth(0.5f, 0.1f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        GameObject.Destroy(myLine, 0.1f);
    }


    // Update is called once per frame
    void Update () {
		
	}
}
