using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour
{
    public SceneAndGUI SceneAndGUI;
    private void OnCollisionEnter(Collision collision)
    {
        SceneAndGUI.Lost();
    }
}
