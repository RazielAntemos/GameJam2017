using UnityEngine;
using UnityEngine.AI;
using XInputDotNetPure;

public class BoatResponseBehaviour : MonoBehaviour
{
    private const float m_WaveResponseMagnitude = 1000;

    public Rigidbody m_Rigidbody;

    

    public bool _allowEmitterInfluence;
    public float DebugSpeed;
    public Vector3 DebugVelocity;

    private GamePadState state;
    private GamePadState prevState;

    public float _WaveStrength = 1f;
    private bool _isCharging;

    // Use this for initialization
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        prevState = state;
        state = GamePad.GetState(PlayerIndex.One);

        //Apply wave emitter on space press or A on gamepad
        //if ((prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed)
        //    || Input.GetKeyDown(KeyCode.Space)) ApplyWaves();

        //charge input with mouse
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
            _WaveStrength = 2;
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
        ///////////////////////////////
        //quick hack add maximum force constraints:
        r = Mathf.Max(1, r);
        /////////////////////
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
        DestroyShip();



    }

    void DestroyShip()
    {
        //for every ship that is removed, make the level faster
        pirateBehaviour.speedUpAllPirateShips();
        //remove the game object
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
        DestroyShip();
    }
}