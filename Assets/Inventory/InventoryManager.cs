using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    static InventoryManager instance;

    public Inventory myBag;
    public GameObject slotGrid;
    public Slot slotPrefab;
    public TextMeshProUGUI itemInformation;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
        }

        instance = this;
    }


    private void OnEnable()
    {
        RefreshItem();
        instance.itemInformation.text = "";
    }

    public static void UpdateItemInfo(string itemDescription)
    {
        instance.itemInformation.text = itemDescription;
    }

    public static void AddLetterToBag(string letter_name)
    {
        var letter = Resources.Load<Item>(letter_name);
        Debug.Log(letter.name);
        if(!instance.myBag.itemList.Contains(letter))
            instance.myBag.itemList.Add(letter);
        RefreshItem();
    }
    public static void AddItemIntoBag(Item item, GameObject obj)
    {
        if(!instance.myBag.itemList.Contains(item))
            instance.myBag.itemList.Add(item);
        RefreshItem();

        Destroy(obj);
    }

    public static void CreateNewItem(Item item)
    {
        Slot newItem = Instantiate(instance.slotPrefab, instance.slotGrid.transform.position, Quaternion.identity);
        newItem.gameObject.transform.SetParent(instance.slotGrid.transform);

        newItem.slotItem = item;
        newItem.slotImage.sprite = item.itemImage;
        newItem.slotName.text = item.itemName;
        newItem.slotAmount.text = item.itemHeld.ToString();
    }

    public static void RefreshItem()
    {
        for(int i = 0; i < instance.slotGrid.transform.childCount; i++)
        {
            if(instance.slotGrid.transform.childCount == 0)
            {
                break;
            }
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < instance.myBag.itemList.Count; i++)
        {
            CreateNewItem(instance.myBag.itemList[i]);
        }
    }

    public void TestButton(string mailname)
    {
        var mailBox = FindAnyObjectByType<ClickDetection>();
        mailBox.AddMail(mailname);
    }
}
