﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public PlatformSpawner PlatformSpawner;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.transform.tag=="Pickables")
        {
            if (collision.collider.gameObject.GetComponent<Pickables>().SkipPlatforms)
            {
                SkipPlatforms(collision.collider.transform.position, collision.collider.gameObject.GetComponent<Pickables>().NumberOfPlatformsToSkip);
            }
            Destroy(collision.collider.gameObject);
        }
       
    }

    public void SkipPlatforms(Vector3 HitPostiion, int NumberPlatformToSkip)
    {
        float HalfWayZposition;

        int LaunchingPlatformIndex = -1;
        for (int i = 0; i < PlatformSpawner.GetPlatforms().Count; i++)
        {
            if (PlatformSpawner.GetPlatforms()[i].GetComponent<PlatformController>().IsPlatformAlreadyUsed)
            {
                LaunchingPlatformIndex = i;//we get the last used platform
            }
        }
        int DestinationPlatformIndex = Mathf.Clamp(LaunchingPlatformIndex + NumberPlatformToSkip, 0, PlatformSpawner.GetPlatforms().Count - 1);
        /*
        float ZDistance = Vector3.Distance(HitPostiion, PlatformSpawner.GetPlatforms()[DestinationPlatformIndex].GetComponent<Transform>().position);
        HalfWayZposition = HitPostiion.z + (ZDistance / 2);
        StartCoroutine(MoveTheBallInArchToDestinations(HalfWayZposition, PlatformSpawner.GetPlatforms()[DestinationPlatformIndex].GetComponent<Transform>().position.z));
   */
        Vector2 CentralPoint = new Vector2();
        CentralPoint.x = HitPostiion.z + ((PlatformSpawner.GetPlatforms()[DestinationPlatformIndex].GetComponent<Transform>().position.z - HitPostiion.z)/2);
        //here assuming that contact point is higher than platform y at all cases
        CentralPoint.y= HitPostiion.y + ((HitPostiion.z - PlatformSpawner.GetPlatforms()[DestinationPlatformIndex].GetComponent<Transform>().position.z) / 2);
        float RadiusSquared =Mathf.Pow(Vector2.Distance(CentralPoint, new Vector2(HitPostiion.z, HitPostiion.y)),2);
        StartCoroutine(MoveTheBallInCircle(CentralPoint, RadiusSquared, PlatformSpawner.GetPlatforms()[DestinationPlatformIndex].GetComponent<Transform>().position.z));
    }
    /*
    IEnumerator MoveTheBallInArchToDestinations(float HalfWayZposition,float FinalZPosition)
    {

        yield return new WaitForFixedUpdate();
        if (transform.position.z < HalfWayZposition)
        {
            transform.position += new Vector3(0, 0.1f, 0.1f);
        }
        else
        {
            transform.position += new Vector3(0, -0.1f, 0.1f);
        }
        if (transform.position.z < FinalZPosition)
        {
          //  StartCoroutine(MoveTheBallInArchToDestinations(HalfWayZposition, FinalZPosition));
        }
        

    }
    */
    IEnumerator MoveTheBallInCircle(Vector2 CentralPoint, float RadiusSquared, float FinalZPosition)
    {

        yield return new WaitForFixedUpdate();
        transform.position += new Vector3(0, 0, 5f * Time.deltaTime);
        transform.position = new Vector3(transform.position.x,CentralPoint.y+Mathf.Sqrt(RadiusSquared-Mathf.Pow(transform.position.z-CentralPoint.x,2)), transform.position.z);
        if (transform.position.z < FinalZPosition)
        {
            StartCoroutine(MoveTheBallInCircle(CentralPoint, RadiusSquared,FinalZPosition));
        }
        
    }
}