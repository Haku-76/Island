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
            if(alcohol == 0)
            {
                alcohol += drinksData.alcohol;
                
            }
            else
                Debug.Log("已经加过酒了");
        }
        else if(drinksData.drinksType == DrinksType.Water)
        {
            Debug.Log("添加了 " + drinksData.drinkName);
        }
    }
}
