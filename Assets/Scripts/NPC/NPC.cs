using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private SpriteRenderer sprite;
    public DialogueRunner dialogueRunner;

    private Vector3 targetPosition;
    private Vector3 startPosition;

    private Stack<Vector3> targetPositions;
    private Coroutine npcMoveRoutine;
    private bool npcMove;
    public bool isMoving;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        targetPositions = new Stack<Vector3>();
        scheduleSet = new SortedSet<ScheduleDetails>();
        sprite.enabled = false;
        foreach(var schedule in scheduleData.scheduleList)
        {
            scheduleSet.Add(schedule);
        }
    }

    void Update()
    {

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
    public void Test()
    {
        OnTimeUpdateEvent(1, TimeQuantum.Dusk);
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
        sprite.enabled = true;
        Color startColor = Color.clear;
        Color targetColor = Color.white;
        float burnDuration = 1.0f;
        float elapsedTime = 0f;
        while(elapsedTime < burnDuration)
        {
            sprite.color = Color.Lerp(startColor, targetColor, elapsedTime/burnDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        sprite.color = targetColor;
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

        dir = targetPos.x - transform.position.x;

        while(elapsedTime < journeyTime)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / journeyTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        transform.position = targetPos;
        interactable = true;
        canStartDialogue = true;
        npcMove = false;
        OnMoveEndEvent();
    }

    public void BuildPath(ScheduleDetails schedule)
    {
        targetPositions.Clear();
        currentSchedule = schedule;

        targetPositions.Push(currentSchedule.targetPosition);
        startPosition = schedule.burnPosition;
        dialogueStartNode = schedule.dialogueStartNode;
    }

    private void OnMoveEndEvent()
    {
        // dialogueRunner.StartDialogue(dialogueStartNode);
    }

    public void StartDialogue()
    {
        if(interactable)
            dialogueRunner.StartDialogue(dialogueStartNode);
    }

    public void SwitchAnimation()
    {
        isMoving = transform.position != targetPosition;
        anim.SetBool("IsMoving", isMoving);

        if(isMoving)
        {
            anim.SetFloat("Dir", dir);
        }
        else
        {
            anim.SetBool("Exit", false);
        }
    }
}
