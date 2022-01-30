using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private int playScene;

    public void play()
    {
        SceneManager.LoadScene(playScene);
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
