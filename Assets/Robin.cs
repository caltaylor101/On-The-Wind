using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robin : MonoBehaviour
{
    public GameObject[] dandelionList;
    [SerializeField]private GameObject spotlight;
    public GameObject targetDandelion;
    private bool runAttack = false;
    [SerializeField] private float speed = 15;
    [SerializeField] private GameObject backObject;
    // Start is called before the first frame update
    void Start()
    {
        dandelionList = GameObject.FindGameObjectsWithTag("OtherDandelion");
        Attack(AcquireTarget(dandelionList));
    }

    // Update is called once per frame
    void Update()
    {
        if (runAttack)
        {
            Attack(targetDandelion);
        }
    }

    //public void StartAttack()

    public void Attack(GameObject target)
    {
        if (target)
        {
            Debug.Log(target);
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
            Debug.Log("Destriyed Dandelion");
            Destroy(trigger.gameObject);
        }
        if (trigger.tag == "BackObject")
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject.GetComponent<Rigidbody>());
            Destroy(collision.gameObject.GetComponent<MeshCollider>());
            Destroy(collision.gameObject.GetComponent<CapsuleCollider>());
            Destroy(collision.gameObject.GetComponent<MeshRenderer>());
            Destroy(collision.gameObject.GetComponent<PlayerTriggers>());
            Destroy(collision.gameObject.GetComponent<PlayerController>());
            Destroy(collision.gameObject.GetComponent<MeshRenderer>());

            collision.transform.parent = gameObject.transform;
            collision.transform.localPosition = new Vector3(1.49f, 4.37f, .41f);
            // collision.transform.Rotate(new Vector3(0, 180, 88.644f));
            //collision.transform.rotation = Quaternion.Euler(0, 180, 88.644f);
            collision.transform.localRotation = Quaternion.Euler(0, 180, 88.644f);


            //new Quaternion(0, 180, 88.644f, 0);
        }
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
