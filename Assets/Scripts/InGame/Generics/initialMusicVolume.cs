using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initialMusicVolume : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("musicVolume");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
