using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggers : MonoBehaviour
{
    // Start is called before the first frame update
    private string windTunnelTag = "WindTunnel";
    [SerializeField] private PlayerController playerController;
    [SerializeField] private float windPower;
    private bool windTunnelEnter = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider trigger)
    {
        if ((trigger.tag == windTunnelTag) && windTunnelEnter == false)
        {
            windTunnelEnter = true;
            playerController.maxSpeed *= windPower;
            playerController.maxVelocity *= windPower;
        }
    }

    private void OnTriggerExit(Collider trigger)
    {
        if ((trigger.tag == windTunnelTag) && windTunnelEnter == true)
        {
            windTunnelEnter = false;
            playerController.speed /= windPower;
            playerController.maxSpeed /= windPower;
            playerController.maxVelocity /= windPower;
            playerController.thisRigidbody.velocity /= windPower;
        }
    }


}
