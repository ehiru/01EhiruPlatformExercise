using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingTrigger : MonoBehaviour
{
    public string endingASceneName = "Ending_A";
    public string endingBSceneName = "Ending_B";
    public int thresholdScore = 5;
    public bool playerIsClose = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && playerIsClose)
        {
            int score = PlayerPrefs.GetInt("TotalScore", 0);
            Debug.Log("Final Score: " + score);

            if (score >= thresholdScore)
            {
                SceneManager.LoadScene(endingBSceneName);
            }
            else if (score > 0)
            {
                SceneManager.LoadScene(endingASceneName);
            }
            else
            {
                Debug.Log("Score is 0. No ending triggered.");
            }
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

