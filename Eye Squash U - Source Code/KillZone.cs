using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.name.Contains("Ball"))
        {
            if (!col.GetComponent<Ball>().Dead)
            {
                col.GetComponent<Ball>().Dead = true;
                col.GetComponent<Ball>().Hits = 0;
            }
        }
    }
}
