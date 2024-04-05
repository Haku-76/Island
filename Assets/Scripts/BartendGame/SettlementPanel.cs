using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettlementPanel : MonoBehaviour
{
    private TextMeshProUGUI text;

    void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Settle(float alcohol)
    {
        text.text = alcohol.ToString();
    }
}
