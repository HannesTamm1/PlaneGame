using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{
    public GameObject finishPanel;
    public string homeSceneName = "MainMenu";

    private bool levelCompleted = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !levelCompleted)
        {
            levelCompleted = true;

            UnlockNewLevel();

            finishPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    void UnlockNewLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int reachedIndex = PlayerPrefs.GetInt("ReachedIndex", 1);

        if (currentIndex >= reachedIndex)
        {
            PlayerPrefs.SetInt("ReachedIndex", currentIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", currentIndex + 1);
            PlayerPrefs.Save();
        }
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadHome()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(homeSceneName);
    }
}