using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : MonoBehaviour
{
    public bool Upgraded { get { return _upgraded; } }
    bool _upgraded = false;
    [SerializeField] GameObject _upgrade, _base;
    [SerializeField] SwingMoneyText _text;

    public void Unlock()
    {
        _text.gameObject.SetActive(true);
    }

    public void Upgrade()
    {
        _base.SetActive(false);
        _upgrade.SetActive(true);
        _upgraded = true;
        _text.Upgraded = true;
    }
}
