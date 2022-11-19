using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeOutDialog : Dialog
{
    public void BackToMenu()
    {
        if (SceneController.Ins)
        {
            SceneController.Ins.LoadCurrentScene();
        }
    }

    public void Replay()
    {
        SceneManager.sceneLoaded += OnSceneLoadEvent;
        if (SceneController.Ins)
        {
            SceneController.Ins.LoadCurrentScene();
        }
    }

    public void OnSceneLoadEvent(Scene scene , LoadSceneMode mode)
    {
        // if (GameManager.Ins)
        // {
        //     GameManager.Ins.PlayGame();
        // }
        // SceneManager.sceneLoaded -= OnSceneLoadEvent;
    }
}