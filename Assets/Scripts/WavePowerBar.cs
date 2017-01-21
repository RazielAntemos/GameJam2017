using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WavePowerBar : MonoBehaviour {

    public Slider powerSlider;
    float wavePower;

    BoatResponseBehaviour boatResponse;


    private void Update()
    {
        boatResponse = GetComponent<BoatResponseBehaviour>();
        wavePower = boatResponse._WaveStrength;

        powerSlider.value = wavePower;
        if (wavePower == 1)
            //  powerSlider.enabled = false;
            powerSlider.gameObject.SetActive(false);
        else
            powerSlider.gameObject.SetActive(true);


        Debug.Log(powerSlider.enabled);
    }
}
