using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeChange : MonoBehaviour
{
    public string targetTag;
    private bool canChange=false;
    private void MouseRaycast(int Day, int Month, TimeQuantum timeQuantum)
    {
            Vector2 rayOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero);

            if (hit)
            {
                if (hit.collider.gameObject.CompareTag(targetTag))
                {
                    Debug.Log("Hit object with tag: " + targetTag);
                    canChange = true;
                }
            else
            {
                canChange = false;
            }
            }
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            MouseRaycast(TimeEventSystem.instance.Day,TimeEventSystem.instance.Month,TimeEventSystem.instance.timeQuantum);
            if (canChange)
            {
                TimeEventSystem.instance.Skip();
            }
        }
    }
}
