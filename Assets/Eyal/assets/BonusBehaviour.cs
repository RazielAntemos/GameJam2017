using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBehaviour : MonoBehaviour {
   
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        //check if colliding with a boat
        var boat = collision.collider.GetComponent<BoatResponseBehaviour>();
        Debug.Log("collision");
        if (boat != null)
        {

            boat.OnBonus(this);
            GameObject.Destroy(this);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        //check if colliding with a boat
        var boat = collider.GetComponent<BoatResponseBehaviour>();
        Debug.Log("Bonus");
        if (boat != null)
        {

            boat.OnBonus(this);
            Destroy(gameObject);
        }
    }

}
