using UnityEngine;
using System.Collections.Generic;
using Yarn.Unity;
using UnityEngine.EventSystems;
using Spine.Unity;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public bool canMoveToNonNPC;

    [Space(15)]
    public float moveSpeed;
    public float interactionDistance;

    [Space(15)]
    public Vector3 targetPosition;
    public NPC targetNPC;
    public bool isMovingToNPC;
    public bool isMovingToNull;

    private bool isLocking;

    [Space(15)]
    public GameObject openBag;

    void OnEnable()
    {
        TimeEventSystem.onLastTimeEnd += OnLastTimeEndEvent;
        TimeEventSystem.onTimeChange += OnTimeChangeEvent;
    }

    void OnDisable()
    {
        TimeEventSystem.onLastTimeEnd -= OnLastTimeEndEvent;
        TimeEventSystem.onTimeChange -= OnTimeChangeEvent;
    }

    void Update()
    {
        if(isLocking)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            HandleMouseClick();
            FilpCharacter();
        }

        if (isMovingToNPC || isMovingToNull)
        {
            MoveTowardsTarget();
        }
        else
        {
            this.GetComponent<SkeletonAnimation>().AnimationName = null;
        }
    }

    private Vector3 pre_Pos;
    private void OnLastTimeEndEvent(int month, int day, TimeQuantum timeQuantum)
    {
        // pre_Pos = transform.position;
        // transform.position = Position_Data.Bed_Pos;
        // transform.GetComponent<SkeletonAnimation>().AnimationName = "down";
    }

    private void OnTimeChangeEvent(int month, int day ,TimeQuantum timeQuantum)
    {
        // StartCoroutine(GetUpCoroutine());
    }

    IEnumerator GetUpCoroutine()
    {
        transform.GetComponent<SkeletonAnimation>().AnimationName = "getup";
        yield return new WaitForSeconds(1f);
        transform.position = pre_Pos;
        pre_Pos = Vector3.zero;
    }

    private void HandleMouseClick()
    {
        if (IsPointerOverUIObject())
        {
            return;
        }

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag("NPC"))
        {
            targetPosition = new Vector3(hit.collider.gameObject.transform.position.x, this.transform.position.y, this.transform.position.z);

            if (Vector3.Distance(this.transform.position, targetPosition) > interactionDistance)
            {
                targetNPC = hit.collider.gameObject.GetComponent<NPC>();
                isMovingToNPC = true;
                isMovingToNull = false;
            }
        }

        else if (canMoveToNonNPC == true)
        {
            targetPosition = new Vector3(mousePos.x, this.transform.position.y, this.transform.position.z);
            targetNPC = null;
            isMovingToNull = true;
            isMovingToNPC = false;
        }
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        foreach (RaycastResult result in results)
        {
            if (result.gameObject.GetComponent<PreventClickThrough>() != null)
            {
                return true;
            }
        }
        return false;
    }

    private void MoveTowardsTarget()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);
        this.GetComponent<SkeletonAnimation>().AnimationName = "walk";

        if (Vector3.Distance(this.transform.position, targetPosition) < interactionDistance)
        {
            isMovingToNPC = false;
            isMovingToNull = false;
            if (targetNPC != null)
            {
                InteractWithNPC(targetNPC);
            }
        }
    }

    private void FilpCharacter()
    {
        if (isMovingToNPC == true || isMovingToNull == true)
        {
            if (targetPosition.x > transform.position.x)
            {
                transform.localScale = new Vector3(-0.1f, transform.localScale.y, transform.localScale.z);
            }
            else if (targetPosition.x < transform.position.x)
            {
                transform.localScale = new Vector3(0.1f, transform.localScale.y, transform.localScale.z);
            }
        }
    }

    private void InteractWithNPC(NPC npc)
    {
        npc.StartDialogue(this);
    }

    public void LockPlayer()
    {
        isLocking = true;
        this.GetComponent<SkeletonAnimation>().AnimationName = null;
    }
    public void UnLockPlayer()
    {
        isLocking = false;
    }
}
