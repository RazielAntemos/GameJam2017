using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalBehavior : MonoBehaviour {
    
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter(Collider collider)
    {
        //check if colliding with a boat
        var boat = collider.GetComponent<BoatResponseBehaviour>();
        if (boat != null)
        {
            boat.m_Rigidbody.AddExplosionForce(1000, this.transform.position, 10);
            boat.onReachedGoal(this);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //check if colliding with a boat
        var boat = collision.collider.GetComponent<BoatResponseBehaviour>();
        if (boat != null)
        {
            boat.m_Rigidbody.AddExplosionForce(1000, this.transform.position, 10);
            boat.onReachedGoal(this);           
        }
    }

   

}
