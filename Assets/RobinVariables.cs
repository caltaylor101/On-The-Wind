using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobinVariables : MonoBehaviour
{

    public GameObject spotlight;
    //public GameObject targetDandelion;
    public float speed = 15;
    public float maxSpeed = 50;
    public GameObject backObject;

    public GameObject robinPrefab;
    public GameObject player;

    [SerializeField] private float robinStartTime = 5;
    [SerializeField] private float robinMaxTime = 10;
    public bool robinSpawned = false;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CreateRobin", robinStartTime, Random.Range(1, robinMaxTime));
        CreateRobin();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void CreateRobin()
    {
        //if (!robinSpawned)
        //{
            robinSpawned = true;
            speed = Random.Range(speed, maxSpeed);
            Instantiate(robinPrefab, new Vector3(player.transform.position.x + Random.Range(-5, 5), player.transform.position.y + Random.Range(8, 15), player.transform.position.z + Random.Range(150, 200)), Quaternion.identity);
        //}
    }
}
