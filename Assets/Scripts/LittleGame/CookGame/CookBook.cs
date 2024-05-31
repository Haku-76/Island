using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dishes_Crafting
{
    public Dishes_Data dish;
    public List<FoodMaterial> requiredFoods;

    public Dishes_Crafting(Dishes_Data result, List<FoodMaterial> requirment)
    {
        dish = result;
        requiredFoods = requirment;
    }
}

public class CookBook
{
    Dictionary<string, Dishes_Crafting> cookBook = new();

    public bool CheckBook(string targetFood, List<FoodMaterial> currentFoods)
    {
        return AreListsEqual(currentFoods, cookBook[targetFood].requiredFoods);
    }

    private static bool AreListsEqual(List<FoodMaterial> list1, List<FoodMaterial> list2)
    {
        if (list1.Count != list2.Count)
        {
            return false;
        }

        var dict1 = list1.GroupBy(x => x.item_name).ToDictionary(g => g.Key, g => g.Count());
        var dict2 = list2.GroupBy(x => x.item_name).ToDictionary(g => g.Key, g => g.Count());

        return dict1.Count == dict2.Count && !dict1.Except(dict2).Any();
    }
}
