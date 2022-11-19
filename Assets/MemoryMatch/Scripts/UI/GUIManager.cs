using UnityEngine.UI;
using UnityEngine;

public class GUIManager
{
    public GameObject mainMenu;
    public GameObject gamePlay;
    public Image timeBar;
    public PauseDialog pauseDialog;
    public GameOverDialog gameOverDialog;
    public TimeOutDialog timeOutDialog;
    public LevelText levelText;

    [Header("Event Listener")]

    [SerializeField] private FloatEventChannel _OnTimeChange;

    private void OnEnable() {
        if(_OnTimeChange) _OnTimeChange.OnEventRaised += UpdateTimeBar;
    }

    private void OnDisable() {
        if(_OnTimeChange) _OnTimeChange.OnEventRaised -= UpdateTimeBar;
    }

    public void UpdateTimeBar(float value)
    {
        if(timeBar) timeBar.fillAmount = value;
    }
}
