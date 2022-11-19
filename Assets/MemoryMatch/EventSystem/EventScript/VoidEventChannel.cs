using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Events/Void Event Channel"))]
public class VoidEventChannel : ScriptableObject
{
    public Action OnEventRaised;
    public void RaiseEvent()
    {
        #if UNITY_EDITOR
            if (OnEventRaised == null)
            {
                Debug.LogError($"No Listen to this Event {name}");
            }
        #endif
        OnEventRaised?.Invoke();
    }
}