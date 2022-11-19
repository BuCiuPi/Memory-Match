using UnityEngine;

public class PauseDialog : Dialog
{

    public override void Show(bool isShow)
    {
        Time.timeScale = 0f;
        base.Show(isShow);
        
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        Close();
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        if (SceneController.Ins)
        {
            SceneController.Ins.LoadCurrentScene();
        }
    }
}
