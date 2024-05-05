using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Spine.Unity;
using Unity.VisualScripting;
using UnityEngine;
using Yarn.Unity;

public class NPC : MonoBehaviour
{
    [Header("日程表")]
    public ScheduleData_SO scheduleData;
    private SortedSet<ScheduleDetails> scheduleSet;
    private ScheduleDetails currentSchedule;

    [Header("NPC 参数")]
    public NPCCourse npc_Course;
    public float move_Speed;
    public bool interactable;
    private bool canStartDialogue;
    private string dialogueStartNode;

    private float dir;

    [Header("组件")]
    protected Animator anim;
    [SerializeField]private GameObject sprite;
    [SerializeField]private Material mat;
    public DialogueRunner dialogueRunner;
    public GameActions gameActions;

    [Header("动画")]
    public AnimationClip blankAnimationClip;

    [Header("酒精相关")]
    public float adapted_Alcohol;
    public WaterTag adapted_Taste;
    
    /// <summary>
    /// 0 => 完全合适= =
    /// 1 => 酒精偏多= =
    /// 2 => 酒精偏少= =
    /// 3 => 口感不对= =
    /// 4 => 两种要求都不满足
    /// </summary>
    public string[] dialogueStartNodes_AfterDrinking = new string[5];

    private Vector3 targetPosition;
    private Vector3 startPosition;

    private Stack<Vector3> targetPositions;
    private Coroutine npcMoveRoutine;
    private bool npc_isActive;
    protected bool isMoving;
    protected bool spAnimation;
    private bool isInteracted;
    private bool isOver = false;
    private AnimationClip afterMoveClip;
    private AnimatorOverrideController animOverride;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        targetPositions = new Stack<Vector3>();
        scheduleSet = new SortedSet<ScheduleDetails>();
        animOverride = new AnimatorOverrideController(anim.runtimeAnimatorController);
        anim.runtimeAnimatorController = animOverride;
        
        // sprite.SetActive(false);
        SetNPCActive(false);
        foreach(var schedule in scheduleData.scheduleList)
        {
            scheduleSet.Add(schedule);
        }
    }

    void OnEnable()
    {
        TimeEventSystem.onTimeChange += OnTimeUpdateEvent;
        TimeEventSystem.onLastTimeEnd += OnLastTimeEnd;
    }

    void OnDisable()
    {
        TimeEventSystem.onTimeChange -= OnTimeUpdateEvent;
        TimeEventSystem.onLastTimeEnd -= OnLastTimeEnd;
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
        isMoving = false;
        spAnimation = false;
        isInteracted = false;
    }

    [ContextMenu("测试NPC 行为1")]
    public void Test1()
    {
        //OnTimeUpdateEvent(7, 0, TimeQuantum.Dusk);
    }


    [ContextMenu("测试NPC 行为2")]
    public void Test2()
    {
       // OnTimeUpdateEvent(7, 1, TimeQuantum.Dusk);
    }

    private void OnTimeUpdateEvent(int month, int day, TimeQuantum timeQuantum)
    {
        if(NPCManager.Instance.GetNPCCourse(npc_Course))
        {
            int time = month * 10000 + day * 100 + (int)timeQuantum;

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
    }

    private void OnLastTimeEnd(int month, int day, TimeQuantum timeQuantum)
    {
        if(npc_isActive)
        {
            StartCoroutine(NPCExit());
        }
    }

    public void OnFinishGameEvent(MixedWine_Data wine)
    {
        Debug.Log($"NPC_OnFinishGameEvent{this.gameObject.name}");
        float curAlcohol = wine.alcohol;
        WaterTag curTaste = wine.taste;

        bool isAlcoholRight = Math.Abs(curAlcohol - adapted_Alcohol) < 0.1f;
        bool isTasteRight = curTaste == adapted_Taste;

        if(isAlcoholRight && isTasteRight)
        {
            dialogueRunner.StartDialogue(dialogueStartNodes_AfterDrinking[0]);
        }
        else if(!isAlcoholRight && !isTasteRight)
        {
            dialogueRunner.StartDialogue(dialogueStartNodes_AfterDrinking[4]);
        }
        else if(!isAlcoholRight)
        {
            //酒精过多
            if((curAlcohol - adapted_Alcohol) > 0f)
            {
                dialogueRunner.StartDialogue(dialogueStartNodes_AfterDrinking[1]);
            }
            else//酒精过少
            {
                dialogueRunner.StartDialogue(dialogueStartNodes_AfterDrinking[2]);
            }
        }
        else if(!isTasteRight)
        {
            dialogueRunner.StartDialogue(dialogueStartNodes_AfterDrinking[3]);
        }
    }


#region NPCActions
    public void exitBar()
    {
        spAnimation = false;
        StartCoroutine(MoveRoutine(startPosition, move_Speed, () => {StartCoroutine(NPCExit());}));
    } 

#endregion

    private void SetNPCActive(bool value)
    {
        sprite.SetActive(value);
        GetComponent<Collider2D>().enabled = value;
    }

    private void Movement()
    {
        if(!isMoving)
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
        StartCoroutine(BurnNPC(() => {StartCoroutine(MoveRoutine(target, speed, () => {StartCoroutine(OnMoveEndEvent());}));}));
    }

    private IEnumerator BurnNPC(System.Action onComplete)
    {
        npc_isActive = true;
        InitNPC();
        transform.position = startPosition;
        // sprite.SetActive(true);
        SetNPCActive(true);
        float startAlpha = 0;
        float targetAlpha = 1;
        float burnDuration = 1.0f;
        float elapsedTime = 0f;
        while(elapsedTime < burnDuration)
        {
            var alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime/burnDuration);
            mat.SetFloat("_Alpha", alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        mat.SetFloat("_Alpha", targetAlpha);
        onComplete?.Invoke();
    }
    private IEnumerator NPCExit()
    {
        float startAlpha = 1;
        float targetAlpha = 0;
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
        // sprite.SetActive(false);
        SetNPCActive(false);
        npc_isActive = false;
    }

    private IEnumerator MoveRoutine(Vector3 targetPos, float speed, Action onComplete)
    {
        targetPosition = targetPos;
        interactable = false;
        Vector3 startPos = transform.position;
        Debug.Log($"StartPos{startPos}; targetPos{targetPos}");
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
        onComplete?.Invoke();
        // StartCoroutine(OnMoveEndEvent());
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
            animOverride[blankAnimationClip] = afterMoveClip;
            spAnimation = true;
            yield  return null;
            // anim.SetBool("EventAnimation", false);
        }
        else
        {
            animOverride[afterMoveClip] = blankAnimationClip;
            spAnimation = false;
        }
    }

    public void StartDialogue(PlayerController player)
    {
        if(interactable && !isInteracted)
        {
            isInteracted = true;
            player.LockPlayer();
            dialogueRunner.StartDialogue(dialogueStartNode);
            gameActions.currentNPC = this;
        }
    }

    public void SwitchAnimation()
    {
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("EventAnimation", spAnimation);

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

    public void NPCOver()
    {
        isOver = true;
    }
}
