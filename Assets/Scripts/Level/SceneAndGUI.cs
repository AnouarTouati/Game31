using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneAndGUI : MonoBehaviour
{

    public GameObject LostGUI;
    public GameObject FinishGUI;
    public bool DidFinishedLevel = false;
    public GameObject WinVFXPrefab;
    
    public GameObject Ball;
    private void Start()
    {
        LostGUI.SetActive(false) ;
    }
/*
   public void PlaySkipPlatformsParticles()
    {
        GameObject GO = Instantiate(SkipPlatformsVFXPrefab, Ball.GetComponent<Transform>().position, Quaternion.identity);
        GO.GetComponent<ParticleSystem>().Play();
        Ball.GetComponent<Ball>().SkipPlatformsVFX = GO;
        //this gameobject will follow the ball using the Ball script
    }
    */
    public void Lost()
    { 
        LostGUI.SetActive(true);
        Time.timeScale = 0;
    }
    public void FinishedLevel()
    {
        GameObject GO = Instantiate(WinVFXPrefab, Ball.GetComponent<Transform>().position, Quaternion.identity);
        GO.GetComponent<ParticleSystem>().Play();
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
