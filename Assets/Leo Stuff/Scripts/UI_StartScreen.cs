using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_StartScreen : MonoBehaviour
{
  [SerializeField] private List<GameObject> SpriteObj;
  [SerializeField] private List<GameObject> ParticleObj;


  [SerializeField] public string FirstLevel;
  [SerializeField] public float fadeOutDur;
  [SerializeField] public float fadeInDur;
  [SerializeField] public float changeSceneDur;
  private bool isTransition = false;

  private void Awake()
  {
    Color color = Color.clear;
    foreach (GameObject o in SpriteObj) {
      o.GetComponent<SpriteRenderer>().color = color;
    }
  }


  // Start is called before the first frame update
  void Start()
  {
    StartCoroutine(StartScene());
  }

  // Update is called once per frame
  void Update()
  {
    if(!isTransition && Input.GetMouseButtonDown(0))
    {
      StartCoroutine(ChangeScene());
    }
  }

  private IEnumerator StartScene()
  {
    Debug.Log("Starting Scene");
    //Fade Canvas and Objects
    float et = 0.0f;
    Color color = Color.white;

    while (et < fadeOutDur)
    {
      et += Time.deltaTime;
      color.a = Mathf.Clamp01(et / fadeOutDur);
      foreach (GameObject o in SpriteObj)
      {
        o.GetComponent<SpriteRenderer>().color = color;
      }

      yield return null;
      color.a = 1;
      foreach (GameObject o in SpriteObj)
      {
        o.GetComponent<SpriteRenderer>().color = color;
      }
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
      color.a = 0;
      foreach (GameObject o in SpriteObj)
      {
        o.GetComponent<SpriteRenderer>().color = color;
      }
    }

    while (et < changeSceneDur)
    {
      et += Time.deltaTime;
      yield return null;
    }

    Debug.Log("Changing Scene");

    //Change Scene
    SceneManager.LoadScene(FirstLevel);
  }

}
