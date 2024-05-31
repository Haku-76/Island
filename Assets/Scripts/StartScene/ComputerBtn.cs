using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerBtn : MonoBehaviour
{
    void OnMouseDown()
    {
        UIPanelManager.Instance.showPanel<UIPopComputer>(false);

        gameObject.SetActive(false);
    }
}
