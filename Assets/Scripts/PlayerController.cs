using UnityEngine;
using Yarn.Unity;

public class PlayerController : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    public bool canMoveToNonNPC;

    [Space(15)]
    public float moveSpeed;
    public float interactionDistance;

    [Space(15)]
    public Vector3 targetPosition;
    public NPC targetNPC;
    public bool isMovingToNPC;
    public bool isMovingToNull;

    private bool isTalking;
    
    void Update()
    {
        if(isTalking)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            HandleMouseClick();
            FilpCharacter();
        }

        if (isMovingToNPC)
        {
            MoveTowardsTarget();
        }

        if (isMovingToNull)
        {
            MoveTowardsTarget();
        }
    }

    private void HandleMouseClick()
    {
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
            isMovingToNPC = false ;
        }
    }

    private void MoveTowardsTarget()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(this.transform.position, targetPosition) < interactionDistance)
        {
            isMovingToNPC = false;
            if (targetNPC != null)
            {
                InteractWithNPC(targetNPC);
            }
        }

        else if (this.transform.position == targetPosition)
        {
            isMovingToNull = false;
        }
    }

    private void FilpCharacter()
    {
        if (isMovingToNPC == true || isMovingToNull == true)
        {
            if (targetPosition.x > transform.position.x)
            {
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            }
            else if (targetPosition.x < transform.position.x)
            {
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            }
        }
    }

    private void InteractWithNPC(NPC npc)
    {
        isTalking = true;
        npc.StartDialogue();
    }
}
