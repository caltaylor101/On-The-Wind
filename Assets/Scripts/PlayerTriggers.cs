using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggers : MonoBehaviour
{
    [SerializeField] private GameObject part1;
    [SerializeField] private GameObject part3;
    // Start is called before the first frame update
    private string windTunnelTag = "WindTunnel";
    private string verticalWindTunnelTag = "VerticalWindTunnel";
    [SerializeField] private PlayerController playerController;
    [SerializeField] private float windPower;
    [SerializeField] private float verticalWindPower;
    [SerializeField] private RuntimeAnimatorController prologueScene;
    private bool windTunnelEnter = false;
    private bool verticalWindTunnelEnter = false;

    private bool animationStart = false;
    private bool destinationTriggered = false;
    private Vector3 destination = new Vector3(-11.56f, 12.32f, 94.34f);

    [SerializeField] GameObject backObject;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (destinationTriggered)
        {
            destinationTriggered = false;
            Animation1Start();
        }


    }

    private void LateUpdate()
    {
        if (animationStart)
        {
            //Invoke("AnimationForce", 4);
            //playerController.animatorNull = false;
            float delta = 5 * Time.deltaTime;
            Vector3 currentPosition = transform.position;
            Vector3 nextPosition = Vector3.MoveTowards(currentPosition, destination, delta);
            playerController.speed *= 0;
            transform.position = nextPosition;
            if (currentPosition == destination)
            {
                Debug.Log("DESTINATION TRIGGERED");
                destinationTriggered = true;
                animationStart = false;
            }
        }
    }

    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.tag == "MaxHeightTrigger")
        {
            
            playerController.maxHeightTrigger = trigger.gameObject.GetComponent<HeightTriggerVariables>().maxHeightTrigger;
        }
        //if ((trigger.tag == windTunnelTag) && windTunnelEnter == false)
        if ((trigger.tag == windTunnelTag))
        {
            //windTunnelEnter = true;
            playerController.maxSpeed *= windPower;
            playerController.maxVelocity *= windPower;
        }

        if ((trigger.tag == verticalWindTunnelTag) && verticalWindTunnelEnter == false)
        {
            Debug.Log("Windy!");
            //windTunnelEnter = true;
            playerController.thisRigidbody.AddForce(Vector3.up * verticalWindPower);
            //playerController.maxVelocity *= verticalWindPower;
        }
        if ((trigger.tag == "AnimationTrigger1"))
        {
            if (!destinationTriggered)
            {
                animationStart = true;
            }
            playerController.thisRigidbody.velocity *= 0;
            playerController.speed *= 0;
            playerController.animationDelay = 1;
            playerController.newAnimationBool = true;
            playerController.thisRigidbody.useGravity = false;
        }

        if (trigger.tag == "FluffCollectable")
        {
            Destroy(trigger.gameObject);
            playerController.stamina += 10;
        }

        if (trigger.tag == "LevelLoad")
        {
            part1.SetActive(false);
            part3.SetActive(true);
        }

        if (trigger.tag == "Speed")
        {
            playerController.baseSpeed *= 2;
            playerController.maxVelocity *= 2;
            Destroy(part1);
            Destroy(part3);
            Destroy(GameObject.Find("Part2"));
        }

        if (trigger.gameObject.tag == "Bird")
        {
            Destroy(gameObject.GetComponent<Rigidbody>());
            Destroy(gameObject.GetComponent<MeshCollider>());
            Destroy(gameObject.GetComponent<CapsuleCollider>());
            Destroy(gameObject.GetComponent<MeshRenderer>());
            Destroy(gameObject.GetComponent<PlayerTriggers>());
            Destroy(gameObject.GetComponent<PlayerController>());
            Destroy(gameObject.GetComponent<MeshRenderer>());

            gameObject.transform.parent = trigger.gameObject.transform.parent;
            transform.localPosition = new Vector3(1.484f, 2.728f, 2.21f);
            // trigger.transform.Rotate(new Vector3(0, 180, 88.644f));
            //trigger.transform.rotation = Quaternion.Euler(0, 180, 88.644f);
            transform.localRotation = Quaternion.Euler(0, 180, 93.021f);
            //new Quaternion(0, 180, 88.644f, 0);
            backObject.transform.position = new Vector3(backObject.transform.position.x, 100, backObject.transform.position.z);
        }
    }

    private void Animation1Start()
    {
        //playerController.animatorNull = false;

        if (playerController.thisRigidbody.useGravity)
        {
            playerController.thisRigidbody.useGravity = false;
        }
        GetComponent<Rigidbody>().useGravity = false;
        gameObject.AddComponent<Animator>();
        
        GetComponent<Animator>().runtimeAnimatorController = prologueScene;
        playerController.newAnimationBool = false;
        playerController.animatorNull = false;
        playerController.speed = 1;
    }

    private void AnimationForce()
    {
        playerController.thisRigidbody.AddForce(Vector3.forward * 5);
        playerController.thisRigidbody.useGravity = true;

    }


    private void OnTriggerExit(Collider trigger)
    {
        //if ((trigger.tag == windTunnelTag) && windTunnelEnter == true)
        if ((trigger.tag == windTunnelTag))
        {
            windTunnelEnter = false;
            playerController.speed /= windPower;
            playerController.maxSpeed /= windPower;
            playerController.maxVelocity /= windPower;
            playerController.thisRigidbody.velocity /= windPower;
        }
    }


}
