using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    private string objectID;
    private bool isInteracted = false; // 是否被玩家互動過

    void Start()
    {
        objectID = gameObject.name + "_" + GetInstanceID(); // 獨立 ID

        // **讀取存檔狀態**
        if (PlayerPrefs.HasKey(objectID + "_interacted"))
        {
            isInteracted = PlayerPrefs.GetInt(objectID + "_interacted") == 1;

            // 如果是開關、門，設定開啟狀態
            if (isInteracted)
            {
                ApplyInteractionEffect();
            }
        }
    }

    public void Interact()
    {
        isInteracted = true;
        PlayerPrefs.SetInt(objectID + "_interacted", 1);
        PlayerPrefs.Save();

        ApplyInteractionEffect();
    }

    private void ApplyInteractionEffect()
    {
        // 這裡可以根據你的物件類型自訂不同效果：
        if (gameObject.CompareTag("Door")) 
        {
            gameObject.SetActive(false); // 門消失
        }
        else if (gameObject.CompareTag("Switch"))
        {
            GetComponent<Animator>().SetTrigger("Activate"); // 觸發開關動畫
        }
        else if (gameObject.CompareTag("Enemy"))
        {
            gameObject.SetActive(false); // 敵人消失
        }
    }
}

