using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [Header("Level Difficulty Control")]
    public int ClockWiseSpeedMultiplier;
    public int CounterClockWiseSpeedMultiplier;
    public int JumpVelocityMultiplier;


    private float ClockWiseSpeed=0.1f;
    private float CounterClockWiseSpeed=3f;
    private float JumpVelocity =10f;
   
    private ContactPoint ContactPoint;
    private bool IsInContactWithBall = false;
    private Rigidbody BallRigidbody;
    private Transform BallTransform;

    private float Zposition = 0f;
    private bool isMoving = true;
   
    
    void Update()
    {
      
        if (IsInContactWithBall)
        {
          
            if (transform.rotation.eulerAngles.x > 180)
            {
                BallTransform.position = new Vector3(BallTransform.position.x, BallTransform.position.y, Zposition);
                isMoving = false;
            }
            else
            {
                if (isMoving == false)
                {
                    BallRigidbody.velocity = new Vector3(0, 0, 0);
                    BallRigidbody.angularVelocity = new Vector3(0, 0, 0);
                    isMoving = true;
                }
                
                Zposition = BallTransform.position.z;
            }
             
        }
       
      

        if (Input.GetKey(KeyCode.Space))
        {
            if (IsInContactWithBall)
            {
                float JumpMagnitude;
              /*  if (transform.rotation.eulerAngles.x > 5 && transform.rotation.eulerAngles.x < 180)
                {
                    JumpMagnitude = JumpVelocity * Mathf.Clamp(2*Mathf.Abs(transform.position.y - BallTransform.position.y), 0.1f, 1f);
                }
                else
                {
                    JumpMagnitude = 0f;
                }*/
                if(BallTransform.position.z>transform.position.z && BallTransform.position.z < (transform.position.z + 2.5f))
                    {
                    float value = Mathf.Abs(BallTransform.position.z - transform.position.z);
                    JumpMagnitude = JumpVelocity * Mathf.Clamp(value * Mathf.Abs(transform.position.y - BallTransform.position.y), 0.1f, 1f);
                }
                else
                {
                    JumpMagnitude = 0f;
                }
                Vector3 JumpVector = JumpMagnitude * (-ContactPoint.normal);
                JumpVector.x = BallRigidbody.velocity.x;
                JumpVector.z=BallRigidbody.velocity.z;
                
                BallRigidbody.velocity = JumpVector;

            }
           
              GetComponent<Transform>().RotateAroundLocal(GetComponent<Transform>().right, -CounterClockWiseSpeed * Time.deltaTime);
            
        }
        else
        {
            if (transform.rotation.eulerAngles.x > 180)
            {
                GetComponent<Transform>().RotateAroundLocal(GetComponent<Transform>().right, 20f*ClockWiseSpeed * Time.deltaTime);
            }
            else
            {
                GetComponent<Transform>().RotateAroundLocal(GetComponent<Transform>().right, ClockWiseSpeed * Time.deltaTime);
            }
            
        }
   

    }

    private void OnCollisionEnter(Collision collision)
    {
        IsInContactWithBall = true;
        BallRigidbody = collision.collider.attachedRigidbody;
        BallTransform = collision.collider.transform;
        Zposition = BallTransform.position.z;
        ContactPoint = collision.GetContact(0); 
    }
    private void OnCollisionExit(Collision collision)
    {
        IsInContactWithBall = false;
        BallRigidbody = null;
        BallTransform = null;
    }
   
}
