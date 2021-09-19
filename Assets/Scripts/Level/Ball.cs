using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] PlatformSpawner PlatformSpawner;
    [SerializeField] SceneAndGUI SceneAndGUI; 
    [SerializeField] GameObject SkipPlatformsVFXPrefab;
    // public GameObject SkipPlatformsVFX;
    [SerializeField] GameObject SkipPlatformsVFXObject;
    [SerializeField] AudioScript audioScript;
   
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.transform.tag=="Pickables")
        {
            if (collision.collider.gameObject.GetComponent<Pickables>().SkipPlatforms)
            {
                SkipPlatforms(transform.position, collision.collider.gameObject.GetComponent<Pickables>().NumberOfPlatformsToSkip);
            }
            Destroy(collision.collider.gameObject);
        } else if (collision.collider.transform.tag == "Platform")
        {
            audioScript.PlayPlatformHit();
        }
       
    }

    private void SkipPlatforms(Vector3 HitPostiion, int NumberPlatformToSkip)
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
         CentralPoint.y= PlatformSpawner.GetPlatforms()[DestinationPlatformIndex].GetComponent<Transform>().position.y + ((HitPostiion.y - PlatformSpawner.GetPlatforms()[DestinationPlatformIndex].GetComponent<Transform>().position.y) / 2);
       CentralPoint -= new Vector2(1, 2);
        
        float RadiusSquared =Mathf.Pow(Vector2.Distance(CentralPoint, new Vector2(HitPostiion.z, HitPostiion.y)),2);
       SkipPlatformsVFXObject = Instantiate(SkipPlatformsVFXPrefab, GetComponent<Transform>().position, Quaternion.identity);
        SkipPlatformsVFXObject.GetComponent<ParticleSystem>().Play();
        GetComponent<Rigidbody>().isKinematic = true;
        StartCoroutine(MoveTheBallInCircle(CentralPoint, RadiusSquared, PlatformSpawner.GetPlatforms()[DestinationPlatformIndex].GetComponent<Transform>().position.y, SkipPlatformsVFXObject));
        audioScript.PlaySkipBoard();
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
   private IEnumerator MoveTheBallInCircle(Vector2 CentralPoint, float RadiusSquared, float FinalYPosition,GameObject SkipPlatformsVFX)
    {
     
        yield return new WaitForFixedUpdate();
      

            //   Vector3 PreviousPosition = transform.position;//just for particles system
            transform.position += new Vector3(0, 0, 5f * Time.deltaTime);
      
            transform.position = new Vector3(transform.position.x,CentralPoint.y + Mathf.Sqrt(RadiusSquared - Mathf.Pow(transform.position.z - CentralPoint.x, 2)), transform.position.z);
            
        if (transform.position.y > FinalYPosition+1f)
            {
                StartCoroutine(MoveTheBallInCircle(CentralPoint, RadiusSquared, FinalYPosition,SkipPlatformsVFX));
            }
            else
        {
            Destroy(SkipPlatformsVFXObject);
            SkipPlatformsVFXObject = null;
            GetComponent<Rigidbody>().isKinematic = false;
        }
        // float angle= Vector3.Angle(new Vector3(0, 0, 1), transform.position - SkipPlatformsVFX.GetComponent<Transform>().position);
        float angle = Vector3.SignedAngle(new Vector3(0, 0, 1), transform.position - SkipPlatformsVFX.GetComponent<Transform>().position, new Vector3(1, 0, 0)); 
               SkipPlatformsVFX.GetComponent<Transform>().position = transform.position;
               SkipPlatformsVFX.GetComponent<Transform>().rotation = Quaternion.Euler(angle-180, 0, 0);
    }
}
