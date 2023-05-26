using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playSounds : MonoBehaviour
{
    [Header("On collision sounds (Random)")]
    public AudioClip[] onCollisionSounds;

    [Header("On initialize sounds (Random)")]
    public AudioClip[] onInitializeSounds;

    [Header("On update sounds (Random)")]
    public AudioClip[] onUpdateSounds;

    private int randomSound = 0;
    private AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        if (onUpdateSounds.Length > 0)
        {
            randomSound = Random.Range(0, onUpdateSounds.Length - 1);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (onInitializeSounds.Length > 0)
        {
            AudioClip sound = onInitializeSounds[Random.Range(0, onInitializeSounds.Length - 1)];

            // float hitVol = other.relativeVelocity.magnitude * velToVol;
            // print(sound);
            // print(Resources.Load("AudioClips/pingPongHit_01"));

            source.clip = sound;
            source.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (onUpdateSounds.Length > 0)
        {
            AudioClip sound = onUpdateSounds[randomSound];

            // float hitVol = other.relativeVelocity.magnitude * velToVol;
            // print(sound);
            // print(Resources.Load("AudioClips/pingPongHit_01"));

            source.clip = sound;
            source.Play();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        AudioClip sound = onCollisionSounds[Random.Range(0, onCollisionSounds.Length - 1)];

        // float hitVol = other.relativeVelocity.magnitude * velToVol;
        // print(sound);
        // print(Resources.Load("AudioClips/pingPongHit_01"));

        source.clip = sound;
        source.Play();
    }
}
