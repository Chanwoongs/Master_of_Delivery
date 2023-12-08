using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialPause : MonoBehaviour
{
    GameObject skip;

    private void Start()
    {
        skip = GameObject.Find("TalkCanvas").transform.GetChild(2).gameObject;
    }

    public void SkipTutorial()
    {
        skip.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("MainScene");
        Cursor.visible = false;
    }

    public void GoTitle()
    {
        skip.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("TitleScene");
        Cursor.visible = true;
    } 
}
