using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class pirateBehaviour : MonoBehaviour {

    GameObject m_Target;
    public ParticleSystem m_Explosion;
    NavMeshAgent m_NavMeshAgent;

    // Use this for initialization
    void Start () {
         m_NavMeshAgent = GetComponent<NavMeshAgent>();
        

    }


    /// <summary>
    /// Speed up the pirate ships as the level progresses
    /// </summary>
    public static void speedUpAllPirateShips()
    {
        var ships=GameObject.FindObjectsOfType<pirateBehaviour>();
        foreach(var ship in ships)
        {
            ship.speedUp();
        }
    }

    /// <summary>
    /// Speed up the pirate ships over time
    /// </summary>
    public void speedUp()
    {
        float speedUpFactor = 1.4f;
        m_NavMeshAgent.speed *= speedUpFactor;
        m_NavMeshAgent.acceleration *= speedUpFactor;
        m_NavMeshAgent.angularSpeed *= speedUpFactor;
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
                closestDistance = distance;
                closestBoat = boat;
            }
        }
        if(closestBoat!=null && closestBoat != m_Target)
        {
            m_Target = closestBoat.gameObject;
            m_NavMeshAgent.SetDestination(m_Target.transform.position);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        attackBoat(other.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //check if colliding with a boat
        var collider = collision.collider.gameObject;
        attackBoat(collider);
    }

    private void attackBoat(GameObject collider)
    {
        var boat = collider.GetComponent<BoatResponseBehaviour>();
       
        if (boat != null)
        {
            Debug.Log("pirates attack");
            m_Explosion.Stop();
            m_Explosion.Play();
            SoundEffectsBehaciour.explosion();
            boat.m_Rigidbody.AddExplosionForce(BoatResponseBehaviour.ExplosionForce, this.transform.position, 10);
            boat.OnPirates(this);
        }
    }
}
