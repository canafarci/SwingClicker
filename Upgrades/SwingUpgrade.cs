using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SwingUpgrade : Upgrade
{
    Sprite _oldSprite;
    bool _canUpgrade = true;
    bool _hasLoaded = false;
    List<Swing> _swings = new List<Swing>();
    override protected void Awake()
    {
        base.Awake();
        _oldSprite = _buttonImage.sprite;
    }
    override public void OnUpgradeClicked()
    {
        if (!_canUpgrade) { return; }

        base.OnUpgradeClicked();
    }
    protected override void SetLevelValues(int level)
    {
        GameConfig config = References.Instance.GameConfig;

        _moneyToUpgrade = config.SwingUpgrades[_level].Cost;
        _text.UpdateText(_moneyToUpgrade);
        _manager.UpdateText();
        UpgradeSwing();

        if (!_hasLoaded)
            StartCoroutine(Load(level));

        base.SetLevelValues(level);
    }

    IEnumerator Load(int level)
    {
        yield return new WaitForSeconds(0.25f);
        while (level > 0)
        {
            UpgradeSwing();
            level--;
        }
        _hasLoaded = true;
    }

    public void ResetList() => _swings = new List<Swing>();
    public void AddNewSwing(Swing swing)
    {
        _swings.Add(swing);
    }
    public void CheckAvaliable()
    {
        bool canUpgrade = false;

        foreach (Swing sw in _swings)
        {
            if (!sw.Upgraded)
                canUpgrade = true;
        }
        if (canUpgrade)
        {
            _buttonImage.sprite = _oldSprite;
            _text.UpdateText(_moneyToUpgrade);
            _buttonImage.DOColor(new Color(183f / 255f, 0f, 1f, 1f), 0.51f);
        }
        else
            OnMaxLevel();

        _canUpgrade = canUpgrade;
    }

    void UpgradeSwing()
    {
        foreach (Swing sw in _swings)
        {
            if (!sw.Upgraded)
            {
                sw.Upgrade();
                _manager.UpgradedSeatCount++;
                break;
            }
        }

        CheckAvaliable();
    }
}
