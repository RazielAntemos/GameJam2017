using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatResponseBehaviour : MonoBehaviour {

    const float m_WaveResponseMagnitude = 6;

    public Rigidbody m_Rigidbody;
    public GameObject m_EmptyFront;
    public GameObject m_EmptyBack;

    public float DebugSpeed;
    public Vector3 DebugVelocity;

    public GameObject[] PushingPoints;
    
    // Use this for initialization
    void Start()
    {
        m_Rigidbody = this.GetComponent<Rigidbody>();
        //m_Rigidbody.velocity = transform.forward* 3;

        
    }

    // Update is called once per frame
    void Update () {
        ApplyWaves();
        DebugVelocity = m_Rigidbody.velocity;
        DebugSpeed = DebugVelocity.magnitude;
	}


    /// <summary>
    /// Applies the forces of all wave emitters currently in the scene.
    /// </summary>
    public void ApplyWaves()
    {
        var Emitters = GameObject.FindGameObjectsWithTag("WaveEmitter");
        foreach (var Emitter in Emitters)
        {
            ApplyWave(Emitter.transform.position);
        }
    }


    public void ApplyWave(Vector3 waveCenter)
    {
        foreach (var PushingPoint in PushingPoints)
        {
            Vector3 pushPosition = PushingPoint.transform.position;
            Vector3 diff = pushPosition - waveCenter;
            float pushMagnitude = m_WaveResponseMagnitude / diff.magnitude;
            var force = diff.normalized * pushMagnitude;

            if (pushMagnitude < 0.05)
                continue;

            m_Rigidbody.AddForceAtPosition(force, pushPosition);
            Debug.DrawRay(pushPosition, force);
        }
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
