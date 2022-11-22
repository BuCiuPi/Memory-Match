using UnityEngine.UI;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject gamePlay;

    public Image timeBar;
    public PauseDialog pauseDialog;
    public GameOverDialog gameOverDialog;
    public TimeOutDialog timeOutDialog;
    public Text Time;

    [Header("Event Listener")]

    [SerializeField] private FloatEventChannel _OnTimeChange;
    [SerializeField] private IntEventChannel _OnTimeTextChange;

    [SerializeField] private VoidEventChannel _OnGameStart;
    [SerializeField] private VoidEventChannel _OnGamePause;
    [SerializeField] private VoidEventChannel _OnGameContinue;
    [SerializeField] private VoidEventChannel _OnGameEnd;

    private void OnEnable()
    {
        if (_OnTimeChange) _OnTimeChange.OnEventRaised += UpdateTimeBar;
        if (_OnTimeTextChange) _OnTimeTextChange.OnEventRaised += OnTimeTextUpdate;
        if (_OnGameStart) _OnGameStart.OnEventRaised += OnGameStart;
        if (_OnGamePause) _OnGamePause.OnEventRaised += OnGamePause;
        if (_OnGameContinue) _OnGameContinue.OnEventRaised += OnGameContinue;
        if (_OnGameEnd) _OnGameEnd.OnEventRaised += OnGameEnd;
    }

    private void OnDisable()
    {
        if (_OnTimeChange) _OnTimeChange.OnEventRaised -= UpdateTimeBar;
        if (_OnTimeTextChange) _OnTimeTextChange.OnEventRaised -= OnTimeTextUpdate;
        if (_OnGameStart) _OnGameStart.OnEventRaised -= OnGameStart;
        if (_OnGamePause) _OnGamePause.OnEventRaised -= OnGamePause;
        if (_OnGameContinue) _OnGameContinue.OnEventRaised -= OnGameContinue;
        if (_OnGameEnd) _OnGameEnd.OnEventRaised -= OnGameEnd;
    }

    private void OnGameStart()
    {
        mainMenu.SetActive(false);
        gamePlay.SetActive(true);
    }

    private void OnGamePause()
    {
        pauseDialog.Show(true);
    }

    private void OnGameContinue()
    {

    }

    private void OnGameEnd()
    {
        timeOutDialog.Show(true);
    }

    private void UpdateTimeBar(float value)
    {
        if (timeBar) timeBar.fillAmount = value;

    }

    private void OnTimeTextUpdate(int value)
    {
        Time.text = value.ToString();
    }
}
