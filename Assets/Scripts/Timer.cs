using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    [SerializeField]
    [Range(1,500)]
    float timeLeft = 300f;

    public Text timerText;

    private void Update()
    {
        timeLeft -= Time.deltaTime;

        timerText.text = "Time left: " + Mathf.Round(timeLeft);
    }
}
