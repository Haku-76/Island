using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Spine.Unity;
using UnityEngine;
using Yarn.Unity;

public class NPC : MonoBehaviour
{
    [Header("日程表")]
    public ScheduleData_SO scheduleData;
    private SortedSet<ScheduleDetails> scheduleSet;
    private ScheduleDetails currentSchedule;

    [Header("NPC 参数")]
    public float move_Speed;
    public bool interactable;
    private bool canStartDialogue;
    private string dialogueStartNode;

    private float dir;

    [Header("组件")]
    private Animator anim;
    [SerializeField]private GameObject sprite;
    [SerializeField]private Material mat;
    public DialogueRunner dialogueRunner;

    private Vector3 targetPosition;
    private Vector3 startPosition;

    private Stack<Vector3> targetPositions;
    private Coroutine npcMoveRoutine;
    private bool npcMove;
    public bool isMoving;
    private AnimationClip afterMoveClip;
    public AnimationClip blankAnimationClip;
    private AnimatorOverrideController animOverride;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        targetPositions = new Stack<Vector3>();
        scheduleSet = new SortedSet<ScheduleDetails>();
        animOverride = new AnimatorOverrideController(anim.runtimeAnimatorController);
        anim.runtimeAnimatorController = animOverride;
        
        sprite.SetActive(false);
        foreach(var schedule in scheduleData.scheduleList)
        {
            scheduleSet.Add(schedule);
        }
    }

    void Update()
    {
        SwitchAnimation();
        Flip();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void InitNPC()
    {
        targetPosition = transform.position;
    }

    [ContextMenu("测试NPC 行为1")]
    public void Test1()
    {
        OnTimeUpdateEvent(1, TimeQuantum.Dusk);
    }


    [ContextMenu("测试NPC 行为2")]
    public void Test2()
    {
        OnTimeUpdateEvent(2, TimeQuantum.Dusk);
    }

    private void OnTimeUpdateEvent(int day, TimeQuantum timeQuantum)
    {
        int time = day * 100 + (int)timeQuantum;

        ScheduleDetails matchSchedule = null;
        foreach(var schedule in scheduleSet)
        {
            if(schedule.Time == time)
            {
                matchSchedule = schedule;
            }
            else if(schedule.Time > time)
            {
                break;
            }
        }

        if(matchSchedule != null)
        {
            BuildPath(matchSchedule);
        }
    }

    private void Movement()
    {
        if(!npcMove)
        {
            if(targetPositions.Count > 0)
            {
                targetPosition = targetPositions.Pop();
                
                MoveToTarget(targetPosition, move_Speed);
            }
            
        }
    }

    private void MoveToTarget(Vector3 target, float speed)
    {
        StartCoroutine(BurnNPC(() => {StartCoroutine(MoveRoutine(target, speed));}));
    }

    private IEnumerator BurnNPC(System.Action onBurnComplete)
    {
        transform.position = startPosition;
        sprite.SetActive(true);
        float startAlpha = 0;
        float targetAlpha = 1;
        float burnDuration = 1.0f;
        float elapsedTime = 0f;
        while(elapsedTime < burnDuration)
        {
            var alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime/burnDuration);
            mat.SetFloat("_Alpha", alpha);
            Debug.Log(mat == null);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        mat.SetFloat("_Alpha", targetAlpha);
        onBurnComplete?.Invoke();
    }

    private IEnumerator MoveRoutine(Vector3 targetPos, float speed)
    {
        npcMove = true;
        interactable = false;
        Vector3 startPos = transform.position;
        float distance = Vector3.Distance(startPos, targetPos);
        float journeyTime = distance / speed;
        float elapsedTime = 0f;
        isMoving = true;
        dir = targetPos.x - transform.position.x;

        while(elapsedTime < journeyTime)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / journeyTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        isMoving = false;
        transform.position = targetPos;
        interactable = true;
        canStartDialogue = true;
        npcMove = false;
        StartCoroutine(OnMoveEndEvent());
    }

    public void BuildPath(ScheduleDetails schedule)
    {
        targetPositions.Clear();
        currentSchedule = schedule;
        afterMoveClip = schedule.clipAfterArive;
        targetPositions.Push(currentSchedule.targetPosition);
        startPosition = schedule.burnPosition;
        dialogueStartNode = schedule.dialogueStartNode;
    }

    private IEnumerator OnMoveEndEvent()
    {
        if(afterMoveClip != null)
        {
            Debug.Log($" == OnMoveEndEvent == afterMoveClip != null");
            animOverride[blankAnimationClip] = afterMoveClip;
            anim.SetBool("EventAnimation", true);
            yield  return null;
            // anim.SetBool("EventAnimation", false);
        }
        else
        {
            Debug.Log($" == OnMoveEndEvent == afterMoveClip == null");
            animOverride[afterMoveClip] = blankAnimationClip;
            anim.SetBool("EventAnimation", false);
        }
    }

    public void StartDialogue()
    {
        if(interactable)
            dialogueRunner.StartDialogue(dialogueStartNode);
    }

    public void SwitchAnimation()
    {
        anim.SetBool("isMoving", isMoving);

        // if(isMoving)
        // {
        //     anim.SetFloat("Dir", dir);
        // }
        // else
        // {
        //     anim.SetBool("Exit", false);
        // }
    }

    public void Flip()
    {
        if (targetPosition.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        else if (targetPosition.x < transform.position.x)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
    }
}
