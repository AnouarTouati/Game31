using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [HideInInspector]
    public Transform DownForcePosition;
    [HideInInspector]
    public float DownForceMagnitude;
    [HideInInspector]
    public float UpForceMagnitude;

    private float ClockWiseSpeed=0.1f;
    private float CounterClockWiseSpeed=1f;
    private float JumpVelocity =70f;
    private ContactPoint ContactPoint;
    private bool IsInContactWithBall = false;
    private Rigidbody BallRigidbody;
    private Transform BallTransform;

    public float Zposition = 0f;
    private bool isMoving = true;
    private bool JumpedFromThisPlatform;
    
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
                if (transform.rotation.eulerAngles.x > 3 && transform.rotation.eulerAngles.x < 180)
                {
                    JumpMagnitude = JumpVelocity * Mathf.Clamp(Mathf.Abs(transform.position.y - BallTransform.position.y), 0.1f, 1f);

                }
                else
                {
                    JumpMagnitude = 0f;
                }
                Debug.Log("Jump Magnitude = " + JumpMagnitude);
                Vector3 JumpVector = JumpMagnitude * (-ContactPoint.normal);
                JumpVector.x = BallRigidbody.velocity.x;
                JumpVector.z=BallRigidbody.velocity.z;

                Debug.Log("JumpVector = " + JumpVector);
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

    private void MoveUsingPhysics()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            Vector3 UpForceVector = UpForceMagnitude * DownForcePosition.up;
            Debug.Log("UpForceVector = " + UpForceVector);
            GetComponent<Rigidbody>().AddForceAtPosition(UpForceVector, DownForcePosition.position);
        }
        else
        {
            Vector3 DownForceVector = DownForceMagnitude * -DownForcePosition.up;
            Debug.Log("DownForceVector = " + DownForceVector);
            GetComponent<Rigidbody>().AddForceAtPosition(DownForceVector, DownForcePosition.position);
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
        Debug.Log("Collision Exit");
    }
}
