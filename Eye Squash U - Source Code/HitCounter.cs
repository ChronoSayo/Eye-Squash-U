using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Counts amount of hits the ball does on walls.
public class HitCounter : MonoBehaviour
{
    public Transform ball;

    private Ball _ballScript;
    private int _oldHits;
    private string _hitsText, _bestText;
    private List<Text> _hits;

    void Start()
    {
        _hits = new List<Text>();
        foreach (Transform t in transform)
            _hits.Add(t.GetComponent<Text>());

        _ballScript = ball.GetComponent<Ball>();

        _hitsText = "Hits: ";
        _bestText = "Best: ";

        _hits[0].text = _hitsText;
        _hits[1].text = _bestText + "???";

        _oldHits = 0;
    }

    void Update()
    {
        //Updates most amount of hits as they precede previous high score.
        _hits[0].text = _hitsText + _ballScript.Hits.ToString();
        if (_ballScript.Hits < _oldHits)
            _hits[1].text = _bestText + _oldHits;
        else
            _oldHits = _ballScript.Hits;
    }
}
