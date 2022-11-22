using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Scriptable Object/Audio/Audio Info")]
public class AudioInfoSO : ScriptableObject
{
    public string ID;
    public AudioClip AudioClip;
    public string[] Tag;
}
