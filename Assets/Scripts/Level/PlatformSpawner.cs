using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject PlatformPrefab;
    public Transform FirstPlatformTransform;
    public float DistanceBetweenPlatform;
    public int NumberOfPlatforms;
    private List<GameObject> Platforms=new List<GameObject>();
    void Start()
    {
        
        for(int i = 0; i < NumberOfPlatforms; i++)
        {
          Platforms.Add(Instantiate(PlatformPrefab, FirstPlatformTransform.position + new Vector3(0, 0, DistanceBetweenPlatform*i),FirstPlatformTransform.rotation,transform));
        }
        Platforms.Add(Instantiate(PlatformPrefab, FirstPlatformTransform.position + new Vector3(0, 0, DistanceBetweenPlatform * NumberOfPlatforms), FirstPlatformTransform.rotation, transform));
        Platforms[Platforms.Count - 1].GetComponent<PlatformController>().SetFinalPlatform(true);
    }

   public List<GameObject> GetPlatforms() { return Platforms; }
    
}
