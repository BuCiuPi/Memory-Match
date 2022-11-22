
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int timeLimit;
    public GameState state;

    [Header("Controller")]
    [SerializeField] private CardInventoryController cardInventoryController;
    [SerializeField] private GamePlayController gamePlayController;


    [Header("Event Listener")]

    [SerializeField] private VoidEventChannel _OnPlusTotalMoving;
    [SerializeField] private VoidEventChannel _OnPlusMatchedCard;

    [SerializeField] private VoidEventChannel _OnEndCountDown;

    [Header("Event Raiser")]
    [SerializeField] private IntEventChannel _SetGameState;

    [SerializeField] private VoidEventChannel _GameStart;
    [SerializeField] private VoidEventChannel _GamePause;
    [SerializeField] private VoidEventChannel _GameContinue;
    [SerializeField] private VoidEventChannel _GameEnd;

    [SerializeField] private FloatEventChannel _OnStartCountDown;

    private int _totalMatchItem;
    private int _totalMoving;
    private int _rightMoving;

    #region Build-In Func


    private void OnEnable()
    {
        if (_OnPlusTotalMoving) _OnPlusTotalMoving.OnEventRaised += PlusTotalMoving;
        if (_OnPlusMatchedCard) _OnPlusMatchedCard.OnEventRaised += PlusMatchesCard;
        if (_OnEndCountDown) _OnEndCountDown.OnEventRaised += OnGameOver;
    }

    private void OnDisable()
    {
        if (_OnPlusTotalMoving) _OnPlusTotalMoving.OnEventRaised -= PlusTotalMoving;
        if (_OnPlusMatchedCard) _OnPlusMatchedCard.OnEventRaised -= PlusMatchesCard;
        if (_OnEndCountDown) _OnEndCountDown.OnEventRaised -= OnGameOver;
    }

    public void PlusTotalMoving()
    {
        _totalMoving++;
    }

    public void PlusMatchesCard()
    {
        _totalMatchItem++;
    }

    #endregion

    #region Init Func

    public void PlayMusic()
    {

        if (AudioController.Ins)
        {
            AudioController.Ins.PlayBackgroundMusic();
        }
    }

    public void PlayGame()
    {
        Time.timeScale = 0f;
        _totalMatchItem = cardInventoryController.Init();
        gamePlayController.Init();
        PlayMusic();
        _GameStart?.RaiseEvent();
        _OnStartCountDown.RaiseEvent(15f);
        Time.timeScale = 1f;
    }

    #endregion

    #region GamePlayFunc

    private void CheckGameOver()
    {
        if (_rightMoving == _totalMatchItem)
        {
            OnGameOver();
        }
    }

    private void OnGameOver()
    {
        _GameEnd?.RaiseEvent();
        Time.timeScale = 0f;
        if (AudioController.Ins)
            AudioController.Ins.PlaySound("SE_TimeOut");
    }

    #endregion
}