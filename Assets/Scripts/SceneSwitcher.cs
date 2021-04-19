using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void PaloseboScene()
    {
        SceneManager.LoadScene(1);
    }
    public void PogScene()
    {
        SceneManager.LoadScene(2);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
