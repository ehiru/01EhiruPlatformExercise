using TMPro;
using UnityEngine;

public class TMPMaterialLock : MonoBehaviour
{
    public TMP_Text tmpText; // 拖入你的 TextMeshPro 物件
    private Material defaultMaterial;

    void Start()
    {
        if (tmpText != null)
        {
            defaultMaterial = new Material(tmpText.fontMaterial); // 複製一份原始材質
            tmpText.fontMaterial = defaultMaterial; // 鎖定材質
        }
    }

    void LateUpdate()
    {
        if (tmpText.fontMaterial != defaultMaterial)
        {
            tmpText.fontMaterial = defaultMaterial; // 如果材質被更改，強制還原
        }
    }
}
