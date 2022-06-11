using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggers : MonoBehaviour
{
    // Start is called before the first frame update
    private string windTunnelTag = "WindTunnel";
    [SerializeField] private PlayerController playerController;
    public float maxSpeedMultiplier = 2;
    public float maxVelocityMultiplier = 2;
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
            playerController.maxSpeed *= maxSpeedMultiplier;
            playerController.maxVelocity *= maxVelocityMultiplier;
        }
    }

    private void OnTriggerExit(Collider trigger)
    {
        if (trigger.tag == windTunnelTag)
        {
            playerController.maxSpeed /= maxSpeedMultiplier;
            playerController.maxVelocity /= maxVelocityMultiplier;
        }
    }
}
