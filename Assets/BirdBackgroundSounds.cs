using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBackgroundSounds : MonoBehaviour
{

    public AudioSource[] birdSounds;
    public bool playBirdSounds = false;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("PlayBirdSound", 1, 1);

        InvokeRepeating("PlayBirdSoundsSwitch", 3, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayBirdSound()
    {
        if (playBirdSounds)
        {
            AudioSource birdSoundToPlay = birdSounds[Random.Range(0, birdSounds.Length - 1)];
            birdSoundToPlay.Play();
        }
        
    }

    public void PlayBirdSoundsSwitch()
    {
        playBirdSounds = !playBirdSounds;
    }
}
