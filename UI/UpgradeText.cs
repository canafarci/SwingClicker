using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;

    public void UpdateText(int cost)
    {
        _text.text = "$" + cost.ToString();
    }

    public void SetMax()
    {
        _text.text = "MAX";
    }
}
