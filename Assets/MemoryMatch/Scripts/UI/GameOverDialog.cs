using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverDialog : Dialog
{
    public Text totalMovesText;
    public Text bestMovesText;

    public override void Show(bool isShow)
    {
        // base.Show(isShow);
        // if (totalMovesText)
        // {
        //     totalMovesText.text = GameManager.Ins.TotalMoving.ToString();
        // }
        // if (bestMovesText)
        // {
        //     bestMovesText.text = Pref.bestMoves.ToString(); 
        // }
    }

    public void Continue()
    {
        SceneManager.sceneLoaded += OnSceneLoadEvent;
        if (SceneController.Ins)
        {
            SceneController.Ins.LoadCurrentScene();
        }
    }

    public void OnSceneLoadEvent(Scene scene, LoadSceneMode mode)
    {
        // if (GameManager.Ins)
        // {
        //     GameManager.Ins.PlayGame();
        // }
        // SceneManager.sceneLoaded -= OnSceneLoadEvent;
    }
}
