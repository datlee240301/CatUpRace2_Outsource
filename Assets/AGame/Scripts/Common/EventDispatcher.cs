using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class EventDispatcher 
{
    private static Dictionary<EventID, List<Action<object>>> eventListeners = new Dictionary<EventID, List<Action<object>>>();

    public static void RegisterListener(EventID eventID, Action<object> callback)
    {
        if (!eventListeners.ContainsKey(eventID))
        {
            eventListeners[eventID] = new List<Action<object>>();
        }
        eventListeners[eventID].Add(callback);
    }

    public static void PostEvent(EventID eventID, object param = null)
    {
        if (eventListeners.ContainsKey(eventID))
        {
            List<Action<object>> listeners = eventListeners[eventID];
            foreach (Action<object> listener in listeners)
            {
                listener.Invoke(param);
            }
        }

        Debug.Log("post event " + eventID);
    }

    public static void RemoveListener(EventID eventID, Action<object> callback)
    {
        if (eventListeners.ContainsKey(eventID))
        {
            List<Action<object>> listeners = eventListeners[eventID];
            listeners.Remove(callback);
        }
    }
}


