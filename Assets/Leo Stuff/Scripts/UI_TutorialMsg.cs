using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_TutorialMsg : MonoBehaviour
{
  [SerializeField] private float fadeTime;

  [SerializeField] private bool UseAutomaticTriggers = true;
  [SerializeField] private float MessageDuration;
  [SerializeField] private float ActivationDistance;

  [SerializeField] private bool DebugFlip;

  private bool isActivated = false;
  private float lifeTimer = 0;

  private void Awake()
  {
    //Always start faded 
    GetComponent<CanvasGroup>().alpha = 0;
  }

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    Vector3 vec = transform.position - Camera.main.transform.position;
    float distance = vec.magnitude;
    FaceCamera(vec);

    //Default Logic for triggering the tutorial message
    if (!UseAutomaticTriggers)
      return;

    if (!isActivated)
    {
      if (distance <= ActivationDistance)
        ActivateMessage();
    }
    else
    {
      lifeTimer += Time.deltaTime;

      if (distance > ActivationDistance || lifeTimer >= MessageDuration)
        DeactivateMessage();
    }
  }

  public void ActivateMessage()
  {
    if (DebugFlip)
    {
      Debug.Log("Tutorial Message Activated");
    }

    isActivated = true;
    StartCoroutine(FadeIn());
  }

  public void DeactivateMessage()
  {
    if (DebugFlip)
    {
      Debug.Log("Tutorial Message Deactivated");
    }

    isActivated = false;
    StartCoroutine(FadeOut());
  }


  void FaceCamera(Vector3 dVec)
  {
    if (DebugFlip)
    {
      Debug.Log("Distance to Camera:" 
        + dVec.magnitude);
    }

    //transform.LookAt(Camera.main.transform, transform.up);
    transform.rotation = Quaternion.LookRotation(dVec) * Quaternion.Euler(0, 0, 0);
  }




  private IEnumerator FadeIn()
  {
    //Fade Canvas and Objects
    float et = 0.0f;

    while (et < fadeTime)
    {
      et += Time.deltaTime;
      GetComponent<CanvasGroup>().alpha = Mathf.Clamp01(et / fadeTime);
      yield return null;
      GetComponent<CanvasGroup>().alpha = 1;
    }
  }

  private IEnumerator FadeOut()
  {
    //Fade Canvas and Objects
    float et = 0.0f;

    while (et < fadeTime)
    {
      et += Time.deltaTime;
      GetComponent<CanvasGroup>().alpha = 1.0f - Mathf.Clamp01(et / fadeTime);
      yield return null;
      GetComponent<CanvasGroup>().alpha = 0;
    }
  }


}
