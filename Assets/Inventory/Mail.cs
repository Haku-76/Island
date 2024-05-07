using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mail : MonoBehaviour
{
    public Item letter;

    void OnMouseDown()
    {
        InventoryManager.AddItemIntoBag(letter, this.gameObject);

    }
}
