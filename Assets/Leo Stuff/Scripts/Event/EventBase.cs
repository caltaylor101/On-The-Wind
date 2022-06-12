using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventBase<T> : ScriptableObject
{
  private readonly List<IEventListener<T>> eventListeners = new List<IEventListener<T>>();
  public bool DebugMsg = false;

  public void Raise(T item)
  {
    if (DebugMsg)
      Debug.Log(DebugLog(item));

    for (int i = eventListeners.Count - 1; i >= 0; i--)
      eventListeners[i].OnEventRaised(item);
  }

  public void RegisterListener(IEventListener<T> listener)
  {
    if (eventListeners.Contains(listener)) { return; }
    eventListeners.Add(listener);
  }

  public void UnregisterListener(IEventListener<T> listener)
  {
    if (!eventListeners.Contains(listener)) { return; }
    eventListeners.Remove(listener);
  }

  public string DebugLog(T item)
  {
    return "Raised Event [" + this + "] with value [" + item + "].";
  }
}


