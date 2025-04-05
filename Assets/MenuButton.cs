using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    // 當按鈕被點擊時，這個方法會被調用
    public void OpenMenu()
    {
        SceneManager.LoadScene("_Menu"); // 替換為你的 Menu 場景名稱
    }
}

