using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatResponseBehaviour : MonoBehaviour {

    public Rigidbody m_Rigidbody;

    public float DebugSpeed;
    public Vector3 DebugVelocity;
    
    // Use this for initialization
    void Start()
    {
        m_Rigidbody = this.GetComponent<Rigidbody>();
        m_Rigidbody.velocity = transform.forward* 3;
    }

    // Update is called once per frame
    void Update () {
        DebugVelocity = m_Rigidbody.velocity;
        DebugSpeed = DebugVelocity.magnitude;
	}

    internal void OnPirates(pirateBehaviour pirateBehaviour)
    {
        //boat killed by pirates!
        Destroy(gameObject);
    }


    /// <summary>
    /// Called when boat picks up a bonus
    /// </summary>
    /// <param name="bonus">the bonus that is picked up</param>
    public void OnBonus(BonusBehaviour bonus)
    {

    }

    public void OnBomb(BombBehaviour bomb)
    {

    }

    public void onReachedGoal(GoalBehavior goal)
    {
        //boat has reached goal, it should no longer exist...
        Destroy(gameObject);
    }
}
