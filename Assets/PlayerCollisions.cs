using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    private string obstacleTag = "Obstacle";
    private bool hitObstacle = false;
    private int baseSpeed = 1;
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
            hitObstacle = true;

            Destroy(collision.gameObject.GetComponent<MeshCollider>());
            playerController.speed /= 3;
            Debug.Log("hit obstacle");
            Invoke("ReturnToBaseSpeed", 2);
            hitObstacle = false;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if ((collision.gameObject.tag == obstacleTag) && !hitObstacle)
        {
            //hitObstacle = false;
            //gameObject.AddComponent<MeshCollider>();
        }
    }

    private void ReturnToBaseSpeed()
    {
        playerController.speed = baseSpeed;
    }
}
