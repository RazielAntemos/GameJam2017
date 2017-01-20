﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatResponseBehaviour : MonoBehaviour {

    const float m_WaveResponseMagnitude = 1000;

    Rigidbody m_Rigidbody;

    public float DebugSpeed;
    public Vector3 DebugVelocity;
    
    // Use this for initialization
    void Start()
    {
        m_Rigidbody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update () {
        ApplyWaves();
        DebugVelocity = m_Rigidbody.velocity;
        DebugSpeed = DebugVelocity.magnitude;
	}

    /// <summary>
    /// Called when boat picks up a bonus
    /// </summary>
    /// <param name="bonus">the bonus that is picked up</param>
    public void OnBonus(BonusBehaviour bonus)
    {

    }

    public Vector3 CalculateWaveForce( Vector3 waveCenter )
    {
        Vector3 diff = transform.position - waveCenter;
        float r = diff.magnitude;
        float r2 = Mathf.Pow(r, 2);

        if (1/r2 < 0.01)
        {
            return new Vector3();
        }
        return diff.normalized * m_WaveResponseMagnitude / r2;
    }


    /// <summary>
    /// Applies the forces of all wave emitters currently in the scene.
    /// </summary>
    public void ApplyWaves()
    {
        var Emitters = GameObject.FindGameObjectsWithTag("WaveEmitter");
        foreach( var Emitter in Emitters )
        {
            Vector3 Force = CalculateWaveForce(Emitter.transform.position);
            m_Rigidbody.AddForce(Force, ForceMode.Force);
        }
    }

}
