using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    [SerializeField]
    [Range(1,500)]
    float timeLeft = 300;
    public float minutes;
    public float seconds;
    public Text timerText;

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        if (seconds < 0)
        {
            seconds = 60;
            minutes--;
        }
        minutes = timeLeft / 60;
        seconds = timeLeft;
        timerText.text = "Time left: " + Mathf.Round(minutes) + - + Mathf.Round(timeLeft);
    }
}
