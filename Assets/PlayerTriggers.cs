using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggers : MonoBehaviour
{
    // Start is called before the first frame update
    private string windTunnelTag = "WindTunnel";
    [SerializeField] private PlayerController playerController;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.tag == windTunnelTag)
        {
            playerController.maxSpeed *= 3;
            playerController.maxVelocity *= 3;
        }
    }
}
