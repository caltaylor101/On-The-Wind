using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Robin : MonoBehaviour
{
    public GameObject[] dandelionList;
    [SerializeField]private GameObject spotlight;
    public GameObject targetDandelion;
    private bool runAttack = false;
    [SerializeField] private float speed = 15;
    [SerializeField] private GameObject backObject;


    public GameObject gameRun;
    public GameObject player;
    public RobinVariables robinVariables;
    // Start is called before the first frame update

    private void Awake()
    {
        player = GameObject.Find("Player");
        if (!player)
        {
            player = GameObject.Find("TestPlayer");
        }
        gameRun = GameObject.Find("GameRun");
        robinVariables = gameRun.GetComponent<RobinVariables>();
        spotlight = robinVariables.spotlight;
        //targetDandelion = robinVariables.targetDandelion;
        speed = robinVariables.speed;
        backObject = robinVariables.backObject;
        transform.rotation = Quaternion.Euler(0, 180, 0);

    }
    void Start()
    {
        dandelionList = GameObject.FindGameObjectsWithTag("OtherDandelion");
        if (dandelionList.Length >= 1)
        {
            Attack(AcquireTarget(dandelionList));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (runAttack)
        {
            Attack(targetDandelion);
        }

        if (transform.position.z + 3f < player.transform.position.z)
        {
            Destroy(targetDandelion);
            Destroy(gameObject);
        }
    }

    //public void StartAttack()

    public void Attack(GameObject target)
    {
        if (target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);
        }
        else
        {
            MoveBackAndDelete();
        }
    }

    public void MoveBackAndDelete()
    {
        transform.position = Vector3.MoveTowards(transform.position, backObject.transform.position, Time.deltaTime * speed);

    }

    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject == targetDandelion)
        {
            Destroy(trigger.gameObject);
            Invoke("DestroyRobin", 5);
            
        }
        if (trigger.tag == "BackObject")
        {
            robinVariables.robinSpawned = false;
            Destroy(gameObject);
        }
        /*
        if (trigger.gameObject.tag == "Player")
        {
            Destroy(trigger.gameObject.GetComponent<Rigidbody>());
            Destroy(trigger.gameObject.GetComponent<MeshCollider>());
            Destroy(trigger.gameObject.GetComponent<CapsuleCollider>());
            Destroy(trigger.gameObject.GetComponent<MeshRenderer>());
            Destroy(trigger.gameObject.GetComponent<PlayerTriggers>());
            Destroy(trigger.gameObject.GetComponent<PlayerController>());
            Destroy(trigger.gameObject.GetComponent<MeshRenderer>());

            trigger.transform.parent = gameObject.transform;
            trigger.transform.localPosition = new Vector3(1.484f, 2.728f, 2.21f);
            // trigger.transform.Rotate(new Vector3(0, 180, 88.644f));
            //trigger.transform.rotation = Quaternion.Euler(0, 180, 88.644f);
            trigger.transform.localRotation = Quaternion.Euler(0, 180, 93.021f);
            //new Quaternion(0, 180, 88.644f, 0);
        }*/

    }

    private void DestroyRobin()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnCollisionEnter(Collision collision)
    {
        /*if (collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject.GetComponent<Rigidbody>());
            Destroy(collision.gameObject.GetComponent<MeshCollider>());
            Destroy(collision.gameObject.GetComponent<CapsuleCollider>());
            Destroy(collision.gameObject.GetComponent<MeshRenderer>());
            Destroy(collision.gameObject.GetComponent<PlayerTriggers>());
            Destroy(collision.gameObject.GetComponent<PlayerController>());
            Destroy(collision.gameObject.GetComponent<MeshRenderer>());

            collision.transform.parent = gameObject.transform;
            collision.transform.localPosition = new Vector3(1.484f, 2.728f, 2.21f);
            // collision.transform.Rotate(new Vector3(0, 180, 88.644f));
            //collision.transform.rotation = Quaternion.Euler(0, 180, 88.644f);
            collision.transform.localRotation = Quaternion.Euler(0, 180, 93.021f);
            //new Quaternion(0, 180, 88.644f, 0);
        }*/
    }

    private GameObject AcquireTarget(GameObject[] list)
    {
        int randomNumber = Random.Range(1, (list.Length - 1));
        spotlight.GetComponent<DandelionSpotlight>().targetDandelion = list[randomNumber];
        list[randomNumber].GetComponent<ObjectConstantMovement>().targeted = true;
        targetDandelion = list[randomNumber];
        runAttack = true;
        return list[randomNumber];
    }



}
