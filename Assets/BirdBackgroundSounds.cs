using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBackgroundSounds : MonoBehaviour
{

    public AudioSource[] birdSounds;
    public bool playBirdSounds = false;
    [SerializeField] private float playBirdSoundVar1;
    [SerializeField] private float playBirdSoundVar2;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("PlayBirdSound", playBirdSoundVar1, playBirdSoundVar2);

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
