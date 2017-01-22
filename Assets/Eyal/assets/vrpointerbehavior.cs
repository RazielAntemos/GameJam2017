using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vrpointerbehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var groundPlane = new Plane(Vector3.up, 0);
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
           // Camera.main.ScreenPointToRay(new Vector3(300,300));
        float rayDistance;
        if (groundPlane.Raycast(ray, out rayDistance))
            transform.position = ray.GetPoint(rayDistance);
    }
}
