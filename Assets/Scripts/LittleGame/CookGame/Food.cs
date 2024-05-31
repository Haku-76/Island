using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : DragItem
{
    // public FoodMaterial food;
    public override void Excute(DragTarget target)
    {
        StartCoroutine(AddFood(target as Pot));
    }

    IEnumerator AddFood(Pot target)
    {
        
        GameRoot.Instance.drinks_DragUI.PourOut();
        yield return new WaitForSeconds(2f);

        target.AddFood(itemData as FoodMaterial);
        GameRoot.Instance.drinks_DragUI.image.enabled = false;
    }
}
