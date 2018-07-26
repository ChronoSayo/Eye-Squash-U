using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Transform audioComponent;
    public Transform player;
    public Transform shadow;
    public float speed;

    private Audio _audioScript;
    private Vector3 _direction;
    private Vector3 _shadowDefaultSize;
    private int _hits;
    private float _speedY, _speedX, _startPositionZ;
    private float _respawnTime;
    private bool _dead;

    void Start ()
    {
        _audioScript = audioComponent.GetComponent<Audio>();

        _startPositionZ = transform.position.z;
        _direction = transform.forward;
        _shadowDefaultSize = shadow.localScale;

        Respawn();

        _hits = 0;
    }
    
    void Update ()
    {
        ForwardMovement();
        //Custom-made shadow to get a 3D feel and better see depth of where the ball is.
        CastShadow();
    }

    private void CastShadow()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, 100))
        {
            if (hit.transform.name.Contains("Kill") || hit.transform.name.Contains("Player"))
            {
                //Hide it somewhere.
                shadow.position = 10000 * Vector3.up;
            }
            else
            {
                //Offset shadow from the ground to avoid clipping.
                int scalingY = 6;
                shadow.position = hit.point + (hit.transform.forward / scalingY);
                shadow.rotation = hit.transform.rotation;

                //Adjust size of shadow based of distant, with some added scaling.
                int scalingDist = 4;
                float dist = Vector3.Distance(shadow.position, transform.position) / scalingDist;
                shadow.localScale = new Vector3(_shadowDefaultSize.x + dist, _shadowDefaultSize.y + dist, _shadowDefaultSize.z);

                //Add transparency based on distant, with some added scaling.
                Material shadowMat = shadow.GetComponent<Renderer>().material;
                Color shadowColor = shadowMat.color;
                float halfDistScaling = dist / 2;
                int fullAlpha = 1;
                float alpha = fullAlpha - halfDistScaling;
                shadowMat.color = new Color(shadowColor.r, shadowColor.g, shadowColor.b, alpha);
            }
        }
    }

    private void ForwardMovement()
    {
        transform.position += transform.forward * (speed * Time.deltaTime);
    }

    private IEnumerator RespawnTimer()
    {
        yield return new WaitForSeconds(_respawnTime);
        Respawn();
    }

    private void Respawn()
    {
        _respawnTime = 1;
        _dead = false;
        transform.forward = _direction;
        transform.position = new Vector3(0, 0, _startPositionZ);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.transform == player)
        {
            transform.forward -= Vector3.Reflect(player.position, Vector3.forward);
            Hits = 0;
        }
        else
        {
            transform.forward = Vector3.Reflect(transform.forward, col.transform.forward);
            Hits++;
        }

        _audioScript.PlayWallHit();
    }

    public bool Dead
    {
        set
        {
            if (value)
            {
                _audioScript.PlayDeath();
                StartCoroutine(RespawnTimer());
            }
            _dead = value;
        }
        get { return _dead; }
    }

    public int Hits
    {
        set { _hits = value; }
        get { return _hits; }
    }
}
