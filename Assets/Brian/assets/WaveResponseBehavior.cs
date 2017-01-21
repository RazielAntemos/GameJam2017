﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveResponseBehavior : MonoBehaviour {

    const float m_WaveResponseMagnitude = 10;

    List<WaveObject> m_Waves;

    public Rigidbody  m_Rigidbody;
    public GameObject[] PushingPoints;

    // Use this for initialization
    void Start () {
        m_Rigidbody = this.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        ApplyWaves();
	}

    /// <summary>
    /// Applies the forces of all wave emitters currently in the scene.
    /// </summary>
    public void ApplyWaves()
    {
        var Water = GameObject.FindGameObjectWithTag("Water");
        var Behavior = Water.GetComponent<WaterBehaviour>();
        var Waves = Behavior.GetWaves();

        foreach( var Wave in Waves )
        {
            ApplyWave(Wave);
        }
    }


    public void ApplyWave( WaveObject Wave )
    {
        foreach( var PushingPoint in PushingPoints )
        {
            // TODO: Calculate force based on distance travelled (time started and propagation speed)

        }
    }


    public void ApplyWave(Vector3 waveCenter)
    {
        foreach (var PushingPoint in PushingPoints)
        {
            Vector3 pushPosition = PushingPoint.transform.position;
            Vector3 diff = pushPosition - waveCenter;
            float pushMagnitude = m_WaveResponseMagnitude / diff.magnitude;
            var force = diff.normalized * pushMagnitude;

            if (pushMagnitude < 0.05)
                continue;

            m_Rigidbody.AddForceAtPosition(force, pushPosition);
            Debug.DrawRay(pushPosition, force);
        }
    }

}
