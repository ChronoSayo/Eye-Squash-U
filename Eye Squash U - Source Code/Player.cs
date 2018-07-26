using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.EyeTracking;

//Steers paddle.
public class Player : MonoBehaviour
{
    public Camera mainCamera;
    public Renderer eyecon; //Icon.

    private float _eyeSpeed;

    void Start ()
    {
        Cursor.visible = false;

        //The speed of which the paddle should follow the eye.
        _eyeSpeed = 3;
    }
    
    void Update ()
    {
        //Checks when eye tracker is available. Translates controls depending on setting.
        if (GetValidEyeTracking)
        {
            transform.position = Vector3.Lerp(transform.position, mainCamera.ScreenToWorldPoint(
                new Vector3(EyeTracking.GetGazePoint().Screen.x, EyeTracking.GetGazePoint().Screen.y, transform.position.z)), 
                _eyeSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = mainCamera.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        }

        //Displays icon when eye tracker is available.
        if (eyecon.enabled != GetValidEyeTracking)
            eyecon.enabled = GetValidEyeTracking;
    }

    private bool GetValidEyeTracking
    {
        get { return EyeTracking.GetGazeTrackingStatus().IsTrackingEyeGaze && EyeTracking.GetGazePoint().IsValid; }
    }
}
