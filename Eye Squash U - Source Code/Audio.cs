using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioClip deathClip;
    public List<AudioClip> wallHitClips;
    public List<AudioClip> crackClips;

    //Plays Twinkle Twinkle Little Star if you hold left click mouse.
    private int _twinkleLittleStarsCounter;
    private List<AudioClip> _twinkleLittleStars;

    private AudioSource _audio;

    void Start ()
    {
        _audio = GetComponent<AudioSource>();

        _twinkleLittleStarsCounter = 0;
        _twinkleLittleStars = new List<AudioClip>();
        TwinkleLittleStar();
    }

    public void PlayWallHit()
    {
        if (Input.GetMouseButton(0))
            PlayTwinkleLittleStar();
        else
        {
            _audio.PlayOneShot(wallHitClips[Random.Range(0, wallHitClips.Count)]);
            _twinkleLittleStarsCounter = 0;
        }
    }

    public void PlayDeath()
    {
        _audio.PlayOneShot(deathClip);
    }

    public void PlayCrack()
    {
        _audio.PlayOneShot(crackClips[Random.Range(0, crackClips.Count)]);
    }

    //Plays added notes after each other.
    public void PlayTwinkleLittleStar()
    {
        _audio.PlayOneShot(_twinkleLittleStars[_twinkleLittleStarsCounter]);
        _twinkleLittleStarsCounter++;
        if (_twinkleLittleStarsCounter >= _twinkleLittleStars.Count)
            _twinkleLittleStarsCounter = 0;
    }

    //Adds all the notes in order.
    private void TwinkleLittleStar()
    {
        AudioClip C = wallHitClips[0];
        AudioClip D = wallHitClips[1];
        AudioClip E = wallHitClips[2];
        AudioClip F = wallHitClips[3];
        AudioClip G = wallHitClips[4];
        AudioClip A = wallHitClips[5];

        //Intro
        Intro(C, D, E, F, G, A);
        Mid(D, E, F, G);
        Intro(C, D, E, F, G, A);
    }

    private void Mid(AudioClip D, AudioClip E, AudioClip F, AudioClip G)
    {
        for (int i = 0; i < 2; i++)
        {
            DoubleAdd(G);
            DoubleAdd(F);
            DoubleAdd(E);
            SingleAdd(D);
        }
    }

    private void Intro(AudioClip C, AudioClip D, AudioClip E, AudioClip F, AudioClip G, AudioClip A)
    {
        DoubleAdd(C);
        DoubleAdd(G);
        DoubleAdd(A);
        SingleAdd(G);

        DoubleAdd(F);
        DoubleAdd(E);
        DoubleAdd(D);
        SingleAdd(C);
    }

    private void DoubleAdd(AudioClip clip)
    {
        for (int i = 0; i < 2; i++)
            _twinkleLittleStars.Add(clip);
    }

    private void SingleAdd(AudioClip clip)
    {
        _twinkleLittleStars.Add(clip);
    }
}
