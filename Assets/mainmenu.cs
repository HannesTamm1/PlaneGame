using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour
{
    public void playgame()
    {
        SceneManager.LoadSceneAsync("level1");
    }

    public void quitgame()
    {
        Application.Quit();
    }
}
