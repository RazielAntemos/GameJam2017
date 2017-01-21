using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEmitter : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if( Input.GetMouseButtonUp(0) )
        {
            EmitWave();
        }
	}


    void EmitWave()
    {
        GameObject Water = GameObject.FindGameObjectWithTag("Water");
        WaterBehaviour Behavior = Water.GetComponent<WaterBehaviour>();
        Behavior.EmitWave(new WaveObject(transform.position));
    }

}
