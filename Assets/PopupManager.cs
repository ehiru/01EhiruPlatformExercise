using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public GameObject popupPanel;
    private bool isPopupOpen = false;

    void Update()
    {
        if (isPopupOpen)
        {
            // 檢查是否有任意按鍵或滑鼠點擊
            if (Input.anyKeyDown || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                ClosePopup();
            }
        }
    }

    public void OpenPopup()
    {
        popupPanel.SetActive(true);
        isPopupOpen = true;
    }

    void ClosePopup()
    {
        popupPanel.SetActive(false);
        isPopupOpen = false;
    }
}
