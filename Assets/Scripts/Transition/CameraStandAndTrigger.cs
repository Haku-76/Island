using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraStandAndTrigger : MonoBehaviour
{
    public CinemachineVirtualCamera cameraStand;
    public bool isFollowPlayer;
    public bool isUpstairsTriggered;
    public bool isOutsideTriggered1;
    public bool isOutsideTriggered2;
    public bool isSleepTriggered;
    

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Enter{this.name}Area");
        if(other.gameObject.CompareTag("Player"))
        {
            TransitionManager.Instance.SetCameraWithTransition(cameraStand, isFollowPlayer);
        }

        if (this.gameObject.name == "Trigger_Upstairs" && isUpstairsTriggered == false)
        {
            isUpstairsTriggered = true;
            TransitionManager.Instance.SetCameraWithTransition(cameraStand,isFollowPlayer);
            DialogueManager.Instance.StartDialogue("UpStairsGuide");
            Destroy(this.gameObject);
            TransitionManager.Instance.ReturnPreCamera();
        }
        if (this.gameObject.name == "Trigger_Outside_Details_1" && isOutsideTriggered1 == false)
        {
            isOutsideTriggered1 = true;
            DialogueManager.Instance.StartDialogue("GoOutGuide");
        }

        if (this.gameObject.name == "Trigger_Bed" && isSleepTriggered == false)
        {
            isSleepTriggered = true;
            TransitionManager.Instance.SetCameraWithTransition(cameraStand,isFollowPlayer);
            DialogueManager.Instance.StartDialogue("SleepGuide");
            Destroy(this.gameObject);
            TransitionManager.Instance.ReturnPreCamera();
        }

        if (this.gameObject.name == "Trigger_Outside_Details_2" && isOutsideTriggered2 == false)
        {
            isOutsideTriggered2 = true;
            TransitionManager.Instance.SetCameraWithTransition(cameraStand,isFollowPlayer);
            DialogueManager.Instance.StartDialogue("MailboxGuide");
            TransitionManager.Instance.ReturnPreCamera();
        }
    }
}
