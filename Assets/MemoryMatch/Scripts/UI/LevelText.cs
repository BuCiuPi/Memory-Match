using UnityEngine.UI;
using UnityEngine;

public class LevelText : MonoBehaviour
{
    public Text textLevel;
    public void ShowTextLevel(string text)
    {
        textLevel.text = text;
    }
}
