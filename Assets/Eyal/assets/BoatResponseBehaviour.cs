using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatResponseBehaviour : MonoBehaviour {
    Rigidbody m_Rigidbody;
    // Use this for initialization
    void Start()
    {
        m_Rigidbody = this.GetComponent<Rigidbody>();
        m_Rigidbody.velocity = transform.forward* 3;
    }

    // Update is called once per frame
    void Update () {
		
	}

    /// <summary>
    /// Called when boat picks up a bonus
    /// </summary>
    /// <param name="bonus">the bonus that is picked up</param>
    public void OnBonus(BonusBehaviour bonus)
    {

    }
}
