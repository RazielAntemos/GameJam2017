using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleWaveCollider : MonoBehaviour 
{
    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;
    public float m_ParticleStrength = 1;
    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

        Rigidbody rb = other.GetComponent<Rigidbody>();
        int i = 0;

        while (i < numCollisionEvents)
        {
            if (rb)
            {
                Vector3 pos = collisionEvents[i].intersection;
                Vector3 force = collisionEvents[i].velocity * m_ParticleStrength;
                rb.AddForce(force);
            }
            i++;
        }
    }
}