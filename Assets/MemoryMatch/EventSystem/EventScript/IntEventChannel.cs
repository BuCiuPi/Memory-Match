using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Events/Int Event Channel"))]
public class IntEventChannel : ScriptableObject
{
    public Action<int> OnEventRaised;

    public void RaiseEvent(int value)
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
