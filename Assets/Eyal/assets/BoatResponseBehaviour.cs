using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatResponseBehaviour : MonoBehaviour {

    const float m_WaveResponseMagnitude = 1000;

    public Rigidbody m_Rigidbody;

    public bool _allowEmitterInfluence;
    public float DebugSpeed;
    public Vector3 DebugVelocity;

    
    public float _WaveStrength = 1f;
    private bool _isCharging;

    // Use this for initialization
    void Start()
    {
        m_Rigidbody = this.GetComponent<Rigidbody>();
        m_Rigidbody.velocity = transform.forward* 3;
    }

    // Update is called once per frame
    private void Update ()
    {

        ChargingInput();
        DebugVelocity = m_Rigidbody.velocity;
        DebugSpeed = DebugVelocity.magnitude;
	}

    private void ChargingInput()
    {
        if (Input.GetMouseButtonUp(0))
        {
            _isCharging = false;
            ApplyWaves();
            _WaveStrength = 1;

        }

        if (Input.GetMouseButtonDown(0))
        {
            _isCharging = true;
        }

        if (_isCharging) _WaveStrength += 0.1f;
    }

    public Vector3 CalculateWaveForce(Vector3 waveCenter)
    {
        Vector3 diff = transform.position - waveCenter;
        float r = diff.magnitude;
        float r2 = Mathf.Pow(r, 2);

        if (1 / r2 < 0.01)
        {
            return new Vector3();
        }
        return diff.normalized * m_WaveResponseMagnitude / r2;
    }


    /// <summary>
    /// Applies the forces of all wave emitters currently in the scene.
    /// </summary>
    public void ApplyWaves()
    {
        var Emitters = GameObject.FindGameObjectsWithTag("WaveEmitter");
        foreach (var Emitter in Emitters)
        {
            Vector3 Force = CalculateWaveForce(Emitter.transform.position);
            m_Rigidbody.AddForce(Force * _WaveStrength, ForceMode.Force);
        }
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
