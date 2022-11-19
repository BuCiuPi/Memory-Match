using System;
using UnityEngine;

[CreateAssetMenu(menuName ="Events/Card/Card Event Channel")]
public class CardEventChannel : ScriptableObject
{
    public Action<IItemCard> OnEventRaised;

    public void RaiseEvent(IItemCard itemInfo)
    {
        #if UNITY_EDITOR
            if (OnEventRaised == null)
            {
                Debug.LogError($"No Listen to this Event {name}");
            }
        #endif
        OnEventRaised?.Invoke(itemInfo);
    }
}

