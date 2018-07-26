using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCrack : MonoBehaviour
{
    public Transform ball;
    public Transform mainCamera;
    public Transform audioComponent;

    private Audio _audioScript;
    private float _removeTime, _shakeTime;
    private float _z, _shake;
    private bool _hit;
    private SpriteRenderer _currentSprite;
    private List<SpriteRenderer> _sprites;

    void Start()
    {
        _audioScript = audioComponent.GetComponent<Audio>();

        _sprites = new List<SpriteRenderer>();
        foreach (Transform t in transform)
        {
            if(t.GetComponent<SpriteRenderer>())
            {
                t.GetComponent<SpriteRenderer>().enabled = false;
                _sprites.Add(t.GetComponent<SpriteRenderer>());
            }
        }

        _removeTime = 1;
        _shakeTime = 0.2f;

        _z = _sprites[0].transform.position.z;
        _shake = 1;
    }

    void Update()
    {
        if (_hit)
            ShakeCamera();
    }

    private IEnumerator FixCamera()
    {
        yield return new WaitForSeconds(_removeTime);
        _currentSprite.enabled = false;
    }

    private IEnumerator ShakeCameraTime()
    {
        yield return new WaitForSeconds(_shakeTime);
        ResetShake();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.transform == ball)
        {
            _currentSprite = GetRandomSprite();
            _currentSprite.enabled = true;

            //Add crack on where the ball hits.
            RaycastHit hit;
            if (Physics.Raycast(col.transform.position, col.transform.forward, out hit))
                _currentSprite.transform.position = new Vector3(hit.point.x, hit.point.y, _z);

            _hit = true;

            StartCoroutine(FixCamera());
            StartCoroutine(ShakeCameraTime());

            _audioScript.PlayCrack();
        }
    }

    private void ShakeCamera()
    {
        if (_shake > 0)
        {
            float shakeAmount = 0.1f;
            float decrease = 1;
            mainCamera.localPosition = Random.insideUnitSphere * shakeAmount;
            _shake -= Time.deltaTime * decrease;
        }
    }

    private void ResetShake()
    {
        mainCamera.position = Vector3.zero;
        _shake = 1;
        _hit = false;
    }

    //Adds variety on the cracks from the same sprite.
    private SpriteRenderer GetRandomSprite()
    {
        SpriteRenderer sprite = _sprites[Random.Range(0, _sprites.Count)];
        sprite.flipX = FiftyFifty;
        sprite.flipY = FiftyFifty;
        return sprite;
    }

    private bool FiftyFifty
    {
        get { return Random.Range(0, 2) == 0 ? true : false; }
    }
}
