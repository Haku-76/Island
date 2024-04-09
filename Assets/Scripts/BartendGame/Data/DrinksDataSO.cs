using UnityEngine;

[CreateAssetMenu(fileName = "DrinksData", menuName = "Drinks/DrinksData")]
public class DrinksDataSO : ScriptableObject
{
    public Sprite icon;
    public string drinkName;
    [TextArea]
    public string description;
    public DrinksType drinksType;
    public float alcohol;
    public WaterTag taste;
}

public class MixedWine_Data
{
    public float alcohol;
    public WaterTag taste;
}

[System.Serializable]
public enum DrinksType
{
    wine,
    water,
}

[System.Serializable]
public enum WaterTag
{
    probiotics,
    soda,
    sprite,
    lemonade
}