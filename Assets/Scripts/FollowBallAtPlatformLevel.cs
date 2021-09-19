using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBallAtPlatformLevel : MonoBehaviour
{

    [SerializeField] GameObject Ball;
    
    void Update()
    {
        transform.position = new Vector3(transform.position.x,transform.position.y,Ball.transform.position.z);
    }
}
