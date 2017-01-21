using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigateToPortBehavior : MonoBehaviour {
    public PortBehaviour m_TargetPort;
    // Use this for initialization
    void Start () {
        //select a random port:
        var ports = GameObject.FindObjectsOfType<PortBehaviour>();
        m_TargetPort = ports[Random.Range(0, ports.Length)];
        //navigate to random port
        var targetPosition = m_TargetPort.transform.position;
        this.transform.LookAt(targetPosition);
        GetComponent<NavMeshAgent>().SetDestination(targetPosition);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
