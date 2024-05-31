using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragItem : MonoBehaviour
{
    public DragItem_Data itemData;
    public void OnMouseDown()
    {
        var image = GameRoot.Instance.drinks_DragUI.image;
        image.enabled = true;
        image.sprite = itemData.icon;
        image.SetNativeSize();
    }

    public void OnMouseDrag()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameRoot.Instance.drinks_DragUI.transform.position = new Vector3(mousePosition.x, mousePosition.y, 5);
    }

    public void OnMouseUp()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null && hit.collider.GetComponent<DragTarget>() != null)
        {
            var target = hit.collider.GetComponent<DragTarget>();
            Excute(target);
            return;
        }

        GameRoot.Instance.drinks_DragUI.image.enabled = false;
    }

    public virtual void Excute(DragTarget target)
    {
        
    }
}
