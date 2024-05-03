using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPopScenes : BasePanel
{
    public Button btnComputer;
    public override void Init()
    {
        btnComputer.onClick.AddListener(() =>
        {
            UIPanelManager.Instance.showPanel<UIPopComputer>(false);
        });
    }
}
