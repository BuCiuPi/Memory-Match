
using UnityEngine;
[System.Serializable]
public class Level
{
    private int m_numOfLevel;
    public float timeLimt;
    public Sprite[] itemIcon;

    public int numOfLevel { get => m_numOfLevel; set => m_numOfLevel = value; }
}
