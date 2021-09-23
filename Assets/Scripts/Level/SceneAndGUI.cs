using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneAndGUI : MonoBehaviour
{

    [SerializeField] GameObject LostGUI;
    [SerializeField] GameObject FinishGUI;
    [SerializeField] GameObject WatchAdGUI;
    [SerializeField] GameObject AdFailedGUI;
    [SerializeField] Text LivesCount;
    [SerializeField] bool didFinishLevel = false;
    public bool DidFinishLevel
    {
        get { return didFinishLevel; }
    }
    [SerializeField] GameObject WinVFXPrefab;
    [SerializeField] AudioScript audioScript;
    [SerializeField] GameObject Ball;
    [SerializeField] RewardedAdsButton rewardedAdsButton;
    private void Start()
    {
        LostGUI.SetActive(false) ;
        LivesCount.text = ""+GameSystem.Lives;
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
        GameSystem.Lives--;
        LostGUI.SetActive(true);
        audioScript.PlayLost();
        Time.timeScale = 0;
    }
    public void FinishedLevel()
    {
        GameObject GO = Instantiate(WinVFXPrefab, Ball.GetComponent<Transform>().position, Quaternion.identity);
        GO.GetComponent<ParticleSystem>().Play();
        audioScript.PlayWin();
        didFinishLevel = true;
        FinishGUI.SetActive(true);
    }
    public void Retry()
    {
        if (GameSystem.Lives < 1)
        {
            AskToWatchAd();
        }
        else
        {
            Time.timeScale = 1;
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
    private void AskToWatchAd()
    {
        LostGUI.SetActive(false);
        WatchAdGUI.SetActive(true);
    }
    private void AdWatchedSuccessfully() 
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
   
}
