using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Localization;

public class NPC2 : MonoBehaviour
{
    [Header("Dialogue System")]
    public GameObject dialoguePanel;
    public Text dialogueText;
    public LocalizedString[] dialogues;
    private int index = 0;

    [Header("UI Elements")]
    public GameObject contButton;
    public GameObject optionPanel;
    public Button[] optionButtons;
    public LocalizedString[] optionTexts;
    public float wordSpeed = 0.05f;
    public bool playerIsClose = false;

    [System.Serializable]
    public class OptionEvent : UnityEvent<int> { }
    public OptionEvent onOptionSelected;

    private bool hasChosenOption = false;
    private string playerPrefKey;

    private int score = 0; // ✅ 分數變數加在這

    private void Start()
    {
        optionPanel.SetActive(false);
        playerPrefKey = "NPC_DialogueChoice_" + gameObject.name;

        if (PlayerPrefs.HasKey(playerPrefKey))
        {
            int savedChoice = PlayerPrefs.GetInt(playerPrefKey);
            ApplyDialogueChoice(savedChoice);
            hasChosenOption = true;
        }

        for (int i = 0; i < optionButtons.Length; i++)
        {
            int capturedIndex = i;
            optionButtons[i].onClick.AddListener(() => OnOptionSelected(capturedIndex));
        }
    }

  private void Update()
{
    if (Input.GetKeyDown(KeyCode.Return) && playerIsClose)
    {
        if (dialoguePanel.activeInHierarchy)
        {
            // 如果打字動畫完成，Enter 就是「繼續」
            if (dialogueText.text == dialogues[index].GetLocalizedString())
            {
                NextLine();
            }
            else
            {
                // 如果還在打字中，就快速顯示完整對話
                StopAllCoroutines();
                dialogueText.text = dialogues[index].GetLocalizedString();
            }
        }
        else
        {
            // 開啟對話面板並開始打字
            dialoguePanel.SetActive(true);
            StartCoroutine(Typing());
        }
    }

    // 顯示繼續按鈕條件不變（維持原本功能）
    if (dialogueText.text == dialogues[index].GetLocalizedString())
    {
        contButton.SetActive(true);
    }
    else
    {
        contButton.SetActive(false); // 預防提早顯示
    }
}

    public void NextLine()
    {
        contButton.SetActive(false);

        if (index < dialogues.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            if (hasChosenOption)
                CloseDialogue();
            else
                ShowOptions();
        }
    }

    private void ShowOptions()
    {
        if (hasChosenOption) return;

        optionPanel.SetActive(true);

        for (int i = 0; i < optionTexts.Length && i < optionButtons.Length; i++)
        {
            int idx = i;
            optionTexts[i].GetLocalizedStringAsync().Completed += (handle) =>
            {
                optionButtons[idx].GetComponentInChildren<Text>().text = handle.Result;
            };
            optionButtons[i].gameObject.SetActive(true);
        }
    }

    public void OnOptionSelected(int optionIndex)
{
    if (hasChosenOption) return;

    hasChosenOption = true;
    optionPanel.SetActive(false);
    StopAllCoroutines();
    dialogueText.text = "";

    if (optionIndex == 0)
    {
        score += 1;
        Debug.Log("Player selected Egg Waffle (+1)");
    }
    else if (optionIndex == 1)
    {
        score += 5;
        Debug.Log("Player selected Fishball (+5)");
    }

    // 🔥 儲存分數進全域分數
    int currentTotalScore = PlayerPrefs.GetInt("TotalScore", 0);
    currentTotalScore += score;
    PlayerPrefs.SetInt("TotalScore", currentTotalScore);
    PlayerPrefs.Save();

    Debug.Log("Total Score: " + currentTotalScore);

    PlayerPrefs.SetInt(playerPrefKey, optionIndex);

    ApplyDialogueChoice(optionIndex);
    index = 0;
    dialoguePanel.SetActive(true);
    StartCoroutine(Typing());
}


    private void ApplyDialogueChoice(int optionIndex)
    {
        if (optionIndex == 0)
        {
            dialogues = new LocalizedString[]
            {
                new LocalizedString { TableReference = "FirstDialogues", TableEntryReference = "Greek" },
                new LocalizedString { TableReference = "FirstDialogues", TableEntryReference = "people" }
            
            };
        }
        else if (optionIndex == 1)
        {
            dialogues = new LocalizedString[]
            {
                new LocalizedString { TableReference = "FirstDialogues", TableEntryReference = "christian" },
                new LocalizedString { TableReference = "FirstDialogues", TableEntryReference = "people" }
              
            };
        }
    }

    public void CloseDialogue()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        dialogueText.text = "";
        string localizedText = dialogues[index].GetLocalizedString();

        foreach (char letter in localizedText.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            CloseDialogue();
        }
    }
}
