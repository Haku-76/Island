using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GOmovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public CameraController cameraController;
    private bool isMoving = true;
    public GameObject NPC;
    public float jumpDuration = 3f;
    public float InBarDuration = 5f;
    public Vector3 targetPosition2;
    public GameObject barGamePrefab;

    void Start()
    {
        StartCoroutine(MoveCoroutine());
    }

    IEnumerator MoveCoroutine()
    {
        yield return new WaitForSeconds(7f); 

        // 7秒后停止移动
        isMoving = false;

        float elapsedTime = 0f;

        Vector3 initialPosition = NPC.transform.position;
        Vector3 targetPosition1 = new Vector3(27.32f, 1.65f, 0f);
        
        while (elapsedTime < jumpDuration)
        {
            // 使用 Vector3.Lerp 逐渐从初始位置移动到目标位置
            NPC.transform.position = Vector3.Lerp(initialPosition, targetPosition1, elapsedTime / jumpDuration);
            elapsedTime += Time.deltaTime; // 更新经过的时间
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        elapsedTime = 0f;

        if (cameraController != null)
        {
            cameraController.ZoomIn();
        }
        else
        {
            Debug.LogError("CameraController not found!");
        }
        
        while (elapsedTime < InBarDuration)
        {
            NPC.transform.position = Vector3.Lerp(targetPosition1, targetPosition2, elapsedTime / InBarDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        NPC.AddComponent<OnClickEvent>();
    }

    void Update()
    {
        if (isMoving)
        {
            transform.Translate(Vector3.left * (moveSpeed * Time.deltaTime));
        }
    }

    public void WantAWine()
    {
        Vector3 screenCenter = new Vector3(0f,0f, 0f);
        Instantiate(barGamePrefab, screenCenter, Quaternion.identity);
    }
}

