using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName =("Events/ Float Event Channel"))]
public class FloatEventChannel : ScriptableObject
{
    public Action<float> OnEventRaised;
    public void RaiseEvent(float value)
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
