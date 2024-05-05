using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class UIPopEmail : BasePanel
{
    public Button btnReply;
    public Button btnSend;

    public GameObject normalGroup;
    public GameObject replyGroup;

    private PlayableDirector timelineDirector;
    public override void Init()
    {
        timelineDirector = GameObject.Find("EndDirector").GetComponent<PlayableDirector>();
        btnReply.onClick.AddListener(() =>
        {
            btnSend.interactable = true;
            normalGroup.SetActive(false);
            replyGroup.SetActive(true);
            btnReply.interactable = false;
        });

        btnSend.onClick.AddListener(() =>
        {
            timelineDirector.Play();
        });
    }
}
