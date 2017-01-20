using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehaviour : MonoBehaviour {
    public ParticleSystem m_Explosion;
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        //check if colliding with a boat
        var boat = collision.collider.GetComponent<BoatResponseBehaviour>();
        Debug.Log("Boom!");
        m_Explosion.Stop();
        m_Explosion.Play();
        if (boat != null)
        {
            boat.m_Rigidbody.AddExplosionForce(1000, this.transform.position, 10);
            boat.OnBomb(this);
            GameObject.Destroy(this.gameObject);
        }
    }

   

}
