using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeOutDialog : Dialog
{
    public GameManager gameManager;
    public void BackToMenu()
    {
        if (SceneController.Ins)
        {
            SceneController.Ins.LoadCurrentScene();
        }
    }

    public void Replay()
    {
        gameObject.SetActive(false);
        gameManager?.PlayGame();
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
