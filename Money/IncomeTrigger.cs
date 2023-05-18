using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncomeTrigger : MonoBehaviour
{
    Income _income;
    private void Awake()
    {
        _income = FindObjectOfType<Income>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ticker")) return;
        _income.IncomeTick();
    }
}
