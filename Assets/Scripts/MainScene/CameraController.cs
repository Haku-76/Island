using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float zoomDuration = 3f; // 拉远镜头的持续时间
    public Vector3 targetPosition; // 目标位置，即拉远后的摄像机位置
    public float targetSize;
    private Vector3 originalPosition = new Vector3(0f, 0f, -10f);
    private float originalSize = 5f;

    void Start()
    {
        // 延迟2秒后执行 ZoomOut 函数
        Invoke("ZoomOut", 2f);
    }

    public void ZoomOut()
    {
        StartCoroutine(ZoomOutCoroutine());
    }

    public void ZoomIn()
    {
        StartCoroutine(ZoomInCoroutine());
    }

    IEnumerator ZoomOutCoroutine()
    {
        Vector3 initialPosition = Camera.main.transform.position; // 获取初始摄像机位置
        float initialSize = Camera.main.orthographicSize; // 获取初始摄像机大小
        float elapsedTime = 0f;

        while (elapsedTime < zoomDuration)
        {
            // 使用 Vector3.Lerp 逐渐从初始位置移动到目标位置
            Camera.main.transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / zoomDuration);

            // 使用 Mathf.Lerp 逐渐从初始大小移动到目标大小
            Camera.main.orthographicSize = Mathf.Lerp(initialSize, targetSize, elapsedTime / zoomDuration);

            elapsedTime += Time.deltaTime; // 更新经过的时间
            yield return null;
        }

        // 确保最终位置是目标位置
        Camera.main.transform.position = targetPosition;

        // 确保最终大小是目标大小
        Camera.main.orthographicSize = targetSize;
    }
    
    IEnumerator ZoomInCoroutine()
    {
        Vector3 initialPosition = Camera.main.transform.position; // 获取当前摄像机位置
        float initialSize = Camera.main.orthographicSize; // 获取当前摄像机大小
        float elapsedTime = 0f;

        while (elapsedTime < zoomDuration)
        {
            // 使用 Vector3.Lerp 逐渐从当前位置移动到初始位置
            Camera.main.transform.position = Vector3.Lerp(initialPosition, originalPosition, elapsedTime / zoomDuration);

            // 使用 Mathf.Lerp 逐渐从当前大小移动到初始大小
            Camera.main.orthographicSize = Mathf.Lerp(initialSize, originalSize, elapsedTime / zoomDuration);

            elapsedTime += Time.deltaTime; // 更新经过的时间
            yield return null;
        }

        // 确保最终位置是初始位置
        Camera.main.transform.position = originalPosition;

        // 确保最终大小是初始大小
        Camera.main.orthographicSize = originalSize;
    }
}



