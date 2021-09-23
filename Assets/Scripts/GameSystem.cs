using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    [SerializeField]
    private SaveGame _saveGame;
    public static int Lives = 2;

    private void Start()
    {
        Lives = _saveGame.Lives;
    }
}
