using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeFade : MonoBehaviour
{
  //How many seconds for it to get to max volume to min and vice versa
  [SerializeField] private float fadeInTime = 2;
  [SerializeField] private float fadeOutTime = 2;
  [SerializeField] private bool EnableDebugInputs = false;
  [SerializeField] AudioSource baseMusic;
  [SerializeField] AudioSource bounceMusic;
  [SerializeField] AudioSource weatherMusic;

  private bool isFadeOut = false;
  private bool isFadeIn = false;

  //This script must be attached to an object with a AudioSource component
  private AudioSource source;

  private void Awake()
  {
  }

  // Start is called before the first frame update
  void Start()
  {
    
  }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && gameObject.tag == "Music")
        {
            Debug.Log("MusicStart");
            source = baseMusic;
            bounceMusic.volume = 0;
            baseMusic.volume = 0;
            weatherMusic.volume = 0;
            StartMusic();
            baseMusic.Play();
            bounceMusic.Play();
            weatherMusic.Play();
        }

        if (other.tag == "Player" && gameObject.tag == "BounceMusic")
        {
            Debug.Log("BounceMusicStart");
            source = bounceMusic;
            bounceMusic.volume = 0;
            //baseMusic.volume = 0;
            weatherMusic.volume = 0;
            StartMusic();
        }


    }

    // Update is called once per frame
    void Update()
  {
    if (EnableDebugInputs)
    {
      //Testing purposes
      if (Input.GetKeyDown(KeyCode.W))
        StartMusic();
      if (Input.GetKeyDown(KeyCode.S))
        StopMusic();
    }
  }

  public void StartMusic()
  {
    StartCoroutine(FadeIn());
  }

  public void StopMusic()
  {
    StartCoroutine(FadeOut());
  }


  private IEnumerator FadeIn()
  {
    float et = 0.0f;
    isFadeIn = true;
    isFadeOut = false;

    while (et < fadeInTime)
    {
      if (!isFadeIn)
        break;

      et += Time.deltaTime;
      source.volume = Mathf.Clamp01(et / fadeInTime);
      yield return null;
      source.volume = 1;
    }
  }

  private IEnumerator FadeOut()
  {
    float et = 0.0f;
    isFadeIn = false;
    isFadeOut = true;

    while (et < fadeOutTime)
    {
      if (!isFadeOut)
        break;

      et += Time.deltaTime;
      source.volume = 1.0f - Mathf.Clamp01(et / fadeOutTime);
      yield return null;
      source.volume = 0;
    }
  }
}
