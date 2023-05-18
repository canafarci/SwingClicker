using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CountUpgrade : Upgrade
{
    Sprite _oldSprite;
    override protected void Awake()
    {
        base.Awake();
        _oldSprite = _buttonImage.sprite;
    }
    override public void OnUpgradeClicked()
    {
        int maxLevel = References.Instance.GameConfig.SwingCountUpgrades.Length;
        if ((!_manager.UpgradedJoint && 12 == _level + 1) ||
             (_manager.UpgradedJoint && maxLevel == _level + 1)) { return; }

        base.OnUpgradeClicked();
    }

    protected override void SetLevelValues(int level)
    {
        GameConfig config = References.Instance.GameConfig;

        _moneyToUpgrade = config.SwingCountUpgrades[_level].Cost;
        _text.UpdateText(_moneyToUpgrade);
        _manager.UnlockSwing(_level);

        base.SetLevelValues(level);

        int maxLevel = References.Instance.GameConfig.SwingCountUpgrades.Length;

        if ((!_manager.UpgradedJoint && 12 == _level + 1) ||
             (_manager.UpgradedJoint && maxLevel == _level + 1)) { OnMaxLevel(); }
    }

    public void OnJointUpgraded()
    {
        _buttonImage.sprite = _oldSprite;
        _text.UpdateText(_moneyToUpgrade);
        _buttonImage.DOColor(new Color(183f / 255f, 0f, 1f, 1f), 0.51f);
        _text.UpdateText(_moneyToUpgrade);
    }
}
