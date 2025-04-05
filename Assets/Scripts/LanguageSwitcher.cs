using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LanguageSwitcher : MonoBehaviour
{
    // 此方法用來根據語言代碼切換語言
    public void SwitchLanguage(string languageCode)
    {
        // 獲取對應語言的 Locale
        Locale locale = LocalizationSettings.AvailableLocales.GetLocale(languageCode);
        
        // 如果找到對應的語言，則設置為當前語言
        if (locale != null)
        {
            LocalizationSettings.SelectedLocale = locale;
        }
        else
        {
            Debug.LogError("語言代碼無效: " + languageCode);
        }
    }
}
