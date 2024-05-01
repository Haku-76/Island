using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class SettlementPanel : MonoBehaviour
{
    private GameObject entry;
    private TextMeshProUGUI text;

    void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        entry = transform.GetChild(0).gameObject;
    }

    void OnEnable()
    {
        GameRoot.FinishGameEvent += Settle;
    }

    void OnDisable()
    {
        GameRoot.FinishGameEvent -= Settle;
    }

    void Start()
    {
        entry.SetActive(false);
    }

    public void Settle(MixedWine_Data data)
    {
        entry.SetActive(true);
        text.text = data.alcohol.ToString();

    }
}
