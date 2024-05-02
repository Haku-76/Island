using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    private bool isPlayerIn;
    private List<TransitionArrow> arrows = new();

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var arrow = transform.GetChild(i).GetComponent<TransitionArrow>();
            arrows.Add(arrow);
            arrow.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isPlayerIn = true;
            foreach(var arrow in arrows)
            {
                arrow.gameObject.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isPlayerIn = false;
            foreach(var arrow in arrows)
            {
                arrow.gameObject.SetActive(false);
            }
        }
    }
}
