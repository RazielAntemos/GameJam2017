using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEmitter : MonoBehaviour {

    protected const float s_changePositionMaxCountdown = 5;
    protected float m_changePositionCountdown;

	// Use this for initialization
	void Start () {
        m_changePositionCountdown = s_changePositionMaxCountdown;
	}
	
	// Update is called once per frame
	void Update () {
        m_changePositionCountdown -= Time.deltaTime;
        
        if ( m_changePositionCountdown <= 0 )
        {
            m_changePositionCountdown = s_changePositionMaxCountdown;
            transform.position = new Vector3(Random.Range(-20, 20), 0, Random.Range(-20, 20));
        }
	}
}
