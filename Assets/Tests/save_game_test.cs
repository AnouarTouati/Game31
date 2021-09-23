using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class save_game_test
{
    
    [Test]
    public void write_read_lifes()
    {
        //ARRANGE
        SaveGame saveGame = new SaveGame();
        //ACT
        saveGame.Lives = 3;
        //ASSERT
        Assert.AreEqual(3, saveGame.Lives);

    }
/*
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator save_game_testWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }*/
}
