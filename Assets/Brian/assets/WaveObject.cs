using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveObject {
    
    public Vector3 Origin;
    public float StartTime;

    /**
     * Maximum impulse the wave can exert upon a target at its origin in kg*m/s
     */
    public float MaxMagnitude = 10;

    /**
     * Radial propagation speed in m/s
     */
    public float PropagationSpeed = 10;

    /**
     * Maximum distance this wave will travel in m
     */
    public float Distance = 400;

    public WaveObject( Vector3 Origin ) : this(Origin, 10) {}

    public WaveObject( Vector3 Origin, float PropagationSpeed ) : this(Origin, PropagationSpeed, 400) {}

    public WaveObject( Vector3 Origin, float PropagationSpeed, float MaxDistance )
    {
        this.Origin = Origin;
        this.PropagationSpeed = PropagationSpeed;
        this.Distance = MaxDistance;
        this.StartTime = Time.time;
    }


    /// <summary>
    /// Calculates the magnitude of the force of the wave exerted upon the target location in dependance on
    /// current game time and location.
    /// </summary>
    /// <param name="Target"></param>
    /// <returns></returns>
    public float CalcForce( Vector3 Target )
    {
        float timeDiff = Time.time - StartTime;
        float timePeak = CalcPeakTime(Target);
        float startTimeDiff = timePeak - Mathf.PI;
        float endTimeDiff = timePeak + Mathf.PI;

        if( timeDiff < startTimeDiff || timeDiff > endTimeDiff )
        {
            return 0;
        }

        Vector3 Diff = Target - Origin;
        Vector3 Unit = Diff.normalized;
        float Magnitude = CalcMaxMagnitude(Target);
        return Mathf.Cos() * Magnitude;
    }

    /// <summary>
    /// Calculates the maximum possible magnitude for the given target.
    /// </summary>
    /// <param name="Target"></param>
    /// <returns></returns>
    public float CalcMaxMagnitude( Vector3 Target )
    {
        Vector3 Diff = Target - Origin;
        return Mathf.Max(0, MaxMagnitude - MaxMagnitude / Distance * Diff.magnitude);
    }

    public float CalcPeakTime( Vector3 Target )
    {
        float Dist = (Target - Origin).magnitude;
        return PropagationSpeed / 2 + Dist / PropagationSpeed;
    }
    
}
