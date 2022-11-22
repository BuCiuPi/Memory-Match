using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownTiming : MonoBehaviour
{
    #region Editor Variable
    [Header("Event Listener")]
    [SerializeField] private FloatEventChannel _OnStartCountDown;
    [SerializeField] private VoidEventChannel _OnPauseCountDown;
    [SerializeField] private VoidEventChannel _OnContinueCountDown;

    [Header("Event Raiser")]
    [SerializeField] private VoidEventChannel _EndCountDown;
    [SerializeField] private FloatEventChannel _CountDownTimeChange;
    [SerializeField] private IntEventChannel _CountDownTimeChangePerSec;
    [SerializeField] private FloatEventChannel _CountDownPercentChange;

    #endregion

    #region Variable

    private float _timeLimit;
    private float _timeLeft;
    private float _secLeft;
    private float _LastSecLeft;

    private CountDownState _state = CountDownState.End;

    #endregion

    #region Build-In Func

    // Update is called once per frame
    void Update()
    {
        TimeCountDown();
    }

    void OnEnable()
    {
        if (_OnStartCountDown) _OnStartCountDown.OnEventRaised += StartCountDown;
        if (_OnPauseCountDown) _OnPauseCountDown.OnEventRaised += PauseCountDown;
        if (_OnContinueCountDown) _OnContinueCountDown.OnEventRaised += ContinueCountDown;
    }

    void OnDisable()
    {
        if (_OnStartCountDown) _OnStartCountDown.OnEventRaised -= StartCountDown;
        if (_OnPauseCountDown) _OnPauseCountDown.OnEventRaised -= PauseCountDown;
        if (_OnContinueCountDown) _OnContinueCountDown.OnEventRaised -= ContinueCountDown;
    }

    #endregion

    #region Main Func

    public void TimeCountDown()
    {
        if (_state != CountDownState.Start) return;

        UpdateTime();
        UpdateTimeBySec();
        CheckEndCountDown();

        _CountDownTimeChange?.RaiseEvent(_timeLeft);
        _CountDownPercentChange?.RaiseEvent(GetCountDownPercent());
    }

    private void UpdateTime()
    {
        _timeLeft -= Time.deltaTime;
    }

    private void UpdateTimeBySec()
    {
        int currentSecLeft = Mathf.FloorToInt(_timeLeft);
        if (currentSecLeft < _LastSecLeft && currentSecLeft >= 0)
        {
            _secLeft = currentSecLeft;
            _LastSecLeft = _secLeft;

            _CountDownTimeChangePerSec?.RaiseEvent(currentSecLeft);
        }
    }

    private void CheckEndCountDown()
    {
        if (_timeLeft <= 0)
        {
            EndCountDown();
        }
    }

    private float GetCountDownPercent()
    {
        return _timeLeft / _timeLimit;
    }

    #endregion

    #region Event Func

    /// <summary>
    /// Start count-down
    /// </summary>
    /// <param name="time">second</param>
    public void StartCountDown(float time)
    {
        _timeLeft = _LastSecLeft = _timeLimit = time;
        _state = CountDownState.Start;
    }

    public void PauseCountDown()
    {
        _state = CountDownState.Pause;
    }

    public void ContinueCountDown()
    {
        _state = CountDownState.Start;
    }

    public void EndCountDown()
    {
        _state = CountDownState.End;

        _timeLeft = 0;
        _secLeft = 0;

        _EndCountDown?.RaiseEvent();
    }

    #endregion

}

public enum CountDownState
{
    Start,
    Pause,
    End
}
