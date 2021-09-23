using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBallAtPlatformLevel : MonoBehaviour, IFollowBallAtPlatformLevel
{

    [SerializeField] Ball ball;
   public Ball Ball { get { return ball; } set { ball = value; } }

    public Vector3 Position { get =>transform.position; set => transform.position=value; }

    private FollowBallAtPlatformLevelController followBallAtPlatformLevelController;
    void Awake()
    {
        followBallAtPlatformLevelController = new FollowBallAtPlatformLevelController(this,ball);
    }
    void Update()
    {
        if (ball != null)
            followBallAtPlatformLevelController.FollowAlongZAxis();
    }
  

}
public interface IFollowBallAtPlatformLevel
{
    Vector3 Position { get; set; }
  
}