using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DrinksUI : MonoBehaviour
{
    Animator animator;
    Canvas canvas;
    public Image image;

    void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        animator = GetComponent<Animator>();
        image = GetComponent<Image>();
    }

    public void PourOut()
    {
        animator.SetBool("PourOut", true);
    }

    public void EndPourOut()
    {
        animator.SetBool("PourOut", false);
    }

    public void SetUIPos(Vector3 pos)
    {
        Vector2 uiPos = WorldToCanvasPosition(Camera.main, canvas, pos);
        GetComponent<RectTransform>().anchoredPosition = uiPos;
    }

    private Vector2 WorldToCanvasPosition(Camera cam, Canvas canvas, Vector3 worldPosition)
    {
        Vector2 screenPosition = cam.WorldToScreenPoint(worldPosition);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, screenPosition, cam, out Vector2 localPoint);
        return localPoint;
    }
}
