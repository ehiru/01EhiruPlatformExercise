using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpoint;
    private Transform currentCheckpoint;
    private Health playerHealth;
    private UIManager uiManager;

    // 用來儲存 checkpoint 位置
    private const string PosX = "Checkpoint_X";
    private const string PosY = "Checkpoint_Y";

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        uiManager = FindObjectOfType<UIManager>();

        LoadCheckpoint(); // 嘗試載入儲存的 checkpoint 位置
    }

    public void RespawnCheck()
    {
        if (currentCheckpoint == null)
        {
            uiManager.GameOver(); // 如果沒找到 checkpoint，顯示遊戲結束
            return;
        }

        playerHealth.Respawn(); // 恢復玩家的血量與動畫
        transform.position = currentCheckpoint.position; // 移動玩家到 checkpoint 的位置

        // 移動攝影機到新的區域
        Camera.main.GetComponent<CameraController>().MoveToNewRoom(currentCheckpoint.parent);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Checkpoint"))
        {
            currentCheckpoint = collision.transform;

            // 儲存 checkpoint 的位置
            PlayerPrefs.SetFloat(PosX, currentCheckpoint.position.x);
            PlayerPrefs.SetFloat(PosY, currentCheckpoint.position.y);
            PlayerPrefs.Save();

            // 播放 checkpoint 音效
            SoundManager.instance.PlaySound(checkpoint);

            // 播放 checkpoint 的啟動動畫
            collision.GetComponent<Collider2D>().enabled = false; // 禁用 collider 讓玩家不能再觸發
            collision.GetComponent<Animator>().SetTrigger("activate");
        }
    }

    private void LoadCheckpoint()
    {
        // 嘗試從 PlayerPrefs 載入最後儲存的 checkpoint 位置
        if (PlayerPrefs.HasKey(PosX) && PlayerPrefs.HasKey(PosY))
        {
            float x = PlayerPrefs.GetFloat(PosX);
            float y = PlayerPrefs.GetFloat(PosY);

            // 設定玩家的位置
            transform.position = new Vector3(x, y, transform.position.z);
            Debug.Log("載入 checkpoint 位置：" + transform.position);
        }
    }
}





//有重啟遊戲重生 極簡版：No Animation
// using UnityEngine;

// public class PlayerRespawn : MonoBehaviour
// {
//     private const string PosX = "Checkpoint_X";
//     private const string PosY = "Checkpoint_Y";

//     private void Start()
//     {
//         if (PlayerPrefs.HasKey(PosX) && PlayerPrefs.HasKey(PosY))
//         {
//             float x = PlayerPrefs.GetFloat(PosX);
//             float y = PlayerPrefs.GetFloat(PosY);
//             transform.position = new Vector3(x, y, transform.position.z);
//             Debug.Log("載入成功：位置為 " + transform.position);
//         }
//         else
//         {
//             Debug.Log("尚未儲存任何 checkpoint");
//         }
//     }

//     private void OnTriggerEnter2D(Collider2D collision)
//     {
//         if (collision.CompareTag("Checkpoint"))
//         {
//             Vector3 pos = collision.transform.position;
//             PlayerPrefs.SetFloat(PosX, pos.x);
//             PlayerPrefs.SetFloat(PosY, pos.y);
//             PlayerPrefs.Save();
//             Debug.Log("儲存 checkpoint：" + pos);
//         }
//     }
// }








//No 重啟遊戲重生
// using UnityEngine;

// public class PlayerRespawn : MonoBehaviour
// {
//     [SerializeField] private AudioClip checkpoint;
//     private Transform currentCheckpoint;
//     private Health playerHealth;
//     private UIManager uiManager;

//     private void Awake()
//     {
//         playerHealth = GetComponent<Health>();
//         uiManager = FindObjectOfType<UIManager>();
//     }

//     public void RespawnCheck()
//     {
//         if (currentCheckpoint == null) 
//         {
//             uiManager.GameOver();
//             return;
//         }

//         playerHealth.Respawn(); //Restore player health and reset animation
//         transform.position = currentCheckpoint.position; //Move player to checkpoint location

//         //Move the camera to the checkpoint's room
//         Camera.main.GetComponent<CameraController>().MoveToNewRoom(currentCheckpoint.parent);
//     }
//     private void OnTriggerEnter2D(Collider2D collision)
//     {
//         if (collision.gameObject.tag == "Checkpoint")
//         {
//             currentCheckpoint = collision.transform;
//             SoundManager.instance.PlaySound(checkpoint);
//             collision.GetComponent<Collider2D>().enabled = false;
//             collision.GetComponent<Animator>().SetTrigger("activate");
//         }
//     }
// }