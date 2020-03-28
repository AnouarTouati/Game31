using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform BallTransform;
   
    
    void Update()
    {
        GetComponent<Transform>().position =new Vector3(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y, BallTransform.position.z);
    }
}
