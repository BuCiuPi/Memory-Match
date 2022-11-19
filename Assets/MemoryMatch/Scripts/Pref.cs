
using UnityEngine;

public static class Pref 
{
    public static int bestMoves
    {
        set
        {
            int oldMove = PlayerPrefs.GetInt(PrefKey.BestScore.ToString());
            if(oldMove > value || oldMove == 0)
            {
                PlayerPrefs.SetInt(PrefKey.BestScore.ToString(), value);
            }
        }
        get => PlayerPrefs.GetInt(PrefKey.BestScore.ToString(), 0);
    }

    public static int currentLevel
    {
        set
        {
            PlayerPrefs.SetInt(PrefKey.CurrentLevel.ToString(), value);
        }
        get => PlayerPrefs.GetInt(PrefKey.CurrentLevel.ToString(),0);
    }


    
}
