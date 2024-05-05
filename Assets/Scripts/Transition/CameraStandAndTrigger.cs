using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraStandAndTrigger : MonoBehaviour
{
    public CinemachineVirtualCamera cameraStand;
    public bool isFollowPlayer;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Enter{this.name}Area");
        if(other.gameObject.CompareTag("Player"))
        {
            TransitionManager.Instance.SetCamera(cameraStand);
        } 
    }
}
