using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_StartScreen : MonoBehaviour
{
  [SerializeField] private List<GameObject> SpriteObj;
  [SerializeField] private List<GameObject> ParticleObj;
  [SerializeField] private GameObject AmbientObj;


  [SerializeField] public string FirstLevel;
  [SerializeField] public float fadeInDur;
  [SerializeField] public float fadeOutDur;
  [SerializeField] public float changeSceneDur;
  private bool isTransition = false;

  private AudioSource source;
  [SerializeField] private float originalVolume;

  private void Awake()
  {
    Color color = Color.clear;
    foreach (GameObject o in SpriteObj) {
      o.GetComponent<SpriteRenderer>().color = color;
    }

    source = AmbientObj.GetComponent<AudioSource>();
    //originalVolume = source.volume;
  }


  // Start is called before the first frame update
  void Start()
  {
    StartCoroutine(StartScene());
    StartCoroutine(FadeAmbientIn());
  }

  // Update is called once per frame
  void Update()
  {
    if(!isTransition && Input.GetMouseButtonDown(0))
    {
      StartCoroutine(ChangeScene());
      StartCoroutine(FadeAmbientOut());
    }
  }

  private IEnumerator FadeAmbientIn()
  {
    float et = 0.0f;

    while (et < fadeInDur)
    {
      et += Time.deltaTime;
      source.volume = originalVolume * (et / fadeInDur);
      yield return null;
      source.volume = originalVolume;
    }
  }

  private IEnumerator FadeAmbientOut()
  {
    float et = 0.0f;

    while (et < fadeOutDur)
    {
      et += Time.deltaTime;
      source.volume = originalVolume - (originalVolume * (et / fadeOutDur));
      yield return null;
      source.volume = 0;
    }
  }


  private IEnumerator StartScene()
  {
    Debug.Log("Starting Scene");
    //Fade Canvas and Objects
    float et = 0.0f;
    Color color = Color.white;

    while (et < fadeInDur)
    {
      et += Time.deltaTime;
      color.a = Mathf.Clamp01(et / fadeInDur);
      foreach (GameObject o in SpriteObj)
      {
        o.GetComponent<SpriteRenderer>().color = color;
      }

      yield return null;
    }

    color.a = 1;
    foreach (GameObject o in SpriteObj)
    {
      o.GetComponent<SpriteRenderer>().color = color;
    }

  }

  private IEnumerator ChangeScene()
  {
    //Fade Canvas and Objects
    float et = 0.0f;
    Color color = Color.white;

    //Start Particle System
    foreach (GameObject o in ParticleObj)
    {
      o.GetComponent<ParticleSystem>().Play();
    }

    while (et < fadeOutDur)
    {
      et += Time.deltaTime;
      color.a = 1.0f - Mathf.Clamp01(et / fadeOutDur);
      foreach (GameObject o in SpriteObj)
      {
        o.GetComponent<SpriteRenderer>().color = color;
      }

      yield return null;
    }

    color.a = 0;
    foreach (GameObject o in SpriteObj)
    {
      o.GetComponent<SpriteRenderer>().color = color;
    }

    while (et < changeSceneDur)
    {
      et += Time.deltaTime;
      yield return null;
    }

    Debug.Log("Changing Scene");

    //Change Scene
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
  }

}
