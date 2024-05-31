using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : DragTarget
{
    [SerializeField]List<FoodMaterial> currentFoodInPot = new();

    public void AddFood(FoodMaterial food)
    {
        currentFoodInPot.Add(food);
    }

    public bool IsContainFood(FoodMaterial foodMat)
    {
        foreach(var food in currentFoodInPot)
        {
            if(food.item_name == foodMat.item_name)
            {
                return true;
            }
        }

        return false;
    }

}
