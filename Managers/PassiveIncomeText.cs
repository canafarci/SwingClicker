using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using DG.Tweening;

public class PassiveIncomeText : MonoBehaviour
{
    TextMeshProUGUI _text;
    InputReader _reader;
    float _moneyPerSecond;
    Coroutine _timeDecayRoutine;
    float _multiplier = 1f;
    Vector3 _startScale;

    void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _reader = FindObjectOfType<InputReader>();
        _startScale = _text.transform.localScale;
    }
    private void OnEnable() => _reader.ClickHandler += OnClick;
    private void OnDisable() => _reader.ClickHandler -= OnClick;
    private void OnClick()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(_text.transform.DOScale(_startScale * 1.2f, 0.01f));
        seq.Append(_text.transform.DOScale(_startScale, 0.2f));

        if (_timeDecayRoutine != null)
            StopCoroutine(_timeDecayRoutine);

        _timeDecayRoutine = StartCoroutine(TimeDecay());
    }

    public void UpdateText(float moneyPerSwing, int upgradedSeatCount, int swingCount)
    {
        _moneyPerSecond = ((moneyPerSwing * swingCount) + (moneyPerSwing * upgradedSeatCount)) / 2f * 1.134512f;
        SetText();
    }

    void SetText()
    {
        _text.text = (_moneyPerSecond * _multiplier).ToString("F2") + "$/sec";
    }

    IEnumerator TimeDecay()
    {
        _multiplier = 2f;
        SetText();
        yield return new WaitForSeconds(1f);
        _multiplier = 1f;
        SetText();
        _timeDecayRoutine = null;
    }
}
