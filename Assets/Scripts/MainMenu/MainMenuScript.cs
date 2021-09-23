using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuScript : MonoBehaviour
{

    [SerializeField] Button Play;
    [SerializeField] Text Lives;
    [SerializeField] GameObject CountDownGUI;
    [SerializeField] Text Time;
    private float TimeCounter;
    [SerializeField] int MaxLivesAllowed;
    [SerializeField] float TimeToWaitBeforeLivesRestore;
    private bool CountDownStarted = false;
  
   
    void Update()
    {
        if (GameSystem.Lives < 1)
        {
            Play.interactable = false;
        }else if (GameSystem.Lives > 0)
        {
            Play.interactable = true;
        }
            Lives.text = ""+GameSystem.Lives;
        if(GameSystem.Lives< MaxLivesAllowed && CountDownStarted==false)
        {
            TimeCounter = TimeToWaitBeforeLivesRestore;
            CountDownGUI.SetActive(true);
            StartCoroutine(CountDownToAddLives());
            CountDownStarted = true;
        }else if (CountDownStarted)
        {

            Time.text = "" + Mathf.RoundToInt(TimeCounter) + "s";
            TimeCounter -= UnityEngine.Time.deltaTime;
        }
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }
    public void ExitFromGame()
    {
        Application.Quit();
    }
    private IEnumerator CountDownToAddLives()
    {
        yield return new WaitForSeconds(TimeToWaitBeforeLivesRestore);
        CountDownGUI.SetActive(false);
        GameSystem.Lives = MaxLivesAllowed;
        CountDownStarted = false;
    }
}
