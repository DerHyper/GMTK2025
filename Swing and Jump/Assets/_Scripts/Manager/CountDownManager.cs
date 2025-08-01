using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class CountDownManager : MonoBehaviour
{
    [SerializeField]
    private GameObject countdownPrefab;
    [SerializeField]
    private Transform NumberPosition;
    private FixedTimer _timer = new();

    private bool _timerHasStarted = false;
    private bool _countHasStarted = false;

    float countDownStartTimer = 10f;

    float timeScineLastCountdownUpdate = 0f;
    List<int> countDownNumbers = new List<int> { 5, 4, 3, 2, 1, 0 };
    int current = 0;

    private void Update()
    {
        if (!_timerHasStarted)
        {
            return;
        }

        _timer.Update();
        float time = _timer.GetTime();
        if (time > countDownStartTimer && !_countHasStarted)
        {
            _countHasStarted = true;

        }

        if (_countHasStarted && timeScineLastCountdownUpdate >= 1f)
        {
            timeScineLastCountdownUpdate = 0f;
            showCountDownNumber(countDownNumbers[current]);
            current++;
        }

        if (current >= countDownNumbers.Count-1)
        {
            EndCountDown();
        }
        timeScineLastCountdownUpdate += Time.deltaTime;
    }

    private void EndCountDown()
    {
        _timerHasStarted = false;
        GameManager.Instance.startFlying();
    }

    private void showCountDownNumber(int number)
    {
        GameObject numberObj = GameObject.Instantiate(countdownPrefab, NumberPosition.position, NumberPosition.rotation, NumberPosition);
        numberObj.GetComponent<CountDownNumber>().Init(number);
    }

    public void StartCountDown()
    {
        _timer.Start();
        _timerHasStarted = true;
    }
}
