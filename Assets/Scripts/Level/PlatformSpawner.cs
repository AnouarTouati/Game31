using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject PlatformPrefab;
    public Transform FirstPlatformTransform;
    public float DistanceBetweenPlatform;
    public int NumberOfPlatforms;
    void Start()
    {
        for(int i = 0; i < NumberOfPlatforms; i++)
        {
            Instantiate(PlatformPrefab, FirstPlatformTransform.position + new Vector3(0, 0, DistanceBetweenPlatform*i),FirstPlatformTransform.rotation,transform);
        }
        Instantiate(PlatformPrefab, FirstPlatformTransform.position + new Vector3(0, 0, DistanceBetweenPlatform * NumberOfPlatforms), FirstPlatformTransform.rotation, transform)
            .GetComponent<PlatformController>().SetFinalPlatform(true);
    }

    
    
}
