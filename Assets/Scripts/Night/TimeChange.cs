using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeChange : MonoBehaviour
{
    public string targetTag;
    int a = 1;
    int b = 1;
    TimeQuantum timeQuantum;
    private void Start()
    {
        TimeEventSystem.onTimeChange += MouseRaycast;
    }

    private void OnDisable()
    {
        TimeEventSystem.onTimeChange -= MouseRaycast;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TimeEventSystem.instance.Skip();
        }
    }

    private void MouseRaycast(int Day, int Month, TimeQuantum timeQuantum)
    {
        Vector2 rayOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero);

        if (hit)
            {
                if (hit.collider.gameObject.CompareTag(targetTag))
                {
                    Debug.Log("Hit object with tag: " + targetTag);
                }
            }
    }
}
