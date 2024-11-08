using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class startUImanager : MonoBehaviour
{
    public void Startgme()
    {
        SceneManager.LoadScene(1);
    }
    public void Credits()
    {
        SceneManager.LoadScene(2);
    }
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void quitgame()
    {
        Application.Quit();
    }
}
