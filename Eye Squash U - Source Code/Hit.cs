using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Hit effect for better feedback where the ball bounces.
public class Hit : MonoBehaviour
{
    private Material _material;
    private Color _defaultColor, _hitColor;
    private float _blinkTime;

    void Start ()
    {
        _material = transform.GetComponent<Renderer>().material;
        _defaultColor = _material.color;
        _hitColor = Color.green;

        _blinkTime = 0.2f;
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(_blinkTime);
        _material.color = _defaultColor;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.transform.name == "Ball")
        {
            _material.color = _hitColor;
            StartCoroutine(Reset());
        }
    }
}
