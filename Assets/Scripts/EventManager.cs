using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    private Dictionary<string, UnityEvent> eventDictionary;

    public static EventManager instance;

    private void Awake()
    {
        instance = this;
        eventDictionary = new Dictionary<string, UnityEvent>();
    }

    public static void StartListening(string eventName, UnityAction listener)
    {
        UnityEvent value = null;
        if(instance.eventDictionary.TryGetValue(eventName, out value))
        {
            value.AddListener(listener);
            return;
        }
        value = new UnityEvent();
        value.AddListener(listener);
        instance.eventDictionary.Add(eventName, value);
    }

    public static void StopListening(string eventName, UnityAction listener)
    {
        if(!(instance == null))
        {
            UnityEvent value = null;
            if (instance.eventDictionary.TryGetValue(eventName, out value))
            {
                value.RemoveListener(listener);
            }
        }
    }

    public static void TriggerEvent(string eventName)
    {
        UnityEvent value = null;
        if (instance.eventDictionary.TryGetValue(eventName, out value))
        {
            value.Invoke();
        }
    }

    public void LocalTriggerEvent(string eventName)
    {
        TriggerEvent(eventName);
    }

}
