using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStateManager : MonoBehaviour
{
    public static SceneStateManager instance;

    public GameObject[] objectsToSave; // 需要保存的对象

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 确保在切换场景时不会被销毁
        }
        else
        {
            Destroy(gameObject); // 确保场景中只有一个 SceneStateManager
        }
    }

    // 保存当前场景的状态
    public void SaveCurrentSceneState()
    {
        foreach (var obj in objectsToSave)
        {
            PlayerPrefs.SetFloat(obj.name + "_posX", obj.transform.position.x);
            PlayerPrefs.SetFloat(obj.name + "_posY", obj.transform.position.y);
            PlayerPrefs.SetFloat(obj.name + "_posZ", obj.transform.position.z);
            PlayerPrefs.SetInt(obj.name + "_active", obj.activeSelf ? 1 : 0);
        }
        PlayerPrefs.Save();
    }

    // 恢复场景状态
    public void LoadSceneState()
    {
        foreach (var obj in objectsToSave)
        {
            if (PlayerPrefs.HasKey(obj.name + "_posX"))
            {
                float posX = PlayerPrefs.GetFloat(obj.name + "_posX");
                float posY = PlayerPrefs.GetFloat(obj.name + "_posY");
                float posZ = PlayerPrefs.GetFloat(obj.name + "_posZ");
                obj.transform.position = new Vector3(posX, posY, posZ);

                bool isActive = PlayerPrefs.GetInt(obj.name + "_active") == 1;
                obj.SetActive(isActive);
            }
        }
    }
}

