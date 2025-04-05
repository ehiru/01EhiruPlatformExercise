using System.Collections;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LocaleSelector : MonoBehaviour
{
    private bool active = false;

    public void ChangeLocals(int localeID)
    {
        if (active) // 修正邏輯錯誤
            return;
        StartCoroutine(SetLocale(localeID));
    }

    IEnumerator SetLocale(int _localeID)
    {
        active = true;

        // 確保 Localization 設定已初始化
        yield return LocalizationSettings.InitializationOperation;

        // 檢查 ID 是否有效，避免超出範圍錯誤
        if (_localeID >= 0 && _localeID < LocalizationSettings.AvailableLocales.Locales.Count)
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];
        }
        else
        {
            Debug.LogWarning("Locale ID 超出範圍: " + _localeID);
        }

        active = false;
    }
}
