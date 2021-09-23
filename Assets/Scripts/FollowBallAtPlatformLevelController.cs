using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBallAtPlatformLevelController
{
    IFollowBallAtPlatformLevel followBallAtPlatformLevel;
    IBall ball;
    public FollowBallAtPlatformLevelController(IFollowBallAtPlatformLevel followBallAtPlatformLevel,IBall ball)
    {
        this.followBallAtPlatformLevel = followBallAtPlatformLevel;
        this.ball = ball;
    }

    public void FollowAlongZAxis()
    {
        followBallAtPlatformLevel.Position = new Vector3(followBallAtPlatformLevel.Position.x, followBallAtPlatformLevel.Position.y, ball.Position.z);
    }
}
