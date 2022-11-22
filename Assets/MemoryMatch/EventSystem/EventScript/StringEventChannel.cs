using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringEventChannel : ScriptableObject
{
    public Action<string> OnEventRaised;

    public void RaiseEvent(string value)
    {
#if UNITY_EDITOR
        if (OnEventRaised == null)
        {
            Debug.LogError($"No Listen to this Event {name}");
        }
#endif
        OnEventRaised?.Invoke(value);
    }
}
