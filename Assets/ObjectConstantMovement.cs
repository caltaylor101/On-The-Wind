using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectConstantMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Rigidbody thisRigidbody;
    [SerializeField] private PlayerController playerController;
    [SerializeField] public float speed = 2f;
    [SerializeField] public float upwardSpeed = 2f;
    [SerializeField] public float maxVelocity = 20f;
    [SerializeField] public float maxSpeed = 20f;

    [SerializeField] private float dandelionMaxHeight;
    [SerializeField] private float dandelionMinHeight;
    [SerializeField] private float dandelionMidHeight;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float dandelionMaxLeft;
    [SerializeField] private float dandelionForceLeft;
    [SerializeField] private float dandelionMaxRight;
    [SerializeField] private float dandelionForceRight;
    [SerializeField] private float distanceFromPlayer;
    public bool targeted = false;
    public bool repositionDandelion = false;
    void Start()
    {
        thisRigidbody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if (playerController)
        {
            ConstantMovement();
        }
    }

    private void ConstantMovement()
    {

        // this allows consistent rotation and resets the dandelion to a starting position after a set time. 
        /* if (restoreRotation)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, originalRotation, Time.deltaTime * rotateSpeed);

            //Debug.Log("Z difference" + (FindDifference(transform.rotation.z, originalRotation.z)));
            //Debug.Log("y difference" + (FindDifference(transform.rotation.y, originalRotation.y)));
            //Debug.Log("x difference" + (FindDifference(transform.rotation.x, originalRotation.x)));
            if ((FindDifference(transform.rotation.z, originalRotation.z) < .1f) && (FindDifference(transform.rotation.x, originalRotation.x) < .1f) && (FindDifference(transform.rotation.y, originalRotation.y) < .1f))
            {
                transform.rotation = originalRotation;
            }
            thisRigidbody.freezeRotation = true;
            if (transform.rotation == originalRotation)
            {
                restoreRotation = false;
                thisRigidbody.freezeRotation = false;

            }
        } */
        if (transform.position.y > dandelionMaxHeight)
        {
            thisRigidbody.useGravity = true;
            thisRigidbody.AddForce(Vector3.up * Time.deltaTime);
        }

        if (transform.position.y < dandelionMaxHeight)
        {
            thisRigidbody.AddForce(Vector3.up * Time.deltaTime * 12);
        }

        if (transform.position.y < dandelionMidHeight)
        {
            thisRigidbody.useGravity = false;
            thisRigidbody.AddForce(Vector3.up * Time.deltaTime * 12);
        }

        if (transform.position.y < dandelionMinHeight)
        {
            thisRigidbody.AddForce(Vector3.up * upwardSpeed);
            transform.position = new Vector3(transform.position.x, dandelionMinHeight, transform.position.z);
        }

        
        if (thisRigidbody.velocity.z < maxVelocity)
        {
            if (thisRigidbody.velocity.z < maxVelocity / 2)
            {
                thisRigidbody.AddForce(Vector3.forward * speed * 7, ForceMode.Acceleration);
            }
            thisRigidbody.AddForce(Vector3.forward * speed, ForceMode.Acceleration);

        }


        transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * rotateSpeed);

        if (transform.position.x < playerController.transform.position.x - dandelionMaxRight)
        {
            thisRigidbody.AddForce(Vector3.right * Time.deltaTime * dandelionForceRight);
        }

        if (transform.position.x > playerController.transform.position.x + dandelionMaxLeft)
        {
            thisRigidbody.AddForce(Vector3.left * Time.deltaTime * dandelionForceLeft);
        }

        if (((transform.position.x < playerController.transform.position.x + .5f) && (transform.position.x > playerController.transform.position.x)) && targeted)
        {
            thisRigidbody.velocity = new Vector3(0, 0, maxVelocity);
            thisRigidbody.angularVelocity = new Vector3(0, 0, maxVelocity);
        }
      
        if (targeted)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerController.transform.position.x, playerController.transform.position.y, transform.position.z), 20);
        }

        if (((transform.position.x < playerController.transform.position.x + .5f) && (transform.position.x > playerController.transform.position.x)) && !repositionDandelion)
        {
            thisRigidbody.velocity = new Vector3(0, 0, maxVelocity);
            thisRigidbody.angularVelocity = new Vector3(0, 0, maxVelocity);
            repositionDandelion = true;
            Invoke("RepositionDandelion", 4f);
        }

        if (transform.position.z < playerController.transform.position.z)
        {
            maxVelocity *= 2;
        }

        if ((transform.position.z - distanceFromPlayer) >= playerController.transform.position.z)
        {
            thisRigidbody.AddForce(Vector3.back * Time.deltaTime * 20);
        }
        else if ((transform.position.z - distanceFromPlayer) < playerController.transform.position.z)
        {
            thisRigidbody.AddForce(Vector3.forward * Time.deltaTime * 20);
        }

        
        //else if (transform.position.z - distanceFromPlayer < playerController.transform.position.z)
        
    }

    private void RepositionDandelion()
    {
        repositionDandelion = false;
    }

}
