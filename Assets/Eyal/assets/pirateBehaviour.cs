using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class pirateBehaviour : MonoBehaviour {

      GameObject m_Target;

    NavMeshAgent m_NavMeshAgent;

    // Use this for initialization
    void Start () {
         m_NavMeshAgent = GetComponent<NavMeshAgent>();
        
	}
	
	// Update is called once per frame
	void Update () {
        var boats = GameObject.FindObjectsOfType<BoatResponseBehaviour>();
        var closestDistance = float.MaxValue;
        BoatResponseBehaviour closestBoat = null;
        //look for the closest boat:
        foreach (var boat in boats)
        {
            var distance = (boat.transform.position - transform.position).magnitude;
            if (distance < closestDistance)
            {
                distance = closestDistance;
                closestBoat = boat;
            }
        }
        if(closestBoat!=null && closestBoat != m_Target)
        {
            m_Target = closestBoat.gameObject;
            m_NavMeshAgent.SetDestination(m_Target.transform.position);
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        //check if colliding with a boat
        var boat = collision.collider.GetComponent<BoatResponseBehaviour>();
        Debug.Log("pirates attack");
        if (boat != null)
        {
            boat.OnPirates(this);            
        }
    }
}
