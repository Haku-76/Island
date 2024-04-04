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
}


public enum DrinksType
{
    wine,
    Water,
}