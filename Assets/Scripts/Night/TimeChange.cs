using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeChange : MonoBehaviour
{
    public string targetTag;
    private bool canChange=false;
    private bool isPlayer = false;
    private bool isBed = false;
    private bool isbell = false;
    private void MouseRaycast()
    {
        Vector2 rayOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero);

        if (hit&&Input.GetMouseButtonDown(0)&&!DialogueManager.Instance.isDialoguing)
        {
            if (hit.collider.gameObject.CompareTag(targetTag))
            {
                Debug.Log(111);
                canChange = true;
            }
        }
        else
        {
            canChange = false;
        }
    }

    public virtual void Update()
    {
        MouseRaycast();
        if (canChange&&isPlayer)
        {
            TimeEventSystem.instance.Skip();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayer = false;
        }
    }
}
