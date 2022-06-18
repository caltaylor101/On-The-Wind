using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    private string obstacleTag = "Obstacle";
    private bool hitObstacle = false;

    private string bounceTag = "Bounce";
    private string forwardBounceTag = "ForwardBounce";
    private string backwardBounceTag = "BackwardBounce";
    private int baseSpeed = 1;

    [SerializeField] private float forwardBounceForce = 35;
    [SerializeField] private float bounceForce = 80;
    [SerializeField] private float backwardBounceForce = 35;
    [SerializeField] AudioSource mushroomBounce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {

        

        if ((collision.gameObject.tag == obstacleTag) && !hitObstacle)
        {
            PlayerHitObstacle(collision);
        }

        if ((collision.gameObject.tag == forwardBounceTag))
        {
            playerController.thisRigidbody.AddForce(Vector3.forward * forwardBounceForce);
            playerController.thisRigidbody.AddForce(Vector3.up * forwardBounceForce);
            playerController.stamina = 100;
            mushroomBounce.Play();
        }
        if ((collision.gameObject.tag == bounceTag))
        {
            playerController.thisRigidbody.AddForce(Vector3.up * bounceForce);
            playerController.stamina = 100;
            mushroomBounce.Play();

        }

        if ((collision.gameObject.tag == backwardBounceTag))
        {
            playerController.thisRigidbody.AddForce(Vector3.back * backwardBounceForce);
            playerController.thisRigidbody.AddForce(Vector3.up * backwardBounceForce);
            playerController.stamina = 100;
            mushroomBounce.Play();

        }

    }

    
    private void OnCollisionExit(Collision collision)
    {
    }

    private void ReturnToBaseSpeed()
    {
        playerController.speed = baseSpeed;
    }

    private void PlayerHitObstacle(Collision collision)
    {
        hitObstacle = true;
        Destroy(collision.gameObject.GetComponent<MeshCollider>());
        playerController.speed /= 3;
        Debug.Log("hit obstacle");
        Invoke("ReturnToBaseSpeed", 2);
        hitObstacle = false;
    }

}
