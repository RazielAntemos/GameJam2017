﻿using UnityEngine;
using UnityEngine.AI;
//using XInputDotNetPure;

public class BoatResponseBehaviour : MonoBehaviour
{
    private const float m_WaveResponseMagnitude = 1000;
     GameObject m_EndOfGameScreen;
    bool m_OnFire = false;
    public ParticleSystem m_FireEffect;
    public Rigidbody m_Rigidbody;
    static int m_ShipsSaved = 0;
    static int m_ShipsLost = 0;

    WavePowerBar WaveBar;

    public bool _allowEmitterInfluence;
    public float DebugSpeed;
    public Vector3 DebugVelocity;

    //private GamePadState state;
    //private GamePadState prevState;

    public float _WaveStrength = 1f;
    private bool _isCharging;
    internal static readonly float ExplosionForce= 2000;

    // Use this for initialization
    private void Start()
    {
        m_EndOfGameScreen = GameObject.FindGameObjectWithTag("EndOfGameScreen");
         m_ShipsSaved = 0;
         m_ShipsLost = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        //prevState = state;
        //state = GamePad.GetState(PlayerIndex.One);

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
            //ApplyWaves();
            _WaveStrength = 2;
        }

        if (Input.GetMouseButtonDown(0))
        {
            _isCharging = true;
        }

        if (_isCharging) { _WaveStrength += 0.1f;
       }
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
        
       
        damageShip();


    }

    void DestroyShip()
    {
        //for every ship that is removed, make the level faster
        pirateBehaviour.speedUpAllPirateShips();
        //remove the game object
        Destroy(gameObject);
        var remainingBoats = FindObjectsOfType<BoatResponseBehaviour>();
        if (remainingBoats==null ||1 >= remainingBoats.Length)
        {
            Debug.Log("Game Over");
            m_EndOfGameScreen.SetActive(true);
            m_EndOfGameScreen.transform.position -= new Vector3(0, 100, 0);
            var endOfGameText = GameObject.FindGameObjectWithTag("EndOfGameText").GetComponent<TextMesh>();
            var endMessage =((m_ShipsSaved>2)? "You wiN\n":"Try agaiN\n")+m_ShipsSaved+"/"+(m_ShipsLost+m_ShipsSaved)+"\nSaveD";
            endOfGameText.text = endMessage;


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
        damageShip();
    }

    private void damageShip()
    {
        if (!m_OnFire)
        {
            m_OnFire = true;
            var fireEnabler=m_FireEffect.emission;
            fireEnabler.enabled = true;
        }
        else
        {
            //ship allready on fire,
            //Destroy it:
            m_ShipsLost++;
            DestroyShip();
        }
    }

    public void onReachedGoal(GoalBehavior goal)
    {
        //only let the right ships into the goal
        var goalResource = goal.GetComponent<ResourceVisualizer>()._startingResource;
        var shipResource = this.GetComponent<ResourceVisualizer>()._startingResource;
        if (shipResource.Equals(goalResource)) {
            m_ShipsSaved++;
            //boat has reached goal, it should no longer exist...
            DestroyShip();
        }
    }
}