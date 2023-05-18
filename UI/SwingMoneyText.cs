using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System;

public class SwingMoneyText : MonoBehaviour
{
    TextMeshProUGUI _text;
    Income _income;
    Vector3 _startPos, _textStartPos;
    Transform _canvas, _parent;
    public bool Upgraded { set { _upgraded = value; } }
    bool _upgraded = false;

    private void Awake()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>(true);
        _income = FindObjectOfType<Income>();
        _startPos = transform.localPosition;
        _textStartPos = _text.transform.localPosition;
        _parent = transform.parent;
    }
    private void OnEnable()
    {
        _income.IncomeTickHandler += OnIncomeTick;
    }
    private void OnDisable()
    {
        _income.IncomeTickHandler -= OnIncomeTick;
    }
    private void OnIncomeTick(float money)
    {
        transform.parent = null; //
        _text.gameObject.SetActive(true);
        _text.DOFade(1f, 0.01f);
        money = money * (_upgraded ? 2f : 1f);
        if (money == (int)money)
            _text.text = "$" + money.ToString("F0");
        else
            _text.text = "$" + money.ToString("F1");

        _text.transform.DOLocalMoveY(_startPos.y + 1f, 0.5f).onComplete = () =>
        {
            _text.DOFade(0f, 0.5f).onComplete = () =>
            {
                transform.parent = _parent;
                transform.localPosition = _startPos;
                _text.transform.localPosition = _textStartPos;
            };

        };
    }
}
