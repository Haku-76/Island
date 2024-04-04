using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bottle : MonoBehaviour
{
    public DrinksDataSO drinksData;
    
    public void OnMouseDown()
    {
        var image = Bar_GameManager.Instance.drinks_DragUI;
        image.enabled = true;
        // image.sprite = drinksData.icon;
    }

    public void OnMouseDrag()
    {
        Bar_GameManager.Instance.drinks_DragUI.transform.position = Input.mousePosition;
    }

    public void OnMouseUp()
    {
        Bar_GameManager.Instance.drinks_DragUI.enabled = false;

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null && hit.collider.GetComponent<WineGlass>() != null)
        {
            var target = hit.collider.GetComponent<WineGlass>();
            target.FillGlass(drinksData);
        }
    }

    
}
