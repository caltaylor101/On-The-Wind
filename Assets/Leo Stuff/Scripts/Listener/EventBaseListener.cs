using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


// T->Type, E->Event, UER->Unity Event Response
public abstract class EventBaseListener<T, E, UER> : MonoBehaviour,
    IEventListener<T> where E : EventBase<T> where UER : UnityEvent<T>
{
  [SerializeField] private E gameEvent;
  public E GameEvent { get { return gameEvent; } set { gameEvent = value; } }

  [SerializeField] private UER unityEventResponse;


  private void OnEnable()
  {
    if (gameEvent == null) { return; }

    GameEvent.RegisterListener(this);
  }

  private void OnDisable()
  {
    if (gameEvent == null) { return; }

    GameEvent.UnregisterListener(this);
  }

  public void OnEventRaised(T data)
  {
    if (unityEventResponse == null) { return; }
    unityEventResponse.Invoke(data);
  }
}
