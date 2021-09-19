using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneAndGUI : MonoBehaviour
{

    public GameObject LostGUI;
    public GameObject FinishGUI;
    public GameObject WatchAdGUI;
    public GameObject AdFailedGUI;
    public Text LivesCount;
    public bool DidFinishedLevel = false;
    public GameObject WinVFXPrefab;
    public AudioScript audioScript;
    public string ActiveSceneName;
    public GameObject Ball;
    public RewardedAdsButton rewardedAdsButton;
    private void Start()
    {
        LostGUI.SetActive(false) ;
        LivesCount.text = ""+GameSystem.Life;
        ActiveSceneName = SceneManager.GetActiveScene().name;
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
    private void Update()
    {
        if (rewardedAdsButton.AdShown)
        {
            AdWatchedSuccessfully();
        }
    }
    public void Lost()
    {
        GameSystem.Life--;
        LostGUI.SetActive(true);
        audioScript.PlayLost();
        Time.timeScale = 0;
    }
    public void FinishedLevel()
    {
        GameObject GO = Instantiate(WinVFXPrefab, Ball.GetComponent<Transform>().position, Quaternion.identity);
        GO.GetComponent<ParticleSystem>().Play();
        audioScript.PlayWin();
        DidFinishedLevel = true;
        FinishGUI.SetActive(true);
    }
    public void Retry()
    {
        if (GameSystem.Life < 1)
        {
            AskToWatchAd();
        }
        else
        {
            Time.timeScale = 1;
            ActiveSceneName = SceneManager.GetActiveScene().name;
           SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }
       
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
          //  Debug.LogError("MyErrors : There is no next scene to load");
        }
       
    }
    public void GoMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0,LoadSceneMode.Single);
    }
    public void AskToWatchAd()
    {
        LostGUI.SetActive(false);
        WatchAdGUI.SetActive(true);
    }
    public void AdWatchedSuccessfully() 
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
    public IEnumerator AdFailed()
    {
        WatchAdGUI.SetActive(false);
        AdFailedGUI.SetActive(true);
        yield return new WaitForSeconds(2f);
        WatchAdGUI.SetActive(true);
        AdFailedGUI.SetActive(false);
    }
    public string GetActiveSceneName()
    {
      return   ActiveSceneName;
    }
}
