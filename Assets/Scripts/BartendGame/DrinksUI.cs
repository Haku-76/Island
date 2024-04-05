using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DrinksUI : MonoBehaviour
{
    Animator animator;
    public Image image;

    void Awake()
    {
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
}
