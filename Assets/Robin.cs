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
        Debug.Log("HIT PLAYER");
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
