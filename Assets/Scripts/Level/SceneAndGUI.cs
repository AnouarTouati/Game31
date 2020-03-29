using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneAndGUI : MonoBehaviour
{

    public GameObject LostGUI;
    public GameObject FinishGUI;
    public bool DidFinishedLevel = false;
    private void Start()
    {
        LostGUI.SetActive(false) ;
    }

    public void Lost()
    { 
        LostGUI.SetActive(true);
        Time.timeScale = 0;
    }
    public void FinishedLevel()
    {
        DidFinishedLevel = true;
        FinishGUI.SetActive(true);
    }
    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
    public void GoNextLevel()
    {
        int NextScene = SceneManager.GetActiveScene().buildIndex;
        NextScene++;
        if (NextScene < SceneManager.sceneCount)
        {
            SceneManager.LoadScene(NextScene);
        }
        else
        {
            Debug.LogError("MyErrors : There is no next scene to load");
        }
       
    }
    public void GoMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0,LoadSceneMode.Single);
    }
}
