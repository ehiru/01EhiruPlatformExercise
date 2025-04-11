using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingNPC : MonoBehaviour
{
    public string endingASceneName = "Ending_A";
    public string endingBSceneName = "Ending_B";
    public int scoreThreshold = 5; // 分數 5 或以上進入 Ending B

    public bool playerIsClose = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && playerIsClose)
        {
            CheckAndLoadEnding();
        }
    }

    private void CheckAndLoadEnding()
    {
        int score = PlayerPrefs.GetInt("TotalScore", 0); // 預設為 0 分

        Debug.Log("Final Score: " + score);

        if (score >= scoreThreshold)
        {
            SceneManager.LoadScene(endingBSceneName);
        }
        else if (score > 0)
        {
            SceneManager.LoadScene(endingASceneName);
        }
        else
        {
            Debug.Log("No valid score found. Player might not have made a choice.");
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
        }
    }
}

