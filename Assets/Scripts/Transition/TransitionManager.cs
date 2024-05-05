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
    
    public GameObject player;

    void Start()
    {
        currentCamera = mainCamera.ActiveVirtualCamera as CinemachineVirtualCamera;
    }

    public void SetCamera(CinemachineVirtualCamera cameraStand)
    {
        currentCamera.Priority = 10;
        cameraStand.Priority = 100;
        currentCamera = cameraStand;
    }

    public void MovePlayerTo(Vector3 targetPos)
    {
        player.transform.position = targetPos;
    }
}
