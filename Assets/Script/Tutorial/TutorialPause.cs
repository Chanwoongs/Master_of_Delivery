using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialPause : MonoBehaviour
{

    public void SkipTutorial()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainScene");
    }

    public void GoTitle()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("TitleScene");
    } 
}
