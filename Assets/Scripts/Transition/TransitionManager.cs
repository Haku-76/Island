using UnityEngine;
using Cinemachine;

public class TransitionManager : MonoBehaviour
{
    private static TransitionManager _instance;
    public static TransitionManager Instance{get => _instance;}

    void Awake()
    {
        if(_instance != null)
        {
            Destroy(_instance);
        }
        else
        {
            _instance = this;
        }
    }

    [SerializeField]private CinemachineBrain mainCamera;

    private CinemachineVirtualCamera currentCamera;
    private CinemachineVirtualCamera prevCamera;

    public PlayerController player;
    [Header("机位")]
    public CinemachineVirtualCamera panoramaCam;

    

    void Start()
    {
        player.gameObject.SetActive(false);
        currentCamera = mainCamera.ActiveVirtualCamera as CinemachineVirtualCamera;
    }

    public void SetCameraWithTransition(CinemachineVirtualCamera cameraStand, bool isFollowPlayer)
    {
        mainCamera.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.EaseInOut;
        mainCamera.m_DefaultBlend.m_Time = 1.5f;
        prevCamera = currentCamera;
        currentCamera.Priority = 10;
        cameraStand.Priority = 100;
        currentCamera = cameraStand;
        if(isFollowPlayer)
            currentCamera.m_Follow = player.transform;
    }
    public void SetCameraWithOutTransition(CinemachineVirtualCamera cameraStand, bool isFollowPlayer)
    {
        mainCamera.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
        prevCamera = currentCamera;
        currentCamera.Priority = 10;
        cameraStand.Priority = 100;
        currentCamera = cameraStand;
        if(isFollowPlayer)
            currentCamera.m_Follow = player.transform;
    }

    public void ReturnPreCamera()
    {
        if(prevCamera == null) return;
        currentCamera.Priority = 10;
        prevCamera.Priority = 100;
        currentCamera = prevCamera;
        prevCamera = null;
    }

    public void MovePlayerTo(Vector3 targetPos)
    {
        player.transform.position = targetPos;
    }

/// <summary>
/// 切换时间段时，移动至全景镜头
/// </summary>
    public void StartSkipTime()
    {
        SetCameraWithTransition(panoramaCam, false);
        player.LockPlayer();
    }

/// <summary>
/// 时间切换动画结束时，恢复至刚才的镜头
/// </summary>
    public void EndSkipTime()
    {
        ReturnPreCamera();
        player.UnLockPlayer();
    }
}
