using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class follow_ball_at_platform_level_test
{
    // A Test behaves as an ordinary method
    [Test]
    public void object1_follows_ball_on_z_axis_position()
    {
        //ARRANGE
        IBall ball = Substitute.For<IBall>();
        Vector3 ballPosition = new Vector3(1,2, 3);
        ball.Position.Returns(ballPosition);
        IFollowBallAtPlatformLevel followBallAtPlatformLevel = Substitute.For<IFollowBallAtPlatformLevel>();
        Vector3 ballFollowerPosition = new Vector3(0, 0, 0);
        followBallAtPlatformLevel.Position.Returns(ballFollowerPosition);
        FollowBallAtPlatformLevelController followBallAtPlatformLevelController = new FollowBallAtPlatformLevelController(followBallAtPlatformLevel, ball);
       
        //ACT
        followBallAtPlatformLevelController.FollowAlongZAxis();

        //ASSERT
        Assert.AreEqual(0, followBallAtPlatformLevel.Position.x);
        Assert.AreEqual(0, followBallAtPlatformLevel.Position.y);
        Assert.AreEqual(3, followBallAtPlatformLevel.Position.z);
        Assert.AreEqual(1, ball.Position.x);
        Assert.AreEqual(2, ball.Position.y);
        Assert.AreEqual(3, ball.Position.z);

    }
    
    /*
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator follow_ball_at_platform_level_testWithEnumeratorPasses()
    {
        yield return null;  
    }*/
}
