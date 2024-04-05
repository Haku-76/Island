using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WineGlass : MonoBehaviour
{
    [SerializeField]private float alcohol;

    void OnMouseDown()
    {
        Debug.Log("StartDrag");
    }

    void OnMouseDrag()
    {
        Debug.Log("Draging");
    }

    void OnMouseUp()
    {
        Debug.Log("EndDrag");
    }

    public void FillGlass(DrinksDataSO drinksData)
    {
        if(drinksData.drinksType == DrinksType.wine)
        {
            if(!Bar_GameManager.Instance.isAddWine)
            {
                alcohol += drinksData.alcohol;
                Bar_GameManager.Instance.progressUI.AddWine();
            }
            else
                Debug.Log("已经加过酒了");
        }
        else if(drinksData.drinksType == DrinksType.Water)
        {
            if(!Bar_GameManager.Instance.isAddWater)
            {
                Bar_GameManager.Instance.progressUI.AddWater();
                Debug.Log("添加了 " + drinksData.drinkName);
            }
            else
            {
                Debug.Log("已经加过饮料了");
            }
        }
    }

    public float GetAlcohol()
    {
        return alcohol;
    }
}
