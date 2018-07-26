using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Transform ball;

    private Ball _ballScript;
    private float _time, _oldTime;
    private string _bestText;
    private List<Text> _timers;

    void Start ()
    {
        _timers = new List<Text>();
        foreach(Transform t in transform)
            _timers.Add(t.GetComponent<Text>());
        
        _bestText = "Best: ";
        
        _timers[1].text = _bestText + "???";

        _ballScript = ball.GetComponent<Ball>();

        _time = _oldTime = 0;
    }
    
    void Update ()
    {
        if (_ballScript.Dead)
        {
            //Set timer when player fails and then reset.
            if (_time >= _oldTime)
            {
                _timers[1].text = _bestText + FormatTimer(_time);
                _oldTime = _time;
            }
            _time = 0;
        }
        else
        {
            _time += Time.deltaTime;
            _timers[0].text = FormatTimer(_time);
        }
    }

    private string FormatTimer(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliSeconds = Mathf.FloorToInt((time * 10) % 10);
        return string.Format("{0:#0}:{1:00}.{2:0}", minutes, seconds, milliSeconds);
    }
}
