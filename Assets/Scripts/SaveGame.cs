using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame : ISaveGame
{
    public int Lives
    {
        get { return PlayerPrefs.GetInt("Lives"); }
        set { PlayerPrefs.SetInt("Lives", value); }
    }
   
}
public interface ISaveGame
{
    int Lives { get; set; }
}
