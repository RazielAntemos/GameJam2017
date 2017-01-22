using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGenerator : MonoBehaviour {
    public ParticleSystem m_Emitter;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            calculateWavePhysics(10);
        }

        if (Input.GetMouseButton(0))
        {
            calculateWavePhysics(1);
        }

    }

    private void calculateWavePhysics(float strength)
    {
        m_Emitter.Emit(1+(int)(
            Math.Pow((1+Math.Sin(Time.time*5)),3)
            * 200*Time.deltaTime
            ));
        var boats = GameObject.FindObjectsOfType<BoatResponseBehaviour>();
        foreach(var boat in boats)
        {

            



                var pushPoint = boat.transform.position + boat.transform.forward*0.5f;//pushPointChild.transform.position;
                var directionToBoat = pushPoint - transform.position;
                var distance = directionToBoat.magnitude;
                if (distance < 10)
                {
                    var minDistance = Mathf.Max(1, distance);
                    var force =8000*strength * Time.deltaTime * directionToBoat.normalized / (distance*distance);
                    boat.m_Rigidbody.AddForceAtPosition(force,pushPoint);
                }
            
        }
    }

   
    
}
