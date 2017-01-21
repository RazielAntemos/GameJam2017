using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGenerator : MonoBehaviour {
    public ParticleSystem m_Emitter;

	// Use this for initialization
	void Start () {
        stopWaves();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            doWaves();
        }
        if (Input.GetMouseButtonUp(0))
        {
            stopWaves();
        }

    }

    public void doWaves()
    {
        
            var emission= m_Emitter.emission;
        emission.enabled = true;
       
        
    }

    public void stopWaves()
    {
        var emission = m_Emitter.emission;
        emission.enabled = false;
    }
    
}
