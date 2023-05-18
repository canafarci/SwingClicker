using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointUpgrade : Upgrade
{
    [SerializeField] GameObject _joint;
    [SerializeField] string _saveIdentifier;
    override public void OnUpgradeClicked()
    {
        if (1 == _level) { return; }

        base.OnUpgradeClicked();
    }

    protected override void SetLevelValues(int level)
    {

        _moneyToUpgrade = 1000;
        _text.UpdateText(_moneyToUpgrade);
        _manager.UpdateText();
        if (level == 1 && !PlayerPrefs.HasKey(_saveIdentifier))
        {
            _manager.OnNewJoint();
            _joint.SetActive(true);
            PlayerPrefs.SetInt(_saveIdentifier, 1);
        }
        else if (level == 1 && PlayerPrefs.HasKey(_saveIdentifier))
        {
            FindObjectOfType<EnableNewJoint>(true).EnableObject();
            _manager.UpgradedJoint = true;
        }

        base.SetLevelValues(level);

        if (1 == _level) { OnMaxLevel(); }
    }
}
