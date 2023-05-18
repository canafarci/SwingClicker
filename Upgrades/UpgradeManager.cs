using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] List<GameObject> _swings;
    public float MoneyPerSwing;
    public int UpgradedSeatCount = 0;
    public int SwingCount { get { return _swingCount; } }
    public bool UpgradedJoint { get { return _jointUpgraded; } set { _jointUpgraded = value; } }
    bool _jointUpgraded = false;
    int _swingCount;
    PassiveIncomeText _incomeText;
    SwingUpgrade _swingUpgrader;
    public int MoneyLevel = 0;
    private void Awake()
    {
        _incomeText = FindObjectOfType<PassiveIncomeText>();
        _swingUpgrader = FindObjectOfType<SwingUpgrade>();
    }

    public void UnlockSwing(int level)
    {
        _swingUpgrader.ResetList();

        for (int i = 0; i < level + 1; i++)
        {
            GameObject swing = _swings[i];
            swing.SetActive(true);
            _swingUpgrader.AddNewSwing(swing.GetComponent<Swing>());
            swing.GetComponent<Swing>().Unlock();
        }
        _swingCount = level + 1;
        _swingUpgrader.CheckAvaliable();
    }

    public void UpdateText()
    {
        _incomeText.UpdateText(MoneyPerSwing, UpgradedSeatCount, _swingCount);
    }

    public void OnNewJoint()
    {
        _jointUpgraded = true;
        FindObjectOfType<CountUpgrade>().OnJointUpgraded();
    }
}
