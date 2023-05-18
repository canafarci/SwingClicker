using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyUpgrade : Upgrade
{
    override public void OnUpgradeClicked()
    {
        int maxLevel = References.Instance.GameConfig.MoneyUpgrades.Length;
        if (maxLevel == _level + 1) { return; }

        base.OnUpgradeClicked();
    }
    protected override void SetLevelValues(int level)
    {
        GameConfig config = References.Instance.GameConfig;

        _moneyToUpgrade = config.MoneyUpgrades[_level].Cost;
        _manager.MoneyPerSwing = config.MoneyUpgrades[_level].MoneyPerSpawn;
        _text.UpdateText(_moneyToUpgrade);
        _manager.UpdateText();
        _manager.MoneyLevel = _level;

        base.SetLevelValues(level);

        int maxLevel = References.Instance.GameConfig.MoneyUpgrades.Length;
        if (maxLevel == _level + 1) { OnMaxLevel(); }
    }
}
