using System;
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
            emitParticles();
        }
        if (Input.GetMouseButtonUp(0))
        {
            stopWaves();
        }

        if (Input.GetMouseButton(0))
        {
            calculateWavePhysics();
        }

    }

    private void calculateWavePhysics()
    {
        var boats = GameObject.FindObjectsOfType<BoatResponseBehaviour>();
        foreach(var boat in boats)
        {
            var boatPosition = boat.transform.position;
            var directionToBoat = boatPosition-transform.position;
            var distance = directionToBoat.magnitude;
            if (distance < 20)
            {
                var minDistance = Mathf.Max(1, distance);
                var force = 5000 * Time.deltaTime * directionToBoat.normalized / distance;
                boat.m_Rigidbody.AddForce(force);
            }
        }
    }

    public void emitParticles()
    {
        
    var emission= m_Emitter.emission;
        emission.enabled = true;
        m_Emitter.Emit(400);       
        
    }

    public void stopWaves()
    {
        var emission = m_Emitter.emission;
        emission.enabled = false;
    }
    
}
