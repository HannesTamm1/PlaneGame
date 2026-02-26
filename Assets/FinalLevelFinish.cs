using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalLevelFinish : MonoBehaviour
{
    public GameObject gameFinishedPanel;
    public string homeSceneName = "MainMenu";

    private bool finished = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !finished)
        {
            finished = true;

            gameFinishedPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void GoHome()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(homeSceneName);
    }
}