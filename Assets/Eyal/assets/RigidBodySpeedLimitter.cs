using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodySpeedLimitter : MonoBehaviour {

    public float m_SpeedLimit = 1;
    Rigidbody m_RigidBody;
	// Use this for initialization
	void Start () {
        m_RigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (m_RigidBody.velocity.magnitude > m_SpeedLimit)
        {
            m_RigidBody.velocity=m_RigidBody.velocity.normalized * m_SpeedLimit;
        }
	}
}
