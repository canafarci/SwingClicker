using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAnimation : MonoBehaviour
{
    InputReader _reader;
    Animator _animator;
    Coroutine _timeDecayRoutine;
    void Awake()
    {
        _reader = FindObjectOfType<InputReader>();
        _animator = GetComponent<Animator>();
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
        _animator.SetBool("Faster", true);
        yield return new WaitForSeconds(.25f);
        _animator.SetBool("Faster", false);
        _timeDecayRoutine = null;
    }
}
