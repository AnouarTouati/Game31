using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneAndGUI : MonoBehaviour
{

    public GameObject RetryButtonGameObject;
    private void Start()
    {
        RetryButtonGameObject.SetActive(false) ;
    }

    public void Lost()
    {
        //do gui stuff stop   player input or pause the game
        RetryButtonGameObject.SetActive(true);
        Time.timeScale = 0;
    }
    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
}
