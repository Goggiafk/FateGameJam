using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
    public GameObject menuObject;
        
    public void changeScene(string name)
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    }

    public void quit()
    {
        Application.Quit();
    }

    public void returnToGame()
    {
        Time.timeScale = 1;
        menuObject.SetActive(false);
    }
}
