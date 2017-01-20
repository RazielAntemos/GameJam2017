using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class pirateBehaviour : MonoBehaviour {

    public  GameObject m_Target;
    NavMeshAgent m_NavMeshAgent;

    // Use this for initialization
    void Start () {
         m_NavMeshAgent = GetComponent<NavMeshAgent>();
        m_NavMeshAgent.SetDestination(m_Target.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
