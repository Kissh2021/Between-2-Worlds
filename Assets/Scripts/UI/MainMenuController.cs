using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private string playScene;
    [SerializeField]
    private string creditScene;

    public void play()
    {
        SceneManager.LoadScene(playScene);
    }

    public void credit()
    {
        SceneManager.LoadScene(creditScene);
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
