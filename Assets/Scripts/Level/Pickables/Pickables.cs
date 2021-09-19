using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickables : MonoBehaviour
{
    [SerializeField] bool skipPlatforms;
    [SerializeField] int numberOfPlatformsToSkip;
    public bool SkipPlatforms
    {
        get { return skipPlatforms; }
    }
    public int NumberOfPlatformsToSkip
    {
        get { return numberOfPlatformsToSkip; }
    }
}

