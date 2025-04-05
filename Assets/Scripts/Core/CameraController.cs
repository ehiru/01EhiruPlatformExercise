// using UnityEngine;

// public class CameraController : MonoBehaviour
// {
//     //Room camera
//     [SerializeField] private float speed;
//     private float currentPosX;
//     private Vector3 velocity = Vector3.zero;

//     //Follow player
//     [SerializeField] private Transform player;
//     [SerializeField] private float aheadDistance;
//     [SerializeField] private float cameraSpeed;
//     private float lookAhead;

//     private void Update()
//     {
//         //Room camera
//         transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z), ref velocity, speed);

//         //Follow player
//         //transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);
//         //lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);
//     }

//     public void MoveToNewRoom(Transform _newRoom)
//     {
//         print("here");
//         currentPosX = _newRoom.position.x;
//     }
// }
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera leftCamera;  // 左側攝影機
    [SerializeField] private Camera rightCamera; // 右側攝影機

    [SerializeField] private Transform player; // 玩家 Transform
    [SerializeField] private Transform playerHead; // Player head (if you use head as reference, optional)

    [SerializeField] private float aheadDistance = 2f; // 攝影機在玩家前的位置
    [SerializeField] private float cameraSpeed = 2f; // 攝影機平滑移動速度

    private float lookAhead; // 攝影機的前瞻距離

    // Room camera
    [SerializeField] private float roomTransitionSpeed = 0.2f; // 房間切換速度
    private float currentPosX; // 房間的 X 軸位置
    private Vector3 velocity = Vector3.zero; // 用於 SmoothDamp 的速率參數

    // Offset for the camera to follow the player slightly
    [SerializeField] private Vector3 cameraOffset = new Vector3(0f, 10f, 0f); // Offset example: camera stays slightly ahead and above the player

    private void Start()
    {
        // 設置左攝影機的視口（占畫面左 30%）
        leftCamera.rect = new Rect(0, 0, 0.3f, 1); // X 起點: 0, Y 起點: 0, 寬度: 0.3, 高度: 1

        // 設置右攝影機的視口（占畫面右 70%）
        rightCamera.rect = new Rect(0.3f, 0, 0.7f, 1); // X 起點: 0.3, Y 起點: 0, 寬度: 0.7, 高度: 1
    }

    private void Update()
    {
        // 如果有玩家，執行跟隨玩家邏輯
        if (player != null)
        {
            FollowPlayer();
        }
        else
        {
            // 如果沒有玩家，則使用房間邏輯
            RoomCamera();
        }
    }

    /// <summary>
    /// 攝影機跟隨玩家邏輯 (now with offset)
    /// </summary>
    private void FollowPlayer()
    {
        // 確保 playerHead 不為空
        if (playerHead == null)
        {
            Debug.LogWarning("Player head transform is not assigned!");
        }

        // Camera target position calculation with offset
        // We use the player's position + the offset values to place the camera
        float targetX = player.position.x + cameraOffset.x + lookAhead;
        float targetY = player.position.y + cameraOffset.y; // Slightly above or below the player
        float targetZ = transform.position.z; // Keep the Z position fixed

        // Create the target position for the camera to follow
        Vector3 targetPosition = new Vector3(targetX, targetY, targetZ);

        // Smoothly move the camera to the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * cameraSpeed);

        // Gradually update the lookAhead distance (aheadDistance)
        lookAhead = Mathf.Lerp(lookAhead, aheadDistance * player.localScale.x, Time.deltaTime * cameraSpeed);
    }

    /// <summary>
    /// 房間攝影機邏輯 (當玩家進入新房間時)
    /// </summary>
    private void RoomCamera()
    {
        // 平滑移動到目標房間位置
        Vector3 targetPosition = new Vector3(currentPosX, transform.position.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, roomTransitionSpeed);
    }

    /// <summary>
    /// 當進入新房間時，更新攝影機目標位置
    /// </summary>
    public void MoveToNewRoom(Transform _newRoom)
    {
        currentPosX = _newRoom.position.x;
    }
}
