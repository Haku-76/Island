using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAddToBag : MonoBehaviour
{
    public Item thisItem;
    public Inventory playerInventory;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Lord"))
        {
            AddNewItem();
            Destroy(this.gameObject);
        }
    }

    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

        if (hit.collider != null && hit.collider.gameObject == this.gameObject)
        {
            Debug.Log("Item clicked!");
            AddNewItem();
            Destroy(this.gameObject);
        }
    }

    public void AddNewItem()
    {
        if (!playerInventory.itemList.Contains(thisItem))
        {
            playerInventory.itemList.Add(thisItem);
            thisItem.itemHeld += 1;
        }
        else
        {
            thisItem.itemHeld += 1;
        }

        InventoryManager.RefreshItem();
    }
}
