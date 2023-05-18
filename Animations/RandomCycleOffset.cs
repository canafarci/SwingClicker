using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomCycleOffset : MonoBehaviour
{
    string _animName;
    Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        int randInt = Random.Range(0, 4);
        _animator.Play(randInt.ToString(), 0, Random.Range(0, _animator.GetCurrentAnimatorStateInfo(0).length));
    }
}
