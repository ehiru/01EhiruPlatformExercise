
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class NPC : MonoBehaviour
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

    private void Start()
    {
        optionPanel.SetActive(false);
        playerPrefKey = "NPC_DialogueChoice_" + gameObject.name;
        
        // 檢查玩家是否已選擇過選項
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
                CloseDialogue();
            }
            else
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }

        if (dialogueText.text == dialogues[index].GetLocalizedString())
        {
            contButton.SetActive(true);
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
            {
                CloseDialogue();
            }
            else
            {
                ShowOptions();
            }
        }
    }

    private void ShowOptions()
    {
        if (hasChosenOption) return;

        optionPanel.SetActive(true);

        for (int i = 0; i < optionTexts.Length && i < optionButtons.Length; i++)
        {
            optionTexts[i].GetLocalizedStringAsync().Completed += (handle) =>
            {
                optionButtons[i].GetComponentInChildren<Text>().text = handle.Result;
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

        PlayerPrefs.SetInt(playerPrefKey, optionIndex);
        PlayerPrefs.Save();
        
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
                new LocalizedString { TableReference = "Dialogues", TableEntryReference = "soft_thing" },
                new LocalizedString { TableReference = "Dialogues", TableEntryReference = "spherical_objects" },
                new LocalizedString { TableReference = "Dialogues", TableEntryReference = "lost_energy" }
            };
        }
        else if (optionIndex == 1)  
        {
            dialogues = new LocalizedString[]
            {
                new LocalizedString { TableReference = "Dialogues", TableEntryReference = "bouncy_texture" },
                new LocalizedString { TableReference = "Dialogues", TableEntryReference = "beef_balls" },
                new LocalizedString { TableReference = "Dialogues", TableEntryReference = "lost_energy" }
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



// using System.Collections;
// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.Events;
// using UnityEngine.Localization;
// using UnityEngine.Localization.Settings;

// public class NPC : MonoBehaviour
// {
//     [Header("Dialogue System")]
//     public GameObject dialoguePanel;
//     public Text dialogueText;
//     public LocalizedString[] dialogues;  // 使用 LocalizedString 陣列存放對話
//     private int index = 0;

//     [Header("UI Elements")]
//     public GameObject contButton;
//     public GameObject optionPanel;
//     public Button[] optionButtons;
//     public LocalizedString[] optionTexts;  // 選項文字也使用 LocalizedString
//     public float wordSpeed = 0.05f;
//     public bool playerIsClose = false;

//     [System.Serializable]
//     public class OptionEvent : UnityEvent<int> { }
//     public OptionEvent onOptionSelected;

//     private bool hasChosenOption = false;

//     private void Start()
//     {
//         optionPanel.SetActive(false);

//         for (int i = 0; i < optionButtons.Length; i++)
//         {
//             int capturedIndex = i;
//             optionButtons[i].onClick.AddListener(() => OnOptionSelected(capturedIndex));
//         }
//     }

//     private void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.Return) && playerIsClose) 
//         {
//             if (dialoguePanel.activeInHierarchy)
//             {
//                 CloseDialogue();
//             }
//             else
//             {
//                 dialoguePanel.SetActive(true);
//                 StartCoroutine(Typing());
//             }
//         }

//         if (dialogueText.text == dialogues[index].GetLocalizedString())
//         {
//             contButton.SetActive(true);
//         }
//     }

//     public void NextLine()
//     {
//         contButton.SetActive(false);

//         if (index < dialogues.Length - 1)
//         {
//             index++;
//             dialogueText.text = "";
//             StartCoroutine(Typing());
//         }
//         else
//         {
//             if (hasChosenOption)
//             {
//                 CloseDialogue();
//             }
//             else
//             {
//                 ShowOptions();
//             }
//         }
//     }

//     private void ShowOptions()
//     {
//         if (hasChosenOption) return;

//         optionPanel.SetActive(true);

//         for (int i = 0; i < optionTexts.Length && i < optionButtons.Length; i++)
//         {
//             optionTexts[i].GetLocalizedStringAsync().Completed += (handle) =>
//             {
//                 optionButtons[i].GetComponentInChildren<Text>().text = handle.Result;
//             };
//             optionButtons[i].gameObject.SetActive(true);
//         }
//     }

//     public void OnOptionSelected(int optionIndex)
//     {
//         if (hasChosenOption) return;

//         hasChosenOption = true;
//         optionPanel.SetActive(false);
//         StopAllCoroutines();
//         dialogueText.text = "";

//         // 設定新的對話內容
//         if (optionIndex == 0)  
//         {
//             dialogues = new LocalizedString[] 
//             {
//                 new LocalizedString { TableReference = "Dialogues", TableEntryReference = "soft_thing" },
//                 new LocalizedString { TableReference = "Dialogues", TableEntryReference = "spherical_objects" },
//                 new LocalizedString { TableReference = "Dialogues", TableEntryReference = "lost_energy" }
//             };
//         }
//         else if (optionIndex == 1)  
//         {
//             dialogues = new LocalizedString[]
//             {
//                 new LocalizedString { TableReference = "Dialogues", TableEntryReference = "bouncy_texture" },
//                 new LocalizedString { TableReference = "Dialogues", TableEntryReference = "beef_balls" },
//                 new LocalizedString { TableReference = "Dialogues", TableEntryReference = "lost_energy" }
//             };
//         }

//         index = 0;
//         dialoguePanel.SetActive(true);
//         StartCoroutine(Typing());
//     }

//     public void CloseDialogue()
//     {
//         dialogueText.text = "";
//         index = 0;
//         dialoguePanel.SetActive(false);
//     }

//     IEnumerator Typing()
//     {
//         dialogueText.text = "";
//         string localizedText = dialogues[index].GetLocalizedString();

//         foreach (char letter in localizedText.ToCharArray())
//         {
//             dialogueText.text += letter;
//             yield return new WaitForSeconds(wordSpeed);
//         }
//     }

//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             playerIsClose = true;
//         }
//     }

//     private void OnTriggerExit2D(Collider2D other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             playerIsClose = false;
//             CloseDialogue();
//         }
//     }
// }