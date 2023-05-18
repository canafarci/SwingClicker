using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Income : MonoBehaviour
{
    InputReader _reader;
    Coroutine _timeDecayRoutine;
    float _multiplier = 1f;
    UpgradeManager _manager;
    public event Action<float> IncomeTickHandler;
    void Awake()
    {
        _reader = FindObjectOfType<InputReader>();
        _manager = FindObjectOfType<UpgradeManager>();
    }
    private void OnEnable() => _reader.ClickHandler += OnClick;
    private void OnDisable() => _reader.ClickHandler -= OnClick;
    private void OnClick()
    {
        if (_timeDecayRoutine != null)
            StopCoroutine(_timeDecayRoutine);

        _timeDecayRoutine = StartCoroutine(TimeDecay());
    }
    IEnumerator TimeDecay()
    {
        _multiplier = 2f;
        yield return new WaitForSeconds(0.25f);
        _multiplier = 1f;
        _timeDecayRoutine = null;
    }
    public void IncomeTick()
    {
        float money = _manager.MoneyPerSwing * _manager.SwingCount * _multiplier;
        Resource.Instance.OnMoneyChange(money);
        IncomeTickHandler.Invoke(_manager.MoneyPerSwing * _multiplier);
    }
}
