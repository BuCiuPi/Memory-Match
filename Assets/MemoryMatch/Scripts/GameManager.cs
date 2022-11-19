
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

    [Header("Event Raiser")]
    [SerializeField] private IntEventChannel _SetGameState;

    [SerializeField] private VoidEventChannel _GameStart;
    [SerializeField] private VoidEventChannel _GamePause;
    [SerializeField] private VoidEventChannel _GameContinue;
    [SerializeField] private VoidEventChannel _GameEnd;

    private int _totalMatchItem;
    private int _totalMoving;
    private int _rightMoving;

    #region Build-In Func

    private void Start()
    {
        _totalMatchItem = cardInventoryController.Init();
        gamePlayController.Init();
    }

    private void OnEnable()
    {
        if (_OnPlusTotalMoving) _OnPlusTotalMoving.OnEventRaised += PlusTotalMoving;
        if (_OnPlusMatchedCard) _OnPlusMatchedCard.OnEventRaised += PlusMatchesCard;
    }

    private void OnDisable()
    {
        if (_OnPlusTotalMoving) _OnPlusTotalMoving.OnEventRaised -= PlusTotalMoving;
        if (_OnPlusMatchedCard) _OnPlusMatchedCard.OnEventRaised -= PlusMatchesCard;
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
        PlayMusic();
    }

    #endregion

    #region GamePlayFunc

    private void CheckGameOver()
    {
        if (_rightMoving == _totalMatchItem)
        {
            _GameEnd?.RaiseEvent();
        }
    }

    #endregion
}