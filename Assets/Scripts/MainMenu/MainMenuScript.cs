using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuScript : MonoBehaviour
{

    public Button Play;
    public Text Lives;
    public GameObject CountDownGUI;
    public Text Time;
    private float TimeCounter;
    public int MaxLivesAllowed;
    public float TimeToWaitBeforeLivesRestore;
    private bool CountDownStarted = false;
    
    void Start()
    {
        
    }

   
    void Update()
    {
        if (GameSystem.Life < 1)
        {
            Play.interactable = false;
        }else if (GameSystem.Life > 0)
        {
            Play.interactable = true;
        }
            Lives.text = ""+GameSystem.Life;
        if(GameSystem.Life< MaxLivesAllowed && CountDownStarted==false)
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
    IEnumerator CountDownToAddLives()
    {
        yield return new WaitForSeconds(TimeToWaitBeforeLivesRestore);
        CountDownGUI.SetActive(false);
        GameSystem.Life = MaxLivesAllowed;
        CountDownStarted = false;
    }
}
