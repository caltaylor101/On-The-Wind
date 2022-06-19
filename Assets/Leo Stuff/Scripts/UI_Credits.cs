using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Credits : MonoBehaviour
{
  [SerializeField] private List<GameObject> TeamNameObj;
  [SerializeField] private GameObject BackObj;
  [SerializeField] private List<GameObject> LogoObj;

  [SerializeField] private float fadeInNameDur;
  [SerializeField] private float fadeInWaitDur;
  [SerializeField] private float fadeInLogoDur;

  [SerializeField] private bool DebugFlip = false;

  private bool isActivated = false;

  private void Awake()
  {
    Color c;

    //Set alphas to 0
    foreach (GameObject text in TeamNameObj)
    {

      c = text.GetComponent<TextMeshProUGUI>().color;
      c.a = 0;
      text.GetComponent<TextMeshProUGUI>().color = c;
    }

    c = BackObj.GetComponent<Image>().color;
    c.a = 0;
    BackObj.GetComponent<Image>().color = c;


    foreach (GameObject img in LogoObj)
    {

      c = img.GetComponent<Image>().color;
      c.a = 0;
      img.GetComponent<Image>().color = c;
    }

  }

  // Start is called before the first frame update
  void Start()
  {
   
  }

  // Update is called once per frame
  void Update()
  {
    if (DebugFlip && Input.GetKeyDown(KeyCode.P))
      StartFadeIn();
  }

  public void StartFadeIn()
  {
    if (!isActivated)
    {
      isActivated = !isActivated;
      StartCoroutine(FadeIn());
    }
  }


  private IEnumerator FadeIn()
  {
    //Text Fade
    float et = 0.0f;
    Color c = Color.white;

    while (et < fadeInNameDur)
    {
      et += Time.deltaTime;
      foreach (GameObject text in TeamNameObj)
      {
        c = text.GetComponent<TextMeshProUGUI>().color;
        c.a = Mathf.Clamp01(et / fadeInNameDur);
        text.GetComponent<TextMeshProUGUI>().color = c;
      }
      yield return null;
    }

    //Once faded in set alpha to 1
    foreach (GameObject text in TeamNameObj)
    {
      
      c = text.GetComponent<TextMeshProUGUI>().color;
      c.a = 1;
      text.GetComponent<TextMeshProUGUI>().color = c;
    }

    //Background Fade
    et = 0.0f;
    while (et < fadeInWaitDur)
    {
      et += Time.deltaTime;
      
      c = BackObj.GetComponent<Image>().color;
      c.a = Mathf.Clamp01(et / fadeInWaitDur);
      BackObj.GetComponent<Image>().color = c;

      yield return null;
    }
    c = BackObj.GetComponent<Image>().color;
    c.a = 1;
    BackObj.GetComponent<Image>().color = c;


    //Image Fade
    et = 0.0f;
    while (et < fadeInLogoDur)
    {
      et += Time.deltaTime;
      foreach (GameObject img in LogoObj)
      {
        c = img.GetComponent<Image>().color;
        c.a = Mathf.Clamp01(et / fadeInLogoDur);
        img.GetComponent<Image>().color = c;
      }
      yield return null;
    }

    //Once faded in set alpha to 1
    foreach (GameObject img in LogoObj)
    {

      c = img.GetComponent<Image>().color;
      c.a = 1;
      img.GetComponent<Image>().color = c;
    }


  }


}
