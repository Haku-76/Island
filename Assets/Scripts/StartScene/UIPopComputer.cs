using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPopComputer : BasePanel
{
   
    public Button btnEmail;

    public override void Init()
    {
        btnEmail.onClick.AddListener(() =>
        {
            UIPanelManager.Instance.showPanel<UIPopEmail>(false);
        });
    }
}
