using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionArrow : MonoBehaviour
{
    public Vector3 targetPos;
    void OnMouseDown()
    {
        TransitionManager.Instance.MovePlayerTo(targetPos);
    }
}
