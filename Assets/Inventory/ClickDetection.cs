using UnityEngine;

public class ClickDetection : MonoBehaviour
{
    public GameObject ExclamationMark;
    public Inventory playerInventory;
    public Item letter;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnButtonClick();
        }
    }

    public void AddMail(string letter_name)
    {
        ExclamationMark.SetActive(true);
        letter = Resources.Load<Item>(letter_name);
    }

    public void OnButtonClick()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject == this.gameObject)
            {
                Debug.Log("111");
                if(letter != null)
                {
                    AddNewItem();
                }
            }
        }
    }

    public void AddNewItem()
    {
        if (!playerInventory.itemList.Contains(letter))
        {
            playerInventory.itemList.Add(letter);
            letter.itemHeld += 1;
        }
        else
        {
            letter.itemHeld += 1;
        }

        InventoryManager.RefreshItem();
    }
}
