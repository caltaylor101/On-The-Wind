using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_StartScreen : MonoBehaviour
{
  [SerializeField] public string FirstLevel;
  [SerializeField] public float fadeOutDur;
  [SerializeField] public float fadeInDur;  
  private bool isTransition = false;

  private void Awake()
  {
    GetComponent<CanvasGroup>().alpha = 0;
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
    //Fade Canvas and Objects
    float et = 0.0f;

    while (et < fadeOutDur)
    {
      et += Time.deltaTime;
      GetComponent<CanvasGroup>().alpha = Mathf.Clamp01(et / fadeOutDur);
      yield return null;
      GetComponent<CanvasGroup>().alpha = 1;
    }
  }

  private IEnumerator ChangeScene()
  {
    //Fade Canvas and Objects
    float et = 0.0f;

    while (et < fadeOutDur)
    {
      et += Time.deltaTime;
      GetComponent<CanvasGroup>().alpha = 1.0f - Mathf.Clamp01(et / fadeOutDur);
      yield return null;
      GetComponent<CanvasGroup>().alpha = 0;
    }

    //Change Scene
    SceneManager.LoadScene(FirstLevel);

  }

}
