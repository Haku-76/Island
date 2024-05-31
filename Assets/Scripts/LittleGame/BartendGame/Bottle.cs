using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bottle : DragItem
{
    public DrinksDataSO drinksData;

    [HideInInspector]public bool addWineOver = false;

    public override void Excute(DragTarget target)
    {
        StartCoroutine(FillGlass(target as WineGlass));
    }

    IEnumerator FillGlass(WineGlass target)
    {
        GameRoot.Instance.drinks_DragUI.SetUIPos(target.bottlePos.position);
        GameRoot.Instance.drinks_DragUI.PourOut();
        yield return new WaitForSeconds(0.5f);
        Bar_GameManager.Instance.StartQTE(2, this);
        yield return new WaitUntil(() => addWineOver);
        target.FillGlass(drinksData);
        GameRoot.Instance.drinks_DragUI.image.enabled = false;
        GameRoot.Instance.drinks_DragUI.EndPourOut();
    }
}
